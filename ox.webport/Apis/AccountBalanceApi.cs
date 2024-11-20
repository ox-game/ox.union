using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using OX.Bapps;
using OX.BMS;
using OX.IO;
using OX.Ledger;
using OX.Mix.ApiModels;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OX.WebPort
{
    public class AccountBalanceApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "balance";
        public override ApiAction Build()
        {
            return new AccountBalanceApi();
        }
    }
    public class AccountBalanceApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                var holder = arg.ToScriptHash();
                var balanceStates = BlockIndex.Instance.GetBalanceStates(holder);
                List<ApiAssetBalanceModel> list = new List<ApiAssetBalanceModel>();
                var lastIndex = BlockchainHelper.LastIndex;
                var miningIndex = BlockIndex.Instance.GetSubBlockIndex<MiningBlockIndex>();

                string memberId = "0";
                string portMember = holder.ToAddress();
                string portMemberId = "0";
                var markIndex = BlockIndex.Instance.GetSubBlockIndex<MarkBlockIndex>();
                if (markIndex.MarkMembers.TryGetValue(holder, out var member))
                {
                    memberId = member.MarkMemberId.ToString();
                    if (markIndex.MarkMembers.TryGetValue(member.Request.PortHolder, out var pm))
                    {
                        portMember = member.Request.PortHolder.ToAddress();
                        portMemberId = pm.MarkMemberId.ToString();
                    }
                }
                foreach (var state in balanceStates)
                {
                    if (flag.IsNullOrEmpty() || flag == state.Key.ToString())
                    {
                        string price = "0";
                        var poolAddress = SwapHelper.BuildAssetPoolAddress(state.Value.AssetId);
                        if (state.Value.AssetId == Blockchain.OXC)
                        {
                            price = "1";
                        }
                        else if (miningIndex.LastSwapVolume.TryGetValue(poolAddress, out var svm))
                        {
                            price = svm.Price.ToString("f6");
                        }
                        string IDOPrice = "0";
                        string IDOEndIndex = "0";
                        if (miningIndex.SwapPairs.TryGetValue(poolAddress, out var sp) && sp.IDO.IsNotNull())
                        {
                            IDOPrice = sp.IDO.Price.ToString();
                            IDOEndIndex = sp.SwapPairReply.Stamp.ToString();
                        }
                        var model = new ApiAssetBalanceModel
                        {
                            AssetId = state.Value.AssetId.ToString(),
                            AssetName = state.Value.AssetName,
                            AvailableBalance = state.Value.AvailableBalance.ToString(),
                            MasterBalance = state.Value.MasterBalance.ToString(),
                            TotalBalance = state.Value.TotalBalance.ToString(),
                            Price = price,
                            IDOPrice = IDOPrice,
                            IDOEndIndex = IDOEndIndex,
                            CurrentIndex = lastIndex.ToString(),
                            MarkMemberId = memberId,
                            PortMemberId = portMemberId,
                            PortHolder = portMember
                        };
                        var seedSH = holder.GetMutualLockSeed();
                        if (miningIndex.MutualLockNodes.TryGetValue(seedSH, out var node))
                        {
                            model.MinerType = node.NodeType.ToString();
                            model.RootSeedAddress = node.RootSeedAddress.ToAddress();
                            model.ParentHolder = node.ParentHolder.ToAddress();
                            model.IsEthMap = node.IsEthMap.ToString();
                            if (markIndex.MarkMembers.TryGetValue(node.ParentHolder, out var parentmember))
                            {
                                model.ParentMinerMemberId = parentmember.MarkMemberId.ToString();
                            }
                        }
                        List<ApiAssetBalanceOutput> outputs = new List<ApiAssetBalanceOutput>();
                        foreach (var om in state.Value.OMS)
                        {
                            var aabo = new ApiAssetBalanceOutput
                            {
                                Amount = om.Output.Value.ToString(),
                                LockExpirationIndex = om.LockExpirationIndex.ToString(),
                                IsLockCoin = om.IsLockCoin.ToString(),
                                IsTimeLock = om.IsTimeLock.ToString(),
                                TxId = om.Input.PrevHash.ToString(),
                                N = om.Input.PrevIndex.ToString(),
                                TimeStamp = om.TimeStamp.ToString(),
                                FromHolder = om.FromHolder.ToAddress(),
                            };
                            outputs.Add(aabo);
                            if (markIndex.MarkMembers.TryGetValue(om.FromHolder, out var frommember))
                            {
                                aabo.FromMemberId = frommember.MarkMemberId.ToString();
                            }
                        }
                        model.Outputs = outputs.ToArray();
                        list.Add(model);
                    }
                }
                var json = JsonConvert.SerializeObject(list);
                return controller.Content(json);
            }
            catch
            {
                return controller.StatusCode(500);
            }
        }
        public override IActionResult ActionPost(ControllerBase controller, string arg, string flag)
        {
            return controller.StatusCode(500);
        }
    }

}

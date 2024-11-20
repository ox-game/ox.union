using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using OX.Bapps;
using OX.BMS;
using OX.Mix.ApiModels;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OX.WebPort
{
    public class AccountUtxoApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "utxo";
        public override ApiAction Build()
        {
            return new AccountUtxoApi();
        }
    }
    public class AccountUtxoApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                var holder = arg.ToScriptHash();
                var assetId = UInt256.Parse(flag);
                var state = BlockIndex.Instance.GetAssetUtxoStates(holder, assetId);
                List<ApiAssetUtxoModel> list = new List<ApiAssetUtxoModel>();
                var lastIndex = BlockchainHelper.LastIndex;
                var model = new ApiAssetUtxoModel
                {
                    AssetId = state.AssetId.ToString(),
                    AssetName = state.AssetName,
                    AvailableBalance = state.AvailableBalance.ToString(),
                    MasterBalance = state.MasterBalance.ToString(),
                    TotalBalance = state.TotalBalance.ToString(),
                    CurrentIndex = lastIndex.ToString()
                };
                List<ApiAssetBalanceOutput> outputs = new List<ApiAssetBalanceOutput>();
                foreach (var om in state.OMS)
                {
                    outputs.Add(new ApiAssetBalanceOutput
                    {
                        Amount = om.Output.Value.ToString(),
                        LockExpirationIndex = om.LockExpirationIndex.ToString(),
                        IsLockCoin = om.IsLockCoin.ToString(),
                        IsTimeLock = om.IsTimeLock.ToString(),
                        TxId = om.Input.PrevHash.ToString(),
                        N = om.Input.PrevIndex.ToString(),
                        TimeStamp = om.TimeStamp.ToString(),
                        FromHolder = om.FromHolder.ToAddress(),
                    });
                }
                model.Outputs = outputs.ToArray();
                list.Add(model);
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

using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using OX.Bapps;
using OX.BMS;
using OX.Mix.ApiModels;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.SmartContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using OX.IO;

namespace OX.WebPort
{
    public class AccountClaimApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "claim";
        public override ApiAction Build()
        {
            return new AccountClaimApi();
        }
    }
    public class AccountClaimApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                var lastIndex = BlockchainHelper.LastIndex;
                ApiClaimModel apiClaimModel = new ApiClaimModel
                {
                    CurrentIndex = lastIndex.ToString()
                };
                var holder = arg.ToScriptHash();
                if (BlockIndex.Instance.Account_UTXO_OXS.TryGetValue(holder, out var dic))
                {
                    List<LockOXS> los = new List<LockOXS>();
                    List<LockOXS> selectedlos = new List<LockOXS>();
                    List<CoinReferenceWrapper> claims = new List<CoinReferenceWrapper>();
                    List<LockOXS> unspendlos = new List<LockOXS>();
                    List<string> contractStrings = new List<string>();
                    int c = 0;
                    foreach (var pair in dic)
                    {
                        if (pair.Value.Flag == LockOXSFlag.Spend)
                        {
                            if (c < 20)
                            {
                                if (pair.Value.IsLockAssetTx)
                                {
                                    var Contract = pair.Value.Tx.GetContract();
                                    ContractWrapper cw = new ContractWrapper
                                    {
                                        ParameterList = Contract.ParameterList.Select(m => (byte)m).ToArray(),
                                        Script = Contract.Script
                                    };
                                    contractStrings.Add(cw.ToArray().ToHexString());
                                }
                                claims.Add(new CoinReferenceWrapper { PrevHash = pair.Key.PrevHash.ToString(), PrevIndex = pair.Key.PrevIndex.ToString() });
                                c++;
                                selectedlos.Add(pair.Value);
                            }
                            los.Add(pair.Value);
                        }
                        else if (pair.Value.Flag == LockOXSFlag.Unspend)
                        {
                            unspendlos.Add(pair.Value);
                        }
                    }
                    apiClaimModel.Claims = claims.ToArray();
                    apiClaimModel.Contracts = contractStrings.ToArray();
                    apiClaimModel.SelectedAmount = OXSHelper.CalculateBonusSpend(selectedlos).ToString();
                    apiClaimModel.AvailableAmount = OXSHelper.CalculateBonusSpend(los).ToString();
                    apiClaimModel.UnavailableAmount += OXSHelper.CalculateBonusUnspend(unspendlos, lastIndex + 1);
                }
                var json = JsonConvert.SerializeObject(apiClaimModel);
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

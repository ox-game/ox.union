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
using OX.Cryptography.ECC;
using static Akka.Actor.ProviderSelection;
namespace OX.WebPort
{
    public class DirectSaleOrderApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "order";
        public override ApiAction Build()
        {
            return new DirectSaleOrderApi();
        }
    }
    public class DirectSaleOrderApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                if (ECPoint.TryParse(arg, ECCurve.Secp256r1, out var holder))
                {
                    var casinoSaleIndex = BlockIndex.Instance.GetSubBlockIndex<CasinoBlockIndex>();
                    List<ApiDirectSaleOrderModel> sellList = new List<ApiDirectSaleOrderModel>();
                    foreach (var mlr in casinoSaleIndex.MutualLockRecords.Where(m => holder.Equals(m.Value.Value.MLST.Seller)))
                    {
                        var islock = mlr.Value.Value.MLBT.IsNotNull();
                        var m = new ApiDirectSaleOrderModel
                        {
                            LockAddress = mlr.Key.ToAddress(),
                            Amount = mlr.Value.Value.MLST.Amount.ToString(),
                            AssetId = mlr.Value.Value.MLST.AssetId.ToString(),
                            RedeemDeadline = mlr.Value.Value.MLST.ExpireTimestamp.ToString(),
                            TimeStamp = mlr.Value.Key.TimeStamp.ToString(),
                            IsLock = islock ? "True" : "False",
                            Buyer = mlr.Value.Value.MLST.Buyer.ToString(),
                            Seller = mlr.Value.Value.MLST.Seller.ToString(),
                            ApproveHash = mlr.Value.Value.MLST.ApproveHash.ToString()
                        };

                        var mlst = mlr.Value.Value.MLST;
                        m.MLSTTxId = mlst.Hash.ToString();
                        for (ushort i = 0; i < mlst.Outputs.Length; i++)
                        {
                            var output = mlst.Outputs[i];
                            if (output.ScriptHash == mlr.Key && output.AssetId == mlst.AssetId && output.Value == Fixed8.One * mlst.Amount * 2)
                            {
                                m.MLSTN = i.ToString();
                                break;
                            }
                        }
                        if (islock)
                        {
                            var mlbt = mlr.Value.Value.MLBT;
                            m.MLBTTxId = mlbt.Hash.ToString();
                            for (ushort i = 0; i < mlbt.Outputs.Length; i++)
                            {
                                var output = mlbt.Outputs[i];
                                if (output.ScriptHash == mlr.Key && output.AssetId == mlst.AssetId && output.Value == Fixed8.One * mlst.Amount)
                                {
                                    m.MLBTN = i.ToString();
                                    break;
                                }
                            }
                        }
                        sellList.Add(m);
                    }
                    List<ApiDirectSaleOrderModel> buyList = new List<ApiDirectSaleOrderModel>();
                    foreach (var mlr in casinoSaleIndex.MutualLockRecords.Where(m => holder.Equals(m.Value.Value.MLST.Buyer)))
                    {
                        buyList.Add(new ApiDirectSaleOrderModel
                        {
                            LockAddress = mlr.Key.ToAddress(),
                            Amount = mlr.Value.Value.MLST.Amount.ToString(),
                            AssetId = mlr.Value.Value.MLST.AssetId.ToString(),
                            RedeemDeadline = mlr.Value.Value.MLST.ExpireTimestamp.ToString(),
                            TimeStamp = mlr.Value.Key.TimeStamp.ToString(),
                            IsLock = mlr.Value.Value.MLBT.IsNotNull() ? "True" : "False",
                            Buyer = mlr.Value.Value.MLST.Buyer.ToString(),
                            Seller = mlr.Value.Value.MLST.Seller.ToString(),
                            ApproveHash = mlr.Value.Value.MLST.ApproveHash.ToString()
                        });
                    }
                    var model = new ApiDirectSaleOrderSetModel
                    {
                        SellOrders = sellList.ToArray(),
                        BuyOrders = buyList.ToArray()
                    };
                    var json = JsonConvert.SerializeObject(model);
                    return controller.Content(json);
                }
                else
                {
                    return controller.StatusCode(500);
                }
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

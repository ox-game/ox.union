using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using Org.BouncyCastle.Asn1.X509;
using OX.Bapps;
using OX.BMS;
using OX.Mix.ApiModels;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OX.WebPort
{
    public class MarkOrderApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "order";
        public override ApiAction Build()
        {
            return new MarkOrderApi();
        }
    }
    public class MarkOrderApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                var holder = arg.ToScriptHash();
                var markIndex = BlockIndex.Instance.GetSubBlockIndex<MarkBlockIndex>();
                if (markIndex.RecentOrders.TryGetValue(holder, out var dic))
                {
                    if (dic.Any())
                    {
                        List<ApiMarkOrderModel> list = new List<ApiMarkOrderModel>();
                        foreach (var d in dic.OrderByDescending(m => m.Value.TimeStamp))
                        {
                            list.Add(new ApiMarkOrderModel
                            {
                                Term = d.Value.Order.Term.ToString(),
                                ChannelRound = d.Value.Order.ChannelRound.ToString(),
                                TimeStamp = d.Value.TimeStamp.ToString(),
                                EncodedHex = d.Value.Order.EncodedData.ToHexString()
                            });
                        }
                        var json = JsonConvert.SerializeObject(list);
                        return controller.Content(json);
                    }
                }
                return controller.StatusCode(500);
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

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
    public class PortMessageApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "portmessage";
        public override ApiAction Build()
        {
            return new PortMessageApi();
        }
    }
    public class PortMessageApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                List<ApiPortMessageModel> list = new List<ApiPortMessageModel>();
                var markIndex = BlockIndex.Instance.GetSubBlockIndex<MarkBlockIndex>();
                if (uint.TryParse(arg, out var portId) && markIndex.RecentPortMessages.TryGetValue(portId, out var dic))
                {
                    if (dic.Any())
                    {
                        foreach (var d in dic.OrderByDescending(m => m.Value.TimeStamp))
                        {
                            list.Add(new ApiPortMessageModel
                            {
                                TimeStamp = d.Value.TimeStamp.ToString(),
                                Msg = d.Value.Message.Content,
                                PortIName = "带单人"
                            });
                        }
                    }
                }
                if (portId != 8004 && markIndex.RecentPortMessages.TryGetValue(8004, out var dic2))
                {
                    if (dic2.Any())
                    {
                        foreach (var d in dic2.OrderByDescending(m => m.Value.TimeStamp))
                        {
                            list.Add(new ApiPortMessageModel
                            {
                                TimeStamp = d.Value.TimeStamp.ToString(),
                                Msg = d.Value.Message.Content,
                                PortIName = "总单人"
                            });
                        }
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

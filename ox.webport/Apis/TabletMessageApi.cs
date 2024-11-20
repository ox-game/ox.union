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
    public class TabletMessageApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "tabletmessage";
        public override ApiAction Build()
        {
            return new TabletMessageApi();
        }
    }
    public class TabletMessageApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                var msgs = BlockIndex.Instance.GetSubBlockIndex<MarkBlockIndex>().TabletMessages.Values;
                if (msgs.IsNotNullAndEmpty())
                {
                    var selectedMsgs = msgs.OrderByDescending(m => m.TimeStamp).Take(20);
                    if (selectedMsgs.IsNotNullAndEmpty())
                    {
                        List<ApiTabletMessageModel> list = new List<ApiTabletMessageModel>();
                        foreach (var d in selectedMsgs)
                        {
                            list.Add(new ApiTabletMessageModel
                            {
                                TimeStamp = d.TimeStamp.ToString(flag),
                                Msg = d.Message.CnContent
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

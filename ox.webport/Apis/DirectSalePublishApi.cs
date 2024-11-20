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
    public class DirectSalePublishApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "publish";
        public override ApiAction Build()
        {
            return new DirectSalePublishApi();
        }
    }
    public class DirectSalePublishApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            try
            {
                List<ApiDirectSalePublishModel> list = new List<ApiDirectSalePublishModel>();
                var casinoSaleIndex = BlockIndex.Instance.GetSubBlockIndex<CasinoBlockIndex>();
                foreach (var publish in casinoSaleIndex.DirectSalePublishs.OrderByDescending(m => m.Value.N))
                {
                    list.Add(new ApiDirectSalePublishModel
                    {
                        Seller = publish.Value.Tx.From.ToString(),
                        N = publish.Value.N.ToString(),
                        TimeStamp = publish.Value.TimeStamp.ToString(),
                        AssetId = publish.Value.Publish.AssetId.ToString(),
                        Contact = publish.Value.Publish.Contact,
                        Remarks = publish.Value.Publish.Remarks
                    });
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

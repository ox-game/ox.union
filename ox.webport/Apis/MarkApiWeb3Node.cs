using Akka.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using OX.Bapps;
using OX.BMS;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OX.WebPort
{
    public class MarkApiWeb3NodeBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "web3node";
        public override ApiAction Build()
        {
            return new MarkApiWeb3Node();
        }
    }
    public class MarkApiWeb3Node : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg,string flag)
        {
            var casinoIndex = BlockIndex.Instance.GetSubBlockIndex<CasinoBlockIndex>();
            if (casinoIndex.IsNotNull())
            {
                ushort catalog = 0;
                if (arg.IsNotNullAndEmpty() && ushort.TryParse(arg, out var c))
                {
                    catalog = c;
                }
                if (casinoIndex.Web3Nodes.TryGetValue(catalog, out var dic))
                {
                    List<API_Web3Node> nodes = new List<API_Web3Node>();
                    foreach (var d in dic.OrderByDescending(m => m.Value))
                    {
                        nodes.Add(new API_Web3Node { Catalog = catalog.ToString(), Address = d.Key, DateTime = d.Value.ToString() });
                    }
                    var json = JsonConvert.SerializeObject(nodes);
                    return controller.Content(json);
                }
                else
                    return controller.StatusCode(500);

            }
            return controller.StatusCode(500);
        }
        public override IActionResult ActionPost(ControllerBase controller, string arg, string flags)
        {
            return controller.StatusCode(500);
        }
    }
    public class API_Web3Node
    {
        public string Catalog;
        public string Address;
        public string DateTime;
    }
}

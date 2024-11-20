using Akka.IO;
using Akka.Actor;
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
    public class AccountTransactionApiBuilder : ApiActionBuilder
    {
        public override string ApiActionName => "transaction";
        public override ApiAction Build()
        {
            return new AccountTransactionApi();
        }
    }
    public class AccountTransactionApi : ApiAction
    {
        public override IActionResult ActionGet(ControllerBase controller, string arg, string flag)
        {
            return controller.StatusCode(500);
        }
        public override IActionResult ActionPost(ControllerBase controller, string arg, string flag)
        {
            if (controller.Request.Form.TryGetValue("tx", out var v))
            {
                try
                {
                    var k = v.FirstOrDefault();
                    var bs = k.HexToBytes();
                    if (bs.TryAsSerializable<ApiTransactionMessage>(out var message))
                    {
                        var tx = message.Data.DeserilizeTransaction((byte)message.TxKind);
                        if (tx.IsNotNull())
                        {
                            if (tx.Verify(Blockchain.Singleton.CurrentSnapshot, Blockchain.Singleton.MemPool.GetVerifiedTransactions()))
                            {
                                Program.BlockHandler.Tell(tx);                               
                                var lastIndex = BlockchainHelper.LastIndex;
                                foreach (var coin in tx.Inputs)
                                {
                                    BlockIndex.Instance.UnconfirmCoins[coin] = lastIndex + 100;
                                }                               
                                return controller.Content("True");
                            }
                        }
                    }
                }
                catch
                {
                    return controller.StatusCode(500);
                }
            }
            return controller.StatusCode(500);
        }
    }

}

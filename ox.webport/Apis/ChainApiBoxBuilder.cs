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
    public class ChainApiBoxBuilder : ApiBoxBuilder
    {
        public override string ApiBoxName => "chain";
        public override ApiBox Build()
        {
            return new ChainApiBox();
        }
    }
    public class ChainApiBox : ApiBox
    {
        public ChainApiBox()
        {
            this.RegisterApiModule(new AccountAssetApiModuleBuilder());
        }
    }
    public class AccountAssetApiModuleBuilder : ApiModuleBuilder
    {
        public override string ApiModuleName => "accountasset";
        public override ApiModule Build()
        {
            return new AccountAssetApiModule();
        }
    }
    public class AccountAssetApiModule : ApiModule
    {
        public AccountAssetApiModule()
        {
            this.RegisterApiAction(new AccountBalanceApiBuilder());
            this.RegisterApiAction(new AccountTransactionApiBuilder());
            this.RegisterApiAction(new AccountUtxoApiBuilder());
            this.RegisterApiAction(new AccountClaimApiBuilder());
        }
    }
   
}

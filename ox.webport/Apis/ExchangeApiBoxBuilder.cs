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
    public class ExchangeApiBoxBuilder : ApiBoxBuilder
    {
        public override string ApiBoxName => "exchange";
        public override ApiBox Build()
        {
            return new ExchangeApiBox();
        }
    }
    public class ExchangeApiBox : ApiBox
    {
        public ExchangeApiBox()
        {
            this.RegisterApiModule(new DirectSaleApiModuleBuilder());
        }
    }
    public class DirectSaleApiModuleBuilder : ApiModuleBuilder
    {
        public override string ApiModuleName => "directsale";
        public override ApiModule Build()
        {
            return new DirectSaleApiModule();
        }
    }
    public class DirectSaleApiModule : ApiModule
    {
        public DirectSaleApiModule()
        {
            this.RegisterApiAction(new DirectSalePublishApiBuilder());
            this.RegisterApiAction(new DirectSaleOrderApiBuilder());
        }
    }
   
}

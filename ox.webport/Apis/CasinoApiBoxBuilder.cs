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
    public class CasinoApiBoxBuilder : ApiBoxBuilder
    {
        public override string ApiBoxName => "casino";
        public override ApiBox Build()
        {
            return new CasinoApiBox();
        }
    }
    public class CasinoApiBox : ApiBox
    {
        public CasinoApiBox()
        {
            this.RegisterApiModule(new MarkApiModuleBuilder());
        }
    }
    public class MarkApiModuleBuilder : ApiModuleBuilder
    {
        public override string ApiModuleName => "mark";
        public override ApiModule Build()
        {
            return new MarkApiModule();
        }
    }
    public class MarkApiModule : ApiModule
    {
        public MarkApiModule()
        {
            this.RegisterApiAction(new MarkAnswerApiBuilder());
            this.RegisterApiAction(new MarkApiWeb3NodeBuilder());
            this.RegisterApiAction(new MarkMemberApiBuilder());
            this.RegisterApiAction(new MarkOrderApiBuilder());
            this.RegisterApiAction(new TabletMessageApiBuilder());
            this.RegisterApiAction(new PortMessageApiBuilder());
        }
    }
   
}

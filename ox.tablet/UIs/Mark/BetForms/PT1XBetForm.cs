using OX.Cryptography.ECC;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using OX.SmartContract;
using OX.Wallets;
using OX.Wallets.NEP6;
using OX.BMS;
using OX.Tablet.UIs.MarkSix;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.Mark.BetForms
{
    public class PT1XBetForm : ZodiacBetForm
    {
        public override uint LeastCodeNumber => 1;
        public override string GetTitleText()
        {
            var methodSetting = MarkSixBetMethod.PT1X.GetMethodSetting();
            return UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
        }
        public override BitMarkSixBetTarget[] GetBetTargets()
        {
            List<BitMarkSixBetTarget> list = new List<BitMarkSixBetTarget>();
            foreach (PointSwitch ps in GetPointControls())
            {
                if (ps.Checked)
                {
                    var target = new BitMarkSixBetTarget
                    {
                        Method = (byte)MarkSixBetMethod.PT1X,
                        BetPoint = new BetPoint(new byte[] { ps.Value })
                    };
                    list.Add(target);
                }
            }
            return list.ToArray();
        }
    }
}

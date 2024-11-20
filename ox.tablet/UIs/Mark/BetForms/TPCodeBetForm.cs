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
    public class TPCodeBetForm : CodeBetForm
    {
        public override uint LeastCodeNumber => 2;
        public override string GetTitleText()
        {
            var methodSetting = MarkSixBetMethod.TP.GetMethodSetting();
            return UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
        }
        public override BitMarkSixBetTarget[] GetBetTargets()
        {
            List<BitMarkSixBetTarget> list = new List<BitMarkSixBetTarget>();
            List<byte> bs = new List<byte>();
            foreach (PointSwitch ps in GetPointControls())
            {
                if (ps.Checked)
                {
                    bs.Add(ps.Value);
                }
            }
            foreach (var point in Ergodic2(bs.ToArray()))
            {
                list.Add(new BitMarkSixBetTarget { Method = (byte)MarkSixBetMethod.TP, BetPoint = point });
            }
            return list.ToArray();
        }

    }

}

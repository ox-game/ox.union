using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.BMS;
using OX.Cryptography.ECC;
using OX.Casino;
using OX.Ledger;
using Sunny.UI;
using OX.Tablet.UIs.MarkSix;
using Sunny.UI.Win32;
using AntDesign;

namespace OX.Tablet
{
    public partial class MarkSixMainPage : BaseContentPage
    {
        List<MarkSixPlazaForm> Plazas = new List<MarkSixPlazaForm>();
        JointMember JointBanker;
        public MarkSixMainPage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "marksix";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            foreach (var plaza in this.Plazas)
                plaza.HeartBeat(beatContext);
            if (JointBanker.IsNotNull())
                JointBanker.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            foreach (var plaza in this.Plazas)
                plaza.BeforeBlock(block);
            if (JointBanker.IsNotNull())
                JointBanker.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            foreach (var plaza in this.Plazas)
                plaza.OnBlock(block);
            if (JointBanker.IsNotNull())
                JointBanker.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            foreach (var plaza in this.Plazas)
                plaza.AfterBlock(block);
            if (JointBanker.IsNotNull())
                JointBanker.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            foreach (var plaza in this.Plazas)
                plaza.OnClipboardString(messageType, msg);
            if (JointBanker.IsNotNull())
                JointBanker.OnClipboardString(messageType,msg);
        }
        public override void MenuPageSelected()
        {
            foreach (var plaza in this.Plazas)
                plaza.MenuPageSelected();
            if (JointBanker.IsNotNull())
                JointBanker.MenuPageSelected();
        }
        void InitPages()
        {
            foreach (var r in NoneFlagEnumHelper.All<MarkSixRound>())
            {
                var page = new MarkSixPlazaForm(new MarkChannelRound { Channel = BetChannel.MarkSix, Round = (byte)r });
                Plazas.Add(page);
                this.tbc_sections.AddPage(page);
            }
            JointBanker = new JointMember();
            this.tbc_sections.AddPage(JointBanker);
        }
    }
}

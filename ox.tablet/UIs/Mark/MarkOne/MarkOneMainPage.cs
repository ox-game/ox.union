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

namespace OX.Tablet
{
    public partial class MarkOneMainPage : BaseContentPage
    {
        List<MarkOnePlazaForm> Plazas = new List<MarkOnePlazaForm>();
        //OrderHistory orderRecordPage = default;
        //JointBanker JointBanker = default;
        public MarkOneMainPage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "markone";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            foreach (var plaza in this.Plazas)
                plaza.HeartBeat(beatContext);
            //if (orderRecordPage.IsNotNull())
            //    orderRecordPage.HeartBeat(beatContext);
            //if (JointBanker.IsNotNull())
            //    JointBanker.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            foreach (var plaza in this.Plazas)
                plaza.BeforeBlock(block);
            //if (orderRecordPage.IsNotNull())
            //    orderRecordPage.BeforeBlock(block);
            //if (JointBanker.IsNotNull())
            //    JointBanker.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            foreach (var plaza in this.Plazas)
                plaza.OnBlock(block);
            //if (orderRecordPage.IsNotNull())
            //    orderRecordPage.OnBlock(block);
            //if (JointBanker.IsNotNull())
            //    JointBanker.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            foreach (var plaza in this.Plazas)
                plaza.AfterBlock(block);
            //if (orderRecordPage.IsNotNull())
            //    orderRecordPage.AfterBlock(block);
            //if (JointBanker.IsNotNull())
            //    JointBanker.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            foreach (var plaza in this.Plazas)
                plaza.OnClipboardString(messageType, msg);
            //if (orderRecordPage.IsNotNull())
            //    orderRecordPage.OnClipboardString(messageType, msg);
            //if (JointBanker.IsNotNull())
            //    JointBanker.OnClipboardString(messageType, msg);
        }
        public override void MenuPageSelected()
        {
            foreach (var plaza in this.Plazas)
                plaza.MenuPageSelected();
            //if (orderRecordPage.IsNotNull())
            //    orderRecordPage.MenuPageSelected();
            //if (JointBanker.IsNotNull())
            //    JointBanker.MenuPageSelected();
        }
        void InitPages()
        {
            foreach (var r in NoneFlagEnumHelper.All<MarkOneRound>())
            {
                var page = new MarkOnePlazaForm(new MarkChannelRound { Channel = BetChannel.MarkOne, Round = (byte)r });
                Plazas.Add(page);
                this.tbc_sections.AddPage(page);
            }
            //JointBanker = new JointBanker() { Text = UIHelper.LocalString("联合坐庄", "Joint Banker") };
            //this.tbc_sections.AddPage(JointBanker);
            //orderRecordPage = new OrderHistory() { Text = UIHelper.LocalString("订单历史", "Order History") };
            //this.tbc_sections.AddPage(orderRecordPage);
        }
    }
}

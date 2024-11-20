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
using OX.Wallets;
using Sunny.UI.Win32;
using OX.Tablet.Config;

namespace OX.Tablet
{
    public partial class BuryMainPage : BaseContentPage
    {
        BuryBetView BuryBetView;
        BurySummaryView BurySummaryView;
        public BuryMainPage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "bury";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            if (BuryBetView.IsNotNull())
                BuryBetView.HeartBeat(beatContext);
            if (BurySummaryView.IsNotNull())
                BurySummaryView.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            if (BuryBetView.IsNotNull())
                BuryBetView.BeforeBlock(block);
            if (BurySummaryView.IsNotNull())
                BurySummaryView.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            if (BuryBetView.IsNotNull())
                BuryBetView.OnBlock(block);
            if (BurySummaryView.IsNotNull())
                BurySummaryView.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            if (BuryBetView.IsNotNull())
                BuryBetView.AfterBlock(block);
            if (BurySummaryView.IsNotNull())
                BurySummaryView.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }
        public override void MenuPageSelected()
        {
            if (BuryBetView.IsNotNull())
                BuryBetView.MenuPageSelected();
            if (BurySummaryView.IsNotNull())
                BurySummaryView.MenuPageSelected();
        }
        void InitPages()
        {
            this.BuryBetView = new BuryBetView() { Text = UIHelper.LocalString("爆雷", "Bury") };
            this.tbc_sections.AddPage(this.BuryBetView);
            this.BurySummaryView = new BurySummaryView() { Text = UIHelper.LocalString("数据汇总", "Data Summary") };
            this.tbc_sections.AddPage(this.BurySummaryView);
        }
    }
}

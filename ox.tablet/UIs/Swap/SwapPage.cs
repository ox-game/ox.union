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
using OX.Tablet.Config;

namespace OX.Tablet
{
    public partial class SwapPage : BaseContentPage
    {

        ExchangePage ExchangePage = default;
        InGoldPage InGoldPage = default;
        OutGoldPage OutGoldPage = default;
        USDTCastPage USDTCastPage = default;
        public SwapPage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "exchange";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            if (ExchangePage.IsNotNull())
                ExchangePage.HeartBeat(beatContext);
            if (InGoldPage.IsNotNull())
                InGoldPage.HeartBeat(beatContext);
            if (OutGoldPage.IsNotNull())
                OutGoldPage.HeartBeat(beatContext);
            if (USDTCastPage.IsNotNull())
                USDTCastPage.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            if (ExchangePage.IsNotNull())
                ExchangePage.BeforeBlock(block);
            if (InGoldPage.IsNotNull())
                InGoldPage.BeforeBlock(block);
            if (OutGoldPage.IsNotNull())
                OutGoldPage.BeforeBlock(block);
            if (USDTCastPage.IsNotNull())
                USDTCastPage.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            if (ExchangePage.IsNotNull())
                ExchangePage.OnBlock(block);
            if (InGoldPage.IsNotNull())
                InGoldPage.OnBlock(block);
            if (OutGoldPage.IsNotNull())
                OutGoldPage.OnBlock(block);
            if (USDTCastPage.IsNotNull())
                USDTCastPage.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            if (ExchangePage.IsNotNull())
                ExchangePage.AfterBlock(block);
            if (InGoldPage.IsNotNull())
                InGoldPage.AfterBlock(block);
            if (OutGoldPage.IsNotNull())
                OutGoldPage.AfterBlock(block);
            if (USDTCastPage.IsNotNull())
                USDTCastPage.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }
        public override void MenuPageSelected()
        {
            if (ExchangePage.IsNotNull())
                ExchangePage.MenuPageSelected();
            if (InGoldPage.IsNotNull())
                InGoldPage.MenuPageSelected();
            if (OutGoldPage.IsNotNull())
                OutGoldPage.MenuPageSelected();
            if (USDTCastPage.IsNotNull())
                USDTCastPage.MenuPageSelected();
        }
        void InitPages()
        {
            ExchangePage = new ExchangePage() { Text = UIHelper.LocalString("交易", "Exchange") };
            this.tbc_sections.AddPage(ExchangePage);
            InGoldPage = new InGoldPage() { Text = UIHelper.LocalString("场外入金", "OTC Deposit") };
            this.tbc_sections.AddPage(InGoldPage);
            OutGoldPage = new OutGoldPage() { Text = UIHelper.LocalString("场外出金", "OTC Withdrawal") };
            this.tbc_sections.AddPage(OutGoldPage);
            USDTCastPage = new USDTCastPage() { Text = UIHelper.LocalString("USDT铸造", "USDT Casting") };
            this.tbc_sections.AddPage(USDTCastPage);
        }
    }
}

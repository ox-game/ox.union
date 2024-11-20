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
    public partial class DirectSale : BaseContentPage
    {

        MarketPlacePage MarketPlacePage = default;
        SellerPage SellerPage = default;
        BuyerPage BuyerPage = default;
        public DirectSale()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "swap";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            if (MarketPlacePage.IsNotNull())
                MarketPlacePage.HeartBeat(beatContext);
            if (SellerPage.IsNotNull())
                SellerPage.HeartBeat(beatContext);
            if (BuyerPage.IsNotNull())
                BuyerPage.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            if (MarketPlacePage.IsNotNull())
                MarketPlacePage.BeforeBlock(block);
            if (SellerPage.IsNotNull())
                SellerPage.BeforeBlock(block);
            if (BuyerPage.IsNotNull())
                BuyerPage.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            if (MarketPlacePage.IsNotNull())
                MarketPlacePage.OnBlock(block);
            if (SellerPage.IsNotNull())
                SellerPage.OnBlock(block);
            if (BuyerPage.IsNotNull())
                BuyerPage.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            if (MarketPlacePage.IsNotNull())
                MarketPlacePage.AfterBlock(block);
            if (SellerPage.IsNotNull())
                SellerPage.AfterBlock(block);
            if (BuyerPage.IsNotNull())
                BuyerPage.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            if (MarketPlacePage.IsNotNull())
                MarketPlacePage.OnClipboardString(messageType,msg);
            if (SellerPage.IsNotNull())
                SellerPage.OnClipboardString(messageType, msg);
            if (BuyerPage.IsNotNull())
                BuyerPage.OnClipboardString(messageType, msg);
        }
        public override void MenuPageSelected()
        {
            if (MarketPlacePage.IsNotNull())
                MarketPlacePage.MenuPageSelected();
            if (SellerPage.IsNotNull())
                SellerPage.MenuPageSelected();
            if (BuyerPage.IsNotNull())
                BuyerPage.MenuPageSelected();
        }
        void InitPages()
        {
            MarketPlacePage = new MarketPlacePage() { Text = UIHelper.LocalString("卖场", "Market Place") };
            this.tbc_sections.AddPage(MarketPlacePage);
            SellerPage = new SellerPage() { Text = UIHelper.LocalString("我是卖家", "As Seller") };
            this.tbc_sections.AddPage(SellerPage);
            BuyerPage = new BuyerPage() { Text = UIHelper.LocalString("我是买家", "As Buyer") };
            this.tbc_sections.AddPage(BuyerPage);
        }
    }
}

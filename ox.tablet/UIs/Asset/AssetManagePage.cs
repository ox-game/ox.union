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
    public partial class AssetManagePage : BaseContentPage
    {

        ContactSubPage ContactSubPage = default;
        MasterAssetsSubPage MasterAssetsSubPage = default;
        ExchangeAssetsSubPage ExchangeAssetsSubPage = default;

        public AssetManagePage()
        {
            InitializeComponent();
        }
        public static string _moduleKey => "assetmanage";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            if (ContactSubPage.IsNotNull())
                ContactSubPage.HeartBeat(beatContext);
            if (MasterAssetsSubPage.IsNotNull())
                MasterAssetsSubPage.HeartBeat(beatContext);
            if (ExchangeAssetsSubPage.IsNotNull())
                ExchangeAssetsSubPage.HeartBeat(beatContext);

        }
        public override void BeforeBlock(Block block)
        {
            if (ContactSubPage.IsNotNull())
                ContactSubPage.BeforeBlock(block);
            if (MasterAssetsSubPage.IsNotNull())
                MasterAssetsSubPage.BeforeBlock(block);
            if (ExchangeAssetsSubPage.IsNotNull())
                ExchangeAssetsSubPage.BeforeBlock(block);

        }
        public override void OnBlock(Block block)
        {
            if (ContactSubPage.IsNotNull())
                ContactSubPage.OnBlock(block);
            if (MasterAssetsSubPage.IsNotNull())
                MasterAssetsSubPage.OnBlock(block);
            if (ExchangeAssetsSubPage.IsNotNull())
                ExchangeAssetsSubPage.OnBlock(block);

        }
        public override void AfterBlock(Block block)
        {
            if (ContactSubPage.IsNotNull())
                ContactSubPage.AfterBlock(block);
            if (MasterAssetsSubPage.IsNotNull())
                MasterAssetsSubPage.AfterBlock(block);
            if (ExchangeAssetsSubPage.IsNotNull())
                ExchangeAssetsSubPage.AfterBlock(block);

        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {


        }
        public override void MenuPageSelected()
        {
            InitPages();
            if (ContactSubPage.IsNotNull())
                ContactSubPage.MenuPageSelected();
            if (MasterAssetsSubPage.IsNotNull())
                MasterAssetsSubPage.MenuPageSelected();
            if (ExchangeAssetsSubPage.IsNotNull())
                ExchangeAssetsSubPage.MenuPageSelected();

        }
        void InitPages()
        {
            this.tbc_sections.RemoveAllPages();
            MasterAssetsSubPage = new MasterAssetsSubPage() { Text = UIHelper.LocalString("主账户", "Master Account") };
            this.tbc_sections.AddPage(MasterAssetsSubPage);
            if (SecureHelper.IsSeniorRunMode)
            {
                ExchangeAssetsSubPage = new ExchangeAssetsSubPage() { Text = UIHelper.LocalString("汇兑账户", "Exchange Account") };
                this.tbc_sections.AddPage(ExchangeAssetsSubPage);
            }
            ContactSubPage = new ContactSubPage() { Text = UIHelper.LocalString("联系人", "Contacts") };
            this.tbc_sections.AddPage(ContactSubPage);
        }
    }
}

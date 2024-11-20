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
    public partial class SettingPage : BaseContentPage
    {

        CommonPage CommonPage = default;
        AccountPage AccountPage = default;
        AdminPage AdminPage = default;
        PortPage PortPage = default;
        MessagePage MessagePage = default;
        public SettingPage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "swap";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            if (CommonPage.IsNotNull())
                CommonPage.HeartBeat(beatContext);
            if (AccountPage.IsNotNull())
                AccountPage.HeartBeat(beatContext);
            if (AdminPage.IsNotNull())
                AdminPage.HeartBeat(beatContext);
            if (PortPage.IsNotNull())
                PortPage.HeartBeat(beatContext);
            if (MessagePage.IsNotNull())
                MessagePage.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            if (CommonPage.IsNotNull())
                CommonPage.BeforeBlock(block);
            if (AccountPage.IsNotNull())
                AccountPage.BeforeBlock(block);
            if (AdminPage.IsNotNull())
                AdminPage.BeforeBlock(block);
            if (PortPage.IsNotNull())
                PortPage.BeforeBlock(block);
            if (MessagePage.IsNotNull())
                MessagePage.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            if (CommonPage.IsNotNull())
                CommonPage.OnBlock(block);
            if (AccountPage.IsNotNull())
                AccountPage.OnBlock(block);
            if (AdminPage.IsNotNull())
                AdminPage.OnBlock(block);
            if (PortPage.IsNotNull())
                PortPage.OnBlock(block);
            if (MessagePage.IsNotNull())
                MessagePage.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            if (CommonPage.IsNotNull())
                CommonPage.AfterBlock(block);
            if (AccountPage.IsNotNull())
                AccountPage.AfterBlock(block);
            if (AdminPage.IsNotNull())
                AdminPage.AfterBlock(block);
            if (PortPage.IsNotNull())
                PortPage.AfterBlock(block);
            if (MessagePage.IsNotNull())
                MessagePage.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            if (CommonPage.IsNotNull())
                CommonPage.OnClipboardString(messageType, msg);
            if (AccountPage.IsNotNull())
                AccountPage.OnClipboardString(messageType, msg);
            if (AdminPage.IsNotNull())
                AdminPage.OnClipboardString(messageType, msg);
            if (PortPage.IsNotNull())
                PortPage.OnClipboardString(messageType, msg);
            if (MessagePage.IsNotNull())
                MessagePage.OnClipboardString(messageType, msg);        
        }
        public override void MenuPageSelected()
        {
            if (CommonPage.IsNotNull())
                CommonPage.MenuPageSelected();
            if (AccountPage.IsNotNull())
                AccountPage.MenuPageSelected();
            if (AdminPage.IsNotNull())
                AdminPage.MenuPageSelected();
            if (PortPage.IsNotNull())
                PortPage.MenuPageSelected();
            if (MessagePage.IsNotNull())
                MessagePage.MenuPageSelected();         
        }
        void InitPages()
        {
            CommonPage = new CommonPage() { Text = UIHelper.LocalString("通用", "General") };
            this.tbc_sections.AddPage(CommonPage);
            AccountPage = new AccountPage() { Text = UIHelper.LocalString("账户", "Account") };
            this.tbc_sections.AddPage(AccountPage);
            MessagePage = new MessagePage() { Text = UIHelper.LocalString("公告", "Messages") };
            this.tbc_sections.AddPage(MessagePage);           
            if (SecureHelper.IsCasinoSlot())
            {
                AdminPage = new AdminPage() { Text = UIHelper.LocalString("管理", "Admin") };
                this.tbc_sections.AddPage(AdminPage);
            }
            if (SecureHelper.IsPort())
            {
                PortPage = new PortPage() { Text = UIHelper.LocalString("盘口", "Port") };
                this.tbc_sections.AddPage(PortPage);
            }
        }
    }
}

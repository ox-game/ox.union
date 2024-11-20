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
using Nethereum.Model;
using OX.Persistence;

namespace OX.Tablet.FlashMessages
{
    public partial class FlashMessagePage : BaseContentPage
    {

        NoticeSubPage NoticeSubPage = default;
        SupportSubPage SupportSubPage = default;
        UnicastSubPage UnicastSubPage = default;
        SetSemanticSubPage SetSemanticSubPage = default;
        public FlashMessagePage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "flashmessage";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.HeartBeat(beatContext);
            if (SupportSubPage.IsNotNull())
                SupportSubPage.HeartBeat(beatContext);
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.HeartBeat(beatContext);
           
        }
        public override void BeforeBlock(Block block)
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.BeforeBlock(block);
            if (SupportSubPage.IsNotNull())
                SupportSubPage.BeforeBlock(block);
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.BeforeBlock(block);
         
        }
        public override void OnBlock(Block block)
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.OnBlock(block);
            if (SupportSubPage.IsNotNull())
                SupportSubPage.OnBlock(block);
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.OnBlock(block);
           
        }
        public override void AfterBlock(Block block)
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.AfterBlock(block);
            if (SupportSubPage.IsNotNull())
                SupportSubPage.AfterBlock(block);
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.AfterBlock(block);
           
        }
        public override void OnFlashMessage(FlashMessage message)
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.OnFlashMessage(message);
            if (SupportSubPage.IsNotNull())
                SupportSubPage.OnFlashMessage(message);
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.OnFlashMessage(message);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.OnClipboardString(messageType, msg);
            if (SupportSubPage.IsNotNull())
                SupportSubPage.OnClipboardString(messageType, msg);
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.OnClipboardString(messageType, msg);
            
        }
        public override void MenuPageSelected()
        {
            if (NoticeSubPage.IsNotNull())
                NoticeSubPage.MenuPageSelected();
            if (SupportSubPage.IsNotNull())
                SupportSubPage.MenuPageSelected();
            if (UnicastSubPage.IsNotNull())
                UnicastSubPage.MenuPageSelected();
           
        }
        void InitPages()
        {
            NoticeSubPage = new NoticeSubPage() { Text = UIHelper.LocalString("通知", "Notice") };
            this.tbc_sections.AddPage(NoticeSubPage);
            var dmbs = FlashMessageHelper.GetDomain(SecureHelper.MasterAccount.ScriptHash);
            if (dmbs.IsNotNullAndEmpty())
            {
                SupportSubPage = new SupportSubPage() { Text = UIHelper.LocalString("技术支持", "Support") };
                this.tbc_sections.AddPage(SupportSubPage);
                UnicastSubPage = new UnicastSubPage() { Text = UIHelper.LocalString("私聊", "Chat") };
                this.tbc_sections.AddPage(UnicastSubPage);
            }
            else
            {
                SetSemanticSubPage = new SetSemanticSubPage() { Text = UIHelper.LocalString("设置名称", "Set Name") };
                this.tbc_sections.AddPage(SetSemanticSubPage);
            }
        }
    }
}

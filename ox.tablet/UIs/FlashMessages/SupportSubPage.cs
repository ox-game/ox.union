using Akka.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.DirectSales;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Sunny.UI.Win32;
using OX.Tablet.Config;
using OX.Bapps;
using OX.Casino;
using OX.Persistence;
using OX.Tablet.FlashMessages;
using System.IO;
using OX.SmartContract;
using OX.Wallets;
using OX.Cryptography;
using static Akka.Actor.ProviderSelection;

namespace OX.Tablet.FlashMessages
{
    public partial class SupportSubPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        Tuple<UInt256, UnicastTalkLineValue> TP;

        UnicastChatbox chatbox;
        UnicastChatboxInfo cbi;

        public SupportSubPage()
        {
            InitializeComponent();
            var localAccount = SecureHelper.MasterAccount;
            var talkLine = ECDiffieHellmanHelper.ECDHDeriveKeyHash(localAccount.Key, MarkBetAddressHelper.Instance.MarkAdminPublicKey);
            string talkLabel = UIHelper.LocalString("向港澳联合基金会寻求在线帮助", "Seek online assistance from HK & Macau Union Foundation");
            cbi = new UnicastChatboxInfo();
            cbi.TP = new Tuple<UInt256, UnicastTalkLineValue>(talkLine, new UnicastTalkLineValue { Label = talkLabel, Local = localAccount.ScriptHash, Remote = MarkBetAddressHelper.Instance.MarkAdminPublicKey });
            cbi.LocalAccount = localAccount;
           
            cbi.ChatPlaceholder = UIHelper.LocalString("请输入信息...", "Please enter a message...");
            chatbox = new UnicastChatbox(cbi);
            chatbox.Name = "chat_box";
            chatbox.Dock = DockStyle.Fill;
            this.Controls.Add(chatbox);
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {

        }
        public void BeforeBlock(Block block)
        {

        }
        public void OnBlock(Block block)
        {

        }
        public void AfterBlock(Block block)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {
            if (chatbox.IsNotNull())
            {
                chatbox.OnFlashMessage(flashMessage);
            }
        }
        public virtual void MenuPageSelected()
        {

            loadMessages();
        }

        void setColor(UIButton control, Color color)
        {
            control.FillColor = color;
            control.FillColor2 = color;
            control.FillHoverColor = color;
            control.FillPressColor = color;
            control.FillSelectedColor = color;
            control.RectColor = color;
            control.RectHoverColor = color;
            control.RectPressColor = color;
            control.RectSelectedColor = color;
        }



        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }
        void loadMessages()
        {
            this.chatbox.ReloadOldRecords();
        }


    }
}

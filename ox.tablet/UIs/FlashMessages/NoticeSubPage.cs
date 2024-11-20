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

namespace OX.Tablet.FlashMessages
{
    public partial class NoticeSubPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        uint Range = 0;

        public NoticeSubPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("最新通知", "Lastest Notice");
            this.bt_refresh.Text = UIHelper.LocalString("更多", "More");
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
            if (FlashMemoryHelper.FlashHashs.Contains(flashMessage.Hash))
            {
                FlashMemoryHelper.FlashHashs.Enqueue(flashMessage.Hash);
                switch (flashMessage.Type)
                {

                    case FlashMessageType.FlashMulticast:
                        FlashMulticast fm = flashMessage as FlashMulticast;
                        if (fm.IsNotNull())
                        {
                            if (fm.TalkLine == MarkBetAddressHelper.Instance.NoticeTalkLine && fm.Sender.Equals(MarkBetAddressHelper.Instance.MarkAdminPublicKey))
                            {
                                this.DoInvoke(() =>
                                {
                                    //loadMessages();
                                    if (fm.TryDecrypt(MarkBetAddressHelper.Instance.NoticeShareKey, out byte[] plaintText))
                                    {
                                        if (fm.ContentType == FlashMessageContentType.Text)
                                        {
                                            var str = System.Text.Encoding.UTF8.GetString(plaintText);
                                            var c = new NoticeInfo(DateTime.Now.ToTimestamp(), str) { Width = this.pn_pairs.Panel.Width - 10 };
                                            this.pn_pairs.Add(c);
                                            this.pn_pairs.Panel.Controls.SetChildIndex(c, 0);
                                        }
                                    }
                                });
                            }
                        }
                        break;


                }
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

            this.pn_pairs.Clear();
            var rs = SecureHelper.BlockIndex.GetSubBlockIndex<FlashBlockIndex>().GetLastMulticastRecords(MarkBetAddressHelper.Instance.NoticeTalkLine, out Range);
            if (rs.IsNotNullAndEmpty())
            {
                foreach (var r in rs.OrderByDescending(m => m.Value.RecordIndex))
                {
                    if (r.Value.FlashUnicast.TryDecrypt(MarkBetAddressHelper.Instance.NoticeShareKey, out byte[] plaintText))
                    {

                        if (r.Value.FlashUnicast.ContentType == FlashMessageContentType.Text)
                        {
                            var str = System.Text.Encoding.UTF8.GetString(plaintText);
                            var c = new NoticeInfo(r.Value.Timestamp, str) { Width = this.pn_pairs.Panel.Width - 10 };
                            this.pn_pairs.Add(c);
                            this.pn_pairs.Panel.Controls.SetChildIndex(c, int.MaxValue);
                        }
                    }
                }
            }

        }
        void moreMessages()
        {
            if (Range > 0)
            {
                var newRange = Range - 1;
                var newRs = SecureHelper.BlockIndex.GetSubBlockIndex<FlashBlockIndex>().GetMulticastRecords(MarkBetAddressHelper.Instance.NoticeTalkLine, newRange);

                if (newRs.IsNotNullAndEmpty())
                {
                    foreach (var r in newRs.OrderByDescending(m => m.Value.RecordIndex))
                    {
                        if (r.Value.FlashUnicast.TryDecrypt(MarkBetAddressHelper.Instance.NoticeShareKey, out byte[] plaintText))
                        {

                            if (r.Value.FlashUnicast.ContentType == FlashMessageContentType.Text)
                            {
                                var str = System.Text.Encoding.UTF8.GetString(plaintText);
                                var c = new NoticeInfo(r.Value.Timestamp, str) { Width = this.pn_pairs.Panel.Width - 10 };
                                this.pn_pairs.Add(c);
                                this.pn_pairs.Panel.Controls.SetChildIndex(c, int.MaxValue);
                            }
                        }
                    } 
                }

            }

        }
        private void bt_refresh_Click(object sender, EventArgs e)
        {
            moreMessages();
        }

        private void pn_pairs_SizeChanged(object sender, EventArgs e)
        {
            foreach (var c in this.pn_pairs.GetAllControls())
            {
                c.Width = this.pn_pairs.Panel.Width - 10;
            }
        }
    }
}

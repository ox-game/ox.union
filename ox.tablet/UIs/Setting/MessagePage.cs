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

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MessagePage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public MessagePage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("最新公告", "Lastest Messages");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
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
        public void OnFlashMessage(FlashMessage message)
        {

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
            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var directSaleIndex = SecureHelper.BlockIndex.GetSubBlockIndex<DirectSaleBlockIndex>();
                var messages = directSaleIndex.GetAll<UInt256, TabletMessageMerge>(CasinoBizPersistencePrefixes.Tablet_Message);
                if (messages.IsNotNullAndEmpty())
                {
                    foreach (var message in messages.OrderByDescending(m => m.Value.TimeStamp))
                    {
                        this.pn_pairs.Add(new TabletInfo(message.Value) { Width = this.pn_pairs.Panel.Width - 10 });
                    }
                }
            }
        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            loadMessages();
        }

        private void pn_pairs_SizeChanged(object sender, EventArgs e)
        {
            foreach (var c in this.pn_pairs.GetAllControls())
            {
                c.Width = this.pn_pairs.Panel.Width-10;
            }
        }
    }
}

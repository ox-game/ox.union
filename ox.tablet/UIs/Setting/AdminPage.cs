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
using OX.IO;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class AdminPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public AdminPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("发布Web3节点", "Publish Web3 Nodes");
            this.L2.Text = UIHelper.LocalString("发送平板消息", "Send Tablet Message");
            this.bt_refresh.Text = UIHelper.LocalString("添加", "Add");
            this.bt_publish.Text = UIHelper.LocalString("发布", "Publish");
            this.bt_push_message.Text = UIHelper.LocalString("发送", "Send");
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
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }

        public virtual void MenuPageSelected()
        {
            this.lb_nodes.Items.Clear();
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


        private void bt_refresh_Click(object sender, EventArgs e)
        {
            var node = new Web3Node
            {
                Catalog = ushort.Parse(this.tb_catalog.Text),
                NodeAddress = this.tb_IPAddress.Text,
                Port = ushort.Parse(this.tb_port.Text)
            };
            this.lb_nodes.Items.Add(node);
        }

        private void pn_pairs_SizeChanged(object sender, EventArgs e)
        {

        }

        private void bt_push_message_Click(object sender, EventArgs e)
        {
            if (SecureHelper.IsCasinoSlot())
            {
                TabletMessage reply = new TabletMessage
                {
                    MessageType = 0,
                    CnContent = tb_cn.Text,
                    EnContent = tb_en.Text
                };


                var tx = new ReplyTransaction()
                {
                    EdgeVersion = 0x00,
                    BizScriptHash = casino.CasinoMasterAccountAddress,
                    DataType = (byte)CasinoType.TabletMessage,
                    Data = reply.ToArray(),
                    BizNo = 0,
                    To = UInt160.Zero,
                    Attributes = new TransactionAttribute[0],
                    Outputs = new TransactionOutput[0],
                    Inputs = new CoinReference[0]
                };
                var newTx = tx.BuildAndSignNoneOutput(Fixed8.Zero, new AvatarAccount { Contract = SecureHelper.MasterAccount.Contract, Key = SecureHelper.MasterAccount.Key, ScriptHash = SecureHelper.MasterAccount.ScriptHash });
                if (newTx.IsNotNull())
                {
                    Program.BlockHandler.Tell(newTx);
                    foreach (var coin in newTx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(newTx, UIHelper.LocalString("等待确认消息...", "Waiting  confirm message")).ShowDialog();
                }
            }
        }

        private void bt_publish_Click(object sender, EventArgs e)
        {
            if (SecureHelper.IsCasinoSlot())
            {
                List<Web3Node> nodes = new List<Web3Node>();
                foreach (var c in this.lb_nodes.Items)
                {
                    if (c is Web3Node node)
                        nodes.Add(node);
                }
                Web3NodeSet reply = new Web3NodeSet
                {
                    Nodes = nodes.ToArray()
                };


                var tx = new ReplyTransaction()
                {
                    EdgeVersion = 0x00,
                    BizScriptHash = casino.CasinoMasterAccountAddress,
                    DataType = (byte)CasinoType.CasinoWeb3NodePublish,
                    Data = reply.ToArray(),
                    BizNo = 0,
                    To = UInt160.Zero,
                    Attributes = new TransactionAttribute[0],
                    Outputs = new TransactionOutput[0],
                    Inputs = new CoinReference[0]
                };
                var newTx = tx.BuildAndSignNoneOutput(Fixed8.Zero, new AvatarAccount { Contract = SecureHelper.MasterAccount.Contract, Key = SecureHelper.MasterAccount.Key, ScriptHash = SecureHelper.MasterAccount.ScriptHash });
                if (newTx.IsNotNull())
                {
                    Program.BlockHandler.Tell(newTx);
                    foreach (var coin in newTx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(newTx, UIHelper.LocalString("等待确认发布Web3节点...", "Waiting  confirm publish web3 node")).ShowDialog();
                }
            }
        }
    }
}

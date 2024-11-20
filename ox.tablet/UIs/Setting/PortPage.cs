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
    public partial class PortPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public PortPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("盘口消息", "Port Message");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            this.bt_push_message.Text = UIHelper.LocalString("发送", "Send");
            this.lb_msg.Text = UIHelper.LocalString("消息:", "Message:");
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
            reload();
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




        private void pn_pairs_SizeChanged(object sender, EventArgs e)
        {

        }

        private void bt_push_message_Click(object sender, EventArgs e)
        {
            if (SecureHelper.IsPort())
            {
                PortMessage request = new PortMessage
                {
                    MessageType = 0,
                    Flag = 0,
                    Content = this.tb_msg.Text
                };
                List<TransactionOutput> outputs = new List<TransactionOutput>();
                TransactionOutput output = new TransactionOutput()
                {
                    AssetId = Blockchain.OXC,
                    ScriptHash = casino.CasinoMasterAccountAddress,
                    Value = Fixed8.One * 2
                };
                outputs.Add(output);
                var tx = new AskTransaction
                {
                    From = SecureHelper.MasterAccount.Key.PublicKey,
                    Outputs = outputs.ToArray(),
                    DataType = (byte)CasinoType.PortMessage,
                    Data = request.ToArray(),
                    BizScriptHash = casino.CasinoMasterAccountAddress,
                    Inputs = new CoinReference[0],
                };
                var newTx = tx.BuildAndSignOneOXCOutput(Fixed8.Zero);
                if (newTx.IsNotNull())
                {
                    Program.BlockHandler.Tell(newTx);
                    foreach (var coin in newTx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(newTx, UIHelper.LocalString("等待确认盘口消息发送成功...", "Waiting  confirm port message ...")).ShowDialog();
                }
            }
        }

        private void bt_refresh_Click_1(object sender, EventArgs e)
        {
            reload();
        }
        void reload()
        {
            this.pn_msg.Clear();

            var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
            foreach (var m in bmsIndex.GetAll<UInt256, PortMessageMix>(CasinoBizPersistencePrefixes.BMS_Port_Message).OrderByDescending(m => m.Value.TimeStamp))
            {
                this.pn_msg.Add(new PortMessageInfo(m.Value) { Width = this.pn_msg.Panel.Width - 10 });
            }
        }
    }
}

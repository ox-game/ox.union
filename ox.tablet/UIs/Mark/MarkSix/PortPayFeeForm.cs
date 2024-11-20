using OX.Bapps;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OX.BMS;
using Sunny.UI;
using Akka.Actor;
using OX.Network.P2P;
using NBitcoin.Secp256k1;
using AntDesign;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using OX.Tablet.Config;
using OX.Cryptography;
using static OX.BMS.MarkPortPlayerTermKey;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class PortPayFeeForm : UIForm
    {
        MixMarkMember Member;
        MarkPortPlayerTermRecord Record;
        Fixed8 AvailableBalance = Fixed8.Zero;
        AgentFeeNode Node;
        public PortPayFeeForm(MixMarkMember member, MarkPortPlayerTermRecord record)
        {
            this.Member = member;
            this.Record = record;
            InitializeComponent();

        }

        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.lb_bet_amount.Text = UIHelper.LocalString("盘口ID:", "Port ID:");
            this.Text = UIHelper.LocalString($"支付会员{Member.MarkMemberId}佣金", $"Pay fee to member {Member.MarkMemberId}");
            this.bt_NewRoom.Text = UIHelper.LocalString("马上支付", "Pay Now");
            this.lb_balance.Text = UIHelper.LocalString("可用余额:", "Available Balance:");
            this.lb_rate.Text = UIHelper.LocalString("佣金率:", "Fee Rate:");
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
            this.lb_bet_amount.Text = UIHelper.LocalString("下注额:", "Bet Amount:");
            this.lb_fee_amount.Text = UIHelper.LocalString("佣金额:", "Fee Amount:");
            this.lb_term.Text = this.Record.MarkPortPlayerTermKey.Term.ToString();

            this.tb_bet_amount.Text = this.Record.MarkPortPlayerTermValue.TotalBetAmount.ToString("f0");
            this.AvailableBalance = SecureHelper.GetMasterAvailableBalance(Blockchain.OXC);
            this.tb_balance.Text = AvailableBalance.ToString();

            Node = NodeConfig.Instance.AgentFees.FirstOrDefault(m => m.MemberId == this.Member.MarkMemberId.ToString());
            if (Node.IsNull())
            {
                Node = new AgentFeeNode(this.Member.MarkMemberId.ToString(), "5",string.Empty);
                NodeConfig.Instance.AgentFees.Add(Node);
                NodeConfig.Instance.Save();
            }
            this.nd_rate.Value = int.Parse(Node.Rate);
            RefreshFee();
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {

        }
        public void OnBlock(Block block)
        {
        }
        public void BeforeOnBlock(Block block) { }
        public void AfterOnBlock(Block block) { }

        public void OnRebuild() { }

        UInt160 BuildAddress(byte flag)
        {
            var tx = new SlotSideTransaction()
            {
                Channel = 0x02,
                Slot = casino.CasinoMasterAccountPubKey,
                SideType = SideType.PublicKey,
                Data = SecureHelper.MasterAccount.Key.PublicKey.ToArray(),
                Flag = flag,
                AuthContract = Blockchain.SideAssetContractScriptHash,
                Attributes = new TransactionAttribute[0],
                Outputs = new TransactionOutput[0],
                Inputs = new CoinReference[0]
            };
            return tx.GetContract().ScriptHash;
        }

        private void bt_NewRoom_Click(object sender, EventArgs e)
        {

            Register();

        }
        void Register()
        {
            uint amt = uint.Parse(this.tb_fee_amount.Text);
            if (amt == 0) return;
            Fixed8 Amount = Fixed8.One * amt;
            if (this.Record.MarkPortPlayerTermValue.FeeAmount >= Amount) return;
            TransactionOutput output = new TransactionOutput()
            {
                AssetId = Blockchain.OXC,
                ScriptHash = Member.Holder,
                Value = Amount- this.Record.MarkPortPlayerTermValue.FeeAmount
            };
            var tx = new AskTransaction
            {
                From = SecureHelper.MasterAccount.Key.PublicKey,
                Outputs = new[] { output },
                DataType = (byte)CasinoType.MarkPortPayFee,
                Data = this.Record.MarkPortPlayerTermKey.ToArray(),
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
                this.Close();
                new WaitTxForm(newTx, UIHelper.LocalString("等待确认支付佣金...", "Waiting  confirm pay fee ...")).ShowDialog();
            }

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void nd_rate_ValueChanged(object sender, EventArgs e)
        {
            Node.Rate = this.nd_rate.Value.ToString();
            NodeConfig.Instance.Save();
            RefreshFee();
        }

        void RefreshFee()
        {
            var betAmt = uint.Parse(this.tb_bet_amount.Text);
            uint k = ((betAmt * (uint)this.nd_rate.Value) / 100);
            this.tb_fee_amount.Text = k.ToString();
            this.bt_NewRoom.Enabled = AvailableBalance >= Fixed8.One * k;
        }
    }
}

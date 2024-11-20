using Akka.IO;
using Akka.Actor;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Wallets;
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
using static QRCoder.PayloadGenerator.SwissQrCode;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class OutGoldPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        byte ratio = 5;
        Fixed8 AvailableBalance = Fixed8.Zero;
        public OutGoldPage()
        {
            InitializeComponent();

            this.lb_amount.Text = UIHelper.LocalString("出金额:", "Sale Amount:");
            this.lb_usdt_balance.Text = UIHelper.LocalString("可用余额:", "Available Balance:");
            this.st_state.ActiveText = UIHelper.LocalString(OTCStatus.Redeem.StringValue(), OTCStatus.Redeem.EngStringValue());
            this.st_state.InActiveText = UIHelper.LocalString(OTCStatus.Open.StringValue(), OTCStatus.Open.EngStringValue());
            this.rbg_ratio.Text = UIHelper.LocalString($"入金手续费率", "Percentage of deposit procedures");
            this.btn_do_sale.Text = UIHelper.LocalString("申请出金", "Post Sale");
            this.rbg_ratio.SelectedIndex = ratio;
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
        public void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public virtual void MenuPageSelected()
        {
            RefreshState();
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



        void RefreshState()
        {
            this.lb_pool_address.Text = string.Empty;
            this.lb_pool_balance.Text = string.Empty;
            this.tb_amount.Text = "0";
            if (SecureHelper.IsAuthenticated)
            {

                var st = SecureHelper.ExchangeAccount.EthAddress.BuildOTCDealerTransaction();
                var sh = st.GetContract().ScriptHash;
                var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                if (miningIndex.OTCDealers.TryGetValue(sh, out var dealer))
                {
                    this.st_state.Active = dealer.Setting.State == OTCStatus.Redeem;
                    this.rbg_ratio.SelectedIndex = dealer.Setting.InRate;
                }
                this.lb_pool_address.Text = UIHelper.LocalString($"交易池地址:{sh.ToAddress()}", $"OTC Pool Address:{sh.ToAddress()}");
                var account = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(sh);
                if (account.IsNotNull())
                {
                    if (account.Balances.TryGetValue(Mining.USDT_Asset, out Fixed8 balance))
                    {
                        this.lb_pool_balance.Text = UIHelper.LocalString($"交易池余额:{balance.ToString()}", $"OTC Pool Balance:{balance.ToString()}");
                    }
                }
                var balanceStates = SecureHelper.GetMasterBalanceStates();
                if (balanceStates.TryGetValue(Mining.USDT_Asset, out var balanceState))
                {
                    AvailableBalance = balanceState.AvailableBalance;
                    this.tb_balance.Text = AvailableBalance.ToString();
                }
            }
        }


        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void btn_do_sale_Click(object sender, EventArgs e)
        {
            if (Fixed8.TryParse(this.tb_amount.Text, out var amt) && amt > Fixed8.Zero && amt <= AvailableBalance)
            {
                OTCStatus state = this.st_state.Active ? OTCStatus.Redeem : OTCStatus.Open;

                OTCSetting setting = new OTCSetting
                {
                    InRate = this.ratio,
                    OutRate = 0,
                    EthAsset = 0,
                    State = state
                };
                var tx = SecureHelper.ExchangeAccount.EthAddress.BuildOTCDealerTransaction(setting);
                var sh = tx.GetContract().ScriptHash;

                List<CoinReference> inputs = new List<CoinReference>();
                List<AvatarAccount> avatars = new List<AvatarAccount>();
                List<TransactionOutput> outputs = new List<TransactionOutput>();

                outputs.Add(new TransactionOutput
                {
                    ScriptHash = sh,
                    AssetId = Mining.USDT_Asset,
                    Value = amt
                });
                var utxos = SecureHelper.BlockIndex.GetMasterUtxos(Mining.USDT_Asset);
                if (utxos.IsNotNullAndEmpty())
                {
                    List<string> excludedUtxoKeys = new List<string>();
                    if (utxos.SortSearch(amt.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                    {
                        if (remainder > 0)
                            outputs.Add(new TransactionOutput { AssetId = Mining.USDT_Asset, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
                    }
                    foreach (var utxo in selectedUtxos)
                    {
                        inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });

                        if (utxo.IsLockCoin)
                        {
                            LockAssetTransaction lat = new LockAssetTransaction { IsTimeLock = utxo.IsTimeLock, LockExpiration = utxo.LockExpirationIndex, Recipient = SecureHelper.MasterAccount.Key.PublicKey };
                            avatars.Add(LockAssetHelper.CreateAccount(lat.GetContract(), SecureHelper.MasterAccount.Key));
                        }
                        else
                        {
                            avatars.Add(LockAssetHelper.CreateAccount(SecureHelper.MasterAccount.Contract, SecureHelper.MasterAccount.Key));
                        }
                    }
                    tx.Outputs = outputs.ToArray();
                    tx.Inputs = inputs.ToArray();
                    var ecKey = SecureHelper.ExchangeAccount.Key;
                    var signer = new Nethereum.Signer.EthereumMessageSigner();
                    var signMessage = signer.EncodeUTF8AndSign(tx.InputOutputHash.ToArray().ToHexString(), ecKey);
                    tx.Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.EthSignature, Data = signMessage.HexToByteArray() } };
                    tx = LockAssetHelper.Build(tx, avatars.ToArray());
                    if (tx.IsNotNull())
                    {
                        Program.BlockHandler.Tell(tx);
                        foreach (var coin in tx.Inputs)
                        {
                            SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                        }
                        new WaitTxForm(tx, UIHelper.LocalString("等待出金确认...", "Waiting  confirm transaction"), state =>
                        {
                            if (state)
                            {
                                this.RefreshState();
                            }
                        }).ShowDialog();
                    }
                }
            }
        }

        private void rbg_ratio_ValueChanged(object sender, int index, string text)
        {
            ratio = (byte)index;
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            var v = this.uiTrackBar1.Value;
            var b = (decimal)AvailableBalance;
            var m = v * b / 100;
            this.tb_amount.Text = ((uint)m).ToString();
        }
    }
}

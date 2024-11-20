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
using QRCoder;
using OX.SmartContract;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class USDTCastPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        byte ratio = 5;
        Fixed8 AvailableBalance = Fixed8.Zero;
        string pledgeAddress;
        public USDTCastPage()
        {
            InitializeComponent();

            this.bt_cast_record.Text = UIHelper.LocalString("铸造记录", "Cast Record");
            this.bt_destroy_record.Text = UIHelper.LocalString("销毁记录", "Destory Record");
            this.L_cast.Text = UIHelper.LocalString($"铸造USDT", "Cast　USDT");
            this.L_destroy.Text = UIHelper.LocalString($"销毁USDT", "Destory　USDT");
            this.lb_amount.Text = UIHelper.LocalString($"USDT金额:", "USDT Amount:");
            this.lb_usdt_balance.Text = UIHelper.LocalString($"USDT余额:", "USDT Balance:");
            this.bt_do_destroy.Text = UIHelper.LocalString($"马上销毁", "Now Destory");
            this.setColor(this.bt_do_destroy, FocusColor);

            this.rtb_msg.Text = UIHelper.LocalString(
                                              "任何以太坊地址可以在以太坊主网上通过抵押ERC20 USDT来为自己铸造等额的OX-ECO USDT。同时，这种跨链价值转移是双向的，以太坊地址可以通过销毁总额不大于累计铸造额的OX-ECO USDT来赎回自身抵押的等额ERC20 USDT",
                                              "Any Ethereum address can cast an equal amount of OX-ECO USDT for itself on the Ethereum mainnet by mortgaging ERC20 USDT. Meanwhile, this cross chain value transfer is bidirectional, and Ethereum addresses can redeem their pledged ERC20 USDT of equal value by destroying OX-ECO USDT with a total amount not exceeding the cumulative casting amount");
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
            this.tb_amount.Text = "0";
            if (SecureHelper.IsAuthenticated)
            {
                var balanceStates = SecureHelper.GetExchangeBalanceStates();
                if (balanceStates.TryGetValue(Mining.USDT_Asset, out var balanceState))
                {
                    AvailableBalance = balanceState.AvailableBalance;
                    this.tb_balance.Text = AvailableBalance.ToString();
                }
            }
            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                pledgeAddress = miningIndex.GetAnchorMortgageIssuePoolAddress();
                this.lb_pledge_address.Text = UIHelper.LocalString($"ERC20 USDT充值地址:{pledgeAddress}", $"ERC20 USDT Recharge Address:{pledgeAddress}");
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(pledgeAddress, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                this.img_cast.Image = qrCode.GetGraphic(20);
            }
        }


        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_cast_record_Click(object sender, EventArgs e)
        {
            this.lb_Details.Items.Clear();
            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                var CastingRecords = miningIndex.GetAll<AnchorMortgageIssueKey, TransactionOutput>(MiningPersistencePrefixes.AMI_USDTCastRecord, new StringWrapper(SecureHelper.ExchangeAccount.EthAddress.ToLower()));
                foreach (var p in CastingRecords.OrderByDescending(m => m.Key.Timestamp))
                {
                    this.lb_Details.Items.Add($"{p.Key.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}    {p.Value.Value}  USDT");
                }
            }
        }

        private void img_cast_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(pledgeAddress);
            this.ShowSuccessTip(UIHelper.LocalString($"{pledgeAddress} copied", $"{pledgeAddress} 已复制"));
        }

        private void bt_destroy_record_Click(object sender, EventArgs e)
        {
            this.lb_Details.Items.Clear();
            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                var DestroyRecords = miningIndex.GetAll<AnchorMortgageIssueKey, TransactionOutput>(MiningPersistencePrefixes.AMI_USDTDestroyRecord, new StringWrapper(SecureHelper.ExchangeAccount.EthAddress.ToLower()));
                foreach (var p in DestroyRecords.OrderByDescending(m => m.Key.Timestamp))
                {
                    this.lb_Details.Items.Add($"{p.Key.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}    {p.Value.Value}  USDT");
                }
            }
        }
        public Transaction BuildTx()
        {
            Transaction tx = default;

            UInt160 sh = UInt160.Zero;

            if (Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount) && amount >= Fixed8.Zero * 100 && amount <= AvailableBalance)
            {
                List<CoinReference> inputs = new List<CoinReference>();
                Dictionary<UInt160, Contract> contracts = new Dictionary<UInt160, Contract>();
                List<TransactionOutput> outputs = new List<TransactionOutput>();
                TransactionOutput output = new TransactionOutput { AssetId = Mining.USDT_Asset, ScriptHash = sh, Value = amount };
                outputs.Add(output);
                var utxos = SecureHelper.BlockIndex.GetExchangeUtxos(Mining.USDT_Asset);
                if (utxos.IsNotNullAndEmpty())
                {
                    List<string> excludedUtxoKeys = new List<string>();
                    if (utxos.SortSearch(output.Value.GetInternalValue(), excludedUtxoKeys, out EthMapUTXO[] selectedUtxos, out long remainder))
                    {
                        if (remainder > 0)
                            outputs.Add(new TransactionOutput { AssetId = Mining.USDT_Asset, ScriptHash = SecureHelper.ExchangeAccount.MapAddress, Value = new Fixed8(remainder) });
                    }
                    foreach (var utxo in selectedUtxos)
                    {
                        inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });
                        EthereumMapTransaction emt = new EthereumMapTransaction { EthereumAddress = utxo.EthAddress, LockExpirationIndex = utxo.LockExpirationIndex };
                        var c = emt.GetContract();
                        var esh = c.ScriptHash;
                        if (!contracts.ContainsKey(esh))
                            contracts[esh] = c;
                    }
                    tx = new ContractTransaction
                    {
                        Attributes = new TransactionAttribute[0],
                        Outputs = outputs.ToArray(),
                        Inputs = inputs.ToArray(),
                        Witnesses = new Witness[0]
                    };

                    var signer = new Nethereum.Signer.EthereumMessageSigner();
                    var signMessage = signer.EncodeUTF8AndSign(tx.InputOutputHash.ToArray().ToHexString(), SecureHelper.ExchangeAccount.Key);
                    tx.Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.EthSignature, Data = System.Text.Encoding.UTF8.GetBytes(signMessage) } };
                    List<AvatarAccount> avatars = new List<AvatarAccount>();
                    foreach (var c in contracts)
                    {
                        avatars.Add(LockAssetHelper.CreateAccount(c.Value, SecureHelper.MasterAccount.Key));
                    }
                    tx = LockAssetHelper.Build(tx, avatars.ToArray());
                }
            }
            return tx;
        }

        private void bt_do_destroy_Click(object sender, EventArgs e)
        {
            var tx = BuildTx();
            if (tx.IsNotNull())
            {
                Program.BlockHandler.Tell(tx);
                foreach (var coin in tx.Inputs)
                {
                    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                }
                new WaitTxForm(tx, UIHelper.LocalString("等待销毁USDT交易确认...", "Waiting  confirm destroy USDT transaction")).ShowDialog();
            }
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

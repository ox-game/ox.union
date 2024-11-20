using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.IO;
using Akka.Util;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Tablet.Config;
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

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarketPlacePage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        public static Dictionary<UInt256,string> AssetInfo = new Dictionary<UInt256,string>();
        static MarketPlacePage()
        {
            AssetInfo[Blockchain.OXC] = "OXC";
            AssetInfo[Mining.USDT_Asset] = "USDT";
            AssetInfo[Blockchain.OXS] = "OXS";
            AssetInfo[Mining.SLM_Asset] = "SLM";      
        }
        public MarketPlacePage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("最新挂牌", "Lastest Sellers");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            this.lb_asset.Text = UIHelper.LocalString("出售资产:", "Sale Asset:");
            this.lb_contact.Text = UIHelper.LocalString("联系方式:", "Contact:");
            this.lb_mark.Text = UIHelper.LocalString("备注:", "Remarks:");
            this.btn_publish.Text = UIHelper.LocalString("双击挂牌", "2hit-List");
            this.btn_publish.UseDoubleClick = true;
            this.btn_publish.DoubleClick += Btn_publish_DoubleClick;
            this.setColor(this.btn_publish, FocusColor);
        }

        private void Btn_publish_DoubleClick(object sender, EventArgs e)
        {
            var tx = BuildTx();
            if (tx.IsNotNull())
            {
                Program.BlockHandler.Tell(tx);
                foreach (var coin in tx.Inputs)
                {
                    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                }
                new WaitTxForm(tx, UIHelper.LocalString("等待挂单确认...", "Waiting  confirm order transaction")).ShowDialog();
            }
        }
        public Transaction BuildTx()
        {
            AskTransaction at = default;
            var balance = SecureHelper.GetMasterAvailableBalance(Blockchain.OXC);
            if (balance >= Fixed8.One * 10)
            {

                List<TransactionOutput> outputs = new List<TransactionOutput>();
                TransactionOutput output = new TransactionOutput()
                {
                    AssetId = Blockchain.OXC,
                    ScriptHash = casino.CasinoMasterAccountAddress,
                    Value = Fixed8.One * 10
                };
                outputs.Add(output);

                AssetViewModel avm = this.cb_asset.SelectedItem as AssetViewModel;
                DirectSalePublish publish = new DirectSalePublish
                {
                    AssetId = avm.AssetId,
                    Contact = this.rtb_contact.Text.Trim(),
                    Remarks = this.rtb_remarks.Text.Trim()
                };
                if (!Blockchain.Singleton.VerifySlotValidator(casino.CasinoMasterAccountAddress, out AccountState _, out var _, out Fixed8 askFee)) return default;

                if (askFee > Fixed8.Zero)
                {
                    outputs.Add(new TransactionOutput() { AssetId = Blockchain.OXC, ScriptHash = casino.CasinoMasterAccountAddress, Value = askFee });
                }

                List<CoinReference> inputs = new List<CoinReference>();
                List<AvatarAccount> avatars = new List<AvatarAccount>();

                var utxos = SecureHelper.BlockIndex.GetMasterUtxos(Blockchain.OXC);
                if (utxos.IsNotNullAndEmpty())
                {
                    List<string> excludedUtxoKeys = new List<string>();
                    var totalAmt = outputs.Sum(m => m.Value);
                    if (utxos.SortSearch(totalAmt.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                    {
                        if (remainder > 0)
                            outputs.Add(new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
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

                    at = new AskTransaction
                    {
                        From = SecureHelper.MasterAccount.Key.PublicKey,
                        Outputs = outputs.ToArray(),
                        DataType = (byte)CasinoType.DirectSalePublish,
                        Data = publish.ToArray(),
                        BizScriptHash = casino.CasinoMasterAccountAddress,
                        Inputs = inputs.ToArray()
                    };
                    at = LockAssetHelper.Build(at, avatars.ToArray());
                }
            }
            return at;
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
            this.cb_asset.Items.Clear();
            foreach(var ast in AssetInfo)
            {
                this.cb_asset.Items.Add(new AssetViewModel { Name = ast.Value, AssetId = ast.Key });
            }
            this.cb_asset.SelectedIndex = 0;
            ReloadRecord();
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

        void ReloadRecord()
        {
            this.DoInvoke(() =>
            {
                this.pn_pairs.Clear();
                if (SecureHelper.BlockIndex.IsNotNull())
                {
                    var directSaleIndex = SecureHelper.BlockIndex.GetSubBlockIndex<DirectSaleBlockIndex>();
                   var dsps= directSaleIndex.DirectSalePublishs.Values.ToArray();
                    foreach (var publish in dsps.OrderByDescending(m => m.N))
                    {
                        if (AssetInfo.TryGetValue(publish.Publish.AssetId, out var assetName))
                        {
                            DirectSalePublishInfo dspInfo = new DirectSalePublishInfo(this,publish, assetName);
                            this.pn_pairs.Add(dspInfo);
                        }
                    }
                }
            });
        }

        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            ReloadRecord();
        }

    }
}

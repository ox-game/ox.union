using Akka.Actor;
using Akka.IO;
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
using OX.SmartContract;
using static OX.Tablet.NewMutualLockSellerTx;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class BuyerPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public BuyerPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("我的买单", "My buys orders");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            //this.bt_paste_approve.Text = UIHelper.LocalString("粘贴授权", "Paste approve");
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
            //if (messageType == ClipboardMessageType.DirectSaleApprove)
            //{
            //    if (DirectSaleApprove.BuyerTryParser(SecureHelper.MasterAccount.Key, msg, out var approve, out var content) && approve.Buyer.Equals(SecureHelper.MasterAccount.Key.PublicKey))
            //    {
            //        ShowAndReceiptApprove(approve, content);
            //    }
            //}
        }
        public virtual void MenuPageSelected() { ReloadRecord(); }

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
                    foreach (var ml in directSaleIndex.MutualLockRecords.Where(m => !m.Value.Key.IsSeller).OrderByDescending(m => m.Value.Key.TimeStamp))
                    {
                        this.pn_pairs.Add(new MutualLockBuyerInfo(this, ml.Value));
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

        private void bt_paste_approve_Click(object sender, EventArgs e)
        {
            var str = Clipboard.GetText();
            if (DirectSaleApprove.BuyerTryParser(SecureHelper.MasterAccount.Key, str, out var approve, out var content) && approve.Buyer.Equals(SecureHelper.MasterAccount.Key.PublicKey))
            {
                ShowAndReceiptApprove(approve, content);
            }
        }
        void ShowAndReceiptApprove(DirectSaleApprove approve, DirectSaleApproveContent content)
        {
            Clipboard.Clear();
            var mlst = Blockchain.Singleton.GetTransaction(content.MLSTHash);
            if (mlst.IsNotNull() && mlst is MutualLockSellerTransaction tr)
            {
                var sh = tr.GetContract().ScriptHash;
                var mutualLockState = Blockchain.Singleton.CurrentSnapshot.MutualLockStates.TryGet(sh);
                if (mutualLockState.IsNotNull())
                {
                    var tx = BuildTx(mutualLockState, approve, content);
                    if (tx.IsNotNull())
                    {
                        Program.BlockHandler.Tell(tx);
                        //foreach (var coin in tx.Inputs)
                        //{
                        //    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                        //}
                        new WaitTxForm(tx, UIHelper.LocalString("等待确认资产交割...", "Waiting  confirm delivery asset ...")).ShowDialog();
                    }
                }
            }

        }
        public ContractTransaction BuildTx(MutualLockState mutualLockState, DirectSaleApprove approve, DirectSaleApproveContent content)
        {
            var mlst = mutualLockState.SellerTx;
            var sh = mlst.GetContract().ScriptHash;
            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            Fixed8 total = Fixed8.Zero;
            for (ushort i = 0; i < mlst.Outputs.Length; i++)
            {
                var output = mlst.Outputs[i];
                if (output.ScriptHash == sh)
                {
                    inputs.Add(new CoinReference { PrevHash = mlst.Hash, PrevIndex = i });
                    avatars.Add(LockAssetHelper.CreateAccount(mlst.GetContract(), SecureHelper.MasterAccount.Key));
                    total += output.Value;
                    break;
                }
            }
            if (mutualLockState.Locked && mutualLockState.BuyerTxHash.IsNotNull() && mutualLockState.BuyerTxHash != UInt256.Zero)
            {
                var gt = Blockchain.Singleton.GetTransaction(mutualLockState.BuyerTxHash);
                if (gt.IsNotNull() && gt is MutualLockBuyerTransaction mlbt)
                {
                    for (ushort i = 0; i < mlbt.Outputs.Length; i++)
                    {
                        var output = mlbt.Outputs[i];
                        if (output.ScriptHash == sh)
                        {
                            inputs.Add(new CoinReference { PrevHash = mlbt.Hash, PrevIndex = i });
                            //avatars.Add(LockAssetHelper.CreateAccount(mlst.GetContract(), SecureHelper.MasterAccount.Key));
                            total += output.Value;
                            break;
                        }
                    }
                }
            }
            outputs.Add(new TransactionOutput { ScriptHash = Contract.CreateSignatureRedeemScript(mlst.Seller).ToScriptHash(), AssetId = mlst.AssetId, Value = Fixed8.One * mlst.Amount });
            outputs.Add(new TransactionOutput { ScriptHash = Contract.CreateSignatureRedeemScript(mlst.Buyer).ToScriptHash(), AssetId = mlst.AssetId, Value = total - (Fixed8.One * mlst.Amount) });

            var tx = new ContractTransaction
            {
                Inputs = inputs.ToArray(),
                Outputs = outputs.ToArray(),
                Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.ApproveHash, Data = content.ApproveCode.ToArray() } }
            };
            return LockAssetHelper.Build(tx, avatars.ToArray());
        }

    }
}

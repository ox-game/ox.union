using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Network.P2P;
using Sunny.UI;
using Akka.Actor;
using OX.Ledger;
using OX.SmartContract;
using OX.Wallets;
using OX.Tablet.UIs.MarkSix;
using OX.DirectSales;
using OX.IO;
using OX.Network.P2P.Payloads;
using static OX.Tablet.NewMutualLockSellerTx;
using OX.Tablet.Config;

namespace OX.Tablet
{

    public partial class MutualLockBuyerInfo : UserControl
    {
        BuyerPage ParentPage;
        MutualLockSellerTransactionMerge Merge;

        public MutualLockBuyerInfo(BuyerPage parentPage, MutualLockSellerTransactionMerge merge)
        {
            this.ParentPage = parentPage;
            this.Merge = merge;
            InitializeComponent();
            this.uiGroupBox1.Text = merge.Key.TimeStamp.ToDateTime().ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            var sellerAddr = Contract.CreateSignatureRedeemScript(merge.Value.MLST.Seller).ToScriptHash().ToAddress();
            this.lb_seller.Text = UIHelper.LocalString($"卖家地址:{sellerAddr}", $"Seller Address:{sellerAddr}");
            var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(merge.Value.MLST.AssetId);
            this.lb_assetName.Text = UIHelper.LocalString($"资产:{assetState.GetName()}", $"Asset:{assetState.GetName()}");
            this.lb_amount.Text = UIHelper.LocalString($"交割金额:{merge.Value.MLST.Amount}", $"Delivery Amount:{merge.Value.MLST.Amount}");
            var expireTime = merge.Value.MLST.ExpireTimestamp.ToDateTime().ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            bool locked = merge.Value.MLBT.IsNotNull();
            this.lb_delivery_expire.Text = UIHelper.LocalString($"赎回时间:{expireTime}", $"Redeemable deadline:{expireTime}");
            this.lb_lock_state.Text = locked ? UIHelper.LocalString($"交割状态:已锁单", $"Delivery State:Locked") : UIHelper.LocalString($"交割状态:未锁单", $"Delivery State:Not Locked");

            if (locked || merge.Value.MLST.ExpireTimestamp < DateTime.UtcNow.ToTimestamp())
            {
                this.bt_do_lock.Enabled = false;
                if (locked)
                    this.uiGroupBox1.RectColor = Color.Red;
            }
            this.bt_do_lock.Text = UIHelper.LocalString("锁单", "Lock it");
        }

        private void bt_do_lock_Click(object sender, EventArgs e)
        {
            var sh = this.Merge.Value.MLST.GetContract().ScriptHash;
            var mutualLockState = Blockchain.Singleton.CurrentSnapshot.MutualLockStates.TryGet(sh);
            if (mutualLockState.IsNotNull() && !mutualLockState.Locked)
            {
                var tx = BuildTx();
                if (tx.IsNotNull())
                {
                    Program.BlockHandler.Tell(tx);
                    foreach (var coin in tx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(tx, UIHelper.LocalString("等待锁定确认...", "Waiting  confirm lock transaction")).ShowDialog();
                }
            }
        }

        public MutualLockBuyerTransaction BuildTx()
        {

            var tx = new MutualLockBuyerTransaction
            {
                Buyer = this.Merge.Value.MLST.Buyer,
                MutualLockScriptHash = this.Merge.Value.MLST.GetContract().ScriptHash
            };

            var amount = Fixed8.One * this.Merge.Value.MLST.Amount;

            List<TransactionOutput> outputs = new List<TransactionOutput>();
            TransactionOutput output = new TransactionOutput()
            {
                AssetId = this.Merge.Value.MLST.AssetId,
                ScriptHash = this.Merge.Value.MLST.GetContract().ScriptHash,
                Value = amount
            };
            outputs.Add(output);

            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();

            var utxos = SecureHelper.BlockIndex.GetMasterUtxos(this.Merge.Value.MLST.AssetId);
            if (utxos.IsNotNullAndEmpty())
            {
                List<string> excludedUtxoKeys = new List<string>();
                var totalAmt = outputs.Sum(m => m.Value);
                if (utxos.SortSearch(totalAmt.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                {
                    if (remainder > 0)
                        outputs.Add(new TransactionOutput { AssetId = this.Merge.Value.MLST.AssetId, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
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
                tx = LockAssetHelper.Build(tx, avatars.ToArray());
            }
            return tx;
        }


        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }
    }
}

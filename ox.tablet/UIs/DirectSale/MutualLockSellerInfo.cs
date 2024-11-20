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
using OX.Tablet.Config;

namespace OX.Tablet
{

    public partial class MutualLockSellerInfo : UserControl
    {
        SellerPage ParentPage;
        MutualLockSellerTransactionMerge Merge;

        public MutualLockSellerInfo(SellerPage parentPage, MutualLockSellerTransactionMerge merge)
        {
            this.ParentPage = parentPage;
            this.Merge = merge;
            InitializeComponent();
            this.uiGroupBox1.Text = merge.Key.TimeStamp.ToDateTime().ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            var buyerAddr = Contract.CreateSignatureRedeemScript(merge.Value.MLST.Buyer).ToScriptHash().ToAddress();
            this.lb_buyer.Text = UIHelper.LocalString($"买家地址:{buyerAddr}", $"Buyer Address:{buyerAddr}");
            var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(merge.Value.MLST.AssetId);
            this.lb_assetName.Text = UIHelper.LocalString($"资产:{assetState.GetName()}", $"Asset:{assetState.GetName()}");
            this.lb_amount.Text = UIHelper.LocalString($"交割金额:{merge.Value.MLST.Amount}", $"Delivery Amount:{merge.Value.MLST.Amount}");
            var expireTime = merge.Value.MLST.ExpireTimestamp.ToDateTime().ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            bool locked = merge.Value.MLBT.IsNotNull();
            this.lb_delivery_expire.Text = UIHelper.LocalString($"赎回时间:{expireTime}", $"Redeemable deadline:{expireTime}");
            this.lb_lock_state.Text = locked ? UIHelper.LocalString($"交割状态:已锁单", $"Delivery State:Locked") : UIHelper.LocalString($"交割状态:未锁单", $"Delivery State:Not Locked");

            if (locked)
                this.uiGroupBox1.RectColor = Color.Red;
            this.bt_delivery.Text = UIHelper.LocalString("交割", "Delivery");

            if (merge.Value.MLST.ExpireTimestamp > DateTime.UtcNow.ToTimestamp() || locked)
                this.bt_redeem.Enabled = false;
            this.bt_redeem.Text = UIHelper.LocalString("赎回", "Redeem");
        }

        private void bt_redeem_Click(object sender, EventArgs e)
        {
            var sh = Merge.Value.MLST.GetContract().ScriptHash;
            var mutualLockState = Blockchain.Singleton.CurrentSnapshot.MutualLockStates.TryGet(sh);
            if (mutualLockState.IsNotNull()&&!mutualLockState.Locked)
            {
                var tx = BuildRedeemTx(mutualLockState);
                if (tx.IsNotNull())
                {
                    Program.BlockHandler.Tell(tx);
                    //foreach (var coin in tx.Inputs)
                    //{
                    //    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    //}
                    new WaitTxForm(tx, UIHelper.LocalString("等待确认资产赎回...", "Waiting  confirm redeem asset ...")).ShowDialog();
                }
            }

        }

        public ContractTransaction BuildTx(MutualLockState mutualLockState, UInt256 approveCode)
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
                            avatars.Add(LockAssetHelper.CreateAccount(mlst.GetContract(), SecureHelper.MasterAccount.Key));
                            total += output.Value;
                            break;
                        }
                    }
                }
            }
            outputs.Add(new TransactionOutput { ScriptHash = Contract.CreateSignatureRedeemScript(mlst.Seller).ToScriptHash(), AssetId = mlst.AssetId, Value = total });

            var tx = new ContractTransaction
            {
                Inputs = inputs.ToArray(),
                Outputs = outputs.ToArray(),
                Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.ApproveHash, Data = approveCode.ToArray() } }
            };
            return LockAssetHelper.Build(tx, avatars.ToArray());
        }

        public ContractTransaction BuildRedeemTx(MutualLockState mutualLockState)
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
            outputs.Add(new TransactionOutput { ScriptHash = Contract.CreateSignatureRedeemScript(mlst.Seller).ToScriptHash(), AssetId = mlst.AssetId, Value = total });

            var tx = new ContractTransaction
            {
                Inputs = inputs.ToArray(),
                Outputs = outputs.ToArray(),
                Attributes = new TransactionAttribute[0] 
            };
            return LockAssetHelper.Build(tx, avatars.ToArray());
        }

        public ContractTransaction BuildDeliveryTx(MutualLockState mutualLockState)
        {
            var mlst = mutualLockState.SellerTx;
            var sh = mlst.GetContract().ScriptHash;
            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            Fixed8 buyerAmount = Fixed8.One * mutualLockState.SellerTx.Amount;
            Fixed8 sellerAmount = Fixed8.One * mutualLockState.SellerTx.Amount;
            for (ushort i = 0; i < mlst.Outputs.Length; i++)
            {
                var output = mlst.Outputs[i];
                if (output.ScriptHash == sh)
                {
                    inputs.Add(new CoinReference { PrevHash = mlst.Hash, PrevIndex = i });
                    avatars.Add(LockAssetHelper.CreateAccount(mlst.GetContract(), SecureHelper.MasterAccount.Key));
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
                            avatars.Add(LockAssetHelper.CreateAccount(mlst.GetContract(), SecureHelper.MasterAccount.Key));
                            buyerAmount += output.Value;
                            break;
                        }
                    }
                }
            }
            outputs.Add(new TransactionOutput { ScriptHash = Contract.CreateSignatureRedeemScript(mlst.Seller).ToScriptHash(), AssetId = mlst.AssetId, Value = sellerAmount });
            outputs.Add(new TransactionOutput { ScriptHash = Contract.CreateSignatureRedeemScript(mlst.Buyer).ToScriptHash(), AssetId = mlst.AssetId, Value = buyerAmount });

            var tx = new ContractTransaction
            {
                Inputs = inputs.ToArray(),
                Outputs = outputs.ToArray(),
                Attributes = new TransactionAttribute[0]  
            };
            return LockAssetHelper.Build(tx, avatars.ToArray());
        }
        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }
        private void bt_delivery_Click(object sender, EventArgs e)
        {
            var sh = Merge.Value.MLST.GetContract().ScriptHash;
            var mutualLockState = Blockchain.Singleton.CurrentSnapshot.MutualLockStates.TryGet(sh);
            if (mutualLockState.IsNotNull())
            {
                var tx = BuildDeliveryTx(mutualLockState);
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
}

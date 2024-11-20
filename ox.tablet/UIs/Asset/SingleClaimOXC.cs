using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using OX.Bapps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Claims;
using Akka.Actor.Dsl;
using Akka.Util;
using OX.Network.P2P;
using Sunny.UI;
using OX.Tablet.Config;

namespace OX.Tablet
{
    public partial class SingleClaimOXC : UIForm
    {
        List<CoinReference> claims = new List<CoinReference>();
        List<LockOXS> los = new List<LockOXS>();
        Dictionary<UInt160, AvatarAccount> acts = new Dictionary<UInt160, AvatarAccount>();
        Fixed8 LockAvailable = Fixed8.Zero;
        Fixed8 LockUnavailable = Fixed8.Zero;
        public SingleClaimOXC()
        {
            InitializeComponent();
        }

        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("提取OXC", "OXC Claim");
            this.lb_Available.Text = UIHelper.LocalString("可提取:", "Available:");
            this.lb_Unavailable.Text = UIHelper.LocalString("不可提取:", "Unavailable:");
            this.btnOK.Text = UIHelper.LocalString("提取", "Claim");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");

            using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
            {
                var ks = SecureHelper.BlockIndex.GetAll<CoinReference, LockOXS>(TabletPersistencePrefixes.OXS_Claim_OXC);
                if (ks.IsNotNullAndEmpty())
                {
                    List<LockOXS> unspendlos = new List<LockOXS>();
                    foreach (var pair in ks.Where(m => m.Value.Holder.Equals(SecureHelper.MasterAccount.ScriptHash)))
                    {
                        if (pair.Value.Flag == LockOXSFlag.Spend)
                        {
                            AvatarAccount lockAccount = default;
                            if (pair.Value.IsLockAssetTx)
                            {
                                lockAccount = LockAssetHelper.CreateAccount(pair.Value.Tx.GetContract(), SecureHelper.MasterAccount.Key);//lock asset account have a some private key with master account
                            }
                            else
                                lockAccount = LockAssetHelper.CreateAccount(SecureHelper.MasterAccount.Contract, SecureHelper.MasterAccount.Key);//lock asset account have a some private key with master account
                            acts[lockAccount.ScriptHash] = lockAccount;
                            claims.Add(pair.Key);
                            los.Add(pair.Value);
                        }
                        else if (pair.Value.Flag == LockOXSFlag.Unspend)
                        {
                            unspendlos.Add(pair.Value);
                        }
                    }
                    LockAvailable += OXSHelper.CalculateBonusSpend(los);
                    LockUnavailable += OXSHelper.CalculateBonusUnspend(unspendlos, snapshot.Height + 1);
                }
                tb_Available.Text = LockAvailable.ToString();
                tb_Unavailable.Text = LockUnavailable.ToString();
                if (LockAvailable == Fixed8.Zero) this.btnOK.Enabled = false;
            }
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public Transaction BuildTx()
        {
            if (this.claims.IsNullOrEmpty()) return default;
            var tx = new ClaimTransaction
            {
                Claims = this.claims.ToArray(),
                Attributes = new TransactionAttribute[0],
                Inputs = new CoinReference[0],
                Witnesses = new Witness[0],
                Outputs = new[]
                                 {
                            new TransactionOutput{
                                AssetId = Blockchain.OXC_Token.Hash,
                                Value =LockAvailable,
                                ScriptHash =SecureHelper.MasterAccount.ScriptHash
                            }
                        }
            };
            return LockAssetHelper.Build(tx, acts.Values.ToArray());
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            var tx = BuildTx();
            if (tx.IsNull()) this.Close();
            Program.BlockHandler.Tell(tx);
            new WaitTxForm(tx, UIHelper.LocalString("等待交易确认...", "Waiting  confirm transaction")).ShowDialog();
            this.Close();
        }
    }
}

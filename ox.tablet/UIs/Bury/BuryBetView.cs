using Akka.IO;
using Akka.Util;
using Akka.Actor;
using Org.BouncyCastle.Bcpg;
using OX.Bapps;
using OX.BMS;
using OX.Cryptography;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P;
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
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class BuryBetView : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        uint needReload = uint.MaxValue - 10;//To prevent overflow, subtract 10
        Fixed8 AvailableBalance = Fixed8.Zero;
        Fixed8 MinBet = Fixed8.Zero;
        public BuryBetView()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("埋雷区", "Build Bury");
            this.L2.Text = UIHelper.LocalString("爆雷区", "Trigger Bury");
            this.lb_plainCode.Text = UIHelper.LocalString("明码:", "Plain Code:");
            this.lb_cipherCode.Text = UIHelper.LocalString("暗码:", "Cipher Code:");
            this.lb_amount.Text = UIHelper.LocalString("埋雷金额:", "Bury Amount:");
            this.lb_balance.Text = UIHelper.LocalString("可用余额:", "Available Balance:");
            this.bt_do_bury.Text = UIHelper.LocalString("埋雷", "Go Bury");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            this.setColor(bt_do_bury, FocusColor);
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            if (beatContext.BalanceChanged)
            {
                GetMasterOXCBalance();
            }
        }
        void GetMasterOXCBalance()
        {
            if (SecureHelper.GetMasterBalanceStates().TryGetValue(Blockchain.OXC, out var balance))
            {
                AvailableBalance = balance.AvailableBalance;
            }
            this.DoInvoke(() =>
            {
                this.tb_balance.Text = AvailableBalance.ToString();
            });
        }
        public void BeforeBlock(Block block)
        {
            if (block.Index > needReload + 5)////To prevent overflow, subtract 10
            {
                needReload = uint.MaxValue - 10;
                ReloadBurys();
            }
        }
        public void OnBlock(Block block)
        {

        }
        public void AfterBlock(Block block)
        {
            this.DoInvoke(() =>
            {
                if (this.Visible)
                {
                    for (ushort i = 0; i < block.Transactions.Length; i++)
                    {
                        var tx = block.Transactions[i];
                        if (tx is BizTransaction biztx && biztx.BizScriptHash == casino.CasinoMasterAccountAddress)
                        {
                            var bizshs = new UInt160[] { casino.CasinoMasterAccountAddress };
                            if (biztx is AskTransaction at)
                            {
                                if (at.GetDataModel<BuryRequest>(bizshs, (byte)CasinoType.Bury, out BuryRequest buryrequest) && buryrequest.BetAddress == casino.BuryBetAddress)
                                {
                                    if (needReload == uint.MaxValue - 10)
                                        needReload = block.Index;
                                }
                            }
                        }
                        if (tx is RangeTransaction rt)
                        {
                            try
                            {
                                var attr = rt.Attributes.Where(m => m.Usage == TransactionAttributeUsage.RelatedData).FirstOrDefault();
                                if (attr.IsNotNull())
                                {
                                    var request = attr.Data.AsSerializable<BuryRequest>();
                                    if (request.BetAddress == casino.BuryBetAddress)
                                    {
                                        if (needReload == uint.MaxValue - 10)
                                            needReload = block.Index;
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }

                    }
                }
            });


        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public virtual void MenuPageSelected()
        {
            GetMasterOXCBalance();
            ReloadBurys();
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
            ReloadBurys();
        }
        void ReloadBurys()
        {
            this.DoInvoke(() =>
            {
                if (this.Visible)
                {
                    this.pn_pairs.Controls.Clear();
                    if (SecureHelper.BlockIndex.IsNotNull() && SecureHelper.IsAuthenticated)
                    {
                        var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();

                        var setting = casinoIndex.GetCasinoSetting(CasinoSettingTypes.BuryMinBet);
                        if (setting.IsNotNullAndEmpty())
                        {
                            Fixed8.TryParse(setting, out MinBet);
                        }
                        this.tb_balance.Minimum = MinBet.GetInternalValue() / Fixed8.D;

                        var number = casinoIndex.BuryNumber;
                        uint p = number > 200 ? number - 200 : 0;
                        int k = 0;
                        for (uint i = number; i > p; i--)
                        {
                            var buryRecord = casinoIndex.GetBury(casino.BuryBetAddress, i);
                            if (buryRecord.IsNotNull())
                            {
                                k++;
                                var replyBury = casinoIndex.GetRoomReplyBury(buryRecord.TxId);
                                this.pn_pairs.Controls.Add(new BuryButton(this, buryRecord, replyBury, k));
                            }
                        }

                    }
                }
            });
        }

        private void bt_do_bury_Click(object sender, EventArgs e)
        {
            if (!byte.TryParse(this.tb_plainCode.Text, out byte plainCode))
                return;
            if (!byte.TryParse(this.tb_cipherCode.Text, out byte cipherCode))
                return;
            if (!Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amt))
                return;

            if (amt > AvailableBalance)
                return;
            if (amt < MinBet)
                return;
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            TransactionOutput output = new TransactionOutput()
            {
                AssetId = Blockchain.OXC,
                ScriptHash = casino.BuryBetAddress,
                Value = amt
            };
            outputs.Add(output);
            PrivateBuryRequest PrivateBuryRequest = new PrivateBuryRequest { Rand = (uint)new Random().Next(0, int.MaxValue), CipherBuryPoint = cipherCode };
            VerifyPrivateBuryRequest VerifyPrivateBuryRequest = new VerifyPrivateBuryRequest { From = SecureHelper.MasterAccount.ScriptHash, PrivateBuryRequest = PrivateBuryRequest };

            var encryptDataForCipherCode = PrivateBuryRequest.ToArray().Encrypt(SecureHelper.MasterAccount.Key, casino.CasinoMasterAccountPubKey);
            BuryRequest request = new BuryRequest
            {
                From = SecureHelper.MasterAccount.ScriptHash,
                BetAddress = casino.BuryBetAddress,
                PlainBuryPoint = plainCode,
                VerifyHash = VerifyPrivateBuryRequest.Hash,
                CryptoData = encryptDataForCipherCode
            };



            if (!Blockchain.Singleton.VerifySlotValidator(casino.CasinoMasterAccountAddress, out AccountState _, out Fixed8 balance, out Fixed8 askFee)) return ;

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
                var totalAmt = outputs.Sum(m => m.Value)+Fixed8.One;
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

               var tx = new AskTransaction
                {
                    From = SecureHelper.MasterAccount.Key.PublicKey,
                    Outputs = outputs.ToArray(),
                    DataType = (byte)CasinoType.Bury,
                    Data = request.ToArray(),                  
                    BizScriptHash = casino.CasinoMasterAccountAddress,
                    Inputs = inputs.ToArray()
                };
                tx = LockAssetHelper.Build(tx, avatars.ToArray());
                if (tx.IsNotNull())
                {
                    Program.BlockHandler.Tell(tx);
                    foreach (var coin in tx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(tx, UIHelper.LocalString("等待埋雷确认...", "Waiting  confirm bet transaction")).ShowDialog();
                }
            }
        }
    }
}

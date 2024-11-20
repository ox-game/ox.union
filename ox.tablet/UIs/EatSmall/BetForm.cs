using Akka.Event;
using OX.Ledger;
using Sunny.UI;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using OX.IO;
using OX.Network.P2P.Payloads;
using Nethereum.Util;
using OX.Wallets;
using Nethereum.Signer.Crypto;
using OX.SmartContract;
using OX.Casino;
using System.Drawing;
using NBitcoin.Secp256k1;
using OX.Tablet.UIs.MarkSix;
using Sunny.UI.Win32;
using System;
using OX.Bapps;
using static QRCoder.PayloadGenerator.SwissQrCode;
using OX.Tablet.Config;
namespace OX.Tablet
{
    public partial class BetForm : UIForm, IBlockchainHandler
    {
        RoomView ParentView;
        MixRoom Room;
        uint PeroidBlocks;
        uint MinBetHeight;
        Fixed8 MinBet;
        byte Position;
        Fixed8 AvailableBalance = Fixed8.Zero;
        public BetForm(RoomView parentView, MixRoom room, Fixed8 minBet, byte position)
        {
            ParentView = parentView;
            this.Room = room;
            this.MinBet = minBet;
            this.Position = position;
            InitializeComponent();
            PeroidBlocks = Game.PeroidBlocks(room.Request);
            UpdateMinBetHeight();
            var fee = Fixed8.One + this.Room.GetPrivateRoomBetFee();
            this.Text = UIHelper.LocalString($"大吃小下注  {this.Position}号位      单注费 {fee} OXC", $"EatSmall Bet in Position {this.Position}       bet fee {fee} OXC");
            this.lb_roomid.Text = UIHelper.LocalString($"房间号:    {this.Room.RoomId}", $"Room Id:    {this.Room.RoomId}");
            this.lb_amount.Text = UIHelper.LocalString("下注金额:", "Bet Amount:");
            this.lb_balance.Text = UIHelper.LocalString("可用余额:", "Balance:");
            this.lb_bet_height.Text = UIHelper.LocalString("下注高度:", "Bet Height:");
            this.lb_special_code.Text = UIHelper.LocalString("特码:", "Code:");
            this.tb_balance.Text = "0";
            this.btnOK.Text = UIHelper.LocalString("下注", "Bet");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.tb_amount.Minimum = minBet.GetInternalValue() / Fixed8.D;
            foreach (var code in GuessKey.SpecialChars)
                this.cb_codes.Items.Add(code.ToString().ToUpper());
            Random rd = new Random();
            this.cb_codes.SelectedIndex = rd.Next(0, GuessKey.SpecialChars.Length);
            if (SecureHelper.IsAuthenticated && SecureHelper.BlockIndex.IsNotNull())
            {
                var balanceStates = SecureHelper.GetMasterBalanceStates();
                if (balanceStates.IsNotNullAndEmpty())
                {
                    if (balanceStates.TryGetValue(Blockchain.OXC, out var balanceState))
                    {
                        AvailableBalance = balanceState.AvailableBalance;
                        this.tb_balance.Text = balanceState.AvailableBalance.ToString();
                    }
                }
            }
        }
        void UpdateMinBetHeight()
        {
            var height = Blockchain.Singleton.HeaderHeight;
            var index = height;
            var remainder = index % PeroidBlocks;
            var zindex = index - remainder;
            var pb = PeroidBlocks;
            if (PeroidBlocks == 10)
            {
                zindex += PeroidBlocks;
                pb = PeroidBlocks * 2;
                var c = index % pb;
                index -= c;
                if (this.Room.Request.Flag % 2 == 1)
                {
                    int cc = c > 10 ? 30 : 10;
                    index += (uint)cc;
                }
                else
                {
                    if (c > 0) index += pb;
                }
                zindex = index;
            }
            else
            {
                zindex += GameHelper.ReviseIndex(this.Room);
                if (zindex < height) zindex += PeroidBlocks;
            }

            var fremainder = zindex - height;
            if (fremainder <= (PeroidBlocks == 10 ? 5 : 17))
            {
                this.cb_Height.FillColor = Color.Red;
                zindex += pb;
            }
            else
            {
                this.cb_Height.FillColor = Color.White;
            }

            if (this.MinBetHeight != zindex)
            {
                this.MinBetHeight = zindex;
                this.cb_Height.Items.Clear();
                for (uint i = 0; i < 10; i++)
                {
                    this.cb_Height.Items.Add(this.MinBetHeight + pb * i);
                }
                if (this.ParentView.CurrentIndex > this.MinBetHeight)
                {
                    this.cb_Height.SelectedItem = this.ParentView.CurrentIndex;
                }
                else
                    this.cb_Height.SelectedIndex = 0;
            }
            this.cb_Height.Refresh();
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            UpdateMinBetHeight();
            if (beatContext.IsNormalSync)
            {
                EnableBet();
            }
            else
            {
                DisableBet();
            }
        }
        void DisableBet()
        {
            this.btnOK.Enabled = false;
        }
        void EnableBet()
        {
            this.btnOK.Enabled = true;
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
        public void MenuPageSelected()
        {

        }
        public Transaction BuildTx()
        {
            AskTransaction at = default;
            if (this.cb_Height.SelectedItem == null)
                return default;
            if (!SecureHelper.IsAuthenticated || SecureHelper.BlockIndex.IsNull()) return default;
            var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();
            if (casinoIndex.GetRoomDestory(this.Room.RoomId).IsNotNull())
            {
                return default;
            }

            if (this.cb_Height.SelectedItem is uint index)
            {
                if (index % PeroidBlocks != GameHelper.ReviseIndex(this.Room))
                    return default;
                if (!Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amt))
                    return default;


                if (amt > this.AvailableBalance)
                    return default;
                if (amt < MinBet)
                {
                    return default;
                }
                try
                {
                    if (Room.ValidPrivateRoom(SecureHelper.MasterAccount.ScriptHash))
                    {
                        amt = Fixed8.One * (amt.GetInternalValue() / Fixed8.D);
                        List<TransactionOutput> outputs = new List<TransactionOutput>();
                        TransactionOutput output = new TransactionOutput()
                        {
                            AssetId = Room.Request.AssetId,
                            ScriptHash = this.Room.BetAddress,
                            Value = amt
                        };
                        outputs.Add(output);
                        var betFee = Room.GetPrivateRoomBetFee();
                        if (betFee > Fixed8.Zero)
                        {
                            output = new TransactionOutput()
                            {
                                AssetId = Blockchain.OXC,
                                ScriptHash = Room.Holder,
                                Value = betFee
                            };
                            outputs.Add(output);
                        }
                        var spc = this.cb_codes.Text.ToLower();
                        if (spc.IsNullOrEmpty()) return default;
                        var spcc = spc.ToCharArray()[0];
                        if (!GuessKey.SpecialChars.Contains(spcc)) return default;
                        BetRequest request = new BetRequest() { BetPoint = $"{this.Position.ToString()}|{spcc}", From = SecureHelper.MasterAccount.ScriptHash, Index = index, BetAddress = this.Room.BetAddress, Passport = default };
                        if (!Blockchain.Singleton.VerifySlotValidator(casino.CasinoMasterAccountAddress, out AccountState _, out Fixed8 balance, out Fixed8 askFee)) return default;

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
                           var totalAmt= outputs.Sum(m => m.Value);
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
                                DataType = (byte)CasinoType.Bet,
                                Data = request.ToArray(),
                                MaxIndex = index,
                                BizScriptHash = casino.CasinoMasterAccountAddress,
                                Inputs = inputs.ToArray()
                            };
                            at = LockAssetHelper.Build(at, avatars.ToArray());
                        }

                    }
                }
                catch
                {
                    return default;
                }
            }
            return at;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
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
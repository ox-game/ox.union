using Org.BouncyCastle.Bcpg;
using OX.Bapps;
using OX.BMS;
using OX.Casino;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Plugins;
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
using OX.SmartContract;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class RoomView : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        MixRoom Room;
        byte BankerPosition = 0;
        uint PeroidBlocks;
        public uint CurrentIndex { get; private set; }
        Fixed8 MinBet = Fixed8.One;
        Fixed8 masterBalance = Fixed8.Zero;
        Fixed8 poolBalance = Fixed8.Zero;
        Fixed8 allBetAmount = Fixed8.Zero;
        Riddles Riddles;
        ulong MineNonce;
        bool needRefresh = false;
        public IEnumerable<KeyValuePair<BetKey, Betting>> Bettings { get; protected set; } = default;
        public IEnumerable<KeyValuePair<RoundClearKey, RoundClearResult>> ClearResult { get; protected set; } = default;
        Dictionary<byte, PositionPanel> PositionPanels = new Dictionary<byte, PositionPanel>();
        public RoomView(MixRoom room)
        {
            this.Room = room;
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("开奖结果", "Winning results");
            this.lb_bet_info.Text = string.Empty;
            this.bt_next.Text = UIHelper.LocalString("下一局", "Next");
            this.bt_pre.Text = UIHelper.LocalString("上一局", "Previous");
            //this.setColor(this.bt_go_current, FocusColor);
            InitPosition();
            InitSetting();
            ResetAnShowIndex();
        }
        void InitSetting()
        {
            BankerPosition = Room.Request.Flag;
            PeroidBlocks = Game.PeroidBlocks(Room.Request);
            if (SecureHelper.BlockIndex.IsNotNull())
            { 
                var feesetting = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>().GetCasinoSetting(CasinoSettingTypes.EatSmallMinBet);

                if (feesetting.IsNotNullAndEmpty())
                {
                    Fixed8.TryParse(feesetting, out MinBet);
                }
            }
            int c = 0;
            int r = 0;
            for (byte i = 0; i < 10; i++)
            {
                if (i != this.BankerPosition)
                {
                    var panel = new PositionPanel(this, this.Room, MinBet, i);
                    this.Box.Controls.Add(panel, c, r);
                    PositionPanels[i] = panel;
                    c++;
                    if (c >= 3)
                    {
                        r++;
                        c = 0;
                    }
                }
            }
        }
        private void InitPosition()
        {
            Box.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 33.33F);
            Box.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 33.33F);
            Box.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 33.33F);
            Box.RowStyles[0] = new RowStyle(SizeType.Percent, 33.33F);
            Box.RowStyles[1] = new RowStyle(SizeType.Percent, 33.33F);
            Box.RowStyles[2] = new RowStyle(SizeType.Percent, 33.33F);

        }
        public void ResetAnShowIndex()
        {
            var index = Blockchain.Singleton.HeaderHeight;
            if (PeroidBlocks == 10)
            {
                var pb = PeroidBlocks * 2;
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
            }
            else
            {
                var rem = index % PeroidBlocks;
                var newIndex = index - rem + GetReviseIndex();
                if (newIndex < index)
                    newIndex += PeroidBlocks;
                index = newIndex;
            }
            if (this.CurrentIndex != index)
            {
                this.CurrentIndex = index;
                ShowIndex();
            }
        }
        public bool ResetIndex()
        {
            var index = Blockchain.Singleton.HeaderHeight;

            if (PeroidBlocks == 10)
            {
                var pb = PeroidBlocks * 2;
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
            }
            else
            {
                var rem = index % PeroidBlocks;
                var newIndex = index - rem + GetReviseIndex();
                if (newIndex < index)
                    newIndex += PeroidBlocks;
                index = newIndex;
            }
            if (this.CurrentIndex != index)
            {
                this.CurrentIndex = index;
                return true;
            }
            return false;
        }
        public uint GetReviseIndex()
        {
            return this.Room.ReviseIndex();
        }
        public void ShowIndex()
        {

            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();
                this.Bettings = casinoIndex.GetBettings(this.Room.BetAddress, this.CurrentIndex);
                this.ClearResult = casinoIndex.GetRoundClearResults(this.Room.BetAddress, this.CurrentIndex);
                this.allBetAmount = this.Bettings.Sum(m => m.Value.Amount);
                this.Riddles = default;
                this.MineNonce = 0;
                var riddleRecord = casinoIndex.GetRiddles(this.CurrentIndex);
                if (riddleRecord.IsNotNull())
                {
                    Riddles = riddleRecord;
                    this.MineNonce = TabletCasinoHelper.GetMineNonce(this.CurrentIndex);
                }
                RefreshRoomState();
                ShowRoomState();
            }

        }
        public void RefreshRoomState()
        {
            using var snapshot = Blockchain.Singleton.GetSnapshot();
            var acts = snapshot.Accounts.GetAndChange(Room.BankerAddress, () => null);
            this.masterBalance = acts.IsNotNull() ? acts.GetBalance(Room.Request.AssetId) : Fixed8.Zero;
            var acts2 = snapshot.Accounts.GetAndChange(Room.PoolAddress, () => null);
            this.poolBalance = acts2.IsNotNull() ? acts2.GetBalance(Room.Request.AssetId) : Fixed8.Zero;
        }
        public void ShowRoomState()
        {
            this.DoInvoke(() =>
            {
                this.lb_prize.Clear();
                var tip = string.Empty;
                var c = string.Empty;
                char[] keys = default;
                if (this.Room.Request.Permission == RoomPermission.Public && this.Riddles.IsNotNull() && this.MineNonce > 0)
                {
                    var guessKey = this.Riddles.GetGuessKey(this.Room.Request.Kind);
                    if (guessKey.IsNotNull())
                    {
                        keys = guessKey.ReRandomSanGongOrLotto(this.MineNonce, this.CurrentIndex);
                        c = $"[{keys[this.BankerPosition].ToString()}]";
                        tip = $"{guessKey.SpecialChar}-{guessKey.SpecialPosition}".ToUpper();
                    }
                }

                this.bt_go_current.Update(this.Room, this.CurrentIndex, tip);
                this.lb_bet_info.Text = UIHelper.LocalString($"总投注额:{this.allBetAmount},庄家投注在{BankerPosition}{c}号位投注额:{this.masterBalance}", $"Current bankroll :{this.allBetAmount},Banker Bet Position {BankerPosition}{c} :{this.masterBalance}");
                this.lb_pool_info.Text = UIHelper.LocalString($"奖池余额:{this.poolBalance}", $"Pool balance:{this.poolBalance}");

                if (this.ClearResult.IsNotNullAndEmpty())
                {
                    List<TransactionOutput> list = new List<TransactionOutput>();
                    foreach (var result in this.ClearResult)
                    {
                        var tx = Blockchain.Singleton.GetTransaction(result.Value.TxHash);
                        if (tx is ReplyTransaction rt)
                        {
                            var bizshs = new UInt160[] { casino.CasinoMasterAccountAddress };

                            if (rt.GetDataModel<RoundClear>(bizshs, (byte)CasinoType.RoundClear, out RoundClear roundClear))
                            {
                                if (roundClear.BetAddress == this.Room.BetAddress && roundClear.Index == this.CurrentIndex)
                                {
                                    foreach (var output in tx.Outputs)
                                    {
                                        if (output.AssetId == this.Room.Request.AssetId)
                                        {
                                            list.Add(output);

                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (var output in list.OrderByDescending(m => m.Value))
                    {
                        this.lb_prize.Add(new PrizeButton(this, output));
                    }
                }
                foreach (var panel in this.PositionPanels.Values)
                {
                    List<Betting> bettings = new List<Betting>();
                    if (this.Bettings != default)
                    {
                        var bs = this.Bettings.Where(m =>
                        {
                            var cs = m.Value.BetRequest.BetPoint.ToCharArray();
                            return cs.IsNotNullAndEmpty() && cs[0].ToString() == panel.Position.ToString();
                        }).Select(m => m.Value);
                        if (bs.IsNotNullAndEmpty())
                        {
                            bettings.AddRange(bs);
                        }
                    }
                    panel.RefreshData(bettings.ToArray(), keys);
                }
            });
        }
        public bool AllowBet()
        {
            if (SecureHelper.BlockIndex.IsNull()) return false;
            return SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>().VerifyPartnerLock(this.Room, out IEnumerable<RoomPartnerLockRecord> validRecords, out Fixed8 havLockTotal, out Fixed8 needLockTotal, out uint earliestExpiration);
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            foreach (var panel in this.PositionPanels.Values)
            {
                panel.HeartBeat(beatContext);
            }
        }
        public void BeforeBlock(Block block)
        {
            foreach (var panel in this.PositionPanels.Values)
            {
                panel.BeforeBlock(block);
            }
        }
        public void OnBlock(Block block)
        {
            foreach (var panel in this.PositionPanels.Values)
            {
                panel.OnBlock(block);
            }
            if (needRefresh)
            {
                this.ShowIndex();
                needRefresh = false;
            }
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
                            if (biztx is ReplyTransaction rt)
                            {

                                if (rt.GetDataModel<RoundClear>(bizshs, (byte)CasinoType.RoundClear, out RoundClear roundClear) && roundClear.Index == this.CurrentIndex)
                                {
                                    if (roundClear.BetAddress == this.Room.BetAddress)
                                    {
                                        needRefresh = true;
                                    }
                                }
                                if (rt.GetDataModel<RiddlesAndHash>(bizshs, (byte)CasinoType.RiddlesAndHash, out RiddlesAndHash riddlesandhash) && riddlesandhash.Riddles.IsNotNull() && riddlesandhash.Riddles.Index == this.CurrentIndex)
                                {
                                    needRefresh = true;
                                }
                            }

                            else if (biztx is AskTransaction at)
                            {
                                if (at.GetDataModel<BetRequest>(bizshs, (byte)CasinoType.Bet, out BetRequest bet) && bet.Index == this.CurrentIndex)
                                {
                                    if (bet.BetAddress == this.Room.BetAddress)
                                    {
                                        needRefresh = true;
                                    }
                                }

                            }
                        }

                        if (tx is RangeTransaction rgt)
                        {
                            try
                            {
                                var attr = rgt.Attributes.Where(m => m.Usage == TransactionAttributeUsage.RelatedData).FirstOrDefault();
                                if (attr.IsNotNull())
                                {
                                    var request = attr.Data.AsSerializable<BetRequest>();
                                    if (request.BetAddress == this.Room.BetAddress && request.Index == this.CurrentIndex)
                                    {
                                        needRefresh = true;
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
            foreach (var panel in this.PositionPanels.Values)
            {
                panel.AfterBlock(block);
            }
        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public void MenuPageSelected()
        {
            foreach (var panel in this.PositionPanels.Values)
            {
                panel.MenuPageSelected();
            }
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

        private void bt_next_Click(object sender, EventArgs e)
        {
            //this.ViewHead.cb_auto.Checked = false;
            if (this.PeroidBlocks == 10)
            {
                this.CurrentIndex += this.PeroidBlocks * 2;
            }
            else
            {
                var preCurrentIndex = this.CurrentIndex;
                var preReviseIndex = GetReviseIndex();
                this.CurrentIndex += this.PeroidBlocks;
                var nextReviseIndex = GetReviseIndex();
                if (preReviseIndex != nextReviseIndex)
                {
                    this.CurrentIndex += nextReviseIndex;
                }
            }
            this.ShowIndex();
        }

        private void bt_pre_Click(object sender, EventArgs e)
        {
            var pb = this.PeroidBlocks;
            if (this.PeroidBlocks == 10)
                pb = this.PeroidBlocks * 2;
            if (this.CurrentIndex > pb)
            {
                var preCurrentIndex = this.CurrentIndex;
                var preReviseIndex = GetReviseIndex();
                this.CurrentIndex -= pb;
                var nextReviseIndex = GetReviseIndex();
                if (preReviseIndex != nextReviseIndex)
                {
                    this.CurrentIndex -= preReviseIndex;
                }
            }
            else this.CurrentIndex = 0;

            this.ShowIndex();
        }

        private void bt_go_current_Click(object sender, EventArgs e)
        {
            ResetAnShowIndex();
        }
    }
}

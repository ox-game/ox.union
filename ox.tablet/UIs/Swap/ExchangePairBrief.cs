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
using OX.BMS;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Tablet.UIs.MarkSix;
using System.Runtime;
using OX.IO;
using OX.Tablet.Config;

namespace OX.Tablet
{

    public partial class ExchangePairBrief : UserControl, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        UInt160 HostSH;
        SwapPairMerge SwapPairMerge;
        IDORecord LastIDORecord;
        SwapVolumeMerge LastSwapVolume;
        bool IsIDOTime = false;
        public SwapPairIDO IDO;
        public string AssetName = string.Empty;
        public ExchangePairBrief(UInt160 hostSH, SwapPairMerge swapPairMerge)
        {
            this.HostSH = hostSH;
            this.SwapPairMerge = swapPairMerge;
            try
            {
                IDO = this.SwapPairMerge.SwapPairReply.Mark.AsSerializable<SwapPairIDO>();
            }
            catch { }
            InitializeComponent();
            this.AssetName = swapPairMerge.TargetAssetState.GetName();
            this.uiGroupBox1.Text = this.AssetName;
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
            this.bt_swap_go.Text = UIHelper.LocalString("去交易", "Go Swap");
            this.bt_swap_go.Radius = 50;

            RefreshBuy();
            RefreshPrice();
            RefreshState();

        }

        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }

        public void HeartBeat(HeartBeatContext beatContext)
        {

        }
        public void BeforeBlock(Block block) { }
        public void OnBlock(Block block)
        {
            RefreshBuy();
        }
        public void AfterBlock(Block block)
        {

            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                foreach (var tx in block.Transactions)
                {
                    foreach (var output in tx.Outputs)
                    {
                        if (output.ScriptHash.Equals(HostSH) && (output.AssetId.Equals(this.SwapPairMerge.SwapPairReply.TargetAssetId) || output.AssetId.Equals(Blockchain.OXC)))
                        {
                            RefreshState();
                        }
                    }
                    if (tx.References.IsNotNullAndEmpty())
                    {
                        foreach (var reference in tx.References)
                        {
                            if (miningIndex.SwapPairs.TryGetValue(reference.Value.ScriptHash, out SwapPairMerge spm) && reference.Value.ScriptHash.Equals(this.HostSH))
                            {
                                if (spm.TargetAssetState.AssetId.Equals(reference.Value.AssetId) || Blockchain.OXC.Equals(reference.Value.AssetId))
                                {
                                    var attr = tx.Attributes.FirstOrDefault(m => m.Usage == TransactionAttributeUsage.Remark2);
                                    if (attr.IsNotNull())
                                    {
                                        try
                                        {
                                            var sop = attr.Data.AsSerializable<SwapVolume>();
                                            if (sop.IsNotNull())
                                            {
                                                this.LastSwapVolume = new SwapVolumeMerge { Volume = sop, Price = (decimal)sop.PricingAssetVolume.GetInternalValue() / (decimal)sop.TargetAssetVolume.GetInternalValue() }; ;
                                                RefreshPrice();
                                            }
                                        }
                                        catch { }
                                    }
                                    var attr3 = tx.Attributes.FirstOrDefault(m => m.Usage == TransactionAttributeUsage.Remark3);
                                    if (attr3.IsNotNull())
                                    {
                                        try
                                        {
                                            var idor = attr3.Data.AsSerializable<IDORecord>();
                                            if (idor.IsNotNull())
                                            {
                                                this.LastIDORecord = idor;
                                            }
                                        }
                                        catch { }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public void RefreshBuy()
        {
            this.DoInvoke(() =>
            {
                if (this.SwapPairMerge.SwapPairReply.Stamp > Blockchain.Singleton.Height)
                {
                    IsIDOTime = true;
                    this.bt_swap_go.Text = UIHelper.LocalString("IDO预购", "IDO Buy");
                }
                else
                {
                    IsIDOTime = false;
                    this.bt_swap_go.Text = UIHelper.LocalString("去交易", "Go Swap");
                }
            });
        }
        public void RefreshPrice()
        {
            this.DoInvoke(() =>
            {
                this.lb_price.Text = string.Empty;
                if (this.LastSwapVolume.IsNull())
                {
                    if (SecureHelper.BlockIndex.IsNotNull())
                    {
                        var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                        var vom = miningIndex.Get<SwapVolumeMerge>(MiningPersistencePrefixes.Exchange_Pair_Record_Last, this.HostSH);
                        if (vom.IsNotNull())
                            this.LastSwapVolume = vom;
                    }
                }
                if (this.LastSwapVolume.IsNotNull())
                {
                    this.lb_price.Text = this.LastSwapVolume.Price.ToString("f6");
                }
            });
        }
        public void RefreshState()
        {
            this.DoInvoke(() =>
            {
                this.lb_ido_lock_index.Text = string.Empty;
                this.lb_ido_lock_price.Text = string.Empty;
                this.lb_target_balance.Text = String.Empty;
                this.lb_oxc_balance.Text = String.Empty;
                this.lb_stamp.Text = string.Empty;
                this.lb_lock_index.Text = UIHelper.LocalString($"交易锁仓:{this.SwapPairMerge.SwapPairReply.LockExpire}", $"Lock Blocks:{this.SwapPairMerge.SwapPairReply.LockExpire}");
                if (this.LastSwapVolume.IsNotNull())
                {
                    this.lb_target_balance.Text = UIHelper.LocalString($"{this.AssetName} 余额:{this.LastSwapVolume.Volume.TargetBalance.ToString("f2")}", $"{this.AssetName} Balance:{this.LastSwapVolume.Volume.TargetBalance.ToString("f2")}");
                    this.lb_oxc_balance.Text = UIHelper.LocalString($"OXC 余额:{this.LastSwapVolume.Volume.PricingBalance.ToString("f2")}", $"OXC Balance:{this.LastSwapVolume.Volume.PricingBalance.ToString("f2")}");
                }
                else
                {
                    var acts = Blockchain.Singleton.CurrentSnapshot.Accounts.GetAndChange(HostSH, () => null);
                    if (acts.IsNotNull())
                    {
                        var targetBalance = acts.GetBalance(SwapPairMerge.TargetAssetState.AssetId);
                        var pricingBalance = acts.GetBalance(Blockchain.OXC);
                        this.lb_target_balance.Text = UIHelper.LocalString($"{this.AssetName} 余额:{targetBalance.ToString("f2")}", $"{this.AssetName} Balance:{targetBalance.ToString("f2")}");
                        this.lb_oxc_balance.Text = UIHelper.LocalString($"OXC 余额:{pricingBalance.ToString("f2")}", $"OXC Balance:{pricingBalance.ToString("f2")}");
                    }
                }
                if (this.IDO.IsNotNull())
                {
                    this.lb_ido_lock_price.Text = UIHelper.LocalString($"预购价格:{this.IDO.Price.ToString()}", $"IDO Price:{this.IDO.Price.ToString()}");
                    this.lb_ido_lock_index.Text = UIHelper.LocalString($"预购锁仓:{this.IDO.IDOLockExpire} 区块", $"IDO Lock:{this.IDO.IDOLockExpire} blocks");
                    this.lb_stamp.Text = UIHelper.LocalString($"首次开盘:第{this.SwapPairMerge.SwapPairReply.Stamp}区块", $"First Open At:{this.SwapPairMerge.SwapPairReply.Stamp}");
                }
            });
        }

        private void bt_bet_banker_Click(object sender, EventArgs e)
        {
            new ExchangeRechargeForm(this.HostSH, this.SwapPairMerge, this.IsIDOTime).ShowDialog();
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
    }
}

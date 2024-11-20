using Akka.IO;
using Org.BouncyCastle.Bcpg;
using OX.Bapps;
using OX.BMS;
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
    public partial class ExchangePage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        Dictionary<UInt160, ExchangePairBrief> Pairs = new Dictionary<UInt160, ExchangePairBrief>();
        public ExchangePage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("所有交易对", "All Exchange Pair");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            foreach (var p in this.Pairs.Values)
                p.HeartBeat(beatContext);
        }
        public void BeforeBlock(Block block)
        {
            foreach (var p in this.Pairs.Values)
                p.BeforeBlock(block);
        }
        public void OnBlock(Block block)
        {

            foreach (var p in this.Pairs.Values)
                p.OnBlock(block);
        }
        public void AfterBlock(Block block)
        {
            foreach (var p in this.Pairs.Values)
                p.AfterBlock(block);
        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public virtual void MenuPageSelected() { ShowPairs(); }

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



        void ShowPairs()
        {
            this.DoInvoke(() =>
            {
                this.pn_pairs.Clear();
                this.Pairs.Clear();
                if (SecureHelper.BlockIndex.IsNotNull())
                {
                    var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                    foreach (var p in miningIndex.GetAll<UInt160, SwapPairMerge>(MiningPersistencePrefixes.Exchange_Pair).OrderByDescending(m => m.Value.SwapPairReply.TargetAssetId.Equals(Blockchain.OXS)).ThenByDescending(m => m.Value.SwapPairReply.TargetAssetId == Mining.USDT_Asset).ThenByDescending(m => m.Value.Index))
                    {
                        if (miningIndex.SwapPairStates.TryGetValue(p.Key, out SwapPairStateReply stateReply) && stateReply.Flag != 1)
                            continue;
                        var c = new ExchangePairBrief(p.Key, p.Value);
                        this.pn_pairs.Add(c);
                        this.Pairs[p.Key] = c;
                    }
                }
            });
        }


        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            ShowPairs();
        }
    }
}

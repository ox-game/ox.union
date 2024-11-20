using Akka.IO;
using Org.BouncyCastle.Bcpg;
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
    public partial class InGoldPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public InGoldPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("场外出金单", "OTC Withdrawal List");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
           
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
                    var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                    foreach (var r in miningIndex.OTCDealers.Select(m => m.Value).Where(m => m.Setting.State == OTCStatus.Open).OrderBy(m => m.Setting.InRate))
                    {
                        var account = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(r.InPoolAddress);
                        if (account.IsNotNull() && account.Balances.TryGetValue(Mining.USDT_Asset, out Fixed8 OTCDealerOXPoolBalance))
                        {
                            this.pn_pairs.Add(new OTCDealerInfo(r, OTCDealerOXPoolBalance));
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

using Akka.IO;
using OX.BMS;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Tablet.Config;
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

namespace OX.Tablet
{
    public partial class ExchangeAssetsSubPage : UIPage, IBlockchainHandler
    {
        Dictionary<UInt256, EthAssetBalanceState> ExchangeBalanceStates = new Dictionary<UInt256, EthAssetBalanceState>();

        public ExchangeAssetsSubPage()
        {
            InitializeComponent();
            this.bt_refresh_balance.Text = UIHelper.LocalString("刷新资产", "Refresh Assets");
            this.bt_asset_record.Text = UIHelper.LocalString("交易记录", "Asset Records");
        }

        public void HeartBeat(HeartBeatContext beatContext)
        {
        }
        public void BeforeBlock(Block block) { }
        public void OnBlock(Block block) { }
        public void AfterBlock(Block block)
        {

        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }
        public virtual void MenuPageSelected()
        {
            RefreshBalanceState();
            pn_assets_SizeChanged(null, new EventArgs());
        }
        void RefreshBalanceState()
        {


            ExchangeBalanceStates = SecureHelper.GetExchangeBalanceStates();
            this.pn_exchange_assets.Controls.Clear();
            foreach (var bs in this.ExchangeBalanceStates.OrderByDescending(m => m.Value.AssetId == Blockchain.OXS).ThenByDescending(m => m.Value.AssetId == Blockchain.OXC).ThenByDescending(m => m.Value.AssetId == Mining.USDT_Asset))
            {
                var assetBalance = new ExchangeAssetBalance(bs.Value, this.lb_assetDetails);
                assetBalance.AssetSelected += Exchange_AssetBalance_AssetSelected;
                this.pn_exchange_assets.Controls.Add(assetBalance);
            }


        }

        private void Exchange_AssetBalance_AssetSelected(ExchangeAssetBalance assetBalance)
        {
            foreach (Control c in this.pn_exchange_assets.Controls)
            {
                if (c is ExchangeAssetBalance ab)
                {
                    if (ab != assetBalance)
                    {
                        ab.UnSelect();
                    }
                }
            }
        }


        private void bt_refresh_balance_Click(object sender, EventArgs e)
        {
            RefreshBalanceState();
            pn_assets_SizeChanged(null, new EventArgs());
        }

        private void pn_assets_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.pn_exchange_assets.Controls)
            {
                c.Width = this.pn_exchange_assets.Width - 10;
            }
        }

        private void bt_asset_record_Click(object sender, EventArgs e)
        {
            this.lb_assetDetails.Items.Clear();
            if (SecureHelper.BlockIndex.BlockAssetRecords.IsNotNullAndEmpty())
            {
                Dictionary<UInt256, string> assetNames = new Dictionary<UInt256, string>();
                var vs = SecureHelper.BlockIndex.BlockAssetRecords.Values.OrderByDescending(m => m.Index).Take(100);
                foreach (var v in vs.OrderByDescending(m => m.Index))
                {
                    foreach (var balance in v.Balances)
                    {
                        if (!assetNames.TryGetValue(balance.Key, out var name))
                        {
                            var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(balance.Key);
                            if (assetState.IsNotNull())
                            {
                                name = assetState.GetName();
                                assetNames[balance.Key] = name;
                            }
                        }
                        string s = string.Empty;
                        if (balance.Value > Fixed8.Zero)
                            s = UIHelper.LocalString($"转入 {name}    {balance.Value}   |   {v.TimeStamp.ToDateTime().ToLocalTime()}", $"income {name}    {balance.Value}   |   {v.TimeStamp.ToDateTime().ToLocalTime()}");
                        else
                            s = UIHelper.LocalString($"转出 {name}    {balance.Value}   |   {v.TimeStamp.ToDateTime().ToLocalTime()}", $"pay out {name}    {balance.Value}   |   {v.TimeStamp.ToDateTime().ToLocalTime()}");
                        this.lb_assetDetails.Items.Add(s);
                    }
                }
            }
        }



        private void tbc_sections_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}

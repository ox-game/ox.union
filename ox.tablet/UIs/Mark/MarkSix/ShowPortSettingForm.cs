using OX.Bapps;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OX.BMS;
using Sunny.UI;
using Akka.Actor;
using OX.Network.P2P;
using NBitcoin.Secp256k1;
using AntDesign;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class ShowPortSettingForm : UIForm
    {
        MixMarkMember MyBanker;
        public ShowPortSettingForm(MixMarkMember myBanker)
        {
            this.MyBanker = myBanker;
            InitializeComponent();
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("盘口参数", "Port Parameters");
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
            this.bankerSetting1.LoadSetting(this.MyBanker.MarkSetting);
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {

        }
        public void OnBlock(Block block)
        {
        }
        public void BeforeOnBlock(Block block) { }
        public void AfterOnBlock(Block block) { }

        public void OnRebuild() { }



        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

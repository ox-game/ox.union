using Akka.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.DirectSales;
using OX.Ledger;
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
using Sunny.UI.Win32;
using OX.Tablet.UIs.Mark;
using OX.Tablet.Config;
using OX.IO;
using OX.Tablet.UIs.Mark.MarkSix;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkSixAwardSubPage : BaseBoundView
    {

        MarkSixPlazaForm ParentPage;
        public MarkSixAwardSubPage(MarkSixPlazaForm parentPage)
        {
            this.ParentPage = parentPage;
            InitializeComponent();
            this.Text = UIHelper.LocalString("开奖结果", "Award");
            this.bt_copy_image.Text = UIHelper.LocalString("复制开奖结果", "Copy Award Result");
        }



        public override void HeartBeat(HeartBeatContext beatContext)
        {

        }
        public override void BeforeBlock(Block block)
        {

        }
        public override void OnBlock(Block block)
        {

        }
        public override void AfterBlock(Block block)
        {

        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }
        public override void ReloadOrders()
        {
            this.AwardResult.Reload(this.ParentPage.CurrentSelectedTerm);
        }
        public override void MenuPageSelected()
        {
            ReloadOrders();
        }





        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_copy_image_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(this.AwardResult.Export());
            this.ShowSuccessTip(UIHelper.LocalString("开奖结果已复制", "Award result copied"));
        }
    }
}

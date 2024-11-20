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
namespace OX.Tablet.UIs.Mark.MarkSix
{
    public  class BaseBoundView : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        public BaseBoundView() : base()
        {

        }
        bool _allowEdit = true;
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set { _allowEdit = value; }
        }
        public virtual void HeartBeat(HeartBeatContext beatContext) { }
        public virtual void BeforeBlock(Block block) { }
        public virtual void OnBlock(Block block) { }
        public virtual void AfterBlock(Block block) { }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public virtual void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public virtual void ReloadOrders() { }
        public virtual void MenuPageSelected() { }
        public void setColor(UIButton control, Color color)
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

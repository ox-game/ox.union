using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using OX;
using OX.Ledger;
using OX.Network.P2P;
using OX.Wallets.NEP6;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Bapps;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Runtime.InteropServices;
using OX.Tablet;
using OX.Tablet.UIs;
using OX.Tablet.Config;
using Microsoft.Win32;
using System.Diagnostics;
using Akka.Event;
//using OX.Tablet.WeChat;
using Sunny.UI;
//using K4os.Compression.LZ4.Encoders;
//using K4os.Compression.LZ4;
using System.Xml;
using Sunny.UI.Win32;
//using JiebaNet.Segmenter.Common;
using OX.BMS;
using System.Threading;

namespace OX.Tablet
{
    public partial class NoticeForm : Form
    {


        public bool NeedExit = false;
        static NoticeForm _instance;
        public static NoticeForm Instance
        {
            get
            {
                if (_instance.IsNull())
                    _instance = new NoticeForm();
                return _instance;
            }
        }
        private bool isDragging = false;
        private Point downPosition;
        private Point lastFormPosition;
        int c = 0;
        public NoticeForm()
        {
            InitializeComponent();
            this.bt_expand.Text = UIHelper.LocalString("展开", "Expand");         
            this.TopMost = true;
        }

        public void Append(string msg)
        {
            this.lb_msg.Items.Insert(0, msg);
            if (this.lb_msg.Items.Count > 50)
            {
                this.lb_msg.Items.RemoveAt(50);
            }
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void SyncForm_Load(object sender, EventArgs e)
        {
            Screen screen = Screen.PrimaryScreen;
            Rectangle workingArea = screen.WorkingArea;

            int x = workingArea.Right - this.Width - 10;
            int y = workingArea.Top + 10;

            this.Location = new Point(x, y);

        }




        private void SyncForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                downPosition = Cursor.Position;
                lastFormPosition = this.Location;
            }

        }

        private void bt_expand_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm.Instance.Show();
        }

        private void NoticeForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void NoticeForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int moveX = Cursor.Position.X - downPosition.X;
                int moveY = Cursor.Position.Y - downPosition.Y;
                this.Location = new Point(lastFormPosition.X + moveX, lastFormPosition.Y + moveY);
            }
        }


        public void HeartBeat(HeartBeatContext beatContext)
        {

        }

       
         
       
      
        
    }
}

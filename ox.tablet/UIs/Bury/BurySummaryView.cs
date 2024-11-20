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
using Akka.IO;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class BurySummaryView : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        bool needRefresh = false;
        BuryStatisticPanel Plain200Panel;
        BuryStatisticPanel Cipher200Panel;
        BuryStatisticPanel PlainAllPanel;
        BuryStatisticPanel CipherAllPanel;
        public BurySummaryView()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("雷码统计", "Bury Code Statistics");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            InitPosition();
        }
        private void InitPosition()
        {
            Box.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 33.33F);
            Box.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 33.33F);
            Box.RowStyles[0] = new RowStyle(SizeType.Percent, 33.33F);
            Box.RowStyles[1] = new RowStyle(SizeType.Percent, 33.33F);

            Plain200Panel = new BuryStatisticPanel(this, UIHelper.LocalString("当前明码统计", "Current plain code statistics"));
            this.Box.Controls.Add(Plain200Panel, 0, 0);
            Cipher200Panel = new BuryStatisticPanel(this, UIHelper.LocalString("当前暗码统计", "Current secret code statistics"));
            this.Box.Controls.Add(Cipher200Panel, 1, 0);
            PlainAllPanel = new BuryStatisticPanel(this, UIHelper.LocalString("历史明码统计", "Historical plain code statistics"));
            this.Box.Controls.Add(PlainAllPanel, 0, 1);
            CipherAllPanel = new BuryStatisticPanel(this, UIHelper.LocalString("历史暗码统计", "Historical secret code statistics"));
            this.Box.Controls.Add(CipherAllPanel, 1, 1);
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
        public void MenuPageSelected()
        {
            ReloadBurys();
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

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            ReloadBurys();
        }
        void ReloadBurys()
        {
            this.DoInvoke(() =>
            {
                if (this.Visible)
                {
                    this.Plain200Panel.Controls.Clear();
                    this.Cipher200Panel.Controls.Clear();
                    this.PlainAllPanel.Controls.Clear();
                    this.CipherAllPanel.Controls.Clear();
                    if (SecureHelper.BlockIndex.IsNotNull() && SecureHelper.IsAuthenticated)
                    {
                        var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();
                        var number = casinoIndex.BuryNumber;
                        uint p = number > 200 ? number - 200 : 0;
                        int k = 0;
                        Dictionary<BuryCodeKey, uint> dic = new Dictionary<BuryCodeKey, uint>();
                        for (uint i = number; i > p; i--)
                        {
                            var buryRecord = casinoIndex.GetBury(casino.BuryBetAddress, i);
                            if (buryRecord.IsNotNull())
                            {
                                k++;
                                var replyBury = casinoIndex.GetRoomReplyBury(buryRecord.TxId);
                              
                                BuryCodeKey bck = new BuryCodeKey { BetAddress = casino.BuryBetAddress, CodeKind = 0, Code = buryRecord.Request.PlainBuryPoint };

                                if (dic.TryGetValue(bck, out uint c))
                                {
                                    dic[bck] = c + 1;
                                }
                                else
                                {
                                    dic[bck] = 1;
                                }
                                if (replyBury.IsNotNull())
                                {
                                    BuryCodeKey bcw = new BuryCodeKey { BetAddress = casino.BuryBetAddress, CodeKind = 1, Code = replyBury.ReplyBury.PrivateBuryRequest.CipherBuryPoint };

                                    if (dic.TryGetValue(bcw, out uint cc))
                                    {
                                        dic[bcw] = cc + 1;
                                    }
                                    else
                                    {
                                        dic[bcw] = 1;
                                    }
                                }
                            }
                        }


                        foreach (var pair in dic.Where(m => m.Key.CodeKind == 0).OrderByDescending(m => m.Value).Take(40))
                        {
                            this.Plain200Panel.Controls.Add(new UIButton { Width = 100, Height = 30, Text = $"{pair.Key.Code}({pair.Value})" });
                        }
                        foreach (var pair in dic.Where(m => m.Key.CodeKind == 1).OrderByDescending(m => m.Value).Take(40))
                        {
                            this.Cipher200Panel.Controls.Add(new UIButton { Width = 100, Height = 30, Text = $"{pair.Key.Code}({pair.Value})" });
                        }
                        var ps = casinoIndex.GetRoomCodeCount(casino.BuryBetAddress);
                        foreach (var pair in ps.Where(m => m.Key.CodeKind == 0).OrderByDescending(m => m.Value).Take(40))
                        {
                            this.PlainAllPanel.Controls.Add(new UIButton { Width = 100, Height = 30, Text = $"{pair.Key.Code}({pair.Value})" });
                        }
                        foreach (var pair in ps.Where(m => m.Key.CodeKind == 1).OrderByDescending(m => m.Value).Take(40))
                        {
                            this.CipherAllPanel.Controls.Add(new UIButton { Width = 100, Height = 30, Text = $"{pair.Key.Code}({pair.Value})" });
                        }


                    }
                }
            });
        }
    }
}

using Akka.Actor;
using OX.Ledger;
using OX.Network.P2P;
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
using System.Threading;
using OX.BMS;
using OX.IO;
using Akka.Actor.Dsl;
using System.Runtime.InteropServices;
using OX.DirectSales;
using System.Web;
using OX.Tablet.UIs.Mark;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using OX.Tablet.Models;
using Microsoft.Extensions.DependencyInjection;
using Akka.Util;
using AntDesign;
using Org.BouncyCastle.Asn1.Ocsp;
using OX.Cryptography;
using OX.Casino;
using OX.Tablet.Config;
using OX.Tablet.UIs;
using System.IO;
using OX.Tablet.FlashMessages;
namespace OX.Tablet
{
    public partial class MainForm : UIForm2
    {
        private uint PreBlockTime = 0;
        private uint LastIndexTimeStamp = 0;
        SyncLockForm SyncLockForm;
        public bool IsNormalSync
        {
            get
            {
                var stamp = ProtocolSettings.Default.SecondsPerBlock * 10 + 10;
                return PreBlockTime + stamp > DateTime.Now.ToTimestamp();
            }
        }


        BaseContentPage CurrentPage;
        public bool NeedExit = false;
        uint BalanceChangedLastIndex = 0;
        static MainForm _instance;
        public static MainForm Instance
        {
            get
            {
                if (_instance.IsNull())
                    _instance = new MainForm();
                return _instance;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            TabletBlockHandler.BlockCompleted += TabletBlockHandler_BlockCompleted;
            TabletBlockHandler.SyncBlocksCompleted += TabletBlockHandler_SyncBlocksCompleted;
            TabletBlockHandler.FlashMessageCaptured += TabletBlockHandler_FlashMessageCaptured;
            AddClipboardFormatListener(this.Handle);
            this.SyncLockForm = new SyncLockForm(this);
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            BlockIndex.BlockIndexed += BlockIndex_BlockIndexed;
            this.Text = UIHelper.LocalString("OX 生态", "OX Eco");
            this.lb_msg.Text = UIHelper.LocalString("状态消息", "state message");
        }

        private void TabletBlockHandler_BlockCompleted(Block block)
        {
            if (IsDisposed) return;
            //PreBlockTime = block.Timestamp;
            foreach (var module in PageTreeNode.Modules.Values)
            {
                this.DoInvoke(() =>
                {
                    module.BeforeBlock(block);
                });
            }
            foreach (var module in PageTreeNode.Modules.Values)
            {
                this.DoInvoke(() =>
                {
                    module.OnBlock(block);
                });
            }
            foreach (var module in PageTreeNode.Modules.Values)
            {
                this.DoInvoke(() =>
                {
                    module.AfterBlock(block);
                });
            }
        }

        private void TabletBlockHandler_SyncBlocksCompleted(Block block)
        {
            PreBlockTime = block.Timestamp;
        }

        private void TabletBlockHandler_FlashMessageCaptured(FlashMessage message)
        {
            this.DoInvoke(() =>
            {
                var flashIndex = SecureHelper.BlockIndex.GetSubBlockIndex<FlashBlockIndex>();
                flashIndex.OnFlashMessage(message);
            });

            foreach (var module in PageTreeNode.Modules.Values)
            {
                this.DoInvoke(() =>
                {
                    module.OnFlashMessage(message);
                });
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
        const int WM_CLIPBOARDUPDATE = 0x031D;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_CLIPBOARDUPDATE:
                    IDataObject iData = Clipboard.GetDataObject();
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        var str = (string)iData.GetData(DataFormats.Text);
                        new TextInput { Text = str, FromName = string.Empty }.TextHandle();
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        private void BlockIndex_BlockIndexed(uint Index, Block block)
        {
            var ts = System.DateTime.Now.ToTimestamp();
            if (ts > LastIndexTimeStamp + 5)
            {
                LastIndexTimeStamp = ts;
                this.DoInvoke(() =>
                {
                    this.lb_accountIndex.Text = Index.ToString();
                });
            }
        }

        private void uiTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        void RefreshStatus(HeartBeatContext context)
        {
            this.DoInvoke(() =>
            {
                if (context.TimeStamp % 2 == 0 && !NeedExit)
                {
                    var msg = $"  {Blockchain.Singleton.Height}/{Blockchain.Singleton.HeaderHeight}";
                    var nodes = "  " + LocalNode.Singleton.GetRemoteNodes().Count().ToString() + UIHelper.LocalString(" 节点", " Nodes");
                    this.lb_blockchain_state.Text = msg;
                    this.lb_blockchain_nodes.Text = nodes;
                    if (context.IsNormalSync)
                    {
                        if (SyncLockForm.Visible)
                        {
                            SyncLockForm.Hide();
                            if (SecureHelper.NeedExit)
                            {
                                SyncLockForm.allowClose = true;
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        if (!SyncLockForm.Visible)
                            SyncLockForm.ShowDialog();
                        if (SyncLockForm.Visible)
                            SyncLockForm.SetProgress(msg);
                    }
                }
                if (context.Is10Minutes)
                {
                    var setting = NodeConfig.Instance;
                    List<PeerNode> collectSeeds = new List<PeerNode>();
                    foreach (var node in LocalNode.Singleton.GetRemoteNodes())
                    {
                        if (node.Listener.Port == Settings.Default.P2P.Port)
                        {
                            collectSeeds.Add(new PeerNode(node.Listener.Address.ToString(), node.Listener.Port.ToString()));
                        }
                    }
                    if (setting.CollectPeers.IsNotNullAndEmpty())
                    {
                        foreach (var seed in setting.CollectPeers)
                        {
                            if (!collectSeeds.Contains(seed))
                            {
                                collectSeeds.Add(seed);
                            }
                        }
                    }
                    setting.CollectPeers = collectSeeds.Take(20).ToList();
                    setting.Save();
                }
            });
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ReloadPages();
            if (!SecureHelper.IsAuthenticated)
            {
                if (!SyncLockForm.Visible)
                {
                    var cpk = NodeConfig.Instance.CipherPriKey;
                    if (cpk.IsNotNullAndEmpty())
                    {
                        new UnlockAccountForm().ShowDialog();
                    }
                    else
                    {
                        new NewAccountForm().ShowDialog();
                    }
                }
            }

        }
        void ReloadPages()
        {
            this.MainMenu.Nodes.Clear();

            var BitMarkSixNode = new PageTreeNode<MarkSixMainPage>(UIHelper.LocalString("六合彩", "Mark Six"));
            this.MainMenu.Nodes.Add(BitMarkSixNode);
            //var BitMarkOneNode = new PageTreeNode<MarkOneMainPage>(UIHelper.LocalString("一合彩", "Mark One"));
            //this.MainMenu.Nodes.Add(BitMarkOneNode);
            if (SecureHelper.IsSeniorRunMode)
            {
                var EatSmallNode = new PageTreeNode<EatSmallMainPage>(UIHelper.LocalString("大吃小", "Eat Small"));
                this.MainMenu.Nodes.Add(EatSmallNode);
                var BuryNode = new PageTreeNode<BuryMainPage>(UIHelper.LocalString("爆雷", "Bury"));
                this.MainMenu.Nodes.Add(BuryNode);
                var exchangeNode = new PageTreeNode<SwapPage>(UIHelper.LocalString("兑换", "Swap"));
                this.MainMenu.Nodes.Add(exchangeNode);
                var directSale = new PageTreeNode<DirectSale>(UIHelper.LocalString("卖币", "Sale Coin"));
                this.MainMenu.Nodes.Add(directSale);
                var flashMessagePage = new PageTreeNode<FlashMessagePage>(UIHelper.LocalString("闪信", "Flash"));
                this.MainMenu.Nodes.Add(flashMessagePage);
            }
            var AssetNode = new PageTreeNode<AssetManagePage>(UIHelper.LocalString("资产", "Assets"));
            this.MainMenu.Nodes.Add(AssetNode);
            var settingNode = new PageTreeNode<SettingPage>(UIHelper.LocalString("设置", "Setting"));
            SecureHelper.RunModeChanged += Instance_RunModeChanged;
            this.MainMenu.Nodes.Add(settingNode);
        }

        private void Instance_RunModeChanged()
        {
            this.ReloadPages();
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void MainMenu_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            if (node is PageTreeNode ptn)
            {
                try
                {
                    CurrentPage = ptn.FocusPage(false);
                    this.ContentPanel.Controls.Clear();
                    this.ContentPanel.Controls.Add(CurrentPage);
                    CurrentPage.MenuPageSelected();
                    this.Invalidate();
                }
                catch
                {
                    CurrentPage = ptn.FocusPage(true);
                    this.ContentPanel.Controls.Clear();
                    this.ContentPanel.Controls.Add(CurrentPage);
                    CurrentPage.MenuPageSelected();
                    this.Invalidate();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HeartBeatContext context = new HeartBeatContext() { IsNormalSync = IsNormalSync };
            context.BalanceChangedLastIndex = SecureHelper.BalanceChangedLastIndex;
            if (SecureHelper.BalanceChangedLastIndex > this.BalanceChangedLastIndex)
            {
                this.BalanceChangedLastIndex = SecureHelper.BalanceChangedLastIndex;
                context.BalanceChanged = true;
            }
            RefreshStatus(context);
            foreach (var module in PageTreeNode.Modules.Values)
                module.HeartBeat(context);
            HandlerBMSTransactionQueue(context);
            NoticeForm.Instance.HeartBeat(context);
        }
        void HandlerBMSTransactionQueue(HeartBeatContext context)
        {
            if (context.IsOnceHalfMinute && SecureHelper.BlockIndex.IsNotNull())
            {
                var term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
                var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
                if (agentIndex.TermCipherOrders.TryGetValue(term, out var termCipherOrders))
                {
                    foreach (var order in termCipherOrders)
                    {
                        if (order.Value.State == MarkCipherOrderStatus.Inited)
                        {
                            var latest = term.ToDateTime().AddSeconds(SecureHelper.GetMarkSixOpenSeconds() - 60);
                            var UTCLatest = latest.ToTimestamp();
                            var UTCEarliest = latest.AddHours(-24).AddMinutes(2).ToTimestamp();

                            TransactionOutput output = new TransactionOutput()
                            {
                                AssetId = Blockchain.OXC,
                                ScriptHash = MarkBetAddressHelper.Instance.GetMarkBetAddress(order.Key.ChannelRound),
                                Value = Fixed8.One * order.Value.Order.Amount
                            };

                            MarkEncodedBetSet mebs = new MarkEncodedBetSet { BetItems = order.Value.Order.BetItems };
                            var plainCode = mebs.ToArray();
                            var cipherCode = plainCode.Encrypt(SecureHelper.MasterAccount.Key, casino.CasinoMasterAccountPubKey);
                            MarkEncodedBetOrder encodeOrder = new MarkEncodedBetOrder
                            {
                                Term = order.Value.Order.Term,
                                ChannelRound = order.Value.Order.ChannelRound,
                                Player = order.Value.Order.Player,
                                PortHolder = order.Value.Order.PortHolder,
                                EncodedData = cipherCode
                            };
                            var tx = new AskTransaction
                            {
                                Flag = 1,
                                MaxIndex = UTCLatest,
                                MinIndex = UTCEarliest,
                                From = SecureHelper.MasterAccount.Key.PublicKey,
                                Outputs = new[] { output },
                                DataType = (byte)CasinoType.MarkCipherBet,
                                Data = encodeOrder.ToArray(),
                                BizScriptHash = casino.CasinoMasterAccountAddress,
                                Inputs = new CoinReference[0],
                            };
                            var newTx = tx.BuildAndSignOneOXCOutput(Fixed8.Zero);
                            if (newTx.IsNotNull())
                            {
                                Program.BlockHandler.Tell(newTx);
                                agentIndex.Do(batch =>
                                {
                                    order.Value.State = MarkCipherOrderStatus.Broadcast;
                                    order.Value.TxHash = newTx.Hash;
                                    agentIndex.UpdateMarkCipherOrder(batch, order.Key, order.Value);
                                    agentIndex.AddUnconfirmOrder(batch, newTx.Hash, order.Key);
                                });
                                foreach (var coin in newTx.Inputs)
                                {
                                    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            NoticeForm.Instance.Hide();
        }
    }
}

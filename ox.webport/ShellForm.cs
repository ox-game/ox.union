using Akka.Actor;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
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
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Akka.Util;
using Org.BouncyCastle.Asn1.Ocsp;
using OX.Cryptography;
using OX.Casino;
using OX.WebPort.Config;
using System.IO;
using OX.WebPort;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using NBitcoin.Secp256k1;
using Microsoft.AspNetCore.Mvc;
namespace OX.WebPort
{
    public partial class ShellForm : Form
    {
        private IWebHost _webHost;

        private uint PreBlockTime = 0;
        private uint LastIndexTimeStamp = 0;
        public bool IsNormalSync
        {
            get
            {
                var stamp = ProtocolSettings.Default.SecondsPerBlock * 10 + 10;
                return PreBlockTime + stamp > DateTime.Now.ToTimestamp();
            }
        }




        public ShellForm()
        {
            InitializeComponent();

            this.Text = "OX-ECO Web Port";

            this.timer1.Enabled = false;
            this.timer1.Interval = 500;
            this.lb_status.Text = string.Empty;
            this.lb_nodes.Text = string.Empty;
            this.lb_index.Text = string.Empty;
        }

        private void TabletBlockHandler_BlockCompleted(Block block)
        {
            if (IsDisposed) return;
            //PreBlockTime = block.Timestamp;

        }

        private void TabletBlockHandler_SyncBlocksCompleted(Block block)
        {
            PreBlockTime = block.Timestamp;
        }

        private void TabletBlockHandler_FlashMessageCaptured(FlashMessage message)
        {
        }


        private void BlockIndex_BlockIndexed(uint Index, Block block)
        {
            var ts = System.DateTime.Now.ToTimestamp();
            if (ts > LastIndexTimeStamp + 5)
            {
                LastIndexTimeStamp = ts;
                this.DoInvoke(() =>
                {
                    this.lb_index.Text = Index.ToString();
                });
            }
        }

        private void uiTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.BlockHandler = WebPortBlockHandler.Instance;
            Program.BlockIndex = BlockIndex.Instance;
            WebPortBlockHandler.BlockCompleted += TabletBlockHandler_BlockCompleted;
            WebPortBlockHandler.SyncBlocksCompleted += TabletBlockHandler_SyncBlocksCompleted;
            WebPortBlockHandler.FlashMessageCaptured += TabletBlockHandler_FlashMessageCaptured;

            BlockIndex.Instance.BlockIndexed += BlockIndex_BlockIndexed;
        }





        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAspNetCoreHost();
        }

        private void bt_start_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            StartAspNetCoreHost();
        }

        private void StartAspNetCoreHost()
        {
            _webHost = new WebHostBuilder()
               .UseKestrel(options =>
               {
                   options.ListenAnyIP(11332);
                   var path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\obw.pfx";
                   bool ok = true;
                   X509Certificate2 cert = default;
                   try
                   {
                       cert = new X509Certificate2(path, "qazqwe");
                   }
                   catch (Exception e)
                   {
                       ok = false;
                   }
                   if (ok)
                   {
                       options.Listen(System.Net.IPAddress.Any, 443, listenOptions =>
                       {
                           listenOptions.UseHttps(cert);
                       });
                   }
               })
                .UseStartup<Startup>()
                .Build();

            _webHost.Start();
        }
        private void StopAspNetCoreHost()
        {
            _webHost?.StopAsync();
        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HeartBeatContext context = new HeartBeatContext() { IsNormalSync = IsNormalSync };
            RefreshStatus(context);
        }
        void RefreshStatus(HeartBeatContext context)
        {
            this.DoInvoke(() =>
            {
                if (context.TimeStamp % 2 == 0)
                {
                    var msg = $"{Blockchain.Singleton.Height}/{Blockchain.Singleton.HeaderHeight}";
                    var nodes = LocalNode.Singleton.GetRemoteNodes().Count().ToString() + UIHelper.LocalString(" 节点", " Nodes");
                    this.lb_status.Text = msg;
                    this.lb_nodes.Text = nodes;

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

        private void bt_rebuild_Click(object sender, EventArgs e)
        {
            Program.BlockIndex.Rebuild();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hex = "b1fdb502b10002e673665c8a62067bc2764fa4fa75328494bf74546dbf60faf04dd8d21f25e32402062103a051dbb8a7926c23af92413f02ac9ef27b02a6c529a52c9f2d7a479bed1919c30092c88a023efc2823e081ae63e4cc80ed6d03f48376020b2d451c0287cefb8b1d7092d19bd339c64c8cd3000000000000000000570000000000000000000c009411050001c8000500029001050003e803050004b80b05000510270500067017050008fa00050009b40005000af40105000bb40005000c102705000300e803050001fa00050002b40005000000024e841d19b2e250d3d7f5bb0e652b849e4574fad99a05e8e376a004cf053905880100f283f74ff1c2a2c9e82df2afab2554da4fc5dd42b13f5a0554b99f6eac5da656000003af3d1f6d03de30b1abfc3c9110ea4bf9935fc0bc153a665ba9d86558bd369dfb00ca9a3b00000000615b82960a611d40b07d8c266fd0e325164d5c02af3d1f6d03de30b1abfc3c9110ea4bf9935fc0bc153a665ba9d86558bd369dfb00e1f505000000000e737d2f83fd4ca841f2b1a815712ac8c4f6d835af3d1f6d03de30b1abfc3c9110ea4bf9935fc0bc153a665ba9d86558bd369dfbccc52dd20d00000052bea955b157b46fc6b3cd565cb65a219caa0f3e0241407ed72ffc9440a4cd9853ebf79b0ff6f2b9efe87677d2d5a4cccbdaffd0c17efe047730c24fa44434b3e820eb19f8f8fa7b996170207b50339a291536ef2765e3232103a051dbb8a7926c23af92413f02ac9ef27b02a6c529a52c9f2d7a479bed1919c3ac4140ee071f82aa14b865173c4cc0da7148e0b4e96356f6b0a0443366442fa3ab249d3271c549c10d9862f4cbe771c891462dd25b71d7bd7f76e8e29825ec3c138efd3d2103a051dbb8a7926c23af92413f02ac9ef27b02a6c529a52c9f2d7a479bed1919c30003ae9c0f00675a6bf5b3c97ea9bfabee6e13512198f3a88aa441";
            if (hex.HexToBytes().TryAsSerializable<ApiTransactionMessage>(out var message))
            {
                var tx = message.Data.DeserilizeTransaction((byte)message.TxKind);
                if (tx.IsNotNull())
                {
                    var ok = tx.Verify(Blockchain.Singleton.CurrentSnapshot, Blockchain.Singleton.MemPool.GetVerifiedTransactions());
                    if (ok)
                    {
                        Program.BlockHandler.Tell(tx);
                        var lastIndex = BlockchainHelper.LastIndex;
                        foreach (var coin in tx.Inputs)
                        {
                            BlockIndex.Instance.UnconfirmCoins[coin] = lastIndex + 100;
                        }                        
                    }
                }
            }
        }
    }
}

using Akka.Actor;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
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
using OX.Casino;
using OX.Cryptography.ECC;
using Org.BouncyCastle.Asn1.X509;
using System.IO;
using OX;
using OX.IO;
using OX.Tablet.Config;
using Sunny.UI.Win32;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class JointMember : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        protected Dictionary<UInt160, PortInfo> BankerBriefs = new Dictionary<UInt160, PortInfo>();
        bool needResortPorts = false;
        //bool needRefreshBankerBetRecord = false;
        MixMarkMember MyMember;
        public JointMember()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("会员", "Member");
            if (SecureHelper.IsPort())
            {
                this.Text = UIHelper.LocalString("盘口", "Port");
            }
            else if (SecureHelper.IsAgent())
            {
                this.Text = UIHelper.LocalString("代理", "Agent");
            }
            this.bt_member_recharge.Text = UIHelper.LocalString("会员续费", "Renewal");
            this.bt_show_unionsetting.Text = UIHelper.LocalString("公共参数", "Rules");
            this.st_auto_cut.ActiveText = UIHelper.LocalString("自动平仓", "Auto Cut");
            this.st_auto_cut.InActiveText = UIHelper.LocalString("不平仓", "None Cut");
            this.L1.Text = UIHelper.LocalString("所有盘口", "All Ports");
            this.bt_recharge.Text = UIHelper.LocalString("加保证金", "Recharge");
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            foreach (var b in this.BankerBriefs.Values)
            {
                b.HeartBeat(beatContext);
            }
            RefreshTerms();
        }
        public void BeforeBlock(Block block)
        {
            foreach (var b in this.BankerBriefs.Values)
            {
                b.BeforeBlock(block);
            }
        }
        public void OnBlock(Block block)
        {
            foreach (var b in this.BankerBriefs.Values)
            {
                b.OnBlock(block);
            }
            if (needResortPorts)
            {
                ResortBankers();
                needResortPorts = false;
            }

        }
        public void AfterBlock(Block block)
        {
            foreach (var tx in block.Transactions)
            {
                if (tx is SlotSideTransaction sst)
                {
                    if (sst.VerifyRegMarkMember(out ECPoint bankerholderPubKey))
                    {
                        InitPorts();
                    }
                }
                var outputs = tx.Outputs?.Where(m => m.AssetId == Blockchain.OXC)?.Select(m => m.ScriptHash);
                if (outputs.IsNotNullAndEmpty())
                {
                    foreach (var output in outputs)
                    {
                        if (this.BankerBriefs.ContainsKey(output))
                        {
                            needResortPorts = true;
                        }
                    }
                }


            }
            foreach (var b in this.BankerBriefs.Values)
            {
                b.AfterBlock(block);
            }
        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            foreach (var b in this.BankerBriefs.Values)
            {
                b.OnClipboardString(messageType, msg);
            }
        }
        public void MenuPageSelected()
        {
            RefreshTerms();
            InitPorts();
            InitMemberInfo();
            this.Box.Panel2.Controls.Clear();
            if (SecureHelper.IsPort())
            {
                this.Text = UIHelper.LocalString("盘口", "Port");
                if (this.MyMember.IsNotNull())
                {
                    var portDetails = new PortDetails(this.MyMember);
                    this.Box.Panel2.Controls.Add(portDetails);
                    portDetails.Dock = DockStyle.Fill;
                    portDetails.MenuPageSelected();
                }
            }
            else if (SecureHelper.IsAgent())
            {
                this.Text = UIHelper.LocalString("代理", "Agent");
                if (this.MyMember.IsNotNull())
                {
                    var agentDetails = new AgentDetails(this.MyMember);
                    this.Box.Panel2.Controls.Add(agentDetails);
                    agentDetails.Dock = DockStyle.Fill;
                    agentDetails.MenuPageSelected();
                }
            }
            foreach (var b in this.BankerBriefs.Values)
            {
                b.MenuPageSelected();
            }
            this.st_auto_cut.Active = NodeConfig.Instance.AutoCutMarkBet;
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


        void ResortBankers()
        {

            this.DoInvoke(() =>
            {
                this.pn_ports.Panel.Controls.Clear();
                foreach (var b in this.BankerBriefs.Values.OrderByDescending(m => m.Member.TotalPrizeAmount))
                {
                    this.pn_ports.Add(b);
                }
            });
        }
        void InitMemberInfo()
        {
            this.MyMember = SecureHelper.GetMarkMember();
            this.lb_MemberId.Text = string.Empty;
            this.slb_memberType.Text = string.Empty;
            this.slb_memberType.Visible = false;
            if (MyMember.IsNotNull())
            {
                Fixed8 depositBalance = Fixed8.Zero;
                var act = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(MyMember.DepositAddress);
                if (act.IsNotNull())
                {
                    depositBalance = act.GetBalance(Blockchain.OXC);
                    this.lb_deposit.Text = UIHelper.LocalString($"保证金:{depositBalance.ToString("f2")} OXC", $"Bond:{depositBalance.ToString("f2")} OXC");
                }

                this.slb_memberType.Visible = true;
                this.lb_MemberId.Text = UIHelper.LocalString($"我的会员ID: {MyMember.MarkMemberId}", $"My Member ID: {MyMember.MarkMemberId}");

                var memberTypeNames = MyMember.Request.MemberType.GetName();
                this.slb_memberType.Text = UIHelper.LocalString("会员过期", "Member lapse");
                bool ok = false;
                if (MyMember.Request.MemberType == MarkMemberType.Port)
                {
                    ok = MyMember.IsPort();
                    var deposit = SecureHelper.GetPortMinDeposit();
                    this.slb_memberType.Symbol = depositBalance >= deposit ? 361452 : 361453;
                }
                else
                {
                    ok = MyMember.IsAgent();
                    this.slb_memberType.Symbol = ok ? 361452 : 361453;
                }
                if (ok)
                {
                    this.slb_memberType.Text = UIHelper.LocalString(memberTypeNames.Name, memberTypeNames.EngName);
                }

                this.lb_due_member.Text = UIHelper.LocalString($"{MyMember.ExpireTimeStamp.ToDateTime().ToString("yyyy-MM-dd")} 到期", $"Due {MyMember.ExpireTimeStamp.ToDateTime().ToString("yyyy-MM-dd")}");

            }
        }
        void InitPorts()
        {
            this.DoInvoke(() =>
            {
                var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
                this.pn_ports.Clear();
                this.BankerBriefs.Clear();
                int bbIndex = 1;
                foreach (var member in bmsIndex.AllMarkMembers)
                {
                    if (member.IsPort())
                    {
                        var bb = new PortInfo(member);
                        bb.TabIndex = bbIndex++;
                        this.BankerBriefs[member.DepositAddress] = bb;
                        bb.BankerSelected += Bb_BankerSelected;
                    }
                }
                foreach (var b in this.BankerBriefs.Values.OrderByDescending(m => m.Member.TotalPrizeAmount))
                {
                    this.pn_ports.Add(b);
                }
            });
        }

        private void Bb_BankerSelected(PortInfo bb)
        {
            foreach (var c in this.pn_ports.GetAllControls())
            {
                if (c is PortInfo bi)
                {
                    if (bi != bb)
                    {
                        bi.SetSelectState(false);
                    }
                }
            }

        }
        Dictionary<BitMarkSixBetTarget, uint> Merge(BitMarkSixBet[][] BetItems)
        {
            Dictionary<BitMarkSixBetTarget, uint> dic = new Dictionary<BitMarkSixBetTarget, uint>();
            foreach (var order in BetItems)
            {
                foreach (var item in order)
                {
                    var amt = item.Amount;
                    if (dic.TryGetValue(item.BetTarget, out uint v))
                    {
                        amt += v;
                    }
                    dic[item.BetTarget] = amt;
                }
            }
            return dic;
        }


        void RefreshTerms()
        {
            this.DoInvoke(() =>
            {
                //var newBankerTerm = System.DateTime.Now.ToUniversalTime().GetBetTermForBanker();
                //var newPlayerTerm = System.DateTime.Now.ToUniversalTime().GetBetTermForPlayer();
                //if (this.CurrentBankerTerm != newBankerTerm)
                //{
                //    this.lb_banker_term.Text = UIHelper.LocalString($"庄家注期:{newBankerTerm.ToString()}", $"Banker:{newBankerTerm.ToString()}");
                //    this.CurrentBankerTerm = newBankerTerm;
                //    InitBankers();
                //}
                //if (this.CurrentPlayerTerm != newPlayerTerm)
                //{
                //    this.lb_player_term.Text = UIHelper.LocalString($"闲家注期:{newPlayerTerm.ToString()}", $"Player:{newPlayerTerm.ToString()}");
                //    this.CurrentPlayerTerm = newPlayerTerm;
                //}
            });
        }

        private void bt_new_banker_Click(object sender, EventArgs e)
        {
            new RechargeMemberForm().ShowDialog();
        }



        private void bt_tool_simple_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/简彩下单.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("简彩下单器 已复制", "简彩下单器  copied"));
        }

        private void bt_tool_full_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/全彩下单.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("全彩下单器 已复制", "全彩下单器  copied"));
        }

        private void bt_Agent_setting_Click(object sender, EventArgs e)
        {
            new ShowBankerSettingForm(SecureHelper.GetMarkAdminMember()).ShowDialog();
            //if (MyMember.IsNotNull())
            //{
            //    if (SecureHelper.GetMasterBalanceStates().TryGetValue(Blockchain.OXC, out var balanceState))
            //    {
            //        var frm = new MasterSimpleTransferForm(balanceState, this.MyMember.BetAddress);
            //        frm.Render();
            //        frm.ShowDialog();
            //        if (frm.IsOK)
            //        {
            //            var tx = frm.BuildTx();
            //            if (tx.IsNotNull())
            //            {
            //                Program.BlockHandler.Tell(tx);
            //                foreach (var coin in tx.Inputs)
            //                {
            //                    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
            //                }
            //                new WaitTxForm(tx, UIHelper.LocalString("等待交易确认...", "Waiting  confirm transaction")).ShowDialog();
            //            }
            //        }
            //        frm.Dispose();
            //    }
            //}
        }

        private void Box_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void st_auto_cut_ValueChanged(object sender, bool value)
        {
            NodeConfig.Instance.MarkAutoCut = value.ToString();
            NodeConfig.Instance.Save();
        }

        private void bt_recharge_Click(object sender, EventArgs e)
        {
            var balance = SecureHelper.GetMasterAvailableBalance(Blockchain.OXC);
            var frm = new MemberDepositForm(balance, MyMember.DepositAddress);
            frm.Render();
            frm.ShowDialog();
            if (frm.IsOK)
            {
                var tx = frm.BuildTx();
                if (tx.IsNotNull())
                {
                    Program.BlockHandler.Tell(tx);
                    foreach (var coin in tx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(tx, UIHelper.LocalString("等待交易确认...", "Waiting  confirm transaction")).ShowDialog();
                }
            }
            frm.Dispose();
        }
    }
}

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
using OX.Tablet.Config;
using OX.Bapps;
using OX.Casino;
using OX.Persistence;
using OX.Tablet.FlashMessages;
using System.IO;
using OX.SmartContract;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using OX.VM;
using OX.Wallets;
using Nethereum.Model;
using OneOf.Types;

namespace OX.Tablet.FlashMessages
{
    public partial class SetSemanticSubPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        const string pattern = @"^[a-z0-9_-]+$";

        public SetSemanticSubPage()
        {
            InitializeComponent();
            this.lb_name.Text = UIHelper.LocalString("名称:", "Name:");
            this.bt_refresh.Text = UIHelper.LocalString("设置名称", "Set Name");
            this.tb_msg.Text = UIHelper.LocalString("闪信是一个发送即焚的去中心化加密通讯功能，只有在消息发送时，消息接收人的软件保持在线状态才能成功接收消息。而且，只有成功注册了唯一名称的账户才能发送消息，名称只能使用英文字母，数字，下划线和中划线，而且一经注册永久不能改变。每个账户所能发送信息的最低要求是主账户持有至少300个OXC，而且持有的OXC金额越大，允许发送消息的频率越高。", "Flash Message is a decentralized encrypted communication function that sends and burns messages, and the recipient's software can only successfully receive the message when it is sent and kept online. Moreover, only accounts that have successfully registered a unique name can send messages. The name can only use English letters, numbers, underscores, and dashes, and once registered, it cannot be permanently changed. The minimum requirement for each account to send messages is that the main account holds at least 300 OXCs, and the larger the amount of OXCs held, the higher the frequency of messages allowed to be sent.");
            var dmbs = FlashMessageHelper.GetDomain(SecureHelper.MasterAccount.ScriptHash);
            if (dmbs.IsNotNullAndEmpty())
            {
                this.tb_name.Text = System.Text.Encoding.UTF8.GetString(dmbs);
            }
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
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {

        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {

        }
        public virtual void MenuPageSelected()
        {

            loadMessages();
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



        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }
        void loadMessages()
        {


        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            var name = this.tb_name.Text.Trim();
            if (name.IsNotNullAndEmpty())
            {
                RegisterDomain(name);
            }
        }
        void RegisterDomain(string domain)
        {
            var dmbs = FlashMessageHelper.GetDomain(SecureHelper.MasterAccount.ScriptHash);
            if (dmbs.IsNullOrEmpty())
            {
                ContractState contract = Blockchain.Singleton.Store.GetContracts().TryGet(Blockchain.FlashMessageContractScriptHash);
                var parameters = contract.ParameterList.Select(p => new ContractParameter(p)).ToArray();
                parameters[0].Value = "register";
                List<ContractParameter> list = new List<ContractParameter>();
                list.Add(new ContractParameter { Type = ContractParameterType.ByteArray, Value = System.Text.Encoding.UTF8.GetBytes(domain) });
                list.Add(new ContractParameter { Type = ContractParameterType.Hash160, Value = SecureHelper.MasterAccount.ScriptHash }); ;
                parameters[1].Value = list;
                byte[] scripts = default;
                using (ScriptBuilder sb = new ScriptBuilder())
                {
                    sb.EmitAppCall(Blockchain.FlashMessageContractScriptHash, parameters);
                    scripts = sb.ToArray();
                }
                var tx = new InvocationTransaction();
                tx.Version = 1;
                tx.Script = scripts;
                if (tx.Attributes == null) tx.Attributes = new TransactionAttribute[0];
                if (tx.Inputs == null) tx.Inputs = new CoinReference[0];
                if (tx.Outputs == null) tx.Outputs = new TransactionOutput[0];
                if (tx.Witnesses == null) tx.Witnesses = new Witness[0];


                Fixed8 fee = Fixed8.One;
                if (tx.Size > 1024)
                {
                    Fixed8 sumFee = Fixed8.FromDecimal(tx.Size * 0.00001m) + Fixed8.FromDecimal(0.001m);
                    if (fee < sumFee)
                    {
                        fee = sumFee;
                    }
                }

                var newTx = tx.BuildAndSignNoneOutput(fee,new AvatarAccount { Contract = SecureHelper.MasterAccount.Contract, Key = SecureHelper.MasterAccount.Key, ScriptHash = SecureHelper.MasterAccount.ScriptHash });

                if (newTx.IsNotNull())
                {
                    Program.BlockHandler.Tell(newTx);
                    foreach (var coin in newTx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(newTx, UIHelper.LocalString("等待确认注册名称...", "Waiting  confirm registwer name ..."), Success => {
                        if (Success)
                        {
                            Application.Exit();
                        }
                    }).ShowDialog();
                }
            }
            else
            {
                this.ShowErrorTip(UIHelper.LocalString("你已经注册过名称，不能重复注册", "You have already registered the name, you cannot register again"));
            }
        }
        private void pn_pairs_SizeChanged(object sender, EventArgs e)
        {

        }
        bool Match(string s)
        {
            return Regex.IsMatch(s, pattern);
        }
        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            var s = this.tb_name.Text;
            var length = s.Length;
            if (length > 20 || !Match(s))
            {
                if (s.IsNotNullAndEmpty())
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_name.Clear();
                    this.tb_name.AppendText(s);
                }
            }
        }
    }
}

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
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.DataProtection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OX.IO;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class EmailConfigPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public EmailConfigPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("QQ 邮箱设置", "QQ Email Setting");
            this.lb_smtp.Text = UIHelper.LocalString("SMTP 服务器:", "SMTP Server:");
            this.lb_pop3.Text = UIHelper.LocalString("POP3 服务器:", "POP3 Server:");
            this.lb_username.Text = UIHelper.LocalString("邮箱地址:", "Email:");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
            this.bt_do_setting.Text = UIHelper.LocalString("保存设置", "Save Setting");
            this.setColor(this.bt_do_setting, FocusColor);
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

        public virtual void MenuPageSelected()
        {

            var hex = NodeConfig.Instance.Email;
            if (hex.IsNotNullAndEmpty())
            {
                if (hex.HexToBytes().TryAsSerializable<EmailConfig>(out var config))
                {
                    this.tb_smtp.Text = config.SmtpServer;
                    this.tb_smtp_port.Text = config.SmtpPort;
                    this.tb_pop3.Text = config.Pop3Server;
                    this.tb_pop3_port.Text = config.Pop3Port;
                    this.tb_username.Text = config.UserName;
                    this.tb_pwd.Text = config.Pwd;
                }
            }
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


        private void bt_refresh_Click(object sender, EventArgs e)
        {
            EmailConfig config = new EmailConfig
            {
                SmtpServer = tb_smtp.Text.Trim(),
                SmtpPort = tb_smtp_port.Text.Trim(),
                Pop3Server = tb_pop3.Text.Trim(),
                Pop3Port = tb_pop3_port.Text.Trim(),
                UserName = tb_username.Text.Trim(),
                Pwd = tb_pwd.Text.Trim(),
            };
            bool secure = true;
            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(config.SmtpServer, int.Parse(config.SmtpPort), secure ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                    client.Authenticate(config.UserName, config.Pwd);

                    //Console.WriteLine("邮件服务器地址可以连接和认证成功！");
                    var hex = config.ToArray().ToHexString();
                    NodeConfig.Instance.Email = hex;
                    NodeConfig.Instance.Save();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("无法连接到邮件服务器： " + ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}

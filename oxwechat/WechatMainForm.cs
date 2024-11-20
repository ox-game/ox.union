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
using Microsoft.Win32;
using System.Diagnostics;
using Akka.Event;
using Sunny.UI;
using System.Xml;
using Sunny.UI.Win32;
using OX.BMS;
using System.Threading;
using OX.IO;

namespace OX.WeChat
{
    public partial class WechatMainForm : Form
    {


        public bool NeedExit = false;
        static WechatMainForm _instance;
        public static WechatMainForm Instance
        {
            get
            {
                if (_instance.IsNull())
                    _instance = new WechatMainForm();
                return _instance;
            }
        }
        private bool isDragging = false;
        private Point downPosition;
        private Point lastFormPosition;
        int c = 0;
        public WechatMainForm()
        {
            InitializeComponent();
            this.bt_step1.Text = UIHelper.LocalString("1.重启微信", "1.Start Wachat");
            this.bt_step2.Text = UIHelper.LocalString("2.一键收单", "2.Acquiring");
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");

            this.bt_union.Text = UIHelper.LocalString("港澳联合", "Mark Union");
            this.bt_hk.Text = UIHelper.LocalString("香港", "Mark HK");
            this.bt_macau.Text = UIHelper.LocalString("澳门", "Mark Macau");
            this.bt_one.Text = UIHelper.LocalString("一合", "Mark One");
            this.bt_decoder.Text = UIHelper.LocalString("解码器", "Decoder");
            this.bt_app.Text = UIHelper.LocalString("49助手", "49 Helper");
            this.bt_player.Text = UIHelper.LocalString("港澳玩家", "Union Player");
            this.TopMost = true;
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
            int y = workingArea.Bottom - this.Height - 10;

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

        private void bt_step1_Click(object sender, EventArgs e)
        {
            var path = GetWeChatInstallPath();
            KillAllWeChat();
            Thread.Sleep(1000);
            Process.Start(path + "\\WeChat.exe");
        }
        void KillAllWeChat()
        {
            Process[] processes = Process.GetProcessesByName("wechat");
            foreach (Process p in processes)
            {
                p.Kill(true);
                //p.Kill();
            }
        }
        private void bt_step2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            this.bt_step2.Enabled = false;
            ExportWechatDatabase();
        }
        public static string GetWeChatInstallPath()
        {
            string path = "";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Tencent\WeChat");
            if (key != null)
            {
                object installPath = key.GetValue("InstallPath");
                if (installPath != null)
                {
                    path = installPath.ToString();
                }
            }
            return path;
        }
        void ExportWechatDatabase()
        {
            string WechatId = string.Empty;
            ProcessInfo info = default;
            Process[] processes = Process.GetProcessesByName("wechat");
            foreach (Process p in processes)
            {
                var lHandles = NativeAPIHelper.GetHandleInfoForPID((uint)p.Id);
                foreach (var h in lHandles)
                {
                    string name = NativeAPIHelper.FindHandleName(h, p);
                    if (name != "")
                    {
                        //if (File.Exists("handle.log"))
                        //{
                        //    File.AppendAllText("handle.log", string.Format("{0}|{1}|{2}|{3}\n", p.Id, h.ObjectTypeIndex, h.HandleValue, name));
                        //}
                        if (name.Contains("\\MicroMsg.db") && name.Substring(name.Length - 3, 3) == ".db")
                        {
                            info = new ProcessInfo();
                            info.ProcessId = p.Id.ToString();
                            info.ProcessName = p.ProcessName;
                            info.DBPath = DevicePathMapper.FromDevicePath(name)!;
                            string[] name_raw = info.DBPath.Split("\\");
                            var UserName = name_raw[name_raw.Length - 3];

                            FileInfo fileInfo = new FileInfo(info.DBPath);
                            DirectoryInfo msgParent = fileInfo.Directory!.Parent!;
                            //WechatId = msgParent.Name;
                            DirectoryInfo[] accounts = msgParent.GetDirectories();

                            DirectoryInfo? newUserName = null;
                            foreach (DirectoryInfo account in accounts)
                            {
                                if (account.Name.Contains("account_"))
                                {
                                    if (newUserName == null)
                                        newUserName = account;
                                    else
                                    {
                                        if (newUserName.LastWriteTime < account.LastWriteTime)
                                            newUserName = account;
                                    }
                                }
                            }

                            if (newUserName != null)
                            {
                                WechatId = newUserName.Name.Split("_")[1];
                            }
                        }
                    }
                }
            }
            if (WechatId.IsNotNullAndEmpty())
            {
                string path = info.DBPath.Replace("\\Msg\\MicroMsg.db", "");
                try
                {
                    bool ok = false;
                    WXWorkspace wXWorkspace = new WXWorkspace(path, WechatId);
                    wXWorkspace.MoveDB();
                    for (int i = 1; i < 4; i++)
                    {
                        WXUserReader UserReader = default;
                        try
                        {
                            wXWorkspace.DecryptDB(info.ProcessId, i);

                            var config = wXWorkspace.ReturnConfig();
                            UserReader = new WXUserReader(config);
                            var contacts = UserReader.GetWXContacts().ToList();
                            List<WXContact> process = new List<WXContact>();
                            List<WXMsg[]> list = new List<WXMsg[]>();
                            foreach (var contact in contacts!)
                            {
                                if (!contact.UserName.Contains("@chatroom") && !contact.UserName.Contains("gh_"))
                                {
                                    //process.Add(contact);
                                    var strs = GetMsgString(UserReader, contact);
                                    if (strs.Any())
                                    {
                                        list.Add(strs);
                                    }
                                }
                            }
                            ok = true;
                            List<TextInput> inputs = new List<TextInput>();
                            foreach (var strs in list)
                            {
                                foreach (var str in strs)
                                {
                                    if (str.IsNotNull())
                                    {
                                        var input = new TextInput { Text = str.StrContent, FromName = str.NickName };
                                        inputs.Add(input);
                                    }
                                }
                            }
                            TextInputArray tia = new TextInputArray { Inputs = inputs.ToArray() };
                            var bs = Convert.ToBase64String(tia.ToArray());
                            Clipboard.SetText(bs);
                            this.ShowSuccessTip(UIHelper.LocalString("一键收单完成并复制,请检查是否有漏洞需要手工复制入库", "One click receipt completion, please check if there are any loopholes that need to be manually copied and stored"));
                            this.timer1.Stop();
                            UserReader.Close();
                            KillAllWeChat();
                            this.bt_step2.Enabled = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (UserReader.IsNotNull())
                                UserReader.Close();
                            this.ShowErrorTip(i.ToString() + "/////" + ex.Message);
                            continue;
                        }
                    }
                    if (!ok)
                    {
                        KillAllWeChat();
                        this.ShowErrorTip(UIHelper.LocalString("一键收单失败", "One click acquiring failed"));
                    }
                }
                catch (Exception e2)
                {
                    KillAllWeChat();
                    this.ShowErrorTip(e2.Message);
                }
            }
            else
            {
                KillAllWeChat();
                this.ShowErrorTip(UIHelper.LocalString("请先尝试重新修改你的微信号", "Please try to modify your WeChat ID again first"));
            }
        }
        public WXMsg[] GetMsgString(WXUserReader reader, WXContact session)
        {
            List<WXMsg> ret = new List<WXMsg>();
            var dt = System.DateTime.Now.ToUniversalTime();
            MarkTerm term = new MarkTerm((ushort)dt.Year, (byte)dt.Month, (byte)dt.Day);
            List<WXMsg>? msgList = reader.GetWXMsgs(session.UserName, term);
            if (msgList.IsNotNullAndEmpty())
            {
                msgList.Sort((x, y) => x.CreateTime.CompareTo(y.CreateTime));
                foreach (var msg in msgList)
                {
                    string txtMsg = "";
                    switch (msg.Type)
                    {
                        case 1:
                            //var ts = (uint)msg.CreateTime;
                            //var dt = ts.ToDateTime();
                            txtMsg = msg.StrContent;
                            if (msg.NickName == "我")
                            {

                            }
                            if (msg.NickName == "收1003")
                            {

                            }
                            if (!msg.IsSender)
                            {
                                if (msg.NickName.StartsWith("收"))
                                {
                                    ret.Add(msg);
                                }
                            }
                            break;
                        case 3:
                            txtMsg = "[图片]";
                            break;
                        case 34:
                            txtMsg = "[语音]";
                            break;
                        case 43:
                            txtMsg = "[视频]";
                            break;
                        case 49:
                            if (msg.SubType == 6 || msg.SubType == 19 || msg.SubType == 40)
                            {
                                txtMsg = "[文件]";
                            }
                            else
                            {
                                //try
                                //{
                                //    using (var decoder = LZ4Decoder.Create(true, 64))
                                //    {
                                //        byte[] target = new byte[10240];
                                //        int res = 0;
                                //        if (msg.CompressContent != null)
                                //            res = LZ4Codec.Decode(msg.CompressContent, 0, msg.CompressContent.Length, target, 0, target.Length);

                                //        byte[] data = target.Skip(0).Take(res).ToArray();
                                //        string xml = Encoding.UTF8.GetString(data);
                                //        if (!string.IsNullOrEmpty(xml))
                                //        {
                                //            xml = xml.Replace("\n", "");
                                //            XmlDocument xmlObj = new XmlDocument();
                                //            xmlObj.LoadXml(xml);
                                //            if (xmlObj.DocumentElement != null)
                                //            {
                                //                string title = "";
                                //                string appName = "";
                                //                string url = "";
                                //                XmlNodeList? findNode = xmlObj.DocumentElement.SelectNodes("/msg/appmsg/title");
                                //                if (findNode != null)
                                //                {
                                //                    if (findNode.Count > 0)
                                //                    {
                                //                        title = findNode[0]!.InnerText;
                                //                    }
                                //                }
                                //                findNode = xmlObj.DocumentElement.SelectNodes("/msg/appmsg/sourcedisplayname");
                                //                if (findNode != null)
                                //                {
                                //                    if (findNode.Count > 0)
                                //                    {
                                //                        appName = findNode[0]!.InnerText;
                                //                    }
                                //                }
                                //                findNode = xmlObj.DocumentElement.SelectNodes("/msg/appmsg/url");
                                //                if (findNode != null)
                                //                {
                                //                    if (findNode.Count > 0)
                                //                    {
                                //                        url = findNode[0]!.InnerText;
                                //                    }
                                //                }
                                //                txtMsg = string.Format("{0},标题：{1},链接：{2}", appName, title, url);
                                //            }
                                //            else
                                //            {
                                //                txtMsg = "[分享链接出错了]";
                                //            }
                                //        }
                                //        else
                                //        {
                                //            txtMsg = "[分享链接出错了]";
                                //        }
                                //    }
                                //}
                                //catch
                                //{
                                //    txtMsg = "[分享链接出错了]";
                                //}
                            }
                            break;
                    }
                }
            }
            return ret.ToArray();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            if (c > 30)
            {
                this.bt_step2.Enabled = true;
                timer1.Stop();
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_union_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/港澳联合.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("港澳联合下单器 已复制", "港澳联合下单器  copied"));
        }

        private void bt_hk_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/香港.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("香港下单器 已复制", "香港下单器  copied"));
        }

        private void bt_macau_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/澳门.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("澳门下单器 已复制", "澳门下单器  copied"));
        }

        private void bt_one_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/一合.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("一合下单器 已复制", "一合下单器  copied"));
        }

        private void bt_decoder_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/解码器.html");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("解码器 已复制", "解码器  copied"));
        }

        private void bt_app_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/49助手.apk");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("49助手 已复制", "49Helper  copied"));
        }

        private void bt_player_Click(object sender, EventArgs e)
        {
            var filepath = Path.GetFullPath("tools/港澳玩家.apk");
            System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
            files.Add(filepath);
            Clipboard.SetFileDropList(files);
            this.ShowSuccessTip(UIHelper.LocalString("港澳玩家 已复制", "Union Player  copied"));
        }
    }
}

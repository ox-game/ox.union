using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Ledger;
using Sunny.UI;
using OX.Persistence;
using OX.Network.P2P.Payloads;
using System.Threading;
using OX.Tablet.Config;

namespace OX.Tablet
{
    public partial class SeedManageForm : Form
    {

        public SeedManageForm()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("管理种子节点", "manage seed nodes");
            this.lb_port.Text = UIHelper.LocalString("端口:", "Port:");
            this.bt_add.Text = UIHelper.LocalString("增加", "Add");
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
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

        private void UnlockAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SeedManageForm_Load(object sender, EventArgs e)
        {
            var extSeeds = NodeConfig.Instance.ExtSeedPeers;
            if (extSeeds.IsNotNullAndEmpty() && extSeeds.Count > 0)
            {
                foreach (var ext in extSeeds)
                {
                    UIButton uib = new UIButton { Text = ext.ToString(), Width = 300, Tag = ext };
                    setColor(uib, Color.DarkGray);
                    uib.UseDoubleClick = true;
                    uib.DoubleClick += Uib_DoubleClick;
                    this.pn_pairs.Add(uib);
                }
            }
        }

        private void Uib_DoubleClick(object sender, EventArgs e)
        {
            if (sender is UIButton uib)
            {
                this.pn_pairs.Remove(uib);
            }
            ReSave();
        }
        void ReSave()
        {
            List<PeerNode> list = new List<PeerNode>();
            foreach (var c in this.pn_pairs.GetAllControls())
            {
                if (c is UIButton uib)
                {
                    list.Add(uib.Tag as PeerNode);
                }
            }
            NodeConfig.Instance.ExtSeedPeers = list;
            NodeConfig.Instance.Save();
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            var ip = this.tb_ip.Value;
            var port = this.tb_port.Text;
            PeerNode pn = new PeerNode(ip.ToString(), port.ToString());
            UIButton uib = new UIButton { Text = pn.ToString(), Tag = pn, Width = 300 };
            setColor(uib, Color.DarkGray);
            uib.UseDoubleClick = true;
            uib.DoubleClick += Uib_DoubleClick;
            this.pn_pairs.Add(uib);
            ReSave();
        }
    }
}

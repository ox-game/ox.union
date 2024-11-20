using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Nethereum.Util;
using OX.Wallets;
using OX.Tablet.Config;
namespace OX.Tablet
{
    public delegate void ContacCreated(Contact contact);
    public partial class NewContact : UserControl
    {
        public event ContacCreated ContacCreated;
        public NewContact()
        {
            InitializeComponent();
            this.uiGroupBox1.Text = UIHelper.LocalString("新建联系人", "New Contact");
            this.lb_name.Text = UIHelper.LocalString("名称:", "Name:");
            this.lb_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.bt_delete.Text = UIHelper.LocalString("创建", "New");

            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
        }

        private void bt_transfer_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact { Kind = 0, Name = this.tb_name.Text.Trim(), Address = this.tb_address.Text.Trim() };
            if (contact.Name.IsNotNullAndEmpty())
            {
                if (contact.Address.IsValidEthereumAddressHexFormat())
                {
                    contact.Kind = 2;
                }
                if (contact.Kind == 0)
                {
                    try
                    {
                        contact.Address.ToScriptHash();
                        contact.Kind = 1;
                    }
                    catch
                    {

                    }
                }
                if (contact.Kind > 0)
                {
                    this.ContacCreated?.Invoke(contact);
                }
            }
        }





        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }

    }
}

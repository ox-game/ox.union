using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Tablet.Config;
using Sunny.UI;

namespace OX.Tablet
{
    public delegate void ContactDeleted(Contact contact);
    public partial class ContactItem : UserControl
    {
        Contact Contact;
        public event ContactDeleted ContactDeleted;
        public ContactItem(Contact contact)
        {
            Contact = contact;
            InitializeComponent();
            this.uiGroupBox1.Text = contact.Name;
            this.bt_delete.Text = UIHelper.LocalString("删除", "Delete");
            this.bt_copy.Text = UIHelper.LocalString("复制", "Copy");
            this.lb_address.Text = contact.Address;

            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
        }

        private void bt_transfer_Click(object sender, EventArgs e)
        {
            this.ContactDeleted?.Invoke(Contact);
        }





        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }

        private void bt_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Contact.Address);
        }
    }
}

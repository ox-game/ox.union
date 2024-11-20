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
using OX.IO;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class ContactSubPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        ContactSet CurrentContactSet = default;
        public ContactSubPage()
        {
            InitializeComponent();          
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
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            
        }

        public virtual void MenuPageSelected()
        {
            Reload();
        }
        void Reload()
        {
            this.pn_contacts.Clear();
            var cs = NodeConfig.Instance.Contacts; //Settings.Default.Contacts;
            int tabIndex = 1;
            NewContact nc = new NewContact();
            nc.TabIndex = 0;
            nc.ContacCreated += Nc_ContacCreated;
            this.pn_contacts.Add(nc);
            if (cs.IsNotNullAndEmpty())
            {
                try
                {
                    CurrentContactSet = cs.HexToBytes().AsSerializable<ContactSet>();
                    if (CurrentContactSet.IsNotNull())
                    {
                        foreach (var contact in CurrentContactSet.Contacts)
                        {
                            ContactItem ci = new ContactItem(contact);
                            ci.ContactDeleted += Ci_ContactDeleted;
                            ci.TabIndex = tabIndex++;
                            this.pn_contacts.Add(ci);
                        }
                    }
                }
                catch
                {

                }
            }
        }
        private void Nc_ContacCreated(Contact contact)
        {
            if (CurrentContactSet.IsNull())
                CurrentContactSet = new ContactSet { Contacts = new Contact[0] };
            var list = CurrentContactSet.Contacts.ToList();
            list.Add(contact);
            CurrentContactSet.Contacts = list.ToArray();
            NodeConfig.Instance.Contacts = CurrentContactSet.ToArray().ToHexString();
            NodeConfig.Instance.Save();
            Reload();
        }

        private void Ci_ContactDeleted(Contact contact)
        {
            if (CurrentContactSet.IsNotNull() && CurrentContactSet.Contacts.IsNotNullAndEmpty())
            {
                CurrentContactSet.Contacts = CurrentContactSet.Contacts.Where(m => m != contact).ToArray();
                NodeConfig.Instance.Contacts = CurrentContactSet.ToArray().ToHexString();
                NodeConfig.Instance.Save();
                Reload();
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
 
    }
}

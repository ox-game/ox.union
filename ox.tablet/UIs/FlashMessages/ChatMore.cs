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

namespace OX.Tablet.FlashMessages
{
    public partial class ChatMore : UIButton
    {

        public ChatMore() : base()
        {
            this.Text = UIHelper.LocalString("更多...", "More...");
            this.Click += ChatMore_Click;
        }

        private void ChatMore_Click(object sender, EventArgs e)
        {
        }

        public void ResetSize(int maxwidth)
        {
            this.Width = maxwidth;
            ResumeLayout();
        }


    }
}

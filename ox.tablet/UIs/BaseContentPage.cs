using OX.Network.P2P.Payloads;
using Sunny.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace OX.Tablet
{
    public class BaseContentPage : UserControl, IBlockchainModule
    {
        public static BaseContentPage CurrentPage { get; set; }
        public BaseContentPage()
        {
            this.Dock = DockStyle.Fill;
        }

        public virtual string ModuleKey { get; }
        public virtual void HeartBeat(HeartBeatContext beatContext) { }
        public virtual void BeforeBlock(Block block) { }
        public virtual void OnBlock(Block block) { }
        public virtual void AfterBlock(Block block) { }
        public virtual void OnFlashMessage(FlashMessage message) { }
        public virtual void MenuPageSelected() { }
        public virtual void OnClipboardString(ClipboardMessageType messageType, string msg) { }

    }
}
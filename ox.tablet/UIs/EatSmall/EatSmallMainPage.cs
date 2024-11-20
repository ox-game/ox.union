using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.BMS;
using OX.Cryptography.ECC;
using OX.Casino;
using OX.Ledger;
using Sunny.UI;
using OX.Tablet.UIs.MarkSix;
using OX.Wallets;
using Sunny.UI.Win32;
using Akka.IO;

namespace OX.Tablet
{
    public partial class EatSmallMainPage : BaseContentPage
    {
        Dictionary<UInt160, RoomView> RoomViews = new Dictionary<UInt160, RoomView>();

        public EatSmallMainPage()
        {
            InitializeComponent();
            InitPages();
        }
        public static string _moduleKey => "eatsmall";
        public override string ModuleKey => _moduleKey;


        public override void HeartBeat(HeartBeatContext beatContext)
        {
            foreach (var plaza in this.RoomViews.Values)
                plaza.HeartBeat(beatContext);
        }
        public override void BeforeBlock(Block block)
        {
            foreach (var plaza in this.RoomViews.Values)
                plaza.BeforeBlock(block);
        }
        public override void OnBlock(Block block)
        {
            foreach (var plaza in this.RoomViews.Values)
                plaza.OnBlock(block);
        }
        public override void AfterBlock(Block block)
        {
            foreach (var plaza in this.RoomViews.Values)
                plaza.AfterBlock(block);
        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            foreach (var plaza in this.RoomViews.Values)
                plaza.OnClipboardString(messageType, msg);
        }
        public override void MenuPageSelected()
        {
            foreach (var plaza in this.RoomViews.Values)
                plaza.MenuPageSelected();
        }
        void InitPages()
        {
            if (SecureHelper.BlockIndex.IsNotNull())
            {
                var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();
                var defaultRooms = casinoIndex.GetCasinoSetting(CasinoSettingTypes.DefaultEatSmallRooms);
                if (defaultRooms.IsNotNullAndEmpty())
                {
                 
                    foreach (var s in defaultRooms.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    {
                        var betAddress = s.ToScriptHash();
                        if (casinoIndex.MixRooms.TryGetValue(betAddress, out var room))
                        {
                            var page = new RoomView(room) { Text = room.RoomId.ToString() };
                            RoomViews.Add(betAddress, page);
                            this.tbc_sections.AddPage(page);
                        }
                    }
                }
            }
        }
    }
}

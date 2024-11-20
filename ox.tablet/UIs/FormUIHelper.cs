using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OX.Tablet
{
    public static class FormUIHelper
    {

        public static void DoInvoke(this Control control, Action action)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.BeginInvoke(new Action(() =>
                    {
                        action();
                    }));
                }
                else
                {
                    action();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

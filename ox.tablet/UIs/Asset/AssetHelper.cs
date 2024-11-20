using OX.Cryptography.ECC;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using OX.SmartContract;
using OX.Wallets;
using OX.Wallets.NEP6;
using OX.BMS;
using OX.Tablet.UIs.MarkSix;
using OX.Consensus;
using Sunny.UI;

namespace OX.Tablet
{
    public interface ITransferForm
    {
        Transaction BuildTx();
    }
    public class BaseTransferForm : UIEditForm, ITransferForm
    {
        public virtual Transaction BuildTx()
        {
            return default;
        }
    }
}

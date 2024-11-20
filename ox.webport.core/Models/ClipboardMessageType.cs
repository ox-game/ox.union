using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.WebPort
{
    public enum ClipboardMessageType : byte
    {
        MarkOrder = 1,
        DirectSaleReply = 2,
        DirectSaleApprove=3
    }
}

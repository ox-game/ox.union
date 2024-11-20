using OX.BMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Tablet.Models
{
    public enum TextOrderResult : byte
    {
        [Name("成功", "Success")]
        Success = 1 << 0,
        [Name("格式错误", "Format Error")]
        FormatError = 1 << 1,
        [Name("订单冲突", "ID Conflict")]
        SNConflict = 1 << 2
    }
}

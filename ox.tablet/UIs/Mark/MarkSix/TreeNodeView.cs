using OX.BMS;
using System.Windows.Forms;

namespace OX.Tablet.UIs.Mark.MarkSix
{
    public class TreeNodeView
    {
        public TreeNode Node;
        public MarkInboundOrder Order => this.Node.Tag as MarkInboundOrder;
        public override string ToString()
        {
            var order = Node.Tag as MarkInboundOrder;
            return $"{Node.Text}  [{order.Amount}]";
        }
    }
}

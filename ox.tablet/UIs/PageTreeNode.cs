using OX.Network.P2P.Payloads;
using System.Windows.Forms;
using Sunny.UI;
using System.Collections.Generic;

using System.ComponentModel;
namespace OX.Tablet
{

    public abstract class PageTreeNode : TreeNode
    {
        public PageTreeNode(string text) : base(text)
        {

        }
        public static Dictionary<string, BaseContentPage> Modules = new Dictionary<string, BaseContentPage>();
        public abstract BaseContentPage FocusPage(bool renewForce);
    }
    public class PageTreeNode<T> : PageTreeNode where T : BaseContentPage, new()
    {
        public T Instance { get; private set; }
        public PageTreeNode(string text) : base(text)
        {
            this.Tag = typeof(T);
        }
        public override BaseContentPage FocusPage(bool renewForce)
        {
            if (renewForce || Instance.IsNull())
            {
                Instance = new T();
                Modules[Instance.ModuleKey] = Instance;
            }
            BaseContentPage.CurrentPage = Instance;
            return Instance;
        }
    }
}
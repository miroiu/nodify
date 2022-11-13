using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for CommentNodeView.xaml
    /// </summary>
    public partial class CommentNodeView : UserControl, IViewFor<CommentNode>
    {
        public CommentNodeView()
        {
            InitializeComponent();
        }
    }
}

using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for GraphNodeView.xaml
    /// </summary>
    public partial class GraphNodeView : UserControl, IViewFor<INodeWidget>
    {
        public GraphNodeView()
        {
            InitializeComponent();
        }
    }
}

using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for NodeValueOutputView.xaml
    /// </summary>
    public partial class NodeValueOutputView : UserControl, IViewFor<ValueOutput<double>>, IViewFor<ValueOutput<int>>, IViewFor<ValueOutput<string>>, IViewFor<ValueOutput<object>>
    {
        public NodeValueOutputView()
        {
            InitializeComponent();
        }
    }
}

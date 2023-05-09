using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for NodeFlowOutputView .xaml
    /// </summary>
    public partial class NodeFlowOutputView : UserControl, IViewFor<FlowOutput>
    {
        public NodeFlowOutputView()
        {
            InitializeComponent();
        }
    }
}

using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for NodeFlowInputView .xaml
    /// </summary>
    public partial class NodeFlowInputView : UserControl, IViewFor<FlowInput>
    {
        public NodeFlowInputView()
        {
            InitializeComponent();
        }
    }
}

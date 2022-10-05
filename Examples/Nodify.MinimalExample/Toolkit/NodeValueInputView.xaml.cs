using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for NodeValueInputView.xaml
    /// </summary>
    public partial class NodeValueInputView : UserControl, IViewFor<ValueInput<int>>, IViewFor<ValueInput<object>>, IViewFor<ValueInput<string>>, IViewFor<ValueInput<double>>, IViewFor<ValueInput<bool>>
    {
        public NodeValueInputView()
        {
            InitializeComponent();
        }
    }
}

using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for StringValueEditorView.xaml
    /// </summary>
    public partial class StringValueEditorView : UserControl, IViewFor<ValueEditor<string>>, IViewFor<ValueEditor<int>>, IViewFor<ValueEditor<double>>
    {
        public StringValueEditorView()
        {
            InitializeComponent();
        }
    }
}

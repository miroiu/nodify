using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for EmptyValueEditorView.xaml
    /// </summary>
    public partial class EmptyValueEditorView : UserControl, IViewFor<ValueEditor<object>>
    {
        public EmptyValueEditorView()
        {
            InitializeComponent();
        }
    }
}

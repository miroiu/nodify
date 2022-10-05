using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier
{
    /// <summary>
    /// Interaction logic for PreviewElementView.xaml
    /// </summary>
    public partial class PreviewElementView : UserControl, IViewFor<IPreviewElement>
    {
        public PreviewElementView()
        {
            InitializeComponent();
        }
    }
}

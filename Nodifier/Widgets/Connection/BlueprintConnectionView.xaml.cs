using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for BlueprintConnectionView.xaml
    /// </summary>
    public partial class BlueprintConnectionView : UserControl, IViewFor<BlueprintConnection>
    {
        public BlueprintConnectionView()
        {
            InitializeComponent();
        }
    }
}

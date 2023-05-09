using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for BlueprintPendingConnectionView.xaml
    /// </summary>
    public partial class BlueprintPendingConnectionView : UserControl, IViewFor<BlueprintPendingConnection>
    {
        public BlueprintPendingConnectionView()
        {
            InitializeComponent();
        }
    }
}

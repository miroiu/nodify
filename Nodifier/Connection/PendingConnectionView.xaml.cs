using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for PendingConnectionView.xaml
    /// </summary>
    public partial class PendingConnectionView : UserControl, IViewFor<IPendingConnection>
    {
        public PendingConnectionView()
        {
            InitializeComponent();
        }
    }
}

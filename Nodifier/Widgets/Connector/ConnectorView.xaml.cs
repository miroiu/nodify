using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for ConnectorView.xaml
    /// </summary>
    public partial class ConnectorView : UserControl, IViewFor<IConnector>
    {
        public ConnectorView()
        {
            InitializeComponent();
        }
    }
}

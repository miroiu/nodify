using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : UserControl, IViewFor<IConnection>
    {
        public ConnectionView()
        {
            InitializeComponent();
        }
    }
}

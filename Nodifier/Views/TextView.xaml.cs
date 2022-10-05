using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for TextView.xaml
    /// </summary>
    public partial class TextView : UserControl, IViewFor<string>
    {
        public TextView()
        {
            InitializeComponent();
        }
    }
}

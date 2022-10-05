using Nodifier.XAML;
using Nodify;
using System.Windows.Controls;

namespace Nodifier.Views
{
    public interface INodifyEditorAware
    {
        NodifyEditor Editor { get; }
    }

    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : UserControl, INodifyEditorAware, IViewFor<IGraph>
    {
        public GraphView()
        {
            InitializeComponent();
        }

        public NodifyEditor Editor => EditorInstance;
    }
}

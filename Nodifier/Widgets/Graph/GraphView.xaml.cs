using Nodifier.XAML;
using Nodify;
using System.Windows.Controls;

namespace Nodifier.Views
{
    public interface IEditorHost
    {
        NodifyEditor Editor { get; }
    }

    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : UserControl, IEditorHost, IViewFor<IGraphWidget>
    {
        public GraphView()
        {
            InitializeComponent();
        }

        public NodifyEditor Editor => EditorInstance;
    }
}

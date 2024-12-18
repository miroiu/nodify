using Nodify.Events;
using System.Windows.Controls;

namespace Nodify.Playground
{
    public partial class NodifyEditorView : UserControl
    {
        public NodifyEditor EditorInstance => Editor;

        public NodifyEditorView()
        {
            InitializeComponent();
        }

        private void Minimap_Zoom(object sender, ZoomEventArgs e)
        {
            EditorInstance.ZoomAtPosition(e.Zoom, e.Location);
        }
    }
}

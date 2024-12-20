using Nodify.Events;
using Nodify.Interactivity;
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

        static NodifyEditorView()
        {
            InputProcessor.Shared<Connector>.RegisterHandlerFactory(elem => new RetargetConnections(elem));
        }

        private void Minimap_Zoom(object sender, ZoomEventArgs e)
        {
            EditorInstance.ZoomAtPosition(e.Zoom, e.Location);
        }
    }
}

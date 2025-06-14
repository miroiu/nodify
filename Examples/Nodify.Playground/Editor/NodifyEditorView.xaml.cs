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

            EditorInstance.ActiveNavigationLayerChanged += DisplayActiveNavigationLayer;
        }

        static NodifyEditorView()
        {
            InputProcessor.Shared<Connector>.ReplaceHandlerFactory<ConnectorState.Connecting>(elem => new CustomConnecting(elem));
            InputProcessor.Shared<Connector>.RegisterHandlerFactory(elem => new RetargetConnections(elem));
        }

        private void Minimap_Zoom(object sender, ZoomEventArgs e)
        {
            EditorInstance.ZoomAtPosition(e.Zoom, e.Location);
        }

        private void DisplayActiveNavigationLayer(KeyboardNavigationLayerId layerId)
        {
            var editorVm = (NodifyEditorViewModel)EditorInstance.DataContext;

            if (layerId == KeyboardNavigationLayerId.Nodes)
            {
                editorVm.KeyboardNavigationLayer = nameof(KeyboardNavigationLayerId.Nodes);
            }
            else if (layerId == KeyboardNavigationLayerId.Connections)
            {
                editorVm.KeyboardNavigationLayer = nameof(KeyboardNavigationLayerId.Connections);
            }
            else if (layerId == KeyboardNavigationLayerId.Decorators)
            {
                editorVm.KeyboardNavigationLayer = nameof(KeyboardNavigationLayerId.Decorators);
            }
            else
            {
                editorVm.KeyboardNavigationLayer = "Custom";
            }
        }
    }
}

using System.Windows;

namespace Nodify.StateMachine
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Connector.EnableStickyConnections = true;
            NodifyEditor.EnableCuttingLinePreview = true;

            EditorGestures.Mappings.Connection.Disconnect.Value = MultiGesture.None;
        }
    }
}

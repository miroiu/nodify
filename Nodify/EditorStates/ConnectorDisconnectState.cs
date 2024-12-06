using System.Windows.Input;

namespace Nodify
{
    public class ConnectorDisconnectState : InputElementState<Connector>
    {
        public ConnectorDisconnectState(Connector connector) : base(connector)
        {
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (gestures.Disconnect.Matches(e.Source, e))
            {
                Element.Focus();
                e.Handled = true;   // prevent interacting with the container
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (gestures.Disconnect.Matches(e.Source, e))
            {
                Element.RemoveConnections();
                e.Handled = true;   // prevent opening context menu
            }
        }
    }
}

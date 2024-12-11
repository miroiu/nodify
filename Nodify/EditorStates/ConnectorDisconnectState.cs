using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Represents a state in which a connector can be disconnected from its connections based on specific gestures.
    /// </summary>
    public class ConnectorDisconnectState : InputElementState<Connector>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorDisconnectState"/> class.
        /// </summary>
        /// <param name="connector">The <see cref="Connector"/> element associated with this state.</param>
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

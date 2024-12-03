using System.Windows.Input;

namespace Nodify
{
    public class ConnectorDefaultState : ConnectorState
    {
        public ConnectorDefaultState(Connector connector) : base(connector) { }

        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (gestures.Disconnect.Matches(e.Source, e))
            {
                Connector.RemoveConnections();
            }
            else if (gestures.Connect.Matches(e.Source, e))
            {
                if (Connector.EnableStickyConnections)
                {
                    Connector.PushState(new ConnectorConnectingStickyState(Connector));
                }
                else
                {
                    Connector.PushState(new ConnectorConnectingState(Connector));
                }
            }

            e.Handled = true;   // prevent interacting with the container
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public class ConnectorConnectingState : ConnectorState
    {
        protected bool Canceled { get; set; } = Connector.AllowPendingConnectionCancellation;

        public ConnectorConnectingState(Connector connector) : base(connector) { }

        public override void Enter(ConnectorState? from)
        {
            Canceled = false;
            Connector.BeginConnecting();
        }

        public override void Exit()
        {
            // TODO: This is not canceled on LostMouseCapture (add OnLostMouseCapture/OnCancel callback?)
            if (Canceled)
            {
                Connector.CancelConnecting();
                Connector.ReleaseMouseCapture();
            }
            else
            {
                Connector.EndConnecting();
            }
        }

        public override void HandleMouseMove(MouseEventArgs e)
        {
            Vector thumbOffset = e.GetPosition(Connector.Thumb) - new Point(Connector.Thumb.ActualWidth / 2, Connector.Thumb.ActualHeight / 2);
            Point editorPosition = Connector.Anchor + thumbOffset;

            Connector.UpdatePendingConnection(editorPosition);
        }

        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (gestures.Connect.Matches(e.Source, e))
            {
                e.Handled = true;   // prevents opening context menu
                PopState();
            }
            else if (Connector.AllowPendingConnectionCancellation && Connector.IsPendingConnection && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                e.Handled = true;   // prevents opening context menu

                PopState();
            }
        }

        public override void HandleKeyUp(KeyEventArgs e)
        {
            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (Connector.AllowPendingConnectionCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }

    public class ConnectorConnectingStickyState : ConnectorConnectingState
    {
        public ConnectorConnectingStickyState(Connector connector) : base(connector) { }

        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (Connector.IsPendingConnection && !gestures.CancelAction.Matches(e.Source, e))
            {
                PopState();

                e.Handled = true;  // prevent interacting with the container
            }
        }

        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            e.Handled = Connector.IsPendingConnection;

            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (Connector.AllowPendingConnectionCancellation && Connector.IsPendingConnection && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }
}

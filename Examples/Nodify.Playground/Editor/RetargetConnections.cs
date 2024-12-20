using Nodify.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Playground
{
    /// <summary>
    /// Connecting state that prevents connecting when <see cref="RetargetConnections"/> is in progress.
    /// </summary>
    public class CustomConnecting : ConnectorState.Connecting
    {
        protected override bool CanBegin => !RetargetConnections.InProgress;

        public CustomConnecting(Connector connector) : base(connector)
        {
        }
    }

    /// <summary>
    /// Hold CTRL+LeftClick on a connector to start reconnecting it.
    /// </summary>
    public class RetargetConnections : DragState<Connector>
    {
        public static InputGestureRef Reconnect { get; } = new Interactivity.MouseGesture(MouseAction.LeftClick, ModifierKeys.Control)
        {
            IgnoreModifierKeysOnRelease = true
        };

        /// <summary>
        /// Used to prevent connecting when <see cref="EditorSettings.EnableStickyConnectors"/> is enabled.
        /// </summary>
        public static bool InProgress { get; private set; }

        protected override bool CanBegin => ViewModel.IsConnected && ViewModel.Flow == ConnectorFlow.Input;
        protected override bool IsToggle => EditorSettings.Instance.EnableStickyConnectors;

        private ConnectorViewModel ViewModel => (ConnectorViewModel)Element.DataContext;
        private Vector _connectorOffset;
        private Connector? _targetConnector;

        public RetargetConnections(Connector element) : base(element, Reconnect, EditorGestures.Mappings.Connector.CancelAction)
        {
            PositionElement = Element.Editor ?? (IInputElement)Element;
        }

        protected override void OnBegin(InputEventArgs e)
        {
            _connectorOffset = ViewModel.Node.Orientation == Orientation.Horizontal
                ? (Vector)EditorSettings.Instance.ConnectionTargetOffset.Value
                : new Vector(EditorSettings.Instance.ConnectionTargetOffset.Value.Y, EditorSettings.Instance.ConnectionTargetOffset.Value.X);

            InProgress = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var position = Element.Editor!.MouseLocation;

            if (EditorSettings.Instance.EnablePendingConnectionHitTesting)
            {
                var connector = Element.FindTargetConnector(position);
                connector?.UpdateAnchor();

                SetTargetConnector(connector);
                UpdateConnections(connector != null ? connector.Anchor : position + _connectorOffset);
            }
            else
            {
                UpdateConnections(position + _connectorOffset);
            }
        }

        private void UpdateConnections(Point position)
        {
            foreach (var connection in ViewModel.Connections)
            {
                connection.Input.Anchor = position;
            }
        }

        protected override void OnEnd(InputEventArgs e)
        {
            var position = Element.Editor!.MouseLocation;
            var target = Element.FindTargetConnector(position);
            target?.UpdateAnchor();

            if (target?.DataContext is ConnectorViewModel targetVM && ViewModel != targetVM)
            {
                ViewModel.Node.Graph.Schema.Rewire(ViewModel, targetVM);
            }

            SetTargetConnector(null);

            // Reset the position of connections that were not rewired
            Element.UpdateAnchor();
            InProgress = false;
        }

        protected override void OnCancel(InputEventArgs e)
        {
            SetTargetConnector(null);

            // Reset the position of connections that were not rewired
            Element.UpdateAnchor();
            InProgress = false;
        }

        /// <summary>
        /// Sets the connection target and updates the visual state of the target element.
        /// </summary>
        private void SetTargetConnector(Connector? target)
        {
            if (target == _targetConnector)
            {
                return;
            }

            if (_targetConnector != null)
            {
                PendingConnection.SetIsOverElement(_targetConnector, false);
            }

            if (target != null)
            {
                PendingConnection.SetIsOverElement(target, true);
            }

            _targetConnector = target;
        }
    }
}

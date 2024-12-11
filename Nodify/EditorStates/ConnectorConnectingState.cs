using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Represents the state for handling a connector's connecting operation in the editor, 
    /// enabling drag-based creation of connections between connectors.
    /// </summary>
    public class ConnectorConnectingState : DragState<Connector>
    {
        protected override bool HasContextMenu => Element.HasContextMenu;
        protected override bool CanCancel => Connector.AllowPendingConnectionCancellation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorConnectingState"/> class.
        /// </summary>
        /// <param name="connector">The connector associated with this state.</param>
        public ConnectorConnectingState(Connector connector)
            : base(connector, EditorGestures.Mappings.Connector.Connect, EditorGestures.Mappings.Connector.CancelAction)
        {
            PositionElement = Element.Editor ?? (IInputElement)Element;
        }

        protected override bool IsToggle => Connector.EnableStickyConnections;

        protected override void OnBegin(InputEventArgs e)
            => Element.BeginConnecting();

        protected override void OnEnd(InputEventArgs e)
            => Element.EndConnecting();

        protected override void OnCancel(InputEventArgs e)
            => Element.CancelConnecting();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Vector thumbOffset = e.GetPosition(Element.Thumb) - new Point(Element.Thumb.ActualWidth / 2, Element.Thumb.ActualHeight / 2);
            Point editorPosition = Element.Anchor + thumbOffset;

            Element.UpdatePendingConnection(editorPosition);
        }
    }
}

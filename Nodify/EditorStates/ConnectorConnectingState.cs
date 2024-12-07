using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public class ConnectorConnectingState : DragState<Connector>
    {
        protected override bool CanCancel => Connector.AllowPendingConnectionCancellation;

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

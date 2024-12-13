using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class ConnectionState
    {
        /// <summary>
        /// Represents a state in which a connection can be disconnected from its connectors based on specific gestures.
        /// </summary>
        public class Disconnect : InputElementState<BaseConnection>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Disconnect"/> class.
            /// </summary>
            /// <param name="connection">The <see cref="BaseConnection"/> element associated with this state.</param>
            public Disconnect(BaseConnection connection) : base(connection)
            {
            }

            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                EditorGestures.ConnectionGestures gestures = EditorGestures.Mappings.Connection;
                if (gestures.Disconnect.Matches(e.Source, e))
                {
                    Element.Focus();
                    e.Handled = true;   // prevent interacting with the editor
                }
            }

            protected override void OnMouseUp(MouseButtonEventArgs e)
            {
                EditorGestures.ConnectionGestures gestures = EditorGestures.Mappings.Connection;
                if (gestures.Disconnect.Matches(e.Source, e))
                {
                    Element.Remove();
                    e.Handled = true;   // prevent opening context menu
                }
            }
        }
    }
}

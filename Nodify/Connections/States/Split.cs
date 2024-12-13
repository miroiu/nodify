using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class ConnectionState
    {
        /// <summary>
        /// Represents a state in which a connection can be split.
        /// </summary>
        public class Split : InputElementState<BaseConnection>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Split"/> class.
            /// </summary>
            /// <param name="connection">The <see cref="BaseConnection"/> element associated with this state.</param>
            public Split(BaseConnection connection) : base(connection)
            {
            }

            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                EditorGestures.ConnectionGestures gestures = EditorGestures.Mappings.Connection;
                if (gestures.Split.Matches(e.Source, e))
                {
                    Element.Focus();
                    Element.SplitAtLocation(e.GetPosition(Element));

                    e.Handled = true;   // prevent interacting with the editor
                }
            }
        }
    }
}

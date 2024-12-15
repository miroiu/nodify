using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class ConnectionState
    {
        /// <summary>
        /// Represents the default input state for a <see cref="BaseConnection"/>.
        /// </summary>
        public class Default : InputElementState<BaseConnection>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Default"/> class.
            /// </summary>
            /// <param name="connection">The <see cref="BaseConnection"/> element associated with this state.</param>
            public Default(BaseConnection connection) : base(connection)
            {
            }

            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                // Allow context menu to appear
                if (e.ChangedButton == MouseButton.Right && Element.HasContextMenu)
                {
                    Element.Focus();
                    e.Handled = true;   // prevents the editor capturing the mouse
                }
            }
        }
    }
}

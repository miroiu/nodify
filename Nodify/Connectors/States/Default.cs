using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class ConnectorState
    {
        /// <summary>
        /// Represents the default input state for a <see cref="Connector"/>.
        /// </summary>
        public class Default : InputElementState<Connector>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Default"/> class.
            /// </summary>
            /// <param name="connector">The <see cref="Connector"/> element associated with this state.</param>
            public Default(Connector connector) : base(connector)
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

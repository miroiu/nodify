using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Defines a contract for handling input events within an element or system.
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Handles a given input event, such as a mouse or keyboard interaction.
        /// </summary>
        /// <param name="e">The <see cref="InputEventArgs"/> representing the input event.</param>
        void HandleEvent(InputEventArgs e);
    }
}

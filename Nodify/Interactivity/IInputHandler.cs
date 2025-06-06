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
        /// <remarks>
        /// This method is invoked when an input event is dispatched to the handler. Implementations should 
        /// handle the event logic and optionally mark the event as handled.
        /// </remarks>
        void HandleEvent(InputEventArgs e);

        /// <summary>
        /// Gets a value indicating whether the handler requires input capture to remain active.
        /// </summary>
        /// <remarks>
        /// This property can be used to determine whether it is safe to release mouse capture, especially during toggled interactions. <br />
        /// Toggled interactions usually involve two steps, and it is important to keep the input capture active until the interaction is completed.
        /// </remarks>
        bool RequiresInputCapture { get; }

        /// <summary>
        /// Gets or sets a value indicating whether events that have been handled should be processed too.
        /// </summary>
        bool ProcessHandledEvents { get; }
    }
}

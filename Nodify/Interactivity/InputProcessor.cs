using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Processes input events and delegates them to registered handlers.
    /// </summary>
    public partial class InputProcessor
    {
        private readonly List<IInputHandler> _handlers = new List<IInputHandler>();

        /// <summary>
        /// Gets a value indicating whether the processor has ongoing interactions that require input capture to remain active.
        /// </summary>
        /// <remarks>
        /// This property can be used to determine whether it is safe to release mouse capture, especially during toggled interactions. <br />
        /// Toggled interactions usually involve two steps, and it is important to keep the input capture active until the interaction is completed.
        /// </remarks>
        public bool RequiresInputCapture { get; private set; }

        /// <summary>
        /// Adds an input handler to the processor.
        /// </summary>
        /// <param name="handler">The input handler to add.</param>
        public void AddHandler(IInputHandler handler)
            => _handlers.Add(handler);

        /// <summary>
        /// Removes all handlers of the specified type from the processor.
        /// </summary>
        /// <typeparam name="T">The type of the handler to remove.</typeparam>
        public void RemoveHandlers<T>() where T : IInputHandler
            => _handlers.RemoveAll(x => x.GetType() == typeof(T));

        /// <summary>
        /// Clears all registered handlers.
        /// </summary>
        public void Clear()
            => _handlers.Clear();

        /// <summary>
        /// Processes an input event and delegates it to the registered handlers.
        /// </summary>
        /// <param name="e">The input event arguments to process.</param>
        public void ProcessEvent(InputEventArgs e)
        {
            RequiresInputCapture = false;

            for (int i = 0; i < _handlers.Count; i++)
            {
                IInputHandler handler = _handlers[i];
                if (!e.Handled || handler.ProcessHandledEvents)
                {
                    handler.HandleEvent(e);
                    RequiresInputCapture |= handler.RequiresInputCapture;
                }
            }
        }
    }
}

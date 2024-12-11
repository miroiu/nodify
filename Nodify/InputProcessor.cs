using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Processes input events and delegates them to registered handlers.
    /// </summary>
    public sealed class InputProcessor
    {
        private readonly HashSet<IInputHandler> _handlers = new HashSet<IInputHandler>();

        /// <summary>
        /// Gets or sets a value indicating whether events that have been handled should be processed.
        /// </summary>
        public bool ProcessHandledEvents { get; set; }

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
            => _handlers.RemoveWhere(x => x.GetType() == typeof(T));

        /// <summary>
        /// Clears all registered handlers.
        /// </summary>
        public void Clear()
            => _handlers.Clear();

        /// <summary>
        /// Processes an input event and delegates it to the registered handlers.
        /// </summary>
        /// <param name="e">The input event arguments to process.</param>
        public void Process(InputEventArgs e)
        {
            if (ProcessHandledEvents)
            {
                foreach (var handler in _handlers)
                {
                    handler.HandleEvent(e);
                }
            }
            else
            {
                foreach (var handler in _handlers)
                {
                    if (e.Handled)
                    {
                        break;
                    }

                    handler.HandleEvent(e);
                }
            }
        }
    }
}

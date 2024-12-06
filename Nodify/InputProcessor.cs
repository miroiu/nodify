using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify
{
    public class InputProcessor
    {
        private readonly HashSet<IInputHandler> _handlers = new HashSet<IInputHandler>();

        public bool ProcessHandledEvents { get; set; }

        public void AddHandler(IInputHandler handler)
            => _handlers.Add(handler);

        public void RemoveHandlers<T>() where T : IInputHandler
            => _handlers.RemoveWhere(x => x.GetType() == typeof(T));

        public void Process(InputEventArgs e)
        {
            foreach (var handler in _handlers)
            {
                if (e.Handled && !ProcessHandledEvents)
                {
                    break;
                }

                handler.HandleEvent(e);
            }
        }
    }
}

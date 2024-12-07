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

        public void Clear()
            => _handlers.Clear();

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

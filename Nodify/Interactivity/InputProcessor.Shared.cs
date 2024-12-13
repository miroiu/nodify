using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public sealed partial class InputProcessor
    {
        /// <summary>
        /// A shared input processor that allows registering and managing global input handlers for a specific type of UI element.
        /// </summary>
        /// <typeparam name="TElement">The type of the UI element that the input handlers will be associated with.</typeparam>
        public sealed class Shared<TElement> : IInputHandler
            where TElement : FrameworkElement
        {
            private static readonly List<KeyValuePair<Type, Func<TElement, IInputHandler>>> _handlerFactories = new List<KeyValuePair<Type, Func<TElement, IInputHandler>>>();

            private readonly List<IInputHandler> _handlers = new List<IInputHandler>();

            /// <summary>
            /// Initializes a new instance of the <see cref="Shared{TElement}"/> class for the specified UI element.
            /// </summary>
            /// <param name="element">The UI element that the shared input handlers will be associated with.</param>
            public Shared(TElement element)
            {
                foreach (var kvp in _handlerFactories)
                {
                    _handlers.Add(kvp.Value(element));
                }
            }

            /// <summary>
            /// Initializes static members of the <see cref="Shared{TElement}"/> class.
            /// Ensures default handlers are registered before methods on this class can be used.
            /// </summary>
            static Shared()
            {
                if (typeof(TElement) == typeof(NodifyEditor))
                {
                    EditorState.RegisterDefaultHandlers();
                }
                else if (typeof(TElement) == typeof(ItemContainer))
                {
                    ContainerState.RegisterDefaultHandlers();
                }
                else if (typeof(TElement) == typeof(Connector))
                {
                    ConnectorState.RegisterDefaultHandlers();
                }
            }

            public void HandleEvent(InputEventArgs e)
            {
                foreach (var handler in _handlers)
                {
                    handler.HandleEvent(e);
                }
            }

            /// <summary>
            /// Registers a factory method for creating an input handler of the specified type.
            /// </summary>
            /// <typeparam name="THandler">The type of the input handler to register.</typeparam>
            /// <param name="factory">A factory method that creates an instance of the input handler for a given UI element.</param>
            /// <exception cref="InvalidOperationException">
            /// Thrown if an input handler of the specified type is already registered.
            /// </exception>
            public static void RegisterHandlerFactory<THandler>(Func<TElement, THandler> factory)
                where THandler : IInputHandler
            {
                if (_handlerFactories.Any(x => x.Key == typeof(THandler)))
                {
                    throw new InvalidOperationException($"An input handler of type '{typeof(THandler)}' has already been registered.");
                }

                _handlerFactories.Add(new KeyValuePair<Type, Func<TElement, IInputHandler>>(typeof(THandler), elem => factory(elem)));
            }

            /// <summary>
            /// Removes all registered factory methods for creating input handlers of the specified type.
            /// </summary>
            /// <typeparam name="THandler">The type of the input handler to remove.</typeparam>
            public static void RemoveHandlerFactories<THandler>()
                => _handlerFactories.RemoveAll(x => x.Key == typeof(THandler));

            /// <summary>
            /// Clears all registered handler factories, effectively removing all shared input handlers.
            /// </summary>
            public static void ClearHandlerFactories()
                => _handlerFactories.Clear();
        }
    }
}

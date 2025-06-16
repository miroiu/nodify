using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Represents a base class for handling input events in a specific state for a framework element.
    /// </summary>
    /// <typeparam name="TElement">The type of the framework element that owns this state.</typeparam>
    public abstract class InputElementState<TElement> : IInputHandler
        where TElement : FrameworkElement
    {
        /// <summary>
        /// Gets the owner of the state.
        /// </summary>
        protected TElement Element { get; }

        public bool RequiresInputCapture { get; protected set; }
        public bool ProcessHandledEvents { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputElementState{TElement}"/> class.
        /// </summary>
        /// <param name="element">The framework element that owns this state.</param>
        protected InputElementState(TElement element)
        {
            Element = element;
        }

        /// <inheritdoc cref="UIElement.OnMouseDown(MouseButtonEventArgs)"/>
        protected virtual void OnMouseDown(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnMouseUp(MouseButtonEventArgs)"/>
        protected virtual void OnMouseUp(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnMouseMove(MouseEventArgs)"/>
        protected virtual void OnMouseMove(MouseEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnMouseWheel(MouseWheelEventArgs)"/>
        protected virtual void OnMouseWheel(MouseWheelEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnKeyUp(KeyEventArgs)"/>
        protected virtual void OnKeyUp(KeyEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnKeyDown(KeyEventArgs)"/>
        protected virtual void OnKeyDown(KeyEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnLostMouseCapture(MouseEventArgs)"/>
        protected virtual void OnLostMouseCapture(MouseEventArgs e) { }

        /// <summary>
        /// Called for any input event that is not explicitly handled by other methods.
        /// </summary>
        /// <param name="e">The input event arguments.</param>
        protected virtual void OnEvent(InputEventArgs e) { }

        /// <summary>
        /// Processes the input event by invoking the appropriate handler method based on the routed event.
        /// </summary>
        /// <param name="e">The input event arguments.</param>
        public void HandleEvent(InputEventArgs e)
        {
            if (e.RoutedEvent == UIElement.MouseMoveEvent)
            {
                OnMouseMove((MouseEventArgs)e);
            }
            else if (e.RoutedEvent == UIElement.MouseDownEvent)
            {
                OnMouseDown((MouseButtonEventArgs)e);
            }
            else if (e.RoutedEvent == UIElement.MouseUpEvent)
            {
                OnMouseUp((MouseButtonEventArgs)e);
            }
            else if (e.RoutedEvent == UIElement.MouseWheelEvent)
            {
                OnMouseWheel((MouseWheelEventArgs)e);
            }
            else if (e.RoutedEvent == UIElement.LostMouseCaptureEvent)
            {
                OnLostMouseCapture((MouseEventArgs)e);
            }
            else if (e.RoutedEvent == UIElement.KeyDownEvent)
            {
                OnKeyDown((KeyEventArgs)e);
            }
            else if (e.RoutedEvent == UIElement.KeyUpEvent)
            {
                OnKeyUp((KeyEventArgs)e);
            }

            OnEvent(e);
        }
    }
}

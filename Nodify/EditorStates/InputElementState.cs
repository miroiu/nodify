using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public interface IInputHandler
    {
        void HandleEvent(InputEventArgs e);
    }

    public abstract class InputElementState<TElement> : IInputHandler
        where TElement : FrameworkElement
    {
        /// <summary>The owner of the state.</summary>
        protected TElement Element { get; }

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

        protected virtual void OnEvent(InputEventArgs e) { }

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

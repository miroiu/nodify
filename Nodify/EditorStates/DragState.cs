using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Represents an abstract base class for managing drag operations within a UI element.
    /// Provides a framework for handling input gestures such as starting, canceling, and completing drag operations.
    /// </summary>
    /// <typeparam name="TElement">The type of <see cref="FrameworkElement"/> that owns the state.</typeparam>
    public abstract class DragState<TElement> : InputElementState<TElement>, IInputHandler
        where TElement : FrameworkElement
    {
        /// <summary>
        /// Gets the gesture used to cancel the drag operation, if defined.
        /// </summary>
        protected InputGesture? CancelGesture { get; }

        /// <summary>
        /// Gets the gesture used to begin the drag operation.
        /// </summary>
        protected InputGesture BeginGesture { get; }

        /// <summary>
        /// Indicates whether the element has a context menu associated with it.
        /// </summary>
        /// <remarks>This property is used to suppress the context menu when a drag operation is performed using the right mouse button.</remarks>
        protected virtual bool HasContextMenu => Element.ContextMenu != null;

        /// <summary>
        /// Determines if the drag operation can begin (see <see cref="OnBegin(InputEventArgs)"/>).
        /// </summary>
        protected virtual bool CanBegin { get; } = true;

        /// <summary>
        /// Determines if the drag operation can be canceled (see <see cref="OnCancel(InputEventArgs)"/>).
        /// </summary>
        protected virtual bool CanCancel { get; } = true;

        /// <summary>
        /// Indicates if the drag gesture is a toggle, meaning the same gesture can be used to both start and stop the operation.
        /// </summary>
        protected virtual bool IsToggle { get; }

        /// <summary>
        /// Gets or sets the UI element used to calculate the mouse position during the drag operation.
        /// </summary>
        protected IInputElement PositionElement { get; set; }

        private bool _canReceiveInput;
        private Point _initialPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragState{TElement}"/> class with a begin gesture.
        /// </summary>
        /// <param name="element">The element associated with this state.</param>
        /// <param name="beginGesture">The gesture used to start the drag operation.</param>
        public DragState(TElement element, InputGesture beginGesture) : base(element)
        {
            BeginGesture = beginGesture;
            PositionElement = element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DragState{TElement}"/> class with begin and cancel gestures.
        /// </summary>
        /// <param name="element">The element associated with this state.</param>
        /// <param name="beginGesture">The gesture used to start the drag operation.</param>
        /// <param name="cancelGesture">The gesture used to cancel the drag operation.</param>
        public DragState(TElement element, InputGesture beginGesture, InputGesture cancelGesture)
            : this(element, beginGesture)
        {
            CancelGesture = cancelGesture;
        }

        void IInputHandler.HandleEvent(InputEventArgs e)
        {
            if (!_canReceiveInput && IsInputEventPressed(e) && CanBegin && BeginGesture.Matches(e.Source, e))
            {
                BeginDrag(e);
                return;
            }

            if (_canReceiveInput && (IsToggle ? IsInputEventPressed(e) : IsInputEventReleased(e)) && BeginGesture.Matches(e.Source, e))
            {
                EndDrag(e);
                return;
            }

            if (_canReceiveInput && (e.RoutedEvent == UIElement.LostMouseCaptureEvent || (CanCancel && CancelGesture?.Matches(e.Source, e) is true && IsInputEventReleased(e))))
            {
                CancelDrag(e);
                return;
            }

            if (_canReceiveInput)
            {
                HandleEvent(e);
            }
        }

        private void CancelDrag(InputEventArgs e)
        {
            _canReceiveInput = false;
            HandleEvent(e);
            OnCancel(e);

            e.Handled = true;
        }

        private void BeginDrag(InputEventArgs e)
        {
            // Avoid stealing mouse capture from other elements
            if (Mouse.Captured == null || Element.IsMouseCaptured)
            {
                _canReceiveInput = true;
                HandleEvent(e); // Handle the event, otherwise CaptureMouse will send a MouseMove event and the current event will be handled out of order
                OnBegin(e);

                e.Handled = true;

                if (e is MouseEventArgs me)
                {
                    _initialPosition = me.GetPosition(PositionElement);
                }

                Element.Focus();
                Element.CaptureMouse();
            }
        }

        private void EndDrag(InputEventArgs e)
        {
            _canReceiveInput = false;
            HandleEvent(e);

            // Suppress the context menu if the mouse moved beyond the defined drag threshold
            if (e is MouseButtonEventArgs mbe && mbe.ChangedButton == MouseButton.Right && HasContextMenu)
            {
                double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
                double dragDistance = (mbe.GetPosition(PositionElement) - _initialPosition).LengthSquared;

                if (dragDistance > dragThreshold)
                {
                    OnEnd(e);
                    e.Handled = true;
                }
                else
                {
                    OnCancel(e);
                }
            }
            else
            {
                OnEnd(e);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Determines if the given input event represents the release of an input gesture.
        /// </summary>
        /// <param name="e">The input event to evaluate.</param>
        /// <returns>True if the event represents the release of a gesture; otherwise, false.</returns>
        protected virtual bool IsInputEventReleased(InputEventArgs e)
        {
            if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Released)
                return true;

            if (e is KeyEventArgs ke && ke.IsUp)
                return true;

            if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Released)
                return true;

            return false;
        }

        /// <summary>
        /// Determines if the given input event represents the press of an input gesture.
        /// </summary>
        /// <param name="e">The input event to evaluate.</param>
        /// <returns>True if the event represents the press of a gesture; otherwise, false.</returns>
        protected virtual bool IsInputEventPressed(InputEventArgs e)
        {
            if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Pressed)
                return true;

            if (e is KeyEventArgs ke && ke.IsDown)
                return true;

            if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Pressed)
                return true;

            return false;
        }

        /// <summary>
        /// Called when the drag operation begins. Override to provide custom behavior.
        /// </summary>
        /// <param name="e">The input event that started the operation.</param>
        protected virtual void OnBegin(InputEventArgs e)
        {
        }

        /// <summary>
        /// Called when the drag operation ends. Override to provide custom behavior.
        /// </summary>
        /// <param name="e">The input event that ended the operation.</param>
        protected virtual void OnEnd(InputEventArgs e)
        {
        }

        /// <summary>
        /// Called when the drag operation is canceled. Override to provide custom behavior.
        /// </summary>
        /// <param name="e">The input event that canceled the operation.</param>
        protected virtual void OnCancel(InputEventArgs e)
        {
        }
    }
}

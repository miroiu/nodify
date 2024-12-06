using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public abstract class ElementOperationState<TElement> : InputElementState<TElement>, IInputHandler
        where TElement : FrameworkElement
    {
        protected InputGesture? CancelGesture { get; }
        protected InputGesture BeginGesture { get; }

        protected virtual bool HasContextMenu => Element.ContextMenu != null;
        protected virtual bool CanBegin { get; } = true;
        protected virtual bool IsToggle { get; }
        protected IInputElement PositionElement { get; set; }

        private bool _canReceiveInput;
        private Point _initialPosition;

        /// <summary>Constructs a new <see cref="EditorState"/>.</summary>
        /// <param name="element">The owner of the state.</param>
        public ElementOperationState(TElement element, InputGesture beginGesture) : base(element)
        {
            BeginGesture = beginGesture;
            PositionElement = element;
        }

        public ElementOperationState(TElement element, InputGesture beginGesture, InputGesture cancelGesture)
            : this(element, beginGesture)
        {
            CancelGesture = cancelGesture;
        }

        void IInputHandler.HandleEvent(InputEventArgs e)
        {
            if (!_canReceiveInput && IsInputEventPressed(e) && BeginGesture.Matches(e.Source, e) && CanBegin)
            {
                BeginOperation(e);
                return;
            }

            if (_canReceiveInput && (IsToggle ? IsInputEventPressed(e) : IsInputEventReleased(e)) && BeginGesture.Matches(e.Source, e))
            {
                EndOperation(e);
                return;
            }

            if (_canReceiveInput && (e.RoutedEvent == UIElement.LostMouseCaptureEvent || (CancelGesture?.Matches(e.Source, e) is true && IsInputEventReleased(e))))
            {
                CancelOperation(e);
                return;
            }

            if (_canReceiveInput)
            {
                HandleEvent(e);
            }
        }

        private void CancelOperation(InputEventArgs e)
        {
            _canReceiveInput = false;
            HandleEvent(e);
            OnCancel(e);

            e.Handled = true;
        }

        private void BeginOperation(InputEventArgs e)
        {
            if (Mouse.Captured == null || Element.IsMouseCaptured)
            {
                _canReceiveInput = true;
                HandleEvent(e); // handle the event, otherwise CaptureMouse will send a MouseMove event and the current event will be handled out of order
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

        private void EndOperation(InputEventArgs e)
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

        private static bool IsInputEventReleased(InputEventArgs e)
        {
            if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Released)
                return true;

            if (e is KeyEventArgs ke && ke.IsUp)
                return true;

            if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Released)
                return true;

            return false;
        }

        private static bool IsInputEventPressed(InputEventArgs e)
        {
            if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Pressed)
                return true;

            if (e is KeyEventArgs ke && ke.IsDown)
                return true;

            if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Pressed)
                return true;

            return false;
        }

        protected virtual void OnBegin(InputEventArgs e)
        {
        }

        protected virtual void OnEnd(InputEventArgs e)
        {
        }

        protected virtual void OnCancel(InputEventArgs e)
        {
        }
    }
}

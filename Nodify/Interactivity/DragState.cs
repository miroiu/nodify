using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Represents an abstract base class for managing drag interactions within a UI element.
    /// Provides a framework for handling input gestures such as starting, canceling, and completing drag interactions.
    /// </summary>
    /// <typeparam name="TElement">The type of <see cref="FrameworkElement"/> that owns the state.</typeparam>
    public abstract class DragState<TElement> : InputElementState<TElement>, IInputHandler
        where TElement : FrameworkElement
    {
        private enum InteractionState
        {
            /// <summary>
            /// Indicates that no drag interaction is active. This is the initial state or the state after a drag interaction has been canceled or completed.
            /// </summary>
            Ready,

            /// <summary>
            /// Indicates that a drag interaction is currently active and handling input events. This state is entered when a drag begins.
            /// </summary>
            InProgress,

            /// <summary>
            /// Indicates that a drag interaction is in the process of ending. This state is used to handle toggled interactions (see <see cref="IsToggle"/>).
            /// </summary>
            Ending
        }

        /// <summary>
        /// Gets the gesture used to cancel the drag interaction, if defined.
        /// </summary>
        protected InputGesture? CancelGesture { get; }

        /// <summary>
        /// Gets the gesture used to begin the drag interaction.
        /// </summary>
        protected InputGesture BeginGesture { get; }

        /// <summary>
        /// Indicates whether the element has a context menu associated with it.
        /// </summary>
        /// <remarks>This property is used to suppress the context menu when a drag interaction is performed using the right mouse button.</remarks>
        protected virtual bool HasContextMenu => Element.ContextMenu != null;

        /// <summary>
        /// Determines if the drag interaction can begin (see <see cref="OnBegin(InputEventArgs)"/>).
        /// </summary>
        protected virtual bool CanBegin { get; } = true;

        /// <summary>
        /// Determines if the drag interaction can be canceled (see <see cref="OnCancel(InputEventArgs)"/>).
        /// </summary>
        protected virtual bool CanCancel { get; } = true;

        /// <summary>
        /// Indicates if the drag gesture is a toggle, meaning the same gesture can be used to both start and stop the interaction.
        /// </summary>
        protected virtual bool IsToggle { get; }

        /// <summary>
        /// Gets or sets the UI element used to calculate the mouse position during the drag interaction.
        /// </summary>
        protected IInputElement PositionElement { get; set; }

        private InteractionState _interactionState;
        private Point _initialPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="DragState{TElement}"/> class with a begin gesture.
        /// </summary>
        /// <param name="element">The element associated with this state.</param>
        /// <param name="beginGesture">The gesture used to start the drag interaction.</param>
        public DragState(TElement element, InputGesture beginGesture) : base(element)
        {
            BeginGesture = beginGesture;
            PositionElement = element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DragState{TElement}"/> class with begin and cancel gestures.
        /// </summary>
        /// <param name="element">The element associated with this state.</param>
        /// <param name="beginGesture">The gesture used to start the drag interaction.</param>
        /// <param name="cancelGesture">The gesture used to cancel the drag interaction.</param>
        public DragState(TElement element, InputGesture beginGesture, InputGesture cancelGesture)
            : this(element, beginGesture)
        {
            CancelGesture = cancelGesture;
        }

        void IInputHandler.HandleEvent(InputEventArgs e)
        {
            if (_interactionState == InteractionState.Ready && TryBeginDragging(e))
            {
                return;
            }

            if (_interactionState == InteractionState.Ending && TryEndDragging(e))
            {
                return;
            }

            if (_interactionState == InteractionState.InProgress)
            {
                if (TryEndDragging(e) || TryCancelDragging(e) || TrySuppressContextMenu(e))
                {
                    return;
                }
            }

            TryHandleEvent(e);
        }

        #region Interaction logic

        // Begin the interaction on gesture press
        private bool TryBeginDragging(InputEventArgs e)
        {
            if (IsInputEventPressed(e) && CanBegin && BeginGesture.Matches(e.Source, e))
            {
                BeginDrag(e);
                return true;
            }

            return false;
        }

        private bool TryEndDragging(InputEventArgs e)
        {
            if (IsInputCaptureLost(e))
            {
                EndDrag(e);
                return true;
            }

            if (IsToggle && _interactionState == InteractionState.InProgress)
            {
                return TryDeferToggleInteractionEnd(e);
            }

            return TryEndInteraction(e);
        }

        // Delay ending toggle interaction until the gesture is released
        private bool TryDeferToggleInteractionEnd(InputEventArgs e)
        {
            if (IsInputEventPressed(e) && BeginGesture.Matches(e.Source, e))
            {
                _interactionState = InteractionState.Ending;
                HandleEvent(e);
                return true;
            }

            return false;
        }

        // End the interaction on gesture release
        private bool TryEndInteraction(InputEventArgs e)
        {
            if (IsInputEventReleased(e) && BeginGesture.Matches(e.Source, e))
            {
                EndDrag(e);
                return true;
            }

            return false;
        }

        // Cancel the interaction
        private bool TryCancelDragging(InputEventArgs e)
        {
            if (CanCancel && IsInputEventReleased(e) && CancelGesture?.Matches(e.Source, e) is true)
            {
                CancelDrag(e);
                return true;
            }

            return false;
        }

        // Suppress the context menu if a toggle interaction is in progress
        private bool TrySuppressContextMenu(InputEventArgs e)
        {
            if (IsToggle && e is MouseButtonEventArgs mbe && mbe.ChangedButton == MouseButton.Right)
            {
                e.Handled = true;
                HandleEvent(e);
                return true;
            }

            return false;
        }

        private void TryHandleEvent(InputEventArgs e)
        {
            if (_interactionState == InteractionState.InProgress || _interactionState == InteractionState.Ending)
            {
                HandleEvent(e);
            }
        }

        internal void BeginDrag(InputEventArgs e)
        {
            // Avoid stealing mouse capture from other elements
            if (CanCaptureInput(e))
            {
                RequiresInputCapture = IsToggle;

                _interactionState = InteractionState.InProgress;
                _initialPosition = GetInitialPosition(e);

                HandleEvent(e); // Handle the event, otherwise CaptureMouse will send a MouseMove event and the current event will be handled out of order
                OnBegin(e);

                e.Handled = true;

                Element.Focus();
                CaptureInput(e);
            }
        }

        private void EndDrag(InputEventArgs e)
        {
            _interactionState = InteractionState.Ready;
            HandleEvent(e);

            // Suppress the context menu if the mouse moved beyond the defined drag threshold
            if (HasContextMenu && e is MouseButtonEventArgs mbe && mbe.ChangedButton == MouseButton.Right)
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

            RequiresInputCapture = false;
        }

        private void CancelDrag(InputEventArgs e)
        {
            _interactionState = InteractionState.Ready;
            HandleEvent(e);
            OnCancel(e);

            e.Handled = true;
            RequiresInputCapture = false;
        }

        #endregion

        /// <summary>
        /// Retrieves the initial position of the input event relative to the <see cref="PositionElement"/>.
        /// </summary>
        /// <param name="e">The <see cref="InputEventArgs"/> representing the input event.</param>
        /// <remarks>
        /// This position is used to calculate the drag distance, to determine whether 
        /// the context menu can appear or if the action is considered a drag operation. The behavior is influenced 
        /// by the <see cref="NodifyEditor.MouseActionSuppressionThreshold"/>.
        /// </remarks>
        protected virtual Point GetInitialPosition(InputEventArgs e)
        {
            if (e is MouseEventArgs me)
            {
                return me.GetPosition(PositionElement);
            }

            return default;
        }

        /// <summary>
        /// Determines whether input capture can be acquired for the <see cref="InputElementState{TElement}.Element" />.
        /// </summary>
        /// <param name="e">The <see cref="InputEventArgs"/> representing the input event.</param>
        /// <remarks>Must return true if the input is already captured by the current element.</remarks>
        protected virtual bool CanCaptureInput(InputEventArgs e)
            => Mouse.Captured == null || Element.IsMouseCaptured;

        /// <summary>
        /// Captures input for the element.
        /// </summary>
        /// <param name="e">The <see cref="InputEventArgs"/> representing the input event.</param>
        protected virtual void CaptureInput(InputEventArgs e)
            => Element.CaptureMouse();

        /// <summary>
        /// Determines whether input capture has been lost.
        /// </summary>
        /// <param name="e">The <see cref="InputEventArgs"/> representing the input event.</param>
        protected virtual bool IsInputCaptureLost(InputEventArgs e)
            => e.RoutedEvent == UIElement.LostMouseCaptureEvent;

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
        /// Called when the drag interaction begins. Override to provide custom behavior.
        /// </summary>
        /// <param name="e">The input event that started the interaction.</param>
        protected virtual void OnBegin(InputEventArgs e)
        {
        }

        /// <summary>
        /// Called when the drag interaction ends. Override to provide custom behavior.
        /// </summary>
        /// <param name="e">The input event that ended the interaction.</param>
        protected virtual void OnEnd(InputEventArgs e)
        {
        }

        /// <summary>
        /// Called when the drag interaction is canceled. Override to provide custom behavior.
        /// </summary>
        /// <param name="e">The input event that canceled the interaction.</param>
        protected virtual void OnCancel(InputEventArgs e)
        {
        }
    }
}

using System;
using System.Windows.Input;
using System.Windows;

namespace Nodify.Interactivity
{
    internal class StatefulFocusNavigator<TElement>
        where TElement : UIElement, IKeyboardFocusTarget<TElement>
    {
        public delegate bool FindNextFocusTargetDelegate(TraversalRequest request, out TElement? elementToFocus);

        // TODO: When do we clear these?
        private readonly WeakReference<TElement?> _previousFocusedContainer = new WeakReference<TElement?>(null);
        private readonly WeakReference<TElement?> _lastFocusedContainer = new WeakReference<TElement?>(null);
        private FocusNavigationDirection? _previousFocusNavigationDirection;

        private readonly Action<IKeyboardFocusTarget<TElement>> _onFocus;

        public StatefulFocusNavigator(Action<IKeyboardFocusTarget<TElement>> onFocus)
        {
            _onFocus = onFocus;
        }

        public bool TryMoveFocus(TraversalRequest request, FindNextFocusTargetDelegate findNext)
        {
            var currentTarget = Keyboard.FocusedElement as TElement;

            // If the request is in the opposite direction of the last focus navigation, try to restore the previous focused container
            if (_previousFocusedContainer.TryGetTarget(out var prevTarget)
                && _previousFocusNavigationDirection.HasValue
                && request.FocusNavigationDirection.IsOppositeOf(_previousFocusNavigationDirection.Value)
                && prevTarget!.Focus())
            {
                _previousFocusNavigationDirection = request.FocusNavigationDirection;
                _previousFocusedContainer.SetTarget(currentTarget);
                _lastFocusedContainer.SetTarget(prevTarget);

                _onFocus(prevTarget);
                return true;
            }
            else if (findNext(request, out var nextTarget) && nextTarget!.Element.Focus())
            {
                _previousFocusNavigationDirection = request.FocusNavigationDirection;
                _previousFocusedContainer.SetTarget(currentTarget);
                _lastFocusedContainer.SetTarget(nextTarget);

                _onFocus(nextTarget);
                return true;
            }

            return false;
        }

        public bool TryRestoreFocus()
        {
            if (_lastFocusedContainer.TryGetTarget(out var lastTarget) && lastTarget!.Focus())
            {
                _onFocus.Invoke(lastTarget);
            }

            return false;
        }
    }
}

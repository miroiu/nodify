using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    internal readonly struct LinearFocusNavigator<TElement>
        where TElement : UIElement, IKeyboardFocusTarget<TElement>
    {
        private enum LinearNavigationDirection
        {
            First,
            Last,
            Forward,
            Backward
        }

        private readonly IEnumerable<IKeyboardFocusTarget<TElement>> _availableTargets;

        public LinearFocusNavigator(IEnumerable<IKeyboardFocusTarget<TElement>> availableTargets)
        {
            _availableTargets = availableTargets;
        }

        public readonly IKeyboardFocusTarget<TElement>? FindNextFocusTarget(IKeyboardFocusTarget<TElement> currentContainer, TraversalRequest request)
        {
            var direction = IsBackward(request.FocusNavigationDirection) ? LinearNavigationDirection.Backward
                : IsForward(request.FocusNavigationDirection) ? LinearNavigationDirection.Forward
                : request.FocusNavigationDirection == FocusNavigationDirection.First ? LinearNavigationDirection.First : LinearNavigationDirection.Last;

            var availableTargets = _availableTargets as List<IKeyboardFocusTarget<TElement>> ?? _availableTargets.ToList();
            int currentIndex = availableTargets.IndexOf(currentContainer);

            IKeyboardFocusTarget<TElement>? candidate = direction switch
            {
                LinearNavigationDirection.Forward when currentIndex >= 0 && currentIndex + 1 < availableTargets.Count => availableTargets[currentIndex + 1],
                LinearNavigationDirection.Backward when currentIndex > 0 => availableTargets[currentIndex - 1],
                LinearNavigationDirection.First when availableTargets.Count > 0 => availableTargets[0],
                LinearNavigationDirection.Last when availableTargets.Count > 0 => availableTargets[availableTargets.Count - 1],
                _ => null
            };

            // Wrap focus if no candidates found in the current direction  
            if (candidate is null)
            {
                candidate = direction switch
                {
                    LinearNavigationDirection.Forward when availableTargets.Count > 0 => availableTargets[0],
                    LinearNavigationDirection.Backward when availableTargets.Count > 0 => availableTargets[availableTargets.Count - 1],
                    _ => null
                };

                request.Wrapped = candidate != null;
            }

            return candidate;
        }

        private static bool IsForward(FocusNavigationDirection dir)
        {
            return dir == FocusNavigationDirection.Right || dir == FocusNavigationDirection.Up || dir == FocusNavigationDirection.Next;
        }

        private static bool IsBackward(FocusNavigationDirection dir)
        {
            return dir == FocusNavigationDirection.Left || dir == FocusNavigationDirection.Down || dir == FocusNavigationDirection.Previous;
        }
    }
}

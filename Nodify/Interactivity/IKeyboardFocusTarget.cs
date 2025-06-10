using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public interface IKeyboardFocusTarget<TElement>
        where TElement : UIElement
    {
        Rect Bounds { get; }
        TElement Element { get; }
    }

    internal readonly struct DirectionalFocusNavigator<TElement>
        where TElement : UIElement, IKeyboardFocusTarget<TElement>
    {
        private readonly IEnumerable<IKeyboardFocusTarget<TElement>> _availableTargets;

        public DirectionalFocusNavigator(IEnumerable<IKeyboardFocusTarget<TElement>> availableTargets)
        {
            _availableTargets = availableTargets;
        }

        public readonly IKeyboardFocusTarget<TElement>? FindNextFocusTarget(IKeyboardFocusTarget<TElement> currentContainer, TraversalRequest request)
        {
            var currentContainerBounds = currentContainer.Bounds;

            IEnumerable<IKeyboardFocusTarget<TElement>> candidates = request.FocusNavigationDirection switch
            {
                FocusNavigationDirection.Left => _availableTargets.Where(c => c.Bounds.Right <= currentContainerBounds.Left),
                FocusNavigationDirection.Right => _availableTargets.Where(c => c.Bounds.Left >= currentContainerBounds.Right),
                FocusNavigationDirection.Up => _availableTargets.Where(c => c.Bounds.Bottom <= currentContainerBounds.Top),
                FocusNavigationDirection.Down => _availableTargets.Where(c => c.Bounds.Top >= currentContainerBounds.Bottom),
                _ => Enumerable.Empty<IKeyboardFocusTarget<TElement>>()
            };

            // Wrap focus if no candidates found in the current direction  
            if (!candidates.Any())
            {
                candidates = request.FocusNavigationDirection switch
                {
                    FocusNavigationDirection.Left => _availableTargets.OrderByDescending(c => c.Bounds.Left).Take(1),
                    FocusNavigationDirection.Right => _availableTargets.OrderBy(c => c.Bounds.Left).Take(1),
                    FocusNavigationDirection.Up => _availableTargets.OrderByDescending(c => c.Bounds.Top).Take(1),
                    FocusNavigationDirection.Down => _availableTargets.OrderBy(c => c.Bounds.Top).Take(1),
                    _ => Enumerable.Empty<IKeyboardFocusTarget<TElement>>()
                };

                request.Wrapped = true;
            }

            IKeyboardFocusTarget<TElement>? best = null;
            double minDistanceSquared = double.MaxValue;

            foreach (var candidate in candidates)
            {
                double distanceSquared = (candidate.Bounds.TopLeft - currentContainerBounds.TopLeft).LengthSquared;
                if (distanceSquared < minDistanceSquared)
                {
                    minDistanceSquared = distanceSquared;
                    best = candidate;
                }
            }

            return best;
        }
    }

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
            var currentContainerBounds = currentContainer.Bounds;

            var direction = IsBackward(request.FocusNavigationDirection) ? LinearNavigationDirection.Backward
                : IsForward(request.FocusNavigationDirection) ? LinearNavigationDirection.Forward
                : request.FocusNavigationDirection == FocusNavigationDirection.First ? LinearNavigationDirection.First : LinearNavigationDirection.Last;

            var availableTargets = _availableTargets as List<IKeyboardFocusTarget<TElement>> ?? _availableTargets.ToList();
            var currentIndex = availableTargets.IndexOf(currentContainer);

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

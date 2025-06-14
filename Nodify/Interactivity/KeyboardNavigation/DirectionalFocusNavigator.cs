using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
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
                FocusNavigationDirection.Left => _availableTargets.Where(c => c.Bounds.Left < currentContainerBounds.Left),
                FocusNavigationDirection.Right => _availableTargets.Where(c => c.Bounds.Left > currentContainerBounds.Left),
                FocusNavigationDirection.Up => _availableTargets.Where(c => c.Bounds.Top < currentContainerBounds.Top),
                FocusNavigationDirection.Down => _availableTargets.Where(c => c.Bounds.Top > currentContainerBounds.Top),
                FocusNavigationDirection.Previous => FindCandidatesLinearly(currentContainer, request),
                FocusNavigationDirection.Next => FindCandidatesLinearly(currentContainer, request),
                FocusNavigationDirection.First => FindCandidatesLinearly(currentContainer, request),
                FocusNavigationDirection.Last => FindCandidatesLinearly(currentContainer, request),
                _ => Array.Empty<IKeyboardFocusTarget<TElement>>()
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
                    _ => Array.Empty<IKeyboardFocusTarget<TElement>>()
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

        private IKeyboardFocusTarget<TElement>[] FindCandidatesLinearly(IKeyboardFocusTarget<TElement> currentContainer, TraversalRequest request)
        {
            var nextTarget = new LinearFocusNavigator<TElement>(_availableTargets).FindNextFocusTarget(currentContainer, request);
            return nextTarget is null ? Array.Empty<IKeyboardFocusTarget<TElement>>() : new[] { nextTarget };
        }
    }
}

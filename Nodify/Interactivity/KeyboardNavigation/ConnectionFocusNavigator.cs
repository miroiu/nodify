using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    internal readonly struct ConnectionFocusNavigator<TElement>
        where TElement : ConnectionContainer, IKeyboardFocusTarget<TElement>
    {
        private readonly IEnumerable<IKeyboardFocusTarget<TElement>> _availableTargets;

        public ConnectionFocusNavigator(IEnumerable<IKeyboardFocusTarget<TElement>> availableTargets)
        {
            _availableTargets = availableTargets;
        }

        public readonly IKeyboardFocusTarget<TElement>? FindNextFocusTarget(IKeyboardFocusTarget<TElement> currentContainer, TraversalRequest request)
        {
            if (currentContainer is ConnectionContainer container
                && container.Connection is BaseConnection baseConnection
                && CanNavigateDirectional(baseConnection, request.FocusNavigationDirection))
            {
                var directionalFocusNavigator = new DirectionalFocusNavigator<TElement>(_availableTargets);
                return directionalFocusNavigator.FindNextFocusTarget(currentContainer, request);
            }

            var linearFocusNavigator = new LinearFocusNavigator<TElement>(_availableTargets);
            return linearFocusNavigator.FindNextFocusTarget(currentContainer, request);
        }

        private static bool CanNavigateDirectional(BaseConnection baseConnection, FocusNavigationDirection dir)
        {
            return (baseConnection.SourceOrientation == Orientation.Horizontal && IsVertical(dir))
                || (baseConnection.SourceOrientation == Orientation.Vertical && IsHorizontal(dir));
        }

        private static bool IsVertical(FocusNavigationDirection dir)
        {
            return dir == FocusNavigationDirection.Up || dir == FocusNavigationDirection.Down;
        }

        private static bool IsHorizontal(FocusNavigationDirection dir)
        {
            return dir == FocusNavigationDirection.Left || dir == FocusNavigationDirection.Right;
        }
    }
}

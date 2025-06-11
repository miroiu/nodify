using Nodify.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    internal static class EditorGesturesExtensions
    {
        public static SelectionType GetSelectionType(this EditorGestures.SelectionGestures gestures, InputEventArgs e)
        {
            if (gestures.Append.Matches(e.Source, e))
            {
                return SelectionType.Append;
            }

            if (gestures.Invert.Matches(e.Source, e))
            {
                return SelectionType.Invert;
            }

            if (gestures.Remove.Matches(e.Source, e))
            {
                return SelectionType.Remove;
            }

            return SelectionType.Replace;
        }

        public static bool TryGetFocusDirection(this EditorGestures.DirectionalNavigationGestures gestures, InputEventArgs e, out FocusNavigationDirection direction)
        {
            direction = default;

            if (gestures.Left.Matches(e.Source, e))
            {
                direction = FocusNavigationDirection.Left;
                return true;
            }
            if (gestures.Right.Matches(e.Source, e))
            {
                direction = FocusNavigationDirection.Right;
                return true;
            }
            if (gestures.Up.Matches(e.Source, e))
            {
                direction = FocusNavigationDirection.Up;
                return true;
            }
            if (gestures.Down.Matches(e.Source, e))
            {
                direction = FocusNavigationDirection.Down;
                return true;
            }

            return false;
        }

        public static bool TryGetNavigationDirection(this EditorGestures.DirectionalNavigationGestures gestures, InputEventArgs e, out Vector direction)
        {
            double y = gestures.Up.Matches(e.Source, e) ? 1 : gestures.Down.Matches(e.Source, e) ? -1 : 0;
            double x = gestures.Left.Matches(e.Source, e) ? -1 : gestures.Right.Matches(e.Source, e) ? 1 : 0;

            direction = new Vector(x, y);

            return x != 0 || y != 0;
        }

        public static bool IsOppositeOf(this FocusNavigationDirection direction, FocusNavigationDirection other)
        {
            return (direction == FocusNavigationDirection.Left && other == FocusNavigationDirection.Right)
                || (direction == FocusNavigationDirection.Right && other == FocusNavigationDirection.Left)
                || (direction == FocusNavigationDirection.Up && other == FocusNavigationDirection.Down)
                || (direction == FocusNavigationDirection.Down && other == FocusNavigationDirection.Up)
                || (direction == FocusNavigationDirection.Next && other == FocusNavigationDirection.Previous)
                || (direction == FocusNavigationDirection.Previous && other == FocusNavigationDirection.Next)
                || (direction == FocusNavigationDirection.First && other == FocusNavigationDirection.Last)
                || (direction == FocusNavigationDirection.Last && other == FocusNavigationDirection.First);
        }
    }
}

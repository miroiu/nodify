using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Nodify
{
    internal static class DependencyObjectExtensions
    {
        public static T? GetParentOfType<T>(this DependencyObject current)
            where T : DependencyObject
        {
            while ((current = VisualTreeHelper.GetParent(current)) != null)
            {
                if (current is T match)
                {
                    return match;
                }
            }

            return null;
        }

        public static DependencyObject? GetParent(this DependencyObject current, Func<DependencyObject, bool> condition)
        {
            while ((current = VisualTreeHelper.GetParent(current)) != null)
            {
                if (condition(current))
                {
                    return current;
                }
            }

            return null;
        }

        public static T? GetChildOfType<T>(this DependencyObject? depObj) where T : DependencyObject
        {
            if (depObj == null)
            {
                return default;
            }

            var count = VisualTreeHelper.GetChildrenCount(depObj);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child is T result)
                {
                    return result;
                }

                if (GetChildOfType<T>(child) is T r)
                {
                    return r;
                }
            }

            return default;
        }

        public static T? GetElementAtPosition<T>(this UIElement container, Point position)
            where T : UIElement
        {
            T? result = default;
            VisualTreeHelper.HitTest(container, depObj =>
            {
                if (depObj is UIElement elem && elem.IsHitTestVisible)
                {
                    if (elem is T r)
                    {
                        result = r;
                        return HitTestFilterBehavior.Stop;
                    }

                    return HitTestFilterBehavior.Continue;
                }

                return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
            }, hitResult =>
            {
                if (hitResult.VisualHit is T r)
                {
                    result = r;
                    return HitTestResultBehavior.Stop;
                }
                return HitTestResultBehavior.Continue;
            }, new PointHitTestParameters(position));

            return result;
        }

        public static List<FrameworkElement> GetIntersectingElements(this UIElement container, Geometry geometry, IReadOnlyCollection<Type> supportedTypes)
        {
            var result = new List<FrameworkElement>();
            VisualTreeHelper.HitTest(container, depObj =>
            {
                if (depObj is FrameworkElement elem && elem.IsHitTestVisible)
                {
                    if (supportedTypes.Contains(elem.GetType()))
                    {
                        return HitTestFilterBehavior.ContinueSkipChildren;
                    }

                    return HitTestFilterBehavior.ContinueSkipSelf;
                }

                return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
            }, hitResult =>
            {
                result.Add((FrameworkElement)hitResult.VisualHit);
                return HitTestResultBehavior.Continue;
            }, new GeometryHitTestParameters(geometry));

            return result;
        }

        public static IEnumerable<T> GetIntersectingElements<T>(this UIElement container, Rect area, Func<T, Rect> getBounds)
            where T : Visual
        {
            var stack = new Stack<DependencyObject>();
            stack.Push(container);

            while (stack.Count > 0)
            {
                DependencyObject current = stack.Pop();
                int childrenCount = VisualTreeHelper.GetChildrenCount(current);

                for (int i = 0; i < childrenCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);

                    if (child is T tChild)
                    {
                        var bounds = getBounds(tChild);
                        if (bounds.IntersectsWith(area))
                        {
                            yield return tChild;
                            continue;
                        }
                    }

                    stack.Push(child);
                }
            }
        }

        #region Animation

        public static void StartAnimation(this UIElement animatableElement, DependencyProperty dependencyProperty, Point toValue, double animationDurationSeconds, EventHandler? completedEvent = null)
        {
            var fromValue = (Point)animatableElement.GetValue(dependencyProperty);

            PointAnimation animation = new PointAnimation
            {
                From = fromValue,
                To = toValue,
                Duration = TimeSpan.FromSeconds(animationDurationSeconds)
            };

            animation.Completed += delegate (object? sender, EventArgs e)
            {
                animatableElement.SetValue(dependencyProperty, animatableElement.GetValue(dependencyProperty));
                CancelAnimation(animatableElement, dependencyProperty);

                completedEvent?.Invoke(sender, e);
            };

            animation.Freeze();

            animatableElement.BeginAnimation(dependencyProperty, animation);
        }

        public static void StartAnimation(this UIElement animatableElement, DependencyProperty dependencyProperty, double toValue, double animationDurationSeconds, EventHandler? completedEvent = null)
        {
            var fromValue = (double)animatableElement.GetValue(dependencyProperty);

            DoubleAnimation animation = new DoubleAnimation
            {
                From = fromValue,
                To = toValue,
                Duration = TimeSpan.FromSeconds(animationDurationSeconds)
            };

            animation.Completed += delegate (object? sender, EventArgs e)
            {
                animatableElement.SetValue(dependencyProperty, animatableElement.GetValue(dependencyProperty));
                CancelAnimation(animatableElement, dependencyProperty);

                completedEvent?.Invoke(sender, e);
            };

            animation.Freeze();

            animatableElement.BeginAnimation(dependencyProperty, animation);
        }

        public static void StartLoopingAnimation(this UIElement animatableElement, DependencyProperty dependencyProperty, double toValue, double durationInSeconds)
        {
            var fromValue = (double)animatableElement.GetValue(dependencyProperty);

            var animation = new DoubleAnimation
            {
                From = fromValue,
                To = toValue,
                Duration = TimeSpan.FromSeconds(durationInSeconds)
            };

            animation.RepeatBehavior = RepeatBehavior.Forever;

            animation.Freeze();
            animatableElement.BeginAnimation(dependencyProperty, animation);
        }

        public static void CancelAnimation(this UIElement animatableElement, DependencyProperty dependencyProperty)
            => animatableElement.BeginAnimation(dependencyProperty, null);

        #endregion
    }
}

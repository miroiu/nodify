using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Nodify
{
    internal static class DependencyObjectExtensions
    {
        public static T? GetParentOfType<T>(this DependencyObject child)
            where T : DependencyObject
        {
            DependencyObject? current = child;

            do
            {
                current = VisualTreeHelper.GetParent(current);
                if (current == default)
                {
                    return default;
                }

            } while (!(current is T));

            return (T)current;
        }

        public static T? GetElementUnderMouse<T>(this UIElement container, Point relativePosition)
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
            }, new PointHitTestParameters(relativePosition));

            return result;
        }

        #region Animation

        public static async System.Threading.Tasks.Task StartAnimation<T>(this Control animatableElement, StyledProperty<T> dependencyProperty, T toValue, double animationDurationSeconds, EventHandler? completedEvent = null)
        {
            var fromValue = (T)animatableElement.GetValue(dependencyProperty);

            var keyframe1 = new KeyFrame()
            {
                Setters = { new Setter(dependencyProperty, fromValue), }, KeyTime = TimeSpan.FromSeconds(0)
            };

            var keyframe2 = new KeyFrame()
            {
                Setters = { new Setter(dependencyProperty, toValue), }, KeyTime = TimeSpan.FromSeconds(animationDurationSeconds)
            };

            var animation = new Avalonia.Animation.Animation()
            {
                Duration = TimeSpan.FromSeconds(animationDurationSeconds), Children = { keyframe1, keyframe2 },
            };

            await animation.RunAsync(animatableElement);
            
            //animatableElement.SetValue(dependencyProperty, animatableElement.GetValue(dependencyProperty));
            //CancelAnimation(animatableElement, dependencyProperty);

            completedEvent?.Invoke(animatableElement, EventArgs.Empty);
        }

        public static void StartLoopingAnimation<T>(this UIElement animatableElement, StyledProperty<T> dependencyProperty, T toValue, double durationInSeconds, CancellationToken token)
        {
            var fromValue = (T)animatableElement.GetValue(dependencyProperty);

            var keyframe1 = new KeyFrame()
            {
                Setters = { new Setter(dependencyProperty, fromValue), }, KeyTime = TimeSpan.FromSeconds(0)
            };

            var keyframe2 = new KeyFrame()
            {
                Setters = { new Setter(dependencyProperty, toValue), }, KeyTime = TimeSpan.FromSeconds(durationInSeconds)
            };

            var animation = new Avalonia.Animation.Animation()
            {
                Duration = TimeSpan.FromSeconds(durationInSeconds), Children = { keyframe1, keyframe2 },
                IterationCount = IterationCount.Infinite
            };

            animation.RunAsync(animatableElement, token);
        }

        public static void CancelAnimation<T>(this UIElement animatableElement, StyledProperty<T> dependencyProperty, CancellationTokenSource? tokenSource)
            => tokenSource?.Cancel();

        #endregion
    }
}

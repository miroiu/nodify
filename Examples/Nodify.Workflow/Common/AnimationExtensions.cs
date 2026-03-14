using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Nodify.Workflow.Common;

public static class AnimationDefaults
{
    public static readonly IEasingFunction DefaultEase = new CubicEase { EasingMode = EasingMode.EaseOut };
    public static readonly TimeSpan DefaultDuration = TimeSpan.FromMilliseconds(200);
}

public sealed class AnimationOptions<T> where T : struct
{
    public T? From { get; set; }
    public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(200);
    public IEasingFunction? Easing { get; set; } = AnimationDefaults.DefaultEase;
}

public static class AnimationExtensions
{
    #region Animation Core

    private readonly struct AnimationKey(DependencyObject target, DependencyProperty property) : IEquatable<AnimationKey>
    {
        public readonly DependencyObject Target = target;
        public readonly DependencyProperty Property = property;

        public bool Equals(AnimationKey other)
            => ReferenceEquals(Target, other.Target) && Property == other.Property;

        public override bool Equals(object? obj)
            => obj is AnimationKey key && Equals(key);

        public override int GetHashCode()
            => HashCode.Combine(Target, Property);
    }

    private sealed class RunningAnimation(DependencyObject target, DependencyProperty property)
    {
        private static int _nextId;

        private readonly TaskCompletionSource _tcs = new();

        public int Id { get; } = Interlocked.Increment(ref _nextId);

        public void Cancel()
        {
            var key = new AnimationKey(target, property);

            if (_runningAnimations.TryGetValue(key, out var currentAnimation) && currentAnimation.Id == Id)
            {
                _runningAnimations.TryRemove(key, out _);
                target.BeginAnimation(property, null);
                End();
            }
        }

        public void End() => _tcs.TrySetResult();

        public Task CompletionTask => _tcs.Task;
    }

    private sealed class AnimationCompletionHandler(AnimationKey key, RunningAnimation animation)
    {
        public void OnCompleted(object? sender, EventArgs e)
        {
            if (!_runningAnimations.TryGetValue(key, out var runningAnim) || runningAnim.Id != animation.Id)
            {
                return;
            }

            _runningAnimations.TryRemove(key, out _);
            runningAnim.End();
        }
    }

    private static readonly ConcurrentDictionary<AnimationKey, RunningAnimation> _runningAnimations = new();

    public static Task Animate(this DependencyObject target, DependencyProperty property, AnimationTimeline timeline)
    {
        var key = new AnimationKey(target, property);

        var newAnimation = new RunningAnimation(target, property);

        if (_runningAnimations.TryRemove(key, out var previousAnimation))
        {
            previousAnimation.Cancel();
        }

        _runningAnimations[key] = newAnimation;

        var handler = new AnimationCompletionHandler(key, newAnimation);

        timeline.Completed += handler.OnCompleted;

        target.BeginAnimation(property, timeline);

        return newAnimation.CompletionTask;
    }

    private static void BeginAnimation(this DependencyObject target, DependencyProperty property, AnimationTimeline? animation)
    {
        switch (target)
        {
            case UIElement ui:
                ui.BeginAnimation(property, animation);
                break;

            case Animatable anim:
                anim.BeginAnimation(property, animation);
                break;

            case ContentElement content:
                content.BeginAnimation(property, animation);
                break;

            default:
                throw new InvalidOperationException($"{target.GetType()} does not support animation.");
        }
    }

    #endregion

    #region Animation Types

    public static Task Animate(this DependencyObject target, DependencyProperty property, double to, in AnimationOptions<double>? options = null)
    {
        var anim = new DoubleAnimation
        {
            From = options?.From ?? (double)target.GetValue(property),
            To = to,
            Duration = options?.Duration ?? AnimationDefaults.DefaultDuration,
            EasingFunction = options?.Easing ?? AnimationDefaults.DefaultEase
        };

        return target.Animate(property, anim);
    }

    public static Task Animate(this DependencyObject target, DependencyProperty property, Color to, in AnimationOptions<Color>? options = null)
    {
        var anim = new ColorAnimation
        {
            From = options?.From ?? (Color)target.GetValue(property),
            To = to,
            Duration = options?.Duration ?? AnimationDefaults.DefaultDuration,
            EasingFunction = options?.Easing ?? AnimationDefaults.DefaultEase
        };

        return target.Animate(property, anim);
    }

    public static Task Animate(this DependencyObject target, DependencyProperty property, Thickness to, AnimationOptions<Thickness>? options = null)
    {
        var anim = new ThicknessAnimation
        {
            From = options?.From ?? (Thickness)target.GetValue(property),
            To = to,
            Duration = options?.Duration ?? AnimationDefaults.DefaultDuration,
            EasingFunction = options?.Easing ?? AnimationDefaults.DefaultEase
        };

        return target.Animate(property, anim);
    }

    public static Task Animate(this DependencyObject target, DependencyProperty property, Point to, AnimationOptions<Point>? options = null)
    {
        var anim = new PointAnimation
        {
            From = options?.From ?? (Point)target.GetValue(property),
            To = to,
            Duration = options?.Duration ?? AnimationDefaults.DefaultDuration,
            EasingFunction = options?.Easing ?? AnimationDefaults.DefaultEase
        };

        return target.Animate(property, anim);
    }

    public static Task Animate(this DependencyObject target, DependencyProperty property, GridLength to, AnimationOptions<GridLength>? options = null)
    {
        var from = (GridLength)target.GetValue(property);

        var wrapperOptions = options != null ? new AnimationOptions<double> { Duration = options.Duration, From = options.From?.Value, Easing = options.Easing } : null;

        var wrapper = new AnimatableDouble(from.Value, newValue => target.SetCurrentValue(property, new GridLength(newValue)));
        return wrapper.Animate(AnimatableDouble.ValueProperty, to.Value, wrapperOptions);
    }

    private class AnimatableDouble : Animatable
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(AnimatableDouble),
                new PropertyMetadata(0.0d, OnValueChanged));

        private readonly Action<double> _update;

        public AnimatableDouble(double from, Action<double> update)
        {
            _update = update;
            SetCurrentValue(ValueProperty, from);
        }

        private AnimatableDouble()
        {
            _update = (_) => { };
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatableDouble)d)._update.Invoke((double)e.NewValue);
        }

        protected override Freezable CreateInstanceCore() => new AnimatableDouble();
    }

    #endregion

    #region High Level Animations

    public static Task FadeIn(this UIElement el, AnimationOptions<double>? options = null)
    {
        return el.Animate(UIElement.OpacityProperty, 1d, options);
    }

    public static Task FadeOut(this UIElement el, AnimationOptions<double>? options = null)
    {
        return el.Animate(UIElement.OpacityProperty, 0d, options);
    }

    private static T GetOrCreateTransform<T>(FrameworkElement el) where T : Transform, new()
    {
        if (el.RenderTransform is TransformGroup group)
        {
            if (group.Children.OfType<T>().FirstOrDefault() is { } existing)
            {
                return existing;
            }

            var t = new T();
            group.Children.Add(t);
            return t;
        }

        var newGroup = new TransformGroup();

        if (el.RenderTransform is Transform current && current != Transform.Identity)
        {
            newGroup.Children.Add(current);
        }

        var newTransform = new T();
        newGroup.Children.Add(newTransform);
        el.RenderTransform = newGroup;

        return newTransform;
    }

    public static Task TranslateX(this FrameworkElement el, double to, AnimationOptions<double>? options = null)
    {
        var t = GetOrCreateTransform<TranslateTransform>(el);
        return t.Animate(TranslateTransform.XProperty, to, options);
    }

    public static Task TranslateY(this FrameworkElement el, double to, AnimationOptions<double>? options = null)
    {
        var t = GetOrCreateTransform<TranslateTransform>(el);
        return t.Animate(TranslateTransform.YProperty, to, options);
    }

    public static Task ScaleX(this FrameworkElement el, double to, AnimationOptions<double>? options = null)
    {
        var s = GetOrCreateTransform<ScaleTransform>(el);
        return s.Animate(ScaleTransform.ScaleXProperty, to, options);
    }

    public static Task ScaleY(this FrameworkElement el, double to, AnimationOptions<double>? options = null)
    {
        var s = GetOrCreateTransform<ScaleTransform>(el);
        return s.Animate(ScaleTransform.ScaleYProperty, to, options);
    }

    #endregion
}

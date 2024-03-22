using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.VisualTree;

namespace Nodify.Compatibility
{
    internal static class PanelUtilities
    {
        private static void AffectsParentArrangeInvalidate<TPanel>(AvaloniaPropertyChangedEventArgs e)
            where TPanel : Layoutable
        {
            var control = e.Sender as Control;
            var panel = control?.GetVisualParent() as TPanel;
            panel?.InvalidateArrange();
        }
        
        public static void AffectsParentArrange<TPanel>(params AvaloniaProperty[] properties)
            where TPanel : Layoutable
        {
            foreach (var property in properties)
            {
                property.Changed.Subscribe(new AnonuymousObserver<AvaloniaPropertyChangedEventArgs>(AffectsParentArrangeInvalidate<TPanel>));
            }
        }
        
        private class AnonuymousObserver<T> : IObserver<T>
        {
            private readonly Action<T> _onNext;
            private readonly Action<Exception>? _onError;
            private readonly Action? _onCompleted;

            public AnonuymousObserver(Action<T> onNext, Action<Exception>? onError = null, Action? onCompleted = null)
            {
                _onNext = onNext;
                _onError = onError;
                _onCompleted = onCompleted;
            }

            public void OnCompleted() => _onCompleted?.Invoke();
            public void OnError(Exception error) => _onError?.Invoke(error);
            public void OnNext(T value) => _onNext?.Invoke(value);
        }
    }
}
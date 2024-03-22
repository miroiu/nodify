using System.Windows;
using Avalonia;

namespace Nodify
{
    public class BindingProxy : AvaloniaObject
    {
        public static readonly StyledProperty<object?> DataContextProperty =
            AvaloniaProperty.Register<BindingProxy, object?>(nameof(DataContext));

        public object? DataContext
        {
            get => GetValue(DataContextProperty);
            set => SetValue(DataContextProperty, value);
        }
    }
}
using System.Windows;

namespace Nodify
{
    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register(nameof(DataContext), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(default(object)));

        public object DataContext
        {
            get => GetValue(DataContextProperty);
            set => SetValue(DataContextProperty, value);
        }

        protected override Freezable CreateInstanceCore()
            => new BindingProxy();
    }
}

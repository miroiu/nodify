using System.Windows.Controls;
using System.Windows;

namespace Nodify
{
    public class HotKeyControl : Control
    {
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(nameof(Number), typeof(int), typeof(HotKeyControl), new PropertyMetadata(BoxValue.Int0));

        public int Number
        {
            get => (int)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        static HotKeyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HotKeyControl), new FrameworkPropertyMetadata(typeof(HotKeyControl)));
        }
    }
}

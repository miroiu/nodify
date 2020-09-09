using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify.StateMachine
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
        public bool Negate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return (Negate ? !b : b) ? Visibility.Visible : FalseVisibility;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility v && v == Visibility.Visible;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

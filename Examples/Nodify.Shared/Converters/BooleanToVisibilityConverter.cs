using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public bool FalseVisibility { get; set; } = false;
        public bool Negate { get; set; }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? stringValue = value?.ToString();
            if (bool.TryParse(stringValue, out var b))
            {
                return (Negate ? !b : b) ? true : FalseVisibility;
            }
            else if (double.TryParse(stringValue, out var d))
            {
                return (Negate ? !(d > 0) : (d > 0)) ? true : FalseVisibility;
            }

            bool result = value != null;
            return (Negate ? !result : result) ? true : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool v && v;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

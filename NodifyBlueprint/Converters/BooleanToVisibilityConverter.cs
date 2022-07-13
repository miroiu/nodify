using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace NodifyBlueprint
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
        public bool Negate { get; set; }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? stringValue = value?.ToString();
            if (bool.TryParse(stringValue, out bool b))
            {
                return (Negate ? !b : b) ? Visibility.Visible : FalseVisibility;
            }
            else if (double.TryParse(stringValue, out double d))
            {
                return (Negate ? !(d > 0) : (d > 0)) ? Visibility.Visible : FalseVisibility;
            }

            bool result = value != null;
            return (Negate ? !result : result) ? Visibility.Visible : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility v && v == Visibility.Visible;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

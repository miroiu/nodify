using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    public class StringToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility NullVisibility { get; set; } = Visibility.Collapsed;

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => string.IsNullOrEmpty(value as string) ? NullVisibility : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

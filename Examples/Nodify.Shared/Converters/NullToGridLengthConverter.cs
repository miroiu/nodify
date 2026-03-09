using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    [ValueConversion(typeof(object), typeof(GridLength))]
    public class NullToGridLengthConverter : MarkupExtension, IValueConverter
    {
        public GridLength NullValue { get; set; } = new GridLength(0);
        public GridLength NotNullValue { get; set; } = new GridLength(1, GridUnitType.Star);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value == null ? NullValue : NotNullValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}

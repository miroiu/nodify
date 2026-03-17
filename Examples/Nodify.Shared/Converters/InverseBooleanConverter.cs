using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    public class InverseBooleanConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The value must be of type bool.");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The value must be of type bool.");

            return !(bool)value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

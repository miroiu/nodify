using System;
using System.Globalization;
using System.Windows.Data;

namespace Nodify
{
    internal class SubtractConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            // MP! Check: Avalonia needs this.  Why is MultiValueConverter behavior different from WPF?
            if (values.Any(x => x is UnsetValueType)) return false;

            double result = (double)values[0]! - (double)values[1]!;
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

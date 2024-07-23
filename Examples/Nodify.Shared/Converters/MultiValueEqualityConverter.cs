using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nodify
{
    public class MultiValueEqualityConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values == null)
            {
                return false;
            }

            return AllElementsEqual(values) || AllElementsNull(values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static bool AllElementsEqual(IEnumerable<object> values)
        {
            object? firstElement = values.FirstOrDefault();
            return values.All(o => o?.Equals(firstElement) == true);
        }

        private static bool AllElementsNull(IEnumerable<object> values)
        {
            return values.All(o => o == null);
        }
    }
}

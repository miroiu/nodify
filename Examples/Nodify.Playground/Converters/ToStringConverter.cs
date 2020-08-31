using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nodify.Playground
{
    public class ToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Point p)
            {
                return $"{p.X:0.00}, {p.Y:0.00}";
            }

            if(value is double d)
            {
                return d.ToString("0.00");
            }

            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nodify
{
    public class ToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Point p)
            {
                return $"{p.X:0.0}, {p.Y:0.0}";
            }

            if (value is Size s)
            {
                return $"{s.Width:0.0}, {s.Height:0.0}";
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

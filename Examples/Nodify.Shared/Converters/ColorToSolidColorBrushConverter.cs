using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Nodify
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                var brush = new SolidColorBrush(color);

                if(double.TryParse(parameter?.ToString(), out double opacity))
                    brush.Opacity = opacity;

                return brush;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
                return brush.Color;

            return value;
        }
    }
}

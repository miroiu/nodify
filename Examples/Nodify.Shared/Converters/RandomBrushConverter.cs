using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Nodify
{
    public class RandomBrushConverter : IValueConverter
    {
        private readonly Random _rand = new Random();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(parameter?.ToString(), out double alpha))
            {
                return new SolidColorBrush(Color.FromRgb((byte)_rand.Next(256), (byte)_rand.Next(256), (byte)_rand.Next(256)))
                {
                    Opacity = alpha
                };
            }

            return new SolidColorBrush(Color.FromRgb((byte)_rand.Next(256), (byte)_rand.Next(256), (byte)_rand.Next(256)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

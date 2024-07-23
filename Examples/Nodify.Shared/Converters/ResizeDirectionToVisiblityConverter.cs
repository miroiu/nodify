using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nodify
{
    internal class ResizeDirectionToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ResizeDirections resizeDirections))
            {
                return Visibility.Collapsed;
            }

            if (Enum.TryParse(parameter.ToString(), out ResizeDirections direction))
            {
                return resizeDirections.HasFlag(direction) ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

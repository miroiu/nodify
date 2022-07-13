using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NodifyBlueprint
{
    internal class TypeToColorConverter : IValueConverter
    {
        private readonly Dictionary<Type, Color> _mappings = new Dictionary<Type, Color>
        {
            [typeof(int)] = Colors.DodgerBlue,
            [typeof(double)] = Colors.YellowGreen,
            [typeof(bool)] = Colors.Red,
            [typeof(string)] = Colors.LightPink,
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type? type = value?.GetType().GetGenericArguments()[0];
            if (type != null && _mappings.TryGetValue(type, out var color))
            {
                return new SolidColorBrush(color);
            }

            return Brushes.LightSlateGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

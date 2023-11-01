using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Nodifier
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
            if (value is IRelayConnector connector)
            {
                return Convert(connector.Source, targetType, parameter, culture);
            }
            else
            {
                Type[]? args = value?.GetType().GetGenericArguments();

                if (args?.Length > 0)
                {
                    Type? type = args[0];
                    if (type != null && _mappings.TryGetValue(type, out var color))
                    {
                        return new SolidColorBrush(color);
                    }
                }
            }

            return Brushes.LightSlateGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

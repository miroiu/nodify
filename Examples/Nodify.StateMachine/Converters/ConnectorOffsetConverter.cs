using System;
using System.Windows;
using System.Globalization;
using System.Windows.Data;

namespace Nodify.StateMachine
{
    public class ConnectorOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double offset = System.Convert.ToDouble(parameter);
            if (value is Size s)
            {
                return new Size((s.Width + offset) / 2, (s.Height + offset) / 2);
            }

            return new Size(offset / 2, offset / 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double offset = System.Convert.ToDouble(parameter);
            if (value is Size s)
            {
                return new Size((s.Width + offset) / 2, (s.Height + offset) / 2);
            }

            return new Size(offset / 2, offset / 2);
        }
    }
}

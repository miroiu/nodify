using System;
using System.Globalization;
using System.Windows.Data;

namespace Nodify.Playground
{
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine($"Value: {value} :: Parameter: {parameter}");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine($"Value: {value} :: Parameter: {parameter}");
            return value;
        }
    }
}

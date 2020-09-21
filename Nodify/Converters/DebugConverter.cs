using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    public class DebugConverter : MarkupExtension, IValueConverter
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

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

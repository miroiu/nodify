using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify.Playground
{
    public class UIntToRectConverter : MarkupExtension, IValueConverter
    {
        public uint Multiplier { get; set; } = 1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint size = System.Convert.ToUInt32(value) * Multiplier;
            return new Rect(0d, 0d, size, size);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}

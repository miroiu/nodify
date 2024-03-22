using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify.Playground
{
    public class UIntToRelativeRectConverter : MarkupExtension, IValueConverter
    {
        public static UIntToRelativeRectConverter Absolute { get; } = new UIntToRelativeRectConverter() { Unit = RelativeUnit.Absolute };
        
        public static UIntToRelativeRectConverter Relative { get; } = new UIntToRelativeRectConverter() { Unit = RelativeUnit.Relative };
        
        public uint Multiplier { get; set; } = 1;
        
        public RelativeUnit Unit { get; set; } = RelativeUnit.Absolute;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint size = System.Convert.ToUInt32(value) * Multiplier;
            return new RelativeRect(0d, 0d, size, size, Unit);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}

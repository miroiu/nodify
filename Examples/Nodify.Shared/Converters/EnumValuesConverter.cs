using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    public readonly struct EnumValue
    {
        public EnumValue(string name, object? value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public object? Value { get; }
    }

    public class EnumValuesConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                var type = enumValue.GetType();
                var values = Enum.GetValues(type);
                var names = Enum.GetNames(type);

                EnumValue[] result = new EnumValue[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    result[i] = new EnumValue(names[i], values.GetValue(i));
                }

                return result;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

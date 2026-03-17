using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify
{
    public class NullToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isNull = value == null;
            if (Invert)
            {
                isNull = !isNull;
            }

            return isNull ? FalseVisibility : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

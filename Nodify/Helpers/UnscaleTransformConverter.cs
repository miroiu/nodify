using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Nodify
{
    internal class UnscaleTransformConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TransformGroup transformGroup)
                return new MatrixTransform(transformGroup.Children[0].Value.Invert());
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    internal class ScaleDoubleConverter : IMultiValueConverter
    {
        public object Convert(IList<object?> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count == 2 && values[0] is double d1 && values[1] is double d2)
                return d1 * d2;
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ScalePointConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            // MP! Check: Avalonia needs this.  Why is MultiValueConverter behavior different from WPF?
            if (values.Any(x => x is UnsetValueType)) return false;

            Point result = (Point)((Vector)(Point)values[0]! * (double)values[1]!);
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

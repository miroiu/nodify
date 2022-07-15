using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace NodifyBlueprint
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
        public bool Negate { get; set; }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? stringValue = value?.ToString();
            if (bool.TryParse(stringValue, out bool b))
            {
                return (Negate ? !b : b) ? Visibility.Visible : FalseVisibility;
            }
            else if (double.TryParse(stringValue, out double d))
            {
                return (Negate ? !(d > 0) : (d > 0)) ? Visibility.Visible : FalseVisibility;
            }

            bool result = value != null;
            return (Negate ? !result : result) ? Visibility.Visible : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility v && v == Visibility.Visible;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }

    public enum BooleanOperator
    {
        And,
        Or
    }

    public class BooleanExpressionToVisibilityConverter : MarkupExtension, IMultiValueConverter
    {
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
        public bool Negate { get; set; }
        public BooleanOperator Operator { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = Operator == BooleanOperator.And;

            if (Operator == BooleanOperator.And)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    result = result && (bool)values[i];
                }
            }
            else if (Operator == BooleanOperator.Or)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    result = result || (bool)values[i];
                }
            }

            result = Negate ? !result : result;
            return result ? Visibility.Visible : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility v && v == Visibility.Visible;

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

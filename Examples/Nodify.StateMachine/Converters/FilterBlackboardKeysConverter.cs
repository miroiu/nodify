using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify.StateMachine
{
    public class FilterBlackboardKeysConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is IEnumerable<BlackboardKeyViewModel> keys && values[1] is BlackboardKeyType filter)
            {
                return keys.Where(k => k.Type == filter || filter == BlackboardKeyType.Object);
            }

            return values;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

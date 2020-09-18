using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Nodify.StateMachine
{
    public class BlackboardKeyEditorConverter : MarkupExtension, IMultiValueConverter
    {
        public bool CanChangeInputType { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is ICollection<BlackboardKeyViewModel> availableKeys && values[1] is BlackboardKeyViewModel target)
            {
                return new BlackboardKeyEditorViewModel
                {
                    AvailableKeys = availableKeys,
                    Target = target,
                    IsEditing = values.Length >= 3 && values[2] is bool b && b,
                    CanChangeInputType = CanChangeInputType && (target.Type != BlackboardKeyType.Object || target.CanChangeType),
                    CanChangeKeyType = target.CanChangeType
                };
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

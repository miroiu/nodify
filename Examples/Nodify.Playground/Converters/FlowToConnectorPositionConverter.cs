using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Nodify.Playground
{
    public class FlowToConnectorPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ConnectorViewModel connector)
            {
                if (connector.Node.Orientation == Orientation.Horizontal)
                {
                    return connector.Flow == ConnectorFlow.Output
                        ? ConnectorPosition.Right
                        : ConnectorPosition.Left;
                }

                return connector.Flow == ConnectorFlow.Output
                    ? ConnectorPosition.Bottom
                    : ConnectorPosition.Top;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

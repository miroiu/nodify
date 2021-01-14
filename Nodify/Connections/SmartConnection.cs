using System.Windows;

namespace Nodify
{
    public abstract class SmartConnection : BaseConnection
    {
        public static readonly DependencyProperty SourceConnectorProperty = DependencyProperty.Register(nameof(SourceConnector), typeof(object), typeof(SmartConnection));
        public static readonly DependencyProperty TargetConnectorProperty = DependencyProperty.Register(nameof(TargetConnector), typeof(object), typeof(SmartConnection));

        public object? SourceConnector
        {
            get => GetValue(SourceConnectorProperty);
            set => SetValue(SourceConnectorProperty, value);
        }

        public object? TargetConnector
        {
            get => GetValue(TargetConnectorProperty);
            set => SetValue(TargetConnectorProperty, value);
        }

        public SmartConnector? SourceElement
        {
            get
            {
                if (SourceConnector != null)
                {
                    SmartConnector.LoadedConnectors.TryGetValue(SourceConnector, out var result);
                    return result;
                }
                return null;
            }
        }

        public SmartConnector? TargetElement
        {
            get
            {
                if (TargetConnector != null)
                {
                    SmartConnector.LoadedConnectors.TryGetValue(TargetConnector, out var result);
                    return result;
                }
                return null;
            }
        }
    }
}

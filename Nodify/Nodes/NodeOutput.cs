using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class NodeOutput : Connector
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(object), typeof(NodeOutput));
        public static readonly DependencyProperty ConnectorTemplateProperty = DependencyProperty.Register(nameof(ConnectorTemplate), typeof(ControlTemplate), typeof(NodeOutput));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public ControlTemplate ConnectorTemplate
        {
            get => (ControlTemplate)GetValue(ConnectorTemplateProperty);
            set => SetValue(ConnectorTemplateProperty, value);
        }

        static NodeOutput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeOutput), new FrameworkPropertyMetadata(typeof(NodeOutput)));
        }
    }
}

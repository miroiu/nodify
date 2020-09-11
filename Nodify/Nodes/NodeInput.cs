using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class NodeInput : Connector
    {
        #region Dependency Properties

        public static readonly DependencyProperty HeaderProperty = HeaderedContentControl.HeaderProperty.AddOwner(typeof(NodeInput));
        public static readonly DependencyProperty HeaderTemplateProperty = HeaderedContentControl.HeaderTemplateProperty.AddOwner(typeof(NodeInput));
        public static readonly DependencyProperty ConnectorTemplateProperty = DependencyProperty.Register(nameof(ConnectorTemplate), typeof(ControlTemplate), typeof(NodeInput));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        public ControlTemplate ConnectorTemplate
        {
            get => (ControlTemplate)GetValue(ConnectorTemplateProperty);
            set => SetValue(ConnectorTemplateProperty, value);
        }

        #endregion

        static NodeInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeInput), new FrameworkPropertyMetadata(typeof(NodeInput)));
        }
    }
}

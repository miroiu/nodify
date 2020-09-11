using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class NodeOutput : Connector
    {
        #region Dependency Properties
        
        public static readonly DependencyProperty HeaderProperty = HeaderedContentControl.HeaderProperty.AddOwner(typeof(NodeOutput));
        public static readonly DependencyProperty HeaderTemplateProperty = HeaderedContentControl.HeaderTemplateProperty.AddOwner(typeof(NodeOutput));
        public static readonly DependencyProperty ConnectorTemplateProperty = NodeInput.ConnectorTemplateProperty.AddOwner(typeof(NodeOutput));

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

        static NodeOutput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeOutput), new FrameworkPropertyMetadata(typeof(NodeOutput)));
        }
    }
}

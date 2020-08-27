using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class KnotNode : Control
    {
        public static readonly DependencyProperty ConnectorStyleProperty = DependencyProperty.Register(nameof(ConnectorStyle), typeof(Style), typeof(KnotNode));

        public Style ConnectorStyle
        {
            get => (Style)GetValue(ConnectorStyleProperty);
            set => SetValue(ConnectorStyleProperty, value);
        }

        static KnotNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KnotNode), new FrameworkPropertyMetadata(typeof(KnotNode)));
        }
    }
}

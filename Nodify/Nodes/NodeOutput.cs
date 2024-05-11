using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>
    /// Represents the default control for the <see cref="Node.OutputConnectorTemplate"/>.
    /// </summary>
    public partial class NodeOutput : Connector
    {
        #region Dependency Properties
        
        public static readonly StyledProperty<object?> HeaderProperty = HeaderedContentControl.HeaderProperty.AddOwner<NodeOutput>();
        public static readonly StyledProperty<IDataTemplate?> HeaderTemplateProperty = HeaderedContentControl.HeaderTemplateProperty.AddOwner<NodeOutput>();
        public static readonly StyledProperty<ControlTemplate> ConnectorTemplateProperty = NodeInput.ConnectorTemplateProperty.AddOwner<NodeOutput>();
        public static readonly StyledProperty<Orientation> OrientationProperty = StackPanel.OrientationProperty.AddOwner<NodeOutput>();

        /// <summary>
        /// Gets of sets the data used for the control's header.
        /// </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the template used to display the content of the control's header.
        /// </summary>
        public IDataTemplate? HeaderTemplate
        {
            get => GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the template used to display the connecting point of this <see cref="Connector"/>.
        /// </summary>
        public ControlTemplate ConnectorTemplate
        {
            get => (ControlTemplate)GetValue(ConnectorTemplateProperty);
            set => SetValue(ConnectorTemplateProperty, value);
        }

        /// <inheritdoc cref="StackPanel.Orientation" />
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        #endregion

        static NodeOutput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeOutput), new FrameworkPropertyMetadata(typeof(NodeOutput)));
            AffectsMeasure<NodeOutput>(OrientationProperty);
            OrientationProperty.OverrideDefaultValue<NodeOutput>(Orientation.Horizontal);
        }
    }
}

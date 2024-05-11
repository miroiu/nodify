using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>
    /// Represents the default control for the <see cref="Node.InputConnectorTemplate"/>.
    /// </summary>
    public partial class NodeInput : Connector
    {
        #region Dependency Properties

        public static readonly StyledProperty<object?> HeaderProperty = HeaderedContentControl.HeaderProperty.AddOwner<NodeInput>();
        public static readonly StyledProperty<IDataTemplate?> HeaderTemplateProperty = HeaderedContentControl.HeaderTemplateProperty.AddOwner<NodeInput>();
        public static readonly StyledProperty<ControlTemplate> ConnectorTemplateProperty = AvaloniaProperty.Register<NodeInput, ControlTemplate>(nameof(ConnectorTemplate));
        public static readonly StyledProperty<Orientation> OrientationProperty = StackPanel.OrientationProperty.AddOwner<NodeInput>();

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

        static NodeInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeInput), new FrameworkPropertyMetadata(typeof(NodeInput)));
            AffectsMeasure<NodeInput>(OrientationProperty);
            OrientationProperty.OverrideDefaultValue<NodeInput>(Orientation.Horizontal);
        }
    }
}

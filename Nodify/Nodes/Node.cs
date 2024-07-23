using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a control that has a list of <see cref="Input"/> <see cref="Connector"/>s and a list of <see cref="Output"/> <see cref="Connector"/>s.
    /// </summary>
    public partial class Node : HeaderedContentControl
    {
        #region Dependency Properties

        public static readonly StyledProperty<IBrush> ContentBrushProperty = AvaloniaProperty.Register<Node, IBrush>(nameof(ContentBrush));
        public static readonly StyledProperty<IBrush> HeaderBrushProperty = AvaloniaProperty.Register<Node, IBrush>(nameof(HeaderBrush));
        public static readonly StyledProperty<IBrush> FooterBrushProperty = AvaloniaProperty.Register<Node, IBrush>(nameof(FooterBrush));
        public static readonly StyledProperty<object> FooterProperty = AvaloniaProperty.Register<Node, object>(nameof(Footer));
        public static readonly StyledProperty<DataTemplate> FooterTemplateProperty = AvaloniaProperty.Register<Node, DataTemplate>(nameof(FooterTemplate));
        public static readonly StyledProperty<DataTemplate> InputConnectorTemplateProperty = AvaloniaProperty.Register<Node, DataTemplate>(nameof(InputConnectorTemplate));
        public static readonly DirectProperty<Node, bool> HasFooterProperty = AvaloniaProperty.RegisterDirect<Node, bool>(nameof(HasFooter), x => x.HasFooter);
        public static readonly StyledProperty<DataTemplate> OutputConnectorTemplateProperty = AvaloniaProperty.Register<Node, DataTemplate>(nameof(OutputConnectorTemplate));
        public static readonly StyledProperty<IEnumerable> InputProperty = AvaloniaProperty.Register<Node, IEnumerable>(nameof(Input));
        public static readonly StyledProperty<IEnumerable> OutputProperty = AvaloniaProperty.Register<Node, IEnumerable>(nameof(Output));
        public static readonly StyledProperty<ControlTheme> ContentContainerStyleProperty = AvaloniaProperty.Register<Node, ControlTheme>(nameof(ContentContainerStyle));
        public static readonly StyledProperty<ControlTheme> HeaderContainerStyleProperty = AvaloniaProperty.Register<Node, ControlTheme>(nameof(HeaderContainerStyle));
        public static readonly StyledProperty<ControlTheme> FooterContainerStyleProperty = AvaloniaProperty.Register<Node, ControlTheme>(nameof(FooterContainerStyle));

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="ContentControl.Content"/> of this <see cref="Node"/>.
        /// </summary>
        public IBrush ContentBrush
        {
            get => (IBrush)GetValue(ContentBrushProperty);
            set => SetValue(ContentBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="HeaderedContentControl.Header"/> of this <see cref="Node"/>.
        /// </summary>
        public IBrush HeaderBrush
        {
            get => (IBrush)GetValue(HeaderBrushProperty);
            set => SetValue(HeaderBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="Node.Footer"/> of this <see cref="Node"/>.
        /// </summary>
        public IBrush FooterBrush
        {
            get => (IBrush)GetValue(FooterBrushProperty);
            set => SetValue(FooterBrushProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the data for the footer of this control.
        /// </summary>
        public object Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        /// <summary>
        /// Gets or sets the template used to display the content of the control's footer.
        /// </summary>
        public DataTemplate FooterTemplate
        {
            get => (DataTemplate)GetValue(FooterTemplateProperty);
            set => SetValue(FooterTemplateProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the template used to display the content of the control's <see cref="Input"/> connectors.
        /// </summary>
        public DataTemplate InputConnectorTemplate
        {
            get => (DataTemplate)GetValue(InputConnectorTemplateProperty);
            set => SetValue(InputConnectorTemplateProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the template used to display the content of the control's <see cref="Output"/> connectors.
        /// </summary>
        public DataTemplate OutputConnectorTemplate
        {
            get => (DataTemplate)GetValue(OutputConnectorTemplateProperty);
            set => SetValue(OutputConnectorTemplateProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the data for the input <see cref="Connector"/>s of this control.
        /// </summary>
        public IEnumerable Input
        {
            get => (IEnumerable)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the data for the output <see cref="Connector"/>s of this control.
        /// </summary>
        public IEnumerable Output
        {
            get => (IEnumerable)GetValue(OutputProperty);
            set => SetValue(OutputProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the content container.
        /// </summary>
        public ControlTheme ContentContainerStyle
        {
            get => GetValue(ContentContainerStyleProperty);
            set => SetValue(ContentContainerStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the header container.
        /// </summary>
        public ControlTheme HeaderContainerStyle
        {
            get => GetValue(HeaderContainerStyleProperty);
            set => SetValue(HeaderContainerStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the footer container.
        /// </summary>
        public ControlTheme FooterContainerStyle
        {
            get => GetValue(FooterContainerStyleProperty);
            set => SetValue(FooterContainerStyleProperty, value);
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="Footer"/> is <see langword="null" />.
        /// </summary>
        public bool HasFooter => Footer != null;

        private static void OnFooterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Node node = (Node)d;
            node.RaisePropertyChanged(HasFooterProperty, e.OldValue != null, e.NewValue != null);
        }

        #endregion

        static Node()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
            FooterProperty.Changed.AddClassHandler<Node>(OnFooterChanged);
        }
    }
}

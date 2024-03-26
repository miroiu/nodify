using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a control that acts as a <see cref="Connector"/>.
    /// </summary>
    [TemplatePart(Name = ElementContent, Type = typeof(UIElement))]
    public class StateNode : Connector
    {
        protected const string ElementContent = "PART_Content";

        #region Dependency Properties

        public static readonly StyledProperty<IBrush> HighlightBrushProperty = ItemContainer.HighlightBrushProperty.AddOwner<StateNode>();
        public static readonly StyledProperty<object?> ContentProperty = ContentPresenter.ContentProperty.AddOwner<StateNode>();
        public static readonly StyledProperty<IDataTemplate?> ContentTemplateProperty = ContentPresenter.ContentTemplateProperty.AddOwner<StateNode>();

        /// <summary>
        /// Gets or sets the brush used when the <see cref="PendingConnection.IsOverElementProperty"/> attached property is true for this <see cref="StateNode"/>.
        /// </summary>
        public IBrush HighlightBrush
        {
            get => (IBrush)GetValue(HighlightBrushProperty);
            set => SetValue(HighlightBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the data for the control's content.
        /// </summary>
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the template used to display the content of the control's header.
        /// </summary>
        public IDataTemplate? ContentTemplate
        {
            get => GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }
        
        #endregion
        
        /// <summary>
        /// Gets the <see cref="ContentControl"/> control of this <see cref="StateNode"/>.
        /// </summary>
        protected UIElement? ContentControl { get; private set; }

        static StateNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StateNode), new FrameworkPropertyMetadata(typeof(StateNode)));
        }

        /// <inheritdoc />
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            ContentControl = e.NameScope.Find<Control>(ElementContent);
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            // Do not raise PendingConnection events if clicked on content
            if (e.Source is Visual visual && (!ContentControl?.IsAncestorOf(visual) ?? true))
            {
                base.OnMouseDown(e);
            }
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            // Do not raise PendingConnection events if clicked on content
            if (e.Source is Visual visual && (!ContentControl?.IsAncestorOf(visual) ?? true))
            {
                base.OnMouseUp(e);
            }
        }
    }
}

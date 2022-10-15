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

        public static readonly DependencyProperty HighlightBrushProperty = ItemContainer.HighlightBrushProperty.AddOwner(typeof(StateNode));
        public static readonly DependencyProperty ContentProperty = ContentPresenter.ContentProperty.AddOwner(typeof(StateNode));
        public static readonly DependencyProperty ContentTemplateProperty = ContentPresenter.ContentTemplateProperty.AddOwner(typeof(StateNode));
        public static readonly DependencyProperty CornerRadiusProperty = Border.CornerRadiusProperty.AddOwner(typeof(StateNode));

        /// <summary>
        /// Gets or sets the brush used when the <see cref="PendingConnection.IsOverElementProperty"/> attached property is true for this <see cref="StateNode"/>.
        /// </summary>
        public Brush HighlightBrush
        {
            get => (Brush)GetValue(HighlightBrushProperty);
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
        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }
        
        /// <summary>
        /// Gets or sets a value that represents the degree to which the corners of the <see cref="StateNode"/> are rounded.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
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
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentControl = Template.FindName(ElementContent, this) as UIElement;
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            // Do not raise PendingConnection events if clicked on content
            if (e.OriginalSource is Visual visual && (!ContentControl?.IsAncestorOf(visual) ?? true))
            {
                base.OnMouseDown(e);
            }
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            // Do not raise PendingConnection events if clicked on content
            if (e.OriginalSource is Visual visual && (!ContentControl?.IsAncestorOf(visual) ?? true))
            {
                base.OnMouseUp(e);
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Nodify
{
    [TemplatePart(Name = ElementContent, Type = typeof(UIElement))]
    public class StateNode : Connector
    {
        private const string ElementContent = "PART_Content";

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register(nameof(HighlightBrush), typeof(Brush), typeof(StateNode));
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(StateNode));
        public static readonly DependencyProperty ContentTemplateProperty = ContentPresenter.ContentTemplateProperty.AddOwner(typeof(StateNode));
        public static readonly DependencyProperty CornerRadiusProperty = Border.CornerRadiusProperty.AddOwner(typeof(StateNode));
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(nameof(Size), typeof(Size), typeof(StateNode), new FrameworkPropertyMetadata(BoxValue.Size));

        public Brush HighlightBrush
        {
            get => (Brush)GetValue(HighlightBrushProperty);
            set => SetValue(HighlightBrushProperty, value);
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Size Size
        {
            get => (Size)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        static StateNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StateNode), new FrameworkPropertyMetadata(typeof(StateNode)));
        }

        protected UIElement? ContentControl { get; private set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentControl = Template.FindName(ElementContent, this) as UIElement;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Visual visual && (!ContentControl?.IsAncestorOf(visual) ?? true))
            {
                base.OnMouseLeftButtonDown(e);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Visual visual && (!ContentControl?.IsAncestorOf(visual) ?? true))
            {
                base.OnMouseLeftButtonUp(e);
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            Size = sizeInfo.NewSize;
        }
    }
}

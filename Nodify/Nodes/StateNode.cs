using System;
using System.Windows;
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
        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(StateNode));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(StateNode), new FrameworkPropertyMetadata(default(CornerRadius)));
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UIElement? content = Template.FindName(ElementContent, this) as UIElement;

            if (content != null)
            {
                content.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnContentMouseDown));
                content.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnContentMouseUp));
            }
        }

        private void OnContentMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Editor != null && Container != null)
            {
                Editor.UnselectAll();
                Container.IsSelected = true;
            }
        }

        private void OnContentMouseDown(object sender, MouseButtonEventArgs e)
            => e.Handled = true;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            Size = sizeInfo.NewSize;
        }
    }
}

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    public class Node : HeaderedContentControl
    {
        #region Dependecy Properties

        public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register(nameof(HeaderBrush), typeof(Brush), typeof(Node));
        public static readonly DependencyProperty FooterBrushProperty = DependencyProperty.Register(nameof(FooterBrush), typeof(Brush), typeof(Node));
        public static readonly DependencyProperty IconProperty = MenuItem.IconProperty.AddOwner(typeof(Node));
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(nameof(Footer), typeof(object), typeof(Node));
        public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register(nameof(FooterTemplate), typeof(DataTemplate), typeof(Node));
        public static readonly DependencyProperty InputConnectorTemplateProperty = DependencyProperty.Register(nameof(InputConnectorTemplate), typeof(DataTemplate), typeof(Node));
        public static readonly DependencyProperty OutputConnectorTemplateProperty = DependencyProperty.Register(nameof(OutputConnectorTemplate), typeof(DataTemplate), typeof(Node));
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register(nameof(Input), typeof(IEnumerable), typeof(Node));
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register(nameof(Output), typeof(IEnumerable), typeof(Node));

        public Brush HeaderBrush
        {
            get => (Brush)GetValue(HeaderBrushProperty);
            set => SetValue(HeaderBrushProperty, value);
        }

        public Brush FooterBrush
        {
            get => (Brush)GetValue(FooterBrushProperty);
            set => SetValue(FooterBrushProperty, value);
        }

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public object Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        public DataTemplate FooterTemplate
        {
            get => (DataTemplate)GetValue(FooterTemplateProperty);
            set => SetValue(FooterTemplateProperty, value);
        }

        public DataTemplate InputConnectorTemplate
        {
            get => (DataTemplate)GetValue(InputConnectorTemplateProperty);
            set => SetValue(InputConnectorTemplateProperty, value);
        }

        public DataTemplate OutputConnectorTemplate
        {
            get => (DataTemplate)GetValue(OutputConnectorTemplateProperty);
            set => SetValue(OutputConnectorTemplateProperty, value);
        }

        public IEnumerable Input
        {
            get => (IEnumerable)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public IEnumerable Output
        {
            get => (IEnumerable)GetValue(OutputProperty);
            set => SetValue(OutputProperty, value);
        }

        #endregion

        static Node()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
        }
    }
}

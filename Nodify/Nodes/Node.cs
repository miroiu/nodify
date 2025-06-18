using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a control that has a list of <see cref="Input"/> <see cref="Connector"/>s and a list of <see cref="Output"/> <see cref="Connector"/>s.
    /// </summary>
    [TemplatePart(Name = ElementInputItemsControl, Type = typeof(ItemsControl))]
    [TemplatePart(Name = ElementOutputItemsControl, Type = typeof(ItemsControl))]
    [StyleTypedProperty(Property = nameof(ContentContainerStyle), StyleTargetType = typeof(Border))]
    [StyleTypedProperty(Property = nameof(HeaderContainerStyle), StyleTargetType = typeof(Border))]
    [StyleTypedProperty(Property = nameof(FooterContainerStyle), StyleTargetType = typeof(Border))]
    public class Node : HeaderedContentControl
    {
        protected const string ElementInputItemsControl = "PART_Input";
        protected const string ElementOutputItemsControl = "PART_Output";

        #region Dependency Properties

        public static readonly DependencyProperty ContentBrushProperty = DependencyProperty.Register(nameof(ContentBrush), typeof(Brush), typeof(Node));
        public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register(nameof(HeaderBrush), typeof(Brush), typeof(Node));
        public static readonly DependencyProperty FooterBrushProperty = DependencyProperty.Register(nameof(FooterBrush), typeof(Brush), typeof(Node));
        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(nameof(Footer), typeof(object), typeof(Node), new FrameworkPropertyMetadata(OnFooterChanged));
        public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register(nameof(FooterTemplate), typeof(DataTemplate), typeof(Node));
        public static readonly DependencyProperty InputConnectorTemplateProperty = DependencyProperty.Register(nameof(InputConnectorTemplate), typeof(DataTemplate), typeof(Node));
        protected static readonly DependencyPropertyKey HasFooterPropertyKey = DependencyProperty.RegisterReadOnly(nameof(HasFooter), typeof(bool), typeof(Node), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty HasFooterProperty = HasFooterPropertyKey.DependencyProperty;
        public static readonly DependencyProperty OutputConnectorTemplateProperty = DependencyProperty.Register(nameof(OutputConnectorTemplate), typeof(DataTemplate), typeof(Node));
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register(nameof(Input), typeof(IEnumerable), typeof(Node));
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register(nameof(Output), typeof(IEnumerable), typeof(Node));
        public static readonly DependencyProperty ContentContainerStyleProperty = DependencyProperty.Register(nameof(ContentContainerStyle), typeof(Style), typeof(Node));
        public static readonly DependencyProperty HeaderContainerStyleProperty = DependencyProperty.Register(nameof(HeaderContainerStyle), typeof(Style), typeof(Node));
        public static readonly DependencyProperty FooterContainerStyleProperty = DependencyProperty.Register(nameof(FooterContainerStyle), typeof(Style), typeof(Node));

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="ContentControl.Content"/> of this <see cref="Node"/>.
        /// </summary>
        public Brush ContentBrush
        {
            get => (Brush)GetValue(ContentBrushProperty);
            set => SetValue(ContentBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="HeaderedContentControl.Header"/> of this <see cref="Node"/>.
        /// </summary>
        public Brush HeaderBrush
        {
            get => (Brush)GetValue(HeaderBrushProperty);
            set => SetValue(HeaderBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="Node.Footer"/> of this <see cref="Node"/>.
        /// </summary>
        public Brush FooterBrush
        {
            get => (Brush)GetValue(FooterBrushProperty);
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
        public Style ContentContainerStyle
        {
            get => (Style)GetValue(ContentContainerStyleProperty);
            set => SetValue(ContentContainerStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the header container.
        /// </summary>
        public Style HeaderContainerStyle
        {
            get => (Style)GetValue(HeaderContainerStyleProperty);
            set => SetValue(HeaderContainerStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style for the footer container.
        /// </summary>
        public Style FooterContainerStyle
        {
            get => (Style)GetValue(FooterContainerStyleProperty);
            set => SetValue(FooterContainerStyleProperty, value);
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="Footer"/> is <see langword="null" />.
        /// </summary>
        public bool HasFooter => (bool)GetValue(HasFooterProperty);

        private static void OnFooterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Node node = (Node)d;
            node.SetValue(HasFooterPropertyKey, e.NewValue != null ? BoxValue.True : BoxValue.False);
        }

        #endregion

        /// <inheritdoc cref="ItemsControl.GroupStyle"/>
        public ObservableCollection<GroupStyle> InputGroupStyle { get; } = new ObservableCollection<GroupStyle>();
        /// <inheritdoc cref="ItemsControl.GroupStyle"/>
        public ObservableCollection<GroupStyle> OutputGroupStyle { get; } = new ObservableCollection<GroupStyle>();

        protected ItemsControl? InputItemsControl { get; private set; }
        protected ItemsControl? OutputItemsControl { get; private set; }

        static Node()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
            FocusableProperty.OverrideMetadata(typeof(Node), new FrameworkPropertyMetadata(BoxValue.False));
        }

        public Node()
        {
            InputGroupStyle.CollectionChanged += OnInputGroupStyleCollectionChanged;
            OutputGroupStyle.CollectionChanged += OnOutputGroupStyleCollectionChanged;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            InputItemsControl = GetTemplateChild(ElementInputItemsControl) as ItemsControl;
            OutputItemsControl = GetTemplateChild(ElementOutputItemsControl) as ItemsControl;

            if (InputItemsControl != null)
            {
                foreach (var style in InputGroupStyle)
                {
                    InputItemsControl.GroupStyle.Add(style);
                }
            }

            if (OutputItemsControl != null)
            {
                foreach (var style in OutputGroupStyle)
                {
                    OutputItemsControl.GroupStyle.Add(style);
                }
            }
        }

        private void OnInputGroupStyleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (InputItemsControl != null)
            {
                SynchronizeCollection(InputItemsControl.GroupStyle, e);
            }
        }

        private void OnOutputGroupStyleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (OutputItemsControl != null)
            {
                SynchronizeCollection(OutputItemsControl.GroupStyle, e);
            }
        }

        private static void SynchronizeCollection(ObservableCollection<GroupStyle> collection, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        for (int i = 0; i < e.NewItems.Count; i++)
                        {
                            var item = (GroupStyle)e.NewItems[i]!;
                            collection.Add(item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        for (int i = 0; i < e.OldItems.Count; i++)
                        {
                            var item = (GroupStyle)e.OldItems[i]!;
                            collection.Remove(item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    collection[e.NewStartingIndex] = (GroupStyle)e.NewItems![0]!;
                    break;
                case NotifyCollectionChangedAction.Move:
                    collection.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    collection.Clear();
                    break;
            }
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Nodify
{
    public enum GroupingMovementMode
    {
        Group,
        Self
    }

    [TemplatePart(Name = ElementResizeThumb, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementHeader, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementContent, Type = typeof(FrameworkElement))]
    public class GroupingNode : HeaderedContentControl
    {
        private static readonly object GroupMovement = GroupingMovementMode.Group;

        private const string ElementResizeThumb = "PART_ResizeThumb";
        private const string ElementHeader = "PART_Header";
        private const string ElementContent = "PART_Content";

        public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register(nameof(HeaderBrush), typeof(Brush), typeof(GroupingNode));
        public static readonly DependencyProperty CanResizeProperty = DependencyProperty.Register(nameof(CanResize), typeof(bool), typeof(GroupingNode), new FrameworkPropertyMetadata(BoxValue.True));
        public static readonly DependencyProperty DefaultMovementModeProperty = DependencyProperty.Register(nameof(DefaultMovementMode), typeof(GroupingMovementMode), typeof(GroupingNode), new FrameworkPropertyMetadata(GroupMovement));

        public Brush HeaderBrush
        {
            get => (Brush)GetValue(HeaderBrushProperty);
            set => SetValue(HeaderBrushProperty, value);
        }

        public bool CanResize
        {
            get => (bool)GetValue(CanResizeProperty);
            set => SetValue(CanResizeProperty, value);
        }

        public GroupingMovementMode DefaultMovementMode
        {
            get => (GroupingMovementMode)GetValue(DefaultMovementModeProperty);
            set => SetValue(DefaultMovementModeProperty, value);
        }

        public static ModifierKeys SwitchMovementModeModifierKey { get; set; } = ModifierKeys.Shift;

        static GroupingNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupingNode), new FrameworkPropertyMetadata(typeof(GroupingNode)));
        }

        public GroupingNode()
        {
            AddHandler(Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnResize));

            Loaded += OnNodeLoaded;
            Unloaded += OnNodeUnloaded;
        }

        protected NodifyEditor? Editor { get; private set; }
        protected ItemContainer? Container { get; private set; }

        protected FrameworkElement? ResizeThumb;
        protected FrameworkElement? HeaderControl;
        protected FrameworkElement? ContentControl;

        private double _minHeight = 30;
        private double _minWidth = 30;

        private void OnNodeLoaded(object sender, RoutedEventArgs e)
        {
            if (HeaderControl != null)
            {
                HeaderControl.MouseLeftButtonDown += OnHeaderMouseLeftButtonDown;
                HeaderControl.SizeChanged += OnHeaderSizeChanged;
                CalculateDesiredHeaderSize();
            }
        }

        private void OnNodeUnloaded(object sender, RoutedEventArgs e)
        {
            if (HeaderControl != null)
            {
                HeaderControl.MouseLeftButtonDown -= OnHeaderMouseLeftButtonDown;
                HeaderControl.SizeChanged -= OnHeaderSizeChanged;
            }
        }

        private void OnHeaderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Container != null && Editor != null)
            {
                if (Keyboard.Modifiers == SwitchMovementModeModifierKey)
                {
                    DefaultMovementMode = DefaultMovementMode == GroupingMovementMode.Group ? GroupingMovementMode.Self : GroupingMovementMode.Group;
                }

                if (DefaultMovementMode == GroupingMovementMode.Self)
                {
                    Editor.UnselectAll();
                }
                else if (Keyboard.Modifiers != ModifierKeys.Control)
                {
                    Editor.SelectArea(new Rect(Container.Location, RenderSize), append: Container.IsSelected, fit: true);
                    Container.IsSelected = true;
                }

                if (Keyboard.Modifiers == SwitchMovementModeModifierKey)
                {
                    DefaultMovementMode = DefaultMovementMode == GroupingMovementMode.Group ? GroupingMovementMode.Self : GroupingMovementMode.Group;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ResizeThumb = Template.FindName(ElementResizeThumb, this) as FrameworkElement;
            HeaderControl = Template.FindName(ElementHeader, this) as FrameworkElement;
            ContentControl = Template.FindName(ElementContent, this) as FrameworkElement;

            Editor = this.GetParentOfType<NodifyEditor>();
            Container = this.GetParentOfType<ItemContainer>();

            if (Container != null)
            {
                Panel.SetZIndex(Container, -1);
            }
        }

        private void OnResize(object sender, DragDeltaEventArgs e)
        {
            if (CanResize && e.OriginalSource == ResizeThumb)
            {
                var resultWidth = ActualWidth + e.HorizontalChange;
                var resultHeight = ActualHeight + e.VerticalChange;

                // Snap to grid
                if (Editor != null)
                {
                    var cellSize = Editor.GridCellSize;
                    resultWidth = (int)resultWidth / cellSize * cellSize;
                    resultHeight = (int)resultHeight / cellSize * cellSize;
                }

                Width = Math.Max(_minWidth, resultWidth);
                Height = Math.Max(_minHeight, resultHeight);

                e.Handled = true;
            }
        }

        private void OnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
            => CalculateDesiredHeaderSize();

        private void CalculateDesiredHeaderSize()
        {
            if (HeaderControl != null && ResizeThumb != null)
            {
                _minHeight = Math.Max(HeaderControl.ActualHeight + ResizeThumb.ActualHeight, MinHeight);
                _minWidth = Math.Max(ResizeThumb.ActualWidth, MinWidth);

                if (ContentControl != null)
                {
                    _minWidth = Math.Max(_minWidth, ContentControl.DesiredSize.Width);
                    _minHeight = Math.Max(_minHeight, _minHeight + ContentControl.DesiredSize.Height);
                }
            }

            if (Container != null)
            {
                Container.DesiredSizeForSelection = new Size(ActualWidth, Math.Max(HeaderControl?.ActualHeight ?? _minHeight, MinHeight));
            }
        }
    }
}

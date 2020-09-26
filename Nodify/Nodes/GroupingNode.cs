using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Specifies the possible movement modes of a <see cref="GroupingNode"/>.
    /// </summary>
    public enum GroupingMovementMode
    {
        /// <summary>
        /// The <see cref="GroupingNode"/> will move its content when moved.
        /// </summary>
        Group,

        /// <summary>
        /// The <see cref="GroupingNode"/> will not move its content when moved.
        /// </summary>
        Self
    }

    /// <summary>
    /// Defines a headered area that groups <see cref="ItemContainer"/>s inside it and can be resized.
    /// </summary>
    [TemplatePart(Name = ElementResizeThumb, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementHeader, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementContent, Type = typeof(FrameworkElement))]
    public class GroupingNode : HeaderedContentControl
    {
        protected static readonly object GroupMovementBoxed = GroupingMovementMode.Group;

        protected const string ElementResizeThumb = "PART_ResizeThumb";
        protected const string ElementHeader = "PART_Header";
        protected const string ElementContent = "PART_Content";

        #region Dependency Properties

        public static readonly DependencyProperty HeaderBrushProperty = Node.HeaderBrushProperty.AddOwner(typeof(GroupingNode));
        public static readonly DependencyProperty CanResizeProperty = DependencyProperty.Register(nameof(CanResize), typeof(bool), typeof(GroupingNode), new FrameworkPropertyMetadata(BoxValue.True));
        public static readonly DependencyProperty ActualSizeProperty = DependencyProperty.Register(nameof(ActualSize), typeof(Size), typeof(GroupingNode), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnActualSizeChanged));
        public static readonly DependencyProperty MovementModeProperty = DependencyProperty.Register(nameof(MovementMode), typeof(GroupingMovementMode), typeof(GroupingNode), new FrameworkPropertyMetadata(GroupMovementBoxed));

        private static void OnActualSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = (GroupingNode)d;
            var newSize = (Size)e.NewValue;
            node.Width = newSize.Width;
            node.Height = newSize.Height;
        }

        /// <summary>
        /// Gets or sets the brush used for the background of the <see cref="HeaderedContentControl.Header"/> of this <see cref="GroupingNode"/>.
        /// </summary>
        public Brush HeaderBrush
        {
            get => (Brush)GetValue(HeaderBrushProperty);
            set => SetValue(HeaderBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="GroupingNode"/> can be resized.
        /// </summary>
        public bool CanResize
        {
            get => (bool)GetValue(CanResizeProperty);
            set => SetValue(CanResizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the actual size of this <see cref="GroupingNode"/>.
        /// </summary>
        public Size ActualSize
        {
            get => (Size)GetValue(ActualSizeProperty);
            set => SetValue(ActualSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the default movement mode which can be temporarily changed by holding the <see cref="SwitchMovementModeModifierKey"/> while dragging by the header.
        /// </summary>
        public GroupingMovementMode MovementMode
        {
            get => (GroupingMovementMode)GetValue(MovementModeProperty);
            set => SetValue(MovementModeProperty, value);
        }

        #endregion

        #region Fields

        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="GroupingNode"/>.
        /// </summary>
        protected NodifyEditor? Editor { get; private set; }

        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="Container"/>.
        /// </summary>
        protected ItemContainer? Container { get; private set; }

        /// <summary>
        /// Gets the <see cref="FrameworkElement"/> used to resize this <see cref="GroupingNode"/>.
        /// </summary>
        protected FrameworkElement? ResizeThumb;

        /// <summary>
        /// Gets the <see cref="HeaderedContentControl.Header"/> control of this <see cref="GroupingNode"/>.
        /// </summary>
        protected FrameworkElement? HeaderControl;

        /// <summary>
        /// Gets the <see cref="ContentControl.Content"/> control of this <see cref="GroupingNode"/>.
        /// </summary>
        protected FrameworkElement? ContentControl;

        /// <summary>
        /// Gets or sets the key that will toggle between <see cref="GroupingMovementMode"/>s.
        /// </summary>
        public static ModifierKeys SwitchMovementModeModifierKey { get; set; } = ModifierKeys.Shift;

        private double _minHeight = 30;
        private double _minWidth = 30;

        #endregion

        static GroupingNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupingNode), new FrameworkPropertyMetadata(typeof(GroupingNode)));
            Panel.ZIndexProperty.OverrideMetadata(typeof(GroupingNode), new FrameworkPropertyMetadata(-1, OnZIndexPropertyChanged));
        }

        private static void OnZIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = (GroupingNode)d;
            if (node.Container != null)
            {
                Panel.SetZIndex(node.Container, (int)e.NewValue);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupingNode"/> class.
        /// </summary>
        public GroupingNode()
        {
            AddHandler(Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnResize));
            AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnResizeCompleted));

            Loaded += OnNodeLoaded;
            Unloaded += OnNodeUnloaded;
        }

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
                // Switch the default movement mode if necessary
                if (Keyboard.Modifiers == SwitchMovementModeModifierKey)
                {
                    MovementMode = MovementMode == GroupingMovementMode.Group ? GroupingMovementMode.Self : GroupingMovementMode.Group;
                }

                // Deselect all so we can move without the content
                if (MovementMode == GroupingMovementMode.Self)
                {
                    Editor.UnselectAll();
                    Container.IsSelected = true;
                }
                // Select the content and move with it
                else if (Keyboard.Modifiers != ModifierKeys.Control)
                {
                    Editor.SelectArea(new Rect(Container.Location, RenderSize), append: Container.IsSelected, fit: true);
                    Container.IsSelected = true;
                }

                // Switch the default movement mode back
                if (Keyboard.Modifiers == SwitchMovementModeModifierKey)
                {
                    MovementMode = MovementMode == GroupingMovementMode.Group ? GroupingMovementMode.Self : GroupingMovementMode.Group;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ResizeThumb = Template.FindName(ElementResizeThumb, this) as FrameworkElement;
            HeaderControl = Template.FindName(ElementHeader, this) as FrameworkElement;
            ContentControl = Template.FindName(ElementContent, this) as FrameworkElement;

            Container = this.GetParentOfType<ItemContainer>();
            Editor = Container?.Editor ?? this.GetParentOfType<NodifyEditor>();

            if (Container != null)
            {
                Panel.SetZIndex(Container, Panel.GetZIndex(this));
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

        private void OnResizeCompleted(object sender, DragCompletedEventArgs e)
            => ActualSize = new Size(ActualWidth, ActualHeight);

        private void OnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
            => CalculateDesiredHeaderSize();

        private void CalculateDesiredHeaderSize()
        {
            if (HeaderControl != null && ResizeThumb != null)
            {
                _minHeight = Math.Max(HeaderControl.ActualHeight + ResizeThumb.ActualHeight, MinHeight);
                _minWidth = Math.Max(ResizeThumb.ActualWidth, MinWidth);

                // If there's content don't resize it
                if (ContentControl != null)
                {
                    _minWidth = Math.Max(_minWidth, ContentControl.DesiredSize.Width);
                    _minHeight = Math.Max(_minHeight, _minHeight + ContentControl.DesiredSize.Height);
                }
            }

            // Allow selecting only by the header
            if (Container != null)
            {
                Container.DesiredSizeForSelection = new Size(ActualWidth, Math.Max(HeaderControl?.ActualHeight ?? _minHeight, MinHeight));
            }
        }
    }
}

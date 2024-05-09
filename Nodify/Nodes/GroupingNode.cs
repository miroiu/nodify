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
    /// Defines a panel with a header that groups <see cref="ItemContainer"/>s inside it and can be resized.
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

        #region Routed Events

        public static readonly RoutedEvent ResizeStartedEvent = EventManager.RegisterRoutedEvent(nameof(ResizeStarted), RoutingStrategy.Bubble, typeof(ResizeEventHandler), typeof(GroupingNode));
        public static readonly RoutedEvent ResizeCompletedEvent = EventManager.RegisterRoutedEvent(nameof(ResizeCompleted), RoutingStrategy.Bubble, typeof(ResizeEventHandler), typeof(GroupingNode));

        /// <summary>
        /// Occurs when the node finished resizing.
        /// </summary>
        public event ResizeEventHandler ResizeCompleted
        {
            add => AddHandler(ResizeCompletedEvent, value);
            remove => RemoveHandler(ResizeCompletedEvent, value);
        }

        /// <summary>
        /// Occurs when the node started resizing.
        /// </summary>
        public event ResizeEventHandler ResizeStarted
        {
            add => AddHandler(ResizeStartedEvent, value);
            remove => RemoveHandler(ResizeStartedEvent, value);
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty HeaderBrushProperty = Node.HeaderBrushProperty.AddOwner(typeof(GroupingNode));
        public static readonly DependencyProperty CanResizeProperty = DependencyProperty.Register(nameof(CanResize), typeof(bool), typeof(GroupingNode), new FrameworkPropertyMetadata(BoxValue.True));
        public static readonly DependencyProperty ActualSizeProperty = DependencyProperty.Register(nameof(ActualSize), typeof(Size), typeof(GroupingNode), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnActualSizeChanged));
        public static readonly DependencyProperty MovementModeProperty = DependencyProperty.Register(nameof(MovementMode), typeof(GroupingMovementMode), typeof(GroupingNode), new FrameworkPropertyMetadata(GroupMovementBoxed));
        public static readonly DependencyProperty ResizeCompletedCommandProperty = DependencyProperty.Register(nameof(ResizeCompletedCommand), typeof(ICommand), typeof(GroupingNode));
        public static readonly DependencyProperty ResizeStartedCommandProperty = DependencyProperty.Register(nameof(ResizeStartedCommand), typeof(ICommand), typeof(GroupingNode));

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

        /// <summary>
        /// Invoked when the <see cref="ResizeCompleted"/> event is not handled.
        /// Parameter is the <see cref="ItemContainer.ActualSize"/> of the container.
        /// </summary>
        public ICommand? ResizeCompletedCommand
        {
            get => (ICommand?)GetValue(ResizeCompletedCommandProperty);
            set => SetValue(ResizeCompletedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when the <see cref="ResizeStarted"/> event is not handled.
        /// Parameter is the <see cref="ItemContainer.ActualSize"/> of the container.
        /// </summary>
        public ICommand? ResizeStartedCommand
        {
            get => (ICommand?)GetValue(ResizeStartedCommandProperty);
            set => SetValue(ResizeStartedCommandProperty, value);
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
        /// Gets the <see cref="System.Windows.Controls.ContentControl"/> control of this <see cref="GroupingNode"/>.
        /// </summary>
        protected FrameworkElement? ContentControl;

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
            AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(OnResizeStarted));

            Loaded += OnNodeLoaded;
            Unloaded += OnNodeUnloaded;
        }

        private void OnNodeLoaded(object sender, RoutedEventArgs e)
        {
            if (HeaderControl != null)
            {
                HeaderControl.MouseDown += OnHeaderMouseDown;
                HeaderControl.SizeChanged += OnHeaderSizeChanged;
                CalculateDesiredHeaderSize();
            }
        }

        private void OnNodeUnloaded(object sender, RoutedEventArgs e)
        {
            if (HeaderControl != null)
            {
                HeaderControl.MouseDown -= OnHeaderMouseDown;
                HeaderControl.SizeChanged -= OnHeaderSizeChanged;
            }
        }

        private void OnHeaderMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;
            if (Container != null && Editor != null && gestures.Drag.Matches(e.Source, e))
            {
                // Switch the default movement mode if necessary
                var prevMovementMode = MovementMode;
                if (Keyboard.Modifiers == EditorGestures.Mappings.GroupingNode.SwitchMovementMode)
                {
                    MovementMode = MovementMode == GroupingMovementMode.Group ? GroupingMovementMode.Self : GroupingMovementMode.Group;
                }

                // Select the content and move with it
                if (gestures.Selection.Append.Matches(e.Source, e))
                {
                    Editor.SelectArea(new Rect(Container.Location, RenderSize), append: true, fit: true);
                }
                else if (gestures.Selection.Remove.Matches(e.Source, e))
                {
                    Editor.UnselectArea(new Rect(Container.Location, RenderSize), fit: true);
                }
                else if (gestures.Selection.Invert.Matches(e.Source, e))
                {
                    if (Container.IsSelected)
                    {
                        Editor.UnselectArea(new Rect(Container.Location, RenderSize), fit: true);
                        Container.IsSelected = true;
                    }
                    else
                    {
                        Editor.SelectArea(new Rect(Container.Location, RenderSize), append: true, fit: true);
                    }
                }
                else if (gestures.Selection.Replace.Matches(e.Source, e) || EditorGestures.Mappings.ItemContainer.Drag.Matches(e.Source, e))
                {
                    Editor.SelectArea(new Rect(Container.Location, RenderSize), append: Container.IsSelected, fit: true);
                }

                // Deselect content
                if (MovementMode == GroupingMovementMode.Self)
                {
                    Editor.UnselectArea(new Rect(Container.Location, RenderSize), fit: true);
                    Container.IsSelected = true;
                }

                // Switch the default movement mode back
                MovementMode = prevMovementMode;
            }
        }

        /// <inheritdoc />
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
            if (CanResize && ReferenceEquals(e.OriginalSource, ResizeThumb))
            {
                double resultWidth = ActualWidth + e.HorizontalChange;
                double resultHeight = ActualHeight + e.VerticalChange;

                // Snap to grid
                if (Editor != null)
                {
                    uint cellSize = Editor.GridCellSize;
                    resultWidth = (int)resultWidth / cellSize * cellSize;
                    resultHeight = (int)resultHeight / cellSize * cellSize;
                }

                Width = Math.Max(_minWidth, resultWidth);
                Height = Math.Max(_minHeight, resultHeight);

                e.Handled = true;
            }
        }

        private void OnResizeStarted(object sender, DragStartedEventArgs e)
        {
            ActualSize = new Size(ActualWidth, ActualHeight);
            var args = new ResizeEventArgs(ActualSize, ActualSize)
            {
                RoutedEvent = ResizeStartedEvent,
                Source = this
            };

            RaiseEvent(args);

            // Raise ResizeStartedCommand if ResizeStartedEvent event is not handled
            if (!args.Handled && (ResizeStartedCommand?.CanExecute(ActualSize) ?? false))
            {
                ResizeStartedCommand.Execute(ActualSize);
            }
        }

        private void OnResizeCompleted(object sender, DragCompletedEventArgs e)
        {
            Size previousSize = ActualSize;
            var newSize = new Size(ActualWidth, ActualHeight);
            ActualSize = newSize;

            var args = new ResizeEventArgs(previousSize, newSize)
            {
                RoutedEvent = ResizeCompletedEvent,
                Source = this
            };

            RaiseEvent(args);

            // Raise ResizeCompletedCommand if ResizeCompletedEvent event is not handled
            if (!args.Handled && (ResizeCompletedCommand?.CanExecute(newSize) ?? false))
            {
                ResizeCompletedCommand.Execute(newSize);
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

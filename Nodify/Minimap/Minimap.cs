using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Nodify
{
    /// <summary>
    /// A minimap control that can position the viewport, and zoom in and out.
    /// </summary>
    [StyleTypedProperty(Property = nameof(ViewportStyle), StyleTargetType = typeof(Rectangle))]
    [StyleTypedProperty(Property = nameof(ItemContainerTheme), StyleTargetType = typeof(MinimapItem))]
    [TemplatePart(Name = ElementItemsHost, Type = typeof(Panel))]
    public partial class Minimap : ItemsControl
    {
        protected const string ElementItemsHost = "PART_ItemsHost";

        public static readonly DirectProperty<Minimap, Point> ViewportLocationProperty = NodifyEditor.ViewportLocationProperty.AddOwner<Minimap>(e => e.ViewportLocation, (e, v) => e.ViewportLocation = v, default, BindingMode.TwoWay);
        public static readonly StyledProperty<Size> ViewportSizeProperty = NodifyEditor.ViewportSizeProperty.AddOwner<Minimap>();
        public static readonly StyledProperty<ControlTheme> ViewportStyleProperty = AvaloniaProperty.Register<Minimap, ControlTheme>(nameof(ViewportStyle));
        public static readonly StyledProperty<Rect> ExtentProperty = NodifyCanvas.ExtentProperty.AddOwner<Minimap>();
        public static readonly StyledProperty<Rect> ItemsExtentProperty = AvaloniaProperty.Register<Minimap, Rect>(nameof(ItemsExtent));
        public static readonly StyledProperty<Size> MaxViewportOffsetProperty = AvaloniaProperty.Register<Minimap, Size>(nameof(MaxViewportOffset), new Size(2000, 2000));
        public static readonly StyledProperty<bool> ResizeToViewportProperty = AvaloniaProperty.Register<Minimap, bool>(nameof(ResizeToViewport));
        public static readonly StyledProperty<bool> IsReadOnlyProperty = TextBox.IsReadOnlyProperty.AddOwner<Minimap>();

        public static readonly RoutedEvent ZoomEvent = RoutedEvent.Register<ZoomEventArgs>(nameof(Zoom), RoutingStrategies.Bubble, typeof(Minimap));

        /// <inheritdoc cref="NodifyEditor.ViewportLocation" />
        public Point ViewportLocation
        {
            get => viewportLocation;
            set => SetAndRaise(ViewportLocationProperty, ref viewportLocation, value);
        }

        /// <inheritdoc cref="NodifyEditor.ViewportSize" />
        public Size ViewportSize
        {
            get => (Size)GetValue(ViewportSizeProperty);
            set => SetValue(ViewportSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the style to use for the viewport rectangle.
        /// </summary>
        public ControlTheme ViewportStyle
        {
            get => (ControlTheme)GetValue(ViewportStyleProperty);
            set => SetValue(ViewportStyleProperty, value);
        }

        /// <summary>The area covered by the items and the viewport rectangle in graph space.</summary>
        public Rect Extent
        {
            get => (Rect)GetValue(ExtentProperty);
            set => SetValue(ExtentProperty, value);
        }

        /// <summary>The area covered by the <see cref="MinimapItem"/>s in graph space.</summary>
        public Rect ItemsExtent
        {
            get => (Rect)GetValue(ItemsExtentProperty);
            set => SetValue(ItemsExtentProperty, value);
        }

        /// <summary>The max position from the <see cref="NodifyEditor.ItemsExtent"/> that the viewport can move to.</summary>
        public Size MaxViewportOffset
        {
            get => (Size)GetValue(MaxViewportOffsetProperty);
            set => SetValue(MaxViewportOffsetProperty, value);
        }

        /// <summary>Whether the minimap should resize to also display the whole viewport.</summary>
        public bool ResizeToViewport
        {
            get => (bool)GetValue(ResizeToViewportProperty);
            set => SetValue(ResizeToViewportProperty, value);
        }

        /// <summary>Whether the minimap can move and zoom the viewport.</summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>Triggered when zooming in or out using the mouse wheel.</summary>
        public event ZoomEventHandler Zoom
        {
            add => AddHandler(ZoomEvent, value);
            remove => RemoveHandler(ZoomEvent, value);
        }

        /// <summary>
        /// Gets the panel that holds all the <see cref="MinimapItem"/>s.
        /// </summary>
        protected internal ItemsPresenter ItemsHost { get; private set; } = default!;

        static Minimap()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Minimap), new FrameworkPropertyMetadata(typeof(Minimap)));
            ClipToBoundsProperty.OverrideMetadata(typeof(Minimap), new StyledPropertyMetadata<bool>(BoxValue.True));
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            ItemsHost = e.NameScope.Get<ItemsPresenter>("PART_ItemsPresenter");
        }

        protected bool IsDragging { get; private set; }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            var gestures = EditorGestures.Mappings.Minimap;
            if (!IsReadOnly && gestures.DragViewport.Matches(this, e))
            {
                e.Pointer.Capture(this);
                this.PropagateMouseCapturedWithin(true);
                IsDragging = true;

                SetViewportLocation(e.GetPosition(ItemsHost));

                e.Handled = true;
            }
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (IsDragging)
            {
                SetViewportLocation(e.GetPosition(ItemsHost));
            }
        }

        private void SetViewportLocation(Point location)
        {
            var position = location - new Vector(ViewportSize.Width / 2, ViewportSize.Height / 2) + (Vector)Extent.Position;

            if (MaxViewportOffset.Width != 0 || MaxViewportOffset.Height != 0)
            {
                double maxRight = ResizeToViewport ? ItemsExtent.Right : Math.Max(ItemsExtent.Right, ItemsExtent.Left + ViewportSize.Width);
                double maxBottom = ResizeToViewport ? ItemsExtent.Bottom : Math.Max(ItemsExtent.Bottom, ItemsExtent.Top + ViewportSize.Height);

                position = new Point(
                             position.X.Clamp(ItemsExtent.Left - ViewportSize.Width / 2 - MaxViewportOffset.Width, maxRight - ViewportSize.Width / 2 + MaxViewportOffset.Width),
                             position.Y.Clamp(ItemsExtent.Top - ViewportSize.Height / 2 - MaxViewportOffset.Height, maxBottom - ViewportSize.Height / 2 + MaxViewportOffset.Height)
                             );
            }

            ViewportLocation = position;
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            var gestures = EditorGestures.Mappings.Minimap;
            if (IsDragging && gestures.DragViewport.Matches(this, e))
            {
                IsDragging = false;
            }

            var props = e.GetCurrentPoint(this).Properties;
            if (/*IsMouseCaptured && */ !props.IsRightButtonPressed && !props.IsLeftButtonPressed && !props.IsMiddleButtonPressed)
            {
                e.Pointer.Capture(null);
                this.PropagateMouseCapturedWithin(false);
            }
        }

        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            if (!IsReadOnly && !e.Handled && EditorGestures.Mappings.Minimap.ZoomModifierKey == e.KeyModifiers)
            {
                double zoom = Math.Pow(2.0, e.Delta.Length / 3.0 / NodifyEditor.MouseWheelDeltaForOneLine);
                var location = ViewportLocation + ViewportSize.ToVector() / 2;

                var args = new ZoomEventArgs(zoom, location)
                {
                    RoutedEvent = ZoomEvent,
                    Source = this
                };
                RaiseEvent(args);

                e.Handled = true;
            }
        }

        protected DependencyObject GetContainerForItemOverride()
            => new MinimapItem();

        protected bool IsItemItsOwnContainerOverride(object item)
            => item is MinimapItem;
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Nodify
{
    /// <summary>
    /// A minimap control that can position the viewport, and zoom in and out.
    /// </summary>
    [StyleTypedProperty(Property = nameof(ViewportStyle), StyleTargetType = typeof(Rectangle))]
    [StyleTypedProperty(Property = nameof(ItemContainerStyle), StyleTargetType = typeof(MinimapItem))]
    [TemplatePart(Name = ElementItemsHost, Type = typeof(Panel))]
    public class Minimap : ItemsControl
    {
        protected const string ElementItemsHost = "PART_ItemsHost";

        public static readonly DependencyProperty ViewportLocationProperty = NodifyEditor.ViewportLocationProperty.AddOwner(typeof(Minimap), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ViewportSizeProperty = NodifyEditor.ViewportSizeProperty.AddOwner(typeof(Minimap));
        public static readonly DependencyProperty ViewportStyleProperty = DependencyProperty.Register(nameof(ViewportStyle), typeof(Style), typeof(Minimap));
        public static readonly DependencyProperty ExtentProperty = NodifyCanvas.ExtentProperty.AddOwner(typeof(Minimap));

        public static readonly RoutedEvent ZoomEvent = EventManager.RegisterRoutedEvent(nameof(Zoom), RoutingStrategy.Bubble, typeof(ZoomEventHandler), typeof(Minimap));

        /// <inheritdoc cref="NodifyEditor.ViewportLocation" />
        public Point ViewportLocation
        {
            get => (Point)GetValue(ViewportLocationProperty);
            set => SetValue(ViewportLocationProperty, value);
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
        public Style ViewportStyle
        {
            get => (Style)GetValue(ViewportStyleProperty);
            set => SetValue(ViewportStyleProperty, value);
        }

        /// <summary>The area covered by the items and the viewport rectangle in graph space.</summary>
        public Rect Extent
        {
            get => (Rect)GetValue(ExtentProperty);
            set => SetValue(ExtentProperty, value);
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
        protected internal Panel ItemsHost { get; private set; } = default!;

        static Minimap()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Minimap), new FrameworkPropertyMetadata(typeof(Minimap)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsHost = GetTemplateChild(ElementItemsHost) as Panel ?? throw new InvalidOperationException($"{ElementItemsHost} is missing or is not of type Panel.");
        }

        protected bool IsDragging { get; private set; }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var gestures = EditorGestures.Mappings.Minimap;
            if (gestures.DragViewport.Matches(this, e))
            {
                this.CaptureMouseSafe();
                IsDragging = true;

                var position = e.GetPosition(ItemsHost);
                ViewportLocation = position - new Vector(ViewportSize.Width / 2, ViewportSize.Height / 2) + (Vector)Extent.Location;

                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsDragging)
            {
                var position = e.GetPosition(ItemsHost);
                ViewportLocation = position - new Vector(ViewportSize.Width / 2, ViewportSize.Height / 2) + (Vector)Extent.Location;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            var gestures = EditorGestures.Mappings.Minimap;
            if (IsDragging && gestures.DragViewport.Matches(this, e))
            {
                IsDragging = false;
            }

            if (IsMouseCaptured && e.RightButton == MouseButtonState.Released && e.LeftButton == MouseButtonState.Released && e.MiddleButton == MouseButtonState.Released)
            {
                ReleaseMouseCapture();
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!e.Handled && EditorGestures.Mappings.Minimap.ZoomModifierKey == Keyboard.Modifiers)
            {
                double zoom = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
                var location = ViewportLocation + (Vector)ViewportSize / 2;

                var args = new ZoomEventArgs(zoom, location)
                {
                    RoutedEvent = ZoomEvent,
                    Source = this
                };
                RaiseEvent(args);

                e.Handled = true;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
            => new MinimapItem();

        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is MinimapItem;
    }
}

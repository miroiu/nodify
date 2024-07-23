using System;
using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    internal partial class MinimapPanel : Panel
    {
        public static readonly DirectProperty<MinimapPanel, Point> ViewportLocationProperty = NodifyEditor.ViewportLocationProperty.AddOwner<MinimapPanel>(e => e.ViewportLocation, (e, v) => e.ViewportLocation = v);
        public static readonly StyledProperty<Size> ViewportSizeProperty = NodifyEditor.ViewportSizeProperty.AddOwner<MinimapPanel>();
        public static readonly StyledProperty<Rect> ExtentProperty = NodifyCanvas.ExtentProperty.AddOwner<MinimapPanel>();
        public static readonly StyledProperty<Rect> ItemsExtentProperty = Minimap.ItemsExtentProperty.AddOwner<MinimapPanel>();
        public static readonly StyledProperty<bool> ResizeToViewportProperty = Minimap.ResizeToViewportProperty.AddOwner<MinimapPanel>();

        /// <inheritdoc cref="Minimap.ViewportLocation" />
        public Point ViewportLocation
        {
            get => viewportLocation;
            set => SetAndRaise(ViewportLocationProperty, ref viewportLocation, value);
        }

        /// <inheritdoc cref="Minimap.ViewportSize" />
        public Size ViewportSize
        {
            get => (Size)GetValue(ViewportSizeProperty);
            set => SetValue(ViewportSizeProperty, value);
        }

        /// <inheritdoc cref="Minimap.Extent" />
        public Rect Extent
        {
            get => (Rect)GetValue(ExtentProperty);
            set => SetValue(ExtentProperty, value);
        }

        /// <inheritdoc cref="Minimap.Extent" />
        public Rect ItemsExtent
        {
            get => (Rect)GetValue(ItemsExtentProperty);
            set => SetValue(ItemsExtentProperty, value);
        }

        /// <inheritdoc cref="Minimap.ResizeToViewport" />
        public bool ResizeToViewport
        {
            get => (bool)GetValue(ResizeToViewportProperty);
            set => SetValue(ResizeToViewportProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double minX = double.MaxValue;
            double minY = double.MaxValue;

            double maxX = double.MinValue;
            double maxY = double.MinValue;

            UIElementCollection children = Children;
            for (int i = 0; i < children.Count; i++)
            {
                var item = (MinimapItem)children[i];
                item.Measure(availableSize);

                Size size = item.DesiredSize;

                if (item.Location.X < minX)
                {
                    minX = item.Location.X;
                }

                if (item.Location.Y < minY)
                {
                    minY = item.Location.Y;
                }

                double sizeX = item.Location.X + size.Width;
                if (sizeX > maxX)
                {
                    maxX = sizeX;
                }

                double sizeY = item.Location.Y + size.Height;
                if (sizeY > maxY)
                {
                    maxY = sizeY;
                }
            }

            var itemsExtent = minX == double.MaxValue
                ? new Rect(0, 0, 0, 0)
                : new Rect(minX, minY, maxX - minX, maxY - minY);

            SetCurrentValue(ItemsExtentProperty, itemsExtent);

            if (ResizeToViewport)
            {
                itemsExtent.Union(new Rect(ViewportLocation, ViewportSize));
            }

            SetCurrentValue(ExtentProperty, itemsExtent);

            double width = Math.Max(itemsExtent.Size.Width, ViewportSize.Width);
            double height = Math.Max(itemsExtent.Height, ViewportSize.Height);
            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElementCollection children = Children;
            for (int i = 0; i < children.Count; i++)
            {
                var item = (MinimapItem)children[i];
                item.Arrange(new Rect(item.Location - (Vector)Extent.Position, item.DesiredSize));
            }

            return finalSize;
        }
    }
}

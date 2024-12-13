using System;
using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    internal sealed class MinimapPanel : Panel
    {
        public static readonly DependencyProperty ViewportLocationProperty = NodifyEditor.ViewportLocationProperty.AddOwner(typeof(MinimapPanel), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ViewportSizeProperty = NodifyEditor.ViewportSizeProperty.AddOwner(typeof(MinimapPanel), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ExtentProperty = NodifyCanvas.ExtentProperty.AddOwner(typeof(MinimapPanel));
        public static readonly DependencyProperty ItemsExtentProperty = Minimap.ItemsExtentProperty.AddOwner(typeof(MinimapPanel));
        public static readonly DependencyProperty ResizeToViewportProperty = Minimap.ResizeToViewportProperty.AddOwner(typeof(MinimapPanel));

        /// <inheritdoc cref="Minimap.ViewportLocation" />
        public Point ViewportLocation
        {
            get => (Point)GetValue(ViewportLocationProperty);
            set => SetValue(ViewportLocationProperty, value);
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

            UIElementCollection children = InternalChildren;
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

            ItemsExtent = itemsExtent;

            if (ResizeToViewport)
            {
                itemsExtent.Union(new Rect(ViewportLocation, ViewportSize));
            }

            Extent = itemsExtent;

            double width = Math.Max(itemsExtent.Size.Width, ViewportSize.Width);
            double height = Math.Max(itemsExtent.Height, ViewportSize.Height);
            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElementCollection children = InternalChildren;
            for (int i = 0; i < children.Count; i++)
            {
                var item = (MinimapItem)children[i];
                item.Arrange(new Rect(item.Location - (Vector)Extent.Location, item.DesiredSize));
            }

            return finalSize;
        }
    }
}

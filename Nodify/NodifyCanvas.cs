using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>Interface for items inside a <see cref="NodifyCanvas"/>.</summary>
    public interface INodifyCanvasItem
    {
        /// <summary>The location of the item.</summary>
        Point Location { get; }

        /// <summary>The desired size of the item.</summary>
        Size DesiredSize { get; }

        /// <inheritdoc cref="UIElement.Arrange(Rect)" />
        void Arrange(Rect rect);
    }

    /// <summary>A canvas like panel that works with <see cref="INodifyCanvasItem"/>s.</summary>
    public class NodifyCanvas : Panel
    {
        public static readonly StyledProperty<Rect> ExtentProperty = AvaloniaProperty.Register<NodifyCanvas, Rect>(nameof(Extent), BoxValue.Rect);

        /// <summary>The area covered by the children of this panel.</summary>
        public Rect Extent
        {
            get => (Rect)GetValue(ExtentProperty);
            set => SetValue(ExtentProperty, value);
        }

        /// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double minX = double.MaxValue;
            double minY = double.MaxValue;

            double maxX = double.MinValue;
            double maxY = double.MinValue;

            UIElementCollection children = Children;
            for (int i = 0; i < children.Count; i++)
            {
                var item = (INodifyCanvasItem)children[i];
                item.Arrange(new Rect(item.Location, item.DesiredSize));

                Size size = children[i].DesiredSize;

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

            SetCurrentValue(ExtentProperty, minX == double.MaxValue
                ? new Rect(0, 0, 0, 0)
                : new Rect(minX, minY, maxX - minX, maxY - minY));

            return arrangeSize;
        }

        /// <inheritdoc />
        protected override Size MeasureOverride(Size constraint)
        {
            var availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            UIElementCollection children = Children;

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Measure(availableSize);
            }

            return default;
        }

        static NodifyCanvas()
        {
            AffectsParentArrange<NodifyCanvas>(DecoratorContainer.LocationProperty);
            AffectsParentArrange<NodifyCanvas>(DecoratorContainer.ActualSizeProperty);
        }
    }
}

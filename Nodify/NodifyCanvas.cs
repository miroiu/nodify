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

    /// <summary>The base </summary>
    public class NodifyCanvas : Panel
    {
        /// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                var internalChild = (INodifyCanvasItem)InternalChildren[i];
                internalChild.Arrange(new Rect(internalChild.Location, internalChild.DesiredSize));
            }

            return arrangeSize;
        }

        /// <inheritdoc />
        protected override Size MeasureOverride(Size constraint)
        {
            var availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Measure(availableSize);
            }

            return default;
        }
    }
}

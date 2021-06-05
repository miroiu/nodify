using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class NodifyCanvas : Panel
    {
        static NodifyCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodifyCanvas), new FrameworkPropertyMetadata(typeof(NodifyCanvas)));
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            for (var i = 0; i < InternalChildren.Count; i++)
            {
                var internalChild = (ItemContainer)InternalChildren[i];
                internalChild.Arrange(new Rect(internalChild.Location, internalChild.DesiredSize));
            }

            return arrangeSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

            for (var i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Measure(availableSize);
            }

            return default;
        }
    }
}

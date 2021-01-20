using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class NodifyCanvas : Panel
    {
        private readonly Size _availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

        static NodifyCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodifyCanvas), new FrameworkPropertyMetadata(typeof(NodifyCanvas)));
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                var internalChild = (ItemContainer)InternalChildren[i];
                internalChild.Arrange(new Rect(internalChild.Location, internalChild.DesiredSize));
            }

            return arrangeSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Measure(_availableSize);
            }

            return default;
        }
    }
}

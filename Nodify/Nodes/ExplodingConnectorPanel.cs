using System;
using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>
    /// Stacks children vertically when expanded, or overlaps them on a shared baseline for compact "hub" layout.
    /// </summary>
    public class ExplodingConnectorPanel : Panel
    {
        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register(
            nameof(IsCollapsed),
            typeof(bool),
            typeof(ExplodingConnectorPanel),
            new FrameworkPropertyMetadata(BoxValue.False, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// When true, output side aligns overlapping children to the panel's right edge; inputs align to the left.
        /// </summary>
        public static readonly DependencyProperty IsRightAlignedProperty = DependencyProperty.Register(
            nameof(IsRightAligned),
            typeof(bool),
            typeof(ExplodingConnectorPanel),
            new FrameworkPropertyMetadata(BoxValue.False, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty VerticalSpacingProperty = DependencyProperty.Register(
            nameof(VerticalSpacing),
            typeof(double),
            typeof(ExplodingConnectorPanel),
            new FrameworkPropertyMetadata(4d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool IsCollapsed
        {
            get => (bool)GetValue(IsCollapsedProperty);
            set => SetValue(IsCollapsedProperty, value);
        }

        public bool IsRightAligned
        {
            get => (bool)GetValue(IsRightAlignedProperty);
            set => SetValue(IsRightAlignedProperty, value);
        }

        public double VerticalSpacing
        {
            get => (double)GetValue(VerticalSpacingProperty);
            set => SetValue(VerticalSpacingProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (InternalChildren.Count == 0)
            {
                return new Size(0, 0);
            }

            double maxWidth = 0;
            double totalHeight = 0;

            foreach (UIElement? child in InternalChildren)
            {
                if (child == null)
                {
                    continue;
                }

                child.Measure(availableSize);
                maxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                totalHeight += child.DesiredSize.Height;
            }

            if (IsCollapsed)
            {
                double maxHeight = 0;
                foreach (UIElement? child in InternalChildren)
                {
                    if (child == null)
                    {
                        continue;
                    }

                    maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);
                }

                return new Size(maxWidth, maxHeight);
            }

            var spacing = VerticalSpacing;
            var stackHeight = totalHeight + Math.Max(0, InternalChildren.Count - 1) * spacing;
            return new Size(maxWidth, stackHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (InternalChildren.Count == 0)
            {
                return finalSize;
            }

            if (IsCollapsed)
            {
                foreach (UIElement? child in InternalChildren)
                {
                    if (child == null)
                    {
                        continue;
                    }

                    var h = child.DesiredSize.Height;
                    var w = child.DesiredSize.Width;
                    var y = (finalSize.Height - h) / 2;
                    var x = IsRightAligned ? finalSize.Width - w : 0;
                    child.Arrange(new Rect(x, y, w, h));
                }
            }
            else
            {
                var y = 0d;
                var spacing = VerticalSpacing;

                foreach (UIElement? child in InternalChildren)
                {
                    if (child == null)
                    {
                        continue;
                    }

                    var h = child.DesiredSize.Height;
                    var w = child.DesiredSize.Width;
                    var x = IsRightAligned ? finalSize.Width - w : 0;
                    child.Arrange(new Rect(x, y, w, h));
                    y += h + spacing;
                }
            }

            return finalSize;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a line that has an arrow indicating its <see cref="BaseConnection.Direction"/>.
    /// </summary>
    public class LineConnection : BaseConnection
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(LineConnection), new FrameworkPropertyMetadata(BoxValue.Double5, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The radius of the corners between the line segments.
        /// </summary>
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        static LineConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineConnection), new FrameworkPropertyMetadata(typeof(LineConnection)));
            NodifyEditor.CuttingConnectionTypes.Add(typeof(LineConnection));
        }

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1) = GetLinePoints(source, target);

            context.BeginFigure(source, false, false);
            if (CornerRadius > 0 && Spacing > 0)
            {
                AddSmoothCorner(context, source, p0, p1, CornerRadius);
                AddSmoothCorner(context, p0, p1, target, CornerRadius);
            }
            else
            {
                context.LineTo(p0, true, true);
                context.LineTo(p1, true, true);
            }
            context.LineTo(target, true, true);

            return ((target, source), (source, target));
        }

        protected override void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, Orientation orientation = Orientation.Horizontal)
        {
            if (Spacing < 1d)
            {
                Vector delta = source - target;
                double headWidth = ArrowSize.Width;
                double headHeight = ArrowSize.Height / 2;

                double angle = Math.Atan2(delta.Y, delta.X);
                double sinT = Math.Sin(angle);
                double cosT = Math.Cos(angle);

                var from = new Point(target.X + (headWidth * cosT - headHeight * sinT), target.Y + (headWidth * sinT + headHeight * cosT));
                var to = new Point(target.X + (headWidth * cosT + headHeight * sinT), target.Y - (headHeight * cosT - headWidth * sinT));

                context.BeginFigure(target, true, true);
                context.LineTo(from, true, true);
                context.LineTo(to, true, true);
            }
            else
            {
                base.DrawDefaultArrowhead(context, source, target, arrowDirection, orientation);
            }
        }

        protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1) = GetLinePoints(source, target);
            var direction = p0 - p1;

            double spacing = 1d / (DirectionalArrowsCount + 1);
            for (int i = 1; i <= DirectionalArrowsCount; i++)
            {
                double t = (spacing * i + DirectionalArrowsOffset).WrapToRange(0d, 1d);
                var to = InterpolateLineSegment(p0, p1, t);

                DrawDirectionalArrowheadGeometry(context, direction, to);
            }
        }

        private (Point P0, Point P1) GetLinePoints(Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var spacingVertical = new Vector(spacing.Y, spacing.X);

            var p0 = source + (SourceOrientation == Orientation.Vertical ? spacingVertical : spacing);
            var p1 = target - (TargetOrientation == Orientation.Vertical ? spacingVertical : spacing);

            return (p0, p1);
        }

        protected static Point InterpolateLineSegment(Point p0, Point p1, double t)
        {
            return (Point)((1 - t) * (Vector)p0 + t * (Vector)p1);
        }

        protected static ((Point SegmentStart, Point SegmentEnd), Point InterpolatedPoint) InterpolateLine(Point p0, Point p1, Point p2, Point p3, double t)
        {
            double length1 = (p1 - p0).Length;
            double length2 = (p2 - p1).Length;
            double length3 = (p3 - p2).Length;
            double totalLength = length1 + length2 + length3;

            double ratio1 = length1 / totalLength;
            double ratio2 = length2 / totalLength;
            double ratio3 = length3 / totalLength;

            // Interpolate within the appropriate segment based on t
            if (t <= ratio1)
            {
                return ((p0, p1), InterpolateLineSegment(p0, p1, t / ratio1));
            }
            else if (t <= ratio1 + ratio2)
            {
                return ((p1, p2), InterpolateLineSegment(p1, p2, (t - ratio1) / ratio2));
            }

            return ((p2, p3), InterpolateLineSegment(p2, p3, (t - ratio1 - ratio2) / ratio3));
        }

        protected static ((Point SegmentStart, Point SegmentEnd), Point InterpolatedPoint) InterpolateLine(Point p0, Point p1, Point p2, double t)
        {
            double length1 = (p1 - p0).Length;
            double length2 = (p2 - p1).Length;
            double totalLength = length1 + length2;

            double ratio1 = length1 / totalLength;
            double ratio2 = length2 / totalLength;

            // Interpolate within the appropriate segment based on t
            if (t <= ratio1)
            {
                return ((p0, p1), InterpolateLineSegment(p0, p1, t / ratio1));
            }

            return ((p1, p2), InterpolateLineSegment(p1, p2, (t - ratio1) / ratio2));
        }

        protected static void AddSmoothCorner(StreamGeometryContext context, Point start, Point corner, Point end, double radius)
        {
            double distAB = (corner - start).LengthSquared;
            double distBC = (end - corner).LengthSquared;

            double bendSize = Math.Sqrt(Math.Min(distAB, distBC)) / 2;
            radius = Math.Min(bendSize, radius);

            Vector directionToCorner = corner - start;
            Vector directionFromCorner = end - corner;

            if (directionToCorner.LengthSquared != 0)
                directionToCorner.Normalize();

            if (directionFromCorner.LengthSquared != 0)
                directionFromCorner.Normalize();

            Point curveStart = corner - directionToCorner * radius;
            Point curveEnd = corner + directionFromCorner * radius;

            context.LineTo(curveStart, true, true);
            context.QuadraticBezierTo(corner, curveEnd, true, true);
        }
    }
}

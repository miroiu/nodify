using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a cubic bezier curve.
    /// </summary>
    public class Connection : BaseConnection
    {
        static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection), new FrameworkPropertyMetadata(typeof(Connection)));
            NodifyEditor.CuttingConnectionTypes.Add(typeof(Connection));
        }

        private const double _baseOffset = 100d;
        private const double _offsetGrowthRate = 25d;

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetBezierControlPoints(source, target);

            using var _ = context.BeginFigure(source, false, false);
            context.LineTo(p0, true, true);
            context.BezierTo(p1, p2, p3, true, true);
            context.LineTo(target, true, true);

            return ((target, source), (source, target));
        }

        protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetBezierControlPoints(source, target);

            double spacing = 1d / (DirectionalArrowsCount + 1);
            for (int i = 1; i <= DirectionalArrowsCount; i++)
            {
                double t = (spacing * i + DirectionalArrowsOffset).WrapToRange(0d, 1d);
                var to = InterpolateCubicBezier(p0, p1, p2, p3, t);
                var direction = GetBezierTangent(p0, p1, p2, p3, t);

                base.DrawDirectionalArrowheadGeometry(context, direction, to);
            }
        }

        protected override Point GetTextPosition(FormattedText text, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetBezierControlPoints(source, target);
            var textCenter = new Vector(text.Width / 2, text.Height / 2);
            return InterpolateCubicBezier(p0, p1, p2, p3, 0.5) - textCenter;
        }

        private (Point P0, Point P1, Point P2, Point P3) GetBezierControlPoints(Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var spacingVertical = new Vector(spacing.Y, spacing.X);

            Point startPoint = source + (SourceOrientation == Orientation.Vertical ? spacingVertical : spacing);
            Point endPoint = target - (TargetOrientation == Orientation.Vertical ? spacingVertical : spacing);

            Vector delta = target - source;
            double height = Math.Abs(delta.Y);
            double width = Math.Abs(delta.X);

            // Smooth curve when distance is lower than base offset
            double smooth = Math.Min(_baseOffset, height);
            // Calculate offset based on distance
            double offset = Math.Max(smooth, width / 2d);
            // Grow slowly with distance
            offset = Math.Min(_baseOffset + Math.Sqrt(width * _offsetGrowthRate), offset);

            var controlPoint = new Vector(offset * direction, 0d);
            var controlPointVertical = new Vector(controlPoint.Y, controlPoint.X);

            // Avoid sharp bend if orientation different (when close to each other)
            if (TargetOrientation != SourceOrientation)
            {
                controlPoint *= 0.5;
            }

            Point p0 = startPoint;
            Point p1 = startPoint + (SourceOrientation == Orientation.Vertical ? controlPointVertical : controlPoint);
            Point p2 = endPoint - (TargetOrientation == Orientation.Vertical ? controlPointVertical : controlPoint);
            Point p3 = endPoint;

            return (p0, p1, p2, p3);
        }

        private static Vector GetBezierTangent(Point P0, Point P1, Point P2, Point P3, double t)
        {
            // Calculate the derivatives of the Bezier curve equation and negate the result
            return -(-3 * (1 - t) * (1 - t) * (Vector)P0 +
                    (3 * (1 - t) * (1 - t) * (Vector)P1 - 6 * t * (1 - t) * (Vector)P1) +
                    (6 * t * (1 - t) * (Vector)P2 - 3 * t * t * (Vector)P2) +
                    3 * t * t * (Vector)P3);
        }

        protected static Point InterpolateCubicBezier(Point P0, Point P1, Point P2, Point P3, double t)
        {
            // B = (1 − t)^3 * P0 + 3 * t * (1 − t)^2 * P1 + 3 * t^2 * (1 − t) * P2 + t^3 * P3
            return (Point)
                 ((Vector)P0 * (1 - t) * (1 - t) * (1 - t)
                + (Vector)P1 * 3 * t * (1 - t) * (1 - t)
                + (Vector)P2 * 3 * t * t * (1 - t)
                + (Vector)P3 * t * t * t);
        }
    }
}

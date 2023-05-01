using System;
using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a quadratic curve.
    /// </summary>
    public class Connection : BaseConnection
    {
        static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection), new FrameworkPropertyMetadata(typeof(Connection)));
        }

        // ReSharper disable once InconsistentNaming
        private const double _baseOffset = 100d;
        // ReSharper disable once InconsistentNaming
        private const double _offsetGrowthRate = 25d;

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            Point startPoint = source + spacing;
            Point endPoint = target - spacing;

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

            context.BeginFigure(source, false, false);
            context.LineTo(startPoint, true, true);
            context.BezierTo(startPoint + controlPoint, endPoint - controlPoint, endPoint, true, true);
            context.LineTo(target, true, true);

            return ((target, source), (source, target));
        }
    }
}

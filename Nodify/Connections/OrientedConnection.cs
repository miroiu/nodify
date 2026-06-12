using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a cubic bezier curve with oriented connectors.
    /// </summary>
    public class OrientedConnection : BaseConnection
    {
        /// <summary>
        /// Gets or sets the angle of the connection from the source connector.
        /// </summary>
        public double SourceAngle
        {
            get => (double)GetValue(SourceAngleProperty);
            set => SetValue(SourceAngleProperty, value);
        }
        public static readonly DependencyProperty SourceAngleProperty = DependencyProperty.Register(nameof(SourceAngle), typeof(double), typeof(OrientedConnection), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the angle of the connection from the target connector.
        /// </summary>
        public double TargetAngle
        {
            get => (double)GetValue(TargetAngleProperty);
            set => SetValue(TargetAngleProperty, value);
        }

        public static readonly DependencyProperty TargetAngleProperty = DependencyProperty.Register(nameof(TargetAngle), typeof(double), typeof(OrientedConnection), new FrameworkPropertyMetadata(180d, FrameworkPropertyMetadataOptions.AffectsRender));

        static OrientedConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OrientedConnection), new FrameworkPropertyMetadata(typeof(OrientedConnection)));
            NodifyEditor.CuttingConnectionTypes.Add(typeof(OrientedConnection));
        }

        private const double BaseOffset = 100d;
        private const double OffsetGrowthRate = 25d;
        private const double DegreesToRadians = Math.PI / 180;

        /// <summary>Gets the unit vector indicating connection orientation from the source connector.</summary>
        /// <returns>A unit vector representing the orientation.</returns>
        protected virtual Vector GetSourceOrientation()
        {
            var angle = SourceAngle * DegreesToRadians;
            return new Vector(Math.Cos(angle), -Math.Sin(angle));
        }

        /// <summary>Gets the unit vector indicating connection orientation from the target connector.</summary>
        /// <returns>A unit vector representing the orientation.</returns>
        protected virtual Vector GetTargetOrientation()
        {
            var angle = TargetAngle * DegreesToRadians;
            return new Vector(Math.Cos(angle), -Math.Sin(angle));
        }

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetBezierControlPoints(source, target);

            context.BeginFigure(source, false, false);
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

                DrawDirectionalArrowheadGeometry(context, direction, to);
            }
        }

        protected override void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, Orientation orientation = Orientation.Horizontal)
        {
            var connectionDirection = Direction;
            // Reverse the caller logic to determine which of the source or target arrow we are drawing.
            var x = arrowDirection == connectionDirection ? GetTargetOrientation() : GetSourceOrientation();
            // Reverse the direction if we need to draw a backward connection.
            if (connectionDirection == ConnectionDirection.Backward) x = -x;
            var y = new Vector(x.Y, -x.X);

            var headWidth = ArrowSize.Width * x;
            var headHeight = ArrowSize.Height / 2 * y;
            var headMiddle = target + headWidth;

            var from = headMiddle + headHeight;
            var to = headMiddle - headHeight;

            context.BeginFigure(target, true, true);
            context.LineTo(from, true, true);
            context.LineTo(to, true, true);
        }

        protected override Point GetTextPosition(FormattedText text, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetBezierControlPoints(source, target);
            var textCenter = new Vector(text.Width / 2, text.Height / 2);
            return InterpolateCubicBezier(p0, p1, p2, p3, 0.5) - textCenter;
        }

        private (Point P0, Point P1, Point P2, Point P3) GetBezierControlPoints(Point source, Point target)
        {
            var sourceOrientation = GetSourceOrientation();
            var targetOrientation = GetTargetOrientation();

            Point startPoint = source + Spacing * sourceOrientation;
            Point endPoint = target + Spacing * targetOrientation;

            Vector delta = target - source;
            double height = Math.Abs(delta.Y);
            double width = Math.Abs(delta.X);

            // Smooth curve when distance is lower than base offset
            double smooth = Math.Min(BaseOffset, height);
            // Calculate offset based on distance
            double offset = Math.Max(smooth, width / 2d);
            // Grow slowly with distance
            offset = Math.Min(BaseOffset + Math.Sqrt(width * OffsetGrowthRate), offset);

            var controlPoint = offset;

            // Avoid sharp bend if orientation different (when close to each other)
            if (TargetOrientation != SourceOrientation)
            {
                controlPoint *= 0.5;
            }

            Point p0 = startPoint;
            Point p1 = startPoint + controlPoint * sourceOrientation;
            Point p2 = endPoint + controlPoint * targetOrientation;
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

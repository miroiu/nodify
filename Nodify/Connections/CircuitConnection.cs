using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a line that is controlled by an angle.
    /// </summary>
    public class CircuitConnection : LineConnection
    {
        protected const double Degrees = Math.PI / 180.0d;

        public static readonly StyledProperty<double> AngleProperty = AvaloniaProperty.Register<CircuitConnection, double>(nameof(Angle), BoxValue.Double45);

        /// <summary>
        /// The angle of the connection in degrees.
        /// </summary>
        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        static CircuitConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircuitConnection), new FrameworkPropertyMetadata(typeof(CircuitConnection)));
            NodifyEditor.CuttingConnectionTypes.Add(typeof(CircuitConnection));
            AffectsRender<CircuitConnection>(AngleProperty);
        }

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2) = GetLinePoints(source, target);

            using var _ = context.BeginFigure(source, false, false);
            context.LineTo(p0, true, true);
            context.LineTo(p1, true, true);
            context.LineTo(p2, true, true);
            context.LineTo(target, true, true);

            if (Spacing < 1d)
            {
                return ((p1, source), (p1, target));
            }

            return ((p0, source), (p1, target));
        }

        protected override Point GetTextPosition(FormattedText text, Point source, Point target)
        {
            var (p0, p1, p2) = GetLinePoints(source, target);

            Vector deltaSource = p1 - p0;
            Vector deltaTarget = p2 - p1;

            if (deltaSource.SquaredLength > deltaTarget.SquaredLength)
            {
                return new Point((p0.X + p1.X - text.Width) / 2, (p0.Y + p1.Y - text.Height) / 2);
            }

            return new Point((p2.X + p1.X - text.Width) / 2, (p2.Y + p1.Y - text.Height) / 2);
        }

        protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2) = GetLinePoints(source, target);

            double spacing = 1d / (DirectionalArrowsCount + 1);
            for (int i = 1; i <= DirectionalArrowsCount; i++)
            {
                double t = (spacing * i + DirectionalArrowsOffset).WrapToRange(0d, 1d);
                var (segment, to) = InterpolateLine(p0, p1, p2, t);

                var direction = segment.SegmentStart - segment.SegmentEnd;
                base.DrawDirectionalArrowheadGeometry(context, direction, to);
            }
        }

        private (Point P0, Point P1, Point P2) GetLinePoints(in Point source, in Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var spacingVertical = new Vector(spacing.Y, spacing.X);
            var arrowOffset = new Vector(ArrowSize.Width * direction, 0d);

            if (TargetOrientation == Orientation.Vertical)
            {
                arrowOffset = new Vector(arrowOffset.Y, arrowOffset.X);
            }

            Point endPoint = Spacing > 0 ? target - arrowOffset : target;

            Point p1 = source + (SourceOrientation == Orientation.Vertical ? spacingVertical : spacing);
            Point p3 = endPoint - (TargetOrientation == Orientation.Vertical ? spacingVertical : spacing);
            Point p2 = GetControlPoint(p1, p3);

            return (p1, p2, p3);
        }

        private Point GetControlPoint(in Point source, in Point target)
        {
            Vector delta = target - source;
            double tangent = Math.Tan(Angle * Degrees);

            double dx = Math.Abs(delta.X);
            double dy = Math.Abs(delta.Y);

            double slopeWidth = dy / tangent;
            if (dx > slopeWidth)
            {
                return delta.X > 0d ? new Point(target.X - slopeWidth, source.Y) : new Point(source.X - slopeWidth, target.Y);
            }

            double slopeHeight = dx * tangent;
            if (dy > slopeHeight)
            {
                if (delta.Y > 0d)
                {
                    // handle top left
                    return delta.X < 0d ? new Point(source.X, target.Y - slopeHeight) : new Point(target.X, source.Y + slopeHeight);
                }

                // handle bottom left
                if (delta.X < 0d)
                {
                    return new Point(source.X, target.Y + slopeHeight);
                }
            }

            return new Point(target.X, source.Y - slopeHeight);
        }
    }
}

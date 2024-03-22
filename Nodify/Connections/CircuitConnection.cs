using System;
using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a line that is controlled by an angle.
    /// </summary>
    public class CircuitConnection : LineConnection
    {
        protected const double Degrees = Math.PI / 180.0d;

        public static readonly StyledProperty<double> AngleProperty = AvaloniaProperty.Register<LineConnection, double>(nameof(Angle), BoxValue.Double45);

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
            AffectsRender<CircuitConnection>(AngleProperty);
        }

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p1, p2, p3) = GetLinePoints(source, target);

            using var _ = context.BeginFigure(source, false, false);
            context.LineTo(p1, true, true);
            context.LineTo(p2, true, true);
            context.LineTo(p3, true, true);
            context.LineTo(target, true, true);

            if (Spacing < 1d)
            {
                return ((p2, source), (p2, target));
            }

            return ((p1, source), (p2, target));
        }

        private (Point P1, Point P2, Point P3) GetLinePoints(in Point source, in Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var arrowOffset = new Vector(ArrowSize.Width * direction, 0d);
            Point endPoint = Spacing > 0 ? target - arrowOffset : target;

            Point p1 = source + spacing;
            Point p3 = endPoint - spacing;
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

        protected override Point GetTextPosition(FormattedText text)
        {
            (Vector sourceOffset, Vector targetOffset) = GetOffset();
            var (p1, p2, p3) = GetLinePoints(Source + sourceOffset, Target + targetOffset);

            Vector deltaSource = p1 - p2;
            Vector deltaTarget = p3 - p2;

            if (deltaSource.SquaredLength > deltaTarget.SquaredLength)
            {
                return new Point((p1.X + p2.X - text.Width) / 2, (p1.Y + p2.Y - text.Height) / 2);
            }

            return new Point((p3.X + p2.X - text.Width) / 2, (p3.Y + p2.Y - text.Height) / 2);
        }
    }
}

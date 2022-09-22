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

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof(Angle), typeof(double), typeof(LineConnection), new FrameworkPropertyMetadata(BoxValue.Double45, FrameworkPropertyMetadataOptions.AffectsRender));

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
        }

        protected override (Point ArrowSource, Point ArrowTarget) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var arrowOffset = new Vector(ArrowSize.Width * direction, 0d);
            Point endPoint = Spacing > 0 ? target - arrowOffset : target;

            Point p1 = source + spacing;
            Point p3 = endPoint - spacing;
            Point p2 = GetControlPoint(p1, p3);

            context.BeginFigure(source, false, false);
            context.LineTo(p1, true, true);
            context.LineTo(p2, true, true);
            context.LineTo(p3, true, true);
            context.LineTo(target, true, true);

            return (p2, target);
        }

        private Point GetControlPoint(Point source, Point target)
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

using System;
using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a line that is controlled by an angle.
    /// </summary>
    public class CircuitConnection : DirectionalConnection
    {
        protected const double Degrees = Math.PI / 180.0d;

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(nameof(Angle), typeof(double), typeof(DirectionalConnection), new FrameworkPropertyMetadata(45d, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(DirectionalConnection), new FrameworkPropertyMetadata(30d, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The angle of the connection.
        /// </summary>
        public double Angle
        {
            get => (double)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }

        /// <summary>
        /// The distance between the start point and the where the angle breaks.
        /// </summary>
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        static CircuitConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircuitConnection), new FrameworkPropertyMetadata(typeof(CircuitConnection)));
        }

        protected override void DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var p1 = new Point(source.X + Spacing, source.Y);
            var p3 = new Point(target.X - Spacing, target.Y);
            Point p2 = GetControlPoint(p1, p3);

            context.BeginFigure(source, false, false);
            context.LineTo(p1, true, true);
            context.LineTo(p2, true, true);
            context.LineTo(p3, true, true);
            context.LineTo(target, true, true);

            if (Direction == ConnectionDirection.Forward)
            {
                DrawArrowGeometry(context, source, target);
            }
            else
            {
                DrawArrowGeometry(context, target, source);
            }
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
                return delta.X > 0 ? new Point(target.X - slopeWidth, source.Y) : new Point(source.X - slopeWidth, target.Y);
            }

            double slopeHeight = dx * tangent;
            if (dy > slopeHeight)
            {
                if (delta.Y > 0)
                {
                    // handle top left
                    return delta.X < 0 ? new Point(source.X, target.Y - slopeHeight) : new Point(target.X, source.Y + slopeHeight);
                }

                // handle bottom left
                if (delta.X < 0)
                {
                    return new Point(source.X, target.Y + slopeHeight);
                }
            }

            return new Point(target.X, source.Y - slopeHeight);
        }

        protected override void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target)
        {
            double headWidth = ArrowSize.Width;
            double headHeight = ArrowSize.Height / 2.0;

            var from = new Point(target.X - headWidth, target.Y + headHeight);
            var to = new Point(target.X - headWidth, target.Y - headHeight);

            context.BeginFigure(target, true, true);
            context.LineTo(from, true, false);
            context.LineTo(to, true, false);
        }
    }
}

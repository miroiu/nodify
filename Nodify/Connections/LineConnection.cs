using System;
using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a line that has an arrow indicating its <see cref="BaseConnection.Direction"/>.
    /// </summary>
    public class LineConnection : BaseConnection
    {
        static LineConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineConnection), new FrameworkPropertyMetadata(typeof(LineConnection)));
        }

        protected override (Point ArrowSource, Point ArrowTarget) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var arrowOffset = new Vector(ArrowSize.Width * direction, 0d);
            Point endPoint = Spacing > 0 ? target - arrowOffset : target;

            context.BeginFigure(source, false, false);
            context.LineTo(source + spacing, true, true);
            context.LineTo(endPoint - spacing, true, true);
            context.LineTo(endPoint, true, true);

            return (source, target);
        }

        protected override (Point From, Point To) GetArrowHeadPoints(Point source, Point target)
        {
            if(Spacing < 1d)
            {
                Vector delta = source - target;
                double headWidth = ArrowSize.Width;
                double headHeight = ArrowSize.Height;

                double angle = Math.Atan2(delta.Y, delta.X);
                double sinT = Math.Sin(angle);
                double cosT = Math.Cos(angle);

                var from = new Point(target.X + (headWidth * cosT - headHeight * sinT), target.Y + (headWidth * sinT + headHeight * cosT));
                var to = new Point(target.X + (headWidth * cosT + headHeight * sinT), target.Y - (headHeight * cosT - headWidth * sinT));
                return (from, to);
            }

            return base.GetArrowHeadPoints(source, target);
        }
    }
}

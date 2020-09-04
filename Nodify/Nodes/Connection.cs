using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    public class Connection : BaseConnection
    {
        static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection), new FrameworkPropertyMetadata(typeof(Connection)));
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                var (sourceOffset, targetOffset) = GetOffset();

                var source = Source + sourceOffset;
                var target = Target + targetOffset;

                double width = target.X - source.X;
                double height = target.Y - source.Y;

                Point p2 = new Point(source.X + (width / 4d), source.Y);
                Point p3 = new Point(source.X + (width / 2d), source.Y + (height / 2d));
                Point p4 = new Point(source.X + (3d * width / 4d), target.Y);

                var result = new PathGeometry
                {
                    Figures = new PathFigureCollection
                    {
                        new PathFigure
                        {
                            StartPoint = source,
                            IsClosed = false,
                            Segments =
                            {
                                new BezierSegment(source, p2, p3, true),
                                new BezierSegment(p3, p4, target, true)
                            }
                        }
                    }
                };

                result.Freeze();
                return result;
            }
        }
    }
}

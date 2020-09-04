using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Nodify
{
    public class Connection : Shape
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(Point), typeof(Connection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(nameof(Target), typeof(Point), typeof(Connection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));

        public Point Source
        {
            get => (Point)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Point Target
        {
            get => (Point)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection), new FrameworkPropertyMetadata(typeof(Connection)));
        }

        protected override Geometry DefiningGeometry => Bezier(Source, Target);

        public static PathGeometry Bezier(Point start, Point end)
        {
            double width = end.X - start.X;
            double height = end.Y - start.Y;

            Point p2 = new Point(start.X + (width / 4d), start.Y);
            Point p3 = new Point(start.X + (width / 2d), start.Y + (height / 2d));
            Point p4 = new Point(start.X + (3d * width / 4d), end.Y);

            var result = new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    new PathFigure
                    {
                        StartPoint = start,
                        IsClosed = false,
                        Segments =
                        {
                            new BezierSegment(start, p2, p3, true),
                            new BezierSegment(p3, p4, end, true)
                        }
                    }
                }
            };

            result.Freeze();
            return result;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                CaptureMouse();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == this)
            {
                ReleaseMouseCapture();
            }
        }
    }
}

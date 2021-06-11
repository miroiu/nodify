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

        private readonly BezierSegment _firstSegment = new BezierSegment();
        private readonly PathFigure _figure = new PathFigure() { IsClosed = false };
        private readonly PathGeometry _cachedGeometry = new PathGeometry();

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection()
        {
            _cachedGeometry.Figures.Add(_figure);
            _figure.Segments.Add(_firstSegment);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                (Vector sourceOffset, Vector targetOffset) = GetOffset();

                Point source = Source + sourceOffset;
                Point target = Target + targetOffset;

                double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;

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

                _figure.StartPoint = source;
                _firstSegment.Point1 = source + controlPoint;
                _firstSegment.Point2 = target - controlPoint;
                _firstSegment.Point3 = target;

                return _cachedGeometry;
            }
        }
    }
}

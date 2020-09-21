using System;
using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Represents a bezier curve.
    /// </summary>
    public class Connection : BaseConnection
    {
        static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection), new FrameworkPropertyMetadata(typeof(Connection)));
        }

        private const double _baseOffset = 100d;
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
                var (sourceOffset, targetOffset) = GetOffset();

                var source = Source + sourceOffset;
                var target = Target + targetOffset;

                var direction = Direction == ConnectionDirection.Forward ? 1d : -1d;

                var delta = target - source;
                var height = Math.Abs(delta.Y);
                var width = Math.Abs(delta.X);

                // Smooth curve when distance is lower than base offset
                var smooth = Math.Min(_baseOffset, height);
                // Calculate offset based on distance
                var offset = Math.Max(smooth, width / 2d);
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

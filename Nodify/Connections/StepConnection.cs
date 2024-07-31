using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nodify
{
    public enum ConnectorPosition
    {
        Top,
        Left,
        Bottom,
        Right
    }

    public class StepConnection : LineConnection
    {
        public static readonly AvaloniaProperty<ConnectorPosition> SourcePositionProperty = AvaloniaProperty.Register<StepConnection, ConnectorPosition>(nameof(SourcePosition), ConnectorPosition.Right);
        public static readonly AvaloniaProperty<ConnectorPosition> TargetPositionProperty = AvaloniaProperty.Register<StepConnection, ConnectorPosition>(nameof(TargetPosition), ConnectorPosition.Left);

        private static void OnConnectorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var connection = (StepConnection)d;
            connection.CoerceValue(DirectionProperty);
            connection.CoerceValue(SourceOrientationProperty);
            connection.CoerceValue(TargetOrientationProperty);
        }

        static StepConnection()
        {
            SourcePositionProperty.Changed.AddClassHandler<StepConnection>(OnConnectorPositionChanged);
            TargetPositionProperty.Changed.AddClassHandler<StepConnection>(OnConnectorPositionChanged);
            AffectsRender<StepConnection>(SourcePositionProperty, TargetPositionProperty);
            SourceOrientationProperty.OverrideMetadata<StepConnection>(new StyledPropertyMetadata<Orientation>(defaultValue: Orientation.Horizontal, coerce: CoerceSourceOrientation));
            TargetOrientationProperty.OverrideMetadata<StepConnection>(new StyledPropertyMetadata<Orientation>(defaultValue: Orientation.Horizontal, coerce: CoerceTargetOrientation));
            DirectionProperty.OverrideMetadata<StepConnection>(new StyledPropertyMetadata<ConnectionDirection>(defaultValue: ConnectionDirection.Forward, coerce: CoerceConnectionDirection));
            NodifyEditor.CuttingConnectionTypes.Add(typeof(StepConnection));
        }

        private static Orientation CoerceSourceOrientation(DependencyObject d, Orientation baseValue)
        {
            var connection = (StepConnection)d;
            return connection.SourcePosition == ConnectorPosition.Left || connection.SourcePosition == ConnectorPosition.Right
                ? Orientation.Horizontal
                : Orientation.Vertical;
        }

        private static Orientation CoerceTargetOrientation(DependencyObject d, Orientation baseValue)
        {
            var connection = (StepConnection)d;
            return connection.TargetPosition == ConnectorPosition.Left || connection.TargetPosition == ConnectorPosition.Right
                ? Orientation.Horizontal
                : Orientation.Vertical;
        }

        private static ConnectionDirection CoerceConnectionDirection(DependencyObject d, ConnectionDirection baseValue)
        {
            var connection = (StepConnection)d;
            return connection.TargetPosition == ConnectorPosition.Left || connection.TargetPosition == ConnectorPosition.Top
               ? ConnectionDirection.Forward
               : ConnectionDirection.Backward;
        }

        /// <summary>
        /// Gets or sets the position of the source connector.
        /// </summary>
        public ConnectorPosition SourcePosition
        {
            get => (ConnectorPosition)GetValue(SourcePositionProperty);
            set => SetValue(SourcePositionProperty, value);
        }

        /// <summary>
        /// Gets or sets the position of the target connector.
        /// </summary>
        public ConnectorPosition TargetPosition
        {
            get => (ConnectorPosition)GetValue(TargetPositionProperty);
            set => SetValue(TargetPositionProperty, value);
        }

        protected override ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetLinePoints(source, target);

            context.BeginFigure(source, false, false);
            context.LineTo(p0, true, true);
            context.LineTo(p1, true, true);
            context.LineTo(p2, true, true);
            context.LineTo(p3, true, true);
            context.LineTo(target, true, true);

            if (Spacing < 1d)
            {
                return ((p1, source), (p2, target));
            }

            return ((target, source), (source, target));
        }

        protected override Point GetTextPosition(FormattedText text, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetLinePoints(source, target);

            Vector delta1 = p1 - p0;
            Vector delta2 = p2 - p1;
            Vector delta3 = p3 - p2;

            var max = GetMax(delta1, GetMax(delta2, delta3));

            if (max == delta1)
            {
                return new Point((p0.X + p1.X - text.Width) / 2, (p0.Y + p1.Y - text.Height) / 2);
            }
            else if (max == delta2)
            {
                return new Point((p2.X + p1.X - text.Width) / 2, (p2.Y + p1.Y - text.Height) / 2);
            }

            return new Point((p3.X + p2.X - text.Width) / 2, (p3.Y + p2.Y - text.Height) / 2);

            static Vector GetMax(in Vector a, in Vector b)
                => a.SquaredLength > b.SquaredLength ? a : b;
        }

        protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target)
        {
            var (p0, p1, p2, p3) = GetLinePoints(source, target);

            double spacing = 1d / (DirectionalArrowsCount + 1);
            for (int i = 1; i <= DirectionalArrowsCount; i++)
            {
                double t = (spacing * i + DirectionalArrowsOffset).WrapToRange(0d, 1d);
                var (segment, to) = InterpolateLine(p0, p1, p2, p3, t);

                var direction = segment.SegmentStart - segment.SegmentEnd;
                base.DrawDirectionalArrowheadGeometry(context, direction, to);
            }
        }

        private (Point P0, Point P1, Point P2, Point P3) GetLinePoints(Point source, Point target)
        {
            var sourceDir = GetConnectorDirection(SourcePosition);
            var targetDir = GetConnectorDirection(TargetPosition);

            Point startPoint = source + new Vector(Spacing * sourceDir.X, Spacing * sourceDir.Y);
            Point endPoint = target + new Vector(Spacing * targetDir.X, Spacing * targetDir.Y);

            var connectionDir = GetConnectionDirection(startPoint, SourcePosition, endPoint);
            bool horizontalConnection = connectionDir.X != 0;

            if (IsOppositePosition(SourcePosition, TargetPosition))
            {
                var (p1, p2) = GetOppositePositionPoints();
                return (startPoint, p1, p2, endPoint);
            }

            // same: left to left / top to top etc
            if (SourcePosition == TargetPosition)
            {
                var p = GetSamePositionPoint();
                return (startPoint, p, p, endPoint);
            }

            // mixed: right to bottom / left to top etc
            bool isSameDir = horizontalConnection ? sourceDir.X == targetDir.Y : sourceDir.Y == targetDir.X;
            bool startGreaterThanEnd = horizontalConnection ? startPoint.Y > endPoint.Y : startPoint.X > endPoint.X;

            bool positiveDir = horizontalConnection ? sourceDir.X == 1 : sourceDir.Y == 1;
            bool shouldFlip = positiveDir
                ? isSameDir ? !startGreaterThanEnd : startGreaterThanEnd
                : isSameDir ? startGreaterThanEnd : !startGreaterThanEnd;

            if (shouldFlip)
            {
                var sourceTarget = new Point(startPoint.X, endPoint.Y);
                var targetSource = new Point(endPoint.X, startPoint.Y);

                var pf = horizontalConnection ? sourceTarget : targetSource;
                return (startPoint, pf, pf, endPoint);
            }

            var pp = GetSamePositionPoint();
            return (startPoint, pp, pp, endPoint);

            (Point P1, Point P2) GetOppositePositionPoints()
            {
                var center = startPoint + (endPoint - startPoint) / 2;

                (Point P1, Point P2) verticalSplit = (new Point(center.X, startPoint.Y), new Point(center.X, endPoint.Y));
                (Point P1, Point P2) horizontalSplit = (new Point(startPoint.X, center.Y), new Point(endPoint.X, center.Y));

                if (horizontalConnection)
                {
                    // left to right / right to left
                    return sourceDir.X == connectionDir.X ? verticalSplit : horizontalSplit;
                }

                // top to bottom / bottom to top
                return sourceDir.Y == connectionDir.Y ? horizontalSplit : verticalSplit;
            }

            Point GetSamePositionPoint()
            {
                var sourceTarget = new Point(startPoint.X, endPoint.Y);
                var targetSource = new Point(endPoint.X, startPoint.Y);

                if (horizontalConnection)
                {
                    // left to left / right to right
                    return sourceDir.X == connectionDir.X ? targetSource : sourceTarget;
                }

                // top to top / bottom to bottom
                return sourceDir.Y == connectionDir.Y ? sourceTarget : targetSource;
            }

            static Point GetConnectionDirection(in Point source, ConnectorPosition sourcePosition, in Point target)
            {
                return sourcePosition == ConnectorPosition.Left || sourcePosition == ConnectorPosition.Right
                    ? new Point(Math.Sign(target.X - source.X), 0)
                    : new Point(0, Math.Sign(target.Y - source.Y));
            }

            static Point GetConnectorDirection(ConnectorPosition position)
                => position switch
                {
                    ConnectorPosition.Top => new Point(0, -1),
                    ConnectorPosition.Left => new Point(-1, 0),
                    ConnectorPosition.Bottom => new Point(0, 1),
                    ConnectorPosition.Right => new Point(1, 0),
                    _ => default,
                };

            static bool IsOppositePosition(ConnectorPosition sourcePosition, ConnectorPosition targetPosition)
            {
                return sourcePosition == ConnectorPosition.Left && targetPosition == ConnectorPosition.Right
                    || sourcePosition == ConnectorPosition.Right && targetPosition == ConnectorPosition.Left
                    || sourcePosition == ConnectorPosition.Top && targetPosition == ConnectorPosition.Bottom
                    || sourcePosition == ConnectorPosition.Bottom && targetPosition == ConnectorPosition.Top;
            }
        }
    }
}

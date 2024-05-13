using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Nodify
{
    /// <summary>
    /// Specifies the offset type that can be applied to a <see cref="BaseConnection"/> using the <see cref="BaseConnection.SourceOffset"/> and the <see cref="BaseConnection.TargetOffset"/> values.
    /// </summary>
    public enum ConnectionOffsetMode
    {
        /// <summary>
        /// No offset applied.
        /// </summary>
        None,

        /// <summary>
        /// The offset is applied in a circle around the point.
        /// </summary>
        Circle,

        /// <summary>
        /// The offset is applied in a rectangle shape around the point.
        /// </summary>
        Rectangle,

        /// <summary>
        /// The offset is applied in a rectangle shape around the point, perpendicular to the edges.
        /// </summary>
        Edge,

        /// <summary>
        /// The offset is applied as a fixed margin.
        /// </summary>
        Static
    }

    /// <summary>
    /// The direction in which a connection is oriented.
    /// </summary>
    public enum ConnectionDirection
    {
        /// <summary>
        /// From <see cref="BaseConnection.Source"/> to <see cref="BaseConnection.Target"/>.
        /// </summary>
        Forward,

        /// <summary>
        /// From <see cref="BaseConnection.Target"/> to <see cref="BaseConnection.Source"/>.
        /// </summary>
        Backward
    }

    /// <summary>
    /// The end at which the arrow head is drawn.
    /// </summary>
    public enum ArrowHeadEnds
    {
        /// <summary>
        /// Arrow head at start.
        /// </summary>
        Start,

        /// <summary>
        /// Arrow head at end.
        /// </summary>
        End,

        /// <summary>
        /// Arrow heads at both ends.
        /// </summary>
        Both,

        /// <summary>
        /// No arrow head.
        /// </summary>
        None
    }

    /// <summary>
    /// The shape of the arrowhead.
    /// </summary>
    public enum ArrowHeadShape
    {
        /// <summary>
        /// The default arrowhead.
        /// </summary>
        Arrowhead,

        /// <summary>
        /// An ellipse.
        /// </summary>
        Ellipse,

        /// <summary>
        /// A rectangle.
        /// </summary>
        Rectangle
    }

    /// <summary>
    /// Represents the base class for shapes that are drawn from a <see cref="Source"/> point to a <see cref="Target"/> point.
    /// </summary>
    public abstract partial class BaseConnection : WpfShape
    {
        #region Dependency Properties

        public static readonly StyledProperty<Point> SourceProperty = AvaloniaProperty.Register<BaseConnection, Point>(nameof(Source), BoxValue.Point);
        public static readonly StyledProperty<Point> TargetProperty = AvaloniaProperty.Register<BaseConnection, Point>(nameof(Target), BoxValue.Point);
        public static readonly StyledProperty<Size> SourceOffsetProperty = AvaloniaProperty.Register<BaseConnection, Size>(nameof(SourceOffset), BoxValue.ConnectionOffset);
        public static readonly StyledProperty<Size> TargetOffsetProperty = AvaloniaProperty.Register<BaseConnection, Size>(nameof(TargetOffset), BoxValue.ConnectionOffset);
        public static readonly StyledProperty<ConnectionOffsetMode> SourceOffsetModeProperty = AvaloniaProperty.Register<BaseConnection, ConnectionOffsetMode>(nameof(SourceOffsetMode), ConnectionOffsetMode.Static);
        public static readonly StyledProperty<ConnectionOffsetMode> TargetOffsetModeProperty = AvaloniaProperty.Register<BaseConnection, ConnectionOffsetMode>(nameof(TargetOffsetMode), ConnectionOffsetMode.Static);
        public static readonly StyledProperty<Orientation> SourceOrientationProperty = AvaloniaProperty.Register<BaseConnection, Orientation>(nameof(SourceOrientation), Orientation.Horizontal);
        public static readonly StyledProperty<Orientation> TargetOrientationProperty = AvaloniaProperty.Register<BaseConnection, Orientation>(nameof(TargetOrientation), Orientation.Horizontal);
        public static readonly StyledProperty<ConnectionDirection> DirectionProperty = AvaloniaProperty.Register<BaseConnection, ConnectionDirection>(nameof(Direction));
        public static readonly StyledProperty<uint> DirectionalArrowsCountProperty = AvaloniaProperty.Register<BaseConnection, uint>(nameof(DirectionalArrowsCount), BoxValue.UInt0);
        public static readonly StyledProperty<double> DirectionalArrowsOffsetProperty = AvaloniaProperty.Register<BaseConnection, double>(nameof(DirectionalArrowsOffset), BoxValue.Double0);
        public static readonly StyledProperty<double> SpacingProperty = AvaloniaProperty.Register<BaseConnection, double>(nameof(Spacing), BoxValue.Double0);
        public static readonly StyledProperty<Size> ArrowSizeProperty = AvaloniaProperty.Register<BaseConnection, Size>(nameof(ArrowSize), BoxValue.ArrowSize);
        public static readonly StyledProperty<ArrowHeadEnds> ArrowEndsProperty = AvaloniaProperty.Register<BaseConnection, ArrowHeadEnds>(nameof(ArrowEnds), ArrowHeadEnds.End);
        public static readonly StyledProperty<ArrowHeadShape> ArrowShapeProperty = AvaloniaProperty.Register<BaseConnection, ArrowHeadShape>(nameof(ArrowShape), ArrowHeadShape.Arrowhead);
        public static readonly StyledProperty<ICommand> SplitCommandProperty = AvaloniaProperty.Register<BaseConnection, ICommand>(nameof(SplitCommand));
        public static readonly StyledProperty<ICommand> DisconnectCommandProperty = Connector.DisconnectCommandProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<IBrush?> ForegroundProperty = TextBlock.ForegroundProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<string?> TextProperty = TextBlock.TextProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<double> FontSizeProperty = TextElement.FontSizeProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<FontFamily> FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<FontWeight> FontWeightProperty = TextElement.FontWeightProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<FontStyle> FontStyleProperty = TextElement.FontStyleProperty.AddOwner<BaseConnection>();
        public static readonly StyledProperty<FontStretch> FontStretchProperty = TextElement.FontStretchProperty.AddOwner<BaseConnection>();

        static BaseConnection()
        {
            AffectsRender<BaseConnection>(SourceProperty, TargetProperty, SourceOffsetProperty, TargetOffsetProperty, 
                SourceOffsetModeProperty, TargetOffsetModeProperty, DirectionProperty, SpacingProperty, ArrowSizeProperty, ArrowEndsProperty, ArrowShapeProperty, TextProperty, DirectionalArrowsCountProperty, DirectionalArrowsOffsetProperty);
            AffectsGeometry<BaseConnection>(SourceProperty, TargetProperty, SourceOffsetProperty, TargetOffsetProperty, 
                SourceOffsetModeProperty, TargetOffsetModeProperty, DirectionProperty, SpacingProperty, ArrowSizeProperty, ArrowEndsProperty, ArrowShapeProperty, SourceOrientationProperty, TargetOrientationProperty, DirectionalArrowsCountProperty, DirectionalArrowsOffsetProperty);
        }
        
        /// <summary>
        /// Gets or sets the start point of this connection.
        /// </summary>
        public Point Source
        {
            get => (Point)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the end point of this connection.
        /// </summary>
        public Point Target
        {
            get => (Point)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        /// <summary>
        /// Gets or sets the offset from the <see cref="Source"/> point.
        /// </summary>
        public Size SourceOffset
        {
            get => (Size)GetValue(SourceOffsetProperty);
            set => SetValue(SourceOffsetProperty, value);
        }

        /// <summary>
        /// Gets or sets the offset from the <see cref="Target"/> point.
        /// </summary>
        public Size TargetOffset
        {
            get => (Size)GetValue(TargetOffsetProperty);
            set => SetValue(TargetOffsetProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="ConnectionOffsetMode"/> to apply to the <see cref="Source"/> when drawing the connection.
        /// </summary>
        public ConnectionOffsetMode SourceOffsetMode
        {
            get => (ConnectionOffsetMode)GetValue(SourceOffsetModeProperty);
            set => SetValue(SourceOffsetModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="ConnectionOffsetMode"/> to apply to the <see cref="Target"/> when drawing the connection.
        /// </summary>
        public ConnectionOffsetMode TargetOffsetMode
        {
            get => (ConnectionOffsetMode)GetValue(TargetOffsetModeProperty);
            set => SetValue(TargetOffsetModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the orientation in which this connection is flowing.
        /// </summary>
        public Orientation SourceOrientation
        {
            get => (Orientation)GetValue(SourceOrientationProperty);
            set => SetValue(SourceOrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the orientation in which this connection is flowing.
        /// </summary>
        public Orientation TargetOrientation
        {
            get => (Orientation)GetValue(TargetOrientationProperty);
            set => SetValue(TargetOrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction in which this connection is flowing.
        /// </summary>
        public ConnectionDirection Direction
        {
            get => (ConnectionDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        /// <summary>
        /// Gets or sets the number of arrows to be drawn on the line in the direction of the connection (see <see cref="Direction"/>).
        /// </summary>
        public uint DirectionalArrowsCount
        {
            get => (uint)GetValue(DirectionalArrowsCountProperty);
            set => SetValue(DirectionalArrowsCountProperty, value);
        }

        /// <summary>
        /// Gets or sets the offset of the arrows drawn by the <see cref="DirectionalArrowsCount"/> (value is clamped between 0 and 1).
        /// </summary>
        public double DirectionalArrowsOffset
        {
            get => (double)GetValue(DirectionalArrowsOffsetProperty);
            set => SetValue(DirectionalArrowsOffsetProperty, value);
        }

        /// <summary>
        /// Gets or sets the arrowhead ends.
        /// </summary>
        public ArrowHeadEnds ArrowEnds
        {
            get => (ArrowHeadEnds)GetValue(ArrowEndsProperty);
            set => SetValue(ArrowEndsProperty, value);
        }

        /// <summary>
        /// Gets or sets the arrowhead ends.
        /// </summary>
        public ArrowHeadShape ArrowShape
        {
            get => (ArrowHeadShape)GetValue(ArrowShapeProperty);
            set => SetValue(ArrowShapeProperty, value);
        }

        /// <summary>
        /// The distance between the start point and the where the angle breaks.
        /// </summary>
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the arrow head.
        /// </summary>
        public Size ArrowSize
        {
            get => (Size)GetValue(ArrowSizeProperty);
            set => SetValue(ArrowSizeProperty, value);
        }

        /// <summary>
        /// Splits the connection. Triggered by <see cref="EditorGestures.Connection.Split"/> gesture.
        /// Parameter is the location where the splitting ocurred.
        /// </summary>
        public ICommand? SplitCommand
        {
            get => (ICommand)GetValue(SplitCommandProperty);
            set => SetValue(SplitCommandProperty, value);
        }

        /// <summary>
        /// Removes this connection. Triggered by <see cref="EditorGestures.Connection.Disconnect"/> gesture.
        /// Parameter is the location where the disconnect ocurred.
        /// </summary>
        public ICommand? DisconnectCommand
        {
            get => (ICommand?)GetValue(DisconnectCommandProperty);
            set => SetValue(DisconnectCommandProperty, value);
        }

        /// <summary>
        /// The brush used to render the <see cref="Text"/>.
        /// </summary>
        public IBrush? Foreground
        {
            get => (IBrush?)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the text contents of the <see cref="BaseConnection"/>.
        /// </summary>
        public string? Text
        {
            get => (string?)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <inheritdoc cref="TextElement.FontSize" />
        //[TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <inheritdoc cref="TextElement.FontFamily" />
        public FontFamily FontFamily
        {
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <inheritdoc cref="TextElement.FontStyle" />
        public FontStyle FontStyle
        {
            get => (FontStyle)GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        /// <inheritdoc cref="TextElement.FontWeight" />
        public FontWeight FontWeight
        {
            get => (FontWeight)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        /// <inheritdoc cref="TextElement.FontStretch" />
        public FontStretch FontStretch
        {
            get => (FontStretch)GetValue(FontStretchProperty);
            set => SetValue(FontStretchProperty, value);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent DisconnectEvent = RoutedEvent.Register<ConnectionEventArgs>(nameof(Disconnect), RoutingStrategies.Bubble, typeof(BaseConnection));
        public static readonly RoutedEvent SplitEvent = RoutedEvent.Register<ConnectionEventArgs>(nameof(Split), RoutingStrategies.Bubble, typeof(BaseConnection));

        /// <summary>Triggered by the <see cref="EditorGestures.Connection.Disconnect"/> gesture.</summary>
        public event ConnectionEventHandler Disconnect
        {
            add => AddHandler(DisconnectEvent, value);
            remove => RemoveHandler(DisconnectEvent, value);
        }

        /// <summary>Triggered by the <see cref="EditorGestures.Connection.Split"/> gesture.</summary>
        public event ConnectionEventHandler Split
        {
            add => AddHandler(SplitEvent, value);
            remove => RemoveHandler(SplitEvent, value);
        }

        #endregion

        /// <summary>
        /// Gets a vector that has its coordinates set to 0.
        /// </summary>
        protected static readonly Vector ZeroVector = new Vector(0d, 0d);
        
        protected override Geometry CreateDefiningGeometry()
        {
            var _geometry = new StreamGeometry
            {
                // FillRule = FillRule.EvenOdd
            };
            using (StreamGeometryContext context = _geometry.Open())
            {
                (Vector sourceOffset, Vector targetOffset) = GetOffset();
                var (arrowStart, arrowEnd) = DrawLineGeometry(context, Source + sourceOffset, Target + targetOffset);

                if (ArrowSize.Width != 0d && ArrowSize.Height != 0d)
                {
                    var reverseDirection = Direction == ConnectionDirection.Forward ? ConnectionDirection.Backward : ConnectionDirection.Forward;
                    switch (ArrowEnds)
                    {
                        case ArrowHeadEnds.Start:
                            DrawArrowGeometry(context, arrowStart.ArrowStartSource, arrowStart.ArrowStartTarget, reverseDirection, ArrowShape, SourceOrientation);
                            break;
                        case ArrowHeadEnds.End:
                            DrawArrowGeometry(context, arrowEnd.ArrowEndSource, arrowEnd.ArrowEndTarget, Direction, ArrowShape, TargetOrientation);
                            break;
                        case ArrowHeadEnds.Both:
                            DrawArrowGeometry(context, arrowEnd.ArrowEndSource, arrowEnd.ArrowEndTarget, Direction, ArrowShape, TargetOrientation);
                            DrawArrowGeometry(context, arrowStart.ArrowStartSource, arrowStart.ArrowStartTarget, reverseDirection, ArrowShape, SourceOrientation);
                            break;
                        case ArrowHeadEnds.None:
                        default:
                            break;
                    }

                    if (DirectionalArrowsCount > 0)
                    {
                        DrawDirectionalArrowsGeometry(context, Source + sourceOffset, Target + targetOffset);
                    }
                }
            }

            return _geometry;
        }

        protected abstract ((Point ArrowStartSource, Point ArrowStartTarget), (Point ArrowEndSource, Point ArrowEndTarget)) DrawLineGeometry(StreamGeometryContext context, Point source, Point target);

        protected virtual void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target) { }

        protected virtual void DrawDirectionalArrowheadGeometry(StreamGeometryContext context, Vector direction, Point location)
        {
            double headWidth = ArrowSize.Width;
            double headHeight = ArrowSize.Height / 2;

            double angle = Math.Atan2(direction.Y, direction.X);
            double sinT = Math.Sin(angle);
            double cosT = Math.Cos(angle);

            var from = new Point(location.X + (headWidth * cosT - headHeight * sinT), location.Y + (headWidth * sinT + headHeight * cosT));
            var to = new Point(location.X + (headWidth * cosT + headHeight * sinT), location.Y - (headHeight * cosT - headWidth * sinT));

            context.BeginFigure(location, true, true);
            context.LineTo(from, true, true);
            context.LineTo(to, true, true);
        }

        protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, ArrowHeadShape shape = ArrowHeadShape.Arrowhead, Orientation orientation = Orientation.Horizontal)
        {
            switch (shape)
            {
                case ArrowHeadShape.Ellipse:
                    DrawEllipseArrowhead(context, source, target, arrowDirection, orientation);
                    break;
                case ArrowHeadShape.Rectangle:
                    DrawRectangleArrowhead(context, source, target, arrowDirection, orientation);
                    break;
                case ArrowHeadShape.Arrowhead:
                default:
                    DrawDefaultArrowhead(context, source, target, arrowDirection, orientation);
                    break;
            }
        }

        protected virtual void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, Orientation orientation = Orientation.Horizontal)
        {
            double direction = arrowDirection == ConnectionDirection.Forward ? 1d : -1d;

            if (orientation == Orientation.Horizontal)
            {
                double headWidth = ArrowSize.Width;
                double headHeight = ArrowSize.Height / 2;

                var from = new Point(target.X - headWidth * direction, target.Y + headHeight);
                var to = new Point(target.X - headWidth * direction, target.Y - headHeight);

                using var _ = context.BeginFigure(target, true, true);
                context.LineTo(from, true, true);
                context.LineTo(to, true, true);
            }
            else
            {
                double headWidth = ArrowSize.Width / 2;
                double headHeight = ArrowSize.Height;

                var from = new Point(target.X - headWidth, target.Y - headHeight * direction);
                var to = new Point(target.X + headWidth, target.Y - headHeight * direction);

                using var _ = context.BeginFigure(target, true, true);
                context.LineTo(from, true, true);
                context.LineTo(to, true, true);
            }
        }

        protected virtual void DrawRectangleArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, Orientation orientation = Orientation.Horizontal)
        {
            double direction = arrowDirection == ConnectionDirection.Forward ? 1d : -1d;

            if (orientation == Orientation.Horizontal)
            {
                double headWidth = ArrowSize.Width;
                double headHeight = ArrowSize.Height / 2;
                var bottomRight = new Point(target.X, target.Y + headHeight);
                var bottomLeft = new Point(target.X - headWidth * direction, target.Y + headHeight);
                var topLeft = new Point(target.X - headWidth * direction, target.Y - headHeight);
                var topRight = new Point(target.X, target.Y - headHeight);

                using var _ = context.BeginFigure(target, true, true);
                context.LineTo(bottomRight, true, true);
                context.LineTo(bottomLeft, true, true);
                context.LineTo(topLeft, true, true);
                context.LineTo(topRight, true, true);
            }
            else
            {
                double headWidth = ArrowSize.Width / 2;
                double headHeight = ArrowSize.Height;
                var bottomLeft = new Point(target.X - headWidth, target.Y);
                var topLeft = new Point(target.X - headWidth, target.Y - headHeight * direction);
                var topRight = new Point(target.X + headWidth, target.Y - headHeight * direction);
                var bottomRight = new Point(target.X + headWidth, target.Y);

                using var _ = context.BeginFigure(target, true, true);
                context.LineTo(bottomLeft, true, true);
                context.LineTo(topLeft, true, true);
                context.LineTo(topRight, true, true);
                context.LineTo(bottomRight, true, true);
            }
        }

        protected virtual void DrawEllipseArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward, Orientation orientation = Orientation.Horizontal)
        {
            const double ControlPointRatio = 0.55228474983079356; // (Math.Sqrt(2) - 1) * 4 / 3;

            double direction = arrowDirection == ConnectionDirection.Forward ? 1d : -1d;
            var targetLocation = orientation == Orientation.Horizontal
                ? new Point(target.X - ArrowSize.Width / 2 * direction, target.Y)
                : new Point(target.X, target.Y - ArrowSize.Height / 2 * direction);

            double headWidth = ArrowSize.Width / 2;
            double headHeight = ArrowSize.Height / 2;

            double x0 = targetLocation.X - headWidth;
            double x1 = targetLocation.X - headWidth * ControlPointRatio;
            double x2 = targetLocation.X;
            double x3 = targetLocation.X + headWidth * ControlPointRatio;
            double x4 = targetLocation.X + headWidth;

            double y0 = targetLocation.Y - headHeight;
            double y1 = targetLocation.Y - headHeight * ControlPointRatio;
            double y2 = targetLocation.Y;
            double y3 = targetLocation.Y + headHeight * ControlPointRatio;
            double y4 = targetLocation.Y + headHeight;

            using var _ = context.BeginFigure(new Point(x2, y0), true, true);
            context.BezierTo(new Point(x3, y0), new Point(x4, y1), new Point(x4, y2), true, true);
            context.BezierTo(new Point(x4, y3), new Point(x3, y4), new Point(x2, y4), true, true);
            context.BezierTo(new Point(x1, y4), new Point(x0, y3), new Point(x0, y2), true, true);
            context.BezierTo(new Point(x0, y1), new Point(x1, y0), new Point(x2, y0), true, true);
        }

        /// <summary>
        /// Gets the resulting offset after applying the <see cref="SourceOffsetMode"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual (Vector SourceOffset, Vector TargetOffset) GetOffset()
        {
            Vector sourceDelta = Target - Source;
            Vector targetDelta = Source - Target;
            double arrowDirection = Direction == ConnectionDirection.Forward ? 1d : -1d;

            var sourceOffset = GetOffset(SourceOffsetMode, sourceDelta, SourceOffset, arrowDirection);
            var targetOffset = GetOffset(TargetOffsetMode, targetDelta, TargetOffset, -arrowDirection);

            if (SourceOrientation == Orientation.Vertical)
            {
                sourceOffset = new Vector(sourceOffset.Y, sourceOffset.X);
            }

            if (TargetOrientation == Orientation.Vertical)
            {
                targetOffset = new Vector(targetOffset.Y, targetOffset.X);
            }

            return (sourceOffset, targetOffset);

            static Vector GetOffset(ConnectionOffsetMode mode, Vector delta, Size currentOffset, double arrowDirection) => mode switch
            {
                ConnectionOffsetMode.Rectangle => GetRectangleModeOffset(delta, currentOffset),
                ConnectionOffsetMode.Circle => GetCircleModeOffset(delta, currentOffset),
                ConnectionOffsetMode.Edge => GetEdgeModeOffset(delta, currentOffset),
                ConnectionOffsetMode.Static => GetStaticModeOffset(arrowDirection, currentOffset),
                ConnectionOffsetMode.None => ZeroVector,
                _ => throw new NotImplementedException()
            };

            static Vector GetStaticModeOffset(double direction, Size offset)
            {
                double xOffset = offset.Width * direction;
                double yOffset = offset.Height * direction;

                return new Vector(xOffset, yOffset);
            }

            static Vector GetEdgeModeOffset(Vector delta, Size offset)
            {
                double xOffset = Math.Min(Math.Abs(delta.X) / 2d, offset.Width) * Math.Sign(delta.X);
                double yOffset = Math.Min(Math.Abs(delta.Y) / 2d, offset.Height) * Math.Sign(delta.Y);

                return new Vector(xOffset, yOffset);
            }

            static Vector GetCircleModeOffset(Vector delta, Size offset)
            {
                if (delta.SquaredLength > 0d)
                {
                    delta.Normalize();
                }

                return new Vector(delta.X * offset.Width, delta.Y * offset.Height);
            }

            static Vector GetRectangleModeOffset(Vector delta, Size offset)
            {
                if (delta.SquaredLength > 0d)
                {
                    delta.Normalize();
                }

                double angle = Math.Atan2(delta.Y, delta.X);
                double x, y;

                if (offset.Width * 2d * Math.Abs(delta.Y) < offset.Height * 2d * Math.Abs(delta.X))
                {
                    x = Math.Sign(delta.X) * offset.Width;
                    y = Math.Tan(angle) * x;
                }
                else
                {
                    y = Math.Sign(delta.Y) * offset.Height;
                    x = 1.0d / Math.Tan(angle) * y;
                }

                return new Vector(x, y);
            }
        }

        protected virtual Point GetTextPosition(FormattedText text, Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var spacingVertical = new Vector(spacing.Y, spacing.X);

            var p0 = source + (SourceOrientation == Orientation.Vertical ? spacingVertical : spacing);
            var p1 = target - (TargetOrientation == Orientation.Vertical ? spacingVertical : spacing);

            return new Point((p0.X + p1.X - text.Width) / 2, (p0.Y + p1.Y - text.Height) / 2);
        }

        /// <summary>Starts animating the directional arrows.</summary>
        /// <param name="duration">The duration for moving an arrowhead from <see cref="Source"/> to <see cref="Target"/>.</param>
        public void StartAnimation(double duration = 1.5d)
        {
            if (DirectionalArrowsCount > 0)
            {
                animationTokenSource?.Cancel();
                animationTokenSource = new CancellationTokenSource();
                this.StartLoopingAnimation(DirectionalArrowsOffsetProperty, DirectionalArrowsOffset + 1d, duration, animationTokenSource.Token);
            }
        }

        /// <summary>Stops the animation started by <see cref="StartAnimation(double)"/></summary>
        public void StopAnimation()
        {
            this.CancelAnimation(DirectionalArrowsOffsetProperty, animationTokenSource);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            Focus();

            e.Pointer.Capture(this);
            this.PropagateMouseCapturedWithin(true);

            EditorGestures.ConnectionGestures gestures = EditorGestures.Mappings.Connection;
            if (gestures.Split.Matches(e.Source, e) && (SplitCommand?.CanExecute(this) ?? false))
            {
                Point splitLocation = e.GetPosition(this);
                object? connection = DataContext;
                var args = new ConnectionEventArgs(connection)
                {
                    RoutedEvent = SplitEvent,
                    SplitLocation = splitLocation,
                    Source = this
                };

                RaiseEvent(args);

                // Raise SplitCommand if SplitEvent is not handled
                if (!args.Handled && (SplitCommand?.CanExecute(splitLocation) ?? false))
                {
                    SplitCommand.Execute(splitLocation);
                }

                e.Handled = true;
            }
            else if (gestures.Disconnect.Matches(e.Source, e) && (DisconnectCommand?.CanExecute(this) ?? false))
            {
                Point splitLocation = e.GetPosition(this);
                object? connection = DataContext;
                var args = new ConnectionEventArgs(connection)
                {
                    RoutedEvent = DisconnectEvent,
                    SplitLocation = splitLocation,
                    Source = this
                };

                RaiseEvent(args);

                // Raise DisconnectCommand if DisconnectEvent is not handled
                if (!args.Handled && (DisconnectCommand?.CanExecute(splitLocation) ?? false))
                {
                    DisconnectCommand.Execute(splitLocation);
                }

                e.Handled = true;
            }
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            if (ReferenceEquals(e.Pointer.Captured, this))
            {
                e.Pointer.Capture(null);
                this.PropagateMouseCapturedWithin(false);
            }
        }
        
        protected override void Render(DrawingContext drawingContext)
        {
            base.Render(drawingContext);
        
            if (!string.IsNullOrEmpty(Text))
            {
                var typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
                var text = new FormattedText(Text, CultureInfo.CurrentUICulture, FlowDirection, typeface, FontSize, Foreground ?? Stroke);

                (Vector sourceOffset, Vector targetOffset) = GetOffset();
                drawingContext.DrawText(text, GetTextPosition(text, Source + sourceOffset, Target + targetOffset));
            }
        }
    }
}

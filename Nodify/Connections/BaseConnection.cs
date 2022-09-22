using System;
using System.Windows;
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
    /// Represents the base class for shapes that are drawn from a <see cref="Source"/> point to a <see cref="Target"/> point.
    /// </summary>
    public abstract class BaseConnection : Shape
    {
        #region Dependency Properties

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(Point), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(nameof(Target), typeof(Point), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SourceOffsetProperty = DependencyProperty.Register(nameof(SourceOffset), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TargetOffsetProperty = DependencyProperty.Register(nameof(TargetOffset), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty OffsetModeProperty = DependencyProperty.Register(nameof(OffsetMode), typeof(ConnectionOffsetMode), typeof(BaseConnection), new FrameworkPropertyMetadata(default(ConnectionOffsetMode), FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(nameof(Direction), typeof(ConnectionDirection), typeof(BaseConnection), new FrameworkPropertyMetadata(default(ConnectionDirection), FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Double0, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty ArrowSizeProperty = DependencyProperty.Register(nameof(ArrowSize), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.ArrowSize, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SplitCommandProperty = DependencyProperty.Register(nameof(SplitCommand), typeof(ICommand), typeof(BaseConnection));
        public static readonly DependencyProperty DisconnectCommandProperty = Connector.DisconnectCommandProperty.AddOwner(typeof(BaseConnection));

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
        /// Gets or sets the <see cref="ConnectionOffsetMode"/> to apply when drawing the connection.
        /// </summary>
        public ConnectionOffsetMode OffsetMode
        {
            get => (ConnectionOffsetMode)GetValue(OffsetModeProperty);
            set => SetValue(OffsetModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction in which this connection is oriented.
        /// </summary>
        public ConnectionDirection Direction
        {
            get => (ConnectionDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
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
        /// Splits the connection. Triggered on double click.
        /// Parameter is the location where this was clicked.
        /// </summary>
        public ICommand? SplitCommand
        {
            get => (ICommand)GetValue(SplitCommandProperty);
            set => SetValue(SplitCommandProperty, value);
        }

        /// <summary>
        /// Removes this connection. Triggered with ALT+Click.
        /// Parameter is the location where this was clicked.
        /// </summary>
        public ICommand? DisconnectCommand
        {
            get => (ICommand?)GetValue(DisconnectCommandProperty);
            set => SetValue(DisconnectCommandProperty, value);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent DisconnectEvent = EventManager.RegisterRoutedEvent(nameof(Disconnect), RoutingStrategy.Bubble, typeof(ConnectionEventHandler), typeof(BaseConnection));
        public static readonly RoutedEvent SplitEvent = EventManager.RegisterRoutedEvent(nameof(Split), RoutingStrategy.Bubble, typeof(ConnectionEventHandler), typeof(BaseConnection));

        /// <summary>
        /// Occurs when the <see cref="ModifierKeys.Alt"/> key is held and the <see cref="BaseConnection"/> is clicked.
        /// </summary>
        public event ConnectionEventHandler Disconnect
        {
            add => AddHandler(DisconnectEvent, value);
            remove => RemoveHandler(DisconnectEvent, value);
        }
        
        /// <summary>
        /// Occurs when the <see cref="BaseConnection"/> is double clicked.
        /// </summary>
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

        private readonly StreamGeometry _geometry = new StreamGeometry
        {
            FillRule = FillRule.EvenOdd
        };

        protected override Geometry DefiningGeometry
        {
            get
            {
                using (StreamGeometryContext context = _geometry.Open())
                {
                    (Vector sourceOffset, Vector targetOffset) = GetOffset();
                    Point source = Source + sourceOffset;
                    Point target = Target + targetOffset;

                    (Point arrowSource, Point arrowTarget) = DrawLineGeometry(context, source, target);

                    if (ArrowSize.Width != 0d && ArrowSize.Height != 0d)
                    {
                        DrawArrowGeometry(context, arrowSource, arrowTarget);
                    }
                }

                return _geometry;
            }
        }

        protected abstract (Point ArrowSource, Point ArrowTarget) DrawLineGeometry(StreamGeometryContext context, Point source, Point target);

        protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target)
        {
            (Point from, Point to) = GetArrowHeadPoints(source, target);

            context.BeginFigure(target, true, true);
            context.LineTo(from, true, true);
            context.LineTo(to, true, true);
        }

        protected virtual (Point From, Point To) GetArrowHeadPoints(Point source, Point target)
        {
            double headWidth = ArrowSize.Width;
            double headHeight = ArrowSize.Height;

            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var from = new Point(target.X - headWidth * direction, target.Y + headHeight);
            var to = new Point(target.X - headWidth * direction, target.Y - headHeight);
            return (from, to);
        }

        /// <summary>
        /// Gets the resulting offset after applying the <see cref="OffsetMode"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual (Vector SourceOffset, Vector TargetOffset) GetOffset()
        {
            Vector delta = Target - Source;
            Vector delta2 = Source - Target;

            return OffsetMode switch
            {
                ConnectionOffsetMode.Rectangle => (GetRectangleModeOffset(delta, SourceOffset), GetRectangleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Circle => (GetCircleModeOffset(delta, SourceOffset), GetCircleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Edge => (GetEdgeModeOffset(delta, SourceOffset), GetEdgeModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.None => (ZeroVector, ZeroVector),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static Vector GetEdgeModeOffset(Vector delta, Size offset)
        {
            double xOffset = Math.Min(Math.Abs(delta.X) / 2d, offset.Width) * Math.Sign(delta.X);
            double yOffset = Math.Min(Math.Abs(delta.Y) / 2d, offset.Height) * Math.Sign(delta.Y);

            return new Vector(xOffset, yOffset);
        }

        private static Vector GetCircleModeOffset(Vector delta, Size offset)
        {
            if (delta.LengthSquared > 0d)
            {
                delta.Normalize();
            }

            return new Vector(delta.X * offset.Width, delta.Y * offset.Height);
        }

        private static Vector GetRectangleModeOffset(Vector delta, Size offset)
        {
            if (delta.LengthSquared > 0d)
            {
                delta.Normalize();
            }

            double angle = Math.Atan2(delta.Y, delta.X);
            var result = new Vector();

            if (offset.Width * 2d * Math.Abs(delta.Y) < offset.Height * 2d * Math.Abs(delta.X))
            {
                result.X = Math.Sign(delta.X) * offset.Width;
                result.Y = Math.Tan(angle) * result.X;
            }
            else
            {
                result.Y = Math.Sign(delta.Y) * offset.Height;
                result.X = 1.0d / Math.Tan(angle) * result.Y;
            }

            return result;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                CaptureMouse();

                if (e.ClickCount == 2 && (SplitCommand?.CanExecute(this) ?? false))
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
                }
                else if (Keyboard.Modifiers == ModifierKeys.Alt && (DisconnectCommand?.CanExecute(this) ?? false))
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
                }
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }
        }
    }
}

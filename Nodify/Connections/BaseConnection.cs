using System;
using System.Windows;
using System.Windows.Input;
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

        #endregion

        /// <summary>
        /// Gets a vector that has its coordinates set to 0.
        /// </summary>
        protected static readonly Vector ZeroVector = new Vector(0, 0);

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
                _ => (ZeroVector, ZeroVector)
            };
        }

        private Vector GetEdgeModeOffset(Vector delta, Size offset)
        {
            double xOffset = Math.Min(Math.Abs(delta.X) / 2, offset.Width) * Math.Sign(delta.X);
            double yOffset = Math.Min(Math.Abs(delta.Y) / 2, offset.Height) * Math.Sign(delta.Y);

            return new Vector(xOffset, yOffset);
        }

        private Vector GetCircleModeOffset(Vector delta, Size offset)
        {
            if (delta.LengthSquared > 0)
            {
                delta.Normalize();
            }

            return new Vector(delta.X * offset.Width, delta.Y * offset.Height);
        }

        private Vector GetRectangleModeOffset(Vector delta, Size offset)
        {
            if (delta.LengthSquared > 0)
            {
                delta.Normalize();
            }

            double angle = Math.Atan2(delta.Y, delta.X);
            Vector result = new Vector();

            if (offset.Width * 2 * Math.Abs(delta.Y) < offset.Height * 2 * Math.Abs(delta.X))
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

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Nodify
{
    public enum ConnectionOffsetMode
    {
        None,
        Circle,
        Rectangle,
        Edge,
    }

    public abstract class BaseConnection : Shape
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(Point), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(nameof(Target), typeof(Point), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SourceOffsetProperty = DependencyProperty.Register(nameof(SourceOffset), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TargetOffsetProperty = DependencyProperty.Register(nameof(TargetOffset), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty OffsetModeProperty = DependencyProperty.Register(nameof(OffsetMode), typeof(ConnectionOffsetMode), typeof(BaseConnection), new FrameworkPropertyMetadata(default(ConnectionOffsetMode), FrameworkPropertyMetadataOptions.AffectsRender));

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

        public Size SourceOffset
        {
            get => (Size)GetValue(SourceOffsetProperty);
            set => SetValue(SourceOffsetProperty, value);
        }

        public Size TargetOffset
        {
            get => (Size)GetValue(TargetOffsetProperty);
            set => SetValue(TargetOffsetProperty, value);
        }

        public ConnectionOffsetMode OffsetMode
        {
            get => (ConnectionOffsetMode)GetValue(OffsetModeProperty);
            set => SetValue(OffsetModeProperty, value);
        }

        protected static readonly Vector ZeroVector = new Vector(0, 0);

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

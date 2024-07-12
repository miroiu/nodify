using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Nodify
{
    public class ResizablePanel : ContentControl
    {
        internal static readonly object BoxedResizeDirection = ResizeDirections.All;

        public static readonly DependencyProperty DirectionsProperty
            = DependencyProperty.Register(nameof(Directions), typeof(ResizeDirections), typeof(ResizablePanel), new FrameworkPropertyMetadata(BoxedResizeDirection));

        public static readonly DependencyProperty ResizeStartedCommandProperty = DependencyProperty.Register(nameof(ResizeStartedCommand), typeof(ICommand), typeof(ResizablePanel));

        public static readonly DependencyProperty ResizeCompletedCommandProperty = DependencyProperty.Register(nameof(ResizeCompletedCommand), typeof(ICommand), typeof(ResizablePanel));

        public ResizeDirections Directions
        {
            get => (ResizeDirections)GetValue(DirectionsProperty);
            set => SetValue(DirectionsProperty, value);
        }

        public ICommand? ResizeStartedCommand
        {
            get => (ICommand)GetValue(ResizeStartedCommandProperty);
            set => SetValue(ResizeStartedCommandProperty, value);
        }

        public ICommand? ResizeCompletedCommand
        {
            get => (ICommand)GetValue(ResizeCompletedCommandProperty);
            set => SetValue(ResizeCompletedCommandProperty, value);
        }

        static ResizablePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizablePanel), new FrameworkPropertyMetadata(typeof(ResizablePanel)));
        }

        public ResizablePanel()
        {
            AddHandler(Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnResize));
            AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStarted));
            AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompleted));
        }

        private void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            if (ResizeStartedCommand?.CanExecute(null) ?? false)
            {
                ResizeStartedCommand.Execute(null);
            }
        }

        private void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (ResizeCompletedCommand?.CanExecute(null) ?? false)
            {
                ResizeCompletedCommand.Execute(null);
            }
        }

        private void OnResize(object sender, DragDeltaEventArgs e)
        {
            if (e.OriginalSource is Resizer resizer)
            {
                double resizeX = 0;
                double resizeY = 0;

                double moveX = 0;
                double moveY = 0;

                if (resizer.Direction.HasFlag(ResizeDirections.Top))
                {
                    moveY = resizeY = ResizeTop(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.Bottom))
                {
                    resizeY = ResizeBottom(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.Left))
                {
                    moveX = resizeX = ResizeLeft(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.Right))
                {
                    resizeX = ResizeRight(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.TopLeft))
                {
                    moveY = resizeY = ResizeTop(e);
                    moveX = resizeX = ResizeLeft(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.TopRight))
                {
                    moveY = resizeY = ResizeTop(e);
                    resizeX = ResizeRight(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.BottomLeft))
                {
                    resizeY = ResizeBottom(e);
                    moveX = resizeX = ResizeLeft(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.BottomRight))
                {
                    resizeY = ResizeBottom(e);
                    resizeX = ResizeRight(e);
                }

                OnProcessDelta(ref resizeX, ref resizeY);
                OnProcessDelta(ref moveX, ref moveY);

                OnMove(moveX, moveY);

                Width -= resizeX;
                Height -= resizeY;

                e.Handled = true;
            }
        }

        private double ResizeBottom(DragDeltaEventArgs e)
        {
            return Math.Min(-e.VerticalChange, ActualHeight - MinHeight);
        }

        private double ResizeTop(DragDeltaEventArgs e)
        {
            return Math.Min(e.VerticalChange, ActualHeight - MinHeight);
        }

        private double ResizeRight(DragDeltaEventArgs e)
        {
            return Math.Min(-e.HorizontalChange, ActualWidth - MinWidth);
        }

        private double ResizeLeft(DragDeltaEventArgs e)
        {
            return Math.Min(e.HorizontalChange, ActualWidth - MinWidth);
        }

        protected virtual void OnMove(double x, double y)
        {
            Canvas.SetTop(this, Canvas.GetTop(this) + y);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + x);
        }

        protected virtual void OnProcessDelta(ref double dx, ref double dy)
        {
        }
    }

    public class Resizer : Thumb
    {
        public static readonly DependencyProperty DirectionProperty
            = DependencyProperty.Register(nameof(Direction), typeof(ResizeDirections), typeof(Resizer), new FrameworkPropertyMetadata(ResizablePanel.BoxedResizeDirection));

        public ResizeDirections Direction
        {
            get => (ResizeDirections)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        static Resizer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(typeof(Resizer)));
        }
    }

    [Flags]
    public enum ResizeDirections
    {
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        TopLeft = 16,
        TopRight = 32,
        BottomLeft = 64,
        BottomRight = 128,

        Edges = Top | Left | Bottom | Right,
        Corners = TopLeft | TopRight | BottomLeft | BottomRight,
        All = Edges | Corners
    }
}

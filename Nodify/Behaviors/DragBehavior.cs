using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Nodify
{
    public static class DragBehavior
    {
        #region Routed Events

        public static readonly RoutedEvent DragStartedEvent = EventManager.RegisterRoutedEvent("DragStarted", RoutingStrategy.Bubble, typeof(DragStartedEventHandler), typeof(DragBehavior));
        public static readonly RoutedEvent DragCompletedEvent = EventManager.RegisterRoutedEvent("DragCompleted", RoutingStrategy.Bubble, typeof(DragCompletedEventHandler), typeof(DragBehavior));
        public static readonly RoutedEvent DragDeltaEvent = EventManager.RegisterRoutedEvent("DragDelta", RoutingStrategy.Bubble, typeof(DragDeltaEventHandler), typeof(DragBehavior));

        #endregion

        #region Dependency Properties

        public static DependencyProperty IsDraggableProperty = DependencyProperty.RegisterAttached("IsDraggable", typeof(bool), typeof(DragBehavior), new FrameworkPropertyMetadata(BoxValue.False, OnIsDraggableChanged));
        public static DependencyProperty DraggableHostProperty = DependencyProperty.RegisterAttached("DraggableHost", typeof(UIElement), typeof(DragBehavior), new FrameworkPropertyMetadata(null));
        public static DependencyPropertyKey IsDraggingPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsDragging", typeof(bool), typeof(DragBehavior), new FrameworkPropertyMetadata(BoxValue.False));
        public static DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

        public static bool GetIsDragging(UIElement elem)
            => (bool)elem.GetValue(IsDraggingProperty);

        public static void SetIsDragging(UIElement elem, bool value)
            => elem.SetValue(IsDraggingPropertyKey, value);

        public static bool GetIsDraggable(UIElement elem)
            => (bool)elem.GetValue(IsDraggableProperty);

        public static void SetIsDraggable(UIElement elem, bool value)
            => elem.SetValue(IsDraggableProperty, value);

        public static UIElement GetDraggableHost(UIElement elem)
            => (UIElement)elem.GetValue(DraggableHostProperty);

        public static void SetDraggableHost(UIElement elem, UIElement value)
            => elem.SetValue(DraggableHostProperty, value);

        private static void OnIsDraggableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;

            if (d is UIElement elem)
            {
                if (value)
                {
                    elem.MouseLeftButtonUp += OnCompletedDraggingOperation;
                    elem.MouseMove += OnDragging;
                }
                else
                {
                    elem.MouseLeftButtonUp -= OnCompletedDraggingOperation;
                    elem.MouseMove -= OnDragging;
                }
            }
        }

        #endregion

        private static Point _previousPosition;
        private static Point _initialPosition;

        private static void OnDragging(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is UIElement elem)
            {
                // Need host for snapping and to apply transform 
                var host = GetDraggableHost(elem);
                var position = e.GetPosition(host);

                if (_previousPosition != position)
                {
                    var isDragging = GetIsDragging(elem);
                    var isDraggable = GetIsDraggable(elem);

                    if (!isDragging && isDraggable)
                    {
                        SetIsDragging(elem, true);

                        if (host == null)
                        {
                            host = elem.GetParentOfType<NodifyCanvas>() ?? elem.GetParentOfType<Canvas>() ?? elem;
                            SetDraggableHost(elem, host);
                        }

                        _previousPosition = e.GetPosition(host);
                        _initialPosition = _previousPosition;

                        elem.Focus();
                        elem.CaptureMouse();

                        elem.RaiseEvent(new DragStartedEventArgs(_initialPosition.X, _initialPosition.Y)
                        {
                            RoutedEvent = DragStartedEvent
                        });
                    }

                    if (isDragging /*will be false the first frame*/ && isDraggable)
                    {
                        var delta = position - _previousPosition;
                        _previousPosition = position;

                        elem.RaiseEvent(new DragDeltaEventArgs(delta.X, delta.Y)
                        {
                            RoutedEvent = DragDeltaEvent
                        });

                        e.Handled = true;
                    }
                }
            }
        }

        private static void OnCompletedDraggingOperation(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement elem && GetIsDragging(elem))
            {
                if (Mouse.Captured == elem)
                {
                    elem.ReleaseMouseCapture();
                }

                SetIsDragging(elem, false);
                var position = e.GetPosition(GetDraggableHost(elem)) - _initialPosition;

                elem.RaiseEvent(new DragCompletedEventArgs(position.X, position.Y, false)
                {
                    RoutedEvent = DragCompletedEvent
                });

                e.Handled = true;
            }
        }
    }
}

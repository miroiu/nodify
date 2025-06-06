using Nodify.Interactivity;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Nodify
{
    public partial class NodifyEditor
    {
        #region Dependency properties

        public static readonly DependencyProperty AutoPanSpeedProperty = DependencyProperty.Register(nameof(AutoPanSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(15d));
        public static readonly DependencyProperty AutoPanEdgeDistanceProperty = DependencyProperty.Register(nameof(AutoPanEdgeDistance), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(15d));
        public static readonly DependencyProperty DisableAutoPanningProperty = DependencyProperty.Register(nameof(DisableAutoPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnDisableAutoPanningChanged));
        public static readonly DependencyProperty DisablePanningProperty = DependencyProperty.Register(nameof(DisablePanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnDisablePanningChanged));

        protected static readonly DependencyPropertyKey IsPanningPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsPanningProperty = IsPanningPropertyKey.DependencyProperty;

        private static void OnDisableAutoPanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnDisableAutoPanningChanged((bool)e.NewValue);

        private static void OnDisablePanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            editor.OnDisableAutoPanningChanged(editor.DisableAutoPanning || editor.DisablePanning);
        }

        /// <summary>
        /// Gets or sets whether panning should be disabled.
        /// </summary>
        public bool DisablePanning
        {
            get => (bool)GetValue(DisablePanningProperty);
            set => SetValue(DisablePanningProperty, value);
        }

        /// <summary>
        /// Gets or sets whether to disable the auto panning when selecting or dragging near the edge of the editor configured by <see cref="AutoPanEdgeDistance"/>.
        /// </summary>
        public bool DisableAutoPanning
        {
            get => (bool)GetValue(DisableAutoPanningProperty);
            set => SetValue(DisableAutoPanningProperty, value);
        }

        /// <summary>
        /// Gets or sets the speed used when auto-panning scaled by <see cref="AutoPanningTickRate"/>
        /// </summary>
        public double AutoPanSpeed
        {
            get => (double)GetValue(AutoPanSpeedProperty);
            set => SetValue(AutoPanSpeedProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum distance in pixels from the edge of the editor that will trigger auto-panning.
        /// </summary>
        public double AutoPanEdgeDistance
        {
            get => (double)GetValue(AutoPanEdgeDistanceProperty);
            set => SetValue(AutoPanEdgeDistanceProperty, value);
        }

        /// <summary>
        /// Gets a value that indicates whether a panning operation is in progress.
        /// </summary>
        public bool IsPanning
        {
            get => (bool)GetValue(IsPanningProperty);
            private set => SetValue(IsPanningPropertyKey, value);
        }

        #endregion

        /// <summary>
        /// Gets or sets whether panning cancellation is allowed (see <see cref="EditorGestures.NodifyEditorGestures.CancelAction"/>).
        /// </summary>
        public static bool AllowPanningCancellation { get; set; }

        /// <summary>
        /// Gets or sets how often the new <see cref="ViewportLocation"/> is calculated in milliseconds when <see cref="DisableAutoPanning"/> is false.
        /// </summary>
        public static double AutoPanningTickRate { get; set; } = 1;

        private DispatcherTimer? _autoPanningTimer;

        private Point _initialPanningLocation;

        /// <summary>
        /// Starts the panning operation from the specified location. Call <see cref="EndPanning"/> to end the panning operation.
        /// </summary>
        /// <remarks>This method has no effect if a panning operation is already in progress.</remarks>
        /// <param name="location">The initial location where panning starts, in graph space coordinates.</param>
        public void BeginPanning(Point location)
        {
            if (IsPanning)
            {
                return;
            }

            _initialPanningLocation = location;
            ViewportLocation = location;
            IsPanning = true;
        }

        /// <summary>
        /// Starts the panning operation from the current <see cref="ViewportLocation" />.
        /// </summary>
        /// <remarks>This method has no effect if a panning operation is already in progress.</remarks>
        public void BeginPanning()
            => BeginPanning(ViewportLocation);

        /// <summary>
        /// Pans the viewport by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to pan the viewport.</param>
        /// <remarks>
        /// This method adjusts the current <see cref="ViewportLocation"/> incrementally based on the provided amount.
        /// </remarks>
        public void UpdatePanning(Vector amount)
        {
            ViewportLocation -= amount;
        }

        /// <summary>
        /// Ends the current panning operation, retaining the current <see cref="ViewportLocation"/>.
        /// </summary>
        /// <remarks>This method has no effect if there's no panning operation in progress.</remarks>
        public void EndPanning()
        {
            IsPanning = false;
        }

        /// <summary>
        /// Cancels the current panning operation and reverts the viewport to its initial location if <see cref="AllowPanningCancellation"/> is true.
        /// Otherwise, it ends the panning operation by calling <see cref="EndPanning"/>.
        /// </summary>
        /// <remarks>This method has no effect if there's no panning operation in progress.</remarks>
        public void CancelPanning()
        {
            if (!AllowPanningCancellation)
            {
                EndPanning();
                return;
            }

            if (IsPanning)
            {
                ViewportLocation = _initialPanningLocation;
                IsPanning = false;
            }
        }

        #region Auto panning

        private readonly MouseEventArgs _autoPanningEventArgs = new MouseEventArgs(Mouse.PrimaryDevice, 0, Stylus.CurrentStylusDevice)
        {
            RoutedEvent = MouseMoveEvent
        };

        private void HandleAutoPanning(object? sender, EventArgs e)
        {
            if (!IsPanning && IsMouseCaptureWithin)
            {
                Point mousePosition = Mouse.GetPosition(this);
                double edgeDistance = AutoPanEdgeDistance;
                double autoPanSpeed = Math.Min(AutoPanSpeed, AutoPanSpeed * AutoPanningTickRate) / (ViewportZoom * 2);
                double x = ViewportLocation.X;
                double y = ViewportLocation.Y;

                if (mousePosition.X <= edgeDistance)
                {
                    x -= autoPanSpeed;
                }
                else if (mousePosition.X >= ActualWidth - edgeDistance)
                {
                    x += autoPanSpeed;
                }

                if (mousePosition.Y <= edgeDistance)
                {
                    y -= autoPanSpeed;
                }
                else if (mousePosition.Y >= ActualHeight - edgeDistance)
                {
                    y += autoPanSpeed;
                }

                ViewportLocation = new Point(x, y);
                MouseLocation = Mouse.GetPosition(ItemsHost);

                _autoPanningEventArgs.Handled = false;
                _autoPanningEventArgs.Source = this;
                InputProcessor.ProcessEvent(_autoPanningEventArgs);
            }
        }

        /// <summary>
        /// Called when the <see cref="DisableAutoPanning"/> changes.
        /// </summary>
        /// <param name="shouldDisable">Whether to enable or disable auto panning.</param>
        private void OnDisableAutoPanningChanged(bool shouldDisable)
        {
            ClearTimer();
            if (!shouldDisable)
            {
                _autoPanningTimer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher)
                {
                    Interval = TimeSpan.FromMilliseconds(AutoPanningTickRate)
                };
                _autoPanningTimer.Tick += HandleAutoPanning;
                _autoPanningTimer.Start();
            }

            void ClearTimer()
            {
                if (_autoPanningTimer != null)
                {
                    _autoPanningTimer.Stop();
                    _autoPanningTimer.Tick -= HandleAutoPanning;
                    _autoPanningTimer = null;
                }
            }
        }

        #endregion
    }
}

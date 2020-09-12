using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    [TemplatePart(Name = ElementConnector, Type = typeof(FrameworkElement))]
    public class Connector : Control
    {
        private const string ElementConnector = "PART_Connector";

        #region Routed Events

        public static readonly RoutedEvent PendingConnectionStartedEvent = EventManager.RegisterRoutedEvent(nameof(PendingConnectionStarted), RoutingStrategy.Bubble, typeof(PendingConnectionEventHandler), typeof(Connector));
        public static readonly RoutedEvent PendingConnectionCompletedEvent = EventManager.RegisterRoutedEvent(nameof(PendingConnectionCompleted), RoutingStrategy.Bubble, typeof(PendingConnectionEventHandler), typeof(Connector));
        public static readonly RoutedEvent PendingConnectionDragEvent = EventManager.RegisterRoutedEvent(nameof(PendingConnectionDrag), RoutingStrategy.Bubble, typeof(PendingConnectionEventHandler), typeof(Connector));
        public static readonly RoutedEvent DisconnectEvent = EventManager.RegisterRoutedEvent(nameof(Disconnect), RoutingStrategy.Bubble, typeof(ConnectorEventHandler), typeof(Connector));

        public event PendingConnectionEventHandler PendingConnectionStarted
        {
            add => AddHandler(PendingConnectionStartedEvent, value);
            remove => RemoveHandler(PendingConnectionStartedEvent, value);
        }

        public event PendingConnectionEventHandler PendingConnectionCompleted
        {
            add => AddHandler(PendingConnectionCompletedEvent, value);
            remove => RemoveHandler(PendingConnectionCompletedEvent, value);
        }

        public event PendingConnectionEventHandler PendingConnectionDrag
        {
            add => AddHandler(PendingConnectionDragEvent, value);
            remove => RemoveHandler(PendingConnectionDragEvent, value);
        }

        public event ConnectorEventHandler Disconnect
        {
            add => AddHandler(DisconnectEvent, value);
            remove => RemoveHandler(DisconnectEvent, value);
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty AnchorProperty = DependencyProperty.Register(nameof(Anchor), typeof(Point), typeof(Connector), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register(nameof(IsConnected), typeof(bool), typeof(Connector), new FrameworkPropertyMetadata(BoxValue.False, OnIsConnectedChanged));
        public static readonly DependencyProperty DisconnectCommandProperty = DependencyProperty.Register(nameof(DisconnectCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty StartPendingConnectionCommandProperty = DependencyProperty.Register(nameof(StartPendingConnectionCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty CompletePendingConnectionCommandProperty = DependencyProperty.Register(nameof(CompletePendingConnectionCommand), typeof(ICommand), typeof(NodifyEditor));

        public Point Anchor
        {
            get => (Point)GetValue(AnchorProperty);
            set => SetValue(AnchorProperty, value);
        }

        public bool IsConnected
        {
            get => (bool)GetValue(IsConnectedProperty);
            set => SetValue(IsConnectedProperty, value);
        }

        public ICommand DisconnectCommand
        {
            get => (ICommand)GetValue(DisconnectCommandProperty);
            set => SetValue(DisconnectCommandProperty, value);
        }

        public ICommand StartPendingConnectionCommand
        {
            get => (ICommand)GetValue(StartPendingConnectionCommandProperty);
            set => SetValue(StartPendingConnectionCommandProperty, value);
        }

        public ICommand CompletePendingConnectionCommand
        {
            get => (ICommand)GetValue(CompletePendingConnectionCommandProperty);
            set => SetValue(CompletePendingConnectionCommandProperty, value);
        }

        #endregion

        static Connector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connector), new FrameworkPropertyMetadata(typeof(Connector)));
            FocusableProperty.OverrideMetadata(typeof(Connector), new FrameworkPropertyMetadata(BoxValue.True));
        }

        protected FrameworkElement? Thumb { get; private set; }
        protected ItemContainer? Container { get; private set; }
        protected NodifyEditor? Editor { get; private set; }

        public static double OptimizeMinDistance = 1000d;
        public static double OptimizeMinSelection = 100;
        public static bool EnableOptimizations = true;
        private Point _lastUpdatedContainerPosition;
        private Point _thumbCenter;
        private bool _isHooked;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Thumb = Template.FindName(ElementConnector, this) as FrameworkElement ?? this;

            Container = this.GetParentOfType<ItemContainer>();
            Editor = this.GetParentOfType<NodifyEditor>();

            Loaded += OnConnectorLoaded;
            Unloaded += OnConnectorUnloaded;
        }

        #region Update connector

        private void TrySetAnchorUpdateEvents(bool value)
        {
            if (Container != null && Editor != null)
            {
                // If events are not already hooked and we are asked to subscribe
                if (value && !_isHooked)
                {
                    Container.PreviewLocationChanged += UpdateAnchorOptimized;
                    Container.LocationChanged += OnLocationChanged;
                    Editor.ViewportUpdated += OnViewportUpdated;
                    _isHooked = true;
                }
                // If events are already hooked and we are asked to unsubscribe
                else if (_isHooked && !value)
                {
                    Container.PreviewLocationChanged -= UpdateAnchorOptimized;
                    Container.LocationChanged -= OnLocationChanged;
                    Editor.ViewportUpdated -= OnViewportUpdated;
                }
            }
        }

        private void OnConnectorLoaded(object sender, RoutedEventArgs? e)
            => TrySetAnchorUpdateEvents(true);

        private void OnConnectorUnloaded(object sender, RoutedEventArgs e)
            => TrySetAnchorUpdateEvents(false);

        private static void OnIsConnectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var con = (Connector)d;
            con.UpdateAnchor();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            // Subscribe to events if not already subscribed 
            // Useful for advanced connectors that start collapsed because the loaded event is not called
            var newSize = sizeInfo.NewSize;
            if (newSize.Width > 0 || newSize.Height > 0)
            {
                TrySetAnchorUpdateEvents(true);

                UpdateAnchorOptimized(Container!.Location);
            }
        }

        private void OnLocationChanged(object sender, RoutedEventArgs e)
            => UpdateAnchorOptimized(Container!.Location);

        private void OnViewportUpdated(object sender, RoutedEventArgs args)
        {
            if (Container != null && !Container.IsPreviewingLocation && _lastUpdatedContainerPosition != Container.Location)
            {
                UpdateAnchorOptimized(Container.Location);
            }
        }

        private void UpdateAnchorOptimized(Point location)
        {
            // Update only connectors that are connected
            if (Editor != null && IsConnected)
            {
                bool shouldOptimize = EnableOptimizations && Editor.SelectedItems?.Count > OptimizeMinSelection;

                if (shouldOptimize)
                {
                    UpdateAnchorBasedOnLocation(Editor, location);
                }
                else
                {
                    UpdateAnchor(location);
                }
            }
        }

        protected void UpdateAnchorBasedOnLocation(NodifyEditor editor, Point location)
        {
            var viewport = editor.Viewport;
            var offset = OptimizeMinDistance / editor.Scale;

            var area = Rect.Inflate(viewport, offset, offset);

            // Update only the connectors that are in the viewport or will be in the viewport
            if (area.Contains(location))
            {
                UpdateAnchor(location);
            }
        }

        protected void UpdateAnchor(Point location)
        {
            _lastUpdatedContainerPosition = location;

            if (Thumb != null && Container != null)
            {
                var thumbSize = (Vector)Thumb.RenderSize;
                var containerMargin = (Vector)Container.RenderSize - (Vector)Container.DesiredSize;
                Point relativeLocation = Thumb.TranslatePoint((Point)(thumbSize / 2 - containerMargin / 2), Container);
                Anchor = new Point(location.X + relativeLocation.X, location.Y + relativeLocation.Y);
            }
        }

        public void UpdateAnchor()
        {
            if (Container != null)
            {
                UpdateAnchor(Container.Location);
            }
        }

        #endregion

        #region Event Handlers

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Focus();
            if (Keyboard.Modifiers == ModifierKeys.Alt)
            {
                OnDisconnect();
            }
            else
            {
                UpdateAnchor();
                OnConnectorDragStarted();

                CaptureMouse();
            }

            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            Focus();

            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
                OnConnectorDragCompleted();
            }

            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                var offset = e.GetPosition(Thumb) - _thumbCenter;
                OnConnectorDrag(offset);

                e.Handled = true;
            }
        }

        protected virtual void OnConnectorDrag(Vector offset)
        {
            var args = new PendingConnectionEventArgs(DataContext)
            {
                RoutedEvent = PendingConnectionDragEvent,
                OffsetX = offset.X,
                OffsetY = offset.Y,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);
        }

        protected virtual void OnConnectorDragStarted()
        {
            if (Thumb != null)
            {
                _thumbCenter = new Point(Thumb.ActualWidth / 2, Thumb.ActualHeight / 2);
            }

            var args = new PendingConnectionEventArgs(DataContext)
            {
                RoutedEvent = PendingConnectionStartedEvent,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);

            if (!args.Handled && (StartPendingConnectionCommand?.CanExecute(DataContext) ?? false))
            {
                StartPendingConnectionCommand.Execute(DataContext);
            }
        }

        protected virtual void OnConnectorDragCompleted()
        {
            FrameworkElement? elem = null;
            if (Editor != null)
            {
                elem = PendingConnection.GetPotentialConnector(Editor, PendingConnection.GetAllowOnlyConnectorsAttached(Editor));
            }

            var target = elem?.DataContext;

            var args = new PendingConnectionEventArgs(DataContext)
            {
                TargetConnector = target,
                RoutedEvent = PendingConnectionCompletedEvent,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);

            if (!args.Handled && (CompletePendingConnectionCommand?.CanExecute(target) ?? false))
            {
                CompletePendingConnectionCommand.Execute(target);
            }
        }

        protected virtual void OnDisconnect()
        {
            if (IsConnected)
            {
                var connector = DataContext;
                var args = new ConnectorEventArgs(connector)
                {
                    RoutedEvent = DisconnectEvent,
                    Anchor = Anchor,
                    Source = this
                };

                RaiseEvent(args);

                if (!args.Handled && (DisconnectCommand?.CanExecute(connector) ?? false))
                {
                    DisconnectCommand.Execute(connector);
                }
            }
        }

        #endregion
    }
}

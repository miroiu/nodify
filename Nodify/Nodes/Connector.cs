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
        public static readonly DependencyProperty IsPendingConnectionOverProperty = DependencyProperty.Register(nameof(IsPendingConnectionOver), typeof(bool), typeof(Connector), new FrameworkPropertyMetadata(BoxValue.False));
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

        public bool IsPendingConnectionOver
        {
            get => (bool)GetValue(IsPendingConnectionOverProperty);
            set => SetValue(IsPendingConnectionOverProperty, value);
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Thumb = Template.FindName(ElementConnector, this) as FrameworkElement;

            Container = this.GetParentOfType<ItemContainer>();
            Editor = this.GetParentOfType<NodifyEditor>();

            Loaded += OnConnectorLoaded;
            Unloaded += OnConnectorUnloaded;

            // The loaded event will not get called if the initial visibility is collapsed
            if (Container != null && Editor != null)
            {
                Container.PreviewLocationChanged += UpdateConnectorOptimized;
                Container.LocationChanged += OnLocationChanged;
                Editor.ViewportUpdated += OnViewportUpdated;
            }
        }

        private static void OnIsConnectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var con = (Connector)d;
            con.UpdateConnectorSafe();
        }

        #region Update connector

        private void OnConnectorLoaded(object sender, RoutedEventArgs e)
        {
            if (Container != null && Editor != null)
            {
                UpdateConnector(Container.Location);

                Container.PreviewLocationChanged += UpdateConnectorOptimized;
                Container.LocationChanged += OnLocationChanged;
                Editor.ViewportUpdated += OnViewportUpdated;
            }
        }

        private void OnConnectorUnloaded(object sender, RoutedEventArgs e)
        {
            if (Container != null && Editor != null)
            {
                Container.PreviewLocationChanged -= UpdateConnectorOptimized;
                Container.LocationChanged -= OnLocationChanged;
                Editor.ViewportUpdated -= OnViewportUpdated;
            }
        }

        private void OnLocationChanged(object sender, RoutedEventArgs e)
        {
            UpdateConnectorOptimized(Container!.Location);
        }

        private void OnViewportUpdated(object sender, RoutedEventArgs args)
        {
            if (Container != null && !Container.IsPreviewingLocation && _lastUpdatedContainerPosition != Container.Location)
            {
                UpdateConnectorOptimized(Container.Location);
            }
        }

        private void UpdateConnectorOptimized(Point location)
        {
            // Update only connectors that are connected
            if (Editor != null && IsConnected)
            {
                bool shouldOptimize = EnableOptimizations && Editor.SelectedItems?.Count > OptimizeMinSelection;

                if (shouldOptimize)
                {
                    UpdateConnectorBasedOnLocation(Editor, location);
                }
                else
                {
                    UpdateConnector(location);
                }
            }
        }

        protected void UpdateConnectorBasedOnLocation(NodifyEditor editor, Point location)
        {
            var viewport = editor.Viewport;
            var offset = OptimizeMinDistance / editor.Scale;

            var area = Rect.Inflate(viewport, offset, offset);

            // Update only the connectors that are in the viewport or will be in the viewport
            if (area.Contains(location))
            {
                UpdateConnector(location);
            }
        }

        protected void UpdateConnector(Point location)
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

        protected void UpdateConnectorSafe()
        {
            if (Container != null)
            {
                UpdateConnector(Container.Location);
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
                UpdateConnectorSafe();
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

            if (!args.Handled)
            {
                StartPendingConnectionCommand?.Execute(DataContext);
            }
        }

        protected virtual void OnConnectorDragCompleted()
        {
            FrameworkElement? elem = null;
            if (Editor != null)
            {
                elem = GetTargetUnderMouse(Editor);
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

            if (!args.Handled)
            {
                CompletePendingConnectionCommand?.Execute(DataContext);
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

                if (!args.Handled)
                {
                    DisconnectCommand?.Execute(connector);
                }
            }
        }

        private FrameworkElement? GetTargetUnderMouse(FrameworkElement container)
        {
            FrameworkElement? connector = container.GetElementUnderMouse<Connector>();

            if (Editor != null)
            {
                if (connector == null && !PendingConnection.GetAllowOnlyConnectorsAttached(Editor))
                {
                    connector = container.GetElementUnderMouse<Node>() ?? container.GetElementUnderMouse<FrameworkElement>();
                }
            }

            return connector;
        }

        #endregion
    }
}

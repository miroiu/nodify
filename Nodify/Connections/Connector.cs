using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Represents a connector control that can start and complete a <see cref="PendingConnection"/>.
    /// Has a <see cref="ElementConnector"/> that the <see cref="Anchor"/> is calculated from for the <see cref="PendingConnection"/>. Center of this control is used if missing.
    /// </summary>
    [TemplatePart(Name = ElementConnector, Type = typeof(FrameworkElement))]
    public partial class Connector : WpfControl
    {
        protected const string ElementConnector = "PART_Connector";

        #region Routed Events

        public static readonly RoutedEvent<PendingConnectionEventArgs> PendingConnectionStartedEvent = RoutedEvent.Register<PendingConnectionEventArgs>(nameof(PendingConnectionStarted), RoutingStrategies.Bubble, typeof(Connector));
        public static readonly RoutedEvent<PendingConnectionEventArgs> PendingConnectionCompletedEvent = RoutedEvent.Register<PendingConnectionEventArgs>(nameof(PendingConnectionCompleted), RoutingStrategies.Bubble, typeof(Connector));
        public static readonly RoutedEvent<PendingConnectionEventArgs> PendingConnectionDragEvent = RoutedEvent.Register<PendingConnectionEventArgs>(nameof(PendingConnectionDrag), RoutingStrategies.Bubble, typeof(Connector));
        public static readonly RoutedEvent<ConnectorEventArgs> DisconnectEvent = RoutedEvent.Register<ConnectorEventArgs>(nameof(Disconnect), RoutingStrategies.Bubble, typeof(Connector));

        /// <summary>Triggered by the <see cref="EditorGestures.ConnectorGestures.Connect"/> gesture.</summary>
        public event PendingConnectionEventHandler PendingConnectionStarted
        {
            add => AddHandler(PendingConnectionStartedEvent, value);
            remove => RemoveHandler(PendingConnectionStartedEvent, value);
        }

        /// <summary>Triggered by the <see cref="EditorGestures.ConnectorGestures.Connect"/> gesture.</summary>
        public event PendingConnectionEventHandler PendingConnectionCompleted
        {
            add => AddHandler(PendingConnectionCompletedEvent, value);
            remove => RemoveHandler(PendingConnectionCompletedEvent, value);
        }

        /// <summary>
        /// Occurs when the mouse is changing position and the <see cref="Connector"/> has mouse capture.
        /// </summary>
        public event PendingConnectionEventHandler PendingConnectionDrag
        {
            add => AddHandler(PendingConnectionDragEvent, value);
            remove => RemoveHandler(PendingConnectionDragEvent, value);
        }

        /// <summary>Triggered by the <see cref="EditorGestures.ConnectorGestures.Disconnect"/> gesture.</summary>
        public event ConnectorEventHandler Disconnect
        {
            add => AddHandler(DisconnectEvent, value);
            remove => RemoveHandler(DisconnectEvent, value);
        }

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<Point> AnchorProperty = AvaloniaProperty.Register<Connector, Point>(nameof(Anchor), BoxValue.Point);
        public static readonly StyledProperty<bool> IsConnectedProperty = AvaloniaProperty.Register<Connector, bool>(nameof(IsConnected), BoxValue.False);
        public static readonly StyledProperty<ICommand> DisconnectCommandProperty = AvaloniaProperty.Register<Connector, ICommand>(nameof(DisconnectCommand));
        public static readonly DirectProperty<Connector, bool> IsPendingConnectionProperty = AvaloniaProperty.RegisterDirect<Connector, bool>(nameof(IsPendingConnection), x => x.IsPendingConnection);

        /// <summary>
        /// Gets the location where <see cref="Connection"/>s can be attached to. 
        /// Bind with <see cref="System.Windows.Data.BindingMode.OneWayToSource"/>
        /// </summary>
        public Point Anchor
        {
            get => (Point)GetValue(AnchorProperty);
            set => SetValue(AnchorProperty, value);
        }

        /// <summary>
        /// If this is set to false, the <see cref="Disconnect"/> event will not be invoked and the connector will stop updating its <see cref="Anchor"/> when moved, resized etc.
        /// </summary>
        public bool IsConnected
        {
            get => (bool)GetValue(IsConnectedProperty);
            set => SetValue(IsConnectedProperty, value);
        }

        private bool isPendingConnection;
        /// <summary>
        /// Gets a value that indicates whether a <see cref="PendingConnection"/> is in progress for this <see cref="Connector"/>.
        /// </summary>
        public bool IsPendingConnection
        {
            get => isPendingConnection;
            private set => SetAndRaise(IsPendingConnectionProperty, ref isPendingConnection, value);
        }

        /// <summary>
        /// Invoked if the <see cref="Disconnect"/> event is not handled.
        /// Parameter is the <see cref="FrameworkElement.DataContext"/> of this control.
        /// </summary>
        public ICommand? DisconnectCommand
        {
            get => (ICommand?)GetValue(DisconnectCommandProperty);
            set => SetValue(DisconnectCommandProperty, value);
        }

        #endregion

        static Connector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connector), new FrameworkPropertyMetadata(typeof(Connector)));
            FocusableProperty.OverrideDefaultValue<Connector>(true);
            IsConnectedProperty.Changed.AddClassHandler<Connector>(OnIsConnectedChanged);
        }

        #region Fields

        /// <summary>
        /// Gets the <see cref="FrameworkElement"/> used to calculate the <see cref="Anchor"/>.
        /// </summary>
        protected FrameworkElement? Thumb { get; private set; }

        /// <summary>
        /// Gets the <see cref="ItemContainer"/> that contains this <see cref="Connector"/>.
        /// </summary>
        protected ItemContainer? Container { get; private set; }

        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="Container"/>.
        /// </summary>
        protected internal NodifyEditor? Editor { get; private set; }

        /// <summary>
        /// Gets or sets the safe zone outside the editor's viewport that will not trigger optimizations.
        /// </summary>
        public static double OptimizeSafeZone = 1000d;

        /// <summary>
        /// Gets or sets the minimum selected items needed to trigger optimizations when outside of the <see cref="OptimizeSafeZone"/>.
        /// </summary>
        public static uint OptimizeMinimumSelectedItems = 100;

        /// <summary>
        /// Gets or sets if <see cref="Connector"/>s should enable optimizations based on <see cref="OptimizeSafeZone"/> and <see cref="OptimizeMinimumSelectedItems"/>.
        /// </summary>
        public static bool EnableOptimizations = false;

        /// <summary>
        /// Gets or sets whether cancelling a pending connection is allowed.
        /// </summary>
        public static bool AllowPendingConnectionCancellation { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the connection should be completed in two steps.
        /// </summary>
        public static bool EnableStickyConnections { get; set; }

        private Point _lastUpdatedContainerPosition;
        private Point _thumbCenter;
        private bool _isHooked;

        #endregion

        /// <inheritdoc />
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            Thumb = e.NameScope.Find<Control>(ElementConnector) ?? this;

            Container = this.GetParentOfType<ItemContainer>();
            Editor = Container?.Editor ?? this.GetParentOfType<NodifyEditor>();

            Loaded += OnConnectorLoaded;
            Unloaded += OnConnectorUnloaded;
        }

        #region Update connector

        // Toggle events that could be used to update the Anchor
        private void TrySetAnchorUpdateEvents(bool value)
        {
            if (Container != null && Editor != null)
            {
                // If events are not already hooked and we are asked to subscribe
                if (value && !_isHooked)
                {
                    Container.PreviewLocationChanged += UpdateAnchorOptimized;
                    Container.LocationChanged += OnLocationChanged;
                    Container.SizeChanged += OnContainerSizeChanged;
                    // I don't think this is actually needed?
                    //Editor.ViewportUpdated += OnViewportUpdated;
                    _isHooked = true;
                }
                // If events are already hooked and we are asked to unsubscribe
                else if (_isHooked && !value)
                {
                    Container.PreviewLocationChanged -= UpdateAnchorOptimized;
                    Container.LocationChanged -= OnLocationChanged;
                    Container.SizeChanged -= OnContainerSizeChanged;
                    //Editor.ViewportUpdated -= OnViewportUpdated;
                    _isHooked = false;
                }
            }
        }

        private void OnContainerSizeChanged(object? sender, SizeChangedEventArgs e)
            => UpdateAnchorOptimized(Container!.Location);

        private void OnConnectorLoaded(object? sender, RoutedEventArgs? e)
            => TrySetAnchorUpdateEvents(true);

        private void OnConnectorUnloaded(object? sender, RoutedEventArgs e)
            => TrySetAnchorUpdateEvents(false);

        private static void OnIsConnectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var con = (Connector)d;

            if ((bool)e.NewValue)
            {
                con.UpdateAnchor();
            }
        }

        /// <inheritdoc />
        protected override void OnSizeChanged(SizeChangedInfo sizeInfo)
        {
            // Subscribe to events if not already subscribed 
            // Useful for advanced connectors that start collapsed because the loaded event is not called
            Size newSize = sizeInfo.NewSize;
            if (newSize.Width > 0d || newSize.Height > 0d)
            {
                TrySetAnchorUpdateEvents(true);

                if (Container != null)
                {
                    UpdateAnchorOptimized(Container!.Location);
                }
            }
        }

        private void OnLocationChanged(object? sender, RoutedEventArgs e)
            => UpdateAnchorOptimized(Container!.Location);

        private void OnViewportUpdated(object? sender, RoutedEventArgs args)
        {
            if (Container != null && !Container.IsPreviewingLocation && _lastUpdatedContainerPosition != Container.Location)
            {
                UpdateAnchorOptimized(Container.Location);
            }
        }

        /// <summary>
        /// Updates the <see cref="Anchor"/> and applies optimizations if needed based on <see cref="EnableOptimizations"/> flag
        /// </summary>
        /// <param name="location"></param>
        protected void UpdateAnchorOptimized(Point location)
        {
            // Update only connectors that are connected
            if (Editor != null && IsConnected)
            {
                bool shouldOptimize = EnableOptimizations && Editor.SelectedItems?.Count > OptimizeMinimumSelectedItems;

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

        private void UpdateAnchorBasedOnLocation(NodifyEditor editor, Point location)
        {
            var viewport = new Rect(editor.ViewportLocation, editor.ViewportSize);
            double offset = OptimizeSafeZone / editor.ViewportZoom;

            Rect area = viewport.Inflate(offset);

            // Update only the connectors that are in the viewport or will be in the viewport
            if (area.Contains(location))
            {
                UpdateAnchor(location);
            }
        }

        /// <summary>
        /// Updates the <see cref="Anchor"/> relative to a location. (usually <see cref="Container"/>'s location)
        /// </summary>
        /// <param name="location">The relative location</param>
        protected void UpdateAnchor(Point location)
        {
            _lastUpdatedContainerPosition = location;

            if (Thumb != null && Container != null)
            {
                var thumbSize = Thumb.Bounds.Size.ToVector() /*RenderSize*/;
                Vector containerMargin = Container.Bounds.Size.ToVector() /*RenderSize */ - Container.DesiredSize.ToVector();
                Point relativeLocation = Thumb.TranslatePoint((Point)(thumbSize / 2 - containerMargin / 2), Container) ?? default;
                SetCurrentValue(AnchorProperty, new Point(location.X + relativeLocation.X, location.Y + relativeLocation.Y));
            }
        }

        /// <summary>
        /// Updates the <see cref="Anchor"/> based on <see cref="Container"/>'s location.
        /// </summary>
        public void UpdateAnchor()
        {
            if (Container != null)
            {
                UpdateAnchor(Container.Location);
            }
        }

        #endregion

        #region Event Handlers

        /// <inheritdoc />
        protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
        {
            if (ignoreNextOnPointerCaptureLost)
            {
                ignoreNextOnPointerCaptureLost = false;
                return;
            }
            // Always cancel if lost capture
            OnConnectorDragCompleted(cancel: true, null);
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Focus();

            this.CaptureMouseSafe();

            e.Handled = true;

            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (gestures.Disconnect.Matches(e.Source, e))
            {
                OnDisconnect();
            }
            else if (gestures.Connect.Matches(e.Source, e))
            {
                if (EnableStickyConnections && IsPendingConnection)
                {
                    OnConnectorDragCompleted(e: e);
                }
                else
                {
                    UpdateAnchor();
                    OnConnectorDragStarted(e);
                }
            }
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            bool releaseMouseCapture = false;
            // Don't select the ItemContainer when starting a pending connecton for sticky connections
            e.Handled = EnableStickyConnections && IsPendingConnection;

            EditorGestures.ConnectorGestures gestures = EditorGestures.Mappings.Connector;
            if (!EnableStickyConnections && gestures.Connect.Matches(e.Source, e))
            {
                OnConnectorDragCompleted(e: e);
                e.Handled = true;
            }
            else if (AllowPendingConnectionCancellation && IsPendingConnection && gestures.CancelAction.Matches(e.Source, e))
            {
                // Cancel pending connection
                OnConnectorDragCompleted(cancel: true, e: e);
                ReleaseMouseCapture();
                releaseMouseCapture = true;

                // Don't show context menu
                e.Handled = true;
            }

            if (IsMouseCaptured && !IsPendingConnection)
            {
                ReleaseMouseCapture();
                releaseMouseCapture = true;
            }
            
            // Avalonia hack: Avalonia contrary to WPF automatically releases mouse capture on mouse up
            // and there is no way to prevent it. So we need to capture it again if we are still dragging
            if (!releaseMouseCapture && EnableStickyConnections && IsPendingConnection)
            {
                ignoreNextOnPointerCaptureLost = true;
                Dispatcher.UIThread.Post(() =>
                {
                    capturedMouse = e.Capture(this);
                    ignoreNextOnPointerCaptureLost = false;
                });
            }
        }

        /// <inheritdoc />
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (AllowPendingConnectionCancellation && EditorGestures.Mappings.Connector.CancelAction.Matches(e.Source, e))
            {
                // Cancel pending connection
                OnConnectorDragCompleted(cancel: true);
                ReleaseMouseCapture();
            }
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsPendingConnection)
            {
                Vector offset = e.GetPosition(Thumb) - _thumbCenter;
                OnConnectorDrag(offset, e);
            }
        }

        protected virtual void OnConnectorDrag(Vector offset, MouseEventArgs e)
        {
            var args = new PendingConnectionEventArgs(DataContext, e)
            {
                RoutedEvent = PendingConnectionDragEvent,
                OffsetX = offset.X,
                OffsetY = offset.Y,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);
        }

        protected virtual void OnConnectorDragStarted(MouseButtonEventArgs e)
        {
            if (Thumb != null)
            {
                _thumbCenter = new Point(Thumb.Bounds.Width / 2, Thumb.Bounds.Height / 2);
            }

            var args = new PendingConnectionEventArgs(DataContext, e)
            {
                RoutedEvent = PendingConnectionStartedEvent,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);
            IsPendingConnection = !args.Canceled;

            if (IsMouseCaptured && !IsPendingConnection)
            {
                ReleaseMouseCapture();
            }
        }

        protected virtual void OnConnectorDragCompleted(bool cancel = false, MouseButtonEventArgs? e = null)
        {
            if (IsPendingConnection)
            {
                FrameworkElement? elem = Editor != null ? PendingConnection.GetPotentialConnector(Editor, PendingConnection.GetAllowOnlyConnectorsAttached(Editor), e) : null;

                var args = new PendingConnectionEventArgs(DataContext, e)
                {
                    TargetConnector = elem?.DataContext,
                    RoutedEvent = PendingConnectionCompletedEvent,
                    Anchor = Anchor,
                    Source = this,
                    Canceled = cancel
                };

                IsPendingConnection = false;
                RaiseEvent(args);
            }
        }

        protected virtual void OnDisconnect()
        {
            if (IsConnected && !IsPendingConnection)
            {
                object? connector = DataContext;
                var args = new ConnectorEventArgs(connector)
                {
                    RoutedEvent = DisconnectEvent,
                    Anchor = Anchor,
                    Source = this
                };

                RaiseEvent(args);

                // Raise DisconnectCommand if event is Disconnect not handled
                if (!args.Handled && (DisconnectCommand?.CanExecute(connector) ?? false))
                {
                    DisconnectCommand.Execute(connector);
                }
            }
        }

        #endregion
    }
}

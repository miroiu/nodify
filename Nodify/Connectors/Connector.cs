using Nodify.Events;
using Nodify.Interactivity;
using System.Diagnostics;
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
    public class Connector : Control
    {
        protected const string ElementConnector = "PART_Connector";

        #region Routed Events

        public static readonly RoutedEvent PendingConnectionStartedEvent = EventManager.RegisterRoutedEvent(nameof(PendingConnectionStarted), RoutingStrategy.Bubble, typeof(PendingConnectionEventHandler), typeof(Connector));
        public static readonly RoutedEvent PendingConnectionCompletedEvent = EventManager.RegisterRoutedEvent(nameof(PendingConnectionCompleted), RoutingStrategy.Bubble, typeof(PendingConnectionEventHandler), typeof(Connector));
        public static readonly RoutedEvent PendingConnectionDragEvent = EventManager.RegisterRoutedEvent(nameof(PendingConnectionDrag), RoutingStrategy.Bubble, typeof(PendingConnectionEventHandler), typeof(Connector));
        public static readonly RoutedEvent DisconnectEvent = EventManager.RegisterRoutedEvent(nameof(Disconnect), RoutingStrategy.Bubble, typeof(ConnectorEventHandler), typeof(Connector));

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

        public static readonly DependencyProperty AnchorProperty = DependencyProperty.Register(nameof(Anchor), typeof(Point), typeof(Connector), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register(nameof(IsConnected), typeof(bool), typeof(Connector), new FrameworkPropertyMetadata(BoxValue.False, OnIsConnectedChanged));
        public static readonly DependencyProperty DisconnectCommandProperty = DependencyProperty.Register(nameof(DisconnectCommand), typeof(ICommand), typeof(Connector));
        private static readonly DependencyPropertyKey IsPendingConnectionPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPendingConnection), typeof(bool), typeof(Connector), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsPendingConnectionProperty = IsPendingConnectionPropertyKey.DependencyProperty;
        public static readonly DependencyProperty HasCustomContextMenuProperty = NodifyEditor.HasCustomContextMenuProperty.AddOwner(typeof(Connector));

        /// <summary>
        /// Gets the location in graph space coordinates where <see cref="Connection"/>s can be attached to. 
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

        /// <summary>
        /// Gets a value that indicates whether a <see cref="PendingConnection"/> is in progress for this <see cref="Connector"/>.
        /// </summary>
        public bool IsPendingConnection
        {
            get => (bool)GetValue(IsPendingConnectionProperty);
            protected set => SetValue(IsPendingConnectionPropertyKey, value);
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

        /// <summary>
        /// Gets or sets a value indicating whether the connector uses a custom context menu.
        /// </summary>
        /// <remarks>When set to true, the connector handles the right-click event for specific interactions.</remarks>
        public bool HasCustomContextMenu
        {
            get => (bool)GetValue(HasCustomContextMenuProperty);
            set => SetValue(HasCustomContextMenuProperty, value);
        }

        /// <summary>
        /// Gets a value indicating whether the connector has a context menu.
        /// </summary>
        public bool HasContextMenu => ContextMenu != null || HasCustomContextMenu;

        #endregion

        #region Fields

        private FrameworkElement? _thumb;
        /// <summary>
        /// Gets the <see cref="FrameworkElement"/> used to calculate the <see cref="Anchor"/>.
        /// </summary>
        protected internal FrameworkElement Thumb => _thumb ??= Template.FindName(ElementConnector, this) as FrameworkElement ?? this;

        /// <summary>
        /// Gets the <see cref="ItemContainer"/> that contains this <see cref="Connector"/>.
        /// </summary>
        public ItemContainer? Container { get; private set; }

        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="Container"/>.
        /// </summary>
        public NodifyEditor? Editor { get; private set; }

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

        private Point _lastUpdatedContainerPosition;
        private Point _pendingConnectionEndPosition;
        private bool _isHooked;

        #endregion

        static Connector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connector), new FrameworkPropertyMetadata(typeof(Connector)));
            FocusableProperty.OverrideMetadata(typeof(Connector), new FrameworkPropertyMetadata(BoxValue.True));
        }

        public Connector()
        {
            InputProcessor.AddSharedHandlers(this);

            Loaded += OnConnectorLoaded;
            Unloaded += OnConnectorUnloaded;
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Container = this.GetParentOfType<ItemContainer>();
            Editor = Container?.Editor ?? this.GetParentOfType<NodifyEditor>();
        }

        #region Update Anchor

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
                    Editor.ViewportUpdated += OnViewportUpdated;
                    _isHooked = true;
                }
                // If events are already hooked and we are asked to unsubscribe
                else if (_isHooked && !value)
                {
                    Container.PreviewLocationChanged -= UpdateAnchorOptimized;
                    Container.LocationChanged -= OnLocationChanged;
                    Container.SizeChanged -= OnContainerSizeChanged;
                    Editor.ViewportUpdated -= OnViewportUpdated;
                    _isHooked = false;
                }
            }
        }

        private void OnContainerSizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateAnchorOptimized(Container!.Location);

        private void OnConnectorLoaded(object sender, RoutedEventArgs? e)
            => TrySetAnchorUpdateEvents(true);

        private void OnConnectorUnloaded(object sender, RoutedEventArgs e)
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
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
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

        private void OnLocationChanged(object sender, RoutedEventArgs e)
            => UpdateAnchorOptimized(Container!.Location);

        private void OnViewportUpdated(object sender, RoutedEventArgs args)
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
                bool shouldOptimize = EnableOptimizations && Editor.SelectedContainersCount >= OptimizeMinimumSelectedItems;
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

            Rect area = Rect.Inflate(viewport, offset, offset);

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
                var thumbSize = (Vector)Thumb.RenderSize;
                Vector containerMargin = (Vector)Container.RenderSize - (Vector)Container.DesiredSize;
                Point relativeLocation = Thumb.TranslatePoint((Point)(thumbSize / 2 - containerMargin / 2), Container);
                Anchor = new Point(location.X + relativeLocation.X, location.Y + relativeLocation.Y);
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

        #region Gesture Handling

        protected InputProcessor InputProcessor { get; } = new InputProcessor();

        /// <inheritdoc />
        protected override void OnMouseDown(MouseButtonEventArgs e)
            => InputProcessor.ProcessEvent(e);

        /// <inheritdoc />
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            InputProcessor.ProcessEvent(e);

            // Release the mouse capture if all the mouse buttons are released and there's no interaction in progress
            if (!InputProcessor.RequiresInputCapture && IsMouseCaptured && e.RightButton == MouseButtonState.Released && e.LeftButton == MouseButtonState.Released && e.MiddleButton == MouseButtonState.Released)
            {
                ReleaseMouseCapture();
            }
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
            => InputProcessor.ProcessEvent(e);

        /// <inheritdoc />
        protected override void OnMouseWheel(MouseWheelEventArgs e)
            => InputProcessor.ProcessEvent(e);

        /// <inheritdoc />
        protected override void OnLostMouseCapture(MouseEventArgs e)
            => InputProcessor.ProcessEvent(e);

        /// <inheritdoc />
        protected override void OnKeyUp(KeyEventArgs e)
        {
            InputProcessor.ProcessEvent(e);

            // Release the mouse capture if all the mouse buttons are released and there's no interaction in progress
            if (!InputProcessor.RequiresInputCapture && IsMouseCaptured && Mouse.RightButton == MouseButtonState.Released && Mouse.LeftButton == MouseButtonState.Released && Mouse.MiddleButton == MouseButtonState.Released)
            {
                ReleaseMouseCapture();
            }
        }

        /// <inheritdoc />
        protected override void OnKeyDown(KeyEventArgs e)
            => InputProcessor.ProcessEvent(e);

        #endregion

        #region Methods

        /// <summary>
        /// Initiates a new pending connection from this connector (see <see cref="IsPendingConnection"/>).
        /// </summary>
        /// <remarks>This method has no effect if a pending connection is already in progress.</remarks>
        public void BeginConnecting()
            => BeginConnecting(new Vector(0, 0));

        /// <summary>
        /// Initiates a new pending connection from this connector with the specified offset (see <see cref="IsPendingConnection"/>).
        /// </summary>
        /// <remarks>This method has no effect if a pending connection is already in progress.</remarks>
        public void BeginConnecting(Vector offset)
        {
            if (IsPendingConnection)
            {
                return;
            }

            UpdateAnchor();
            _pendingConnectionEndPosition = Anchor + offset;

            var args = new PendingConnectionEventArgs(DataContext)
            {
                RoutedEvent = PendingConnectionStartedEvent,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);
            IsPendingConnection = !args.Canceled;
        }

        /// <summary>
        /// Updates the endpoint of the pending connection by adjusting its position with the specified offset.
        /// </summary>
        /// <param name="offset">The amount to adjust the pending connection's endpoint.</param>
        public void UpdatePendingConnection(Vector offset)
            => UpdatePendingConnection(_pendingConnectionEndPosition + offset);

        /// <summary>
        /// Updates the endpoint of the pending connection to the specified position.
        /// </summary>
        /// <param name="position">The new position for the connection's endpoint.</param>
        public void UpdatePendingConnection(Point position)
        {
            Debug.Assert(IsPendingConnection);

            _pendingConnectionEndPosition = position;

            var args = new PendingConnectionEventArgs(DataContext)
            {
                RoutedEvent = PendingConnectionDragEvent,
                OffsetX = _pendingConnectionEndPosition.X - Anchor.X,
                OffsetY = _pendingConnectionEndPosition.Y - Anchor.Y,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);
        }

        /// <summary>
        /// Cancels the current pending connection without completing it if <see cref="AllowPendingConnectionCancellation"/> is true.
        /// Otherwise, it completes the pending connection by calling <see cref="EndConnecting()"/>.
        /// </summary>
        /// <remarks>This method has no effect if there's no pending connection.</remarks>
        public void CancelConnecting()
        {
            if (!AllowPendingConnectionCancellation)
            {
                EndConnecting();
                return;
            }

            if (IsPendingConnection)
            {
                var args = new PendingConnectionEventArgs(DataContext)
                {
                    RoutedEvent = PendingConnectionCompletedEvent,
                    Anchor = Anchor,
                    Source = this,
                    Canceled = true
                };
                RaiseEvent(args);

                IsPendingConnection = false;
            }
        }

        /// <summary>
        /// Completes the current pending connection.
        /// </summary>
        /// <remarks>
        /// Attempts to identify a target connector near the connection's endpoint and completes the pending connection.
        /// If no target connector is found, the connection may be completed without a valid target.
        /// This method has no effect if there's no pending connection.
        /// </remarks>
        public void EndConnecting()
        {
            if (!IsPendingConnection)
            {
                return;
            }

            FrameworkElement? elem = FindConnectionTarget(_pendingConnectionEndPosition);
            EndConnecting(elem?.DataContext);
        }

        /// <summary>
        /// Completes the current pending connection using the specified connector as the target.
        /// </summary>
        /// <param name="connector">The connector to use as the connection target.</param>
        /// <remarks>This method has no effect if there's no pending connection.</remarks>
        public void EndConnecting(Connector connector)
            => EndConnecting(connector.DataContext);

        private void EndConnecting(object? targetDataContext)
        {
            if (!IsPendingConnection)
            {
                return;
            }

            var args = new PendingConnectionEventArgs(DataContext)
            {
                TargetConnector = targetDataContext,
                RoutedEvent = PendingConnectionCompletedEvent,
                Anchor = Anchor,
                Source = this
            };
            RaiseEvent(args);

            IsPendingConnection = false;
            _pendingConnectionEndPosition = Anchor;
        }

        /// <summary>
        /// Removes all connections associated with this connector.
        /// </summary>
        /// <remarks>This method has no effect if a pending connection is already in progress or the connector is not connected (see <see cref="IsConnected"/>).</remarks>
        public void RemoveConnections()
        {
            if (!IsConnected || IsPendingConnection)
            {
                return;
            }

            object? connector = DataContext;
            var args = new ConnectorEventArgs(connector)
            {
                RoutedEvent = DisconnectEvent,
                Anchor = Anchor,
                Source = this
            };

            RaiseEvent(args);

            // Raise DisconnectCommand if event is not handled
            if (!args.Handled && (DisconnectCommand?.CanExecute(connector) ?? false))
            {
                DisconnectCommand.Execute(connector);
            }
        }

        /// <summary>
        /// Translates the event location to graph space coordinates (relative to the <see cref="NodifyEditor.ItemsHost" />).
        /// </summary>
        /// <param name="e">The mouse event.</param>
        /// <remarks>
        /// Call <see cref="UpdateAnchor()"/> before calling this method if the <see cref="Anchor"/> is not up-to-date.
        /// </remarks>
        internal Point GetLocationInsideEditor(MouseEventArgs e)
        {
            Vector thumbOffset = e.GetPosition(Thumb) - new Point(Thumb.ActualWidth / 2, Thumb.ActualHeight / 2);
            return Anchor + thumbOffset;
        }

        /// <summary>
        /// Searches for a <see cref="Connector"/> at the specified position.
        /// </summary>
        /// <param name="position">The position in the editor to check for a connector.</param>
        public Connector? FindTargetConnector(Point position)
        {
            if (Editor != null)
            {
                return (Connector?)PendingConnection.GetPotentialConnector(Editor, position, true);
            }

            return null;
        }

        /// <summary>
        /// Searches for a potential <see cref="Connector"/> or <see cref="ItemContainer"/> at the specified position within the editor.
        /// </summary>
        /// <param name="position">The position in the editor to check for a potential connection target.</param>
        public FrameworkElement? FindConnectionTarget(Point position)
        {
            if (Editor != null)
            {
                return PendingConnection.GetPotentialConnector(Editor, position);
            }

            return null;
        }

        #endregion
    }
}

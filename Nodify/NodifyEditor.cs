using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Nodify
{
    [TemplatePart(Name = ElementItemsHost, Type = typeof(Panel))]
    [StyleTypedProperty(Property = nameof(SelectionRectangleStyle), StyleTargetType = typeof(Rectangle))]
    public class NodifyEditor : MultiSelector
    {
        private const string ElementItemsHost = "PART_ItemsHost";

        #region Cosmetic Dependency Properties

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScaleChanged, ConstrainScaleToRange));
        public static readonly DependencyProperty MinScaleProperty = DependencyProperty.Register(nameof(MinScale), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(0.1d, OnMinimumChanged));
        public static readonly DependencyProperty MaxScaleProperty = DependencyProperty.Register(nameof(MaxScale), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double2, OnMaximumChanged, CoerceMaximum));
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(nameof(Offset), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnOffsetChanged, OnCoerceOffset));
        public static readonly DependencyProperty FocusLocationAnimationDurationProperty = DependencyProperty.Register(nameof(FocusLocationAnimationDuration), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.DoubleHalf));
        public static readonly DependencyProperty DisableAutoPanningProperty = DependencyProperty.Register(nameof(DisableAutoPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnDisableAutoPanningChanged));
        public static readonly DependencyProperty AutoPanSpeedProperty = DependencyProperty.Register(nameof(AutoPanSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(10d));
        public static readonly DependencyProperty AutoPanEdgeDistanceProperty = DependencyProperty.Register(nameof(AutoPanEdgeDistance), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(30d));
        protected internal static readonly DependencyPropertyKey AppliedTransformPropertyKey = DependencyProperty.RegisterReadOnly(nameof(AppliedTransform), typeof(Transform), typeof(NodifyEditor), new FrameworkPropertyMetadata(new TransformGroup()));
        public static readonly DependencyProperty AppliedTransformProperty = AppliedTransformPropertyKey.DependencyProperty;
        protected internal static readonly DependencyPropertyKey ViewportPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Viewport), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect, OnViewportChanged, OnCoerceViewport));
        public static readonly DependencyProperty ViewportProperty = ViewportPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ConnectionTemplateProperty = DependencyProperty.Register(nameof(ConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty PendingConnectionTemplateProperty = DependencyProperty.Register(nameof(PendingConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty SelectionRectangleStyleProperty = DependencyProperty.Register(nameof(SelectionRectangleStyle), typeof(Style), typeof(NodifyEditor));

        private static void OnViewportChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnViewportUpdated();

        private static object OnCoerceViewport(DependencyObject d, object value)
        {
            var editor = (NodifyEditor)d;
            var offset = editor.Offset;
            var scale = editor.Scale;

            return new Rect(new Point(offset.X / scale, offset.Y / scale), new Size(editor.ActualWidth / scale, editor.ActualHeight / scale));
        }

        private static object OnCoerceOffset(DependencyObject d, object value)
        {
            var editor = (NodifyEditor)d;
            if (editor.DisablePanning)
            {
                return editor.Offset;
            }

            return value;
        }

        private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            editor.OffsetOverride((Point)e.NewValue);
            editor.CoerceValue(ViewportProperty);
            editor.IsPanning = true;
        }

        private static void OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            editor.ScaleOverride((double)e.NewValue);
            editor.CoerceValue(ViewportProperty);
        }

        public Rect Viewport => (Rect)GetValue(ViewportProperty);

        public Point Offset
        {
            get => (Point)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public TransformGroup AppliedTransform => (TransformGroup)GetValue(AppliedTransformProperty);

        public double FocusLocationAnimationDuration
        {
            get => (double)GetValue(FocusLocationAnimationDurationProperty);
            set => SetValue(FocusLocationAnimationDurationProperty, value);
        }

        public bool DisableAutoPanning
        {
            get => (bool)GetValue(DisableAutoPanningProperty);
            set => SetValue(DisableAutoPanningProperty, value);
        }

        public double AutoPanSpeed
        {
            get => (double)GetValue(AutoPanSpeedProperty);
            set => SetValue(AutoPanSpeedProperty, value);
        }

        public double AutoPanEdgeDistance
        {
            get => (double)GetValue(AutoPanEdgeDistanceProperty);
            set => SetValue(AutoPanEdgeDistanceProperty, value);
        }

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public double MinScale
        {
            get => (double)GetValue(MinScaleProperty);
            set => SetValue(MinScaleProperty, value);
        }

        public double MaxScale
        {
            get => (double)GetValue(MaxScaleProperty);
            set => SetValue(MaxScaleProperty, value);
        }

        public DataTemplate ConnectionTemplate
        {
            get => (DataTemplate)GetValue(ConnectionTemplateProperty);
            set => SetValue(ConnectionTemplateProperty, value);
        }

        public DataTemplate PendingConnectionTemplate
        {
            get => (DataTemplate)GetValue(PendingConnectionTemplateProperty);
            set => SetValue(PendingConnectionTemplateProperty, value);
        }

        public Style SelectionRectangleStyle
        {
            get => (Style)GetValue(SelectionRectangleStyleProperty);
            set => SetValue(SelectionRectangleStyleProperty, value);
        }

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(MaxScaleProperty);
            zoom.CoerceValue(ScaleProperty);
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(ScaleProperty);
        }

        private static object CoerceMaximum(DependencyObject d, object value)
        {
            var zoom = (NodifyEditor)d;
            var min = zoom.MinScale;

            return (double)value < min ? min : value;
        }

        private static object ConstrainScaleToRange(DependencyObject d, object value)
        {
            NodifyEditor editor = (NodifyEditor)d;

            if (editor.DisableZooming)
            {
                return editor.Scale;
            }

            double num = (double)value;
            double minimum = editor.MinScale;
            if (num < minimum)
            {
                return minimum;
            }

            double maximum = editor.MaxScale;
            if (num > maximum)
            {
                return maximum;
            }

            return value;
        }

        protected readonly TranslateTransform TranslateTransform = new TranslateTransform();
        protected readonly ScaleTransform ScaleTransform = new ScaleTransform();

        protected virtual void OffsetOverride(Point newValue)
        {
            TranslateTransform.X = -newValue.X;
            TranslateTransform.Y = -newValue.Y;
        }

        protected virtual void ScaleOverride(double newValue)
        {
            ScaleTransform.ScaleX = newValue;
            ScaleTransform.ScaleY = newValue;
        }

        private static void OnDisableAutoPanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnDisableAutoPanningChanged((bool)e.NewValue);

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ConnectionsProperty = DependencyProperty.Register(nameof(Connections), typeof(IEnumerable), typeof(NodifyEditor));
        public static readonly DependencyProperty PendingConnectionProperty = DependencyProperty.Register(nameof(PendingConnection), typeof(object), typeof(NodifyEditor));
        public static readonly DependencyProperty GridCellSizeProperty = DependencyProperty.Register(nameof(GridCellSize), typeof(int), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Int1));
        public static readonly DependencyProperty DisableZoomingProperty = DependencyProperty.Register(nameof(DisableZooming), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty DisablePanningProperty = DependencyProperty.Register(nameof(DisablePanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty EnableRealtimeSelectionProperty = DependencyProperty.Register(nameof(EnableRealtimeSelection), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(NodifyEditor), new FrameworkPropertyMetadata(default(IList), OnSelectedItemsSourceChanged));
        protected internal static readonly DependencyPropertyKey SelectingRectanglePropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectingRectangle), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));
        public static readonly DependencyProperty SelectingRectangleProperty = SelectingRectanglePropertyKey.DependencyProperty;
        protected internal static readonly DependencyPropertyKey IsSelectingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsSelecting), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsSelectingProperty = IsSelectingPropertyKey.DependencyProperty;
        public static readonly DependencyProperty IsPanningProperty = DependencyProperty.Register(nameof(IsPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty MouseLocationProperty = DependencyProperty.Register(nameof(MouseLocation), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point));

        public int GridCellSize
        {
            get => (int)GetValue(GridCellSizeProperty);
            set => SetValue(GridCellSizeProperty, value);
        }

        public IEnumerable Connections
        {
            get => (IEnumerable)GetValue(ConnectionsProperty);
            set => SetValue(ConnectionsProperty, value);
        }

        public object PendingConnection
        {
            get => GetValue(PendingConnectionProperty);
            set => SetValue(PendingConnectionProperty, value);
        }

        public new IList? SelectedItems
        {
            get => (IList?)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        public Rect SelectingRectangle
        {
            get => (Rect)GetValue(SelectingRectangleProperty);
            protected internal set => SetValue(SelectingRectanglePropertyKey, value);
        }

        public bool DisableZooming
        {
            get => (bool)GetValue(DisableZoomingProperty);
            set => SetValue(DisableZoomingProperty, value);
        }

        public bool DisablePanning
        {
            get => (bool)GetValue(DisablePanningProperty);
            set => SetValue(DisablePanningProperty, value);
        }

        public bool IsSelecting
        {
            get => (bool)GetValue(IsSelectingProperty);
            internal set => SetValue(IsSelectingPropertyKey, value);
        }

        public bool EnableRealtimeSelection
        {
            get => (bool)GetValue(EnableRealtimeSelectionProperty);
            set => SetValue(EnableRealtimeSelectionProperty, value);
        }

        public bool IsPanning
        {
            get => (bool)GetValue(IsPanningProperty);
            set => SetValue(IsPanningProperty, value);
        }

        public Point MouseLocation
        {
            get => (Point)GetValue(MouseLocationProperty);
            set => SetValue(MouseLocationProperty, value);
        }

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnSelectedItemsSourceChanged((IList)e.OldValue, (IList)e.NewValue);

        #endregion

        #region Command Dependency Properties
        
        public static readonly DependencyProperty ConnectionCompletedCommandProperty = DependencyProperty.Register(nameof(ConnectionCompletedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty DisconnectConnectorCommandProperty = DependencyProperty.Register(nameof(DisconnectConnectorCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragStartedCommandProperty = DependencyProperty.Register(nameof(ItemsDragStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragCompletedCommandProperty = DependencyProperty.Register(nameof(ItemsDragCompletedCommand), typeof(ICommand), typeof(NodifyEditor));

        public ICommand ConnectionCompletedCommand
        {
            get => (ICommand)GetValue(ConnectionCompletedCommandProperty);
            set => SetValue(ConnectionCompletedCommandProperty, value);
        }

        public ICommand DisconnectConnectorCommand
        {
            get => (ICommand)GetValue(DisconnectConnectorCommandProperty);
            set => SetValue(DisconnectConnectorCommandProperty, value);
        }

        public ICommand ItemsDragStartedCommand
        {
            get => (ICommand)GetValue(ItemsDragStartedCommandProperty);
            set => SetValue(ItemsDragStartedCommandProperty, value);
        }

        public ICommand ItemsDragCompletedCommand
        {
            get => (ICommand)GetValue(ItemsDragCompletedCommandProperty);
            set => SetValue(ItemsDragCompletedCommandProperty, value);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent ViewportUpdatedEvent = EventManager.RegisterRoutedEvent(nameof(ViewportUpdated), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NodifyEditor));

        public void OnViewportUpdated()
            => RaiseEvent(new RoutedEventArgs(ViewportUpdatedEvent, this));

        public event RoutedEventHandler ViewportUpdated
        {
            add => AddHandler(ViewportUpdatedEvent, value);
            remove => RemoveHandler(ViewportUpdatedEvent, value);
        }

        #endregion

        /// <summary>
        /// Value is number of pixels squared.
        /// Useful for <see cref="ContextMenu"/>s to appear if mouse only moved a bit or not at all.
        /// </summary>
        public static double HandleRightClickAfterPanningThreshold { get; set; } = 144d;

        /// <summary>
        /// Correct <see cref="ItemContainer"/>'s position after moving if starting position is not snapped to grid.
        /// </summary>
        public static bool EnableSnappingCorrection { get; set; } = true;

        /// <summary>
        /// How often should we calculate the new <see cref="Offset"/> when dragging or selecting.
        /// </summary>
        /// 
        public static double AutoPanningTimerIntervalMilliseconds { get; set; } = 1;

        /// <summary>
        /// Tells if the <see cref="NodifyEditor"/> is doing operations on multiple items at once.
        /// </summary>
        public bool IsBulkUpdatingItems { get; protected set; }

        /// <summary>
        /// The panel that holds all the <see cref="ItemContainer"/>s.
        /// </summary>
        protected internal Panel? ItemsHost { get; private set; }

        /// <summary>
        /// Helps with selecting <see cref="ItemContainer"/>s and updating the <see cref="SelectingRectangle"/> and <see cref="IsSelecting"/> properties.
        /// </summary>
        protected SelectionHelper Selection { get; private set; }

        /// <summary>
        /// Tells where the mouse cursor was the previous time it moved relative to the <see cref="NodifyEditor"/>.
        /// Check <see cref="MouseLocation"/> for a transformed position.
        /// </summary>
        protected Point PreviousMousePosition;

        /// <summary>
        /// Tells where the mouse cursor is right now relative to the <see cref="NodifyEditor"/>.
        /// Check <see cref="MouseLocation"/> for a transformed position.
        /// </summary>
        protected Point CurrentMousePosition;

        /// <summary>
        /// Tells where the mouse cursor was when the user started interacting with the <see cref="NodifyEditor"/>.
        /// Check <see cref="MouseLocation"/> for a transformed position.
        /// </summary>
        protected Point InitialMousePosition;

        private DispatcherTimer? _autoPanningTimer;

        public NodifyEditor()
        {
            AddHandler(Connector.DisconnectEvent, new ConnectorEventHandler(OnConnectorDisconnected));
            AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnConnectionCompleted));

            AddHandler(DragBehavior.DragStartedEvent, new DragStartedEventHandler(OnItemsDragStarted));
            AddHandler(DragBehavior.DragCompletedEvent, new DragCompletedEventHandler(OnItemsDragCompleted));
            AddHandler(DragBehavior.DragDeltaEvent, new DragDeltaEventHandler(OnItemsDragDelta));

            Selection = new SelectionHelper(this);

            var transform = new TransformGroup();
            transform.Children.Add(ScaleTransform);
            transform.Children.Add(TranslateTransform);

            SetValue(AppliedTransformPropertyKey, transform);

            OnDisableAutoPanningChanged(DisableAutoPanning);
        }

        static NodifyEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodifyEditor), new FrameworkPropertyMetadata(typeof(NodifyEditor)));
            FocusableProperty.OverrideMetadata(typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.True));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsHost = GetTemplateChild(ElementItemsHost) as Panel;
        }

        protected override DependencyObject GetContainerForItemOverride()
            => new ItemContainer()
            {
                // TODO: Make this a TransformGroup and add TranslateTransform to it
                RenderTransform = new TranslateTransform()
            };

        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is ItemContainer;

        #region Auto panning

        private void HandleAutoPanning(object? sender, EventArgs e)
        {
            if (IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed && Mouse.Captured != null)
            {
                var mousePosition = Mouse.GetPosition(this);
                double edgeDistance = AutoPanEdgeDistance;
                double autoPanSpeed = Math.Min(AutoPanSpeed, AutoPanSpeed * AutoPanningTimerIntervalMilliseconds);
                double x = Offset.X;
                double y = Offset.Y;

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

                Offset = new Point(x, y);

                if (IsSelecting)
                {
                    Selection.Update(GetMousePositionTransformed(mousePosition));
                }
            }
        }

        protected virtual void OnDisableAutoPanningChanged(bool shouldDisable)
        {
            if (!shouldDisable)
            {
                if (_autoPanningTimer == null)
                {
                    _autoPanningTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(AutoPanningTimerIntervalMilliseconds), DispatcherPriority.Background, new EventHandler(HandleAutoPanning), Dispatcher);
                }
                else
                {
                    _autoPanningTimer.Interval = TimeSpan.FromMilliseconds(AutoPanningTimerIntervalMilliseconds);
                    _autoPanningTimer.Start();
                }
            }
            else if (shouldDisable && _autoPanningTimer != null)
            {
                _autoPanningTimer.Stop();
            }
        }

        #endregion

        #region Connector handling

        private void OnConnectorDisconnected(object sender, ConnectorEventArgs e)
        {
            if (DisconnectConnectorCommand?.CanExecute(e.Connector) ?? false)
            {
                DisconnectConnectorCommand.Execute(e.Connector);
                e.Handled = true;
            }
        }

        private void OnConnectionCompleted(object sender, PendingConnectionEventArgs e)
        {
            var result = (e.SourceConnector, e.TargetConnector);
            if (ConnectionCompletedCommand?.CanExecute(result) ?? false)
            {
                ConnectionCompletedCommand.Execute(result);
                e.Handled = true;
            }
        }

        #endregion

        #region Mouse Events Handlers

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            var scale = Math.Pow(2, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
            ScaleAtPosition(scale, e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            CurrentMousePosition = e.GetPosition(this);

            if (CurrentMousePosition != PreviousMousePosition)
            {
                MouseLocation = GetMousePositionTransformed(CurrentMousePosition);

                if (Mouse.Captured == this)
                {
                    // Panning
                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        Offset -= CurrentMousePosition - PreviousMousePosition;
                        e.Handled = true;
                    }
                    else if (IsSelecting)
                    {
                        Selection.Update(MouseLocation);
                    }
                    else if (Mouse.Captured == this)
                    {
                        ReleaseMouseCapture();
                    }
                }

                PreviousMousePosition = CurrentMousePosition;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Selection.Start(MouseLocation, EnableRealtimeSelection);

                Focus();
                CaptureMouse();

                InitialMousePosition = e.GetPosition(this);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == this && e.RightButton == MouseButtonState.Released && e.MiddleButton == MouseButtonState.Released)
            {
                Selection.End();

                Focus();
                ReleaseMouseCapture();
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Focus();
                CaptureMouse();

                InitialMousePosition = e.GetPosition(this);
            }
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == this && e.LeftButton == MouseButtonState.Released && e.MiddleButton == MouseButtonState.Released)
            {
                Focus();
                ReleaseMouseCapture();

                if (IsPanning)
                {
                    IsPanning = false;

                    // Handle right click if IsPanning and moved the mouse more than threshold so context menus don't open
                    if ((CurrentMousePosition - InitialMousePosition).LengthSquared > HandleRightClickAfterPanningThreshold)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            CoerceValue(ViewportProperty);
        }

        #endregion

        #region Selection Handlers

        private void OnSelectedItemsSourceChanged(IList oldValue, IList newValue)
        {
            if (oldValue is INotifyCollectionChanged oc)
            {
                oc.CollectionChanged -= OnSelectedItemsChanged;
            }

            if (newValue is INotifyCollectionChanged nc)
            {
                nc.CollectionChanged += OnSelectedItemsChanged;
            }

            var selectedItems = base.SelectedItems;

            BeginUpdateSelectedItems();
            selectedItems.Clear();
            for (int i = 0; i < newValue.Count; i++)
            {
                selectedItems.Add(newValue[i]);
            }
            EndUpdateSelectedItems();
        }

        private void OnSelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    base.SelectedItems.Clear();
                    break;

                case NotifyCollectionChangedAction.Add:
                    var newItems = e.NewItems;
                    if (newItems != null)
                    {
                        var selectedItems = base.SelectedItems;
                        for (int i = 0; i < newItems.Count; i++)
                        {
                            selectedItems.Add(newItems[i]);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var oldItems = e.OldItems;
                    if (oldItems != null)
                    {
                        var selectedItems = base.SelectedItems;
                        for (int i = 0; i < oldItems.Count; i++)
                        {
                            selectedItems.Remove(oldItems[i]);
                        }
                    }
                    break;
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (!IsSelecting)
            {
                var selected = SelectedItems;

                if (selected != null)
                {
                    var added = e.AddedItems;
                    for (int i = 0; i < added.Count; i++)
                    {
                        // Ensure no duplicates are added
                        if (!selected.Contains(added[i]))
                        {
                            selected.Add(added[i]);
                        }
                    }

                    var removed = e.RemovedItems;
                    for (int i = 0; i < removed.Count; i++)
                    {
                        selected.Remove(removed[i]);
                    }
                }
            }
        }

        #endregion

        #region Selection

        public void AppendSelection(Rect area, bool fit = false)
        {
            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.Intersects(area, fit))
                {
                    selected.Add(items[i]);
                }
            }
            EndUpdateSelectedItems();
        }

        public void InvertSelection(Rect area, bool fit = false)
        {
            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.Intersects(area, fit))
                {
                    var item = items[i];
                    if (container.IsSelected)
                    {
                        selected.Remove(item);
                    }
                    else
                    {
                        selected.Add(item);
                    }
                }
            }
            EndUpdateSelectedItems();
        }

        public void SelectArea(Rect area, bool append = false, bool fit = false)
        {
            if (!append)
            {
                UnselectAll();
            }

            AppendSelection(area, fit);
        }

        public void UnselectArea(Rect area, bool fit = false)
        {
            var items = GetItemsInArea(area, fit);
            SetSelectedItems(items, false);
        }

        public void SetSelectedItems(List<ItemContainer> items, bool selected = true)
        {
            BeginUpdateSelectedItems();
            if (selected)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    base.SelectedItems.Add(items[i].DataContext);
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    base.SelectedItems.Remove(items[i].DataContext);
                }
            }
            EndUpdateSelectedItems();
        }

        public List<ItemContainer> GetItemsInArea(Rect area, bool fit = false)
        {
            var items = Items;
            List<ItemContainer> inArea = new List<ItemContainer>(items.Count / 2);

            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.Intersects(area, fit))
                {
                    inArea.Add(container);
                }
            }

            return inArea;
        }

        protected internal void GetItemsInAreaNoAlloc(List<ItemContainer> outItems, Rect area, bool fit = false)
        {
            var items = Items;
            outItems.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.Intersects(area, fit))
                {
                    outItems.Add(container);
                }
            }
        }

        public List<ItemContainer> GetSelectedItems()
        {
            var selectedItems = base.SelectedItems;
            List<ItemContainer> selected = new List<ItemContainer>(selectedItems.Count);

            for (int i = 0; i < selectedItems.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(selectedItems[i]);
                selected.Add(container);
            }

            return selected;
        }

        #endregion

        #region Dragging

        protected ItemContainer? DragInstigator;
        private Vector _dragAccumulator;

        private void OnItemsDragDelta(object sender, DragDeltaEventArgs e)
        {
            // Move selection only if a selected item is being dragged
            if (DragInstigator != null && GridCellSize > 0)
            {
                _dragAccumulator += new Vector(e.HorizontalChange, e.VerticalChange);
                var delta = new Vector((int)_dragAccumulator.X / GridCellSize * GridCellSize, (int)_dragAccumulator.Y / GridCellSize * GridCellSize);
                _dragAccumulator -= delta;

                if (delta.X != 0 || delta.Y != 0)
                {
                    var selectedItems = base.SelectedItems;
                    for (int i = 0; i < selectedItems.Count; i++)
                    {
                        var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(selectedItems[i]);

                        var r = (TranslateTransform)container.RenderTransform;

                        r.X += delta.X; // Snapping without correction
                        r.Y += delta.Y; // Snapping without correction

                        container.OnPreviewLocationChanged(container.Location + new Vector(r.X, r.Y));
                    }
                }
            }
        }

        private void OnItemsDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var selectedItems = base.SelectedItems;
            IsBulkUpdatingItems = true;

            for (int i = 0; i < selectedItems.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(selectedItems[i]);
                var r = (TranslateTransform)container.RenderTransform;

                var result = container.Location + new Vector(r.X, r.Y);

                // Correct the final position
                if (EnableSnappingCorrection)
                {
                    result.X = (int)result.X / GridCellSize * GridCellSize;
                    result.Y = (int)result.Y / GridCellSize * GridCellSize;
                }

                container.Location = result;

                r.X = 0;
                r.Y = 0;
            }

            IsBulkUpdatingItems = false;

            ItemsHost?.InvalidateArrange();

            if (ItemsDragCompletedCommand != null && ItemsDragCompletedCommand.CanExecute(null))
            {
                ItemsDragCompletedCommand.Execute(null);
            }
        }

        private void OnItemsDragStarted(object sender, DragStartedEventArgs e)
        {
            DragInstigator = e.OriginalSource as ItemContainer ?? (e.OriginalSource as UIElement)?.GetParentOfType<ItemContainer>();

            if (DragInstigator != null)
            {
                // Clear the selection if the dragged item is not part of the selection and Control or Shift is not held
                if (!(Keyboard.Modifiers == ModifierKeys.Control || Keyboard.Modifiers == ModifierKeys.Shift || DragInstigator.IsSelected))
                {
                    base.SelectedItems.Clear();
                }

                DragInstigator.IsSelected = true;
            }

            if (ItemsDragStartedCommand != null && ItemsDragStartedCommand.CanExecute(null))
            {
                ItemsDragStartedCommand.Execute(null);
            }
        }

        #endregion

        #region Helpers

        private Point GetMousePositionTransformed(Point location)
            => new Point((Offset.X + location.X) / Scale, (Offset.Y + location.Y) / Scale);

        public void ScaleAtPosition(double scale, Point pos)
        {
            var position = (Vector)pos;
            var prevScale = Scale;

            Scale *= scale;

            if (prevScale != Scale)
            {
                Offset = (Point)((Vector)(Offset + position) * scale - position);
            }
        }

        protected virtual Point PointToViewportCenter(Point point)
            => (Point)((Vector)point * Scale - new Vector(ActualWidth / 2, ActualHeight / 2));

        public void FocusLocation(Point point, bool animated = true)
        {
            Focus();

            if (animated)
            {
                this.StartAnimation(OffsetProperty, PointToViewportCenter(point), FocusLocationAnimationDuration);
            }
            else
            {
                Offset = PointToViewportCenter(point);
            }
        }

        #endregion
    }
}

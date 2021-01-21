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
    /// <summary>
    /// Groups <see cref="ItemContainer"/>s and <see cref="Connection"/>s in an area that you can drag, scale and select.
    /// </summary>
    [TemplatePart(Name = ElementItemsHost, Type = typeof(Panel))]
    [StyleTypedProperty(Property = nameof(SelectionRectangleStyle), StyleTargetType = typeof(Rectangle))]
    public class NodifyEditor : MultiSelector
    {
        protected const string ElementItemsHost = "PART_ItemsHost";

        #region Cosmetic Dependency Properties

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof(Scale), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScaleChanged, ConstrainScaleToRange));
        public static readonly DependencyProperty MinScaleProperty = DependencyProperty.Register(nameof(MinScale), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(0.1d, OnMinimumScaleChanged, CoerceMinimumScale));
        public static readonly DependencyProperty MaxScaleProperty = DependencyProperty.Register(nameof(MaxScale), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double2, OnMaximumScaleChanged, CoerceMaximumScale));
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(nameof(Offset), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnOffsetChanged, OnCoerceOffset));
        public static readonly DependencyProperty BringIntoViewAnimationDurationProperty = DependencyProperty.Register(nameof(BringIntoViewAnimationDuration), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.DoubleHalf));
        public static readonly DependencyProperty DisplayConnectionsOnTopProperty = DependencyProperty.Register(nameof(DisplayConnectionsOnTop), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty DisableAutoPanningProperty = DependencyProperty.Register(nameof(DisableAutoPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnDisableAutoPanningChanged));
        public static readonly DependencyProperty AutoPanSpeedProperty = DependencyProperty.Register(nameof(AutoPanSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(10d));
        public static readonly DependencyProperty AutoPanEdgeDistanceProperty = DependencyProperty.Register(nameof(AutoPanEdgeDistance), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(15d));
        public static readonly DependencyProperty ConnectionTemplateProperty = DependencyProperty.Register(nameof(ConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty PendingConnectionTemplateProperty = DependencyProperty.Register(nameof(PendingConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty SelectionRectangleStyleProperty = DependencyProperty.Register(nameof(SelectionRectangleStyle), typeof(Style), typeof(NodifyEditor));

        #region Callbacks

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
        }

        private static void OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            editor.ScaleOverride((double)e.NewValue);
            editor.CoerceValue(ViewportProperty);
        }

        private static void OnMinimumScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(MaxScaleProperty);
            zoom.CoerceValue(ScaleProperty);
        }

        private static object CoerceMinimumScale(DependencyObject d, object value)
            => (double)value > 0 ? value : 0.01;

        private static void OnMaximumScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(ScaleProperty);
        }

        private static object CoerceMaximumScale(DependencyObject d, object value)
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

        private void OffsetOverride(Point newValue)
        {
            TranslateTransform.X = -newValue.X;
            TranslateTransform.Y = -newValue.Y;
        }

        private void ScaleOverride(double newValue)
        {
            ScaleTransform.ScaleX = newValue;
            ScaleTransform.ScaleY = newValue;
        }

        private static void OnDisableAutoPanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnDisableAutoPanningChanged((bool)e.NewValue);

        #endregion

        /// <summary>
        /// Gets or sets the <see cref="Viewport"/>'s top left coordinates.
        /// </summary>
        public Point Offset
        {
            get => (Point)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        /// <summary>
        /// Gets or sets the animation duration in seconds when bringing a location into view.
        /// </summary>
        public double BringIntoViewAnimationDuration
        {
            get => (double)GetValue(BringIntoViewAnimationDurationProperty);
            set => SetValue(BringIntoViewAnimationDurationProperty, value);
        }

        /// <summary>
        /// Gets or sets whether to display connections on top of nodes or not.
        /// </summary>
        public bool DisplayConnectionsOnTop
        {
            get => (bool)GetValue(DisplayConnectionsOnTopProperty);
            set => SetValue(DisplayConnectionsOnTopProperty, value);
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
        /// Gets or sets the zoom factor of the <see cref="Viewport"/>.
        /// </summary>
        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum zoom factor of the <see cref="Viewport"/>
        /// </summary>
        public double MinScale
        {
            get => (double)GetValue(MinScaleProperty);
            set => SetValue(MinScaleProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum zoom factor of the <see cref="Viewport"/>
        /// </summary>
        public double MaxScale
        {
            get => (double)GetValue(MaxScaleProperty);
            set => SetValue(MaxScaleProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> to use when generating a new <see cref="BaseConnection"/>.
        /// </summary>
        public DataTemplate ConnectionTemplate
        {
            get => (DataTemplate)GetValue(ConnectionTemplateProperty);
            set => SetValue(ConnectionTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> to use for the <see cref="PendingConnection"/>.
        /// </summary>
        public DataTemplate PendingConnectionTemplate
        {
            get => (DataTemplate)GetValue(PendingConnectionTemplateProperty);
            set => SetValue(PendingConnectionTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the style to use for the selection rectangle.
        /// </summary>
        public Style SelectionRectangleStyle
        {
            get => (Style)GetValue(SelectionRectangleStyleProperty);
            set => SetValue(SelectionRectangleStyleProperty, value);
        }

        #endregion

        #region Readonly Dependency Properties

        protected internal static readonly DependencyPropertyKey AppliedTransformPropertyKey = DependencyProperty.RegisterReadOnly(nameof(AppliedTransform), typeof(Transform), typeof(NodifyEditor), new FrameworkPropertyMetadata(new TransformGroup()));
        public static readonly DependencyProperty AppliedTransformProperty = AppliedTransformPropertyKey.DependencyProperty;

        protected internal static readonly DependencyPropertyKey ViewportPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Viewport), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect, OnViewportChanged, OnCoerceViewport));
        public static readonly DependencyProperty ViewportProperty = ViewportPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey SelectedAreaPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedArea), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));
        public static readonly DependencyProperty SelectedAreaProperty = SelectedAreaPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey IsSelectingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsSelecting), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsSelectingProperty = IsSelectingPropertyKey.DependencyProperty;

        public static readonly DependencyPropertyKey IsPanningPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsPanningProperty = IsPanningPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey MouseLocationPropertyKey = DependencyProperty.RegisterReadOnly(nameof(MouseLocation), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty MouseLocationProperty = MouseLocationPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the area of the <see cref="NodifyEditor"/> that is seen on the screen. (<see cref="Offset"/> and <see cref="Scale"/> applied)
        /// </summary>
        public Rect Viewport => (Rect)GetValue(ViewportProperty);

        /// <summary>
        /// Gets the transform that is applied to all child controls.
        /// </summary>
        public Transform AppliedTransform => (Transform)GetValue(AppliedTransformProperty);

        /// <summary>
        /// Gets the currently selected area while <see cref="IsSelecting"/> is true.
        /// </summary>
        public Rect SelectedArea
        {
            get => (Rect)GetValue(SelectedAreaProperty);
            internal set => SetValue(SelectedAreaPropertyKey, value);
        }

        /// <summary>
        /// Gets a value that indicates whether a selection operation is in progress.
        /// </summary>
        public bool IsSelecting
        {
            get => (bool)GetValue(IsSelectingProperty);
            internal set => SetValue(IsSelectingPropertyKey, value);
        }

        /// <summary>
        /// Gets a value that indicates whether a panning operation is in progress.
        /// </summary>
        public bool IsPanning
        {
            get => (bool)GetValue(IsPanningProperty);
            protected set => SetValue(IsPanningPropertyKey, value);
        }

        /// <summary>
        /// Gets the current transformed mouse location using the <see cref="AppliedTransform"/>.
        /// </summary>
        public Point MouseLocation
        {
            get => (Point)GetValue(MouseLocationProperty);
            protected set => SetValue(MouseLocationPropertyKey, value);
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ConnectionsProperty = DependencyProperty.Register(nameof(Connections), typeof(IEnumerable), typeof(NodifyEditor));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(IList), typeof(NodifyEditor), new FrameworkPropertyMetadata(default(IList), OnSelectedItemsSourceChanged));
        public static readonly DependencyProperty PendingConnectionProperty = DependencyProperty.Register(nameof(PendingConnection), typeof(object), typeof(NodifyEditor));
        public static readonly DependencyProperty GridCellSizeProperty = DependencyProperty.Register(nameof(GridCellSize), typeof(uint), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.UInt1, OnGridCellSizeChanged, OnCoerceGridCellSize));
        public static readonly DependencyProperty DisableZoomingProperty = DependencyProperty.Register(nameof(DisableZooming), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty DisablePanningProperty = DependencyProperty.Register(nameof(DisablePanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty EnableRealtimeSelectionProperty = DependencyProperty.Register(nameof(EnableRealtimeSelection), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));

        /// <summary>
        /// Gets or sets the value of an invisible grid used to adjust locations (snapping) of <see cref="ItemContainer"/>s.
        /// </summary>
        public uint GridCellSize
        {
            get => (uint)GetValue(GridCellSizeProperty);
            set => SetValue(GridCellSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the data source that <see cref="BaseConnection"/>s will be generated for.
        /// </summary>
        public IEnumerable Connections
        {
            get => (IEnumerable)GetValue(ConnectionsProperty);
            set => SetValue(ConnectionsProperty, value);
        }

        /// <summary>
        /// Gets of sets the <see cref="FrameworkElement.DataContext"/> of the <see cref="Nodify.PendingConnection"/>.
        /// </summary>
        public object PendingConnection
        {
            get => GetValue(PendingConnectionProperty);
            set => SetValue(PendingConnectionProperty, value);
        }

        /// <summary>
        /// Gets or sets the items in the <see cref="NodifyEditor"/> that are selected.
        /// </summary>
        public new IList? SelectedItems
        {
            get => (IList?)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Gets or sets whether zooming should be disabled.
        /// </summary>
        public bool DisableZooming
        {
            get => (bool)GetValue(DisableZoomingProperty);
            set => SetValue(DisableZoomingProperty, value);
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
        /// Enables selecting and deselecting items while the <see cref="SelectedArea"/> changes.
        /// Disable for maximum performance when hundreds of items are generated.
        /// </summary>
        public bool EnableRealtimeSelection
        {
            get => (bool)GetValue(EnableRealtimeSelectionProperty);
            set => SetValue(EnableRealtimeSelectionProperty, value);
        }

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnSelectedItemsSourceChanged((IList)e.OldValue, (IList)e.NewValue);

        private static object OnCoerceGridCellSize(DependencyObject d, object value)
            => (uint)value > 0u ? value : BoxValue.UInt1;

        private static void OnGridCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        #endregion

        #region Command Dependency Properties

        public static readonly DependencyProperty ConnectionCompletedCommandProperty = DependencyProperty.Register(nameof(ConnectionCompletedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty DisconnectConnectorCommandProperty = DependencyProperty.Register(nameof(DisconnectConnectorCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragStartedCommandProperty = DependencyProperty.Register(nameof(ItemsDragStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragCompletedCommandProperty = DependencyProperty.Register(nameof(ItemsDragCompletedCommand), typeof(ICommand), typeof(NodifyEditor));

        /// <summary>
        /// Invoked when the <see cref="Nodify.PendingConnection"/> is completed. <br />
        /// If you override the <see cref="Nodify.PendingConnection"/> style or <see cref="PendingConnectionTemplate"/>, please use the <see cref="PendingConnection.CompletedCommand"/> instead. <br />
        /// Parameters is <see cref="Tuple{object, object}"/> where <see cref="Tuple{object, object}.Item1"/> is the <see cref="PendingConnection.Source"/> and <see cref="Tuple{object, object}.Item2"/> is <see cref="PendingConnection.Target"/>.
        /// </summary>
        public ICommand ConnectionCompletedCommand
        {
            get => (ICommand)GetValue(ConnectionCompletedCommandProperty);
            set => SetValue(ConnectionCompletedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when the <see cref="Connector.Disconnect"/> event is raised. <br />
        /// Can also be handled at the <see cref="Connector"/> level using the <see cref="Connector.DisconnectCommand"/> command. <br />
        /// Parameter is the <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/>.
        /// </summary>
        public ICommand DisconnectConnectorCommand
        {
            get => (ICommand)GetValue(DisconnectConnectorCommandProperty);
            set => SetValue(DisconnectConnectorCommandProperty, value);
        }

        /// <summary>
        /// Invoked when a drag operation starts for the <see cref="SelectedItems"/>.
        /// </summary>
        public ICommand ItemsDragStartedCommand
        {
            get => (ICommand)GetValue(ItemsDragStartedCommandProperty);
            set => SetValue(ItemsDragStartedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when a drag operation is completed for the <see cref="SelectedItems"/>.
        /// </summary>
        public ICommand ItemsDragCompletedCommand
        {
            get => (ICommand)GetValue(ItemsDragCompletedCommandProperty);
            set => SetValue(ItemsDragCompletedCommandProperty, value);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent ViewportUpdatedEvent = EventManager.RegisterRoutedEvent(nameof(ViewportUpdated), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NodifyEditor));

        /// <summary>
        /// Occurs whenever the <see cref="Viewport"/> changes.
        /// </summary>
        public event RoutedEventHandler ViewportUpdated
        {
            add => AddHandler(ViewportUpdatedEvent, value);
            remove => RemoveHandler(ViewportUpdatedEvent, value);
        }

        /// <summary>
        /// Raises the <see cref="ViewportUpdatedEvent"/>.
        /// Called when the <see cref="Offset"/> or <see cref="Scale"/> is changed.
        /// </summary>
        protected void OnViewportUpdated()
            => RaiseEvent(new RoutedEventArgs(ViewportUpdatedEvent, this));

        #endregion

        #region Fields

        /// <summary>
        /// Gets the transform used to offset the <see cref="Viewport"/>.
        /// </summary>
        protected readonly TranslateTransform TranslateTransform = new TranslateTransform();

        /// <summary>
        /// Gets the transform used to scale the <see cref="Viewport"/>.
        /// </summary>
        protected readonly ScaleTransform ScaleTransform = new ScaleTransform();

        /// <summary>
        /// Gets or sets the maximum number of pixels allowed to move the mouse before cancelling the mouse event.
        /// Useful for <see cref="ContextMenu"/>s to appear if mouse only moved a bit or not at all.
        /// </summary>
        public static double HandleRightClickAfterPanningThreshold { get; set; } = 12d;

        /// <summary>
        /// Correct <see cref="ItemContainer"/>'s position after moving if starting position is not snapped to grid.
        /// </summary>
        public static bool EnableSnappingCorrection { get; set; } = true;

        /// <summary>
        /// Gets or sets how often the new <see cref="Offset"/> is calculated in milliseconds when <see cref="DisableAutoPanning"/> is false.
        /// </summary>
        public static double AutoPanningTickRate { get; set; } = 1;

        /// <summary>
        /// Tells if the <see cref="NodifyEditor"/> is doing operations on multiple items at once.
        /// </summary>
        public bool IsBulkUpdatingItems { get; protected set; }

        /// <summary>
        /// Gets the panel that holds all the <see cref="ItemContainer"/>s.
        /// </summary>
        protected internal Panel? ItemsHost { get; private set; }

        /// <summary>
        /// Helps with selecting <see cref="ItemContainer"/>s and updating the <see cref="SelectedArea"/> and <see cref="IsSelecting"/> properties.
        /// </summary>
        protected SelectionHelper Selection { get; private set; }

        /// <summary>
        /// Gets where the mouse cursor was the previous time it moved relative to the <see cref="NodifyEditor"/>.
        /// Check <see cref="MouseLocation"/> for a transformed position.
        /// </summary>
        protected Point PreviousMousePosition;

        /// <summary>
        /// Gets where the mouse cursor is right now relative to the <see cref="NodifyEditor"/>.
        /// Check <see cref="MouseLocation"/> for a transformed position.
        /// </summary>
        protected Point CurrentMousePosition;

        /// <summary>
        /// Gets where the mouse cursor was relative to the <see cref="NodifyEditor"/> when a mouse button event occured.
        /// Check <see cref="MouseLocation"/> for a transformed position.
        /// </summary>
        protected Point InitialMousePosition;

        private ItemContainer? _dragInstigator;
        private Vector _dragAccumulator;
        private readonly List<ItemContainer> _selectedContainers = new List<ItemContainer>(16);
        private DispatcherTimer? _autoPanningTimer;

        #endregion

        #region Construction

        static NodifyEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodifyEditor), new FrameworkPropertyMetadata(typeof(NodifyEditor)));
            FocusableProperty.OverrideMetadata(typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.True));

            EditorCommands.Register(typeof(NodifyEditor));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodifyEditor"/> class.
        /// </summary>
        public NodifyEditor()
        {
            AddHandler(Connector.DisconnectEvent, new ConnectorEventHandler(OnConnectorDisconnected));
            AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnConnectionCompleted));

            AddHandler(ItemContainer.DragStartedEvent, new DragStartedEventHandler(OnItemsDragStarted));
            AddHandler(ItemContainer.DragCompletedEvent, new DragCompletedEventHandler(OnItemsDragCompleted));
            AddHandler(ItemContainer.DragDeltaEvent, new DragDeltaEventHandler(OnItemsDragDelta));

            Selection = new SelectionHelper(this);

            var transform = new TransformGroup();
            transform.Children.Add(ScaleTransform);
            transform.Children.Add(TranslateTransform);

            SetValue(AppliedTransformPropertyKey, transform);

            OnDisableAutoPanningChanged(DisableAutoPanning);
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

        #endregion

        #region Methods

        /// <summary>
        /// Zoom in at the viewport's center
        /// </summary>
        public void ZoomIn() => ZoomAtPosition(Math.Pow(2.0, 120.0 / 3.0 / Mouse.MouseWheelDeltaForOneLine), RenderTransform.Transform(new Point(RenderSize.Width / 2, RenderSize.Height / 2)));

        /// <summary>
        /// Zoom out at the viewport's center
        /// </summary>
        public void ZoomOut() => ZoomAtPosition(Math.Pow(2.0, -120.0 / 3.0 / Mouse.MouseWheelDeltaForOneLine), RenderTransform.Transform(new Point(RenderSize.Width / 2, RenderSize.Height / 2)));

        /// <summary>
        /// Moves the <see cref="Viewport"/> at the specified location.
        /// </summary>
        /// <param name="point">The location where to move the <see cref="Viewport"/>.</param>
        /// <param name="animated">True to animate the movement.</param>
        public void BringIntoView(Point point, bool animated = true)
        {
            Focus();

            if (animated && point != Offset)
            {
                IsPanning = true;
                DisableZooming = true;

                this.StartAnimation(OffsetProperty, TransformToViewportCenter(point), BringIntoViewAnimationDuration, (s, e) =>
                {
                    IsPanning = false;
                    DisableZooming = false;
                });
            }
            else
            {
                Offset = TransformToViewportCenter(point);
            }
        }

        /// <summary>
        /// Zoom at the specified location.
        /// </summary>
        /// <param name="zoom">The zoom factor.</param>
        /// <param name="pos">The location to focus when zooming.</param>
        public void ZoomAtPosition(double zoom, Point pos)
        {
            var position = (Vector)pos;
            var prevScale = Scale;

            Scale *= zoom;

            if (prevScale != Scale)
            {
                // get the actual zoom value because Scale might have been coerced
                zoom = Scale / prevScale;
                Offset = (Point)((Vector)(Offset + position) * zoom - position);
            }
        }

        #endregion

        #region Auto panning

        private void HandleAutoPanning(object? sender, EventArgs e)
        {
            if (IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed && Mouse.Captured != null)
            {
                var mousePosition = Mouse.GetPosition(this);
                double edgeDistance = AutoPanEdgeDistance;
                double autoPanSpeed = Math.Min(AutoPanSpeed, AutoPanSpeed * AutoPanningTickRate);
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

                // Update the selecting area because the mouse might not move to update it.
                if (IsSelecting)
                {
                    Selection.Update(TransformPosition(mousePosition));
                }
            }
        }

        /// <summary>
        /// Called when the <see cref="DisableAutoPanning"/> changes.
        /// </summary>
        /// <param name="shouldDisable">Whether to enable or disable auto panning.</param>
        protected virtual void OnDisableAutoPanningChanged(bool shouldDisable)
        {
            if (!shouldDisable)
            {
                if (_autoPanningTimer == null)
                {
                    _autoPanningTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(AutoPanningTickRate), DispatcherPriority.Background, new EventHandler(HandleAutoPanning), Dispatcher);
                }
                else
                {
                    _autoPanningTimer.Interval = TimeSpan.FromMilliseconds(AutoPanningTickRate);
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
            if (!e.Canceled)
            {
                var result = (e.SourceConnector, e.TargetConnector);
                if (ConnectionCompletedCommand?.CanExecute(result) ?? false)
                {
                    ConnectionCompletedCommand.Execute(result);
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region Mouse Events Handlers

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            var scale = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
            ZoomAtPosition(scale, e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            CurrentMousePosition = e.GetPosition(this);

            if (CurrentMousePosition != PreviousMousePosition)
            {
                MouseLocation = TransformPosition(CurrentMousePosition);

                if (IsMouseCaptured)
                {
                    // Panning
                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        Offset -= CurrentMousePosition - PreviousMousePosition;
                        IsPanning = true;
                        e.Handled = true;
                    }
                    else if (IsSelecting)
                    {
                        Selection.Update(MouseLocation);
                    }
                    else
                    {
                        // Should not reach this
                        ReleaseMouseCapture();
                    }
                }

                PreviousMousePosition = CurrentMousePosition;
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            // End selection if selecting
            Selection.End();
            IsPanning = false;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Focus();
                CaptureMouse();

                InitialMousePosition = e.GetPosition(this);
                Selection.Start(MouseLocation, EnableRealtimeSelection);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            // End selection if selecting
            Selection.End();

            // If panning, don't interrupt
            if (!IsPanning && IsMouseCaptured)
            {
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
            // If right clicking without panning or selecting allow context menu
            if (IsMouseCaptured && !IsPanning && !IsSelecting)
            {
                Focus();
                ReleaseMouseCapture();
            }
            // If is selecting but not panning, end selection and show context menu
            else if (IsSelecting && !IsPanning)
            {
                Selection.End();
            }
            else if (IsPanning)
            {
                IsPanning = false;

                // Allow selecting and panning at the same time and disable context menu
                if (IsSelecting)
                {
                    e.Handled = true;
                }
                // If is panning but not selecting, release mouse capture and show context menu if necessary
                else
                {
                    // Handle right click if is panning and moved the mouse more than threshold so context menus don't open
                    if ((CurrentMousePosition - InitialMousePosition).LengthSquared > HandleRightClickAfterPanningThreshold * HandleRightClickAfterPanningThreshold)
                    {
                        e.Handled = true;
                    }

                    if (IsMouseCaptured)
                    {
                        ReleaseMouseCapture();
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

        /// <summary>
        /// Inverts the <see cref="ItemContainer"/>s selection in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void InvertSelection(Rect area, bool fit = false)
        {
            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.IsSelectableInArea(area, fit))
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

        /// <summary>
        /// Selects the <see cref="ItemContainer"/>s in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="append">If true, it will add to the existing </param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void SelectArea(Rect area, bool append = false, bool fit = false)
        {
            if (!append)
            {
                UnselectAll();
            }

            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.IsSelectableInArea(area, fit))
                {
                    selected.Add(items[i]);
                }
            }
            EndUpdateSelectedItems();
        }

        /// <summary>
        /// Unselects the <see cref="ItemContainer"/>s in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void UnselectArea(Rect area, bool fit = false)
        {
            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.IsSelectableInArea(area, fit))
                {
                    selected.Remove(items[i]);
                }
            }
            EndUpdateSelectedItems();
        }

        /// <summary>
        /// Adds the <paramref name="items"/> to the <see cref="SelectedItems"/> list.
        /// </summary>
        /// <param name="items">The items to select.</param>
        /// <param name="selected">True to add, false to remove.</param>
        internal void SelectItems(IList<object> items)
        {
            var selectedItems = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                selectedItems.Add(items[i]);
            }
            EndUpdateSelectedItems();
        }

        #endregion

        #region Dragging

        private void OnItemsDragDelta(object sender, DragDeltaEventArgs e)
        {
            // Move selection only if a selected item is being dragged
            if (_dragInstigator != null && _selectedContainers.Count > 0)
            {
                _dragAccumulator += new Vector(e.HorizontalChange, e.VerticalChange);
                var delta = new Vector((int)_dragAccumulator.X / GridCellSize * GridCellSize, (int)_dragAccumulator.Y / GridCellSize * GridCellSize);
                _dragAccumulator -= delta;

                if (delta.X != 0 || delta.Y != 0)
                {
                    for (int i = 0; i < _selectedContainers.Count; i++)
                    {
                        var container = _selectedContainers[i];
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
            if (_selectedContainers.Count > 0)
            {
                if (e.Canceled && ItemContainer.AllowDraggingCancellation)
                {
                    for (int i = 0; i < _selectedContainers.Count; i++)
                    {
                        var container = _selectedContainers[i];
                        var r = (TranslateTransform)container.RenderTransform;

                        r.X = 0;
                        r.Y = 0;

                        container.OnPreviewLocationChanged(container.Location);
                    }
                }
                else
                {
                    IsBulkUpdatingItems = true;

                    for (int i = 0; i < _selectedContainers.Count; i++)
                    {
                        var container = _selectedContainers[i];
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

                    // Draw the containers at the new position.
                    ItemsHost?.InvalidateArrange();
                }

                _dragInstigator = null;
                _selectedContainers.Clear();

                if (ItemsDragCompletedCommand?.CanExecute(null) ?? false)
                {
                    ItemsDragCompletedCommand.Execute(null);
                }
            }
        }

        private void OnItemsDragStarted(object sender, DragStartedEventArgs e)
        {
            _dragInstigator = e.OriginalSource as ItemContainer ?? (e.OriginalSource as UIElement)?.GetParentOfType<ItemContainer>();
            var selectedItems = base.SelectedItems;

            if (_dragInstigator != null)
            {
                // Clear the selection if the dragged item is not part of the selection and Control or Shift is not held
                if (!(Keyboard.Modifiers == ModifierKeys.Control || Keyboard.Modifiers == ModifierKeys.Shift || _dragInstigator.IsSelected))
                {
                    selectedItems.Clear();
                }

                _dragInstigator.IsSelected = true;
            }

            if (selectedItems.Count > 0)
            {
                if (ItemsDragStartedCommand?.CanExecute(null) ?? false)
                {
                    ItemsDragStartedCommand.Execute(null);
                }

                // Make sure we're not adding to a previous selection
                if (_selectedContainers.Count > 0)
                {
                    _selectedContainers.Clear();
                }

                // Increase cache capacity
                if (_selectedContainers.Capacity < selectedItems.Count)
                {
                    _selectedContainers.Capacity = selectedItems.Count;
                }

                // Cache selected containers
                for (int i = 0; i < selectedItems.Count; i++)
                {
                    var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(selectedItems[i]);
                    if (container.IsDraggable)
                    {
                        _selectedContainers.Add(container);
                    }
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Transforms the <paramref name="point"/> to a location relative to the <see cref="ItemsHost"/> panel.
        /// </summary>
        /// <param name="point">The location to transform.</param>
        /// <returns>The relative location.</returns>
        protected Point TransformPosition(Point point)
            => new Point((Offset.X + point.X) / Scale, (Offset.Y + point.Y) / Scale);

        /// <summary>
        /// Transforms the <paramref name="point"/> to a location relative to the <see cref="Viewport"/>'s center.
        /// </summary>
        /// <param name="point">The point to transform.</param>
        /// <returns>The center location.</returns>
        protected Point TransformToViewportCenter(Point point)
            => (Point)((Vector)point * Scale - new Vector(ActualWidth / 2, ActualHeight / 2));

        #endregion
    }
}

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
        public static readonly DependencyProperty AutoPanSpeedProperty = DependencyProperty.Register(nameof(AutoPanSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double2));
        public static readonly DependencyProperty AutoPanEdgeDistanceProperty = DependencyProperty.Register(nameof(AutoPanEdgeDistance), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(20d));
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

        public Point MousePositionTransformed => (Point)(((Vector)Offset + (Vector)Mouse.GetPosition(this)) / Scale);

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

        public static bool EnableSnappingCorrection { get; set; } = true;
        public bool IsBulkUpdatingItems { get; set; }

        protected internal Panel? ItemsHost { get; private set; }
        protected EditorSelection Selection { get; private set; }

        public NodifyEditor()
        {
            AddHandler(Connector.DisconnectEvent, new ConnectorEventHandler(OnConnectorDisconnected));

            AddHandler(DragBehavior.DragStartedEvent, new DragStartedEventHandler(OnItemsDragStarted));
            AddHandler(DragBehavior.DragCompletedEvent, new DragCompletedEventHandler(OnItemsDragCompleted));
            AddHandler(DragBehavior.DragDeltaEvent, new DragDeltaEventHandler(OnItemsDragDelta));

            AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnConnectionCompleted));

            Selection = new EditorSelection(this);

            var transform = new TransformGroup();
            transform.Children.Add(ScaleTransform);
            transform.Children.Add(TranslateTransform);

            SetValue(AppliedTransformPropertyKey, transform);
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

        #region Connector handling

        private void OnConnectorDisconnected(object sender, ConnectorEventArgs e)
        {
            if (DisconnectConnectorCommand != null && DisconnectConnectorCommand.CanExecute(null))
            {
                DisconnectConnectorCommand.Execute(e.Connector);
            }
        }

        private void OnConnectionCompleted(object sender, PendingConnectionEventArgs e)
        {
            if (ConnectionCompletedCommand != null && ConnectionCompletedCommand.CanExecute(null))
            {
                ConnectionCompletedCommand.Execute((e.SourceConnector, e.TargetConnector));
            }
        }

        #endregion

        #region Mouse Events Handlers

        protected Point PreviousMousePosition;
        protected Point CurrentMousePosition;
        protected Point ClickOrigin;

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
                if (Mouse.Captured == this)
                {
                    // Panning
                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        Offset -= CurrentMousePosition - PreviousMousePosition;
                        e.Handled = true;
                    }
                    // Selecting
                    else if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        Selection.Update(MousePositionTransformed);
                    }
                    else if (Mouse.Captured == this)
                    {
                        ReleaseMouseCapture();
                    }
                }

                //if (e.LeftButton == MouseButtonState.Pressed)
                //{
                //    AutoPanIfNecessary();
                //}

                PreviousMousePosition = CurrentMousePosition;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                ClickOrigin = MousePositionTransformed;

                Selection.Start(ClickOrigin, EnableRealtimeSelection);

                Focus();
                CaptureMouse();
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
            }
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == this && e.LeftButton == MouseButtonState.Released && e.MiddleButton == MouseButtonState.Released)
            {
                Focus();
                ReleaseMouseCapture();
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
            selectedItems.Clear();

            for (int i = 0; i < newValue.Count; i++)
            {
                selectedItems.Add(newValue[i]);
            }
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

            var selected = SelectedItems;

            if (selected != null)
            {
                var added = e.AddedItems;
                for (int i = 0; i < added.Count; i++)
                {
                    selected.Add(added[i]);
                }

                var removed = e.RemovedItems;
                for (int i = 0; i < removed.Count; i++)
                {
                    selected.Remove(removed[i]);
                }
            }
        }

        #endregion

        #region Selection

        public void AppendSelection(Rect area)
        {
            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (area.IntersectsWith(new Rect(container.Location, container.DesiredSizeForSelection ?? container.RenderSize)))
                {
                    selected.Add(items[i]);
                }
            }
            EndUpdateSelectedItems();
        }

        public void InvertSelection(Rect area)
        {
            var items = Items;
            var selected = base.SelectedItems;

            BeginUpdateSelectedItems();
            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (area.IntersectsWith(new Rect(container.Location, container.DesiredSizeForSelection ?? container.RenderSize)))
                {
                    var item = items[i];
                    if (selected.Contains(item))
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

        public void SelectArea(Rect area, bool append = false)
        {
            if (!append)
            {
                UnselectAll();
            }

            AppendSelection(area);
        }

        public void UnselectArea(Rect area)
        {
            var items = GetItemsInArea(area);
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

        public List<ItemContainer> GetItemsInArea(Rect area)
        {
            var items = Items;
            List<ItemContainer> inArea = new List<ItemContainer>(items.Count / 2);

            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (area.IntersectsWith(new Rect(container.Location, container.DesiredSizeForSelection ?? container.RenderSize)))
                {
                    inArea.Add(container);
                }
            }

            return inArea;
        }

        protected internal void GetItemsInAreaNoAlloc(List<ItemContainer> outItems, Rect area)
        {
            var items = Items;
            outItems.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (area.IntersectsWith(new Rect(container.Location, container.DesiredSizeForSelection ?? container.RenderSize)))
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

        protected void AutoPanIfNecessary()
        {
            double edgeDistance = AutoPanEdgeDistance;
            double autoPanSpeedModifier = AutoPanSpeed;
            double autoPanSpeedX = Math.Min(Math.Abs(CurrentMousePosition.X - PreviousMousePosition.X + 0.1) * autoPanSpeedModifier, autoPanSpeedModifier * 2);
            double autoPanSpeedY = Math.Min(Math.Abs(CurrentMousePosition.Y - PreviousMousePosition.Y + 0.1) * autoPanSpeedModifier, autoPanSpeedModifier * 2);
            double x = Offset.X;
            double y = Offset.Y;

            if (CurrentMousePosition.X < edgeDistance)
            {
                x -= autoPanSpeedX;
            }
            else if (CurrentMousePosition.X > ActualWidth - edgeDistance)
            {
                x += autoPanSpeedX;
            }

            if (CurrentMousePosition.Y < edgeDistance)
            {
                y -= autoPanSpeedY;
            }
            else if (CurrentMousePosition.Y > ActualHeight - edgeDistance)
            {
                y += autoPanSpeedY;
            }

            Offset = new Point(x, y);
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

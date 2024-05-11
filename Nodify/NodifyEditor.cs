using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Nodify
{
    /// <summary>
    /// Groups <see cref="ItemContainer"/>s and <see cref="Connection"/>s in an area that you can drag, zoom and select.
    /// </summary>
    [TemplatePart(Name = ElementItemsHost, Type = typeof(Panel))]
    [StyleTypedProperty(Property = nameof(ItemContainerStyle), StyleTargetType = typeof(ItemContainer))]
    [StyleTypedProperty(Property = nameof(DecoratorContainerStyle), StyleTargetType = typeof(DecoratorContainer))]
    [StyleTypedProperty(Property = nameof(SelectionRectangleStyle), StyleTargetType = typeof(Rectangle))]
    [ContentProperty(nameof(Decorators))]
    [DefaultProperty(nameof(Decorators))]
    public class NodifyEditor : MultiSelector
    {
        protected const string ElementItemsHost = "PART_ItemsHost";

        #region Viewport

        public static readonly DependencyProperty ViewportZoomProperty = DependencyProperty.Register(nameof(ViewportZoom), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnViewportZoomChanged, ConstrainViewportZoomToRange));
        public static readonly DependencyProperty MinViewportZoomProperty = DependencyProperty.Register(nameof(MinViewportZoom), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(0.1d, OnMinViewportZoomChanged, CoerceMinViewportZoom));
        public static readonly DependencyProperty MaxViewportZoomProperty = DependencyProperty.Register(nameof(MaxViewportZoom), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double2, OnMaxViewportZoomChanged, CoerceMaxViewportZoom));
        public static readonly DependencyProperty ViewportLocationProperty = DependencyProperty.Register(nameof(ViewportLocation), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnViewportLocationChanged));
        public static readonly DependencyProperty ViewportSizeProperty = DependencyProperty.Register(nameof(ViewportSize), typeof(Size), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Size));
        public static readonly DependencyProperty ItemsExtentProperty = DependencyProperty.Register(nameof(ItemsExtent), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));
        public static readonly DependencyProperty DecoratorsExtentProperty = DependencyProperty.Register(nameof(DecoratorsExtent), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));

        protected internal static readonly DependencyPropertyKey ViewportTransformPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ViewportTransform), typeof(Transform), typeof(NodifyEditor), new FrameworkPropertyMetadata(new TransformGroup()));
        public static readonly DependencyProperty ViewportTransformProperty = ViewportTransformPropertyKey.DependencyProperty;

        #region Callbacks

        private static void OnViewportLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            var translate = (Point)e.NewValue;

            editor.TranslateTransform.X = -translate.X * editor.ViewportZoom;
            editor.TranslateTransform.Y = -translate.Y * editor.ViewportZoom;

            editor.OnViewportUpdated();
        }

        private static void OnViewportZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            double zoom = (double)e.NewValue;

            editor.ScaleTransform.ScaleX = zoom;
            editor.ScaleTransform.ScaleY = zoom;

            editor.ViewportSize = new Size(editor.ActualWidth / zoom, editor.ActualHeight / zoom);

            editor.ApplyRenderingOptimizations();
            editor.OnViewportUpdated();
        }

        private static void OnMinViewportZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(MaxViewportZoomProperty);
            zoom.CoerceValue(ViewportZoomProperty);
        }

        private static object CoerceMinViewportZoom(DependencyObject d, object value)
            => (double)value > 0.1d ? value : 0.1d;

        private static void OnMaxViewportZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(ViewportZoomProperty);
        }

        private static object CoerceMaxViewportZoom(DependencyObject d, object value)
        {
            var editor = (NodifyEditor)d;
            double min = editor.MinViewportZoom;

            return (double)value < min ? min : value;
        }

        private static object ConstrainViewportZoomToRange(DependencyObject d, object value)
        {
            var editor = (NodifyEditor)d;

            var num = (double)value;
            double minimum = editor.MinViewportZoom;
            if (num < minimum)
            {
                return minimum;
            }

            double maximum = editor.MaxViewportZoom;
            return num > maximum ? maximum : value;
        }
        #endregion

        #region Routed Events

        public static readonly RoutedEvent ViewportUpdatedEvent = EventManager.RegisterRoutedEvent(nameof(ViewportUpdated), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NodifyEditor));

        /// <summary>
        /// Occurs whenever the viewport updates.
        /// </summary>
        public event RoutedEventHandler ViewportUpdated
        {
            add => AddHandler(ViewportUpdatedEvent, value);
            remove => RemoveHandler(ViewportUpdatedEvent, value);
        }

        /// <summary>
        /// Updates the <see cref="ViewportSize"/> and raises the <see cref="ViewportUpdatedEvent"/>.
        /// Called when the <see cref="UIElement.RenderSize"/> or <see cref="ViewportZoom"/> is changed.
        /// </summary>
        protected void OnViewportUpdated() => RaiseEvent(new RoutedEventArgs(ViewportUpdatedEvent, this));

        #endregion

        #region Properties

        /// <summary>
        /// Gets the transform used to offset the viewport.
        /// </summary>
        protected readonly TranslateTransform TranslateTransform = new TranslateTransform();

        /// <summary>
        /// Gets the transform used to zoom on the viewport.
        /// </summary>
        protected readonly ScaleTransform ScaleTransform = new ScaleTransform();

        /// <summary>
        /// Gets the transform that is applied to all child controls.
        /// </summary>
        public Transform ViewportTransform => (Transform)GetValue(ViewportTransformProperty);

        /// <summary>
        /// Gets the size of the viewport.
        /// </summary>
        public Size ViewportSize
        {
            get => (Size)GetValue(ViewportSizeProperty);
            set => SetValue(ViewportSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the viewport's top-left coordinates in graph space coordinates.
        /// </summary>
        public Point ViewportLocation
        {
            get => (Point)GetValue(ViewportLocationProperty);
            set => SetValue(ViewportLocationProperty, value);
        }


        /// <summary>
        /// Gets or sets the zoom factor of the viewport.
        /// </summary>
        public double ViewportZoom
        {
            get => (double)GetValue(ViewportZoomProperty);
            set => SetValue(ViewportZoomProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum zoom factor of the viewport
        /// </summary>
        public double MinViewportZoom
        {
            get => (double)GetValue(MinViewportZoomProperty);
            set => SetValue(MinViewportZoomProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum zoom factor of the viewport
        /// </summary>
        public double MaxViewportZoom
        {
            get => (double)GetValue(MaxViewportZoomProperty);
            set => SetValue(MaxViewportZoomProperty, value);
        }

        /// <summary>
        /// The area covered by the <see cref="ItemContainer"/>s.
        /// </summary>
        public Rect ItemsExtent
        {
            get => (Rect)GetValue(ItemsExtentProperty);
            set => SetValue(ItemsExtentProperty, value);
        }

        /// <summary>
        /// The area covered by the <see cref="DecoratorContainer"/>s.
        /// </summary>
        public Rect DecoratorsExtent
        {
            get => (Rect)GetValue(DecoratorsExtentProperty);
            set => SetValue(DecoratorsExtentProperty, value);
        }

        #endregion

        private void ApplyRenderingOptimizations()
        {
            if (ItemsHost != null)
            {
                if (EnableRenderingContainersOptimizations && Items.Count >= OptimizeRenderingMinimumContainers)
                {
                    double zoom = ViewportZoom;
                    double availableZoomIn = 1.0 - MinViewportZoom;
                    bool shouldCache = zoom / availableZoomIn <= OptimizeRenderingZoomOutPercent;
                    ItemsHost.CacheMode = shouldCache ? new BitmapCache(1.0 / zoom) : null;
                }
                else
                {
                    ItemsHost.CacheMode = null;
                }
            }
        }

        #endregion

        #region Cosmetic Dependency Properties

        public static readonly DependencyProperty BringIntoViewSpeedProperty = DependencyProperty.Register(nameof(BringIntoViewSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double1000));
        public static readonly DependencyProperty BringIntoViewMaxDurationProperty = DependencyProperty.Register(nameof(BringIntoViewMaxDuration), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Double1));
        public static readonly DependencyProperty DisplayConnectionsOnTopProperty = DependencyProperty.Register(nameof(DisplayConnectionsOnTop), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty DisableAutoPanningProperty = DependencyProperty.Register(nameof(DisableAutoPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnDisableAutoPanningChanged));
        public static readonly DependencyProperty AutoPanSpeedProperty = DependencyProperty.Register(nameof(AutoPanSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(15d));
        public static readonly DependencyProperty AutoPanEdgeDistanceProperty = DependencyProperty.Register(nameof(AutoPanEdgeDistance), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(15d));
        public static readonly DependencyProperty ConnectionTemplateProperty = DependencyProperty.Register(nameof(ConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty DecoratorTemplateProperty = DependencyProperty.Register(nameof(DecoratorTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty PendingConnectionTemplateProperty = DependencyProperty.Register(nameof(PendingConnectionTemplate), typeof(DataTemplate), typeof(NodifyEditor));
        public static readonly DependencyProperty SelectionRectangleStyleProperty = DependencyProperty.Register(nameof(SelectionRectangleStyle), typeof(Style), typeof(NodifyEditor));
        public static readonly DependencyProperty DecoratorContainerStyleProperty = DependencyProperty.Register(nameof(DecoratorContainerStyle), typeof(Style), typeof(NodifyEditor));

        private static void OnDisableAutoPanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnDisableAutoPanningChanged((bool)e.NewValue);

        /// <summary>
        /// Gets or sets the maximum animation duration in seconds for bringing a location into view.
        /// </summary>
        public double BringIntoViewMaxDuration
        {
            get => (double)GetValue(BringIntoViewMaxDurationProperty);
            set => SetValue(BringIntoViewMaxDurationProperty, value);
        }

        /// <summary>
        /// Gets or sets the animation speed in pixels per second for bringing a location into view.
        /// </summary>
        /// <remarks>Total animation duration is calculated based on distance and clamped between 0.1 and <see cref="BringIntoViewMaxDuration"/>.</remarks>
        public double BringIntoViewSpeed
        {
            get => (double)GetValue(BringIntoViewSpeedProperty);
            set => SetValue(BringIntoViewSpeedProperty, value);
        }

        /// <summary>
        /// Gets or sets whether to display connections on top of <see cref="ItemContainer"/>s or not.
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
        /// Gets or sets the <see cref="DataTemplate"/> to use when generating a new <see cref="BaseConnection"/>.
        /// </summary>
        public DataTemplate ConnectionTemplate
        {
            get => (DataTemplate)GetValue(ConnectionTemplateProperty);
            set => SetValue(ConnectionTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> to use when generating a new <see cref="DecoratorContainer"/>.
        /// </summary>
        public DataTemplate DecoratorTemplate
        {
            get => (DataTemplate)GetValue(DecoratorTemplateProperty);
            set => SetValue(DecoratorTemplateProperty, value);
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

        /// <summary>
        /// Gets or sets the style to use for the <see cref="DecoratorContainer"/>.
        /// </summary>
        public Style DecoratorContainerStyle
        {
            get => (Style)GetValue(DecoratorContainerStyleProperty);
            set => SetValue(DecoratorContainerStyleProperty, value);
        }

        #endregion

        #region Readonly Dependency Properties

        protected static readonly DependencyPropertyKey SelectedAreaPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedArea), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));
        public static readonly DependencyProperty SelectedAreaProperty = SelectedAreaPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey IsSelectingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsSelecting), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnIsSelectingChanged));
        public static readonly DependencyProperty IsSelectingProperty = IsSelectingPropertyKey.DependencyProperty;

        public static readonly DependencyPropertyKey IsPanningPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsPanningProperty = IsPanningPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey MouseLocationPropertyKey = DependencyProperty.RegisterReadOnly(nameof(MouseLocation), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty MouseLocationProperty = MouseLocationPropertyKey.DependencyProperty;

        private static void OnIsSelectingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            if ((bool)e.NewValue == true)
                editor.OnItemsSelectStarted();
            else
                editor.OnItemSelectCompleted();
        }

        private void OnItemSelectCompleted()
        {
            if (ItemsSelectCompletedCommand?.CanExecute(null) ?? false)
                ItemsSelectCompletedCommand.Execute(null);
        }

        private void OnItemsSelectStarted()
        {
            if (ItemsSelectStartedCommand?.CanExecute(null) ?? false)
                ItemsSelectStartedCommand.Execute(null);
        }

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
            protected internal set => SetValue(IsPanningPropertyKey, value);
        }

        /// <summary>
        /// Gets the current mouse location in graph space coordinates (relative to the <see cref="ItemsHost" />).
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
        public static readonly DependencyProperty DisablePanningProperty = DependencyProperty.Register(nameof(DisablePanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnDisablePanningChanged));
        public static readonly DependencyProperty EnableRealtimeSelectionProperty = DependencyProperty.Register(nameof(EnableRealtimeSelection), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty DecoratorsProperty = DependencyProperty.Register(nameof(Decorators), typeof(IEnumerable), typeof(NodifyEditor));

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnSelectedItemsSourceChanged((IList)e.OldValue, (IList)e.NewValue);

        private static object OnCoerceGridCellSize(DependencyObject d, object value)
            => (uint)value > 0u ? value : BoxValue.UInt1;

        private static void OnGridCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        private static void OnDisablePanningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            editor.OnDisableAutoPanningChanged(editor.DisableAutoPanning || editor.DisablePanning);
        }

        /// <summary>
        /// Gets or sets the items that will be rendered in the decorators layer via <see cref="DecoratorContainer"/>s.
        /// </summary>
        public IEnumerable Decorators
        {
            get => (IEnumerable)GetValue(DecoratorsProperty);
            set => SetValue(DecoratorsProperty, value);
        }

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

        #endregion

        #region Command Dependency Properties

        public static readonly DependencyProperty ConnectionCompletedCommandProperty = DependencyProperty.Register(nameof(ConnectionCompletedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ConnectionStartedCommandProperty = DependencyProperty.Register(nameof(ConnectionStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty DisconnectConnectorCommandProperty = DependencyProperty.Register(nameof(DisconnectConnectorCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty RemoveConnectionCommandProperty = DependencyProperty.Register(nameof(RemoveConnectionCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragStartedCommandProperty = DependencyProperty.Register(nameof(ItemsDragStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragCompletedCommandProperty = DependencyProperty.Register(nameof(ItemsDragCompletedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsSelectStartedCommandProperty = DependencyProperty.Register(nameof(ItemsSelectStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsSelectCompletedCommandProperty = DependencyProperty.Register(nameof(ItemsSelectCompletedCommand), typeof(ICommand), typeof(NodifyEditor));

        /// <summary>
        /// Invoked when the <see cref="Nodify.PendingConnection"/> is completed. <br />
        /// Use <see cref="PendingConnection.StartedCommand"/> if you want to control the visibility of the connection from the viewmodel. <br />
        /// Parameter is <see cref="PendingConnection.Source"/>.
        /// </summary>
        public ICommand? ConnectionStartedCommand
        {
            get => (ICommand?)GetValue(ConnectionStartedCommandProperty);
            set => SetValue(ConnectionStartedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when the <see cref="Nodify.PendingConnection"/> is completed. <br />
        /// Use <see cref="PendingConnection.CompletedCommand"/> if you want to control the visibility of the connection from the viewmodel. <br />
        /// Parameter is <see cref="Tuple{T, U}"/> where <see cref="Tuple{T, U}.Item1"/> is the <see cref="PendingConnection.Source"/> and <see cref="Tuple{T, U}.Item2"/> is <see cref="PendingConnection.Target"/>.
        /// </summary>
        public ICommand? ConnectionCompletedCommand
        {
            get => (ICommand?)GetValue(ConnectionCompletedCommandProperty);
            set => SetValue(ConnectionCompletedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when the <see cref="Connector.Disconnect"/> event is raised. <br />
        /// Can also be handled at the <see cref="Connector"/> level using the <see cref="Connector.DisconnectCommand"/> command. <br />
        /// Parameter is the <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/>.
        /// </summary>
        public ICommand? DisconnectConnectorCommand
        {
            get => (ICommand?)GetValue(DisconnectConnectorCommandProperty);
            set => SetValue(DisconnectConnectorCommandProperty, value);
        }

        /// <summary>
        /// Invoked when the <see cref="BaseConnection.Disconnect"/> event is raised. <br />
        /// Can also be handled at the <see cref="BaseConnection"/> level using the <see cref="BaseConnection.DisconnectCommand"/> command. <br />
        /// Parameter is the <see cref="BaseConnection"/>'s <see cref="FrameworkElement.DataContext"/>.
        /// </summary>
        public ICommand? RemoveConnectionCommand
        {
            get => (ICommand?)GetValue(RemoveConnectionCommandProperty);
            set => SetValue(RemoveConnectionCommandProperty, value);
        }

        /// <summary>
        /// Invoked when a drag operation starts for the <see cref="SelectedItems"/>.
        /// </summary>
        public ICommand? ItemsDragStartedCommand
        {
            get => (ICommand?)GetValue(ItemsDragStartedCommandProperty);
            set => SetValue(ItemsDragStartedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when a drag operation is completed for the <see cref="SelectedItems"/>.
        /// </summary>
        public ICommand? ItemsDragCompletedCommand
        {
            get => (ICommand?)GetValue(ItemsDragCompletedCommandProperty);
            set => SetValue(ItemsDragCompletedCommandProperty, value);
        }

        /// <summary>Invoked when a selection operation is started.</summary>
        public ICommand? ItemsSelectStartedCommand
        {
            get => (ICommand?)GetValue(ItemsSelectStartedCommandProperty);
            set => SetValue(ItemsSelectStartedCommandProperty, value);
        }

        /// <summary>Invoked when a selection operation is completed.</summary>
        public ICommand? ItemsSelectCompletedCommand
        {
            get => (ICommand?)GetValue(ItemsSelectCompletedCommandProperty);
            set => SetValue(ItemsSelectCompletedCommandProperty, value);
        }

        #endregion

        #region Fields

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
        /// Gets or sets how often the new <see cref="ViewportLocation"/> is calculated in milliseconds when <see cref="DisableAutoPanning"/> is false.
        /// </summary>
        public static double AutoPanningTickRate { get; set; } = 1;

        /// <summary>
        /// Gets or sets if <see cref="NodifyEditor"/>s should enable optimizations based on <see cref="OptimizeRenderingMinimumContainers"/> and <see cref="OptimizeRenderingZoomOutPercent"/>.
        /// </summary>
        public static bool EnableRenderingContainersOptimizations { get; set; } = true;

        /// <summary>
        /// Gets or sets the minimum number of <see cref="ItemContainer"/>s needed to trigger optimizations when reaching the <see cref="OptimizeRenderingZoomOutPercent"/>.
        /// </summary>
        public static uint OptimizeRenderingMinimumContainers { get; set; } = 700;

        /// <summary>
        /// Gets or sets the minimum zoom out percent needed to start optimizing the rendering for <see cref="ItemContainer"/>s.
        /// Value is between 0 and 1.
        /// </summary>
        public static double OptimizeRenderingZoomOutPercent { get; set; } = 0.3;

        /// <summary>
        /// Gets or sets the margin to add in all directions to the <see cref="ItemsExtent"/> or area parameter when using <see cref="FitToScreen(Rect?)"/>.
        /// </summary>
        public static double FitToScreenExtentMargin { get; set; } = 30;

        /// <summary>
        /// Gets or sets if the current position of containers that are being dragged should not be committed until the end of the dragging operation.
        /// </summary>
        public static bool EnableDraggingContainersOptimizations { get; set; } = true;

        /// <summary>
        /// Tells if the <see cref="NodifyEditor"/> is doing operations on multiple items at once.
        /// </summary>
        public bool IsBulkUpdatingItems { get; protected set; }

        /// <summary>
        /// Gets the panel that holds all the <see cref="ItemContainer"/>s.
        /// </summary>
        protected internal Panel ItemsHost { get; private set; } = default!;

        private IDraggingStrategy? _draggingStrategy;
        private DispatcherTimer? _autoPanningTimer;

        /// <summary>
        /// Gets a list of <see cref="ItemContainer"/>s that are selected.
        /// </summary>
        /// <remarks>Cache the result before using it to avoid extra allocations.</remarks>
        protected internal IReadOnlyList<ItemContainer> SelectedContainers
        {
            get
            {
                IList selectedItems = base.SelectedItems;
                var selectedContainers = new List<ItemContainer>(selectedItems.Count);

                for (var i = 0; i < selectedItems.Count; i++)
                {
                    var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(selectedItems[i]);
                    selectedContainers.Add(container);
                }

                return selectedContainers;
            }
        }

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
            AddHandler(Connector.PendingConnectionStartedEvent, new PendingConnectionEventHandler(OnConnectionStarted));
            AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnConnectionCompleted));

            AddHandler(BaseConnection.DisconnectEvent, new ConnectionEventHandler(OnRemoveConnection));

            AddHandler(ItemContainer.DragStartedEvent, new DragStartedEventHandler(OnItemsDragStarted));
            AddHandler(ItemContainer.DragCompletedEvent, new DragCompletedEventHandler(OnItemsDragCompleted));
            AddHandler(ItemContainer.DragDeltaEvent, new DragDeltaEventHandler(OnItemsDragDelta));

            var transform = new TransformGroup();
            transform.Children.Add(ScaleTransform);
            transform.Children.Add(TranslateTransform);

            SetValue(ViewportTransformPropertyKey, transform);

            _states.Push(GetInitialState());

            CanSelectMultipleItems = true;
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsHost = GetTemplateChild(ElementItemsHost) as Panel ?? throw new InvalidOperationException("PART_ItemsHost is missing or is not of type Panel.");

            OnDisableAutoPanningChanged(DisableAutoPanning);

            State.Enter(null);
        }

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
            => new ItemContainer(this)
            {
                RenderTransform = new TranslateTransform()
            };

        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is ItemContainer;

        #endregion

        #region Methods

        /// <summary>
        /// Zoom in at the viewports center
        /// </summary>
        public void ZoomIn() => ZoomAtPosition(Math.Pow(2.0, 120.0 / 3.0 / Mouse.MouseWheelDeltaForOneLine), (Point)((Vector)ViewportLocation + (Vector)ViewportSize / 2));

        /// <summary>
        /// Zoom out at the viewports center
        /// </summary>
        public void ZoomOut() => ZoomAtPosition(Math.Pow(2.0, -120.0 / 3.0 / Mouse.MouseWheelDeltaForOneLine), (Point)((Vector)ViewportLocation + (Vector)ViewportSize / 2));

        /// <summary>
        /// Zoom at the specified location in graph space coordinates.
        /// </summary>
        /// <param name="zoom">The zoom factor.</param>
        /// <param name="location">The location to focus when zooming.</param>
        public void ZoomAtPosition(double zoom, Point location)
        {
            if (!DisableZooming)
            {
                double prevZoom = ViewportZoom;
                ViewportZoom *= zoom;

                if (Math.Abs(prevZoom - ViewportZoom) > 0.001)
                {
                    // get the actual zoom value because Zoom might have been coerced
                    zoom = ViewportZoom / prevZoom;
                    Vector position = (Vector)location;

                    var dist = position - (Vector)ViewportLocation;
                    var zoomedDist = dist * zoom;
                    var diff = zoomedDist - dist;
                    ViewportLocation += diff / zoom;
                }
            }
        }

        /// <summary>
        /// Moves the viewport center at the specified location.
        /// </summary>
        /// <param name="point">The location in graph space coordinates.</param>
        /// <param name="animated">True to animate the movement.</param>
        /// <param name="onFinish">The callback invoked when movement is finished.</param>
        /// <remarks>Temporarily disables editor controls when animated.</remarks>
        public void BringIntoView(Point point, bool animated = true, Action? onFinish = null)
        {
            Point newLocation = (Point)((Vector)point - (Vector)ViewportSize / 2);

            if (animated && newLocation != ViewportLocation)
            {
                IsPanning = true;
                DisablePanning = true;
                DisableZooming = true;

                double distance = (newLocation - ViewportLocation).Length;
                double duration = distance / (BringIntoViewSpeed + (distance / 10)) * ViewportZoom;
                duration = Math.Max(0.1, Math.Min(duration, BringIntoViewMaxDuration));

                this.StartAnimation(ViewportLocationProperty, newLocation, duration, (s, e) =>
                {
                    IsPanning = false;
                    DisablePanning = false;
                    DisableZooming = false;

                    onFinish?.Invoke();
                });
            }
            else
            {
                ViewportLocation = newLocation;
                onFinish?.Invoke();
            }
        }

        /// <summary>
        /// Scales the viewport to fit the specified <paramref name="area"/> or all the <see cref="ItemContainer"/>s if that's possible.
        /// </summary>
        /// <remarks>Does nothing if <paramref name="area"/> is null and there's no items.</remarks>
        public void FitToScreen(Rect? area = null)
        {
            Rect extent = area ?? ItemsExtent;
            extent.Inflate(FitToScreenExtentMargin, FitToScreenExtentMargin);

            if (extent.Width > 0 && extent.Height > 0)
            {
                double widthRatio = ViewportSize.Width / extent.Width;
                double heightRatio = ViewportSize.Height / extent.Height;

                double zoom = Math.Min(widthRatio, heightRatio);
                var center = new Point(extent.X + extent.Width / 2, extent.Y + extent.Height / 2);

                ZoomAtPosition(zoom, center);
                BringIntoView(center, animated: false);
            }
        }

        #endregion

        #region Auto panning

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

                State.HandleAutoPanning(new MouseEventArgs(Mouse.PrimaryDevice, 0));
            }
        }

        /// <summary>
        /// Called when the <see cref="DisableAutoPanning"/> changes.
        /// </summary>
        /// <param name="shouldDisable">Whether to enable or disable auto panning.</param>
        protected virtual void OnDisableAutoPanningChanged(bool shouldDisable)
        {
            if (shouldDisable)
            {
                _autoPanningTimer?.Stop();
            }
            else if (_autoPanningTimer == null)
            {
                _autoPanningTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(AutoPanningTickRate),
                    DispatcherPriority.Background, HandleAutoPanning, Dispatcher);
            }
            else
            {
                _autoPanningTimer.Interval = TimeSpan.FromMilliseconds(AutoPanningTickRate);
                _autoPanningTimer.Start();
            }
        }

        #endregion

        #region Connector handling

        private void OnConnectorDisconnected(object sender, ConnectorEventArgs e)
        {
            if (!e.Handled && (DisconnectConnectorCommand?.CanExecute(e.Connector) ?? false))
            {
                DisconnectConnectorCommand.Execute(e.Connector);
                e.Handled = true;
            }
        }

        private void OnConnectionStarted(object sender, PendingConnectionEventArgs e)
        {
            if (!e.Canceled && ConnectionStartedCommand != null)
            {
                e.Canceled = !ConnectionStartedCommand.CanExecute(e.SourceConnector);
                if (!e.Canceled)
                {
                    ConnectionStartedCommand.Execute(e.SourceConnector);
                }
            }
        }

        private void OnConnectionCompleted(object sender, PendingConnectionEventArgs e)
        {
            if (!e.Canceled)
            {
                (object SourceConnector, object? TargetConnector) result = (e.SourceConnector, e.TargetConnector);
                if (ConnectionCompletedCommand?.CanExecute(result) ?? false)
                {
                    ConnectionCompletedCommand.Execute(result);
                }
            }
        }

        private void OnRemoveConnection(object sender, ConnectionEventArgs e)
        {
            if (RemoveConnectionCommand?.CanExecute(e.Connection) ?? false)
            {
                RemoveConnectionCommand.Execute(e.Connection);
            }
        }

        #endregion

        #region State Handling

        private readonly Stack<EditorState> _states = new Stack<EditorState>();

        /// <summary>The current state of the editor.</summary>
        public EditorState State => _states.Peek();

        /// <summary>Creates the initial state of the editor.</summary>
        /// <returns>The initial state.</returns>
        protected virtual EditorState GetInitialState()
            => new EditorDefaultState(this);

        /// <summary>Pushes the given state to the stack.</summary>
        /// <param name="state">The new state of the editor.</param>
        /// <remarks>Calls <see cref="EditorState.Enter"/> on the new state.</remarks>
        public void PushState(EditorState state)
        {
            var prev = State;
            _states.Push(state);
            state.Enter(prev);
        }

        /// <summary>Pops the current <see cref="State"/> from the stack.</summary>
        /// <remarks>It doesn't pop the initial state. (see <see cref="GetInitialState"/>)
        /// <br />Calls <see cref="EditorState.Exit"/> on the current state.
        /// <br />Calls <see cref="EditorState.ReEnter"/> on the previous state.
        /// </remarks>
        public void PopState()
        {
            // Never remove the default state
            if (_states.Count > 1)
            {
                EditorState prev = _states.Pop();
                prev.Exit();
                State.ReEnter(prev);
            }
        }

        /// <summary>Pops all states from the editor.</summary>
        /// <remarks>It doesn't pop the initial state. (see <see cref="GetInitialState"/>)</remarks>
        public void PopAllStates()
        {
            while (_states.Count > 1)
            {
                PopState();
            }
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            // Needed to not steal mouse capture from children
            if (Mouse.Captured == null || IsMouseCaptured)
            {
                Focus();
                CaptureMouse();

                MouseLocation = e.GetPosition(ItemsHost);
                State.HandleMouseDown(e);
            }
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            MouseLocation = e.GetPosition(ItemsHost);
            State.HandleMouseUp(e);

            // Release the mouse capture if all the mouse buttons are released
            if (IsMouseCaptured && e.RightButton == MouseButtonState.Released && e.LeftButton == MouseButtonState.Released && e.MiddleButton == MouseButtonState.Released)
            {
                ReleaseMouseCapture();
            }

            // Disable context menu if selecting
            if (IsSelecting)
            {
                e.Handled = true;
            }
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseLocation = e.GetPosition(ItemsHost);
            State.HandleMouseMove(e);
        }

        /// <inheritdoc />
        protected override void OnLostMouseCapture(MouseEventArgs e)
            => PopAllStates();

        /// <inheritdoc />
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            State.HandleMouseWheel(e);

            if (!e.Handled && EditorGestures.Mappings.Editor.ZoomModifierKey == Keyboard.Modifiers)
            {
                double zoom = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
                ZoomAtPosition(zoom, e.GetPosition(ItemsHost));

                // Handle it for nested editors
                if (e.Source is NodifyEditor)
                {
                    e.Handled = true;
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
            => State.HandleKeyUp(e);

        protected override void OnKeyDown(KeyEventArgs e)
            => State.HandleKeyDown(e);

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

            IList selectedItems = base.SelectedItems;

            BeginUpdateSelectedItems();
            selectedItems.Clear();
            if (newValue != null)
            {
                for (var i = 0; i < newValue.Count; i++)
                {
                    selectedItems.Add(newValue[i]);
                }
            }
            EndUpdateSelectedItems();
        }

        private void OnSelectedItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    base.SelectedItems.Clear();
                    break;

                case NotifyCollectionChangedAction.Add:
                    IList? newItems = e.NewItems;
                    if (newItems != null)
                    {
                        IList selectedItems = base.SelectedItems;
                        for (var i = 0; i < newItems.Count; i++)
                        {
                            selectedItems.Add(newItems[i]);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    IList? oldItems = e.OldItems;
                    if (oldItems != null)
                    {
                        IList selectedItems = base.SelectedItems;
                        for (var i = 0; i < oldItems.Count; i++)
                        {
                            selectedItems.Remove(oldItems[i]);
                        }
                    }
                    break;
            }
        }

        /// <inheritdoc />
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            IList? selected = SelectedItems;
            if (selected != null)
            {
                IList added = e.AddedItems;
                for (var i = 0; i < added.Count; i++)
                {
                    // Ensure no duplicates are added
                    if (!selected.Contains(added[i]))
                    {
                        selected.Add(added[i]);
                    }
                }

                IList removed = e.RemovedItems;
                for (var i = 0; i < removed.Count; i++)
                {
                    selected.Remove(removed[i]);
                }
            }
        }

        #endregion

        #region Selection

        internal void ApplyPreviewingSelection()
        {
            ItemCollection items = Items;
            IList selected = base.SelectedItems;

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsPreviewingSelection == true)
                {
                    selected.Add(items[i]);
                }
                else if (container.IsPreviewingSelection == false)
                {
                    selected.Remove(items[i]);
                }
                container.IsPreviewingSelection = null;
            }
            EndUpdateSelectedItems();
            IsSelecting = false;
        }

        internal void ClearPreviewingSelection()
        {
            ItemCollection items = Items;
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);
                container.IsPreviewingSelection = null;
            }
        }

        /// <summary>
        /// Inverts the <see cref="ItemContainer"/>s selection in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void InvertSelection(Rect area, bool fit = false)
        {
            ItemCollection items = Items;
            IList selected = base.SelectedItems;

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);

                if (container.IsSelectableInArea(area, fit))
                {
                    object? item = items[i];
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
            IsSelecting = false;
        }

        /// <summary>
        /// Selects the <see cref="ItemContainer"/>s in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="append">If true, it will add to the existing selection.</param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void SelectArea(Rect area, bool append = false, bool fit = false)
        {
            if (!append)
            {
                UnselectAll();
            }

            ItemCollection items = Items;
            IList selected = base.SelectedItems;

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsSelectableInArea(area, fit))
                {
                    selected.Add(items[i]);
                }
            }
            EndUpdateSelectedItems();
            IsSelecting = false;
        }

        /// <summary>
        /// Unselect the <see cref="ItemContainer"/>s in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void UnselectArea(Rect area, bool fit = false)
        {
            IList items = base.SelectedItems;

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(items[i]);
                if (container.IsSelectableInArea(area, fit))
                {
                    items.Remove(items[i]);
                }
            }
            EndUpdateSelectedItems();
            IsSelecting = false;
        }

        #endregion

        #region Dragging

        private void OnItemsDragDelta(object sender, DragDeltaEventArgs e)
        {
            _draggingStrategy?.Update(new Vector(e.HorizontalChange, e.VerticalChange));
        }

        private void OnItemsDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (e.Canceled && ItemContainer.AllowDraggingCancellation)
            {
                _draggingStrategy?.Abort(new Vector(e.HorizontalChange, e.VerticalChange));
            }
            else
            {
                IsBulkUpdatingItems = true;

                _draggingStrategy?.End(new Vector(e.HorizontalChange, e.VerticalChange));

                IsBulkUpdatingItems = false;

                // Draw the containers at the new position.
                ItemsHost.InvalidateArrange();
            }

            if (ItemsDragCompletedCommand?.CanExecute(null) ?? false)
            {
                ItemsDragCompletedCommand.Execute(null);
            }
        }

        private void OnItemsDragStarted(object sender, DragStartedEventArgs e)
        {
            IList selectedItems = base.SelectedItems;

            if (EnableDraggingContainersOptimizations)
            {
                _draggingStrategy = new DraggingOptimized(this);
            }
            else
            {
                _draggingStrategy = new DraggingSimple(this);
            }

            _draggingStrategy.Start(new Vector(e.HorizontalOffset, e.VerticalOffset));

            if (selectedItems.Count > 0)
            {
                if (ItemsDragStartedCommand?.CanExecute(null) ?? false)
                {
                    ItemsDragStartedCommand.Execute(null);
                }

                e.Handled = true;
            }
        }

        #endregion

        /// <inheritdoc />
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            double zoom = ViewportZoom;
            ViewportSize = new Size(ActualWidth / zoom, ActualHeight / zoom);

            OnViewportUpdated();
        }

        #region Utilities

        /// <summary>
        /// Translates the specified location to graph space coordinates (relative to the <see cref="ItemsHost" />).
        /// </summary>
        /// <param name="location">The location coordinates relative to <paramref name="relativeTo"/></param>
        /// <param name="relativeTo">The element where the <paramref name="location"/> was calculated from.</param>
        /// <returns>A location inside the graph.</returns>
        public Point GetLocationInsideEditor(Point location, UIElement relativeTo)
            => relativeTo.TranslatePoint(location, ItemsHost);

        /// <summary>
        /// Translates the event location to graph space coordinates (relative to the <see cref="ItemsHost" />).
        /// </summary>
        /// <param name="args">The drag event.</param>
        /// <returns>A location inside the graph</returns>
        public Point GetLocationInsideEditor(DragEventArgs args)
            => args.GetPosition(ItemsHost);

        /// <summary>
        /// Translates the event location to graph space coordinates (relative to the <see cref="ItemsHost" />).
        /// </summary>
        /// <param name="args">The mouse event.</param>
        /// <returns>A location inside the graph</returns>
        public Point GetLocationInsideEditor(MouseEventArgs args)
            => args.GetPosition(ItemsHost);

        #endregion
    }
}

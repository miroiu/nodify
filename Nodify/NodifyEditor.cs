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
        public static readonly DependencyProperty AutoPanSpeedProperty = DependencyProperty.Register(nameof(AutoPanSpeed), typeof(double), typeof(NodifyEditor), new FrameworkPropertyMetadata(10d));
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

        protected static readonly DependencyPropertyKey IsSelectingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsSelecting), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsSelectingProperty = IsSelectingPropertyKey.DependencyProperty;

        public static readonly DependencyPropertyKey IsPanningPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPanning), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsPanningProperty = IsPanningPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey MouseLocationPropertyKey = DependencyProperty.RegisterReadOnly(nameof(MouseLocation), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty MouseLocationProperty = MouseLocationPropertyKey.DependencyProperty;

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
        /// Gets the current mouse location in graph space coordinates.
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
            editor.OnDisableAutoPanningChanged(editor.DisableAutoPanning);
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
        /// Gets or sets the minimum selected <see cref="ItemContainer"/>s needed to trigger optimizations when reaching the <see cref="OptimizeRenderingZoomOutPercent"/>.
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
        /// Tells if the <see cref="NodifyEditor"/> is doing operations on multiple items at once.
        /// </summary>
        public bool IsBulkUpdatingItems { get; protected set; }

        /// <summary>
        /// Gets the panel that holds all the <see cref="ItemContainer"/>s.
        /// </summary>
        protected internal Panel ItemsHost { get; private set; }

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
        /// Gets where the mouse cursor was in graph space coordinates when a mouse button event occurred.
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
            AddHandler(Connector.PendingConnectionStartedEvent, new PendingConnectionEventHandler(OnConnectionStarted));
            AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnConnectionCompleted));

            AddHandler(BaseConnection.DisconnectEvent, new ConnectionEventHandler(OnRemoveConnection));

            AddHandler(ItemContainer.DragStartedEvent, new DragStartedEventHandler(OnItemsDragStarted));
            AddHandler(ItemContainer.DragCompletedEvent, new DragCompletedEventHandler(OnItemsDragCompleted));
            AddHandler(ItemContainer.DragDeltaEvent, new DragDeltaEventHandler(OnItemsDragDelta));

            Selection = new SelectionHelper(this);

            var transform = new TransformGroup();
            transform.Children.Add(ScaleTransform);
            transform.Children.Add(TranslateTransform);

            SetValue(ViewportTransformPropertyKey, transform);
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsHost = GetTemplateChild(ElementItemsHost) as Panel ?? throw new InvalidOperationException("PART_ItemsHost is missing or is not of type Panel.");

            OnDisableAutoPanningChanged(DisableAutoPanning);
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
                DisableAutoPanning = true;
                DisablePanning = true;
                DisableZooming = true;

                double distance = (newLocation - ViewportLocation).Length;
                double duration = distance / (BringIntoViewSpeed + (distance / 10)) * ViewportZoom;
                duration = Math.Max(0.1, Math.Min(duration, BringIntoViewMaxDuration));

                this.StartAnimation(ViewportLocationProperty, newLocation, duration, (s, e) =>
                {
                    IsPanning = false;
                    DisableAutoPanning = false;
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
            if (IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed && Mouse.Captured != null)
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

                // Update the selecting area because the mouse might not move to update it.
                if (IsSelecting)
                {
                    Point spaceCoords = Mouse.GetPosition(ItemsHost);
                    Selection.Update(spaceCoords);
                }
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
            if (!e.Canceled && (ConnectionStartedCommand?.CanExecute(e.SourceConnector) ?? false))
            {
                ConnectionStartedCommand.Execute(e.SourceConnector);
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

        #region Mouse Events Handlers

        /// <inheritdoc />
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double zoom = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
            ZoomAtPosition(zoom, e.GetPosition(ItemsHost));
            e.Handled = true;
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            CurrentMousePosition = e.GetPosition(this);

            if (CurrentMousePosition != PreviousMousePosition)
            {
                MouseLocation = e.GetPosition(ItemsHost);

                if (IsMouseCaptured)
                {
                    // Panning
                    if (e.RightButton == MouseButtonState.Pressed && !DisablePanning)
                    {
                        ViewportLocation -= (CurrentMousePosition - PreviousMousePosition) / ViewportZoom;
                        IsPanning = true;
                        e.Handled = true;
                    }
                    else if (IsSelecting)
                    {
                        Selection.Update(MouseLocation);
                    }
                    else
                    {
                        ReleaseMouseCapture();
                    }
                }

                PreviousMousePosition = CurrentMousePosition;
            }
        }

        /// <inheritdoc />
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            // End selection if selecting
            Selection.End();
            IsPanning = false;
        }

        /// <inheritdoc />
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Focus();
                CaptureMouse();

                InitialMousePosition = e.GetPosition(ItemsHost);
                Selection.Start(MouseLocation);
                e.Handled = true;
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.Captured == null)
            {
                Focus();
                CaptureMouse();

                InitialMousePosition = e.GetPosition(ItemsHost);
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            double zoom = ViewportZoom;
            ViewportSize = new Size(ActualWidth / zoom, ActualHeight / zoom);

            OnViewportUpdated();
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

            if (!IsSelecting)
            {
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
        }

        #endregion

        #region Selection

        internal void ApplyPreviewingSelection()
        {
            ItemCollection items = Items;
            IList selected = base.SelectedItems;

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

            ItemCollection items = Items;
            IList selected = base.SelectedItems;

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
        }

        /// <summary>
        /// Unselect the <see cref="ItemContainer"/>s in the specified <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to look for <see cref="ItemContainer"/>s.</param>
        /// <param name="fit">True to check if the <paramref name="area"/> contains the <see cref="ItemContainer"/>. <br /> False to check if <paramref name="area"/> intersects the <see cref="ItemContainer"/>.</param>
        public void UnselectArea(Rect area, bool fit = false)
        {
            IList items = base.SelectedItems;

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
                    for (var i = 0; i < _selectedContainers.Count; i++)
                    {
                        ItemContainer container = _selectedContainers[i];
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
                    for (var i = 0; i < _selectedContainers.Count; i++)
                    {
                        ItemContainer container = _selectedContainers[i];
                        var r = (TranslateTransform)container.RenderTransform;

                        r.X = 0;
                        r.Y = 0;

                        container.OnPreviewLocationChanged(container.Location);
                    }
                }
                else
                {
                    IsBulkUpdatingItems = true;

                    for (var i = 0; i < _selectedContainers.Count; i++)
                    {
                        ItemContainer container = _selectedContainers[i];
                        var r = (TranslateTransform)container.RenderTransform;

                        Point result = container.Location + new Vector(r.X, r.Y);

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
                    ItemsHost.InvalidateArrange();
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
            IList selectedItems = base.SelectedItems;

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
                for (var i = 0; i < selectedItems.Count; i++)
                {
                    var container = (ItemContainer)ItemContainerGenerator.ContainerFromItem(selectedItems[i]);
                    if (container.IsDraggable)
                    {
                        _selectedContainers.Add(container);
                    }
                }

                e.Handled = true;
            }
        }

        #endregion
    }
}

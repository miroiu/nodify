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
    [StyleTypedProperty(Property = nameof(ItemContainerTheme), StyleTargetType = typeof(ItemContainer))]
    [StyleTypedProperty(Property = nameof(DecoratorContainerStyle), StyleTargetType = typeof(DecoratorContainer))]
    [StyleTypedProperty(Property = nameof(SelectionRectangleStyle), StyleTargetType = typeof(Rectangle))]
    [ContentProperty(nameof(Decorators))]
    [DefaultProperty(nameof(Decorators))]
    public partial class NodifyEditor : MultiSelector
    {
        protected const string ElementItemsHost = "PART_ItemsHost";

        #region Viewport

        public static readonly StyledProperty<double> ViewportZoomProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(ViewportZoom), BoxValue.Double1, defaultBindingMode: BindingMode.TwoWay, coerce: ConstrainViewportZoomToRange);
        public static readonly StyledProperty<double> MinViewportZoomProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(MinViewportZoom), 0.1d, coerce: CoerceMinViewportZoom);
        public static readonly StyledProperty<double> MaxViewportZoomProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(MaxViewportZoom), BoxValue.Double2, coerce: CoerceMaxViewportZoom);
        public static readonly StyledProperty<Point> ViewportLocationProperty = AvaloniaProperty.Register<NodifyEditor, Point>(nameof(ViewportLocation), BoxValue.Point, defaultBindingMode: BindingMode.TwoWay);
        public static readonly StyledProperty<Size> ViewportSizeProperty = AvaloniaProperty.Register<NodifyEditor, Size>(nameof(ViewportSize), BoxValue.Size);
        public static readonly StyledProperty<Rect> ItemsExtentProperty = AvaloniaProperty.Register<NodifyEditor, Rect>(nameof(ItemsExtent), BoxValue.Rect);
        public static readonly StyledProperty<Rect> DecoratorsExtentProperty = AvaloniaProperty.Register<NodifyEditor, Rect>(nameof(DecoratorsExtent), BoxValue.Rect);
        public static readonly DirectProperty<NodifyEditor, bool> IsMouseCaptureWithinProperty = AvaloniaProperty.RegisterDirect<NodifyEditor, bool>(nameof(IsMouseCaptureWithin), x => x.IsMouseCaptureWithin);
        
        public static readonly StyledProperty<Transform> ViewportTransformProperty = AvaloniaProperty.Register<NodifyEditor, Transform>(nameof(ViewportTransform), new TransformGroup());
        /// https://github.com/AvaloniaUI/Avalonia/issues/11959 workaround
        public static readonly StyledProperty<Transform> DpiScaledViewportTransformProperty = AvaloniaProperty.Register<NodifyEditor, Transform>(nameof(DpiScaledViewportTransform), new TransformGroup());
 
        #region Callbacks

        private static void UpdateViewportTransform(NodifyEditor editor)
        {
            var transform = new TransformGroup();
            transform.Children.Add(editor.ScaleTransform);
            transform.Children.Add(editor.TranslateTransform);

            editor.SetCurrentValue(ViewportTransformProperty, transform);
            
            var dpiScaledTransform = new TransformGroup();
            dpiScaledTransform.Children.Add(editor.ScaleTransform);
            dpiScaledTransform.Children.Add(editor.DpiScaledTranslateTransform);
            
            editor.SetCurrentValue(DpiScaledViewportTransformProperty, dpiScaledTransform);
        }

        private static void OnViewportLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            var translate = (Point)e.NewValue;

            // once https://github.com/AvaloniaUI/Avalonia/issues/15097 is fixed, dont't create new instances
            editor.TranslateTransform = new();
            editor.DpiScaledTranslateTransform = new();

            editor.TranslateTransform.X = -translate.X * editor.ViewportZoom;
            editor.TranslateTransform.Y = -translate.Y * editor.ViewportZoom;

            var renderScale = (editor.GetVisualRoot()?.RenderScaling ?? 1);
            editor.DpiScaledTranslateTransform.X = editor.TranslateTransform.X * renderScale;
            editor.DpiScaledTranslateTransform.Y = editor.TranslateTransform.Y * renderScale;

            editor.OnViewportUpdated();
            UpdateViewportTransform(editor); // won't be required once https://github.com/AvaloniaUI/Avalonia/issues/15097 if fixed
        }

        private static void OnViewportZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            double zoom = (double)e.NewValue;

            // once https://github.com/AvaloniaUI/Avalonia/issues/15097 is fixed, dont't create new instance
            editor.ScaleTransform = new();
            
            editor.ScaleTransform.ScaleX = zoom;
            editor.ScaleTransform.ScaleY = zoom;

            editor.ViewportSize = new Size(editor.Bounds.Width / zoom, editor.Bounds.Height / zoom);
            UpdateViewportTransform(editor); // won't be required once https://github.com/AvaloniaUI/Avalonia/issues/15097 if fixed

            editor.ApplyRenderingOptimizations();
            editor.OnViewportUpdated();
        }

        private static void OnMinViewportZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(MaxViewportZoomProperty);
            zoom.CoerceValue(ViewportZoomProperty);
        }

        private static double CoerceMinViewportZoom(DependencyObject d, double value)
            => (double)value > 0.1d ? value : 0.1d;

        private static void OnMaxViewportZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoom = (NodifyEditor)d;
            zoom.CoerceValue(ViewportZoomProperty);
        }

        private static double CoerceMaxViewportZoom(DependencyObject d, double value)
        {
            var editor = (NodifyEditor)d;
            double min = editor.MinViewportZoom;

            return (double)value < min ? min : value;
        }

        private static double ConstrainViewportZoomToRange(DependencyObject d, double value)
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

        public static readonly RoutedEvent<RoutedEventArgs> ViewportUpdatedEvent = RoutedEvent.Register<RoutedEventArgs>(nameof(ViewportUpdated), RoutingStrategies.Bubble, typeof(NodifyEditor));

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
        protected TranslateTransform TranslateTransform = new TranslateTransform();
        
        /// https://github.com/AvaloniaUI/Avalonia/issues/11959 workaround
        protected TranslateTransform DpiScaledTranslateTransform = new TranslateTransform();

        /// <summary>
        /// Gets the transform used to zoom on the viewport.
        /// </summary>
        protected ScaleTransform ScaleTransform = new ScaleTransform();

        /// <summary>
        /// Gets the transform that is applied to all child controls.
        /// </summary>
        public Transform ViewportTransform
        {
            get { return (Transform)GetValue(ViewportTransformProperty); }
            set { SetValue(ViewportTransformProperty, value); }
        }
        
        /// https://github.com/AvaloniaUI/Avalonia/issues/11959 workaround
        /// <summary>
        /// Gets the transform that is applied to all child controls.
        /// </summary>
        public Transform DpiScaledViewportTransform
        {
            get { return (Transform)GetValue(DpiScaledViewportTransformProperty); }
            set { SetValue(DpiScaledViewportTransformProperty, value); }
        }

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

        private bool isMouseCaptureWithin;
        /// <summary>
        /// Gets a value that indicates whether the mouse is captured to the <see cref="NodifyEditor"/>.
        /// </summary>
        public bool IsMouseCaptureWithin
        {
            get => isMouseCaptureWithin;
            protected set => SetAndRaise(IsMouseCaptureWithinProperty, ref isMouseCaptureWithin, value);
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
                    //ItemsHost.CacheMode = shouldCache ? new BitmapCache(1.0 / zoom) : null;
                }
                else
                {
                    //ItemsHost.CacheMode = null;
                }
            }
        }

        #endregion

        #region Cosmetic Dependency Properties

        public static readonly StyledProperty<double> BringIntoViewSpeedProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(BringIntoViewSpeed), BoxValue.Double1000);
        public static readonly StyledProperty<double> BringIntoViewMaxDurationProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(BringIntoViewMaxDuration), BoxValue.Double1);
        public static readonly StyledProperty<bool> DisplayConnectionsOnTopProperty = AvaloniaProperty.Register<NodifyEditor, bool>(nameof(DisplayConnectionsOnTop), BoxValue.False);
        public static readonly StyledProperty<bool> DisableAutoPanningProperty = AvaloniaProperty.Register<NodifyEditor, bool>(nameof(DisableAutoPanning), BoxValue.False);
        public static readonly StyledProperty<double> AutoPanSpeedProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(AutoPanSpeed), 15d);
        public static readonly StyledProperty<double> AutoPanEdgeDistanceProperty = AvaloniaProperty.Register<NodifyEditor, double>(nameof(AutoPanEdgeDistance), 15d);
        public static readonly StyledProperty<DataTemplate> ConnectionTemplateProperty = AvaloniaProperty.Register<NodifyEditor, DataTemplate>(nameof(ConnectionTemplate));
        public static readonly StyledProperty<DataTemplate> DecoratorTemplateProperty = AvaloniaProperty.Register<NodifyEditor, DataTemplate>(nameof(DecoratorTemplate));
        public static readonly StyledProperty<DataTemplate> PendingConnectionTemplateProperty = AvaloniaProperty.Register<NodifyEditor, DataTemplate>(nameof(PendingConnectionTemplate));
        public static readonly StyledProperty<ControlTheme> SelectionRectangleStyleProperty = AvaloniaProperty.Register<NodifyEditor, ControlTheme>(nameof(SelectionRectangleStyle));
        public static readonly StyledProperty<ControlTheme> DecoratorContainerStyleProperty = AvaloniaProperty.Register<NodifyEditor, ControlTheme>(nameof(DecoratorContainerStyle));

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
        public ControlTheme SelectionRectangleStyle
        {
            get => (ControlTheme)GetValue(SelectionRectangleStyleProperty);
            set => SetValue(SelectionRectangleStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the style to use for the <see cref="DecoratorContainer"/>.
        /// </summary>
        public ControlTheme DecoratorContainerStyle
        {
            get => (ControlTheme)GetValue(DecoratorContainerStyleProperty);
            set => SetValue(DecoratorContainerStyleProperty, value);
        }

        #endregion

        #region Readonly Dependency Properties

        public static readonly DirectProperty<NodifyEditor, Rect> SelectedAreaProperty = AvaloniaProperty.RegisterDirect<NodifyEditor, Rect>(nameof(SelectedArea), x => x.SelectedArea);

        public static readonly DirectProperty<NodifyEditor, bool> IsSelectingProperty = AvaloniaProperty.RegisterDirect<NodifyEditor, bool>(nameof(IsSelecting), x => x.IsSelecting);

        public static readonly DirectProperty<NodifyEditor, bool> IsPanningProperty = AvaloniaProperty.RegisterDirect<NodifyEditor, bool>(nameof(IsPanning), x => x.IsPanning);

        public static readonly DirectProperty<NodifyEditor, Point> MouseLocationProperty = AvaloniaProperty.RegisterDirect<NodifyEditor, Point>(nameof(MouseLocation), x => x.MouseLocation);

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

        private Rect selectedArea;
        /// <summary>
        /// Gets the currently selected area while <see cref="IsSelecting"/> is true.
        /// </summary>
        public Rect SelectedArea
        {
            get => selectedArea;
            internal set => SetAndRaise(SelectedAreaProperty, ref selectedArea, value);
        }

        private bool isSelecting;
        /// <summary>
        /// Gets a value that indicates whether a selection operation is in progress.
        /// </summary>
        public bool IsSelecting
        {
            get => isSelecting;
            internal set => SetAndRaise(IsSelectingProperty, ref isSelecting, value);
        }

        private bool isPanning;
        /// <summary>
        /// Gets a value that indicates whether a panning operation is in progress.
        /// </summary>
        public bool IsPanning
        {
            get => isPanning;
            protected internal set => SetAndRaise(IsPanningProperty, ref isPanning, value);
        }

        private Point mouseLocation;
        /// <summary>
        /// Gets the current mouse location in graph space coordinates (relative to the <see cref="ItemsHost" />).
        /// </summary>
        public Point MouseLocation
        {
            get => mouseLocation;
            protected set => SetAndRaise(MouseLocationProperty, ref mouseLocation, value);
        }

        /// <summary>
        /// Gets the current mouse location relative to NotifyEditor
        /// </summary>
        private Point RelativeMouseLocation;

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<IEnumerable> ConnectionsProperty = AvaloniaProperty.Register<NodifyEditor, IEnumerable>(nameof(Connections));
        public new static readonly StyledProperty<IList> SelectedItemsProperty = AvaloniaProperty.Register<NodifyEditor, IList>(nameof(SelectedItems));
        public static readonly StyledProperty<object> PendingConnectionProperty = AvaloniaProperty.Register<NodifyEditor, object>(nameof(PendingConnection));
        public static readonly StyledProperty<uint> GridCellSizeProperty = AvaloniaProperty.Register<NodifyEditor, uint>(nameof(GridCellSize), BoxValue.UInt1, coerce: OnCoerceGridCellSize);
        public static readonly StyledProperty<bool> DisableZoomingProperty = AvaloniaProperty.Register<NodifyEditor, bool>(nameof(DisableZooming), BoxValue.False);
        public static readonly StyledProperty<bool> DisablePanningProperty = AvaloniaProperty.Register<NodifyEditor, bool>(nameof(DisablePanning), BoxValue.False);
        public static readonly StyledProperty<bool> EnableRealtimeSelectionProperty = AvaloniaProperty.Register<NodifyEditor, bool>(nameof(EnableRealtimeSelection), BoxValue.False);
        public static readonly StyledProperty<IEnumerable> DecoratorsProperty = AvaloniaProperty.Register<NodifyEditor, IEnumerable>(nameof(Decorators));

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((NodifyEditor)d).OnSelectedItemsSourceChanged((IList)e.OldValue, (IList)e.NewValue);

        private static uint OnCoerceGridCellSize(DependencyObject d, uint value)
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
        [Content]
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

        public static readonly StyledProperty<ICommand> ConnectionCompletedCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(ConnectionCompletedCommand));
        public static readonly StyledProperty<ICommand> ConnectionStartedCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(ConnectionStartedCommand));
        public static readonly StyledProperty<ICommand> DisconnectConnectorCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(DisconnectConnectorCommand));
        public static readonly StyledProperty<ICommand> RemoveConnectionCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(RemoveConnectionCommand));
        public static readonly StyledProperty<ICommand> ItemsDragStartedCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(ItemsDragStartedCommand));
        public static readonly StyledProperty<ICommand> ItemsDragCompletedCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(ItemsDragCompletedCommand));
        public static readonly StyledProperty<ICommand> ItemsSelectStartedCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(ItemsSelectStartedCommand));
        public static readonly StyledProperty<ICommand> ItemsSelectCompletedCommandProperty = AvaloniaProperty.Register<NodifyEditor, ICommand>(nameof(ItemsSelectCompletedCommand));

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
        public static double AutoPanningTickRate { get; set; } = 16;

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
        protected internal ItemsPresenter ItemsHost { get; private set; }

        private Border MainBorderPart { get; set; }

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
                    var container = (ItemContainer)ContainerFromItem(selectedItems[i]);
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
            FocusableProperty.OverrideDefaultValue<NodifyEditor>(true);
            IsSelectingProperty.Changed.AddClassHandler<NodifyEditor>(OnIsSelectingChanged);
            DisableAutoPanningProperty.Changed.AddClassHandler<NodifyEditor>(OnDisableAutoPanningChanged);
            GridCellSizeProperty.Changed.AddClassHandler<NodifyEditor>(OnGridCellSizeChanged);
            DisablePanningProperty.Changed.AddClassHandler<NodifyEditor>(OnDisablePanningChanged);
            ViewportLocationProperty.Changed.AddClassHandler<NodifyEditor>(OnViewportLocationChanged);
            ViewportZoomProperty.Changed.AddClassHandler<NodifyEditor>(OnViewportZoomChanged);
            MinViewportZoomProperty.Changed.AddClassHandler<NodifyEditor>(OnMinViewportZoomChanged);
            MaxViewportZoomProperty.Changed.AddClassHandler<NodifyEditor>(OnMaxViewportZoomChanged);
            SelectedItemsProperty.Changed.AddClassHandler<NodifyEditor>(OnSelectedItemsSourceChanged);

            EditorCommands.Register(typeof(NodifyEditor));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodifyEditor"/> class.
        /// </summary>
        public NodifyEditor()
        {
            AddHandler(Gestures.PointerTouchPadGestureMagnifyEvent, OnPointerTouchPadGestureMagnify);
            AddHandler(Connector.DisconnectEvent, new ConnectorEventHandler(OnConnectorDisconnected));
            AddHandler(Connector.PendingConnectionStartedEvent, new PendingConnectionEventHandler(OnConnectionStarted));
            AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnConnectionCompleted));

            AddHandler(BaseConnection.DisconnectEvent, new ConnectionEventHandler(OnRemoveConnection));

            AddHandler(ItemContainer.DragStartedEvent, OnItemsDragStarted);
            AddHandler(ItemContainer.DragCompletedEvent, OnItemsDragCompleted);
            AddHandler(ItemContainer.DragDeltaEvent, OnItemsDragDelta);

            UpdateViewportTransform(this);
            
            _states.Push(GetInitialState());

            CanSelectMultipleItems = true;
        }

        /// <inheritdoc />
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            ItemsHost = e.NameScope.Find<ItemsPresenter>("PART_ItemsPresenter") ?? throw new InvalidOperationException("PART_ItemsHost is missing or is not of type Panel.");

            MainBorderPart = e.NameScope.Get<Border>("PART_MainBorder");

            OnDisableAutoPanningChanged(DisableAutoPanning);

            State.Enter(null, null);
        }
        
        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
            => new ItemContainer(this)
            {
                RenderTransform = new TranslateTransform(),
                RenderTransformOrigin = new RelativePoint(0, 0, RelativeUnit.Relative)
            };

        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is ItemContainer;

        #endregion

        #region Methods

        private const int MouseWheelDeltaForOneLine = 120;
        
        /// <summary>
        /// Zoom in at the viewports center
        /// </summary>
        public void ZoomIn() => ZoomAtPosition(Math.Pow(2.0, 120.0 / 3.0 / MouseWheelDeltaForOneLine), (Point)((Vector)ViewportLocation + ViewportSize.ToVector() / 2));

        /// <summary>
        /// Zoom out at the viewports center
        /// </summary>
        public void ZoomOut() => ZoomAtPosition(Math.Pow(2.0, -120.0 / 3.0 / MouseWheelDeltaForOneLine), (Point)((Vector)ViewportLocation + ViewportSize.ToVector() / 2));

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

                if (Math.Abs(prevZoom - ViewportZoom) > 0.0000001)
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
            Point newLocation = (Point)((Vector)point - ViewportSize.ToVector() / 2);

            if (animated && newLocation != ViewportLocation)
            {
                IsPanning = true;
                SetCurrentValue(DisablePanningProperty, true);
                SetCurrentValue(DisableZoomingProperty, true);

                double distance = (newLocation - ViewportLocation).Length();
                double duration = distance / (BringIntoViewSpeed + (distance / 10)) * ViewportZoom;
                duration = Math.Max(0.1, Math.Min(duration, BringIntoViewMaxDuration));

                this.StartAnimation(ViewportLocationProperty, newLocation, duration, (s, e) =>
                {
                    IsPanning = false;
                    SetCurrentValue(DisablePanningProperty, false);
                    SetCurrentValue(DisableZoomingProperty, false);

                    onFinish?.Invoke();
                });
            }
            else
            {
                SetCurrentValue(ViewportLocationProperty, newLocation);
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
            extent.Inflate(FitToScreenExtentMargin);

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
                Point mousePosition = RelativeMouseLocation;
                double edgeDistance = AutoPanEdgeDistance;
                double autoPanSpeed = Math.Min(AutoPanSpeed, AutoPanSpeed * AutoPanningTickRate) / (ViewportZoom * 2);
                double x = ViewportLocation.X;
                double y = ViewportLocation.Y;

                if (mousePosition.X <= edgeDistance)
                {
                    x -= autoPanSpeed;
                }
                else if (mousePosition.X >= Bounds.Width - edgeDistance)
                {
                    x += autoPanSpeed;
                }

                if (mousePosition.Y <= edgeDistance)
                {
                    y -= autoPanSpeed;
                }
                else if (mousePosition.Y >= Bounds.Height - edgeDistance)
                {
                    y += autoPanSpeed;
                }

                SetCurrentValue(ViewportLocationProperty, new Point(x, y));

                State.HandleAutoPanning(null);
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
                    DispatcherPriority.Background, HandleAutoPanning);
            }
            else
            {
                _autoPanningTimer.Interval = TimeSpan.FromMilliseconds(AutoPanningTickRate);
                _autoPanningTimer.Start();
            }
        }

        #endregion

        #region Connector handling

        private void OnConnectorDisconnected(object? sender, ConnectorEventArgs e)
        {
            if (!e.Handled && (DisconnectConnectorCommand?.CanExecute(e.Connector) ?? false))
            {
                DisconnectConnectorCommand.Execute(e.Connector);
                e.Handled = true;
            }
        }

        private void OnConnectionStarted(object? sender, PendingConnectionEventArgs e)
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

        private void OnConnectionCompleted(object? sender, PendingConnectionEventArgs e)
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

        private void OnRemoveConnection(object? sender, ConnectionEventArgs e)
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
        public void PushState(EditorState state, MouseEventArgs e)
        {
            var prev = State;
            _states.Push(state);
            state.Enter(prev, e);
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
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            // Needed to not steal mouse capture from children
            if (e.Pointer.Captured == null || ReferenceEquals(e.Pointer.Captured, this) 
                                           || ReferenceEquals(e.Pointer.Captured, MainBorderPart)) // <-- this is a hack. Apparently Avalonia captures the pointer before I am even able to do anything. Is there any better way?
            {
                Focus();
                e.Pointer.Capture(this);
                IsMouseCaptureWithin = true;

                MouseLocation = e.GetPosition(ItemsHost);
                State.HandleMouseDown(new MouseButtonEventArgs(e));
            }
        }

        /// <inheritdoc />
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);
            if (e.Handled)
                return;
            
            MouseLocation = e.GetPosition(ItemsHost);
            State.HandleMouseUp(new MouseButtonEventArgs(e));

            // Release the mouse capture if all the mouse buttons are released
            if (/*ReferenceEquals(e.Pointer.Captured, this) &&*/e.GetCurrentPoint(this) is { Properties: { IsLeftButtonPressed: false, IsRightButtonPressed: false, IsMiddleButtonPressed: false} })
            {
                e.Pointer.Capture(null);
                IsMouseCaptureWithin = false;
            }

            // Disable context menu if selecting
            if (IsSelecting)
            {
                e.Handled = true;
            }
        }

        /// <inheritdoc />
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            MouseLocation = e.GetPosition(ItemsHost);
            RelativeMouseLocation = e.GetPosition(this);
            State.HandleMouseMove(new MouseMoveEventArgs(e));
        }

        /// <inheritdoc />
        protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
            => PopAllStates();

        /// <inheritdoc />
        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            State.HandleMouseWheel(new MouseWheelEventArgs());

            if (!e.Handled && EditorGestures.Zoom == e.KeyModifiers)
            {
                double zoom = Math.Pow(2.0, e.Delta.Y / 3.0 / MouseWheelDeltaForOneLine * MouseWheelAvaloniaToWpfScale);
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
                    UnselectAll();
                    break;

                case NotifyCollectionChangedAction.Add:
                    IList? newItems = e.NewItems;
                    if (newItems != null)
                    {
                        for (var i = 0; i < newItems.Count; i++)
                        {
                            Selection.Select(Items.IndexOf(newItems[i]));
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    IList? oldItems = e.OldItems;
                    if (oldItems != null)
                    {
                        for (var i = 0; i < oldItems.Count; i++)
                        {
                            Selection.Deselect(Items.IndexOf(oldItems[i]));
                        }
                    }
                    break;
            }
        }

        /// <inheritdoc />
        protected override void OnSelectionChanged(SelectionModelSelectionChangedEventArgs e)
        {
            IList? selected = SelectedItems;
            if (selected != null)
            {
                IReadOnlyList<object?> added = e.SelectedItems;
                for (var i = 0; i < added.Count; i++)
                {
                    // Ensure no duplicates are added
                    if (!selected.Contains(added[i]))
                    {
                        selected.Add(added[i]);
                    }
                }

                IReadOnlyList<object?> removed = e.DeselectedItems;
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

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsPreviewingSelection == true)
                {
                    Selection.Select(i);
                }
                else if (container.IsPreviewingSelection == false)
                {
                    Selection.Deselect(i);
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
                        Selection.Deselect(i);
                    }
                    else
                    {
                        Selection.Select(i);
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
                Selection.Clear();
            }

            ItemCollection items = Items;

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsSelectableInArea(area, fit))
                {
                    Selection.Select(i);
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
            IReadOnlyList<object?> items = Selection.SelectedItems;

            IsSelecting = true;
            BeginUpdateSelectedItems();
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)ContainerFromItem(items[i]);
                if (container.IsSelectableInArea(area, fit))
                {
                    Selection.Deselect(Items.IndexOf(items[i]));
                }
            }
            EndUpdateSelectedItems();
            IsSelecting = false;
        }

        #endregion

        #region Dragging

        private void OnItemsDragDelta(object? sender, DragDeltaEventArgs e)
        {
            _draggingStrategy?.Update(new Vector(e.HorizontalChange, e.VerticalChange));
        }

        private void OnItemsDragCompleted(object? sender, DragCompletedEventArgs e)
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
                ItemsHost.Panel?.InvalidateArrange();
            }

            if (ItemsDragCompletedCommand?.CanExecute(null) ?? false)
            {
                ItemsDragCompletedCommand.Execute(null);
            }
        }

        private void OnItemsDragStarted(object? sender, DragStartedEventArgs e)
        {
            if (EnableDraggingContainersOptimizations)
            {
                _draggingStrategy = new DraggingOptimized(this);
            }
            else
            {
                _draggingStrategy = new DraggingSimple(this);
            }

            _draggingStrategy.Start(new Vector(e.HorizontalOffset, e.VerticalOffset));

            if (Selection.Count > 0)
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
        protected override void OnSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnSizeChanged(sizeInfo);

            double zoom = ViewportZoom;
            SetCurrentValue(ViewportSizeProperty, new Size(Bounds.Width / zoom, Bounds.Height / zoom));

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
            => relativeTo.TranslatePoint(location, ItemsHost) ?? default;

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
        public Point GetLocationInsideEditor(PointerEventArgs args)
            => args.GetPosition(ItemsHost);

        #endregion
    }
}

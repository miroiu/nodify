using System.Collections.Generic;
using System.Windows;

namespace Nodify.Playground
{
    public enum ConnectionStyle
    {
        Default,
        Line,
        Circuit,
        Step
    }

    public class EditorSettings : ObservableObject
    {
        private readonly IReadOnlyCollection<ISettingViewModel> _settings;
        public IEnumerable<ISettingViewModel> Settings => PlaygroundSettings.Instance.FilterAndSort(_settings);

        private readonly IReadOnlyCollection<ISettingViewModel> _advancedSettings;
        public IEnumerable<ISettingViewModel> AdvancedSettings => PlaygroundSettings.Instance.FilterAndSort(_advancedSettings);

        private EditorSettings()
        {
            PlaygroundSettings.Instance.PropertyChanged += OnSearchTextChanged;

            _settings = new List<ISettingViewModel>()
            {
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableRealtimeSelection,
                    val => Instance.EnableRealtimeSelection = val,
                    "Realtime selection: ",
                    "Selects items when finished if disabled."),
                new ProxySettingViewModel<bool>(
                    () => Instance.SelectableNodes,
                    val => Instance.SelectableNodes = val,
                    "Selectable nodes: ",
                    "Whether nodes can be selected."),
                new ProxySettingViewModel<bool>(
                    () => Instance.DraggableNodes,
                    val => Instance.DraggableNodes= val,
                    "Draggable nodes: ",
                    "Whether nodes can be dragged."),
                new ProxySettingViewModel<bool>(
                    () => Instance.CanSelectMultipleNodes,
                    val => Instance.CanSelectMultipleNodes = val,
                    "Can select multiple nodes: "),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnablePendingConnectionSnapping,
                    val => Instance.EnablePendingConnectionSnapping = val,
                    "Pending connection snapping: ",
                    "Whether to snap the pending connection to connectors"),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnablePendingConnectionPreview,
                    val => Instance.EnablePendingConnectionPreview = val,
                    "Pending connection preview: ",
                    "Show information about the pending connection."),
                new ProxySettingViewModel<bool>(
                    () => Instance.AllowConnectingToConnectorsOnly,
                    val => Instance.AllowConnectingToConnectorsOnly = val,
                    "Disable drop connection on node: ",
                    "Can connect directly to nodes if enabled"),
                new ProxySettingViewModel<bool>(
                    () => Instance.DisableAutoPanning,
                    val => Instance.DisableAutoPanning = val,
                    "Disable auto panning: "),
                new ProxySettingViewModel<bool>(
                    () => Instance.DisablePanning,
                    val => Instance.DisablePanning = val,
                    "Disable panning: "),
                new ProxySettingViewModel<bool>(
                    () => Instance.DisableZooming,
                    val => Instance.DisableZooming = val,
                    "Disable zooming: "),
                new ProxySettingViewModel<uint>(
                    () => Instance.GridSpacing,
                    val => Instance.GridSpacing = val,
                    "Grid spacing: ",
                    "Snapping value in pixels"),
                new ProxySettingViewModel<PointEditor>(
                    () => Instance.Location,
                    val => Instance.Location = val,
                    "Location: ",
                    "The viewport's location."),
                new ProxySettingViewModel<double>(
                    () => Instance.Zoom,
                    val => Instance.Zoom = val,
                    "Zoom: ",
                    "The viewport's zoom. Not accurate when trying to zoom outside the MinViewportZoom and MaxViewportZoom because of dependency property coercion not updating the binding with the final result."),
                new ProxySettingViewModel<double>(
                    () => Instance.MinZoom,
                    val => Instance.MinZoom = val,
                    "Min zoom: "),
                new ProxySettingViewModel<double>(
                    () => Instance.MaxZoom,
                    val => Instance.MaxZoom = val,
                    "Max zoom: "),
                new ProxySettingViewModel<double>(
                    () => Instance.AutoPanningSpeed,
                    val => Instance.AutoPanningSpeed = val,
                    "Auto panning speed: ",
                    "Speed value in pixels per tick"),
                new ProxySettingViewModel<double>(
                    () => Instance.AutoPanningEdgeDistance,
                    val => Instance.AutoPanningEdgeDistance = val,
                    "Auto panning edge distance: ",
                    "Distance from edge to trigger auto panning"),
                new ProxySettingViewModel<bool>(
                    () => Instance.SelectableConnections,
                    val => Instance.SelectableConnections = val,
                    "Selectable connections: ",
                    "Whether connections can be selected."),
                new ProxySettingViewModel<bool>(
                    () => Instance.CanSelectMultipleConnections,
                    val => Instance.CanSelectMultipleConnections = val,
                    "Can select multiple connections: "),
                new ProxySettingViewModel<ConnectionStyle>(
                    () => Instance.ConnectionStyle,
                    val => Instance.ConnectionStyle = val,
                    "Connection style: "),
                new ProxySettingViewModel<string?>(
                    () => Instance.ConnectionText,
                    val => Instance.ConnectionText = val,
                    "Connection text: "),
                new ProxySettingViewModel<double>(
                    () => Instance.CircuitConnectionAngle,
                    val => Instance.CircuitConnectionAngle = val,
                    "Connection angle: ",
                    "Applies to circuit connection style"),
                new ProxySettingViewModel<double>(
                    () => Instance.ConnectionSpacing,
                    val => Instance.ConnectionSpacing = val,
                    "Connection spacing: ",
                    "The distance between the start point and the where the angle breaks"),
                new ProxySettingViewModel<PointEditor>(
                    () => Instance.ConnectionArrowSize,
                    val => Instance.ConnectionArrowSize = val,
                    "Connection arrowhead size: ",
                    "The size of the arrowhead."),
                new ProxySettingViewModel<uint>(
                    () => Instance.DirectionalArrowsCount,
                    val => Instance.DirectionalArrowsCount = val,
                    "Directional arrows count: ",
                    "The number of arrowheads to draw on the line flowing in the direction of the connection."),
                new ProxySettingViewModel<double>(
                    () => Instance.DirectionalArrowsOffset,
                    val => Instance.DirectionalArrowsOffset = val,
                    "Directional arrows offset: ",
                    "Used to animate the directional arrowheads flowing in the direction of the connection (value is between 0 and 1)."),
                new ProxySettingViewModel<bool>(
                    () => Instance.IsAnimatingConnections,
                    val => Instance.IsAnimatingConnections = val,
                    "Animate directional arrows: ",
                    "Used to animate the directional arrowheads by animating the DirectionalArrowsOffset value"),
                new ProxySettingViewModel<double>(
                    () => Instance.DirectionalArrowsAnimationDuration,
                    val => Instance.DirectionalArrowsAnimationDuration = val,
                    "Arrows animation duration: ",
                    "The duration in seconds of a directional arrowhead flowing from start to end."),
                new ProxySettingViewModel<ArrowHeadEnds>(
                    () => Instance.ArrowHeadEnds,
                    val => Instance.ArrowHeadEnds = val,
                    "Connection arrowhead end: ",
                    "The location of the arrowhead."),
                new ProxySettingViewModel<ArrowHeadShape>(
                    () => Instance.ArrowHeadShape,
                    val => Instance.ArrowHeadShape = val,
                    "Connection arrowhead shape: ",
                    "The shape of the arrow head."),
                new ProxySettingViewModel<ConnectionOffsetMode>(
                    () => Instance.ConnectionSourceOffsetMode,
                    val => Instance.ConnectionSourceOffsetMode = val,
                    "Connection source offset mode: "),
                new ProxySettingViewModel<ConnectionOffsetMode>(
                    () => Instance.ConnectionTargetOffsetMode,
                    val => Instance.ConnectionTargetOffsetMode = val,
                    "Connection target offset mode: "),
                new ProxySettingViewModel<PointEditor>(
                    () => Instance.ConnectionSourceOffset,
                    val => Instance.ConnectionSourceOffset = val,
                    "Connection source offset: "),
                new ProxySettingViewModel<PointEditor>(
                    () => Instance.ConnectionTargetOffset,
                    val => Instance.ConnectionTargetOffset = val,
                    "Connection target offset: "),
                new ProxySettingViewModel<bool>(
                    () => Instance.DisplayConnectionsOnTop,
                    val => Instance.DisplayConnectionsOnTop = val,
                    "Display connections on top: "),
                new ProxySettingViewModel<double>(
                    () => Instance.BringIntoViewSpeed,
                    val => Instance.BringIntoViewSpeed = val,
                    "Bring into view speed: ",
                    "Bring location into view animation speed in pixels per second"),
                new ProxySettingViewModel<double>(
                    () => Instance.BringIntoViewMaxDuration,
                    val => Instance.BringIntoViewMaxDuration = val,
                    "Bring into view max duration: ",
                    "Bring location into view max animation duration"),
                new ProxySettingViewModel<GroupingMovementMode>(
                    () => Instance.GroupingNodeMovement,
                    val => Instance.GroupingNodeMovement = val,
                    "Grouping node movement: ",
                    "Whether the grouping node is sticky or not"),
            };

            _advancedSettings = new List<ISettingViewModel>()
            {
                new ProxySettingViewModel<double>(
                    () => Instance.HandleRightClickAfterPanningThreshold,
                    val => Instance.HandleRightClickAfterPanningThreshold = val,
                    "Disable context menu after panning: ",
                    "Disable after mouse moved this far"),
                new ProxySettingViewModel<double>(
                    () => Instance.AutoPanningTickRate,
                    val => Instance.AutoPanningTickRate = val,
                    "Auto panning tick rate: ",
                    "How often is the new position calculated in milliseconds. Disable and enable auto panning for this to have effect."),
                new ProxySettingViewModel<bool>(
                    () => Instance.AllowCuttingCancellation,
                    val => Instance.AllowCuttingCancellation = val,
                    "Allow cutting cancellation: ",
                    "Right click to cancel cutting."),
                new ProxySettingViewModel<bool>(
                    () => Instance.AllowDraggingCancellation,
                    val => Instance.AllowDraggingCancellation = val,
                    "Allow dragging cancellation: ",
                    "Right click to cancel dragging."),
                new ProxySettingViewModel<bool>(
                    () => Instance.AllowPendingConnectionCancellation,
                    val => Instance.AllowPendingConnectionCancellation = val,
                    "Allow pending connection cancellation: ",
                    "Right click to cancel pending connection."),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableSnappingCorrection,
                    val => Instance.EnableSnappingCorrection = val,
                    "Enable snapping correction: ",
                    "Correct the final position when moving a selection"),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableCuttingLinePreview,
                    val => Instance.EnableCuttingLinePreview = val,
                    "Enable cutting line preview: ",
                    "Applies custom connection style on intersection (hurts performance due to hit testing)."),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableConnectorOptimizations,
                    val => Instance.EnableConnectorOptimizations = val,
                    "Enable connector optimizations: ",
                    "Enables optimizations for connectors based on viewport distance and minimum selected nodes."),
                new ProxySettingViewModel<double>(
                    () => Instance.OptimizeSafeZone,
                    val => Instance.OptimizeSafeZone = val,
                    "Optimize connectors safe zone: ",
                    "The minimum distance from the viewport where connectors will start optimizing"),
                new ProxySettingViewModel<uint>(
                    () => Instance.OptimizeMinimumSelectedItems,
                    val => Instance.OptimizeMinimumSelectedItems = val,
                    "Optimize connectors minimum selection: ",
                    "The minimum selected items needed to start optimizing when outside the safe zone."),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableRenderingOptimizations,
                    val => Instance.EnableRenderingOptimizations = val,
                    "Enable nodes rendering optimization: ",
                    "Enables rendering optimizations for nodes based on zoom out percent and nodes count. (zoom in/out to apply optimization)"),
                new ProxySettingViewModel<double>(
                    () => Instance.OptimizeRenderingZoomOutPercent,
                    val => Instance.OptimizeRenderingZoomOutPercent = val,
                    "Rendering optimization zoom out percent: ",
                    "The zoom out percent that triggers the optimization. (percent of x = 1 - MinViewportZoom)"),
                new ProxySettingViewModel<uint>(
                    () => Instance.OptimizeRenderingMinimumNodes,
                    val => Instance.OptimizeRenderingMinimumNodes = val,
                    "Rendering optimization minimum nodes: ",
                    "The minimum nodes needed to start optimizing when zoom out percent is met."),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableDraggingOptimizations,
                    val => Instance.EnableDraggingOptimizations = val,
                    "Enable nodes dragging optimizations: ",
                    "Simulates dragging visually but only commits the changes at the end."),
                new ProxySettingViewModel<double>(
                    () => Instance.FitToScreenExtentMargin,
                    val => Instance.FitToScreenExtentMargin = val,
                    "Fit to screen extent margin: ",
                    "Adds some margin to the nodes extent when fit to screen"),
                new ProxySettingViewModel<bool>(
                    () => Instance.EnableStickyConnectors,
                    val => Instance.EnableStickyConnectors = val,
                    "Enable sticky connectors: ",
                    "The connection can be completed in two steps (e.g. click to create pending connection, click to connect)"),
            };

            EnableCuttingLinePreview = true;
        }

        private void OnSearchTextChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PlaygroundSettings.SearchText))
            {
                OnPropertyChanged(nameof(Settings));
                OnPropertyChanged(nameof(AdvancedSettings));
            }
        }

        public static EditorSettings Instance { get; } = new EditorSettings();

        #region Default settings

        private bool _enablePendingConnectionSnapping = true;
        public bool EnablePendingConnectionSnapping
        {
            get => _enablePendingConnectionSnapping;
            set => SetProperty(ref _enablePendingConnectionSnapping, value);
        }

        private bool _enablePendingConnectionPreview = true;
        public bool EnablePendingConnectionPreview
        {
            get => _enablePendingConnectionPreview;
            set => SetProperty(ref _enablePendingConnectionPreview, value);
        }

        private bool _allowConnectingToConnectorsOnly;
        public bool AllowConnectingToConnectorsOnly
        {
            get => _allowConnectingToConnectorsOnly;
            set => SetProperty(ref _allowConnectingToConnectorsOnly, value);
        }

        private bool _realtimeSelection = true;
        public bool EnableRealtimeSelection
        {
            get => _realtimeSelection;
            set => SetProperty(ref _realtimeSelection, value);
        }

        private bool _disableAutoPanning = false;
        public bool DisableAutoPanning
        {
            get => _disableAutoPanning;
            set => SetProperty(ref _disableAutoPanning, value);
        }

        private double _autoPanningSpeed = 15d;
        public double AutoPanningSpeed
        {
            get => _autoPanningSpeed;
            set => SetProperty(ref _autoPanningSpeed, value);
        }

        private double _autoPanningEdgeDistance = 15d;
        public double AutoPanningEdgeDistance
        {
            get => _autoPanningEdgeDistance;
            set => SetProperty(ref _autoPanningEdgeDistance, value);
        }

        private bool _disablePanning = false;
        public bool DisablePanning
        {
            get => _disablePanning;
            set => SetProperty(ref _disablePanning, value);
        }

        private bool _disableZooming = false;
        public bool DisableZooming
        {
            get => _disableZooming;
            set => SetProperty(ref _disableZooming, value);
        }

        private uint _gridSpacing = 15u;
        public uint GridSpacing
        {
            get => _gridSpacing;
            set => SetProperty(ref _gridSpacing, value);
        }

        private double _minZoom = 0.1;
        public double MinZoom
        {
            get => _minZoom;
            set => SetProperty(ref _minZoom, value);
        }

        private double _maxZoom = 2;
        public double MaxZoom
        {
            get => _maxZoom;
            set => SetProperty(ref _maxZoom, value);
        }

        private double _zoom = 1;
        public double Zoom
        {
            get => _zoom;
            set => SetProperty(ref _zoom, value);
        }

        private PointEditor _location = new PointEditor();
        public PointEditor Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private bool _selectableConnections = true;
        public bool SelectableConnections
        {
            get => _selectableConnections;
            set => SetProperty(ref _selectableConnections, value);
        }

        private bool _canSelectMultipleConnections = true;
        public bool CanSelectMultipleConnections
        {
            get => _canSelectMultipleConnections;
            set => SetProperty(ref _canSelectMultipleConnections, value);
        }

        private bool _draggableNodes = true;
        public bool DraggableNodes
        {
            get => _draggableNodes;
            set => SetProperty(ref _draggableNodes, value);
        }

        private bool _selectableNodes = true;
        public bool SelectableNodes
        {
            get => _selectableNodes;
            set => SetProperty(ref _selectableNodes, value);
        }

        private bool _canSelectMultipleNodes = true;
        public bool CanSelectMultipleNodes
        {
            get => _canSelectMultipleNodes;
            set => SetProperty(ref _canSelectMultipleNodes, value);
        }

        private ConnectionStyle _connectionStyle;
        public ConnectionStyle ConnectionStyle
        {
            get => _connectionStyle;
            set => SetProperty(ref _connectionStyle, value);
        }

        private string? _connectionText;
        public string? ConnectionText
        {
            get => _connectionText;
            set => SetProperty(ref _connectionText, value);
        }

        private double _circuitConnectionAngle = 45;
        public double CircuitConnectionAngle
        {
            get => _circuitConnectionAngle;
            set => SetProperty(ref _circuitConnectionAngle, value);
        }

        private double _connectionSpacing = 20;
        public double ConnectionSpacing
        {
            get => _connectionSpacing;
            set => SetProperty(ref _connectionSpacing, value);
        }

        private ConnectionOffsetMode _srcConnectionOffsetMode = ConnectionOffsetMode.Static;
        public ConnectionOffsetMode ConnectionSourceOffsetMode
        {
            get => _srcConnectionOffsetMode;
            set => SetProperty(ref _srcConnectionOffsetMode, value);
        }

        private ConnectionOffsetMode _targetConnectionOffsetMode = ConnectionOffsetMode.Static;
        public ConnectionOffsetMode ConnectionTargetOffsetMode
        {
            get => _targetConnectionOffsetMode;
            set => SetProperty(ref _targetConnectionOffsetMode, value);
        }

        private ArrowHeadEnds _arrowHeadEnds = ArrowHeadEnds.End;
        public ArrowHeadEnds ArrowHeadEnds
        {
            get => _arrowHeadEnds;
            set => SetProperty(ref _arrowHeadEnds, value);
        }

        private ArrowHeadShape _arrowHeadShape = ArrowHeadShape.Arrowhead;
        public ArrowHeadShape ArrowHeadShape
        {
            get => _arrowHeadShape;
            set => SetProperty(ref _arrowHeadShape, value);
        }

        private PointEditor _connectionSourceOffset = new Size(14, 0);
        public PointEditor ConnectionSourceOffset
        {
            get => _connectionSourceOffset;
            set => SetProperty(ref _connectionSourceOffset, value);
        }

        private PointEditor _connectionTargetOffset = new Size(14, 0);
        public PointEditor ConnectionTargetOffset
        {
            get => _connectionTargetOffset;
            set => SetProperty(ref _connectionTargetOffset, value);
        }

        private uint _directionalArrowsCount = 3;
        public uint DirectionalArrowsCount
        {
            get => _directionalArrowsCount;
            set => SetProperty(ref _directionalArrowsCount, value);
        }

        private double _directionalArrowsOffset;
        public double DirectionalArrowsOffset
        {
            get => _directionalArrowsOffset;
            set => SetProperty(ref _directionalArrowsOffset, value);
        }

        private bool _isAnimatingConnections;
        public bool IsAnimatingConnections
        {
            get => _isAnimatingConnections;
            set => SetProperty(ref _isAnimatingConnections, value);
        }

        private double _directionalArrowsAnimationDuration = 2.0;
        public double DirectionalArrowsAnimationDuration
        {
            get => _directionalArrowsAnimationDuration;
            set => SetProperty(ref _directionalArrowsAnimationDuration, value);
        }

        private PointEditor _connectionArrowSize = new Size(8, 8);
        public PointEditor ConnectionArrowSize
        {
            get => _connectionArrowSize;
            set => SetProperty(ref _connectionArrowSize, value);
        }

        private bool _displayConnectionsOnTop;
        public bool DisplayConnectionsOnTop
        {
            get => _displayConnectionsOnTop;
            set => SetProperty(ref _displayConnectionsOnTop, value);
        }

        private double _bringIntoViewSpeed = 1000;
        public double BringIntoViewSpeed
        {
            get => _bringIntoViewSpeed;
            set => SetProperty(ref _bringIntoViewSpeed, value);
        }

        private double _bringIntoViewMaxDuration = 1;
        public double BringIntoViewMaxDuration
        {
            get => _bringIntoViewMaxDuration;
            set => SetProperty(ref _bringIntoViewMaxDuration, value);
        }

        private GroupingMovementMode _groupingNodeMovement;
        public GroupingMovementMode GroupingNodeMovement
        {
            get => _groupingNodeMovement;
            set => SetProperty(ref _groupingNodeMovement, value);
        }

        #endregion

        #region Advanced settings

        public double HandleRightClickAfterPanningThreshold
        {
            get => NodifyEditor.HandleRightClickAfterPanningThreshold;
            set => NodifyEditor.HandleRightClickAfterPanningThreshold = value;
        }

        public double AutoPanningTickRate
        {
            get => NodifyEditor.AutoPanningTickRate;
            set => NodifyEditor.AutoPanningTickRate = value;
        }

        public bool AllowCuttingCancellation
        {
            get => CuttingLine.AllowCuttingCancellation;
            set => CuttingLine.AllowCuttingCancellation = value;
        }

        public bool AllowDraggingCancellation
        {
            get => ItemContainer.AllowDraggingCancellation;
            set => ItemContainer.AllowDraggingCancellation = value;
        }

        public bool AllowPendingConnectionCancellation
        {
            get => Connector.AllowPendingConnectionCancellation;
            set => Connector.AllowPendingConnectionCancellation = value;
        }

        public bool EnableSnappingCorrection
        {
            get => NodifyEditor.EnableSnappingCorrection;
            set => NodifyEditor.EnableSnappingCorrection = value;
        }

        public bool EnableCuttingLinePreview
        {
            get => NodifyEditor.EnableCuttingLinePreview;
            set => NodifyEditor.EnableCuttingLinePreview = value;
        }

        public bool EnableConnectorOptimizations
        {
            get => Connector.EnableOptimizations;
            set => Connector.EnableOptimizations = value;
        }

        public double OptimizeSafeZone
        {
            get => Connector.OptimizeSafeZone;
            set => Connector.OptimizeSafeZone = value;
        }

        public uint OptimizeMinimumSelectedItems
        {
            get => Connector.OptimizeMinimumSelectedItems;
            set => Connector.OptimizeMinimumSelectedItems = value;
        }

        public bool EnableRenderingOptimizations
        {
            get => NodifyEditor.EnableRenderingContainersOptimizations;
            set => NodifyEditor.EnableRenderingContainersOptimizations = value;
        }

        public uint OptimizeRenderingMinimumNodes
        {
            get => NodifyEditor.OptimizeRenderingMinimumContainers;
            set => NodifyEditor.OptimizeRenderingMinimumContainers = value;
        }

        public double OptimizeRenderingZoomOutPercent
        {
            get => NodifyEditor.OptimizeRenderingZoomOutPercent;
            set => NodifyEditor.OptimizeRenderingZoomOutPercent = value;
        }

        public double FitToScreenExtentMargin
        {
            get => NodifyEditor.FitToScreenExtentMargin;
            set => NodifyEditor.FitToScreenExtentMargin = value;
        }

        public bool EnableDraggingOptimizations
        {
            get => NodifyEditor.EnableDraggingContainersOptimizations;
            set => NodifyEditor.EnableDraggingContainersOptimizations = value;
        }

        public bool EnableStickyConnectors
        {
            get => Connector.EnableStickyConnections;
            set => Connector.EnableStickyConnections = value;
        }

        #endregion
    }
}

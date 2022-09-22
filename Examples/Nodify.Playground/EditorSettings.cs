namespace Nodify.Playground
{
    public enum ConnectionStyle
    {
        Default,
        Line,
        Circuit
    }

    public class EditorSettings : ObservableObject
    {
        private EditorSettings() { }

        public static EditorSettings Instance { get; } = new EditorSettings();

        #region Default settings

        private ConnectionStyle _connectionStyle;
        public ConnectionStyle ConnectionStyle
        {
            get => _connectionStyle;
            set => SetProperty(ref _connectionStyle, value);
        }

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

        private double _autoPanningSpeed = 10d;
        public double AutoPanningSpeed
        {
            get => _autoPanningSpeed;
            set => SetProperty(ref _autoPanningSpeed, value);
        }

        public double _autoPanningEdgeDistance = 15d;
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

        private ConnectionOffsetMode _connectionOffsetMode = ConnectionOffsetMode.None;
        public ConnectionOffsetMode ConnectionOffsetMode
        {
            get => _connectionOffsetMode;
            set => SetProperty(ref _connectionOffsetMode, value);
        }

        private PointEditor _connectionSourceOffset = new PointEditor { X = 15, Y = 0 };
        public PointEditor ConnectionSourceOffset
        {
            get => _connectionSourceOffset;
            set => SetProperty(ref _connectionSourceOffset, value);
        }

        private PointEditor _connectionTargetOffset = new PointEditor { X = 15, Y = 0 };
        public PointEditor ConnectionTargetOffset
        {
            get => _connectionTargetOffset;
            set => SetProperty(ref _connectionTargetOffset, value);
        }

        private PointEditor _connectionArrowSize = new PointEditor { X = 7, Y = 6 };
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

        private double _bringIntoViewAnimationDuration = 0.5;
        public double BringIntoViewAnimationDuration
        {
            get => _bringIntoViewAnimationDuration;
            set => SetProperty(ref _bringIntoViewAnimationDuration, value);
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

        #endregion
    }
}

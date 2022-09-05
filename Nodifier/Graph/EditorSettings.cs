using Nodify;
using Stylet;

namespace Nodifier
{
    public interface IEditorSettings
    {
        double MinViewportZoom { get; set; }
        double MaxViewportZoom { get; set; }

        double GridSnapSize { get; set; }

        bool DisableZooming { get; set; }
        bool DisablePanning { get; set; }
        bool DisableAutoPanning { get; set; }
        bool EnableRealtimeSelection { get; set; }
        bool ShowGridLines { get; set; }
    }

    public class EditorSettings : PropertyChangedBase, IEditorSettings
    {
        private double _minViewportZoom = 0.1d;
        public double MinViewportZoom
        {
            get => _minViewportZoom;
            set => SetAndNotify(ref _minViewportZoom, value);
        }

        private double _maxViewportZoom = 2d;
        public double MaxViewportZoom
        {
            get => _maxViewportZoom;
            set => SetAndNotify(ref _maxViewportZoom, value);
        }

        private double _gridSnapSize;
        public double GridSnapSize
        {
            get => _gridSnapSize;
            set => SetAndNotify(ref _gridSnapSize, value);
        }

        private bool _disableZooming;
        public bool DisableZooming
        {
            get => _disableZooming;
            set => SetAndNotify(ref _disableZooming, value);
        }

        private bool _disablePanning;
        public bool DisablePanning
        {
            get => _disablePanning;
            set => SetAndNotify(ref _disablePanning, value);
        }

        private bool _disableAutoPanning;
        public bool DisableAutoPanning
        {
            get => _disableAutoPanning;
            set => SetAndNotify(ref _disableAutoPanning, value);
        }

        private bool _enableRealtimeSelection = true;
        public bool EnableRealtimeSelection
        {
            get => _enableRealtimeSelection;
            set => SetAndNotify(ref _enableRealtimeSelection, value);
        }

        private bool _showGridLines = true;
        public bool ShowGridLines
        {
            get => _showGridLines;
            set => SetAndNotify(ref _showGridLines, value);
        }

        #region Global Settings

        public static double HandleRightClickAfterPanningThreshold
        {
            get => NodifyEditor.HandleRightClickAfterPanningThreshold;
            set => NodifyEditor.HandleRightClickAfterPanningThreshold = value;
        }

        public static double AutoPanningTickRate
        {
            get => NodifyEditor.AutoPanningTickRate;
            set => NodifyEditor.AutoPanningTickRate = value;
        }

        public static bool AllowDraggingCancellation
        {
            get => ItemContainer.AllowDraggingCancellation;
            set => ItemContainer.AllowDraggingCancellation = value;
        }

        public static bool AllowPendingConnectionCancellation
        {
            get => Connector.AllowPendingConnectionCancellation;
            set => Connector.AllowPendingConnectionCancellation = value;
        }

        public static bool EnableSnappingCorrection
        {
            get => NodifyEditor.EnableSnappingCorrection;
            set => NodifyEditor.EnableSnappingCorrection = value;
        }

        public static bool EnableConnectorOptimizations
        {
            get => Connector.EnableOptimizations;
            set => Connector.EnableOptimizations = value;
        }

        public static double OptimizeSafeZone
        {
            get => Connector.OptimizeSafeZone;
            set => Connector.OptimizeSafeZone = value;
        }

        public static uint OptimizeMinimumSelectedItems
        {
            get => Connector.OptimizeMinimumSelectedItems;
            set => Connector.OptimizeMinimumSelectedItems = value;
        }

        public static bool EnableRenderingOptimizations
        {
            get => NodifyEditor.EnableRenderingContainersOptimizations;
            set => NodifyEditor.EnableRenderingContainersOptimizations = value;
        }

        public static uint OptimizeRenderingMinimumNodes
        {
            get => NodifyEditor.OptimizeRenderingMinimumContainers;
            set => NodifyEditor.OptimizeRenderingMinimumContainers = value;
        }

        public static double OptimizeRenderingZoomOutPercent
        {
            get => NodifyEditor.OptimizeRenderingZoomOutPercent;
            set => NodifyEditor.OptimizeRenderingZoomOutPercent = value;
        }

        #endregion
    }
}

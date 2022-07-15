using Nodify;

namespace Nodifier
{
    public static class EditorSettings
    {
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
    }
}

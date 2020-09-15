namespace Nodify.Playground
{
    public class EditorSettings : ObservableObject
    {
        private EditorSettings() { }

        public static EditorSettings Instance { get; } = new EditorSettings();

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

        private int _gridSpacing = 15;
        public int GridSpacing
        {
            get => _gridSpacing;
            set => SetProperty(ref _gridSpacing, value);
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
    }
}

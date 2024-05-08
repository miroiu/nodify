using System.Windows.Controls;

namespace Nodify.Playground
{
    public class PendingConnectionViewModel : ObservableObject
    {
        private NodifyEditorViewModel _graph = default!;
        public NodifyEditorViewModel Graph
        {
            get => _graph;
            internal set => SetProperty(ref _graph, value);
        }

        private ConnectorViewModel? _source;
        public ConnectorViewModel? Source
        {
            get => _source;
            set
            {
                if(SetProperty(ref _source, value))
                {
                    SetTargetOrientation();
                }
            }
        }

        private object? _previewTarget;
        public object? PreviewTarget
        {
            get => _previewTarget;
            set
            {
                if (SetProperty(ref _previewTarget, value))
                {
                    OnPreviewTargetChanged();
                }
            }
        }

        private string? _previewText;
        public string? PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

        private Orientation _targetOrientation;
        public Orientation TargetOrientation
        {
            get => _targetOrientation;
            set => SetProperty(ref _targetOrientation, value);
        }

        protected virtual void OnPreviewTargetChanged()
        {
            bool canConnect = PreviewTarget != null && Graph.Schema.CanAddConnection(Source!, PreviewTarget);
            PreviewText = PreviewTarget switch
            {
                ConnectorViewModel con when con == Source => $"Can't connect to self",
                ConnectorViewModel con => $"{(canConnect ? "Connect" : "Can't connect")} to {con.Title ?? "pin"}",
                FlowNodeViewModel flow => $"{(canConnect ? "Connect" : "Can't connect")} to {flow.Title ?? "node"}",
                _ => null
            };

            SetTargetOrientation();
        }

        private void SetTargetOrientation()
        {
            TargetOrientation = PreviewTarget switch
            {
                ConnectorViewModel con when con.Node is FlowNodeViewModel flow => flow.Orientation,
                FlowNodeViewModel flow => flow.Orientation,
                NodifyEditorViewModel editor when Source?.Node is FlowNodeViewModel flow => flow.Orientation,
                _ => Orientation.Horizontal,
            };
        }
    }
}

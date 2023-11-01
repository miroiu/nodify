using System;
using System.Windows;

namespace Nodifier
{
    public interface IPendingConnection
    {
        bool IsVisible { get; set; }
        Point TargetLocation { get; }

        /// <summary>The preview target could be a connector, a node or a graph.</summary>
        public object? PreviewTarget { get; }

        /// <summary>The text to show when the connection is over the <see cref="PreviewTarget"/>.</summary>
        public string? PreviewText { get; }

        void Start(IConnector source);
        void Complete(object target);
    }

    public class PendingConnection : PropertyChangedBase, IPendingConnection
    {
        public IGraphWidget Graph { get; }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetAndNotify(ref _isVisible, value);
        }

        private Point _targetLocation;
        public Point TargetLocation
        {
            get => _targetLocation;
            set => SetAndNotify(ref _targetLocation, value);
        }

        private object? _previewTarget;
        public object? PreviewTarget
        {
            get => _previewTarget;
            set
            {
                if (SetAndNotify(ref _previewTarget, value))
                {
                    OnPropertyChanged(nameof(PreviewText));
                }
            }
        }

        public virtual string? PreviewText => null;

        private IConnector? _source;

        public PendingConnection(IGraphWidget graph)
        {
            Graph = graph;
        }

        public virtual void Start(IConnector source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public virtual void Complete(object target)
        {
            if (_source == null)
            {
                throw new GraphException($"Must call {nameof(Start)} before calling {nameof(Complete)}.");
            }

            if (target is IConnector connector)
            {
                _source.TryConnectTo(connector);
            }
            else if (target is IGraphElement element)
            {
                _source.TryConnectTo(element);
            }
        }
    }
}

namespace Nodifier
{
    public interface IBlueprintPendingConnection
    {
        IConnector? Source { get; }
    }

    public class BlueprintPendingConnection : PendingConnection, IBlueprintPendingConnection
    {
        public BlueprintPendingConnection(IGraph graph) : base(graph)
        {
        }

        private IConnector? _source;
        public IConnector? Source
        {
            get => _source;
            set => SetAndNotify(ref _source, value);
        }

        public override void Start(IConnector source)
        {
            base.Start(source);

            Source = source;
        }
    }
}

namespace NodifyBlueprint
{
    public interface IBlueprintConnection : IConnection
    {
    }

    public class BlueprintConnection : NodeConnection, IBlueprintConnection
    {
        public BlueprintConnection(IConnector source, IConnector target) : base(source is IOutputConnector ? source : target, source is IOutputConnector ? target : source)
        {
        }
    }
}

namespace NodifyBlueprint
{
    public interface IBlueprintConnection
    {
    }

    public class BlueprintConnection : NodeConnection, IBlueprintConnection
    {
        public BlueprintConnection(IConnector source, IConnector target) : base(source is IOutputConnector ? source : target, source is IOutputConnector ? target : source)
        {
        }
    }
}

namespace Nodifier
{
    public interface IBlueprintConnection : IConnection
    {
    }

    public class BlueprintConnection : NodeConnection, IBlueprintConnection
    {
        public BlueprintConnection(IConnector source, IConnector target) : base(source, target)
        {
        }
    }
}

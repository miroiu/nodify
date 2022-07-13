namespace NodifyBlueprint
{
    public interface IConnection
    {
        IConnector Source { get; }
        IConnector Target { get; }
    }
}
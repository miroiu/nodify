namespace NodifyBlueprint
{
    public interface IGraphSchema
    {
        bool CanConnect(IConnector source, IConnector target);
    }

    public class GraphSchema : IGraphSchema
    {
        public static readonly IGraphSchema Default = new GraphSchema();

        public virtual bool CanConnect(IConnector source, IConnector target)
        {
            IConnector? input = source is IInputConnector ? source : target is IInputConnector ? target : null;
            IConnector? output = source is IOutputConnector ? source : target is IOutputConnector ? target : null;
            return source != target 
                && source.Node != target.Node
                && source.Node.Graph == target.Node.Graph 
                && input != null && output != null;
        }
    }
}

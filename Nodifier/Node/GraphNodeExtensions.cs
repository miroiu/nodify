namespace Nodifier
{
    public static class GraphNodeExtensions
    {
        public static bool TryConnect(this IGraphNode source, IGraphNode target)
        {
            foreach (IConnector output in source.Output)
            {
                if (output.TryConnectTo(target))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
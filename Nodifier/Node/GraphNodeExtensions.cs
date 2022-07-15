namespace Nodifier
{
    public static class GraphNodeExtensions
    {
        public static bool TryConnect(this IGraphNode source, IGraphNode target)
        {
            bool result = false;
            foreach (IConnector output in source.Output)
            {
                result |= output.TryConnectTo(target);
            }
            return result;
        }
    }
}
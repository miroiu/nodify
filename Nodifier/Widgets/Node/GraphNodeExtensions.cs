namespace Nodifier
{
    public static class GraphNodeExtensions
    {
        public static bool TryConnect(this INodeWidget source, INodeWidget target)
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
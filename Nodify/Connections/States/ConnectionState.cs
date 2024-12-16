namespace Nodify.Interactivity
{
    public static partial class ConnectionState
    {
        internal static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<BaseConnection>.RegisterHandlerFactory(elem => new Disconnect(elem));
            InputProcessor.Shared<BaseConnection>.RegisterHandlerFactory(elem => new Split(elem));
        }
    }
}

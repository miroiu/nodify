namespace Nodify.Interactivity
{
    public static partial class ConnectorState
    {
        public static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<Connector>.RegisterHandlerFactory(elem => new Disconnect(elem));
            InputProcessor.Shared<Connector>.RegisterHandlerFactory(elem => new Connecting(elem));
            InputProcessor.Shared<Connector>.RegisterHandlerFactory(elem => new Default(elem));
        }
    }
}

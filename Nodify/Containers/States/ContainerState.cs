namespace Nodify.Interactivity
{
    public static partial class ContainerState
    {
        public static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<ItemContainer>.RegisterHandlerFactory(elem => new Default(elem));
        }
    }
}

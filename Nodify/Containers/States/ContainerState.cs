namespace Nodify.Interactivity
{
    public static partial class ContainerState
    {
        internal static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<ItemContainer>.RegisterHandlerFactory(elem => new Default(elem));
        }
    }
}

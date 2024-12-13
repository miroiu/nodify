namespace Nodify.Interactivity
{
    public static partial class MinimapState
    {
        internal static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<Minimap>.RegisterHandlerFactory(elem => new Panning(elem));
            InputProcessor.Shared<Minimap>.RegisterHandlerFactory(elem => new Zooming(elem));
        }
    }
}

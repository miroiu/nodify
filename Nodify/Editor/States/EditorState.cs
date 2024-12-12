namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        public static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Panning(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new PanningWithMouseWheel(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Selecting(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Zooming(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new PushingItems(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Cutting(elem));
        }
    }
}

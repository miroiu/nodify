namespace Nodify.Interactivity
{
    public static partial class MinimapState
    {
        /// <summary>
        /// Determines whether toggled panning mode is enabled, allowing the user to start and end the interaction in two steps with the same input gesture.
        /// </summary>
        public static bool EnableToggledPanningMode { get; set; }

        internal static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<Minimap>.RegisterHandlerFactory(elem => new Panning(elem));
            InputProcessor.Shared<Minimap>.RegisterHandlerFactory(elem => new Zooming(elem));
            InputProcessor.Shared<Minimap>.RegisterHandlerFactory(elem => new KeyboardNavigation(elem));
        }
    }
}

namespace Nodify.Interactivity
{
    public static partial class ContainerState
    {
        /// <summary>
        /// Determines whether toggled dragging mode is enabled, allowing the user to start and end the interaction in two steps with the same input gesture.
        /// </summary>
        public static bool EnableToggledDraggingMode { get; set; }

        internal static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<ItemContainer>.RegisterHandlerFactory(elem => new Default(elem));
        }
    }
}

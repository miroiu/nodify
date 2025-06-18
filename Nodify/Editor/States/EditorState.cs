namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        /// <summary>
        /// Gets or sets a value indicating whether panning is allowed while selecting items in the editor.
        /// </summary>
        public static bool AllowPanningWhileSelecting { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether panning is allowed while cutting connections in the editor.
        /// </summary>
        public static bool AllowPanningWhileCutting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether panning is allowed while pushing items in the editor.
        /// </summary>
        public static bool AllowPanningWhilePushingItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether zooming is allowed while selecting items in the editor.
        /// </summary>
        public static bool AllowZoomingWhileSelecting { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether zooming is allowed while cutting connections in the editor.
        /// </summary>
        public static bool AllowZoomingWhileCutting { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether zooming is allowed while pushing items in the editor.
        /// </summary>
        public static bool AllowZoomingWhilePushingItems { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether zooming is allowed while panning the editor viewport.
        /// </summary>
        public static bool AllowZoomingWhilePanning { get; set; } = true;

        /// <summary>
        /// Determines whether toggled selecting mode is enabled, allowing the user to start and end the interaction in two steps with the same input gesture.
        /// </summary>
        public static bool EnableToggledSelectingMode { get; set; }

        /// <summary>
        /// Determines whether toggled panning mode is enabled, allowing the user to start and end the interaction in two steps with the same input gesture.
        /// </summary>
        public static bool EnableToggledPanningMode { get; set; }

        /// <summary>
        /// Determines whether toggled cutting mode is enabled, allowing the user to start and end the interaction in two steps with the same input gesture.
        /// </summary>
        public static bool EnableToggledCuttingMode { get; set; }

        /// <summary>
        /// Determines whether toggled pushing items mode is enabled, allowing the user to start and end the interaction in two steps with the same input gesture.
        /// </summary>
        public static bool EnableToggledPushingItemsMode { get; set; }

        internal static void RegisterDefaultHandlers()
        {
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Panning(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new PanningWithMouseWheel(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Selecting(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Zooming(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new PushingItems(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new Cutting(elem));
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(elem => new KeyboardNavigation(elem));
        }
    }
}

using System.Windows.Input;

namespace Nodify
{
    /// <summary>Gestures used by the <see cref="NodifyEditor"/>.</summary>
    public static class EditorGestures
    {
        /// <summary>The selection strategies.</summary>
        public static class Selection
        {
            /// <summary>The default <see cref="MouseAction"/> to use for selection.</summary>
            public static MouseAction DefaultMouseAction { get; set; } = MouseAction.LeftClick;

            /// <summary>Gesture to replace previous selection with the selected items.</summary>
            /// <remarks>Defaults to <see cref="DefaultMouseAction"/>.</remarks>
            public static InputGesture Replace { get; set; } = new MouseGesture(DefaultMouseAction);

            /// <summary>Gesture to remove the selected items from the previous selection.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="DefaultMouseAction"/>.</remarks>
            public static InputGesture Remove { get; set; } = new MouseGesture(DefaultMouseAction, ModifierKeys.Alt);

            /// <summary>Gesture to add the new selected items to the previous selection.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Shift"/>+<see cref="DefaultMouseAction"/>.</remarks>
            public static InputGesture Append { get; set; } = new MouseGesture(DefaultMouseAction, ModifierKeys.Shift);

            /// <summary>Gesture to invert the selected items.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="DefaultMouseAction"/>.</remarks>
            public static InputGesture Invert { get; set; } = new MouseGesture(DefaultMouseAction, ModifierKeys.Control);

            /// <summary>Cancel the current selection operation reverting to the previous selection.</summary>
            /// <remarks>Defaults to <see cref="Key.Escape"/>.</remarks>
            public static InputGesture Cancel { get; set; } = new KeyGesture(Key.Escape);
        }

        /// <summary>Gesture used to start selecting using a <see cref="Selection"/> strategy.</summary>
        public static InputGesture Select { get; } = new MultiGesture(MultiGesture.Match.Any, Selection.Replace, Selection.Remove, Selection.Append, Selection.Invert);

        /// <summary>Gesture used to start panning.</summary>
        /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="MouseAction.MiddleClick"/>.</remarks>
        public static InputGesture Pan { get; set; } = new MultiGesture(MultiGesture.Match.Any, new MouseGesture(MouseAction.RightClick), new MouseGesture(MouseAction.MiddleClick));

        /// <summary>The key modifier required to start zooming by mouse wheel.</summary>
        /// <remarks>Defaults to <see cref="ModifierKeys.None"/>.</remarks>
        public static ModifierKeys Zoom { get; set; } = ModifierKeys.None;

        /// <summary>Gesture used to zoom in.</summary>
        /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemPlus"/>.</remarks>
        public static InputGesture ZoomIn { get; set; } = new MultiGesture(MultiGesture.Match.Any, new KeyGesture(Key.OemPlus, ModifierKeys.Control), new KeyGesture(Key.Add, ModifierKeys.Control));

        /// <summary>Gesture used to zoom out.</summary>
        /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemMinus"/>.</remarks>
        public static InputGesture ZoomOut { get; set; } = new MultiGesture(MultiGesture.Match.Any, new KeyGesture(Key.OemMinus, ModifierKeys.Control), new KeyGesture(Key.Subtract, ModifierKeys.Control));

        /// <summary>Gesture used to move the editor's viewport location to (0, 0).</summary>
        /// <remarks>Defaults to <see cref="Key.Home"/>.</remarks>
        public static InputGesture ResetViewportLocation { get; set; } = new KeyGesture(Key.Home);

        /// <summary>Gesture used to fit as many containers as possible into the viewport.</summary>
        /// <remarks>Defaults to <see cref="ModifierKeys.Shift"/>+<see cref="Key.Home"/>.</remarks>
        public static InputGesture FitToScreen { get; set; } = new KeyGesture(Key.Home, ModifierKeys.Shift);

        /// <summary>Gestures used by the <see cref="BaseConnection"/>.</summary>
        public static class Connection
        {
            /// <summary>Gesture to call the <see cref="BaseConnection.SplitCommand"/> command.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftDoubleClick"/>.</remarks>
            public static InputGesture Split { get; set; } = new MouseGesture(MouseAction.LeftDoubleClick);

            /// <summary>Gesture to call the <see cref="BaseConnection.DisconnectCommand"/> command.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public static InputGesture Disconnect { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
        }

        /// <summary>Gestures used by the <see cref="Connector"/>.</summary>
        public static class Connector
        {
            /// <summary>Gesture to call the <see cref="Nodify.Connector.DisconnectCommand"/>.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public static InputGesture Disconnect { get; set; } = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);

            /// <summary>Gesture to start and complete a pending connection.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftClick"/>.</remarks>
            public static InputGesture Connect { get; set; } = new MouseGesture(MouseAction.LeftClick);

            /// <summary>Gesture to cancel the pending connection.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="Key.Escape"/>.</remarks>
            public static InputGesture CancelAction { get; set; } = new MultiGesture(MultiGesture.Match.Any, new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
        }

        /// <summary>Gestures used by the <see cref="ItemContainer"/>.</summary>
        public static class ItemContainer
        {
            /// <summary>Gesture to select the container using a <see cref="Selection"/> strategy.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or any of the <see cref="Selection"/> gestures.</remarks>
            public static InputGesture Select { get; set; } = new MultiGesture(MultiGesture.Match.Any, Selection.Replace, Selection.Remove, Selection.Append, Selection.Invert, new MouseGesture(MouseAction.RightClick));

            /// <summary>Gesture to start and complete a dragging operation.</summary>
            /// <remarks>Using a <see cref="Selection"/> strategy to drag from a new selection. 
            /// <br /> Defaults to any of the <see cref="Selection"/> gestures.
            /// </remarks>
            public static InputGesture Drag { get; set; } = new MultiGesture(MultiGesture.Match.Any, Selection.Replace, Selection.Remove, Selection.Append, Selection.Invert);

            /// <summary>Gesture to cancel the dragging operation.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="Key.Escape"/>.</remarks>
            public static InputGesture CancelAction { get; set; } = new MultiGesture(MultiGesture.Match.Any, new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
        }

        /// <summary>Gestures for the <see cref="GroupingNode"/>.</summary>
        public static class GroupingNode
        {
            /// <summary>The key modifier that will toggle between <see cref="GroupingMovementMode"/>s.</summary>
            /// <remarks>The modifier must be allowed by the <see cref="ItemContainer.Drag"/> gesture.
            /// <br /> Defaults to <see cref="ModifierKeys.Shift"/>.
            /// </remarks>
            public static ModifierKeys SwitchMovementMode { get; set; } = ModifierKeys.Shift;
        }
    }
}

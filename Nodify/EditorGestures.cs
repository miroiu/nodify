using System.Windows.Input;

namespace Nodify
{
    /// <summary>Gestures used by built-in controls inside the <see cref="NodifyEditor"/>.</summary>
    public class EditorGestures
    {
        public static readonly EditorGestures Mappings = new EditorGestures();

        /// <summary>Gestures for the selection.</summary>
        public class SelectionGestures
        {
            /// <summary>Disable selection gestures.</summary>
            public static readonly SelectionGestures None = new SelectionGestures(MouseAction.None);

            public SelectionGestures(MouseAction mouseAction)
            {
                Replace = new MouseGesture(mouseAction);
                Remove = new MouseGesture(mouseAction, ModifierKeys.Alt);
                Append = new MouseGesture(mouseAction, ModifierKeys.Shift);
                Invert = new MouseGesture(mouseAction, ModifierKeys.Control);
                Select = new AnyGesture(Replace, Remove, Append, Invert);
                Cancel = new KeyGesture(Key.Escape);
            }

            public SelectionGestures() : this(MouseAction.LeftClick)
            {
            }

            /// <summary>Gesture to replace previous selection with the selected items.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Replace { get; }

            /// <summary>Gesture to remove the selected items from the previous selection.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Remove { get; }

            /// <summary>Gesture to add the new selected items to the previous selection.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Shift"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Append { get; }

            /// <summary>Gesture to invert the selected items.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Invert { get; }

            /// <summary>Cancel the current selection operation reverting to the previous selection.</summary>
            /// <remarks>Defaults to <see cref="Key.Escape"/>.</remarks>
            public InputGestureRef Cancel { get; }

            /// <summary>Gesture used to start selecting using a <see cref="SelectionGestures"/> strategy.</summary>
            public InputGestureRef Select { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(SelectionGestures gestures)
            {
                Replace.Value = gestures.Replace.Value;
                Remove.Value = gestures.Remove.Value;
                Append.Value = gestures.Append.Value;
                Invert.Value = gestures.Invert.Value;
                Select.Value = gestures.Select.Value;
                Cancel.Value = gestures.Cancel.Value;
            }
        }

        /// <summary>Gestures for the item containers.</summary>
        public class ItemContainerGestures
        {
            public ItemContainerGestures()
            {
                Selection = new SelectionGestures();
                Selection.Select.Value = new AnyGesture(
                    Selection.Replace,
                    Selection.Remove,
                    Selection.Append,
                    Selection.Invert,
                    new MouseGesture(MouseAction.RightClick));

                Drag = new AnyGesture(Selection.Replace, Selection.Remove, Selection.Append, Selection.Invert);
                CancelAction = new AnyGesture(new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
            }

            /// <summary>Gesture to select the container using a <see cref="SelectionGestures"/> strategy.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or any of the <see cref="SelectionGestures"/> gestures.</remarks>
            public SelectionGestures Selection { get; }

            /// <summary>Gesture to start and complete a dragging operation.</summary>
            /// <remarks>Using a <see cref="Selection"/> strategy to drag from a new selection. 
            /// <br /> Defaults to any of the <see cref="Selection"/> gestures.
            /// </remarks>
            public InputGestureRef Drag { get; }

            /// <summary>Gesture to cancel the dragging operation.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="Key.Escape"/>.</remarks>
            public InputGestureRef CancelAction { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(ItemContainerGestures gestures)
            {
                Selection.Apply(gestures.Selection);
                Drag.Value = gestures.Drag.Value;
                CancelAction.Value = gestures.CancelAction.Value;
            }
        }

        /// <summary>Gestures for the editor.</summary>
        public class NodifyEditorGestures
        {
            public NodifyEditorGestures()
            {
                Selection = new SelectionGestures();
                Pan = new AnyGesture(new MouseGesture(MouseAction.RightClick), new MouseGesture(MouseAction.MiddleClick));
                ZoomModifierKey = ModifierKeys.None;
                ZoomIn = new MultiGesture(MultiGesture.Match.Any, new KeyGesture(Key.OemPlus, ModifierKeys.Control), new KeyGesture(Key.Add, ModifierKeys.Control));
                ZoomOut = new MultiGesture(MultiGesture.Match.Any, new KeyGesture(Key.OemMinus, ModifierKeys.Control), new KeyGesture(Key.Subtract, ModifierKeys.Control));
                ResetViewportLocation = new KeyGesture(Key.Home);
                FitToScreen = new KeyGesture(Key.Home, ModifierKeys.Shift);
            }

            /// <summary>Gesture used to start selecting using a <see cref="SelectionGestures"/> strategy.</summary>
            public SelectionGestures Selection { get; }

            /// <summary>Gesture used to start panning.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="MouseAction.MiddleClick"/>.</remarks>
            public InputGestureRef Pan { get; }

            /// <summary>The key modifier required to start zooming by mouse wheel.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.None"/>.</remarks>
            public ModifierKeys ZoomModifierKey { get; set; }

            /// <summary>Gesture used to zoom in.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemPlus"/>.</remarks>
            public InputGestureRef ZoomIn { get; }

            /// <summary>Gesture used to zoom out.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemMinus"/>.</remarks>
            public InputGestureRef ZoomOut { get; }

            /// <summary>Gesture used to move the editor's viewport location to (0, 0).</summary>
            /// <remarks>Defaults to <see cref="Key.Home"/>.</remarks>
            public InputGestureRef ResetViewportLocation { get; }

            /// <summary>Gesture used to fit as many containers as possible into the viewport.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Shift"/>+<see cref="Key.Home"/>.</remarks>
            public InputGestureRef FitToScreen { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(NodifyEditorGestures gestures)
            {
                Selection.Apply(gestures.Selection);
                Pan.Value = gestures.Pan.Value;
                ZoomModifierKey = gestures.ZoomModifierKey;
                ZoomIn.Value = gestures.ZoomIn.Value;
                ZoomOut.Value = gestures.ZoomOut.Value;
                ResetViewportLocation.Value = gestures.ResetViewportLocation.Value;
                FitToScreen.Value = gestures.FitToScreen.Value;
            }
        }

        /// <summary>Gestures used by the <see cref="Connector"/>.</summary>
        public class ConnectorGestures
        {
            public ConnectorGestures()
            {
                Disconnect = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
                Connect = new MouseGesture(MouseAction.LeftClick);
                CancelAction = new AnyGesture(new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
            }

            /// <summary>Gesture to call the <see cref="Connector.DisconnectCommand"/>.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Disconnect { get; }

            /// <summary>Gesture to start and complete a pending connection.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Connect { get; }

            /// <summary>Gesture to cancel the pending connection.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="Key.Escape"/>.</remarks>
            public InputGestureRef CancelAction { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(ConnectorGestures gestures)
            {
                Disconnect.Value = gestures.Disconnect.Value;
                Connect.Value = gestures.Connect.Value;
                CancelAction.Value = gestures.CancelAction.Value;
            }
        }

        /// <summary>Gestures used by the <see cref="BaseConnection"/>.</summary>
        public class ConnectionGestures
        {
            public ConnectionGestures()
            {
                Split = new MouseGesture(MouseAction.LeftDoubleClick);
                Disconnect = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
            }

            /// <summary>Gesture to call the <see cref="BaseConnection.SplitCommand"/> command.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftDoubleClick"/>.</remarks>
            public InputGestureRef Split { get; }

            /// <summary>Gesture to call the <see cref="BaseConnection.DisconnectCommand"/> command.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Disconnect { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(ConnectionGestures gestures)
            {
                Split.Value = gestures.Split.Value;
                Disconnect.Value = gestures.Disconnect.Value;
            }
        }

        /// <summary>Gestures for the <see cref="GroupingNode"/>.</summary>
        public class GroupingNodeGestures
        {
            /// <summary>The key modifier that will toggle between <see cref="GroupingMovementMode"/>s.</summary>
            /// <remarks>The modifier must be allowed by the <see cref="ItemContainer.Drag"/> gesture.
            /// <br /> Defaults to <see cref="ModifierKeys.Shift"/>.
            /// </remarks>
            public ModifierKeys SwitchMovementMode { get; set; } = ModifierKeys.Shift;

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(GroupingNodeGestures gestures)
            {
                SwitchMovementMode = gestures.SwitchMovementMode;
            }
        }

        /// <summary>Gestures for the editor.</summary>
        public NodifyEditorGestures Editor { get; } = new NodifyEditorGestures();

        /// <summary>Gestures for the item container.</summary>
        public ItemContainerGestures ItemContainer { get; } = new ItemContainerGestures();

        /// <summary>Gestures for the connector.</summary>
        public ConnectorGestures Connector { get; } = new ConnectorGestures();

        /// <summary>Gestures for the connection.</summary>
        public ConnectionGestures Connection { get; } = new ConnectionGestures();

        /// <summary>Gestures for the grouping node.</summary>
        public GroupingNodeGestures GroupingNode { get; } = new GroupingNodeGestures();

        /// <summary>Copies from the specified gestures.</summary>
        /// <param name="gestures">The gestures to copy.</param>
        public void Apply(EditorGestures gestures)
        {
            Editor.Apply(gestures.Editor);
            ItemContainer.Apply(gestures.ItemContainer);
            Connector.Apply(gestures.Connector);
            Connection.Apply(gestures.Connection);
            GroupingNode.Apply(gestures.GroupingNode);
        }
    }
}

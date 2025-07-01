using System.Windows.Input;

namespace Nodify.Interactivity
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

            /// <summary>
            /// Initializes a new instance of the <see cref="SelectionGestures"/> class with specified mouse action 
            /// and a flag indicating whether modifier keys should be ignored when releasing the mouse button.
            /// </summary>
            /// <param name="mouseAction">The mouse action to trigger the gestures.</param>
            /// <param name="ignoreModifierKeysOnRelease">
            /// A value indicating whether modifier keys (Alt, Shift, Control) should be ignored when the mouse button is released.
            /// </param>
            public SelectionGestures(MouseAction mouseAction, bool ignoreModifierKeysOnRelease)
            {
                Replace = new MouseGesture(mouseAction);
                Remove = new MouseGesture(mouseAction, ModifierKeys.Alt, ignoreModifierKeysOnRelease);
                Append = new MouseGesture(mouseAction, ModifierKeys.Shift, ignoreModifierKeysOnRelease);
                Invert = new MouseGesture(mouseAction, ModifierKeys.Control, ignoreModifierKeysOnRelease);
                Select = new AnyGesture(Replace, Remove, Append, Invert);
                Cancel = new KeyGesture(Key.Escape);
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SelectionGestures"/> class with a specified mouse action.
            /// Modifier keys will be ignored when releasing the mouse button.
            /// </summary>
            /// <param name="mouseAction">The mouse action to trigger the gestures.</param>
            public SelectionGestures(MouseAction mouseAction)
                : this(mouseAction, true)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SelectionGestures"/> class with a flag indicating 
            /// whether modifier keys should be ignored when releasing the mouse button. 
            /// The default mouse action is <see cref="MouseAction.LeftClick"/>.
            /// </summary>
            /// <param name="ignoreModifierKeysOnRelease">
            /// A value indicating whether modifier keys (Alt, Shift, Control) should be ignored when the mouse button is released.
            /// </param>
            public SelectionGestures(bool ignoreModifierKeysOnRelease)
                : this(MouseAction.LeftClick, ignoreModifierKeysOnRelease)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SelectionGestures"/> class with default values:
            /// the mouse action is <see cref="MouseAction.LeftClick"/>, and modifier keys are ignored when releasing the mouse button.
            /// </summary>
            public SelectionGestures() : this(true)
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

            /// <summary>
            /// Unbinds the all the gestures used for selection.
            /// </summary>
            public void Unbind()
                => Apply(None);
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
                    Selection.Invert);

                Drag = new AnyGesture(Selection.Replace, Selection.Remove, Selection.Append, Selection.Invert);
                CancelAction = new AnyGesture(new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
            }

            /// <summary>Gesture to select the container using a <see cref="SelectionGestures"/> strategy.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftClick"/> or any of the <see cref="SelectionGestures"/> gestures.</remarks>
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

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                Selection.Unbind();
                Drag.Unbind();
                CancelAction.Unbind();
            }
        }

        /// <summary>
        /// Keyboard gestures used for navigating the editor and moving selected items.
        /// </summary>
        public class DirectionalNavigationGestures
        {
            public DirectionalNavigationGestures(ModifierKeys modifierKeys = ModifierKeys.None)
            {
                Up = new KeyGesture(Key.Up, modifierKeys);
                Left = new KeyGesture(Key.Left, modifierKeys);
                Down = new KeyGesture(Key.Down, modifierKeys);
                Right = new KeyGesture(Key.Right, modifierKeys);
            }

            public DirectionalNavigationGestures(Key triggerKey, ModifierKeys modifierKeys = ModifierKeys.None, bool repeated = false)
            {
                Up = new KeyComboGesture(triggerKey, Key.Up, modifierKeys) { AllowRepeatingComboKey = repeated };
                Left = new KeyComboGesture(triggerKey, Key.Left, modifierKeys) { AllowRepeatingComboKey = repeated };
                Down = new KeyComboGesture(triggerKey, Key.Down, modifierKeys) { AllowRepeatingComboKey = repeated };
                Right = new KeyComboGesture(triggerKey, Key.Right, modifierKeys) { AllowRepeatingComboKey = repeated };
            }

            /// <summary>
            /// Gesture used for navigating or moving upward.
            /// </summary>
            public InputGestureRef Up { get; }

            /// <summary>
            /// Gesture used for navigating or moving left.
            /// </summary>
            public InputGestureRef Left { get; }

            /// <summary>
            /// Gesture used for navigating or moving downward.
            /// </summary>
            public InputGestureRef Down { get; }

            /// <summary>
            /// Gesture used for navigating or moving right.
            /// </summary>
            public InputGestureRef Right { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(DirectionalNavigationGestures gestures)
            {
                Up.Value = gestures.Up.Value;
                Left.Value = gestures.Left.Value;
                Down.Value = gestures.Down.Value;
                Right.Value = gestures.Right.Value;
            }

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                Up.Unbind();
                Left.Unbind();
                Down.Unbind();
                Right.Unbind();
            }
        }

        /// <summary>Gestures for the editor.</summary>
        public class NodifyEditorGestures
        {
            /// <summary>
            /// Keyboard gestures used for navigation, selection, and manipulation in the editor.
            /// </summary>
            public class KeyboardGestures
            {
                public KeyboardGestures()
                {
                    Pan = new DirectionalNavigationGestures(Key.Space, repeated: true);
                    DragSelection = new DirectionalNavigationGestures(ModifierKeys.Control);
                    NavigateSelection = new DirectionalNavigationGestures(ModifierKeys.None);
                    ToggleSelected = new AnyGesture(new KeyGesture(Key.Space), new KeyGesture(Key.Enter));
                    DeselectAll = new KeyGesture(Key.Escape);
                    NextNavigationLayer = new KeyGesture(Key.OemCloseBrackets, ModifierKeys.Control);
                    PrevNavigationLayer = new KeyGesture(Key.OemOpenBrackets, ModifierKeys.Control);
                }

                /// <summary>
                /// Directional gestures used for panning the viewport.
                /// </summary>
                /// <remarks>Defaults to <see cref="Key.Space"/>+arrow keys.</remarks>
                public DirectionalNavigationGestures Pan { get; }

                /// <summary>
                /// Directional gestures used for dragging the selected items.
                /// </summary>
                /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+arrow keys.</remarks>
                public DirectionalNavigationGestures DragSelection { get; }

                /// <summary>
                /// Directional gestures used to navigate the selection focus (e.g., between nodes).
                /// </summary>
                /// <remarks>Defaults to arrow keys.</remarks>
                public DirectionalNavigationGestures NavigateSelection { get; }

                /// <summary>
                /// Gesture used to toggle the selected state of the currently focused item.
                /// </summary>
                /// <remarks>Defaults to <see cref="Key.Space"/></remarks>
                public InputGestureRef ToggleSelected { get; }

                /// <summary>
                /// Gesture used to clear the current selection.
                /// </summary>
                /// <remarks>Defaults to <see cref="Key.Escape"/>.</remarks>
                public InputGestureRef DeselectAll { get; }

                /// <summary>
                /// Gesture used to activate the previous keyboard navigation layer.
                /// </summary>
                /// <remarks><see cref="ModifierKeys.Control"/>+<see cref="Key.OemCloseBrackets"/>.</remarks>
                public InputGestureRef NextNavigationLayer { get; }

                /// <summary>
                /// Gesture used to activate the next keyboard navigation layer.
                /// </summary>
                /// <remarks><see cref="ModifierKeys.Control"/>+<see cref="Key.OemOpenBrackets"/>.</remarks>
                public InputGestureRef PrevNavigationLayer { get; }

                /// <summary>Copies from the specified gestures.</summary>
                /// <param name="gestures">The gestures to copy.</param>
                public void Apply(KeyboardGestures gestures)
                {
                    Pan.Apply(gestures.Pan);
                    DragSelection.Apply(gestures.DragSelection);
                    NavigateSelection.Apply(gestures.NavigateSelection);
                    ToggleSelected.Value = gestures.ToggleSelected.Value;
                    DeselectAll.Value = gestures.DeselectAll.Value;
                    NextNavigationLayer.Value = gestures.NextNavigationLayer.Value;
                    PrevNavigationLayer.Value = gestures.PrevNavigationLayer.Value;
                }

                /// <summary>
                /// Unbinds all the gestures.
                /// </summary>
                public void Unbind()
                {
                    Pan.Unbind();
                    DragSelection.Unbind();
                    NavigateSelection.Unbind();
                    ToggleSelected.Unbind();
                    DeselectAll.Unbind();
                    NextNavigationLayer.Unbind();
                    PrevNavigationLayer.Unbind();
                }
            }

            public NodifyEditorGestures()
            {
                Keyboard = new KeyboardGestures();
                Selection = new SelectionGestures();
                SelectAll = ApplicationCommands.SelectAll.InputGestures[0].AsRef();
                Cutting = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt | ModifierKeys.Shift, true);
                PushItems = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Control | ModifierKeys.Shift, true);
                Pan = new AnyGesture(new MouseGesture(MouseAction.RightClick), new MouseGesture(MouseAction.MiddleClick));
                ZoomModifierKey = ModifierKeys.None;
                ZoomIn = new AnyGesture(new KeyGesture(Key.OemPlus, ModifierKeys.Control), new KeyGesture(Key.Add, ModifierKeys.Control));
                ZoomOut = new AnyGesture(new KeyGesture(Key.OemMinus, ModifierKeys.Control), new KeyGesture(Key.Subtract, ModifierKeys.Control));
                ResetViewport = new KeyGesture(Key.Home);
                FitToScreen = new KeyGesture(Key.Home, ModifierKeys.Shift);
                CancelAction = new AnyGesture(new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
                PanWithMouseWheel = false;
                PanHorizontalModifierKey = ModifierKeys.Shift;
                PanVerticalModifierKey = ModifierKeys.None;
            }

            public KeyboardGestures Keyboard { get; }

            /// <summary>Gesture used to start selecting using a <see cref="SelectionGestures"/> strategy.</summary>
            public SelectionGestures Selection { get; }

            /// <summary>Gesture used to select all <see cref="Nodify.ItemContainer"/>s in the editor.</summary>
            public InputGestureRef SelectAll { get; }

            /// <summary>Gesture used to start cutting connections.</summary>
            public InputGestureRef Cutting { get; }

            /// <summary>Gesture used to start panning.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="MouseAction.MiddleClick"/>.</remarks>
            public InputGestureRef Pan { get; }

            /// <summary>Whether panning using mouse wheel is allowed.</summary>
            /// <remarks>Set the <see cref="ZoomModifierKey"/> to allow zooming using the mouse wheel.</remarks>
            public bool PanWithMouseWheel { get; set; }

            /// <summary>The modifier key required to start panning vertically with the mouse wheel (see <see cref="PanWithMouseWheel"/>)</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.None"/>.</remarks>
            public ModifierKeys PanVerticalModifierKey { get; set; }

            /// <summary>The modifier key required to start panning horizontally with the mouse wheel (see <see cref="PanWithMouseWheel"/>)</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Shift"/>.</remarks>
            public ModifierKeys PanHorizontalModifierKey { get; set; }

            /// <summary>Gesture used to start pushing.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="ModifierKeys.Shift"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef PushItems { get; }

            /// <summary>The key modifier required to start zooming by mouse wheel.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.None"/>.</remarks>
            public ModifierKeys ZoomModifierKey { get; set; }

            /// <summary>Gesture used to zoom in.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemPlus"/>.</remarks>
            public InputGestureRef ZoomIn { get; }

            /// <summary>Gesture used to zoom out.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemMinus"/>.</remarks>
            public InputGestureRef ZoomOut { get; }

            /// <summary>Gesture used to move the editor's viewport location to (0, 0) and set the zoom to 1.</summary>
            /// <remarks>Defaults to <see cref="Key.Home"/>.</remarks>
            public InputGestureRef ResetViewport { get; }

            /// <summary>Gesture used to fit as many containers as possible into the viewport.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Shift"/>+<see cref="Key.Home"/>.</remarks>
            public InputGestureRef FitToScreen { get; }

            /// <summary>Gesture to cancel the current operation.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="Key.Escape"/>.</remarks>
            public InputGestureRef CancelAction { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(NodifyEditorGestures gestures)
            {
                Keyboard.Apply(gestures.Keyboard);
                Selection.Apply(gestures.Selection);
                SelectAll.Value = gestures.SelectAll.Value;
                Cutting.Value = gestures.Cutting.Value;
                PushItems.Value = gestures.PushItems.Value;
                Pan.Value = gestures.Pan.Value;
                ZoomModifierKey = gestures.ZoomModifierKey;
                ZoomIn.Value = gestures.ZoomIn.Value;
                ZoomOut.Value = gestures.ZoomOut.Value;
                ResetViewport.Value = gestures.ResetViewport.Value;
                FitToScreen.Value = gestures.FitToScreen.Value;
                CancelAction.Value = gestures.CancelAction.Value;
                PanWithMouseWheel = gestures.PanWithMouseWheel;
                PanHorizontalModifierKey = gestures.PanHorizontalModifierKey;
                PanVerticalModifierKey = gestures.PanVerticalModifierKey;
            }

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                Keyboard.Unbind();
                Selection.Unbind();
                SelectAll.Unbind();
                Cutting.Unbind();
                Pan.Unbind();
                PushItems.Unbind();
                ZoomIn.Unbind();
                ZoomOut.Unbind();
                ResetViewport.Unbind();
                FitToScreen.Unbind();
                CancelAction.Unbind();
            }
        }

        /// <summary>Gestures used by the <see cref="Connector"/>.</summary>
        public class ConnectorGestures
        {
            public ConnectorGestures()
            {
                Disconnect = new AnyGesture(new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt), new KeyGesture(Key.Delete));
                Connect = new AnyGesture(new MouseGesture(MouseAction.LeftClick), new KeyGesture(Key.Space));
                CancelAction = new AnyGesture(new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
            }

            /// <summary>Gesture to call the <see cref="Connector.DisconnectCommand"/>.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/> or <see cref="Key.Delete"/>.</remarks>
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

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                Disconnect.Unbind();
                Connect.Unbind();
                CancelAction.Unbind();
            }
        }

        /// <summary>Gestures used by the <see cref="BaseConnection"/>.</summary>
        public class ConnectionGestures
        {
            public ConnectionGestures()
            {
                Split = new MouseGesture(MouseAction.LeftDoubleClick);
                Selection = new SelectionGestures(MouseAction.LeftClick);
                Disconnect = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Alt);
            }

            /// <summary>Gesture to call the <see cref="BaseConnection.SplitCommand"/> command.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.LeftDoubleClick"/>.</remarks>
            public InputGestureRef Split { get; }

            /// <summary>Gesture used to start selecting using a <see cref="SelectionGestures"/> strategy.</summary>
            public SelectionGestures Selection { get; }

            /// <summary>Gesture to call the <see cref="BaseConnection.DisconnectCommand"/> command.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Alt"/>+<see cref="MouseAction.LeftClick"/>.</remarks>
            public InputGestureRef Disconnect { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(ConnectionGestures gestures)
            {
                Split.Value = gestures.Split.Value;
                Disconnect.Value = gestures.Disconnect.Value;
                Selection.Apply(gestures.Selection);
            }

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                Split.Unbind();
                Selection.Unbind();
                Disconnect.Unbind();
            }
        }

        /// <summary>Gestures for the <see cref="GroupingNode"/>.</summary>
        public class GroupingNodeGestures
        {
            public GroupingNodeGestures()
            {
                SwitchMovementMode = ModifierKeys.Shift;
                ToggleContentSelection = new AnyGesture(new KeyGesture(Key.Space, ModifierKeys.Control), new KeyGesture(Key.Enter, ModifierKeys.Control));
            }

            /// <summary>The key modifier that will toggle between <see cref="GroupingMovementMode"/>s.</summary>
            /// <remarks>The modifier must be allowed by the <see cref="ItemContainer.Drag"/> gesture.
            /// <br /> Defaults to <see cref="ModifierKeys.Shift"/>.
            /// </remarks>
            public ModifierKeys SwitchMovementMode { get; set; }

            /// <summary>Gesture to toggle the content selection of the <see cref="GroupingNode"/> when it is selected.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.Space"/>.</remarks>
            public InputGestureRef ToggleContentSelection { get; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(GroupingNodeGestures gestures)
            {
                SwitchMovementMode = gestures.SwitchMovementMode;
                ToggleContentSelection.Value = gestures.ToggleContentSelection.Value;
            }

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                ToggleContentSelection.Unbind();
            }
        }

        /// <summary>Gestures used by the <see cref="Nodify.Minimap"/> control.</summary>
        public class MinimapGestures
        {
            public MinimapGestures()
            {
                Pan = new DirectionalNavigationGestures();
                DragViewport = new MouseGesture(MouseAction.LeftClick);
                ResetViewport = new KeyGesture(Key.Home);
                CancelAction = new AnyGesture(new MouseGesture(MouseAction.RightClick), new KeyGesture(Key.Escape));
                ZoomIn = new AnyGesture(new KeyGesture(Key.OemPlus, ModifierKeys.Control), new KeyGesture(Key.Add, ModifierKeys.Control));
                ZoomOut = new AnyGesture(new KeyGesture(Key.OemMinus, ModifierKeys.Control), new KeyGesture(Key.Subtract, ModifierKeys.Control));
                ZoomModifierKey = ModifierKeys.None;
            }

            /// <summary>
            /// Directional gestures used for panning the viewport.
            /// </summary>
            /// <remarks>Defaults to <see cref="Key.Space"/>+arrow keys.</remarks>
            public DirectionalNavigationGestures Pan { get; }

            /// <summary>Gesture to move the viewport inside the <see cref="Minimap" />.</summary>
            public InputGestureRef DragViewport { get; }

            /// <summary>Gesture to move the viewport inside the <see cref="Minimap" />.</summary>
            public InputGestureRef ResetViewport { get; }

            /// <summary>Gesture to cancel the panning operation.</summary>
            /// <remarks>Defaults to <see cref="MouseAction.RightClick"/> or <see cref="Key.Escape"/>.</remarks>
            public InputGestureRef CancelAction { get; }

            /// <summary>Gesture used to zoom in.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemPlus"/>.</remarks>
            public InputGestureRef ZoomIn { get; }

            /// <summary>Gesture used to zoom out.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.Control"/>+<see cref="Key.OemMinus"/>.</remarks>
            public InputGestureRef ZoomOut { get; }

            /// <summary>The key modifier required to start zooming by mouse wheel.</summary>
            /// <remarks>Defaults to <see cref="ModifierKeys.None"/>.</remarks>
            public ModifierKeys ZoomModifierKey { get; set; }

            /// <summary>Copies from the specified gestures.</summary>
            /// <param name="gestures">The gestures to copy.</param>
            public void Apply(MinimapGestures gestures)
            {
                Pan.Apply(gestures.Pan);
                ZoomIn.Value = gestures.ZoomIn.Value;
                ZoomOut.Value = gestures.ZoomOut.Value;
                ResetViewport.Value = gestures.ResetViewport.Value;
                DragViewport.Value = gestures.DragViewport.Value;
                CancelAction.Value = gestures.CancelAction.Value;
                ZoomModifierKey = gestures.ZoomModifierKey;
            }

            /// <summary>
            /// Unbinds all the gestures.
            /// </summary>
            public void Unbind()
            {
                Pan.Unbind();
                ZoomIn.Unbind();
                ZoomOut.Unbind();
                ResetViewport.Unbind();
                DragViewport.Unbind();
                CancelAction.Unbind();
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

        /// <summary>Gestures for the minimap.</summary>
        public MinimapGestures Minimap { get; } = new MinimapGestures();

        /// <summary>Copies from the specified gestures.</summary>
        /// <param name="gestures">The gestures to copy.</param>
        public void Apply(EditorGestures gestures)
        {
            Editor.Apply(gestures.Editor);
            ItemContainer.Apply(gestures.ItemContainer);
            Connector.Apply(gestures.Connector);
            Connection.Apply(gestures.Connection);
            GroupingNode.Apply(gestures.GroupingNode);
            Minimap.Apply(gestures.Minimap);
        }

        /// <summary>
        /// Unbinds all the gestures used by the editor and its controls.
        /// </summary>
        public void Unbind()
        {
            Editor.Unbind();
            ItemContainer.Unbind();
            Connector.Unbind();
            Connection.Unbind();
            Minimap.Unbind();
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// The default state of the editor.
    /// <br />
    /// <br />  Default State
    /// <br />  	- mouse left down  	-> Selecting State
    /// <br />  	- mouse right down  -> Panning State
    /// <br /> 	
    /// <br />  Selecting State
    /// <br />  	- mouse left up 	-> Default State
    /// <br />  	- mouse right down 	-> Panning State
    /// <br /> 
    /// <br />  Panning State
    /// <br />  	- mouse right up	-> previous state (Selecting State or Default State)
    /// <br />  	- mouse left up		-> Panning State
    /// <br />	
    /// </summary>
    public class EditorDefaultState : EditorState
    {
        /// <summary>Constructs an instance of the <see cref="EditorDefaultState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorDefaultState(NodifyEditor editor) : base(editor)
        {
        }

        /// <inheritdoc />
        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (Editor.CanSelectMultipleItems && gestures.Selection.Select.Matches(e.Source, e))
            {
                SelectionType selectionType = SelectionHelper.GetSelectionType(e);
                PushState(new EditorSelectingState(Editor, selectionType));
            }
            else if (!Editor.DisablePanning && gestures.Pan.Matches(e.Source, e))
            {
                PushState(new EditorPanningState(Editor));
            }
            else if (gestures.Cutting.Matches(e.Source, e))
            {
                PushState(new EditorCuttingState(Editor));
            }
            else if (gestures.PushItems.Matches(e.Source, e))
            {
                PushState(new EditorPushingItemsState(Editor));
            }
        }

        public override void HandleMouseWheel(MouseWheelEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.PanWithMouseWheel)
            {
                if (Keyboard.Modifiers == gestures.PanHorizontalModifierKey)
                {
                    Editor.ViewportLocation = new Point(Editor.ViewportLocation.X - e.Delta / Editor.ViewportZoom, Editor.ViewportLocation.Y);
                    e.Handled = true;
                }
                else if (Keyboard.Modifiers == gestures.PanVerticalModifierKey)
                {
                    Editor.ViewportLocation = new Point(Editor.ViewportLocation.X, Editor.ViewportLocation.Y - e.Delta / Editor.ViewportZoom);
                    e.Handled = true;
                }
            }
        }
    }
}

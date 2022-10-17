using System.Windows.Input;
using static Nodify.SelectionHelper;

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
            if (EditorGestures.Select.Matches(e.Source, e))
            {
                SelectionType selectionType = GetSelectionType(e);
                var selecting = new EditorSelectingState(Editor, selectionType);
                PushState(selecting);
            }
            else if (!Editor.DisablePanning && EditorGestures.Pan.Matches(e.Source, e))
            {
                PushState(new EditorPanningState(Editor));
            }
        }

        private static SelectionType GetSelectionType(MouseButtonEventArgs e)
        {
            if (EditorGestures.Selection.Append.Matches(e.Source, e))
            {
                return SelectionType.Append;
            }

            if (EditorGestures.Selection.Invert.Matches(e.Source, e))
            {
                return SelectionType.Invert;
            }

            if (EditorGestures.Selection.Remove.Matches(e.Source, e))
            {
                return SelectionType.Remove;
            }

            return SelectionType.Replace;
        }
    }
}

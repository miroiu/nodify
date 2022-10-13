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
            if (EditorGestures.Select.Matches(e.Source, e))
            {
                var selecting = new EditorSelectingState(Editor);
                Editor.PushState(selecting);
            }
            else if (EditorGestures.Pan.Matches(e.Source, e) && !Editor.DisablePanning)
            {
                Editor.PushState(new EditorPanningState(Editor));
            }
        }
    }
}

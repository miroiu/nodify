using System.Windows.Input;

namespace Nodify
{
    /// <summary>The base class for editor states.</summary>
    public abstract class EditorState : InputElementState<EditorState>
    {
        /// <summary>Constructs a new <see cref="EditorState"/>.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorState(NodifyEditor editor)
        {
            Editor = editor;
        }

        /// <summary>The owner of the state.</summary>
        protected NodifyEditor Editor { get; }

        /// <summary>Handles auto panning when mouse is outside the editor.</summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> that contains the event data.</param>
        public virtual void HandleAutoPanning(MouseEventArgs e) { }

        /// <summary>Pushes a new state into the stack.</summary>
        /// <param name="newState">The new state.</param>
        public virtual void PushState(EditorState newState) => Editor.PushState(newState);

        /// <summary>Pops the current state from the stack.</summary>
        public virtual void PopState() => Editor.PopState();
    }
}

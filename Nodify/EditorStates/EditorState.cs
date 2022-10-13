using System.Windows.Input;

namespace Nodify
{
    /// <summary>The base class for editor states.</summary>
    public abstract class EditorState
    {
        /// <summary>Constructs a new <see cref="EditorState"/>.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorState(NodifyEditor editor)
        {
            Editor = editor;
        }

        /// <summary>The owner of the state.</summary>
        protected NodifyEditor Editor { get; }

        /// <inheritdoc cref="NodifyEditor.OnMouseDown(MouseButtonEventArgs)"/>
        public virtual void HandleMouseDown(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="NodifyEditor.OnMouseUp(MouseButtonEventArgs)"/>
        public virtual void HandleMouseUp(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="NodifyEditor.OnMouseMove(MouseEventArgs)"/>
        public virtual void HandleMouseMove(MouseEventArgs e) { }

        /// <inheritdoc cref="NodifyEditor.OnMouseWheel(MouseWheelEventArgs)"/>
        public virtual void HandleMouseWheel(MouseWheelEventArgs e) { }

        /// <summary>Handles auto panning when mouse is outside the editor.</summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> that contains the event data.</param>
        public virtual void HandleAutoPanning(MouseEventArgs e) { }

        /// <summary>Called when <see cref="NodifyEditor.PushState(EditorState)"/> or <see cref="NodifyEditor.PopState"/> is called.</summary>
        public virtual void Enter() { }

        /// <summary>Called when <see cref="NodifyEditor.PopState"/> is called.</summary>
        public virtual void Exit() { }
    }
}

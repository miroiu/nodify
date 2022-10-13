using System.Windows.Input;

namespace Nodify
{
    /// <summary>The default state of the editor.</summary>
    public class EditorDefaultState : EditorState
    {
        /// <summary>Constructs an instance of the <see cref="EditorDefaultState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorDefaultState(NodifyEditor editor) : base(editor)
        {
        }

        // TODO: Take key combinations into account like CTRL+CLICK (maybe using MouseGesture)
        /// <inheritdoc />
        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var selecting = new EditorSelectingState(Editor);
                Editor.PushState(selecting);
            }
            else if (e.ChangedButton == MouseButton.Right && !Editor.DisablePanning)
            {
                Editor.PushState(new EditorPanningState(Editor));
            }
        }
    }
}

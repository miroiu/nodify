using System.Windows.Input;

namespace Nodify
{
    /// <summary>The selecting state of the editor.</summary>
    public class EditorSelectingState : EditorState
    {
        /// <summary>The selection helper.</summary>
        protected SelectionHelper Selection { get; }

        /// <summary>Constructs an instance of the <see cref="EditorSelectingState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorSelectingState(NodifyEditor editor) : base(editor)
            => Selection = new SelectionHelper(editor);

        /// <inheritdoc />
        public override void Enter()
            => Selection.Start(Editor.MouseLocation);

        /// <inheritdoc />
        public override void Exit()
            => Selection.End();

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
            => Selection.Update(Editor.MouseLocation);

        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            if (EditorGestures.Pan.Matches(e.Source, e))
            {
                Editor.PushState(new EditorPanningState(Editor));
            }
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            if (EditorGestures.Select.Matches(e.Source, e))
            {
                Editor.PopState();
            }
        }

        public override void HandleAutoPanning(MouseEventArgs e) 
            => HandleMouseMove(e);
    }
}

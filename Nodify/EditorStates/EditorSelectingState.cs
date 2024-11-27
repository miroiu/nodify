using System.Windows.Input;

namespace Nodify
{
    /// <summary>The selecting state of the editor.</summary>
    public class EditorSelectingState : EditorState
    {
        private readonly SelectionType _type;
        private bool Canceled { get; set; } = NodifyEditor.AllowSelectionCancellation;

        /// <summary>Constructs an instance of the <see cref="EditorSelectingState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        /// <param name="type">The selection strategy.</param>
        public EditorSelectingState(NodifyEditor editor, SelectionType type) : base(editor)
        {
            _type = type;
        }

        /// <inheritdoc />
        public override void Enter(EditorState? from)
        {
            Canceled = false;

            Editor.BeginSelecting(Editor.MouseLocation, _type);
        }

        /// <inheritdoc />
        public override void Exit()
        {
            // TODO: This is not canceled on LostMouseCapture (add OnLostMouseCapture/OnCancel callback?)
            if (Canceled)
            {
                Editor.CancelSelecting();
            }
            else
            {
                Editor.EndSelecting();
            }
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e) 
            => Editor.UpdateSelection(Editor.MouseLocation);

        /// <inheritdoc />
        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            if (!Editor.DisablePanning && EditorGestures.Mappings.Editor.Pan.Matches(e.Source, e))
            {
                PushState(new EditorPanningState(Editor));
            }
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.SelectionGestures gestures = EditorGestures.Mappings.Editor.Selection;
            if(gestures.Select.Matches(e.Source, e))
            {
                PopState();
            }
            else if(NodifyEditor.AllowSelectionCancellation && gestures.Cancel.Matches(e.Source, e))
            {
                Canceled = true;
                e.Handled = true;   // prevents opening context menu

                PopState();
            }
        }

        /// <inheritdoc />
        public override void HandleAutoPanning(MouseEventArgs e)
            => HandleMouseMove(e);

        public override void HandleKeyUp(KeyEventArgs e)
        {
            EditorGestures.SelectionGestures gestures = EditorGestures.Mappings.Editor.Selection;
            if (NodifyEditor.AllowSelectionCancellation && gestures.Cancel.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }
}

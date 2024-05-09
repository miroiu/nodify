using System.Windows.Input;
using static Nodify.SelectionHelper;

namespace Nodify
{
    /// <summary>The selecting state of the editor.</summary>
    public class EditorSelectingState : EditorState
    {
        private readonly SelectionType _type;
        private bool _canceled;

        /// <summary>The selection helper.</summary>
        protected SelectionHelper Selection { get; }

        /// <summary>Constructs an instance of the <see cref="EditorSelectingState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorSelectingState(NodifyEditor editor, SelectionType type) : base(editor)
        {
            Selection = new SelectionHelper(editor);
            _type = type;
        }

        /// <inheritdoc />
        public override void Enter(EditorState? from)
        {
            _canceled = false;
            Selection.Start(Editor.MouseLocation, _type);
        }

        /// <inheritdoc />
        public override void Exit()
        {
            if (_canceled)
            {
                Selection.Abort();
            }
            else
            {
                Selection.End();
            }
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
            => Selection.Update(Editor.MouseLocation);

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

            bool canCancel = gestures.Cancel.Matches(e.Source, e);
            bool canComplete = gestures.Select.Matches(e.Source, e);
            if (canCancel || canComplete)
            {
                _canceled = !canComplete && canCancel;
                PopState();
            }
        }

        /// <inheritdoc />
        public override void HandleAutoPanning(MouseEventArgs e)
            => HandleMouseMove(e);

        public override void HandleKeyUp(KeyEventArgs e)
        {
            if (EditorGestures.Mappings.Editor.Selection.Cancel.Matches(e.Source, e))
            {
                _canceled = true;
                PopState();
            }
        }
    }
}

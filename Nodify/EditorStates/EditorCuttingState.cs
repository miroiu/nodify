using System.Windows.Input;

namespace Nodify
{
    public class EditorCuttingState : EditorState
    {
        public bool Canceled { get; set; } = CuttingLine.AllowCuttingCancellation;

        public EditorCuttingState(NodifyEditor editor) : base(editor)
        {
        }

        public override void Enter(EditorState? from)
        {
            Canceled = false;

            Editor.BeginCutting(Editor.MouseLocation);
        }

        public override void Exit()
        {
            // TODO: This is not canceled on LostMouseCapture (add OnLostMouseCapture/OnCancel callback?)
            if (Canceled)
            {
                Editor.CancelCutting();
            }
            else
            {
                Editor.EndCutting();
            }
        }

        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.Cutting.Matches(e.Source, e))
            {
                PopState();
            }
            else if (CuttingLine.AllowCuttingCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                e.Handled = true;   // prevents opening context menu

                PopState();
            }
        }

        public override void HandleMouseMove(MouseEventArgs e)
        {
            Editor.UpdateCuttingLine(Editor.MouseLocation);
        }

        public override void HandleKeyUp(KeyEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (CuttingLine.AllowCuttingCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public class EditorCuttingState : EditorState
    {
        private Point _initialPosition;
        private bool Canceled { get; set; } = CuttingLine.AllowCuttingCancellation;

        public EditorCuttingState(NodifyEditor editor) : base(editor)
        {
        }

        public override void Enter(EditorState? from)
        {
            Canceled = false;

            _initialPosition = Editor.MouseLocation;
            Editor.BeginCutting(_initialPosition);
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
                // Suppress the context menu if the mouse moved beyond the defined drag threshold
                if (e.ChangedButton == MouseButton.Right && Editor.ContextMenu != null)
                {
                    double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
                    double dragDistance = (Editor.MouseLocation - _initialPosition).LengthSquared;

                    if (dragDistance > dragThreshold)
                    {
                        e.Handled = true;
                    }
                }

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

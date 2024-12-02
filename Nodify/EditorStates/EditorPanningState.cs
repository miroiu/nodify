using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>The panning state of the editor.</summary>
    public class EditorPanningState : EditorState
    {
        private Point _initialMousePosition;
        private Point _previousMousePosition;
        private Point _currentMousePosition;

        private bool Canceled { get; set; } = NodifyEditor.AllowPanningCancellation;

        /// <summary>Constructs an instance of the <see cref="EditorPanningState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorPanningState(NodifyEditor editor) : base(editor)
        {
        }

        /// <inheritdoc />
        public override void Exit()
        {
            // TODO: This is not canceled on LostMouseCapture (add OnLostMouseCapture/OnCancel callback?)
            if (Canceled)
            {
                Editor.CancelPanning();
            }
            else
            {
                Editor.EndPanning();
            }
        }

        /// <inheritdoc />
        public override void Enter(EditorState? from)
        {
            Canceled = false;

            _initialMousePosition = Mouse.GetPosition(Editor);
            _previousMousePosition = _initialMousePosition;
            _currentMousePosition = _initialMousePosition;

            Editor.BeginPanning();
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
        {
            _currentMousePosition = e.GetPosition(Editor);
            Editor.UpdatePanning((_currentMousePosition - _previousMousePosition) / Editor.ViewportZoom);
            _previousMousePosition = _currentMousePosition;
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.Pan.Matches(e.Source, e))
            {
                // Suppress the context menu if the mouse moved beyond the defined drag threshold or the editor is selecting
                if (e.ChangedButton == MouseButton.Right && Editor.HasContextMenu)
                {
                    double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
                    double dragDistance = (_currentMousePosition - _initialMousePosition).LengthSquared;

                    if (dragDistance > dragThreshold || Editor.IsSelecting)
                    {
                        e.Handled = true;
                    }
                }

                PopState();
            }
            else if (gestures.Selection.Select.Matches(e.Source, e) && Editor.IsSelecting)
            {
                PopState();
                // Cancel selection and continue panning
                if (Editor.State is EditorSelectingState && !Editor.DisablePanning)
                {
                    PopState();
                    PushState(new EditorPanningState(Editor));
                }
            }
            else if (NodifyEditor.AllowPanningCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                e.Handled = true;   // prevents opening context menu

                PopState();
            }
        }

        public override void HandleKeyUp(KeyEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (NodifyEditor.AllowPanningCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }
}

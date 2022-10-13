using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    // TODO: Space (trigger) + Click Drag (action)?
    /// <summary>The panning state of the editor.</summary>
    public class EditorPanningState : EditorState
    {
        private Point _initialMousePosition;
        private Point _previousMousePosition;
        private Point _currentMousePosition;

        /// <summary>Constructs an instance of the <see cref="EditorPanningState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorPanningState(NodifyEditor editor) : base(editor)
        {
        }

        /// <inheritdoc />
        public override void Exit()
            => Editor.IsPanning = false;

        /// <inheritdoc />
        public override void Enter()
        {
            _initialMousePosition = Mouse.GetPosition(Editor);
            _previousMousePosition = _initialMousePosition;
            _currentMousePosition = _initialMousePosition;
            Editor.IsPanning = true;
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
        {
            _currentMousePosition = e.GetPosition(Editor);
            Editor.ViewportLocation -= (_currentMousePosition - _previousMousePosition) / Editor.ViewportZoom;
            _previousMousePosition = _currentMousePosition;
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                // Handle right click if panning and moved the mouse more than threshold so context menus don't open
                double contextMenuTreshold = NodifyEditor.HandleRightClickAfterPanningThreshold * NodifyEditor.HandleRightClickAfterPanningThreshold;
                if ((_currentMousePosition - _initialMousePosition).LengthSquared > contextMenuTreshold)
                {
                    e.Handled = true;
                }

                Editor.PopState();
            }
            else if (e.ChangedButton == MouseButton.Left && Editor.IsSelecting)
            {
                // Cancel selection and continue panning
                Editor.PopState();
                Editor.PopState();
                Editor.PushState(new EditorPanningState(Editor));
            }
        }
    }
}

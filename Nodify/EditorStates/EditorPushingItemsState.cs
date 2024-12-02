using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    public class EditorPushingItemsState : EditorState
    {
        private Point _prevPosition;
        private Point _initialPosition;

        private bool Canceled { get; set; } = NodifyEditor.AllowPushItemsCancellation;

        public EditorPushingItemsState(NodifyEditor editor) : base(editor)
        {
        }

        public override void Enter(EditorState? from)
        {
            Canceled = false;

            _initialPosition = Editor.MouseLocation;
            _prevPosition = _initialPosition;
        }

        public override void Exit()
        {
            // TODO: This is not canceled on LostMouseCapture (add OnLostMouseCapture/OnCancel callback?)
            if (Canceled)
            {
                Editor.CancelPushingItems();
            }
            else
            {
                Editor.EndPushingItems();
            }
        }

        public override void HandleMouseMove(MouseEventArgs e)
        {
            if (Editor.IsPushingItems)
            {
                Editor.UpdatePushedArea(Editor.MouseLocation - _prevPosition);
                _prevPosition = Editor.MouseLocation;
            }
            else
            {
                if (Math.Abs(Editor.MouseLocation.X - _prevPosition.X) >= NodifyEditor.MouseActionSuppressionThreshold)
                {
                    Editor.BeginPushingItems(_prevPosition, Orientation.Horizontal);
                }
                else if (Math.Abs(Editor.MouseLocation.Y - _prevPosition.Y) >= NodifyEditor.MouseActionSuppressionThreshold)
                {
                    Editor.BeginPushingItems(_prevPosition, Orientation.Vertical);
                }
            }
        }

        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.PushItems.Matches(e.Source, e))
            {
                // Suppress the context menu if the mouse moved beyond the defined drag threshold
                if (e.ChangedButton == MouseButton.Right && Editor.HasContextMenu)
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
            else if (NodifyEditor.AllowPushItemsCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                e.Handled = true;   // prevents opening context menu

                PopState();
            }
        }

        public override void HandleKeyUp(KeyEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (NodifyEditor.AllowPushItemsCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }
}

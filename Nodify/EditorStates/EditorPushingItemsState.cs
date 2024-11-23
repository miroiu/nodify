using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    public class EditorPushingItemsState : EditorState
    {
        private Point _prevPosition;
        private const int _minDragDistance = 10;

        public bool Canceled { get; set; } = NodifyEditor.AllowPushItemsCancellation;

        public EditorPushingItemsState(NodifyEditor editor) : base(editor)
        {
        }

        public override void Enter(EditorState? from)
        {
            Canceled = false;

            _prevPosition = Editor.MouseLocation;
        }

        public override void Exit()
        {
            if (!Editor.IsPushingItems)
            {
                return;
            }

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
                Editor.PushItems(Editor.MouseLocation - _prevPosition);
                _prevPosition = Editor.MouseLocation;
            }
            else
            {
                if (Math.Abs(Editor.MouseLocation.X - _prevPosition.X) >= _minDragDistance)
                {
                    Editor.StartPushingItems(_prevPosition, Orientation.Horizontal);
                }
                else if (Math.Abs(Editor.MouseLocation.Y - _prevPosition.Y) >= _minDragDistance)
                {
                    Editor.StartPushingItems(_prevPosition, Orientation.Vertical);
                }
            }
        }

        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.PushItems.Matches(e.Source, e))
            {
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

using System.Windows.Input;

namespace Nodify
{
    public class EditorPushingItemsState : EditorState
    {
        private double _prevLocationX;
        public bool Canceled { get; set; } = NodifyEditor.AllowPushItemsCancellation;

        public EditorPushingItemsState(NodifyEditor editor) : base(editor)
        {
        }

        public override void Enter(EditorState? from)
        {
            Canceled = false;

            Editor.StartPushingItems(Editor.MouseLocation);
            _prevLocationX = Editor.MouseLocation.X;
        }

        public override void Exit()
        {
            if (Canceled)
            {
                Editor.CancelPushingItems();
            }
            else
            {
                Editor.EndPushingItems(Editor.MouseLocation);
            }
        }

        public override void HandleMouseMove(MouseEventArgs e)
        {
            var offset = Editor.MouseLocation.X - _prevLocationX;
            _prevLocationX = Editor.MouseLocation.X;

            Editor.PushItems(offset);
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
    }
}

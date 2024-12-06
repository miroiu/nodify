using System.Windows.Input;

namespace Nodify
{
    public class EditorCuttingState : ElementOperationState<NodifyEditor>
    {
        protected override bool HasContextMenu => Element.HasContextMenu;

        public EditorCuttingState(NodifyEditor editor)
            : base(editor, EditorGestures.Mappings.Editor.Cutting, EditorGestures.Mappings.Editor.CancelAction)
        {
        }

        protected override void OnBegin(InputEventArgs e) 
            => Element.BeginCutting();

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
            => Element.UpdateCuttingLine(Element.MouseLocation);

        protected override void OnEnd(InputEventArgs e)
            => Element.EndCutting();

        protected override void OnCancel(InputEventArgs e)
            => Element.CancelCutting();
    }
}

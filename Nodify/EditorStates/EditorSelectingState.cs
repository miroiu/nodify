using System.Windows.Input;

namespace Nodify
{
    /// <summary>The selecting state of the editor.</summary>
    public class EditorSelectingState : DragState<NodifyEditor>
    {
        protected override bool HasContextMenu => Element.HasContextMenu;
        protected override bool CanBegin => Element.CanSelectMultipleItems && !Element.IsPanning;

        /// <summary>Constructs an instance of the <see cref="EditorSelectingState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        /// <param name="type">The selection strategy.</param>
        public EditorSelectingState(NodifyEditor editor)
            : base(editor, EditorGestures.Mappings.Editor.Selection.Select, EditorGestures.Mappings.Editor.Selection.Cancel)
        {
        }

        protected override void OnBegin(InputEventArgs e)
        {
            var selectionType = EditorGestures.Mappings.Editor.Selection.GetSelectionType(e);
            Element.BeginSelecting(selectionType);
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
            => Element.UpdateSelection(Element.MouseLocation);

        protected override void OnEnd(InputEventArgs e)
            => Element.EndSelecting();

        protected override void OnCancel(InputEventArgs e)
            => Element.CancelSelecting();
    }
}

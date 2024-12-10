using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Represents the cutting state in the <see cref="NodifyEditor"/>, allowing users to cut connections between elements using a drag gesture.
    /// </summary>
    public class EditorCuttingState : DragState<NodifyEditor>
    {
        protected override bool HasContextMenu => Element.HasContextMenu;
        protected override bool CanCancel => NodifyEditor.AllowCuttingCancellation;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorCuttingState"/> class.
        /// </summary>
        /// <param name="editor">The <see cref="NodifyEditor"/> associated with this state.</param>
        public EditorCuttingState(NodifyEditor editor)
            : base(editor, EditorGestures.Mappings.Editor.Cutting, EditorGestures.Mappings.Editor.CancelAction)
        {
        }

        protected override void OnBegin(InputEventArgs e) 
            => Element.BeginCutting();

        protected override void OnMouseMove(MouseEventArgs e)
            => Element.UpdateCuttingLine(Element.MouseLocation);

        protected override void OnEnd(InputEventArgs e)
            => Element.EndCutting();

        protected override void OnCancel(InputEventArgs e)
            => Element.CancelCutting();
    }
}

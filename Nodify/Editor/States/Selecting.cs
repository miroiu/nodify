using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        /// <summary>
        /// Represents the selecting state of the <see cref="NodifyEditor"/>.
        /// This state is responsible for handling item selection within the editor.
        /// </summary>
        public class Selecting : DragState<NodifyEditor>
        {
            protected override bool HasContextMenu => Element.HasContextMenu;
            protected override bool CanBegin => Element.CanSelectMultipleItems && !Element.IsPanning && !Element.IsCutting && !Element.IsPushingItems;
            protected override bool CanCancel => NodifyEditor.AllowSelectionCancellation;
            protected override bool IsToggle => EnableToggledSelectingMode;

            /// <summary>
            /// Initializes a new instance of the <see cref="Selecting"/> class.
            /// </summary>
            /// <param name="editor">The <see cref="NodifyEditor"/> associated with this state.</param>
            public Selecting(NodifyEditor editor)
                : base(editor, EditorGestures.Mappings.Editor.Selection.Select, EditorGestures.Mappings.Editor.Selection.Cancel)
            {
            }

            protected override void OnBegin(InputEventArgs e)
            {
                var selectionType = EditorGestures.Mappings.Editor.Selection.GetSelectionType(e);
                Element.BeginSelecting(selectionType);
            }

            protected override void OnMouseMove(MouseEventArgs e)
                => Element.UpdateSelection(Element.MouseLocation);

            protected override void OnEnd(InputEventArgs e)
                => Element.EndSelecting();

            protected override void OnCancel(InputEventArgs e)
                => Element.CancelSelecting();
        }
    }
}

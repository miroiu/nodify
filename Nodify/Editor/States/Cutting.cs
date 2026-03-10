using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        /// <summary>
        /// Represents the cutting state in the <see cref="NodifyEditor"/>, allowing users to cut connections between elements using a drag gesture.
        /// </summary>
        public class Cutting : DragState<NodifyEditor>
        {
            protected override bool HasContextMenu => Element.HasContextMenu;
            protected override bool CanBegin => !Element.IsSelecting && !Element.IsPanning && !Element.IsPushingItems;
            protected override bool CanCancel => NodifyEditor.AllowCuttingCancellation;
            protected override bool IsToggle => EnableToggledCuttingMode;

            protected override InputGesture DragGesture => Element.ActualGestures.Editor.Cutting;
            protected override InputGesture? CancelGesture => Element.ActualGestures.Editor.CancelAction;

            /// <summary>
            /// Initializes a new instance of the <see cref="Cutting"/> class.
            /// </summary>
            /// <param name="editor">The <see cref="NodifyEditor"/> associated with this state.</param>
            public Cutting(NodifyEditor editor) : base(editor)
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
}

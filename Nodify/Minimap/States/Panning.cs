using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class MinimapState
    {
        /// <summary>
        /// Represents the panning state of the <see cref="Minimap"/>, allowing the user to pan the viewport by clicking and dragging.
        /// </summary>
        public class Panning : DragState<Minimap>
        {
            protected override bool CanBegin => !Element.IsReadOnly;
            protected override bool CanCancel => Minimap.AllowPanningCancellation;
            protected override bool IsToggle => EnableToggledPanningMode;

            protected override InputGesture DragGesture => Element.ActualGestures.DragViewport;
            protected override InputGesture? CancelGesture => Element.ActualGestures.CancelAction;

            /// <summary>
            /// Initializes a new instance of the <see cref="Panning"/> class.
            /// </summary>
            /// <param name="minimap">The <see cref="Minimap"/> associated with this state.</param>
            public Panning(Minimap minimap) : base(minimap)
            {
            }

            protected override void OnBegin(InputEventArgs e)
                => Element.BeginPanning();

            protected override void OnMouseMove(MouseEventArgs e)
                => Element.UpdatePanning(Element.MouseLocation);

            protected override void OnEnd(InputEventArgs e)
                => Element.EndPanning();

            protected override void OnCancel(InputEventArgs e)
                => Element.CancelPanning();
        }
    }
}

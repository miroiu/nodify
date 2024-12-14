using System;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        /// <summary>
        /// Represents the zooming state of the <see cref="NodifyEditor"/>.
        /// This state handles zooming operations using the mouse wheel with an optional modifier key.
        /// </summary>
        public class Zooming : InputElementState<NodifyEditor>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Zooming"/> class.
            /// </summary>
            /// <param name="editor">The <see cref="NodifyEditor"/> associated with this state.</param>
            public Zooming(NodifyEditor editor) : base(editor)
            {
            }

            protected override void OnMouseWheel(MouseWheelEventArgs e)
            {
                EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
                if (gestures.ZoomModifierKey == Keyboard.Modifiers && IsZoomingAllowed())
                {
                    double zoom = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
                    Element.ZoomAtPosition(zoom, Element.MouseLocation);
                    e.Handled = true;
                }
            }

            private bool IsZoomingAllowed()
            {
                return !Element.DisableZooming
                    && (AllowZoomingWhileSelecting || !Element.IsSelecting)
                    && (AllowZoomingWhileCutting || !Element.IsCutting)
                    && (AllowZoomingWhilePushingItems || !Element.IsPushingItems)
                    && (AllowZoomingWhilePanning || !Element.IsPanning);
            }
        }
    }
}

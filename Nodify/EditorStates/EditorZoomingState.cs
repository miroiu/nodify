using System;
using System.Windows.Input;

namespace Nodify
{
    public class EditorZoomingState : InputElementState<NodifyEditor>
    {
        /// <summary>Constructs an instance of the <see cref="EditorZoomingState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorZoomingState(NodifyEditor editor) : base(editor)
        {
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.ZoomModifierKey == Keyboard.Modifiers)
            {
                double zoom = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
                Element.ZoomAtPosition(zoom, Element.MouseLocation);
                e.Handled = true;
            }
        }
    }
}

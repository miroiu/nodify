using System;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>The panning state of the editor.</summary>
    public class EditorPanningState : DragState<NodifyEditor>
    {
        protected override bool HasContextMenu => Element.HasContextMenu;
        protected override bool CanBegin => !Element.DisablePanning;
        protected override bool CanCancel => NodifyEditor.AllowPanningCancellation;

        private Point _prevPosition;

        /// <summary>Constructs an instance of the <see cref="EditorPanningState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorPanningState(NodifyEditor editor)
            : base(editor, EditorGestures.Mappings.Editor.Pan, EditorGestures.Mappings.Editor.CancelAction)
        {
        }

        protected override void OnBegin(InputEventArgs e)
        {
            _prevPosition = Mouse.GetPosition(Element);
            Element.BeginPanning();
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var currentMousePosition = e.GetPosition(Element);
            Element.UpdatePanning((currentMousePosition - _prevPosition) / Element.ViewportZoom);
            _prevPosition = currentMousePosition;
        }

        protected override void OnEnd(InputEventArgs e)
            => Element.EndPanning();

        protected override void OnCancel(InputEventArgs e)
            => Element.CancelPanning();
    }

    public class EditorPanningWithMouseWheelState : InputElementState<NodifyEditor>
    {
        /// <summary>Constructs an instance of the <see cref="EditorPanningWithMouseWheelState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorPanningWithMouseWheelState(NodifyEditor editor) : base(editor)
        {
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.PanWithMouseWheel && Keyboard.Modifiers == gestures.PanHorizontalModifierKey)
            {
                double offset = Math.Sign(e.Delta) * Mouse.MouseWheelDeltaForOneLine / 2 / Element.ViewportZoom;
                Element.UpdatePanning(new Vector(offset, 0d));
                e.Handled = true;
            }
            else if (gestures.PanWithMouseWheel && Keyboard.Modifiers == gestures.PanVerticalModifierKey)
            {
                double offset = Math.Sign(e.Delta) * Mouse.MouseWheelDeltaForOneLine / 2 / Element.ViewportZoom;
                Element.UpdatePanning(new Vector(0d, offset));
                e.Handled = true;
            }
        }
    }
}

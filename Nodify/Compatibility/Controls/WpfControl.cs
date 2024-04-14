using System;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Nodify.Compatibility
{
    public class WpfControl : TemplatedControl
    {
        protected PointerEventArgs? currentPointerArgs;
        
        protected override void OnPointerEntered(PointerEventArgs e)
        {
            currentPointerArgs = e;
            OnMouseEnter(e);
            currentPointerArgs = null;
            if (!e.Handled)
                base.OnPointerEntered(e);
        }

        protected override  void OnPointerExited(PointerEventArgs e)
        {
            currentPointerArgs = e;
            OnMouseLeave(e);
            currentPointerArgs = null;
            if (!e.Handled)
                base.OnPointerExited(e);
        }

        protected override  void OnPointerMoved(PointerEventArgs e)
        {
            currentPointerArgs = e;
            OnMouseMove(new MouseMoveEventArgs(e));
            currentPointerArgs = null;
            if (!e.Handled)
                base.OnPointerMoved(e);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            currentPointerArgs = e;
            OnMouseDown(new MouseButtonEventArgs(e));
            currentPointerArgs = null;
            if (!e.Handled)
                base.OnPointerPressed(e);
        }

        protected override  void OnPointerReleased(PointerReleasedEventArgs e)
        {
            currentPointerArgs = e;
            OnMouseUp(new MouseButtonEventArgs(e));
            currentPointerArgs = null;
            if (!e.Handled)
                base.OnPointerReleased(e);
        }
        
        protected virtual void OnMouseEnter(PointerEventArgs e)
        {
        }

        protected virtual void OnMouseLeave(PointerEventArgs e)
        {
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        protected virtual void OnMouseDown(MouseButtonEventArgs e)
        {
        }

        protected virtual void OnMouseUp(MouseButtonEventArgs e)
        {
        }

        protected void CaptureMouseSafe()
        {
            if (currentPointerArgs == null)
                throw new InvalidOperationException($"You may only call {nameof(ReleaseMouseCapture)} from within a {nameof(OnMouseUp)} or {nameof(OnMouseDown)} event handler.");
            currentPointerArgs.Pointer.Capture(this);
            this.PropagateMouseCapturedWithin(true);
        }
        
        protected virtual void ReleaseMouseCapture()
        {
            if (currentPointerArgs == null)
                throw new InvalidOperationException($"You may only call {nameof(ReleaseMouseCapture)} from within a {nameof(OnMouseUp)} or {nameof(OnMouseDown)} event handler.");
            currentPointerArgs.Pointer.Capture(null);
            this.PropagateMouseCapturedWithin(false);
        }
        
        public bool IsMouseCaptured
        {
            get
            {
                if (currentPointerArgs == null)
                    throw new InvalidOperationException($"You may only call {nameof(ReleaseMouseCapture)} from within a {nameof(OnMouseUp)} or {nameof(OnMouseDown)} event handler.");

                return ReferenceEquals(currentPointerArgs?.Pointer.Captured, this);
            }
        }
    }
}
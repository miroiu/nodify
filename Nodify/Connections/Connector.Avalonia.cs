using Avalonia;
using Avalonia.Controls;

namespace Nodify
{
    public partial class Connector
    {
        private bool ignoreNextOnPointerCaptureLost;

        private IDisposable? capturedMouse;
        
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == IsConnectedProperty)
                PseudoClasses.Set(":connected", (bool)change.NewValue);
        }

        protected override void ReleaseMouseCapture()
        {
            if (capturedMouse != null)
            {
                capturedMouse.Dispose();
                capturedMouse = null;
            }
            else if (currentPointerArgs != null)
                base.ReleaseMouseCapture();
        }
    }
}
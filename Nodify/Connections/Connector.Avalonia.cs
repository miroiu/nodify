using Avalonia;
using Avalonia.Controls;

namespace Nodify
{
    public partial class Connector
    {
        private bool ignoreNextOnPointerCaptureLost;

        private IDisposable? capturedMouse;

        public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty = ContentControl.HorizontalContentAlignmentProperty.AddOwner<Connector>();

        public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty = ContentControl.VerticalContentAlignmentProperty.AddOwner<Connector>();

        public HorizontalAlignment HorizontalContentAlignment
        {
            get => GetValue(HorizontalContentAlignmentProperty);
            set => SetValue(HorizontalContentAlignmentProperty, value);
        }

        public VerticalAlignment VerticalContentAlignment
        {
            get => GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }

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
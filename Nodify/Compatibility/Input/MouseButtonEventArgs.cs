namespace Nodify.Compatibility;

public class MouseButtonEventArgs : MouseEventArgs
{
    private readonly RoutedEventArgs args;

    internal MouseButtonEventArgs(PointerEventArgs pointerArgs)
    {
        this.args = pointerArgs;
        var point = pointerArgs.GetCurrentPoint(null);
        ChangedButton = point.Properties.PointerUpdateKind switch
        {
            PointerUpdateKind.LeftButtonPressed => MouseButton.Left,
            PointerUpdateKind.LeftButtonReleased => MouseButton.Left,
            PointerUpdateKind.RightButtonPressed => MouseButton.Right,
            PointerUpdateKind.RightButtonReleased => MouseButton.Right,
            PointerUpdateKind.MiddleButtonPressed => MouseButton.Middle,
            PointerUpdateKind.MiddleButtonReleased => MouseButton.Middle,
            _ => MouseButton.None
        };
        ClickCount = pointerArgs is PointerPressedEventArgs pressedArgs ? pressedArgs.ClickCount : 1;
        Source = pointerArgs.Source;
        KeyModifiers = pointerArgs.KeyModifiers;
        ButtonState = MouseButtonState.Pressed;
        RightButton = point.Properties.IsRightButtonPressed ? MouseButtonState.Pressed : MouseButtonState.Released;
    }

    public MouseButtonEventArgs(PointerReleasedEventArgs releasedEventArgs)
    {
        this.args = releasedEventArgs;
        var point = releasedEventArgs.GetCurrentPoint(null);
        ChangedButton = point.Properties.PointerUpdateKind switch
        {
            PointerUpdateKind.LeftButtonReleased => MouseButton.Left,
            PointerUpdateKind.RightButtonReleased => MouseButton.Right,
            PointerUpdateKind.MiddleButtonReleased => MouseButton.Middle,
            _ => MouseButton.None
        };
        ClickCount = 1;
        Source = releasedEventArgs.Source;
        KeyModifiers = releasedEventArgs.KeyModifiers;
        ButtonState = MouseButtonState.Released;
        RightButton = point.Properties.IsRightButtonPressed ? MouseButtonState.Pressed : MouseButtonState.Released;
    }

    public override bool Handled
    {
        get => args.Handled;
        set => args.Handled = value;
    }
        
    public MouseButton ChangedButton { get; }
        
    public int ClickCount { get; }
        
    public override object? Source { get; }
        
    public MouseButtonState ButtonState { get; }
        
    public override KeyModifiers KeyModifiers { get; }

    public IInputElement? Captured
    {
        get
        {
            if (args is PointerPressedEventArgs e)
            {
                return e.Pointer.Captured;
            }
            else if (args is PointerReleasedEventArgs r)
            {
                return r.Pointer.Captured;
            }

            return null;
        }
    }

    public MouseButtonState RightButton { get; }

    public IDisposable Capture(IInputElement element)
    {
        if (args is PointerPressedEventArgs e)
        {
            e.Pointer.Capture(element);
            element.PropagateMouseCapturedWithin(true);
            return new ReleaseMouseCaptureOperation(e.Pointer, element);
        }
        else if (args is PointerReleasedEventArgs r)
        {
            r.Pointer.Capture(element);
            element.PropagateMouseCapturedWithin(true);
            return new ReleaseMouseCaptureOperation(r.Pointer, element);
        }

        return EmptyDisposable.Instance;
    }
        
    private struct ReleaseMouseCaptureOperation : IDisposable
    {
        private readonly IPointer pointer;
        private readonly IInputElement element;

        public ReleaseMouseCaptureOperation(IPointer pointer, IInputElement element)
        {
            this.pointer = pointer;
            this.element = element;
        }

        public void Dispose()
        {
            if (pointer.Captured == element)
            {
                pointer.Capture(null);
                element.PropagateMouseCapturedWithin(false);
            }
        }
    }
        
    private struct EmptyDisposable : IDisposable
    {
        public static IDisposable Instance { get; } = new EmptyDisposable();
            
        public void Dispose() { }
    }

    public override Point GetPosition(Visual? relativeTo)
    {
        if (args is PointerEventArgs pointerArgs)
        {
            return pointerArgs.GetPosition(relativeTo);
        }
        return default;
    }
}
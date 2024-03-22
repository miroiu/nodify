namespace Nodify.Compatibility;

public class MouseButtonEventArgs : MouseEventArgs
{
    private readonly RoutedEventArgs args;

    internal MouseButtonEventArgs(PointerPressedEventArgs pressedEventArgs)
    {
        this.args = pressedEventArgs;
        var point = pressedEventArgs.GetCurrentPoint(null);
        ChangedButton = point.Properties.PointerUpdateKind switch
        {
            PointerUpdateKind.LeftButtonPressed => MouseButton.Left,
            PointerUpdateKind.RightButtonPressed => MouseButton.Right,
            PointerUpdateKind.MiddleButtonPressed => MouseButton.Middle,
            _ => MouseButton.None
        };
        ClickCount = pressedEventArgs.ClickCount;
        Source = pressedEventArgs.Source;
        KeyModifiers = pressedEventArgs.KeyModifiers;
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
            return new ReleaseMouseCaptureOperation(e.Pointer, element);
        }
        else if (args is PointerReleasedEventArgs r)
        {
            r.Pointer.Capture(element);
            return new ReleaseMouseCaptureOperation(r.Pointer, element);
        }

        return EmptyDisposable.Instance;
    }
        
    private struct ReleaseMouseCaptureOperation : IDisposable
    {
        private readonly IPointer pointer;
        private readonly IInputElement? element;

        public ReleaseMouseCaptureOperation(IPointer pointer, IInputElement? element)
        {
            this.pointer = pointer;
            this.element = element;
        }

        public void Dispose()
        {
            if (pointer.Captured == element)
                pointer.Capture(null);
        }
    }
        
    private struct EmptyDisposable : IDisposable
    {
        public static IDisposable Instance { get; } = new EmptyDisposable();
            
        public void Dispose() { }
    }

    public override Point GetPosition(Visual? relativeTo)
    {
        if (args is PointerPressedEventArgs pressed)
        {
            return pressed.GetPosition(relativeTo);
        }
        else if (args is PointerReleasedEventArgs released)
        {
            return released.GetPosition(relativeTo);
        }
        return default;
    }
}
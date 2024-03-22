namespace Nodify.Compatibility;

public class MouseMoveEventArgs : MouseEventArgs
{
    private readonly PointerEventArgs args;
        
    internal MouseMoveEventArgs(PointerEventArgs e)
    {
        args = e;
    }
        
    public override bool Handled
    {
        get => args.Handled;
        set => args.Handled = value;
    }
        
    public override KeyModifiers KeyModifiers => args.KeyModifiers;
        
    public override object? Source => args.Source;

    public override Point GetPosition(Visual? relativeTo)
    {
        return args.GetPosition(relativeTo);
    }
}
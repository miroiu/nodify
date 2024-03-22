namespace Nodify.Compatibility;

public abstract class MouseEventArgs : EventArgs
{
    public abstract bool Handled { get; set; }
        
    public abstract KeyModifiers KeyModifiers { get; }
        
    public abstract object? Source { get; }

    public abstract Point GetPosition(Visual? relativeTo);
}
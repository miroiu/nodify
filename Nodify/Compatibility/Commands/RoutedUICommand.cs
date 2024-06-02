namespace Nodify.Compatibility;

public class RoutedUICommand : RoutedCommand
{
    public string Text { get; }
    
    public RoutedUICommand(string text, string name, Type? ownerType) : base(name, ownerType)
    {
        Text = text;
    }

    public RoutedUICommand(string text, string name, Type? ownerType, InputGesture inputGesture) : base(name, ownerType, inputGesture)
    {
        Text = text;
    }

    public RoutedUICommand(string text, string name, Type? ownerType, InputGestureCollection inputGestures) : base(name, ownerType, inputGestures)
    {
        Text = text;
    }
}
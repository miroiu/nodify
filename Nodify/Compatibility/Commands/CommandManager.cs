namespace Nodify.Compatibility;

internal static class CommandManager
{
    public static Dictionary<Type, List<CommandBinding>> staticCommandBindings = new Dictionary<Type, List<CommandBinding>>();
        
    public static void RegisterClassCommandBinding(Type type, CommandBinding cmd)
    {
        if (!staticCommandBindings.ContainsKey(type))
        {
            staticCommandBindings.Add(type, new List<CommandBinding>());
        }
        staticCommandBindings[type].Add(cmd);
    }

    public static event EventHandler? RequerySuggested;
        
    public static void InvalidateRequerySuggested()
    {
        RequerySuggested?.Invoke(null, EventArgs.Empty);
    }

    static CommandManager()
    {
        InputElement.GotFocusEvent.AddClassHandler<Interactive>(GotFocusEventHandler);
        InputElement.LostFocusEvent.AddClassHandler<Interactive>(LostFocusEventHandler);
        InputElement.PointerPressedEvent.AddClassHandler<Interactive>(PointerPressedEventHandler);
        InputElement.PointerReleasedEvent.AddClassHandler<Interactive>(PointerReleasedEventHandler);
        InputElement.KeyUpEvent.AddClassHandler<Interactive>(KeyUpEventHandler);
        InputElement.KeyDownEvent.AddClassHandler<Interactive>(KeyDownEventHandler);
    }

    private static void KeyDownEventHandler(Interactive sender, KeyEventArgs e)
    {
        InvalidateRequerySuggested();
    }

    private static void KeyUpEventHandler(Interactive sender, KeyEventArgs e)
    {
        InvalidateRequerySuggested();
    }

    private static void PointerReleasedEventHandler(Interactive sender, PointerReleasedEventArgs e)
    {
        InvalidateRequerySuggested();
    }

    private static void PointerPressedEventHandler(Interactive sender, PointerPressedEventArgs e)
    {
        InvalidateRequerySuggested();
    }

    private static void GotFocusEventHandler(Interactive sender, GotFocusEventArgs e)
    {
        InvalidateRequerySuggested();
    }

    private static void LostFocusEventHandler(Interactive sender, RoutedEventArgs e)
    {
        InvalidateRequerySuggested();
    }
}
using System.Runtime.InteropServices;

namespace Nodify.Compatibility;

internal static class ApplicationCommands
{
    private static readonly KeyModifiers PlatformCommandKey = GetPlatformCommandKey();

    public static RoutedUICommand Delete { get; } = new RoutedUICommand("Delete", nameof(Delete), typeof(ApplicationCommands), new InputKeyGesture(Key.Delete));
    public static RoutedUICommand Copy { get; } = new RoutedUICommand("Copy", nameof(Copy), typeof(ApplicationCommands), new InputKeyGesture(Key.C, PlatformCommandKey));
    public static RoutedUICommand Cut { get; } = new RoutedUICommand("Cut", nameof(Cut), typeof(ApplicationCommands), new InputKeyGesture(Key.X, PlatformCommandKey));
    public static RoutedUICommand Paste { get; } = new RoutedUICommand("Paste", nameof(Paste), typeof(ApplicationCommands), new InputKeyGesture(Key.V, PlatformCommandKey));
    public static RoutedUICommand SelectAll { get; } = new RoutedUICommand("Select all", nameof(SelectAll), typeof(ApplicationCommands), new InputKeyGesture(Key.A, PlatformCommandKey));
    public static RoutedUICommand Undo { get; } = new RoutedUICommand("Undo", nameof(Undo), typeof(ApplicationCommands), new InputKeyGesture(Key.Z, PlatformCommandKey));
    public static RoutedUICommand Redo { get; } = new RoutedUICommand("Redo", nameof(Redo), typeof(ApplicationCommands), new InputKeyGesture(Key.Y, PlatformCommandKey));
    public static RoutedUICommand Find { get; } = new RoutedUICommand("Find", nameof(Find), typeof(ApplicationCommands), new InputKeyGesture(Key.F, PlatformCommandKey));
    public static RoutedUICommand Replace { get; } = new RoutedUICommand("Replace", nameof(Replace), typeof(ApplicationCommands), GetReplaceKeyGesture());
        
    private static KeyModifiers GetPlatformCommandKey()
    {            
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return KeyModifiers.Meta;
        }

        return KeyModifiers.Control;
    }

    private static InputKeyGesture GetReplaceKeyGesture()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return new InputKeyGesture(Key.F, KeyModifiers.Meta | KeyModifiers.Alt);
        }

        return new InputKeyGesture(Key.H, PlatformCommandKey);
    }
}
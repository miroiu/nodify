using System;

namespace Nodify.Compatibility;

internal class DefaultStyleKeyProperty
{
    public static void OverrideMetadata(Type control, FrameworkPropertyMetadata metadata)
    {
        // just a placeholder so that the line doesn't have to be removed to minimize conflicts
        // note: in Avalonia by default StyleOverrideKey is set to the type of the derived control (unlike WPF)
        // so you need to add StyleOverrideKey only if you want to set the type to a different class
    }
}

internal class FrameworkPropertyMetadata
{
    public FrameworkPropertyMetadata(Type type)
    {
            
    }
}
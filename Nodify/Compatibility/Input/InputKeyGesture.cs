using System;
using Avalonia.Input;

namespace Nodify.Compatibility;

internal class InputKeyGesture : InputGesture
{
    private KeyGesture keyGesture;
        
    public InputKeyGesture(Key key, KeyModifiers modifiers = KeyModifiers.None)
    {
        keyGesture = new KeyGesture(key, modifiers);
    }
        
    public override bool Matches(object targetElement, EventArgs inputEventArgs)
    {
        return keyGesture.Matches(inputEventArgs as KeyEventArgs);
    }
}
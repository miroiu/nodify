using System;
using Avalonia.Input;

namespace Nodify.Compatibility;

public class InputKeyGesture : InputGesture
{
    private KeyGesture keyGesture;
        
    public InputKeyGesture(Key key, KeyModifiers modifiers = KeyModifiers.None)
    {
        keyGesture = new KeyGesture(key, modifiers);
    }

    public InputKeyGesture(KeyGesture gesture)
    {
        keyGesture = gesture;
    }

    public override bool Matches(object targetElement, EventArgs inputEventArgs)
    {
        return keyGesture.Matches(inputEventArgs as KeyEventArgs);
    }
}
# MouseGesture Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [MouseGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseGesture) → [MouseGesture](Nodify_Interactivity_MouseGesture)  
  
Represents a mouse gesture that optionally includes a specific key press as part of the gesture.  
  
```csharp  
public sealed class MouseGesture : MouseGesture  
```  
  
## Constructors  
  
### MouseGesture(MouseAction, ModifierKeys, Key)  
  
Initializes a new instance of the [MouseGesture](Nodify_Interactivity_MouseGesture) class with the specified mouse action, modifier keys, and a specific key.  
  
```csharp  
public MouseGesture(MouseAction action, ModifierKeys modifiers, Key key);  
```  
  
**Parameters**  
  
`action` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction): The action associated with this gesture.  
  
`modifiers` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys): The modifiers associated with this gesture.  
  
`key` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key): The key required to match the gesture.  
  
### MouseGesture(MouseAction, Key)  
  
Initializes a new instance of the [MouseGesture](Nodify_Interactivity_MouseGesture) class with the specified mouse action and key.  
  
```csharp  
public MouseGesture(MouseAction action, Key key);  
```  
  
**Parameters**  
  
`action` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction): The action associated with this gesture.  
  
`key` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key): The key required to match the gesture.  
  
### MouseGesture(MouseAction, ModifierKeys)  
  
Initializes a new instance of the [MouseGesture](Nodify_Interactivity_MouseGesture) class with the specified mouse action and modifier keys.  
  
```csharp  
public MouseGesture(MouseAction action, ModifierKeys modifiers);  
```  
  
**Parameters**  
  
`action` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction): The action associated with this gesture.  
  
`modifiers` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys): The modifiers required to match the gesture.  
  
### MouseGesture(MouseAction, ModifierKeys, Boolean)  
  
```csharp  
public MouseGesture(MouseAction action, ModifierKeys modifiers, bool ignoreModifierKeysOnRelease);  
```  
  
**Parameters**  
  
`action` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction)  
  
`modifiers` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
`ignoreModifierKeysOnRelease` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### MouseGesture(MouseAction)  
  
```csharp  
public MouseGesture(MouseAction action);  
```  
  
**Parameters**  
  
`action` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction)  
  
### MouseGesture()  
  
```csharp  
public MouseGesture();  
```  
  
## Properties  
  
### IgnoreModifierKeysOnRelease  
  
Whether to ignore modifier keys when releasing the mouse button.  
  
```csharp  
public bool IgnoreModifierKeysOnRelease { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Key  
  
Gets or sets the key that must be pressed to match this gesture.  
  
```csharp  
public Key Key { get; set; }  
```  
  
**Property Value**  
  
[Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key)  
  
## Methods  
  
### Matches(Object, InputEventArgs)  
  
```csharp  
public override bool Matches(object targetElement, InputEventArgs inputEventArgs);  
```  
  
**Parameters**  
  
`targetElement` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`inputEventArgs` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  

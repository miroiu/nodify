# KeyComboGesture Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [KeyGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyGesture) → [KeyComboGesture](Nodify_Interactivity_KeyComboGesture)  
  
Represents a keyboard gesture that requires a trigger key to be held down
            before pressing a combo key. For example, press and hold Space, then press Left arrow.  
  
```csharp  
public class KeyComboGesture : KeyGesture  
```  
  
## Constructors  
  
### KeyComboGesture(Key, Key)  
  
Initializes a new instance of the [KeyComboGesture](Nodify_Interactivity_KeyComboGesture) class with the specified trigger key,
            combo key, modifiers, and display string.  
  
```csharp  
public KeyComboGesture(Key triggerKey, Key comboKey);  
```  
  
**Parameters**  
  
`triggerKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key): The key that must be pressed first.  
  
`comboKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key): The combo key pressed while the trigger key is held.  
  
### KeyComboGesture(Key, Key, ModifierKeys)  
  
```csharp  
public KeyComboGesture(Key triggerKey, Key comboKey, ModifierKeys modifiers);  
```  
  
**Parameters**  
  
`triggerKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key)  
  
`comboKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key)  
  
`modifiers` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### KeyComboGesture(Key, Key, ModifierKeys, String)  
  
```csharp  
public KeyComboGesture(Key triggerKey, Key comboKey, ModifierKeys modifiers, string displayString);  
```  
  
**Parameters**  
  
`triggerKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key)  
  
`comboKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key)  
  
`modifiers` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
`displayString` [String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
## Properties  
  
### AllowRepeatingComboKey  
  
Gets or sets a value indicating whether the combo key can be repeatedly triggered
            without releasing the trigger key.  
  
```csharp  
public bool AllowRepeatingComboKey { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### TriggerKey  
  
Gets or sets the key that must be pressed first to activate this combo gesture.  
  
```csharp  
public Key TriggerKey { get; set; }  
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
  

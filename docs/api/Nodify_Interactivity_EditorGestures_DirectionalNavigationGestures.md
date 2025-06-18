# EditorGestures.DirectionalNavigationGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures)  
  
**References:** [InputGestureRef](Nodify_Interactivity_InputGestureRef), [NodifyEditorGestures.KeyboardGestures](Nodify_Interactivity_NodifyEditorGestures_KeyboardGestures), [EditorGestures.MinimapGestures](Nodify_Interactivity_EditorGestures_MinimapGestures)  
  
```csharp  
public class DirectionalNavigationGestures  
```  
  
## Constructors  
  
### EditorGestures.DirectionalNavigationGestures(ModifierKeys)  
  
```csharp  
public DirectionalNavigationGestures(ModifierKeys modifierKeys = 0);  
```  
  
**Parameters**  
  
`modifierKeys` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### EditorGestures.DirectionalNavigationGestures(Key, ModifierKeys, Boolean)  
  
```csharp  
public DirectionalNavigationGestures(Key triggerKey, ModifierKeys modifierKeys = 0, bool repeated = false);  
```  
  
**Parameters**  
  
`triggerKey` [Key](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.Key)  
  
`modifierKeys` [ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
`repeated` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Properties  
  
### Down  
  
```csharp  
public InputGestureRef Down { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Left  
  
```csharp  
public InputGestureRef Left { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Right  
  
```csharp  
public InputGestureRef Right { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Up  
  
```csharp  
public InputGestureRef Up { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
## Methods  
  
### Apply(EditorGestures.DirectionalNavigationGestures)  
  
```csharp  
public void Apply(EditorGestures.DirectionalNavigationGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

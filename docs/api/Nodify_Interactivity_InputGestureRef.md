# InputGestureRef Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
**References:** [EditorGestures.ConnectionGestures](Nodify_Interactivity_EditorGestures_ConnectionGestures), [EditorGestures.ConnectorGestures](Nodify_Interactivity_EditorGestures_ConnectorGestures), [EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures), [EditorCommands](Nodify_EditorCommands), [EditorGestures.GroupingNodeGestures](Nodify_Interactivity_EditorGestures_GroupingNodeGestures), [InputGestureRefExtensions](Nodify_Interactivity_InputGestureRefExtensions), [EditorGestures.ItemContainerGestures](Nodify_Interactivity_EditorGestures_ItemContainerGestures), [NodifyEditorGestures.KeyboardGestures](Nodify_Interactivity_NodifyEditorGestures_KeyboardGestures), [EditorGestures.MinimapGestures](Nodify_Interactivity_EditorGestures_MinimapGestures), [EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures), [EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
An input gesture that allows changing its logic at runtime without changing its reference.
            Useful for classes that capture the object reference without the posibility of updating it. (e.g. [EditorCommands](Nodify_EditorCommands))  
  
```csharp  
public sealed class InputGestureRef : InputGesture  
```  
  
## Properties  
  
### Value  
  
The referenced gesture.  
  
```csharp  
public InputGesture Value { get; set; }  
```  
  
**Property Value**  
  
[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
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
  
### Unbind()  
  
Unbinds the current gesture.  
  
```csharp  
public void Unbind();  
```  
  

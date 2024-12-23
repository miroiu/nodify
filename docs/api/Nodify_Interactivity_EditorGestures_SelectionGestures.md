# EditorGestures.SelectionGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
**References:** [EditorGestures.ConnectionGestures](Nodify_Interactivity_EditorGestures_ConnectionGestures), [InputGestureRef](Nodify_Interactivity_InputGestureRef), [EditorGestures.ItemContainerGestures](Nodify_Interactivity_EditorGestures_ItemContainerGestures), [EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures)  
  
```csharp  
public class SelectionGestures  
```  
  
## Constructors  
  
### EditorGestures.SelectionGestures(MouseAction, Boolean)  
  
```csharp  
public SelectionGestures(MouseAction mouseAction, bool ignoreModifierKeysOnRelease);  
```  
  
**Parameters**  
  
`mouseAction` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction)  
  
`ignoreModifierKeysOnRelease` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EditorGestures.SelectionGestures(MouseAction)  
  
```csharp  
public SelectionGestures(MouseAction mouseAction);  
```  
  
**Parameters**  
  
`mouseAction` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction)  
  
### EditorGestures.SelectionGestures(Boolean)  
  
```csharp  
public SelectionGestures(bool ignoreModifierKeysOnRelease);  
```  
  
**Parameters**  
  
`ignoreModifierKeysOnRelease` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EditorGestures.SelectionGestures()  
  
```csharp  
public SelectionGestures();  
```  
  
## Fields  
  
### None  
  
```csharp  
public static SelectionGestures None;  
```  
  
**Field Value**  
  
[EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
## Properties  
  
### Append  
  
```csharp  
public InputGestureRef Append { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Cancel  
  
```csharp  
public InputGestureRef Cancel { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Invert  
  
```csharp  
public InputGestureRef Invert { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Remove  
  
```csharp  
public InputGestureRef Remove { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Replace  
  
```csharp  
public InputGestureRef Replace { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Select  
  
```csharp  
public InputGestureRef Select { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
## Methods  
  
### Apply(EditorGestures.SelectionGestures)  
  
```csharp  
public void Apply(EditorGestures.SelectionGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

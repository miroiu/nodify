# NodifyEditorGestures.KeyboardGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [NodifyEditorGestures.KeyboardGestures](Nodify_Interactivity_NodifyEditorGestures_KeyboardGestures)  
  
**References:** [EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures), [InputGestureRef](Nodify_Interactivity_InputGestureRef), [EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures)  
  
```csharp  
public class KeyboardGestures  
```  
  
## Constructors  
  
### NodifyEditorGestures.KeyboardGestures()  
  
```csharp  
public KeyboardGestures();  
```  
  
## Properties  
  
### DeselectAll  
  
```csharp  
public InputGestureRef DeselectAll { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### DragSelection  
  
```csharp  
public DirectionalNavigationGestures DragSelection { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures)  
  
### NavigateSelection  
  
```csharp  
public DirectionalNavigationGestures NavigateSelection { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures)  
  
### NextNavigationLayer  
  
```csharp  
public InputGestureRef NextNavigationLayer { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Pan  
  
```csharp  
public DirectionalNavigationGestures Pan { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures)  
  
### PrevNavigationLayer  
  
```csharp  
public InputGestureRef PrevNavigationLayer { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### ToggleSelected  
  
```csharp  
public InputGestureRef ToggleSelected { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
## Methods  
  
### Apply(NodifyEditorGestures.KeyboardGestures)  
  
```csharp  
public void Apply(NodifyEditorGestures.KeyboardGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [NodifyEditorGestures.KeyboardGestures](Nodify_Interactivity_NodifyEditorGestures_KeyboardGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

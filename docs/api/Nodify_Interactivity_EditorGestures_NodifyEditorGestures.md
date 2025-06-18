# EditorGestures.NodifyEditorGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures)  
  
**References:** [EditorGestures](Nodify_Interactivity_EditorGestures), [InputGestureRef](Nodify_Interactivity_InputGestureRef), [NodifyEditorGestures.KeyboardGestures](Nodify_Interactivity_NodifyEditorGestures_KeyboardGestures), [EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
```csharp  
public class NodifyEditorGestures  
```  
  
## Constructors  
  
### EditorGestures.NodifyEditorGestures()  
  
```csharp  
public NodifyEditorGestures();  
```  
  
## Properties  
  
### CancelAction  
  
```csharp  
public InputGestureRef CancelAction { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Cutting  
  
```csharp  
public InputGestureRef Cutting { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### FitToScreen  
  
```csharp  
public InputGestureRef FitToScreen { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Keyboard  
  
```csharp  
public KeyboardGestures Keyboard { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditorGestures.KeyboardGestures](Nodify_Interactivity_NodifyEditorGestures_KeyboardGestures)  
  
### Pan  
  
```csharp  
public InputGestureRef Pan { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### PanHorizontalModifierKey  
  
```csharp  
public ModifierKeys PanHorizontalModifierKey { get; set; }  
```  
  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### PanVerticalModifierKey  
  
```csharp  
public ModifierKeys PanVerticalModifierKey { get; set; }  
```  
  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### PanWithMouseWheel  
  
```csharp  
public bool PanWithMouseWheel { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### PushItems  
  
```csharp  
public InputGestureRef PushItems { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### ResetViewport  
  
```csharp  
public InputGestureRef ResetViewport { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### SelectAll  
  
```csharp  
public InputGestureRef SelectAll { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Selection  
  
```csharp  
public SelectionGestures Selection { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
### ZoomIn  
  
```csharp  
public InputGestureRef ZoomIn { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### ZoomModifierKey  
  
```csharp  
public ModifierKeys ZoomModifierKey { get; set; }  
```  
  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### ZoomOut  
  
```csharp  
public InputGestureRef ZoomOut { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
## Methods  
  
### Apply(EditorGestures.NodifyEditorGestures)  
  
```csharp  
public void Apply(EditorGestures.NodifyEditorGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

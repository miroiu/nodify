# EditorGestures.MinimapGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures.MinimapGestures](Nodify_Interactivity_EditorGestures_MinimapGestures)  
  
**References:** [EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures), [EditorGestures](Nodify_Interactivity_EditorGestures), [InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
```csharp  
public class MinimapGestures  
```  
  
## Constructors  
  
### EditorGestures.MinimapGestures()  
  
```csharp  
public MinimapGestures();  
```  
  
## Properties  
  
### CancelAction  
  
```csharp  
public InputGestureRef CancelAction { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### DragViewport  
  
```csharp  
public InputGestureRef DragViewport { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Pan  
  
```csharp  
public DirectionalNavigationGestures Pan { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.DirectionalNavigationGestures](Nodify_Interactivity_EditorGestures_DirectionalNavigationGestures)  
  
### ResetViewport  
  
```csharp  
public InputGestureRef ResetViewport { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
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
  
### Apply(EditorGestures.MinimapGestures)  
  
```csharp  
public void Apply(EditorGestures.MinimapGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures.MinimapGestures](Nodify_Interactivity_EditorGestures_MinimapGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

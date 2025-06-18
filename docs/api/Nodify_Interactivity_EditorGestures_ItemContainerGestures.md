# EditorGestures.ItemContainerGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures.ItemContainerGestures](Nodify_Interactivity_EditorGestures_ItemContainerGestures)  
  
**References:** [EditorGestures](Nodify_Interactivity_EditorGestures), [InputGestureRef](Nodify_Interactivity_InputGestureRef), [EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
```csharp  
public class ItemContainerGestures  
```  
  
## Constructors  
  
### EditorGestures.ItemContainerGestures()  
  
```csharp  
public ItemContainerGestures();  
```  
  
## Properties  
  
### CancelAction  
  
```csharp  
public InputGestureRef CancelAction { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Drag  
  
```csharp  
public InputGestureRef Drag { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Selection  
  
```csharp  
public SelectionGestures Selection { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
## Methods  
  
### Apply(EditorGestures.ItemContainerGestures)  
  
```csharp  
public void Apply(EditorGestures.ItemContainerGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures.ItemContainerGestures](Nodify_Interactivity_EditorGestures_ItemContainerGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

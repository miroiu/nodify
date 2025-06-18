# EditorGestures.ConnectionGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures.ConnectionGestures](Nodify_Interactivity_EditorGestures_ConnectionGestures)  
  
**References:** [EditorGestures](Nodify_Interactivity_EditorGestures), [InputGestureRef](Nodify_Interactivity_InputGestureRef), [EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
```csharp  
public class ConnectionGestures  
```  
  
## Constructors  
  
### EditorGestures.ConnectionGestures()  
  
```csharp  
public ConnectionGestures();  
```  
  
## Properties  
  
### Disconnect  
  
```csharp  
public InputGestureRef Disconnect { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
### Selection  
  
```csharp  
public SelectionGestures Selection { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.SelectionGestures](Nodify_Interactivity_EditorGestures_SelectionGestures)  
  
### Split  
  
```csharp  
public InputGestureRef Split { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](Nodify_Interactivity_InputGestureRef)  
  
## Methods  
  
### Apply(EditorGestures.ConnectionGestures)  
  
```csharp  
public void Apply(EditorGestures.ConnectionGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures.ConnectionGestures](Nodify_Interactivity_EditorGestures_ConnectionGestures)  
  
### Unbind()  
  
```csharp  
public void Unbind();  
```  
  

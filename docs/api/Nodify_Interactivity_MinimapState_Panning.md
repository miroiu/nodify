# MinimapState.Panning Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputElementState\<Minimap\>](Nodify_Interactivity_InputElementState_TElement_) → [DragState\<Minimap\>](Nodify_Interactivity_DragState_TElement_) → [MinimapState.Panning](Nodify_Interactivity_MinimapState_Panning)  
  
**References:** [Minimap](Nodify_Minimap)  
  
```csharp  
public class Panning : DragState<Minimap>  
```  
  
## Constructors  
  
### MinimapState.Panning(Minimap)  
  
```csharp  
public Panning(Minimap minimap);  
```  
  
**Parameters**  
  
`minimap` [Minimap](Nodify_Minimap)  
  
## Properties  
  
### CanBegin  
  
```csharp  
protected override bool CanBegin { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CanCancel  
  
```csharp  
protected override bool CanCancel { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsToggle  
  
```csharp  
protected override bool IsToggle { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Methods  
  
### OnBegin(InputEventArgs)  
  
```csharp  
protected override void OnBegin(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnCancel(InputEventArgs)  
  
```csharp  
protected override void OnCancel(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnEnd(InputEventArgs)  
  
```csharp  
protected override void OnEnd(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  

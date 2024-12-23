# DragState\<TElement\> Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_) → [DragState\<TElement\>](Nodify_Interactivity_DragState_TElement_)  
  
```csharp  
public abstract class DragState<TElement> : InputElementState<TElement>  
```  
  
## Constructors  
  
### DragState\<TElement\>(TElement, InputGesture)  
  
```csharp  
public DragState<TElement>(TElement element, InputGesture beginGesture);  
```  
  
**Parameters**  
  
`element` [TElement](Nodify_Interactivity_DragState_TElement__TElement)  
  
`beginGesture` [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
### DragState\<TElement\>(TElement, InputGesture, InputGesture)  
  
```csharp  
public DragState<TElement>(TElement element, InputGesture beginGesture, InputGesture cancelGesture);  
```  
  
**Parameters**  
  
`element` [TElement](Nodify_Interactivity_DragState_TElement__TElement)  
  
`beginGesture` [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
`cancelGesture` [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
## Properties  
  
### BeginGesture  
  
```csharp  
protected InputGesture BeginGesture { get; set; }  
```  
  
**Property Value**  
  
[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
### CanBegin  
  
```csharp  
protected virtual bool CanBegin { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CanCancel  
  
```csharp  
protected virtual bool CanCancel { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CancelGesture  
  
```csharp  
protected InputGesture CancelGesture { get; set; }  
```  
  
**Property Value**  
  
[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
### HasContextMenu  
  
```csharp  
protected virtual bool HasContextMenu { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsToggle  
  
```csharp  
protected virtual bool IsToggle { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### PositionElement  
  
```csharp  
protected IInputElement PositionElement { get; set; }  
```  
  
**Property Value**  
  
[IInputElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.IInputElement)  
  
## Methods  
  
### CanCaptureInput(InputEventArgs)  
  
```csharp  
protected virtual bool CanCaptureInput(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CaptureInput(InputEventArgs)  
  
```csharp  
protected virtual void CaptureInput(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### GetInitialPosition(InputEventArgs)  
  
```csharp  
protected virtual Point GetInitialPosition(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### IsInputCaptureLost(InputEventArgs)  
  
```csharp  
protected virtual bool IsInputCaptureLost(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsInputEventPressed(InputEventArgs)  
  
```csharp  
protected virtual bool IsInputEventPressed(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsInputEventReleased(InputEventArgs)  
  
```csharp  
protected virtual bool IsInputEventReleased(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OnBegin(InputEventArgs)  
  
```csharp  
protected virtual void OnBegin(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnCancel(InputEventArgs)  
  
```csharp  
protected virtual void OnCancel(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnEnd(InputEventArgs)  
  
```csharp  
protected virtual void OnEnd(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  

# InputElementState\<TElement\> Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_)  
  
**Implements:** [IInputHandler](Nodify_Interactivity_IInputHandler)  
  
```csharp  
public abstract class InputElementState<TElement> : IInputHandler  
```  
  
## Constructors  
  
### InputElementState\<TElement\>(TElement)  
  
```csharp  
protected InputElementState<TElement>(TElement element);  
```  
  
**Parameters**  
  
`element` [TElement](Nodify_Interactivity_InputElementState_TElement__TElement)  
  
## Properties  
  
### Element  
  
```csharp  
protected TElement Element { get; set; }  
```  
  
**Property Value**  
  
[TElement](Nodify_Interactivity_InputElementState_TElement__TElement)  
  
### ProcessHandledEvents  
  
```csharp  
public virtual bool ProcessHandledEvents { get; protected set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### RequiresInputCapture  
  
```csharp  
public virtual bool RequiresInputCapture { get; protected set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Methods  
  
### HandleEvent(InputEventArgs)  
  
```csharp  
public virtual void HandleEvent(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnEvent(InputEventArgs)  
  
```csharp  
protected virtual void OnEvent(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### OnKeyDown(KeyEventArgs)  
  
```csharp  
protected virtual void OnKeyDown(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
### OnKeyUp(KeyEventArgs)  
  
```csharp  
protected virtual void OnKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
### OnLostMouseCapture(MouseEventArgs)  
  
```csharp  
protected virtual void OnLostMouseCapture(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected virtual void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected virtual void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected virtual void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseWheel(MouseWheelEventArgs)  
  
```csharp  
protected virtual void OnMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  

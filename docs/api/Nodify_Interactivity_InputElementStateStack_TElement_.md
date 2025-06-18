# InputElementStateStack\<TElement\> Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
**Implements:** [IInputHandler](Nodify_Interactivity_IInputHandler)  
  
**References:** [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  
```csharp  
public class InputElementStateStack<TElement> : IInputHandler  
```  
  
## Constructors  
  
### InputElementStateStack\<TElement\>(TElement)  
  
```csharp  
public InputElementStateStack<TElement>(TElement element);  
```  
  
**Parameters**  
  
`element` [TElement](Nodify_Interactivity_InputElementStateStack_TElement__TElement)  
  
## Properties  
  
### Element  
  
```csharp  
protected TElement Element { get; set; }  
```  
  
**Property Value**  
  
[TElement](Nodify_Interactivity_InputElementStateStack_TElement__TElement)  
  
### ProcessHandledEvents  
  
```csharp  
public virtual bool ProcessHandledEvents { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### RequiresInputCapture  
  
```csharp  
public virtual bool RequiresInputCapture { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### State  
  
```csharp  
public IInputElementState<TElement> State { get; set; }  
```  
  
**Property Value**  
  
[InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  
## Methods  
  
### HandleEvent(InputEventArgs)  
  
```csharp  
public virtual void HandleEvent(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### PopAllStates()  
  
```csharp  
public void PopAllStates();  
```  
  
### PopState()  
  
```csharp  
public void PopState();  
```  
  
### PushState(InputElementStateStack\<TElement\>.IInputElementState\<TElement\>)  
  
```csharp  
public void PushState(IInputElementState<TElement> newState);  
```  
  
**Parameters**  
  
`newState` [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  

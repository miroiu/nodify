# InputElementStateStack\<TElement\>.DragState\<TElement\> Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_) → [DragState\<TElement\>](Nodify_Interactivity_DragState_TElement_) → [InputElementStateStack\<TElement\>.DragState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__DragState_TElement_)  
  
**Implements:** [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  
**References:** [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
```csharp  
public abstract class DragState<TElement> : DragState<TElement>, IInputElementState<TElement>  
```  
  
## Constructors  
  
### InputElementStateStack\<TElement\>.DragState\<TElement\>(InputElementStateStack\<TElement\>, InputGesture, InputGesture)  
  
```csharp  
public DragState<TElement>(InputElementStateStack<TElement> stack, InputGesture exitGesture, InputGesture cancelGesture);  
```  
  
**Parameters**  
  
`stack` [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
`exitGesture` [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
`cancelGesture` [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
### InputElementStateStack\<TElement\>.DragState\<TElement\>(InputElementStateStack\<TElement\>, InputGesture)  
  
```csharp  
public DragState<TElement>(InputElementStateStack<TElement> stack, InputGesture exitGesture);  
```  
  
**Parameters**  
  
`stack` [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
`exitGesture` [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
## Properties  
  
### Stack  
  
```csharp  
public InputElementStateStack<TElement> Stack { get; set; }  
```  
  
**Property Value**  
  
[InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
## Methods  
  
### Enter(InputElementStateStack\<TElement\>.IInputElementState\<TElement\>)  
  
```csharp  
public virtual void Enter(InputElementStateStack<TElement>.IInputElementState<TElement> from);  
```  
  
**Parameters**  
  
`from` [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  
### Exit()  
  
```csharp  
public virtual void Exit();  
```  
  
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
  
### PopState()  
  
```csharp  
public void PopState();  
```  
  
### PushState(InputElementStateStack\<TElement\>.IInputElementState\<TElement\>)  
  
```csharp  
public void PushState(InputElementStateStack<TElement>.IInputElementState<TElement> newState);  
```  
  
**Parameters**  
  
`newState` [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  

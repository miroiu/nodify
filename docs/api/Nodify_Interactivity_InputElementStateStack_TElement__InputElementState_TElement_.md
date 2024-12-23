# InputElementStateStack\<TElement\>.InputElementState\<TElement\> Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_) → [InputElementStateStack\<TElement\>.InputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__InputElementState_TElement_)  
  
**Implements:** [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_)  
  
**References:** [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
```csharp  
public abstract class InputElementState<TElement> : InputElementState<TElement>, IInputElementState<TElement>  
```  
  
## Constructors  
  
### InputElementStateStack\<TElement\>.InputElementState\<TElement\>(InputElementStateStack\<TElement\>)  
  
```csharp  
public InputElementState<TElement>(InputElementStateStack<TElement> stack);  
```  
  
**Parameters**  
  
`stack` [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_)  
  
## Properties  
  
### Stack  
  
```csharp  
protected InputElementStateStack<TElement> Stack { get; set; }  
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
  

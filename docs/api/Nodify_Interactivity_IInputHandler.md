# IInputHandler Interface  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Derived:** [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_), [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_), [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_), [InputElementState\<BaseConnection\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementState\<Connector\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementStateStack\<ItemContainer\>](Nodify_Interactivity_InputElementStateStack_TElement_), [InputElementState\<NodifyEditor\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_), [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_), [InputElementStateStack\<TElement\>.IInputElementState\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement__IInputElementState_TElement_), [InputElementState\<TElement\>](Nodify_Interactivity_InputElementState_TElement_), [InputElementStateStack\<TElement\>](Nodify_Interactivity_InputElementStateStack_TElement_), [InputProcessor.Shared\<TElement\>](Nodify_Interactivity_InputProcessor_Shared_TElement_), [InputElementState\<Minimap\>](Nodify_Interactivity_InputElementState_TElement_)  
  
**References:** [InputProcessor](Nodify_Interactivity_InputProcessor)  
  
Defines a contract for handling input events within an element or system.  
  
```csharp  
public interface IInputHandler  
```  
  
## Properties  
  
### ProcessHandledEvents  
  
Gets or sets a value indicating whether events that have been handled should be processed too.  
  
```csharp  
public virtual bool ProcessHandledEvents { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### RequiresInputCapture  
  
Gets a value indicating whether the handler requires input capture to remain active.  
  
```csharp  
public virtual bool RequiresInputCapture { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Methods  
  
### HandleEvent(InputEventArgs)  
  
Handles a given input event, such as a mouse or keyboard interaction.  
  
```csharp  
public virtual void HandleEvent(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs): The [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs) representing the input event.  
  

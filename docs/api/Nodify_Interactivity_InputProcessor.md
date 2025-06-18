# InputProcessor Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputProcessor](Nodify_Interactivity_InputProcessor)  
  
**Derived:** [InputProcessor.Shared\<TElement\>](Nodify_Interactivity_InputProcessor_Shared_TElement_)  
  
**References:** [Connector](Nodify_Connector), [IInputHandler](Nodify_Interactivity_IInputHandler), [InputProcessorExtensions](Nodify_Interactivity_InputProcessorExtensions), [ItemContainer](Nodify_ItemContainer), [Minimap](Nodify_Minimap), [NodifyEditor](Nodify_NodifyEditor)  
  
Processes input events and delegates them to registered handlers.  
  
```csharp  
public class InputProcessor  
```  
  
## Constructors  
  
### InputProcessor()  
  
```csharp  
public InputProcessor();  
```  
  
## Properties  
  
### RequiresInputCapture  
  
Gets a value indicating whether the processor has ongoing interactions that require input capture to remain active.  
  
```csharp  
public virtual bool RequiresInputCapture { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Methods  
  
### AddHandler(IInputHandler)  
  
Adds an input handler to the processor.  
  
```csharp  
public void AddHandler(IInputHandler handler);  
```  
  
**Parameters**  
  
`handler` [IInputHandler](Nodify_Interactivity_IInputHandler): The input handler to add.  
  
### Clear()  
  
Clears all registered handlers.  
  
```csharp  
public void Clear();  
```  
  
### ProcessEvent(InputEventArgs)  
  
Processes an input event and delegates it to the registered handlers.  
  
```csharp  
public void ProcessEvent(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs): The input event arguments to process.  
  
### RemoveHandlers()  
  
```csharp  
public void RemoveHandlers<T>();  
```  
  

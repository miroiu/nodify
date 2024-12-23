# InputProcessor.Shared\<TElement\> Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputProcessor](Nodify_Interactivity_InputProcessor) → [InputProcessor.Shared\<TElement\>](Nodify_Interactivity_InputProcessor_Shared_TElement_)  
  
**Implements:** [IInputHandler](Nodify_Interactivity_IInputHandler)  
  
```csharp  
public sealed class Shared<TElement> : InputProcessor, IInputHandler  
```  
  
## Constructors  
  
### InputProcessor.Shared\<TElement\>(TElement)  
  
```csharp  
public Shared<TElement>(TElement element);  
```  
  
**Parameters**  
  
`element` [TElement](Nodify_Interactivity_Shared_TElement__TElement)  
  
## Methods  
  
### ClearHandlerFactories()  
  
```csharp  
public static void ClearHandlerFactories();  
```  
  
### HandleEvent(InputEventArgs)  
  
```csharp  
public virtual void HandleEvent(InputEventArgs e);  
```  
  
**Parameters**  
  
`e` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
### RegisterHandlerFactory(Func\<TElement, THandler\>)  
  
```csharp  
public static void RegisterHandlerFactory<THandler>(Func<TElement, THandler> factory);  
```  
  
**Parameters**  
  
`factory` [Func\<TElement, THandler\>](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2)  
  
### RemoveHandlerFactory()  
  
```csharp  
public static void RemoveHandlerFactory<THandler>();  
```  
  
### ReplaceHandlerFactory(Func\<TElement, THandler\>)  
  
```csharp  
public static void ReplaceHandlerFactory<THandler>(Func<TElement, THandler> factory);  
```  
  
**Parameters**  
  
`factory` [Func\<TElement, THandler\>](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2)  
  

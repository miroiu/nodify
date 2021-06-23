# ResizeEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ResizeEventHandler](ResizeEventHandler)  
  
**References:** [ResizeEventArgs](ResizeEventArgs), [GroupingNode](GroupingNode)  
  
Represents the method that will handle resize related routed events.  
  
```csharp  
public delegate void ResizeEventHandler(object sender, ResizeEventArgs e);  
```  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The sender of this event.  
  
`e` [ResizeEventArgs](ResizeEventArgs): The event data.  
  

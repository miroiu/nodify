# ResizeEventHandler Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ResizeEventHandler](Nodify_Events_ResizeEventHandler)  
  
**References:** [GroupingNode](Nodify_GroupingNode), [ResizeEventArgs](Nodify_Events_ResizeEventArgs)  
  
Represents the method that will handle resize related routed events.  
  
```csharp  
public delegate void ResizeEventHandler(object sender, ResizeEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The sender of this event.  
  
`e` [ResizeEventArgs](Nodify_Events_ResizeEventArgs): The event data.  
  

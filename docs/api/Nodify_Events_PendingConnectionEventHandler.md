# PendingConnectionEventHandler Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler)  
  
**References:** [Connector](Nodify_Connector), [PendingConnection](Nodify_PendingConnection), [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs)  
  
Represents the method that will handle [PendingConnection](Nodify_PendingConnection) related routed events.  
  
```csharp  
public delegate void PendingConnectionEventHandler(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs): The event data.  
  

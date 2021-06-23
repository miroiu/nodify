# PendingConnectionEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [PendingConnectionEventHandler](PendingConnectionEventHandler)  
  
**References:** [PendingConnectionEventArgs](PendingConnectionEventArgs), [Connector](Connector), [PendingConnection](PendingConnection)  
  
Represents the method that will handle [PendingConnection](PendingConnection) related routed events.  
  
```csharp  
public delegate void PendingConnectionEventHandler(object sender, PendingConnectionEventArgs e);  
```  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [PendingConnectionEventArgs](PendingConnectionEventArgs): The event data.  
  

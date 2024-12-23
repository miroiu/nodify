# ConnectionEventHandler Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ConnectionEventHandler](Nodify_Events_ConnectionEventHandler)  
  
**References:** [BaseConnection](Nodify_BaseConnection), [ConnectionEventArgs](Nodify_Events_ConnectionEventArgs)  
  
Represents the method that will handle [BaseConnection](Nodify_BaseConnection) related routed events.  
  
```csharp  
public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ConnectionEventArgs](Nodify_Events_ConnectionEventArgs): The event data.  
  

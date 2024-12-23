# ConnectorEventHandler Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ConnectorEventHandler](Nodify_Events_ConnectorEventHandler)  
  
**References:** [Connector](Nodify_Connector), [ConnectorEventArgs](Nodify_Events_ConnectorEventArgs)  
  
Represents the method that will handle [Connector](Nodify_Connector) related routed events.  
  
```csharp  
public delegate void ConnectorEventHandler(object sender, ConnectorEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ConnectorEventArgs](Nodify_Events_ConnectorEventArgs): The event data.  
  

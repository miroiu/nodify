# ConnectorEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ConnectorEventHandler](ConnectorEventHandler)  
  
**References:** [ConnectorEventArgs](ConnectorEventArgs), [Connector](Connector)  
  
Represents the method that will handle [Connector](Connector) related routed events.  
  
```csharp  
public delegate void ConnectorEventHandler(object sender, ConnectorEventArgs e);  
```  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ConnectorEventArgs](ConnectorEventArgs): The event data.  
  

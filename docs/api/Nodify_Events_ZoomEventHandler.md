# ZoomEventHandler Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ZoomEventHandler](Nodify_Events_ZoomEventHandler)  
  
**References:** [Minimap](Nodify_Minimap), [ZoomEventArgs](Nodify_Events_ZoomEventArgs)  
  
Represents the method that will handle [Minimap.Zoom](Nodify_Minimap#zoom) routed event.  
  
```csharp  
public delegate void ZoomEventHandler(object sender, ZoomEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ZoomEventArgs](Nodify_Events_ZoomEventArgs): The event data.  
  

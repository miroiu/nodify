# ItemsMovedEventHandler Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ItemsMovedEventHandler](Nodify_Events_ItemsMovedEventHandler)  
  
**References:** [ItemsMovedEventArgs](Nodify_Events_ItemsMovedEventArgs), [NodifyEditor](Nodify_NodifyEditor)  
  
Represents a method signature used to handle the [NodifyEditor.ItemsMovedEvent](Nodify_NodifyEditor#itemsmovedevent) routed event.  
  
```csharp  
public delegate void ItemsMovedEventHandler(object sender, ItemsMovedEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The source of the event.  
  
`e` [ItemsMovedEventArgs](Nodify_Events_ItemsMovedEventArgs): The event data containing information about the moved items and their offset.  
  

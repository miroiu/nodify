# ItemsMovedEventArgs Class  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ItemsMovedEventArgs](Nodify_Events_ItemsMovedEventArgs)  
  
**References:** [ItemContainer](Nodify_ItemContainer), [ItemsMovedEventHandler](Nodify_Events_ItemsMovedEventHandler), [NodifyEditor](Nodify_NodifyEditor)  
  
Provides data for the [NodifyEditor.ItemsMovedEvent](Nodify_NodifyEditor#itemsmovedevent) routed event.  
  
```csharp  
public class ItemsMovedEventArgs : RoutedEventArgs  
```  
  
## Constructors  
  
### ItemsMovedEventArgs(IReadOnlyCollection\<Object\>, Vector)  
  
Initializes a new instance of the [ItemsMovedEventArgs](Nodify_Events_ItemsMovedEventArgs) class with the specified moved items and offset.  
  
```csharp  
public ItemsMovedEventArgs(IReadOnlyCollection<Object> items, Vector offset);  
```  
  
**Parameters**  
  
`items` [IReadOnlyCollection\<Object\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1): The collection of items that were moved.  
  
`offset` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The vector representing the distance the items were moved.  
  
## Properties  
  
### Items  
  
Gets a collection of [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext)s of the [ItemContainer](Nodify_ItemContainer)s associated with this event.  
  
```csharp  
public IReadOnlyCollection<Object> Items { get; set; }  
```  
  
**Property Value**  
  
[IReadOnlyCollection\<Object\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1)  
  
### Offset  
  
Gets or sets the vector representing the distance the items were moved.  
  
```csharp  
public Vector Offset { get; set; }  
```  
  
**Property Value**  
  
[Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
## Methods  
  
### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  

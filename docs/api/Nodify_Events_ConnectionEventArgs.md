# ConnectionEventArgs Class  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ConnectionEventArgs](Nodify_Events_ConnectionEventArgs)  
  
**References:** [BaseConnection](Nodify_BaseConnection), [ConnectionEventHandler](Nodify_Events_ConnectionEventHandler)  
  
Provides data for [BaseConnection](Nodify_BaseConnection) related routed events.  
  
```csharp  
public class ConnectionEventArgs : RoutedEventArgs  
```  
  
## Constructors  
  
### ConnectionEventArgs(Object)  
  
Initializes a new instance of the [ConnectionEventArgs](Nodify_Events_ConnectionEventArgs) class using the specified [ConnectionEventArgs.Connection](Nodify_Events_ConnectionEventArgs#connection).  
  
```csharp  
public ConnectionEventArgs(object connection);  
```  
  
**Parameters**  
  
`connection` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of a related [BaseConnection](Nodify_BaseConnection).  
  
## Properties  
  
### Connection  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of the [BaseConnection](Nodify_BaseConnection) associated with this event.  
  
```csharp  
public object Connection { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### SplitLocation  
  
Gets or sets the location where the connection should be split.  
  
```csharp  
public Point SplitLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Methods  
  
### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  

# ConnectorEventArgs Class  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ConnectorEventArgs](Nodify_Events_ConnectorEventArgs)  
  
**References:** [Connector](Nodify_Connector), [ConnectorEventHandler](Nodify_Events_ConnectorEventHandler)  
  
Provides data for [Connector](Nodify_Connector) related routed events.  
  
```csharp  
public class ConnectorEventArgs : RoutedEventArgs  
```  
  
## Constructors  
  
### ConnectorEventArgs(Object)  
  
Initializes a new instance of the [ConnectorEventArgs](Nodify_Events_ConnectorEventArgs) class using the specified [ConnectorEventArgs.Connector](Nodify_Events_ConnectorEventArgs#connector).  
  
```csharp  
public ConnectorEventArgs(object connector);  
```  
  
**Parameters**  
  
`connector` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of a related [Connector](Nodify_Connector).  
  
## Properties  
  
### Anchor  
  
Gets or sets the [Connector.Anchor](Nodify_Connector#anchor) of the [Connector](Nodify_Connector) associated with this event.  
  
```csharp  
public Point Anchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Connector  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of the [Connector](Nodify_Connector) associated with this event.  
  
```csharp  
public object Connector { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## Methods  
  
### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  

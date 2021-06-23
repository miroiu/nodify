# ConnectorEventArgs Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ConnectorEventArgs](ConnectorEventArgs)  
  
**References:** [ConnectorEventHandler](ConnectorEventHandler), [Connector](Connector)  
  
Provides data for [Connector](Connector) related routed events.  
  
```csharp  
public class ConnectorEventArgs : RoutedEventArgs  
```  
## Constructors  
  
### ConnectorEventArgs(Object)  
  
Initializes a new instance of the [ConnectorEventArgs](ConnectorEventArgs) class using the specified [ConnectorEventArgs.Connector](ConnectorEventArgs#connector).  
  
```csharp  
public ConnectorEventArgs(object connector);  
```  
**Parameters**  
  
`connector` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of a related [Connector](Connector).  
  
## Properties  
  
### Anchor  
  
Gets or sets the [Connector.Anchor](Connector#anchor) of the [Connector](Connector) associated with this event.  
  
```csharp  
public Point Anchor { get; set; }  
```  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Connector  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the [Connector](Connector) associated with this event.  
  
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
  

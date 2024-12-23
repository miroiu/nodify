# PendingConnectionEventArgs Class  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs)  
  
**References:** [Connector](Nodify_Connector), [PendingConnection](Nodify_PendingConnection), [PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler)  
  
Provides data for [PendingConnection](Nodify_PendingConnection) related routed events.  
  
```csharp  
public class PendingConnectionEventArgs : RoutedEventArgs  
```  
  
## Constructors  
  
### PendingConnectionEventArgs(Object)  
  
Initializes a new instance of the [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs) class using the specified [PendingConnectionEventArgs.SourceConnector](Nodify_Events_PendingConnectionEventArgs#sourceconnector).  
  
```csharp  
public PendingConnectionEventArgs(object sourceConnector);  
```  
  
**Parameters**  
  
`sourceConnector` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of a related [Connector](Nodify_Connector).  
  
## Properties  
  
### Anchor  
  
Gets or sets the [Connector.Anchor](Nodify_Connector#anchor) of the [Connector](Nodify_Connector) that raised this event.  
  
```csharp  
public Point Anchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Canceled  
  
Gets or sets a value that indicates whether this [PendingConnection](Nodify_PendingConnection) was cancelled.  
  
```csharp  
public bool Canceled { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OffsetX  
  
Gets or sets the distance from the [PendingConnectionEventArgs.SourceConnector](Nodify_Events_PendingConnectionEventArgs#sourceconnector) in the X axis.  
  
```csharp  
public double OffsetX { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### OffsetY  
  
Gets or sets the distance from the [PendingConnectionEventArgs.SourceConnector](Nodify_Events_PendingConnectionEventArgs#sourceconnector) in the Y axis.  
  
```csharp  
public double OffsetY { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### SourceConnector  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of the [Connector](Nodify_Connector) that started this [PendingConnection](Nodify_PendingConnection).  
  
```csharp  
public object SourceConnector { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### TargetConnector  
  
Gets or sets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of the target [Connector](Nodify_Connector) when the [PendingConnection](Nodify_PendingConnection) is completed.  
  
```csharp  
public object TargetConnector { get; set; }  
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
  

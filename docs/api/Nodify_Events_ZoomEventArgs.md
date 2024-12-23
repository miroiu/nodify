# ZoomEventArgs Class  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ZoomEventArgs](Nodify_Events_ZoomEventArgs)  
  
**References:** [Minimap](Nodify_Minimap), [ZoomEventHandler](Nodify_Events_ZoomEventHandler)  
  
Provides data for [Minimap.Zoom](Nodify_Minimap#zoom) routed event.  
  
```csharp  
public class ZoomEventArgs : RoutedEventArgs  
```  
  
## Constructors  
  
### ZoomEventArgs(Double, Point)  
  
Initializes a new instance of the [ZoomEventArgs](Nodify_Events_ZoomEventArgs) class using the specified [ZoomEventArgs.Zoom](Nodify_Events_ZoomEventArgs#zoom) and [ZoomEventArgs.Location](Nodify_Events_ZoomEventArgs#location).  
  
```csharp  
public ZoomEventArgs(double zoom, Point location);  
```  
  
**Parameters**  
  
`zoom` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Properties  
  
### Location  
  
Gets the location where the editor should zoom in.  
  
```csharp  
public Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Zoom  
  
Gets the zoom amount.  
  
```csharp  
public double Zoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Methods  
  
### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  

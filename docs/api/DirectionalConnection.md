# DirectionalConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](BaseConnection) → [DirectionalConnection](DirectionalConnection)  
  
**Derived:** [CircuitConnection](CircuitConnection)  
  
**References:** [BaseConnection](BaseConnection)  
  
Represents a line that has an arrow indicating its [BaseConnection.Direction](BaseConnection#direction).  
  
```csharp  
public class DirectionalConnection : BaseConnection  
```  
## Constructors  
  
### DirectionalConnection()  
  
```csharp  
public DirectionalConnection();  
```  
## Fields  
  
### ArrowSizeProperty  
  
```csharp  
public static DependencyProperty ArrowSizeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
## Properties  
  
### ArrowSize  
  
Gets or sets the size of the arrow head.  
  
```csharp  
public Size ArrowSize { get; set; }  
```  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### DefiningGeometry  
  
```csharp  
protected override Geometry DefiningGeometry { get; set; }  
```  
**Property Value**  
  
[Geometry](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Geometry)  
  
## Methods  
  
### DrawArrowGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target);  
```  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected virtual void DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  

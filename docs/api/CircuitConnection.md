# CircuitConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](BaseConnection) → [DirectionalConnection](DirectionalConnection) → [CircuitConnection](CircuitConnection)  
  
Represents a line that is controlled by an angle.  
  
```csharp  
public class CircuitConnection : DirectionalConnection  
```  
## Constructors  
  
### CircuitConnection()  
  
```csharp  
public CircuitConnection();  
```  
## Fields  
  
### AngleProperty  
  
```csharp  
public static DependencyProperty AngleProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### Degrees  
  
```csharp  
protected const double Degrees = 0.017453292519943295d;  
```  
**Field Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### SpacingProperty  
  
```csharp  
public static DependencyProperty SpacingProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
## Properties  
  
### Angle  
  
The angle of the connection.  
  
```csharp  
public double Angle { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Spacing  
  
The distance between the start point and the where the angle breaks.  
  
```csharp  
public double Spacing { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Methods  
  
### DrawArrowGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target);  
```  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  

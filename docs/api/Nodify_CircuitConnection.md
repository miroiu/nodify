# CircuitConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](Nodify_BaseConnection) → [LineConnection](Nodify_LineConnection) → [CircuitConnection](Nodify_CircuitConnection)  
  
Represents a line that is controlled by an angle.  
  
```csharp  
public class CircuitConnection : LineConnection  
```  
  
## Constructors  
  
### CircuitConnection()  
  
```csharp  
public CircuitConnection();  
```  
  
## Fields  
  
### Degrees  
  
```csharp  
protected const double Degrees = 0.017453292519943295d;  
```  
  
**Field Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Properties  
  
### Angle  
  
The angle of the connection in degrees.  
  
```csharp  
public double Angle { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Methods  
  
### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple\<ValueTuple\<Point, Point\>, ValueTuple\<Point, Point\>\>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)  
  
### GetTextPosition(FormattedText, Point, Point)  
  
```csharp  
protected override Point GetTextPosition(FormattedText text, Point source, Point target);  
```  
  
**Parameters**  
  
`text` [FormattedText](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FormattedText)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  

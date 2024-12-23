# LineConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](Nodify_BaseConnection) → [LineConnection](Nodify_LineConnection)  
  
**Derived:** [CircuitConnection](Nodify_CircuitConnection), [StepConnection](Nodify_StepConnection)  
  
**References:** [ConnectionDirection](Nodify_ConnectionDirection)  
  
Represents a line that has an arrow indicating its [BaseConnection.Direction](Nodify_BaseConnection#direction).  
  
```csharp  
public class LineConnection : BaseConnection  
```  
  
## Constructors  
  
### LineConnection()  
  
```csharp  
public LineConnection();  
```  
  
## Properties  
  
### CornerRadius  
  
The radius of the corners between the line segments.  
  
```csharp  
public double CornerRadius { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Methods  
  
### AddSmoothCorner(StreamGeometryContext, Point, Point, Point, Double)  
  
```csharp  
protected static void AddSmoothCorner(StreamGeometryContext context, Point start, Point corner, Point end, double radius);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`start` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`corner` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`end` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`radius` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### DrawDefaultArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected override void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](Nodify_ConnectionDirection)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
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
  
### InterpolateLine(Point, Point, Point, Point, Double)  
  
```csharp  
protected static ValueTuple<ValueTuple<Point, Point>, Point> InterpolateLine(Point p0, Point p1, Point p2, Point p3, double t);  
```  
  
**Parameters**  
  
`p0` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p1` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p2` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p3` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`t` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
**Returns**  
  
[ValueTuple\<ValueTuple\<Point, Point\>, Point\>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)  
  
### InterpolateLine(Point, Point, Point, Double)  
  
```csharp  
protected static ValueTuple<ValueTuple<Point, Point>, Point> InterpolateLine(Point p0, Point p1, Point p2, double t);  
```  
  
**Parameters**  
  
`p0` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p1` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p2` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`t` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
**Returns**  
  
[ValueTuple\<ValueTuple\<Point, Point\>, Point\>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)  
  
### InterpolateLineSegment(Point, Point, Double)  
  
```csharp  
protected static Point InterpolateLineSegment(Point p0, Point p1, double t);  
```  
  
**Parameters**  
  
`p0` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p1` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`t` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  

# Connection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](Nodify_BaseConnection) → [Connection](Nodify_Connection)  
  
**References:** [Connector](Nodify_Connector), [NodifyEditor](Nodify_NodifyEditor)  
  
Represents a cubic bezier curve.  
  
```csharp  
public class Connection : BaseConnection  
```  
  
## Constructors  
  
### Connection()  
  
```csharp  
public Connection();  
```  
  
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
  
### InterpolateCubicBezier(Point, Point, Point, Point, Double)  
  
```csharp  
protected static Point InterpolateCubicBezier(Point P0, Point P1, Point P2, Point P3, double t);  
```  
  
**Parameters**  
  
`P0` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`P1` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`P2` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`P3` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`t` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  

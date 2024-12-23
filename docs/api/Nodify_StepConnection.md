# StepConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](Nodify_BaseConnection) → [LineConnection](Nodify_LineConnection) → [StepConnection](Nodify_StepConnection)  
  
**References:** [ConnectorPosition](Nodify_ConnectorPosition)  
  
```csharp  
public class StepConnection : LineConnection  
```  
  
## Constructors  
  
### StepConnection()  
  
```csharp  
public StepConnection();  
```  
  
## Properties  
  
### SourcePosition  
  
Gets or sets the position of the source connector.  
  
```csharp  
public ConnectorPosition SourcePosition { get; set; }  
```  
  
**Property Value**  
  
[ConnectorPosition](Nodify_ConnectorPosition)  
  
### TargetPosition  
  
Gets or sets the position of the target connector.  
  
```csharp  
public ConnectorPosition TargetPosition { get; set; }  
```  
  
**Property Value**  
  
[ConnectorPosition](Nodify_ConnectorPosition)  
  
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
  

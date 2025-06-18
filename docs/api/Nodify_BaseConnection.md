# BaseConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](Nodify_BaseConnection)  
  
**Implements:** [IKeyboardFocusTarget\<FrameworkElement\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
**Derived:** [LineConnection](Nodify_LineConnection), [Connection](Nodify_Connection)  
  
**References:** [ArrowHeadEnds](Nodify_ArrowHeadEnds), [ArrowHeadShape](Nodify_ArrowHeadShape), [ConnectionDirection](Nodify_ConnectionDirection), [ConnectionEventArgs](Nodify_Events_ConnectionEventArgs), [ConnectionEventHandler](Nodify_Events_ConnectionEventHandler), [ConnectionOffsetMode](Nodify_ConnectionOffsetMode), [CuttingLine](Nodify_CuttingLine), [ConnectionState.Disconnect](Nodify_Interactivity_ConnectionState_Disconnect), [NodifyEditor](Nodify_NodifyEditor), [ConnectionState.Split](Nodify_Interactivity_ConnectionState_Split)  
  
Represents the base class for shapes that are drawn from a [BaseConnection.Source](Nodify_BaseConnection#source) point to a [BaseConnection.Target](Nodify_BaseConnection#target) point.  
  
```csharp  
public abstract class BaseConnection : Shape, IKeyboardFocusTarget<FrameworkElement>  
```  
  
## Constructors  
  
### BaseConnection()  
  
```csharp  
protected BaseConnection();  
```  
  
## Fields  
  
### ZeroVector  
  
Gets a vector that has its coordinates set to 0.  
  
```csharp  
protected static Vector ZeroVector;  
```  
  
**Field Value**  
  
[Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
## Properties  
  
### ArrowEnds  
  
Gets or sets the arrowhead ends.  
  
```csharp  
public ArrowHeadEnds ArrowEnds { get; set; }  
```  
  
**Property Value**  
  
[ArrowHeadEnds](Nodify_ArrowHeadEnds)  
  
### ArrowShape  
  
Gets or sets the arrowhead ends.  
  
```csharp  
public ArrowHeadShape ArrowShape { get; set; }  
```  
  
**Property Value**  
  
[ArrowHeadShape](Nodify_ArrowHeadShape)  
  
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
  
### Direction  
  
Gets or sets the direction in which this connection is flowing.  
  
```csharp  
public ConnectionDirection Direction { get; set; }  
```  
  
**Property Value**  
  
[ConnectionDirection](Nodify_ConnectionDirection)  
  
### DirectionalArrowsAnimationDuration  
  
Gets or sets the duration in seconds of a directional arrow flowing from [BaseConnection.Source](Nodify_BaseConnection#source) to [BaseConnection.Target](Nodify_BaseConnection#target).  
  
```csharp  
public double DirectionalArrowsAnimationDuration { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### DirectionalArrowsCount  
  
Gets or sets the number of arrows to be drawn on the line in the direction of the connection (see [BaseConnection.Direction](Nodify_BaseConnection#direction)).  
  
```csharp  
public uint DirectionalArrowsCount { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### DirectionalArrowsOffset  
  
Gets or sets the offset of the arrows drawn by the [BaseConnection.DirectionalArrowsCount](Nodify_BaseConnection#directionalarrowscount) (value is clamped between 0 and 1).  
  
```csharp  
public double DirectionalArrowsOffset { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### DisconnectCommand  
  
Removes this connection. Triggered by Nodify.Interactivity.EditorGestures.ConnectionGestures.Disconnect gesture.
            Parameter is the location where the disconnect ocurred.  
  
```csharp  
public ICommand DisconnectCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### FocusVisualPadding  
  
The space between the focus visual and the connection geometry.  
  
```csharp  
public double FocusVisualPadding { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### FocusVisualPen  
  
The pen used to render the focus visual.  
  
```csharp  
public Pen FocusVisualPen { get; set; }  
```  
  
**Property Value**  
  
[Pen](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Pen)  
  
### FocusVisualPenKey  
  
The key used to retrieve the [BaseConnection.FocusVisualPen](Nodify_BaseConnection#focusvisualpen) resource.  
  
```csharp  
public static ResourceKey FocusVisualPenKey { get; set; }  
```  
  
**Property Value**  
  
[ResourceKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.ResourceKey)  
  
### FontFamily  
  
```csharp  
public FontFamily FontFamily { get; set; }  
```  
  
**Property Value**  
  
[FontFamily](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FontFamily)  
  
### FontSize  
  
```csharp  
public double FontSize { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### FontStretch  
  
```csharp  
public FontStretch FontStretch { get; set; }  
```  
  
**Property Value**  
  
[FontStretch](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FontStretch)  
  
### FontStyle  
  
```csharp  
public FontStyle FontStyle { get; set; }  
```  
  
**Property Value**  
  
[FontStyle](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FontStyle)  
  
### FontWeight  
  
```csharp  
public FontWeight FontWeight { get; set; }  
```  
  
**Property Value**  
  
[FontWeight](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FontWeight)  
  
### Foreground  
  
The brush used to render the [BaseConnection.Text](Nodify_BaseConnection#text).  
  
```csharp  
public Brush Foreground { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### HasContextMenu  
  
Gets a value indicating whether the connection has a context menu.  
  
```csharp  
public bool HasContextMenu { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### HasCustomContextMenu  
  
Gets or sets a value indicating whether the connection uses a custom context menu.  
  
```csharp  
public bool HasCustomContextMenu { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsAnimatingDirectionalArrows  
  
Gets or sets whether the directional arrows should be flowing through the connection wire.  
  
```csharp  
public bool IsAnimatingDirectionalArrows { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OutlineBrush  
  
The brush used to render the outline.  
  
```csharp  
public Brush OutlineBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### OutlineThickness  
  
The thickness of the outline.  
  
```csharp  
public double OutlineThickness { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### PrioritizeBaseConnectionForSelection  
  
Whether to prioritize controls of type [BaseConnection](Nodify_BaseConnection) inside custom connections (connection wrappers) 
            when setting the [BaseConnection.IsSelectableProperty](Nodify_BaseConnection#isselectableproperty) and [BaseConnection.IsSelectedProperty](Nodify_BaseConnection#isselectedproperty) attached properties.  
  
```csharp  
public static bool PrioritizeBaseConnectionForSelection { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Source  
  
Gets or sets the start point of this connection.  
  
```csharp  
public Point Source { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### SourceOffset  
  
Gets or sets the offset from the [BaseConnection.Source](Nodify_BaseConnection#source) point.  
  
```csharp  
public Size SourceOffset { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### SourceOffsetMode  
  
Gets or sets the [ConnectionOffsetMode](Nodify_ConnectionOffsetMode) to apply to the [BaseConnection.Source](Nodify_BaseConnection#source) when drawing the connection.  
  
```csharp  
public ConnectionOffsetMode SourceOffsetMode { get; set; }  
```  
  
**Property Value**  
  
[ConnectionOffsetMode](Nodify_ConnectionOffsetMode)  
  
### SourceOrientation  
  
Gets or sets the orientation in which this connection is flowing.  
  
```csharp  
public Orientation SourceOrientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### Spacing  
  
The distance between the start point and the where the angle breaks.  
  
```csharp  
public double Spacing { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### SplitCommand  
  
Splits the connection. Triggered by Nodify.Interactivity.EditorGestures.ConnectionGestures.Split gesture.
            Parameter is the location where the splitting ocurred.  
  
```csharp  
public ICommand SplitCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Target  
  
Gets or sets the end point of this connection.  
  
```csharp  
public Point Target { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### TargetOffset  
  
Gets or sets the offset from the [BaseConnection.Target](Nodify_BaseConnection#target) point.  
  
```csharp  
public Size TargetOffset { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### TargetOffsetMode  
  
Gets or sets the [ConnectionOffsetMode](Nodify_ConnectionOffsetMode) to apply to the [BaseConnection.Target](Nodify_BaseConnection#target) when drawing the connection.  
  
```csharp  
public ConnectionOffsetMode TargetOffsetMode { get; set; }  
```  
  
**Property Value**  
  
[ConnectionOffsetMode](Nodify_ConnectionOffsetMode)  
  
### TargetOrientation  
  
Gets or sets the orientation in which this connection is flowing.  
  
```csharp  
public Orientation TargetOrientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### Text  
  
Gets or sets the text contents of the [BaseConnection](Nodify_BaseConnection).  
  
```csharp  
public string Text { get; set; }  
```  
  
**Property Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
## Methods  
  
### DrawArrowGeometry(StreamGeometryContext, Point, Point, ConnectionDirection, ArrowHeadShape, Orientation)  
  
```csharp  
protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, ArrowHeadShape shape = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](Nodify_ConnectionDirection)  
  
`shape` [ArrowHeadShape](Nodify_ArrowHeadShape)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### DrawDefaultArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected virtual void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](Nodify_ConnectionDirection)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### DrawDirectionalArrowheadGeometry(StreamGeometryContext, Vector, Point)  
  
```csharp  
protected virtual void DrawDirectionalArrowheadGeometry(StreamGeometryContext context, Vector direction, Point location);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`direction` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected virtual void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### DrawEllipseArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected virtual void DrawEllipseArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](Nodify_ConnectionDirection)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected virtual ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple\<ValueTuple\<Point, Point\>, ValueTuple\<Point, Point\>\>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)  
  
### DrawRectangleArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected virtual void DrawRectangleArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](Nodify_ConnectionDirection)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### GetIsSelectable(UIElement)  
  
```csharp  
public static bool GetIsSelectable(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### GetIsSelected(UIElement)  
  
```csharp  
public static bool GetIsSelected(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### GetOffset()  
  
Gets the resulting offset after applying the [BaseConnection.SourceOffsetMode](Nodify_BaseConnection#sourceoffsetmode).  
  
```csharp  
protected virtual ValueTuple<Vector, Vector> GetOffset();  
```  
  
**Returns**  
  
[ValueTuple\<Vector, Vector\>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple-2)  
  
### GetTextPosition(FormattedText, Point, Point)  
  
```csharp  
protected virtual Point GetTextPosition(FormattedText text, Point source, Point target);  
```  
  
**Parameters**  
  
`text` [FormattedText](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FormattedText)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### OnKeyDown(KeyEventArgs)  
  
```csharp  
protected override void OnKeyDown(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
### OnKeyUp(KeyEventArgs)  
  
```csharp  
protected override void OnKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
### OnLostMouseCapture(MouseEventArgs)  
  
```csharp  
protected override void OnLostMouseCapture(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseWheel(MouseWheelEventArgs)  
  
```csharp  
protected override void OnMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  
### OnRender(DrawingContext)  
  
```csharp  
protected override void OnRender(DrawingContext drawingContext);  
```  
  
**Parameters**  
  
`drawingContext` [DrawingContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.DrawingContext)  
  
### Remove()  
  
Removes the connection.  
  
```csharp  
public void Remove();  
```  
  
### SetIsSelectable(UIElement, Boolean)  
  
```csharp  
public static void SetIsSelectable(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### SetIsSelected(UIElement, Boolean)  
  
```csharp  
public static void SetIsSelected(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### SplitAtLocation(Point)  
  
Splits the connection at the specified location.  
  
```csharp  
public void SplitAtLocation(Point splitLocation);  
```  
  
**Parameters**  
  
`splitLocation` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point) where the connection should be split.  
  
### StartAnimation(Double)  
  
Starts animating the directional arrows.  
  
```csharp  
public void StartAnimation(double duration = 1.5d);  
```  
  
**Parameters**  
  
`duration` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The duration for moving an arrowhead from [BaseConnection.Source](Nodify_BaseConnection#source) to [BaseConnection.Target](Nodify_BaseConnection#target).  
  
### StopAnimation()  
  
Stops the animation started by Nodify.BaseConnection.StartAnimation(System.Double)  
  
```csharp  
public void StopAnimation();  
```  
  
## Events  
  
### Disconnect  
  
Triggered by the Nodify.Interactivity.EditorGestures.ConnectionGestures.Disconnect gesture.  
  
```csharp  
public event ConnectionEventHandler Disconnect;  
```  
  
**Event Type**  
  
[ConnectionEventHandler](Nodify_Events_ConnectionEventHandler)  
  
### Split  
  
Triggered by the Nodify.Interactivity.EditorGestures.ConnectionGestures.Split gesture.  
  
```csharp  
public event ConnectionEventHandler Split;  
```  
  
**Event Type**  
  
[ConnectionEventHandler](Nodify_Events_ConnectionEventHandler)  
  

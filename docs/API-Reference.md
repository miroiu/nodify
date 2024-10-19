- [Alignment Enum](#alignment-enum)  
- [AllGestures Class](#allgestures-class)  
- [AnyGesture Class](#anygesture-class)  
- [ArrowHeadEnds Enum](#arrowheadends-enum)  
- [ArrowHeadShape Enum](#arrowheadshape-enum)  
- [BaseConnection Class](#baseconnection-class)  
- [BoxValue Class](#boxvalue-class)  
- [CircuitConnection Class](#circuitconnection-class)  
- [Connection Class](#connection-class)  
- [ConnectionDirection Enum](#connectiondirection-enum)  
- [ConnectionEventArgs Class](#connectioneventargs-class)  
- [ConnectionEventHandler Delegate](#connectioneventhandler-delegate)  
- [ConnectionGestures Class](#connectiongestures-class)  
- [ConnectionOffsetMode Enum](#connectionoffsetmode-enum)  
- [Connector Class](#connector-class)  
- [ConnectorEventArgs Class](#connectoreventargs-class)  
- [ConnectorEventHandler Delegate](#connectoreventhandler-delegate)  
- [ConnectorGestures Class](#connectorgestures-class)  
- [ConnectorPosition Enum](#connectorposition-enum)  
- [ContainerDefaultState Class](#containerdefaultstate-class)  
- [ContainerDraggingState Class](#containerdraggingstate-class)  
- [ContainerState Class](#containerstate-class)  
- [CuttingLine Class](#cuttingline-class)  
- [DecoratorContainer Class](#decoratorcontainer-class)  
- [EditorCommands Class](#editorcommands-class)  
- [EditorCuttingState Class](#editorcuttingstate-class)  
- [EditorDefaultState Class](#editordefaultstate-class)  
- [EditorGestures Class](#editorgestures-class)  
- [EditorPanningState Class](#editorpanningstate-class)  
- [EditorSelectingState Class](#editorselectingstate-class)  
- [EditorState Class](#editorstate-class)  
- [GeneratedInternalTypeHelper Class](#generatedinternaltypehelper-class)  
- [GroupingMovementMode Enum](#groupingmovementmode-enum)  
- [GroupingNode Class](#groupingnode-class)  
- [GroupingNodeGestures Class](#groupingnodegestures-class)  
- [INodifyCanvasItem Interface](#inodifycanvasitem-interface)  
- [InputGestureRef Class](#inputgestureref-class)  
- [ItemContainer Class](#itemcontainer-class)  
- [ItemContainerGestures Class](#itemcontainergestures-class)  
- [KnotNode Class](#knotnode-class)  
- [LineConnection Class](#lineconnection-class)  
- [Match Enum](#match-enum)  
- [Minimap Class](#minimap-class)  
- [MinimapGestures Class](#minimapgestures-class)  
- [MinimapItem Class](#minimapitem-class)  
- [MultiGesture Class](#multigesture-class)  
- [Node Class](#node-class)  
- [NodeInput Class](#nodeinput-class)  
- [NodeOutput Class](#nodeoutput-class)  
- [NodifyCanvas Class](#nodifycanvas-class)  
- [NodifyEditor Class](#nodifyeditor-class)  
- [NodifyEditorGestures Class](#nodifyeditorgestures-class)  
- [PendingConnection Class](#pendingconnection-class)  
- [PendingConnectionEventArgs Class](#pendingconnectioneventargs-class)  
- [PendingConnectionEventHandler Delegate](#pendingconnectioneventhandler-delegate)  
- [PreviewLocationChanged Delegate](#previewlocationchanged-delegate)  
- [ResizeEventArgs Class](#resizeeventargs-class)  
- [ResizeEventHandler Delegate](#resizeeventhandler-delegate)  
- [SelectionGestures Class](#selectiongestures-class)  
- [SelectionHelper Class](#selectionhelper-class)  
- [SelectionType Enum](#selectiontype-enum)  
- [StateNode Class](#statenode-class)  
- [StepConnection Class](#stepconnection-class)  
- [ZoomEventArgs Class](#zoomeventargs-class)  
- [ZoomEventHandler Delegate](#zoomeventhandler-delegate)  
  
## Alignment Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
```csharp  
public enum Alignment  
```  
  
### Fields  
  
#### Bottom  
  
```csharp  
Bottom = 2;  
```  
  
#### Center  
  
```csharp  
Center = 5;  
```  
  
#### Left  
  
```csharp  
Left = 1;  
```  
  
#### Middle  
  
```csharp  
Middle = 4;  
```  
  
#### Right  
  
```csharp  
Right = 3;  
```  
  
#### Top  
  
```csharp  
Top = 0;  
```  
  
## AllGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [MultiGesture](#multigesture-class) → [AllGestures](#allgestures-class)  
  
```csharp  
public sealed class AllGestures : MultiGesture  
```  
  
### Constructors  
  
#### AllGestures(InputGesture[])  
  
```csharp  
public AllGestures(InputGesture[] gestures);  
```  
  
**Parameters**  
  
`gestures` [InputGesture[]](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture[])  
  
## AnyGesture Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [MultiGesture](#multigesture-class) → [AnyGesture](#anygesture-class)  
  
```csharp  
public sealed class AnyGesture : MultiGesture  
```  
  
### Constructors  
  
#### AnyGesture(InputGesture[])  
  
```csharp  
public AnyGesture(InputGesture[] gestures);  
```  
  
**Parameters**  
  
`gestures` [InputGesture[]](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture[])  
  
## ArrowHeadEnds Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [BaseConnection](#baseconnection-class)  
  
The end at which the arrow head is drawn.  
  
```csharp  
public enum ArrowHeadEnds  
```  
  
### Fields  
  
#### Both  
  
Arrow heads at both ends.  
  
```csharp  
Both = 2;  
```  
  
#### End  
  
Arrow head at end.  
  
```csharp  
End = 1;  
```  
  
#### None  
  
No arrow head.  
  
```csharp  
None = 3;  
```  
  
#### Start  
  
Arrow head at start.  
  
```csharp  
Start = 0;  
```  
  
## ArrowHeadShape Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [BaseConnection](#baseconnection-class)  
  
The shape of the arrowhead.  
  
```csharp  
public enum ArrowHeadShape  
```  
  
### Fields  
  
#### Arrowhead  
  
The default arrowhead.  
  
```csharp  
Arrowhead = 0;  
```  
  
#### Ellipse  
  
An ellipse.  
  
```csharp  
Ellipse = 1;  
```  
  
#### Rectangle  
  
A rectangle.  
  
```csharp  
Rectangle = 2;  
```  
  
## BaseConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class)  
  
**Derived:** [LineConnection](#lineconnection-class), [Connection](#connection-class)  
  
**References:** [ConnectionEventHandler](#connectioneventhandler-delegate), [ConnectionDirection](#connectiondirection-enum), [ArrowHeadShape](#arrowheadshape-enum), [ConnectionOffsetMode](#connectionoffsetmode-enum), [ArrowHeadEnds](#arrowheadends-enum), [CuttingLine](#cuttingline-class), [LineConnection](#lineconnection-class), [ConnectionEventArgs](#connectioneventargs-class), [NodifyEditor](#nodifyeditor-class)  
  
Represents the base class for shapes that are drawn from a [BaseConnection.Source](#baseconnection-class#source) point to a [BaseConnection.Target](#baseconnection-class#target) point.  
  
```csharp  
public abstract class BaseConnection : Shape  
```  
  
### Constructors  
  
#### BaseConnection()  
  
```csharp  
protected BaseConnection();  
```  
  
### Fields  
  
#### ZeroVector  
  
Gets a vector that has its coordinates set to 0.  
  
```csharp  
protected static Vector ZeroVector;  
```  
  
**Field Value**  
  
[Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
### Properties  
  
#### ArrowEnds  
  
Gets or sets the arrowhead ends.  
  
```csharp  
public ArrowHeadEnds ArrowEnds { get; set; }  
```  
  
**Property Value**  
  
[ArrowHeadEnds](#arrowheadends-enum)  
  
#### ArrowShape  
  
Gets or sets the arrowhead ends.  
  
```csharp  
public ArrowHeadShape ArrowShape { get; set; }  
```  
  
**Property Value**  
  
[ArrowHeadShape](#arrowheadshape-enum)  
  
#### ArrowSize  
  
Gets or sets the size of the arrow head.  
  
```csharp  
public Size ArrowSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### DefiningGeometry  
  
```csharp  
protected override Geometry DefiningGeometry { get; set; }  
```  
  
**Property Value**  
  
[Geometry](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Geometry)  
  
#### Direction  
  
Gets or sets the direction in which this connection is flowing.  
  
```csharp  
public ConnectionDirection Direction { get; set; }  
```  
  
**Property Value**  
  
[ConnectionDirection](#connectiondirection-enum)  
  
#### DirectionalArrowsAnimationDuration  
  
Gets or sets the duration in seconds of a directional arrow flowing from [BaseConnection.Source](#baseconnection-class#source) to [BaseConnection.Target](#baseconnection-class#target).  
  
```csharp  
public double DirectionalArrowsAnimationDuration { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### DirectionalArrowsCount  
  
Gets or sets the number of arrows to be drawn on the line in the direction of the connection (see [BaseConnection.Direction](#baseconnection-class#direction)).  
  
```csharp  
public uint DirectionalArrowsCount { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
#### DirectionalArrowsOffset  
  
Gets or sets the offset of the arrows drawn by the [BaseConnection.DirectionalArrowsCount](#baseconnection-class#directionalarrowscount) (value is clamped between 0 and 1).  
  
```csharp  
public double DirectionalArrowsOffset { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### DisconnectCommand  
  
Removes this connection. Triggered by Nodify.EditorGestures.ConnectionGestures.Disconnect gesture.
            Parameter is the location where the disconnect ocurred.  
  
```csharp  
public ICommand DisconnectCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### FontFamily  
  
```csharp  
public FontFamily FontFamily { get; set; }  
```  
  
**Property Value**  
  
[FontFamily](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FontFamily)  
  
#### FontSize  
  
```csharp  
public double FontSize { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### FontStretch  
  
```csharp  
public FontStretch FontStretch { get; set; }  
```  
  
**Property Value**  
  
[FontStretch](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FontStretch)  
  
#### FontStyle  
  
```csharp  
public FontStyle FontStyle { get; set; }  
```  
  
**Property Value**  
  
[FontStyle](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FontStyle)  
  
#### FontWeight  
  
```csharp  
public FontWeight FontWeight { get; set; }  
```  
  
**Property Value**  
  
[FontWeight](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FontWeight)  
  
#### Foreground  
  
The brush used to render the [BaseConnection.Text](#baseconnection-class#text).  
  
```csharp  
public Brush Foreground { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### IsAnimatingDirectionalArrows  
  
Gets or sets whether the directional arrows should be flowing through the connection wire.  
  
```csharp  
public bool IsAnimatingDirectionalArrows { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OutlineBrush  
  
The brush used to render the outline.  
  
```csharp  
public Brush OutlineBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### OutlineThickness  
  
The thickness of the outline.  
  
```csharp  
public double OutlineThickness { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### PrioritizeBaseConnectionForSelection  
  
Whether to prioritize controls of type [BaseConnection](#baseconnection-class) inside custom connections (connection wrappers) 
            when setting the [BaseConnection.IsSelectableProperty](#baseconnection-class#isselectableproperty) and [BaseConnection.IsSelectedProperty](#baseconnection-class#isselectedproperty) attached properties.  
  
```csharp  
public static bool PrioritizeBaseConnectionForSelection { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### Source  
  
Gets or sets the start point of this connection.  
  
```csharp  
public Point Source { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### SourceOffset  
  
Gets or sets the offset from the [BaseConnection.Source](#baseconnection-class#source) point.  
  
```csharp  
public Size SourceOffset { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### SourceOffsetMode  
  
Gets or sets the [ConnectionOffsetMode](#connectionoffsetmode-enum) to apply to the [BaseConnection.Source](#baseconnection-class#source) when drawing the connection.  
  
```csharp  
public ConnectionOffsetMode SourceOffsetMode { get; set; }  
```  
  
**Property Value**  
  
[ConnectionOffsetMode](#connectionoffsetmode-enum)  
  
#### SourceOrientation  
  
Gets or sets the orientation in which this connection is flowing.  
  
```csharp  
public Orientation SourceOrientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### Spacing  
  
The distance between the start point and the where the angle breaks.  
  
```csharp  
public double Spacing { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### SplitCommand  
  
Splits the connection. Triggered by Nodify.EditorGestures.ConnectionGestures.Split gesture.
            Parameter is the location where the splitting ocurred.  
  
```csharp  
public ICommand SplitCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### Target  
  
Gets or sets the end point of this connection.  
  
```csharp  
public Point Target { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### TargetOffset  
  
Gets or sets the offset from the [BaseConnection.Target](#baseconnection-class#target) point.  
  
```csharp  
public Size TargetOffset { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### TargetOffsetMode  
  
Gets or sets the [ConnectionOffsetMode](#connectionoffsetmode-enum) to apply to the [BaseConnection.Target](#baseconnection-class#target) when drawing the connection.  
  
```csharp  
public ConnectionOffsetMode TargetOffsetMode { get; set; }  
```  
  
**Property Value**  
  
[ConnectionOffsetMode](#connectionoffsetmode-enum)  
  
#### TargetOrientation  
  
Gets or sets the orientation in which this connection is flowing.  
  
```csharp  
public Orientation TargetOrientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### Text  
  
Gets or sets the text contents of the [BaseConnection](#baseconnection-class).  
  
```csharp  
public string Text { get; set; }  
```  
  
**Property Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### Methods  
  
#### DrawArrowGeometry(StreamGeometryContext, Point, Point, ConnectionDirection, ArrowHeadShape, Orientation)  
  
```csharp  
protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, ArrowHeadShape shape = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](#connectiondirection-enum)  
  
`shape` [ArrowHeadShape](#arrowheadshape-enum)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### DrawDefaultArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected virtual void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](#connectiondirection-enum)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### DrawDirectionalArrowheadGeometry(StreamGeometryContext, Vector, Point)  
  
```csharp  
protected virtual void DrawDirectionalArrowheadGeometry(StreamGeometryContext context, Vector direction, Point location);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`direction` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected virtual void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### DrawEllipseArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected virtual void DrawEllipseArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](#connectiondirection-enum)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected virtual ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)  
  
#### DrawRectangleArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected virtual void DrawRectangleArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](#connectiondirection-enum)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### GetIsSelectable(UIElement)  
  
```csharp  
public static bool GetIsSelectable(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### GetIsSelected(UIElement)  
  
```csharp  
public static bool GetIsSelected(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### GetOffset()  
  
Gets the resulting offset after applying the [BaseConnection.SourceOffsetMode](#baseconnection-class#sourceoffsetmode).  
  
```csharp  
protected virtual ValueTuple<Vector, Vector> GetOffset();  
```  
  
**Returns**  
  
[ValueTuple<Vector, Vector>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)  
  
#### GetTextPosition(FormattedText, Point, Point)  
  
```csharp  
protected virtual Point GetTextPosition(FormattedText text, Point source, Point target);  
```  
  
**Parameters**  
  
`text` [FormattedText](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FormattedText)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnRender(DrawingContext)  
  
```csharp  
protected override void OnRender(DrawingContext drawingContext);  
```  
  
**Parameters**  
  
`drawingContext` [DrawingContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.DrawingContext)  
  
#### SetIsSelectable(UIElement, Boolean)  
  
```csharp  
public static void SetIsSelectable(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### SetIsSelected(UIElement, Boolean)  
  
```csharp  
public static void SetIsSelected(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### StartAnimation(Double)  
  
Starts animating the directional arrows.  
  
```csharp  
public void StartAnimation(double duration = 1.5d);  
```  
  
**Parameters**  
  
`duration` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The duration for moving an arrowhead from [BaseConnection.Source](#baseconnection-class#source) to [BaseConnection.Target](#baseconnection-class#target).  
  
#### StopAnimation()  
  
Stops the animation started by Nodify.BaseConnection.StartAnimation(System.Double)  
  
```csharp  
public void StopAnimation();  
```  
  
### Events  
  
#### Disconnect  
  
Triggered by the Nodify.EditorGestures.ConnectionGestures.Disconnect gesture.  
  
```csharp  
public event ConnectionEventHandler Disconnect;  
```  
  
**Event Type**  
  
[ConnectionEventHandler](#connectioneventhandler-delegate)  
  
#### Split  
  
Triggered by the Nodify.EditorGestures.ConnectionGestures.Split gesture.  
  
```csharp  
public event ConnectionEventHandler Split;  
```  
  
**Event Type**  
  
[ConnectionEventHandler](#connectioneventhandler-delegate)  
  
## BoxValue Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [BoxValue](#boxvalue-class)  
  
```csharp  
public static class BoxValue  
```  
  
### Fields  
  
#### ArrowSize  
  
```csharp  
public static object ArrowSize;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### ConnectionOffset  
  
```csharp  
public static object ConnectionOffset;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Double0  
  
```csharp  
public static object Double0;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Double1  
  
```csharp  
public static object Double1;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Double1000  
  
```csharp  
public static object Double1000;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Double2  
  
```csharp  
public static object Double2;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Double45  
  
```csharp  
public static object Double45;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Double5  
  
```csharp  
public static object Double5;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### DoubleHalf  
  
```csharp  
public static object DoubleHalf;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### False  
  
```csharp  
public static object False;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Int0  
  
```csharp  
public static object Int0;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Int1  
  
```csharp  
public static object Int1;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Point  
  
```csharp  
public static object Point;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Rect  
  
```csharp  
public static object Rect;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Size  
  
```csharp  
public static object Size;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Thickness2  
  
```csharp  
public static object Thickness2;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### True  
  
```csharp  
public static object True;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### UInt0  
  
```csharp  
public static object UInt0;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### UInt1  
  
```csharp  
public static object UInt1;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## CircuitConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class) → [LineConnection](#lineconnection-class) → [CircuitConnection](#circuitconnection-class)  
  
Represents a line that is controlled by an angle.  
  
```csharp  
public class CircuitConnection : LineConnection  
```  
  
### Constructors  
  
#### CircuitConnection()  
  
```csharp  
public CircuitConnection();  
```  
  
### Fields  
  
#### Degrees  
  
```csharp  
protected const double Degrees = 0.017453292519943295d;  
```  
  
**Field Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Properties  
  
#### Angle  
  
The angle of the connection in degrees.  
  
```csharp  
public double Angle { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Methods  
  
#### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)
  
#### GetTextPosition(FormattedText, Point, Point)  
  
```csharp  
protected override Point GetTextPosition(FormattedText text, Point source, Point target);  
```  
  
**Parameters**  
  
`text` [FormattedText](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FormattedText)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Connection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class) → [Connection](#connection-class)  
  
**References:** [Connector](#connector-class), [NodifyEditor](#nodifyeditor-class)  
  
Represents a cubic bezier curve.  
  
```csharp  
public class Connection : BaseConnection  
```  
  
### Constructors  
  
#### Connection()  
  
```csharp  
public Connection();  
```  
  
### Methods  
  
#### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)
  
#### GetTextPosition(FormattedText, Point, Point)  
  
```csharp  
protected override Point GetTextPosition(FormattedText text, Point source, Point target);  
```  
  
**Parameters**  
  
`text` [FormattedText](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FormattedText)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### InterpolateCubicBezier(Point, Point, Point, Point, Double)  
  
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
  
## ConnectionDirection Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [BaseConnection](#baseconnection-class), [LineConnection](#lineconnection-class), [PendingConnection](#pendingconnection-class)  
  
The direction in which a connection is oriented.  
  
```csharp  
public enum ConnectionDirection  
```  
  
### Fields  
  
#### Backward  
  
From [BaseConnection.Target](#baseconnection-class#target) to [BaseConnection.Source](#baseconnection-class#source).  
  
```csharp  
Backward = 1;  
```  
  
#### Forward  
  
From [BaseConnection.Source](#baseconnection-class#source) to [BaseConnection.Target](#baseconnection-class#target).  
  
```csharp  
Forward = 0;  
```  
  
## ConnectionEventArgs Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ConnectionEventArgs](#connectioneventargs-class)  
  
**References:** [ConnectionEventHandler](#connectioneventhandler-delegate), [BaseConnection](#baseconnection-class)  
  
Provides data for [BaseConnection](#baseconnection-class) related routed events.  
  
```csharp  
public class ConnectionEventArgs : RoutedEventArgs  
```  
  
### Constructors  
  
#### ConnectionEventArgs(Object)  
  
Initializes a new instance of the [ConnectionEventArgs](#connectioneventargs-class) class using the specified [ConnectionEventArgs.Connection](#connectioneventargs-class#connection).  
  
```csharp  
public ConnectionEventArgs(object connection);  
```  
  
**Parameters**  
  
`connection` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of a related [BaseConnection](#baseconnection-class).  
  
### Properties  
  
#### Connection  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the [BaseConnection](#baseconnection-class) associated with this event.  
  
```csharp  
public object Connection { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### SplitLocation  
  
Gets or sets the location where the connection should be split.  
  
```csharp  
public Point SplitLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Methods  
  
#### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## ConnectionEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ConnectionEventHandler](#connectioneventhandler-delegate)  
  
**References:** [ConnectionEventArgs](#connectioneventargs-class), [BaseConnection](#baseconnection-class)  
  
Represents the method that will handle [BaseConnection](#baseconnection-class) related routed events.  
  
```csharp  
public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ConnectionEventArgs](#connectioneventargs-class): The event data.  
  
## ConnectionGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ConnectionGestures](#connectiongestures-class)  
  
**References:** [EditorGestures](#editorgestures-class), [InputGestureRef](#inputgestureref-class), [SelectionGestures](#selectiongestures-class)  
  
```csharp  
public class ConnectionGestures  
```  
  
### Constructors  
  
#### ConnectionGestures()  
  
```csharp  
public ConnectionGestures();  
```  
  
### Properties  
  
#### Disconnect  
  
```csharp  
public InputGestureRef Disconnect { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Selection  
  
```csharp  
public SelectionGestures Selection { get; set; }  
```  
  
**Property Value**  
  
[SelectionGestures](#selectiongestures-class)  
  
#### Split  
  
```csharp  
public InputGestureRef Split { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
### Methods  
  
#### Apply(ConnectionGestures)  
  
```csharp  
public void Apply(ConnectionGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [ConnectionGestures](#connectiongestures-class)  
  
## ConnectionOffsetMode Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [BaseConnection](#baseconnection-class)  
  
Specifies the offset type that can be applied to a [BaseConnection](#baseconnection-class) using the [BaseConnection.SourceOffset](#baseconnection-class#sourceoffset) and the [BaseConnection.TargetOffset](#baseconnection-class#targetoffset) values.  
  
```csharp  
public enum ConnectionOffsetMode  
```  
  
### Fields  
  
#### Circle  
  
The offset is applied in a circle around the point.  
  
```csharp  
Circle = 1;  
```  
  
#### Edge  
  
The offset is applied in a rectangle shape around the point, perpendicular to the edges.  
  
```csharp  
Edge = 3;  
```  
  
#### None  
  
No offset applied.  
  
```csharp  
None = 0;  
```  
  
#### Rectangle  
  
The offset is applied in a rectangle shape around the point.  
  
```csharp  
Rectangle = 2;  
```  
  
#### Static  
  
The offset is applied as a fixed margin.  
  
```csharp  
Static = 4;  
```  
  
## Connector Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](#connector-class)  
  
**Derived:** [NodeInput](#nodeinput-class), [NodeOutput](#nodeoutput-class), [StateNode](#statenode-class)  
  
**References:** [PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate), [ConnectorEventHandler](#connectoreventhandler-delegate), [ItemContainer](#itemcontainer-class), [PendingConnection](#pendingconnection-class), [Connection](#connection-class), [NodifyEditor](#nodifyeditor-class), [ConnectorEventArgs](#connectoreventargs-class), [PendingConnectionEventArgs](#pendingconnectioneventargs-class), [KnotNode](#knotnode-class), [Node](#node-class), [NodeInput](#nodeinput-class), [NodeOutput](#nodeoutput-class), [StateNode](#statenode-class)  
  
Represents a connector control that can start and complete a [PendingConnection](#pendingconnection-class).
            Has a [Connector.ElementConnector](#connector-class#elementconnector) that the [Connector.Anchor](#connector-class#anchor) is calculated from for the [PendingConnection](#pendingconnection-class). Center of this control is used if missing.  
  
```csharp  
public class Connector : Control  
```  
  
### Constructors  
  
#### Connector()  
  
```csharp  
public Connector();  
```  
  
### Fields  
  
#### ElementConnector  
  
```csharp  
protected const string ElementConnector = "PART_Connector";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
#### EnableOptimizations  
  
Gets or sets if [Connector](#connector-class)s should enable optimizations based on [Connector.OptimizeSafeZone](#connector-class#optimizesafezone) and [Connector.OptimizeMinimumSelectedItems](#connector-class#optimizeminimumselecteditems).  
  
```csharp  
public static bool EnableOptimizations;  
```  
  
**Field Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OptimizeMinimumSelectedItems  
  
Gets or sets the minimum selected items needed to trigger optimizations when outside of the [Connector.OptimizeSafeZone](#connector-class#optimizesafezone).  
  
```csharp  
public static uint OptimizeMinimumSelectedItems;  
```  
  
**Field Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
#### OptimizeSafeZone  
  
Gets or sets the safe zone outside the editor's viewport that will not trigger optimizations.  
  
```csharp  
public static double OptimizeSafeZone;  
```  
  
**Field Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Properties  
  
#### AllowPendingConnectionCancellation  
  
Gets or sets whether cancelling a pending connection is allowed.  
  
```csharp  
public static bool AllowPendingConnectionCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### Anchor  
  
Gets the location where [Connection](#connection-class)s can be attached to. 
            Bind with System.Windows.Data.BindingMode.OneWayToSource  
  
```csharp  
public Point Anchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### Container  
  
Gets the [ItemContainer](#itemcontainer-class) that contains this [Connector](#connector-class).  
  
```csharp  
protected ItemContainer Container { get; set; }  
```  
  
**Property Value**  
  
[ItemContainer](#itemcontainer-class)  
  
#### DisconnectCommand  
  
Invoked if the [Connector.Disconnect](#connector-class#disconnect) event is not handled.
            Parameter is the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of this control.  
  
```csharp  
public ICommand DisconnectCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### EnableStickyConnections  
  
Gets or sets whether the connection should be completed in two steps.  
  
```csharp  
public static bool EnableStickyConnections { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsConnected  
  
If this is set to false, the [Connector.Disconnect](#connector-class#disconnect) event will not be invoked and the connector will stop updating its [Connector.Anchor](#connector-class#anchor) when moved, resized etc.  
  
```csharp  
public bool IsConnected { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsPendingConnection  
  
Gets a value that indicates whether a [PendingConnection](#pendingconnection-class) is in progress for this [Connector](#connector-class).  
  
```csharp  
public bool IsPendingConnection { get; protected set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### Thumb  
  
Gets the [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) used to calculate the [Connector.Anchor](#connector-class#anchor).  
  
```csharp  
protected FrameworkElement Thumb { get; set; }  
```  
  
**Property Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### Methods  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
#### OnConnectorDrag(Vector)  
  
```csharp  
protected virtual void OnConnectorDrag(Vector offset);  
```  
  
**Parameters**  
  
`offset` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
#### OnConnectorDragCompleted(Boolean)  
  
```csharp  
protected virtual void OnConnectorDragCompleted(bool cancel = false);  
```  
  
**Parameters**  
  
`cancel` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OnConnectorDragStarted()  
  
```csharp  
protected virtual void OnConnectorDragStarted();  
```  
  
#### OnDisconnect()  
  
```csharp  
protected virtual void OnDisconnect();  
```  
  
#### OnKeyUp(KeyEventArgs)  
  
```csharp  
protected override void OnKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### OnLostMouseCapture(MouseEventArgs)  
  
```csharp  
protected override void OnLostMouseCapture(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
#### UpdateAnchor(Point)  
  
Updates the [Connector.Anchor](#connector-class#anchor) relative to a location. (usually [Connector.Container](#connector-class#container)'s location)  
  
```csharp  
protected void UpdateAnchor(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The relative location  
  
#### UpdateAnchor()  
  
Updates the [Connector.Anchor](#connector-class#anchor) based on [Connector.Container](#connector-class#container)'s location.  
  
```csharp  
public void UpdateAnchor();  
```  
  
#### UpdateAnchorOptimized(Point)  
  
Updates the [Connector.Anchor](#connector-class#anchor) and applies optimizations if needed based on [Connector.EnableOptimizations](#connector-class#enableoptimizations) flag  
  
```csharp  
protected void UpdateAnchorOptimized(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Events  
  
#### Disconnect  
  
Triggered by the Nodify.EditorGestures.ConnectorGestures.Disconnect gesture.  
  
```csharp  
public event ConnectorEventHandler Disconnect;  
```  
  
**Event Type**  
  
[ConnectorEventHandler](#connectoreventhandler-delegate)  
  
#### PendingConnectionCompleted  
  
Triggered by the Nodify.EditorGestures.ConnectorGestures.Connect gesture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionCompleted;  
```  
  
**Event Type**  
  
[PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate)  
  
#### PendingConnectionDrag  
  
Occurs when the mouse is changing position and the [Connector](#connector-class) has mouse capture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionDrag;  
```  
  
**Event Type**  
  
[PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate)  
  
#### PendingConnectionStarted  
  
Triggered by the Nodify.EditorGestures.ConnectorGestures.Connect gesture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionStarted;  
```  
  
**Event Type**  
  
[PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate)  
  
## ConnectorEventArgs Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ConnectorEventArgs](#connectoreventargs-class)  
  
**References:** [ConnectorEventHandler](#connectoreventhandler-delegate), [Connector](#connector-class)  
  
Provides data for [Connector](#connector-class) related routed events.  
  
```csharp  
public class ConnectorEventArgs : RoutedEventArgs  
```  
  
### Constructors  
  
#### ConnectorEventArgs(Object)  
  
Initializes a new instance of the [ConnectorEventArgs](#connectoreventargs-class) class using the specified [ConnectorEventArgs.Connector](#connectoreventargs-class#connector).  
  
```csharp  
public ConnectorEventArgs(object connector);  
```  
  
**Parameters**  
  
`connector` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of a related [Connector](#connector-class).  
  
### Properties  
  
#### Anchor  
  
Gets or sets the [Connector.Anchor](#connector-class#anchor) of the [Connector](#connector-class) associated with this event.  
  
```csharp  
public Point Anchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### Connector  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the [Connector](#connector-class) associated with this event.  
  
```csharp  
public object Connector { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### Methods  
  
#### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## ConnectorEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ConnectorEventHandler](#connectoreventhandler-delegate)  
  
**References:** [ConnectorEventArgs](#connectoreventargs-class), [Connector](#connector-class)  
  
Represents the method that will handle [Connector](#connector-class) related routed events.  
  
```csharp  
public delegate void ConnectorEventHandler(object sender, ConnectorEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ConnectorEventArgs](#connectoreventargs-class): The event data.  
  
## ConnectorGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ConnectorGestures](#connectorgestures-class)  
  
**References:** [EditorGestures](#editorgestures-class), [InputGestureRef](#inputgestureref-class)  
  
```csharp  
public class ConnectorGestures  
```  
  
### Constructors  
  
#### ConnectorGestures()  
  
```csharp  
public ConnectorGestures();  
```  
  
### Properties  
  
#### CancelAction  
  
```csharp  
public InputGestureRef CancelAction { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Connect  
  
```csharp  
public InputGestureRef Connect { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Disconnect  
  
```csharp  
public InputGestureRef Disconnect { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
### Methods  
  
#### Apply(ConnectorGestures)  
  
```csharp  
public void Apply(ConnectorGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [ConnectorGestures](#connectorgestures-class)  
  
## ConnectorPosition Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [StepConnection](#stepconnection-class)  
  
```csharp  
public enum ConnectorPosition  
```  
  
### Fields  
  
#### Bottom  
  
```csharp  
Bottom = 2;  
```  
  
#### Left  
  
```csharp  
Left = 1;  
```  
  
#### Right  
  
```csharp  
Right = 3;  
```  
  
#### Top  
  
```csharp  
Top = 0;  
```  
  
## ContainerDefaultState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ContainerState](#containerstate-class) → [ContainerDefaultState](#containerdefaultstate-class)  
  
**References:** [ItemContainer](#itemcontainer-class), [ContainerState](#containerstate-class)  
  
The default state of the [ItemContainer](#itemcontainer-class).  
  
```csharp  
public class ContainerDefaultState : ContainerState  
```  
  
### Constructors  
  
#### ContainerDefaultState(ItemContainer)  
  
Creates a new instance of the [ContainerDefaultState](#containerdefaultstate-class).  
  
```csharp  
public ContainerDefaultState(ItemContainer container);  
```  
  
**Parameters**  
  
`container` [ItemContainer](#itemcontainer-class): The owner of the state.  
  
### Methods  
  
#### HandleMouseDown(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public override void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### ReEnter(ContainerState)  
  
```csharp  
public override void ReEnter(ContainerState from);  
```  
  
**Parameters**  
  
`from` [ContainerState](#containerstate-class)  
  
## ContainerDraggingState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ContainerState](#containerstate-class) → [ContainerDraggingState](#containerdraggingstate-class)  
  
**References:** [ItemContainer](#itemcontainer-class), [ContainerState](#containerstate-class)  
  
Dragging state of the container.  
  
```csharp  
public class ContainerDraggingState : ContainerState  
```  
  
### Constructors  
  
#### ContainerDraggingState(ItemContainer)  
  
Constructs an instance of the [ContainerDraggingState](#containerdraggingstate-class) state.  
  
```csharp  
public ContainerDraggingState(ItemContainer container);  
```  
  
**Parameters**  
  
`container` [ItemContainer](#itemcontainer-class): The owner of the state.  
  
### Properties  
  
#### Canceled  
  
```csharp  
public bool Canceled { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Methods  
  
#### Enter(ContainerState)  
  
```csharp  
public override void Enter(ContainerState from);  
```  
  
**Parameters**  
  
`from` [ContainerState](#containerstate-class)  
  
#### Exit()  
  
```csharp  
public override void Exit();  
```  
  
#### HandleKeyUp(KeyEventArgs)  
  
```csharp  
public override void HandleKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public override void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
## ContainerState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ContainerState](#containerstate-class)  
  
**Derived:** [ContainerDefaultState](#containerdefaultstate-class), [ContainerDraggingState](#containerdraggingstate-class)  
  
**References:** [ContainerDefaultState](#containerdefaultstate-class), [ContainerDraggingState](#containerdraggingstate-class), [ItemContainer](#itemcontainer-class), [NodifyEditor](#nodifyeditor-class)  
  
The base class for container states.  
  
```csharp  
public abstract class ContainerState  
```  
  
### Constructors  
  
#### ContainerState(ItemContainer)  
  
Constructs a new [ContainerState](#containerstate-class).  
  
```csharp  
public ContainerState(ItemContainer container);  
```  
  
**Parameters**  
  
`container` [ItemContainer](#itemcontainer-class): The owner of the state.  
  
### Properties  
  
#### Container  
  
The owner of the state.  
  
```csharp  
protected ItemContainer Container { get; set; }  
```  
  
**Property Value**  
  
[ItemContainer](#itemcontainer-class)  
  
#### Editor  
  
The owner of the state.  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](#nodifyeditor-class)  
  
### Methods  
  
#### Enter(ContainerState)  
  
Called when Nodify.ItemContainer.PushState(Nodify.ContainerState) or Nodify.ItemContainer.PopState is called.  
  
```csharp  
public virtual void Enter(ContainerState from);  
```  
  
**Parameters**  
  
`from` [ContainerState](#containerstate-class): The state we enter from (is null for root state).  
  
#### Exit()  
  
Called when Nodify.ItemContainer.PopState is called.  
  
```csharp  
public virtual void Exit();  
```  
  
#### HandleKeyDown(KeyEventArgs)  
  
```csharp  
public virtual void HandleKeyDown(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleKeyUp(KeyEventArgs)  
  
```csharp  
public virtual void HandleKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleMouseDown(MouseButtonEventArgs)  
  
```csharp  
public virtual void HandleMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public virtual void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public virtual void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### HandleMouseWheel(MouseWheelEventArgs)  
  
```csharp  
public virtual void HandleMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  
#### PopState()  
  
Pops the current state from the stack.  
  
```csharp  
public virtual void PopState();  
```  
  
#### PushState(ContainerState)  
  
Pushes a new state into the stack.  
  
```csharp  
public virtual void PushState(ContainerState newState);  
```  
  
**Parameters**  
  
`newState` [ContainerState](#containerstate-class): The new state.  
  
#### ReEnter(ContainerState)  
  
Called when Nodify.ItemContainer.PopState is called.  
  
```csharp  
public virtual void ReEnter(ContainerState from);  
```  
  
**Parameters**  
  
`from` [ContainerState](#containerstate-class): The state we re-enter from.  
  
## CuttingLine Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [CuttingLine](#cuttingline-class)  
  
**References:** [BaseConnection](#baseconnection-class), [NodifyEditor](#nodifyeditor-class)  
  
```csharp  
public class CuttingLine : Shape  
```  
  
### Constructors  
  
#### CuttingLine()  
  
```csharp  
public CuttingLine();  
```  
  
### Properties  
  
#### AllowCuttingCancellation  
  
Gets or sets whether cancelling a cutting operation is allowed.  
  
```csharp  
public static bool AllowCuttingCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### DefiningGeometry  
  
```csharp  
protected override Geometry DefiningGeometry { get; set; }  
```  
  
**Property Value**  
  
[Geometry](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Geometry)  
  
#### EndPoint  
  
Gets or sets the end point.  
  
```csharp  
public Point EndPoint { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### StartPoint  
  
Gets or sets the start point.  
  
```csharp  
public Point StartPoint { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Methods  
  
#### GetIsOverElement(UIElement)  
  
```csharp  
public static bool GetIsOverElement(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OnRender(DrawingContext)  
  
```csharp  
protected override void OnRender(DrawingContext drawingContext);  
```  
  
**Parameters**  
  
`drawingContext` [DrawingContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.DrawingContext)  
  
#### SetIsOverElement(UIElement, Boolean)  
  
```csharp  
public static void SetIsOverElement(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## DecoratorContainer Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [DecoratorContainer](#decoratorcontainer-class)  
  
**Implements:** [INodifyCanvasItem](#inodifycanvasitem-interface)  
  
**References:** [NodifyEditor](#nodifyeditor-class)  
  
The container for all the items generated from the [NodifyEditor.Decorators](#nodifyeditor-class#decorators) collection.  
  
```csharp  
public class DecoratorContainer : ContentControl, INodifyCanvasItem  
```  
  
### Constructors  
  
#### DecoratorContainer()  
  
```csharp  
public DecoratorContainer();  
```  
  
### Properties  
  
#### ActualSize  
  
Gets the actual size of this [DecoratorContainer](#decoratorcontainer-class).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### Location  
  
Gets or sets the location of this [DecoratorContainer](#decoratorcontainer-class) inside the NodifyEditor.DecoratorsHost.  
  
```csharp  
public virtual Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Methods  
  
#### OnLocationChanged()  
  
Raises the [DecoratorContainer.LocationChangedEvent](#decoratorcontainer-class#locationchangedevent).  
  
```csharp  
protected void OnLocationChanged();  
```  
  
#### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
### Events  
  
#### LocationChanged  
  
Occurs when the [DecoratorContainer.Location](#decoratorcontainer-class#location) of this [DecoratorContainer](#decoratorcontainer-class) is changed.  
  
```csharp  
public event RoutedEventHandler LocationChanged;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  
## EditorCommands Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorCommands](#editorcommands-class)  
  
**References:** [ItemContainer](#itemcontainer-class), [NodifyEditor](#nodifyeditor-class), [InputGestureRef](#inputgestureref-class)  
  
```csharp  
public static class EditorCommands  
```  
  
### Properties  
  
#### Align  
  
Aligns [NodifyEditor.SelectedItems](#nodifyeditor-class#selecteditems) using the specified alignment method.
            Parameter is of type Nodify.EditorCommands.Alignment or a string that can be converted to an alignment.  
  
```csharp  
public static RoutedUICommand Align { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
#### BringIntoView  
  
Moves the [NodifyEditor.ViewportLocation](#nodifyeditor-class#viewportlocation) to the specified location.
            Parameter is a [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point) or a string that can be converted to a point.  
  
```csharp  
public static RoutedUICommand BringIntoView { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
#### FitToScreen  
  
Scales the editor's viewport to fit all the [ItemContainer](#itemcontainer-class)s if that's possible.  
  
```csharp  
public static RoutedUICommand FitToScreen { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
#### SelectAll  
  
Select all [ItemContainer](#itemcontainer-class)s in the [NodifyEditor](#nodifyeditor-class).  
  
```csharp  
public static RoutedUICommand SelectAll { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
#### ZoomIn  
  
Zoom in relative to the editor's viewport center.  
  
```csharp  
public static RoutedUICommand ZoomIn { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
#### ZoomOut  
  
Zoom out relative to the editor's viewport center.  
  
```csharp  
public static RoutedUICommand ZoomOut { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
## EditorCuttingState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorState](#editorstate-class) → [EditorCuttingState](#editorcuttingstate-class)  
  
**References:** [NodifyEditor](#nodifyeditor-class), [EditorState](#editorstate-class)  
  
```csharp  
public class EditorCuttingState : EditorState  
```  
  
### Constructors  
  
#### EditorCuttingState(NodifyEditor)  
  
```csharp  
public EditorCuttingState(NodifyEditor editor);  
```  
  
**Parameters**  
  
`editor` [NodifyEditor](#nodifyeditor-class)  
  
### Properties  
  
#### Canceled  
  
```csharp  
public bool Canceled { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Methods  
  
#### Enter(EditorState)  
  
```csharp  
public override void Enter(EditorState from);  
```  
  
**Parameters**  
  
`from` [EditorState](#editorstate-class)  
  
#### Exit()  
  
```csharp  
public override void Exit();  
```  
  
#### HandleKeyUp(KeyEventArgs)  
  
```csharp  
public override void HandleKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public override void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
## EditorDefaultState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorState](#editorstate-class) → [EditorDefaultState](#editordefaultstate-class)  
  
**References:** [NodifyEditor](#nodifyeditor-class)  
  
The default state of the editor.
              Default State
              	- mouse left down  	-> Selecting State
              	- mouse right down  -> Panning State
              Selecting State
              	- mouse left up 	-> Default State
              	- mouse right down 	-> Panning State
              Panning State
              	- mouse right up	-> previous state (Selecting State or Default State)
              	- mouse left up		-> Panning State  
  
```csharp  
public class EditorDefaultState : EditorState  
```  
  
### Constructors  
  
#### EditorDefaultState(NodifyEditor)  
  
Constructs an instance of the [EditorDefaultState](#editordefaultstate-class) state.  
  
```csharp  
public EditorDefaultState(NodifyEditor editor);  
```  
  
**Parameters**  
  
`editor` [NodifyEditor](#nodifyeditor-class): The owner of the state.  
  
### Methods  
  
#### HandleMouseDown(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
## EditorGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures](#editorgestures-class)  
  
**References:** [NodifyEditorGestures](#nodifyeditorgestures-class), [ItemContainerGestures](#itemcontainergestures-class), [ConnectorGestures](#connectorgestures-class), [ConnectionGestures](#connectiongestures-class), [GroupingNodeGestures](#groupingnodegestures-class), [MinimapGestures](#minimapgestures-class), [NodifyEditor](#nodifyeditor-class)  
  
Gestures used by built-in controls inside the [NodifyEditor](#nodifyeditor-class).  
  
```csharp  
public class EditorGestures  
```  
  
### Constructors  
  
#### EditorGestures()  
  
```csharp  
public EditorGestures();  
```  
  
### Fields  
  
#### Mappings  
  
```csharp  
public static EditorGestures Mappings;  
```  
  
**Field Value**  
  
[EditorGestures](#editorgestures-class)  
  
### Properties  
  
#### Connection  
  
Gestures for the connection.  
  
```csharp  
public ConnectionGestures Connection { get; set; }  
```  
  
**Property Value**  
  
[ConnectionGestures](#connectiongestures-class)  
  
#### Connector  
  
Gestures for the connector.  
  
```csharp  
public ConnectorGestures Connector { get; set; }  
```  
  
**Property Value**  
  
[ConnectorGestures](#connectorgestures-class)  
  
#### Editor  
  
Gestures for the editor.  
  
```csharp  
public NodifyEditorGestures Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditorGestures](#nodifyeditorgestures-class)  
  
#### GroupingNode  
  
Gestures for the grouping node.  
  
```csharp  
public GroupingNodeGestures GroupingNode { get; set; }  
```  
  
**Property Value**  
  
[GroupingNodeGestures](#groupingnodegestures-class)  
  
#### ItemContainer  
  
Gestures for the item container.  
  
```csharp  
public ItemContainerGestures ItemContainer { get; set; }  
```  
  
**Property Value**  
  
[ItemContainerGestures](#itemcontainergestures-class)  
  
#### Minimap  
  
Gestures for the minimap.  
  
```csharp  
public MinimapGestures Minimap { get; set; }  
```  
  
**Property Value**  
  
[MinimapGestures](#minimapgestures-class)  
  
### Methods  
  
#### Apply(EditorGestures)  
  
Copies from the specified gestures.  
  
```csharp  
public void Apply(EditorGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures](#editorgestures-class): The gestures to copy.  
  
## EditorPanningState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorState](#editorstate-class) → [EditorPanningState](#editorpanningstate-class)  
  
**References:** [NodifyEditor](#nodifyeditor-class), [EditorState](#editorstate-class)  
  
The panning state of the editor.  
  
```csharp  
public class EditorPanningState : EditorState  
```  
  
### Constructors  
  
#### EditorPanningState(NodifyEditor)  
  
Constructs an instance of the [EditorPanningState](#editorpanningstate-class) state.  
  
```csharp  
public EditorPanningState(NodifyEditor editor);  
```  
  
**Parameters**  
  
`editor` [NodifyEditor](#nodifyeditor-class): The owner of the state.  
  
### Methods  
  
#### Enter(EditorState)  
  
```csharp  
public override void Enter(EditorState from);  
```  
  
**Parameters**  
  
`from` [EditorState](#editorstate-class)  
  
#### Exit()  
  
```csharp  
public override void Exit();  
```  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public override void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
## EditorSelectingState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorState](#editorstate-class) → [EditorSelectingState](#editorselectingstate-class)  
  
**References:** [NodifyEditor](#nodifyeditor-class), [SelectionType](#selectiontype-enum), [SelectionHelper](#selectionhelper-class), [EditorState](#editorstate-class)  
  
The selecting state of the editor.  
  
```csharp  
public class EditorSelectingState : EditorState  
```  
  
### Constructors  
  
#### EditorSelectingState(NodifyEditor, SelectionType)  
  
Constructs an instance of the [EditorSelectingState](#editorselectingstate-class) state.  
  
```csharp  
public EditorSelectingState(NodifyEditor editor, SelectionType type);  
```  
  
**Parameters**  
  
`editor` [NodifyEditor](#nodifyeditor-class): The owner of the state.  
  
`type` [SelectionType](#selectiontype-enum): The selection strategy.  
  
### Properties  
  
#### Selection  
  
The selection helper.  
  
```csharp  
protected SelectionHelper Selection { get; set; }  
```  
  
**Property Value**  
  
[SelectionHelper](#selectionhelper-class)  
  
### Methods  
  
#### Enter(EditorState)  
  
```csharp  
public override void Enter(EditorState from);  
```  
  
**Parameters**  
  
`from` [EditorState](#editorstate-class)  
  
#### Exit()  
  
```csharp  
public override void Exit();  
```  
  
#### HandleAutoPanning(MouseEventArgs)  
  
```csharp  
public override void HandleAutoPanning(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleKeyUp(KeyEventArgs)  
  
```csharp  
public override void HandleKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleMouseDown(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public override void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public override void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
## EditorState Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorState](#editorstate-class)  
  
**Derived:** [EditorCuttingState](#editorcuttingstate-class), [EditorDefaultState](#editordefaultstate-class), [EditorPanningState](#editorpanningstate-class), [EditorSelectingState](#editorselectingstate-class)  
  
**References:** [EditorCuttingState](#editorcuttingstate-class), [EditorPanningState](#editorpanningstate-class), [EditorSelectingState](#editorselectingstate-class), [NodifyEditor](#nodifyeditor-class)  
  
The base class for editor states.  
  
```csharp  
public abstract class EditorState  
```  
  
### Constructors  
  
#### EditorState(NodifyEditor)  
  
Constructs a new [EditorState](#editorstate-class).  
  
```csharp  
public EditorState(NodifyEditor editor);  
```  
  
**Parameters**  
  
`editor` [NodifyEditor](#nodifyeditor-class): The owner of the state.  
  
### Properties  
  
#### Editor  
  
The owner of the state.  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](#nodifyeditor-class)  
  
### Methods  
  
#### Enter(EditorState)  
  
Called when Nodify.NodifyEditor.PushState(Nodify.EditorState) is called.  
  
```csharp  
public virtual void Enter(EditorState from);  
```  
  
**Parameters**  
  
`from` [EditorState](#editorstate-class): The state we enter from (is null for root state).  
  
#### Exit()  
  
Called when Nodify.NodifyEditor.PopState is called.  
  
```csharp  
public virtual void Exit();  
```  
  
#### HandleAutoPanning(MouseEventArgs)  
  
Handles auto panning when mouse is outside the editor.  
  
```csharp  
public virtual void HandleAutoPanning(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs): The [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs) that contains the event data.  
  
#### HandleKeyDown(KeyEventArgs)  
  
```csharp  
public virtual void HandleKeyDown(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleKeyUp(KeyEventArgs)  
  
```csharp  
public virtual void HandleKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### HandleMouseDown(MouseButtonEventArgs)  
  
```csharp  
public virtual void HandleMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### HandleMouseMove(MouseEventArgs)  
  
```csharp  
public virtual void HandleMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### HandleMouseUp(MouseButtonEventArgs)  
  
```csharp  
public virtual void HandleMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### HandleMouseWheel(MouseWheelEventArgs)  
  
```csharp  
public virtual void HandleMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  
#### PopState()  
  
Pops the current state from the stack.  
  
```csharp  
public virtual void PopState();  
```  
  
#### PushState(EditorState)  
  
Pushes a new state into the stack.  
  
```csharp  
public virtual void PushState(EditorState newState);  
```  
  
**Parameters**  
  
`newState` [EditorState](#editorstate-class): The new state.  
  
#### ReEnter(EditorState)  
  
Called when Nodify.NodifyEditor.PopState is called.  
  
```csharp  
public virtual void ReEnter(EditorState from);  
```  
  
**Parameters**  
  
`from` [EditorState](#editorstate-class): The state we re-enter from.  
  
## GeneratedInternalTypeHelper Class  
  
**Namespace:** XamlGeneratedNamespace  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InternalTypeHelper](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Markup.InternalTypeHelper) → [GeneratedInternalTypeHelper](#generatedinternaltypehelper-class)  
  
GeneratedInternalTypeHelper  
  
```csharp  
public sealed class GeneratedInternalTypeHelper : InternalTypeHelper  
```  
  
### Constructors  
  
#### GeneratedInternalTypeHelper()  
  
```csharp  
public GeneratedInternalTypeHelper();  
```  
  
### Methods  
  
#### AddEventHandler(EventInfo, Object, Delegate)  
  
AddEventHandler  
  
```csharp  
protected override void AddEventHandler(EventInfo eventInfo, object target, Delegate handler);  
```  
  
**Parameters**  
  
`eventInfo` [EventInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.EventInfo)  
  
`target` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`handler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
#### CreateDelegate(Type, Object, String)  
  
CreateDelegate  
  
```csharp  
protected override Delegate CreateDelegate(Type delegateType, object target, string handler);  
```  
  
**Parameters**  
  
`delegateType` [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type)  
  
`target` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`handler` [String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
**Returns**  
  
[Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
#### CreateInstance(Type, CultureInfo)  
  
CreateInstance  
  
```csharp  
protected override object CreateInstance(Type type, CultureInfo culture);  
```  
  
**Parameters**  
  
`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type)  
  
`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Globalization.CultureInfo)  
  
**Returns**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### GetPropertyValue(PropertyInfo, Object, CultureInfo)  
  
GetPropertyValue  
  
```csharp  
protected override object GetPropertyValue(PropertyInfo propertyInfo, object target, CultureInfo culture);  
```  
  
**Parameters**  
  
`propertyInfo` [PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.PropertyInfo)  
  
`target` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Globalization.CultureInfo)  
  
**Returns**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### SetPropertyValue(PropertyInfo, Object, Object, CultureInfo)  
  
SetPropertyValue  
  
```csharp  
protected override void SetPropertyValue(PropertyInfo propertyInfo, object target, object value, CultureInfo culture);  
```  
  
**Parameters**  
  
`propertyInfo` [PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.PropertyInfo)  
  
`target` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Globalization.CultureInfo)  
  
## GroupingMovementMode Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [GroupingNode](#groupingnode-class)  
  
Specifies the possible movement modes of a [GroupingNode](#groupingnode-class).  
  
```csharp  
public enum GroupingMovementMode  
```  
  
### Fields  
  
#### Group  
  
The [GroupingNode](#groupingnode-class) will move its content when moved.  
  
```csharp  
Group = 0;  
```  
  
#### Self  
  
The [GroupingNode](#groupingnode-class) will not move its content when moved.  
  
```csharp  
Self = 1;  
```  
  
## GroupingNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [GroupingNode](#groupingnode-class)  
  
**References:** [ResizeEventHandler](#resizeeventhandler-delegate), [GroupingMovementMode](#groupingmovementmode-enum), [NodifyEditor](#nodifyeditor-class), [ItemContainer](#itemcontainer-class)  
  
Defines a panel with a header that groups [ItemContainer](#itemcontainer-class)s inside it and can be resized.  
  
```csharp  
public class GroupingNode : HeaderedContentControl  
```  
  
### Constructors  
  
#### GroupingNode()  
  
Initializes a new instance of the [GroupingNode](#groupingnode-class) class.  
  
```csharp  
public GroupingNode();  
```  
  
### Fields  
  
#### ContentControl  
  
Gets the [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) control of this [GroupingNode](#groupingnode-class).  
  
```csharp  
protected FrameworkElement ContentControl;  
```  
  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
#### ElementContent  
  
```csharp  
protected const string ElementContent = "PART_Content";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
#### ElementHeader  
  
```csharp  
protected const string ElementHeader = "PART_Header";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
#### ElementResizeThumb  
  
```csharp  
protected const string ElementResizeThumb = "PART_ResizeThumb";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
#### GroupMovementBoxed  
  
```csharp  
protected static object GroupMovementBoxed;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### HeaderControl  
  
Gets the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header) control of this [GroupingNode](#groupingnode-class).  
  
```csharp  
protected FrameworkElement HeaderControl;  
```  
  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
#### ResizeThumb  
  
Gets the [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) used to resize this [GroupingNode](#groupingnode-class).  
  
```csharp  
protected FrameworkElement ResizeThumb;  
```  
  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### Properties  
  
#### ActualSize  
  
Gets or sets the actual size of this [GroupingNode](#groupingnode-class).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### CanResize  
  
Gets or sets a value that indicates whether this [GroupingNode](#groupingnode-class) can be resized.  
  
```csharp  
public bool CanResize { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### Container  
  
Gets the [NodifyEditor](#nodifyeditor-class) that owns this [GroupingNode.Container](#groupingnode-class#container).  
  
```csharp  
protected ItemContainer Container { get; set; }  
```  
  
**Property Value**  
  
[ItemContainer](#itemcontainer-class)  
  
#### Editor  
  
Gets the [NodifyEditor](#nodifyeditor-class) that owns this [GroupingNode](#groupingnode-class).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](#nodifyeditor-class)  
  
#### HeaderBrush  
  
Gets or sets the brush used for the background of the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header) of this [GroupingNode](#groupingnode-class).  
  
```csharp  
public Brush HeaderBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### MovementMode  
  
Gets or sets the default movement mode which can be temporarily changed by holding the SwitchMovementModeModifierKey while dragging by the header.  
  
```csharp  
public GroupingMovementMode MovementMode { get; set; }  
```  
  
**Property Value**  
  
[GroupingMovementMode](#groupingmovementmode-enum)  
  
#### ResizeCompletedCommand  
  
Invoked when the [GroupingNode.ResizeCompleted](#groupingnode-class#resizecompleted) event is not handled.
            Parameter is the [ItemContainer.ActualSize](#itemcontainer-class#actualsize) of the container.  
  
```csharp  
public ICommand ResizeCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### ResizeStartedCommand  
  
Invoked when the [GroupingNode.ResizeStarted](#groupingnode-class#resizestarted) event is not handled.
            Parameter is the [ItemContainer.ActualSize](#itemcontainer-class#actualsize) of the container.  
  
```csharp  
public ICommand ResizeStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Methods  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
### Events  
  
#### ResizeCompleted  
  
Occurs when the node finished resizing.  
  
```csharp  
public event ResizeEventHandler ResizeCompleted;  
```  
  
**Event Type**  
  
[ResizeEventHandler](#resizeeventhandler-delegate)  
  
#### ResizeStarted  
  
Occurs when the node started resizing.  
  
```csharp  
public event ResizeEventHandler ResizeStarted;  
```  
  
**Event Type**  
  
[ResizeEventHandler](#resizeeventhandler-delegate)  
  
## GroupingNodeGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [GroupingNodeGestures](#groupingnodegestures-class)  
  
**References:** [EditorGestures](#editorgestures-class)  
  
```csharp  
public class GroupingNodeGestures  
```  
  
### Constructors  
  
#### GroupingNodeGestures()  
  
```csharp  
public GroupingNodeGestures();  
```  
  
### Properties  
  
#### SwitchMovementMode  
  
```csharp  
public ModifierKeys SwitchMovementMode { get; set; }  
```  
  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### Methods  
  
#### Apply(GroupingNodeGestures)  
  
```csharp  
public void Apply(GroupingNodeGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [GroupingNodeGestures](#groupingnodegestures-class)  
  
## INodifyCanvasItem Interface  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [INodifyCanvasItem](#inodifycanvasitem-interface)  
  
**Derived:** [DecoratorContainer](#decoratorcontainer-class), [ItemContainer](#itemcontainer-class)  
  
**References:** [NodifyCanvas](#nodifycanvas-class)  
  
Interface for items inside a [NodifyCanvas](#nodifycanvas-class).  
  
```csharp  
public abstract interface INodifyCanvasItem  
```  
  
### Properties  
  
#### DesiredSize  
  
The desired size of the item.  
  
```csharp  
public virtual Size DesiredSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### Location  
  
The location of the item.  
  
```csharp  
public virtual Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Methods  
  
#### Arrange(Rect)  
  
```csharp  
public virtual void Arrange(Rect rect);  
```  
  
**Parameters**  
  
`rect` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
## InputGestureRef Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [InputGestureRef](#inputgestureref-class)  
  
**References:** [SelectionGestures](#selectiongestures-class), [ItemContainerGestures](#itemcontainergestures-class), [NodifyEditorGestures](#nodifyeditorgestures-class), [ConnectorGestures](#connectorgestures-class), [ConnectionGestures](#connectiongestures-class), [MinimapGestures](#minimapgestures-class), [EditorCommands](#editorcommands-class)  
  
An input gesture that allows changing its logic at runtime without changing its reference.
            Useful for classes that capture the object reference without the posibility of updating it. (e.g. [EditorCommands](#editorcommands-class))  
  
```csharp  
public sealed class InputGestureRef : InputGesture  
```  
  
### Properties  
  
#### Value  
  
The referenced gesture.  
  
```csharp  
public InputGesture Value { get; set; }  
```  
  
**Property Value**  
  
[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)  
  
### Methods  
  
#### Matches(Object, InputEventArgs)  
  
```csharp  
public override bool Matches(object targetElement, InputEventArgs inputEventArgs);  
```  
  
**Parameters**  
  
`targetElement` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`inputEventArgs` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## ItemContainer Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [ItemContainer](#itemcontainer-class)  
  
**Implements:** [INodifyCanvasItem](#inodifycanvasitem-interface)  
  
**References:** [Connector](#connector-class), [ContainerDefaultState](#containerdefaultstate-class), [ContainerDraggingState](#containerdraggingstate-class), [ContainerState](#containerstate-class), [NodifyEditor](#nodifyeditor-class), [PreviewLocationChanged](#previewlocationchanged-delegate), [GroupingNode](#groupingnode-class), [PendingConnection](#pendingconnection-class), [EditorCommands](#editorcommands-class), [SelectionHelper](#selectionhelper-class)  
  
The container for all the items generated by the [ItemsControl.ItemsSource](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl.itemssource) of the [NodifyEditor](#nodifyeditor-class).  
  
```csharp  
public class ItemContainer : ContentControl, INodifyCanvasItem  
```  
  
### Constructors  
  
#### ItemContainer(NodifyEditor)  
  
Constructs an instance of an [ItemContainer](#itemcontainer-class) in the specified [NodifyEditor](#nodifyeditor-class).  
  
```csharp  
public ItemContainer(NodifyEditor editor);  
```  
  
**Parameters**  
  
`editor` [NodifyEditor](#nodifyeditor-class)  
  
### Fields  
  
#### IsPreviewingLocationPropertyKey  
  
```csharp  
public static DependencyPropertyKey IsPreviewingLocationPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### IsPreviewingSelectionPropertyKey  
  
```csharp  
public static DependencyPropertyKey IsPreviewingSelectionPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### Properties  
  
#### ActualSize  
  
Gets the actual size of this [ItemContainer](#itemcontainer-class).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### AllowDraggingCancellation  
  
Gets or sets whether cancelling a dragging operation is allowed.  
  
```csharp  
public static bool AllowDraggingCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### DesiredSizeForSelection  
  
Overrides the size to check against when calculating if this [ItemContainer](#itemcontainer-class) can be part of the current [NodifyEditor.SelectedArea](#nodifyeditor-class#selectedarea).
            Defaults to [UIElement.RenderSize](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement.rendersize).  
  
```csharp  
public Size? DesiredSizeForSelection { get; set; }  
```  
  
**Property Value**  
  
[Size?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable)  
  
#### Editor  
  
The [NodifyEditor](#nodifyeditor-class) that owns this [ItemContainer](#itemcontainer-class).  
  
```csharp  
public NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](#nodifyeditor-class)  
  
#### HighlightBrush  
  
Gets or sets the brush used when the [PendingConnection.IsOverElementProperty](#pendingconnection-class#isoverelementproperty) attached property is true for this [ItemContainer](#itemcontainer-class).  
  
```csharp  
public Brush HighlightBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### IsDraggable  
  
Gets or sets whether this [ItemContainer](#itemcontainer-class) can be dragged.  
  
```csharp  
public bool IsDraggable { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsPreviewingLocation  
  
Gets a value indicating whether this [ItemContainer](#itemcontainer-class) is previewing a new location but didn't logically move there.  
  
```csharp  
public bool IsPreviewingLocation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsPreviewingSelection  
  
Gets a value indicating whether this [ItemContainer](#itemcontainer-class) is about to change its [ItemContainer.IsSelected](#itemcontainer-class#isselected) state.  
  
```csharp  
public Boolean? IsPreviewingSelection { get; set; }  
```  
  
**Property Value**  
  
[Boolean?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable)  
  
#### IsSelectable  
  
Gets or sets whether this [ItemContainer](#itemcontainer-class) can be selected.  
  
```csharp  
public bool IsSelectable { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsSelected  
  
Gets or sets a value that indicates whether this [ItemContainer](#itemcontainer-class) is selected.
            Can only be set if [ItemContainer.IsSelectable](#itemcontainer-class#isselectable) is true.  
  
```csharp  
public bool IsSelected { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### Location  
  
Gets or sets the location of this [ItemContainer](#itemcontainer-class) inside the [NodifyEditor](#nodifyeditor-class) in graph space coordinates.  
  
```csharp  
public virtual Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### SelectedBorderThickness  
  
Gets or sets the border thickness used when [ItemContainer.IsSelected](#itemcontainer-class#isselected) or [ItemContainer.IsPreviewingSelection](#itemcontainer-class#ispreviewingselection) is true.  
  
```csharp  
public Thickness SelectedBorderThickness { get; set; }  
```  
  
**Property Value**  
  
[Thickness](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Thickness)  
  
#### SelectedBrush  
  
Gets or sets the brush used when [ItemContainer.IsSelected](#itemcontainer-class#isselected) or [ItemContainer.IsPreviewingSelection](#itemcontainer-class#ispreviewingselection) is true.  
  
```csharp  
public Brush SelectedBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### SelectedMargin  
  
The calculated margin when the container is selected or previewing selection.  
  
```csharp  
public Thickness SelectedMargin { get; set; }  
```  
  
**Property Value**  
  
[Thickness](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Thickness)  
  
#### State  
  
The current state of the container.  
  
```csharp  
public ContainerState State { get; set; }  
```  
  
**Property Value**  
  
[ContainerState](#containerstate-class)  
  
### Methods  
  
#### GetInitialState()  
  
Creates the initial state of the container.  
  
```csharp  
protected virtual ContainerState GetInitialState();  
```  
  
**Returns**  
  
[ContainerState](#containerstate-class): The initial state.  
  
#### IsSelectableInArea(Rect, Boolean)  
  
Checks if area contains or intersects with this [ItemContainer](#itemcontainer-class) taking into consideration the [ItemContainer.DesiredSizeForSelection](#itemcontainer-class#desiredsizeforselection).  
  
```csharp  
public virtual bool IsSelectableInArea(Rect area, bool isContained);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to check if contains or intersects this [ItemContainer](#itemcontainer-class).  
  
`isContained` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): If true will check if area contains this, otherwise will check if area intersects with this.  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True if area contains or intersects this [ItemContainer](#itemcontainer-class).  
  
#### IsSelectableLocation(Point)  
  
Checks if position is selectable.  
  
```csharp  
protected virtual bool IsSelectableLocation(Point position);  
```  
  
**Parameters**  
  
`position` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A position relative to this [ItemContainer](#itemcontainer-class).  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True if position is selectable.  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
#### OnKeyDown(KeyEventArgs)  
  
```csharp  
protected override void OnKeyDown(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### OnKeyUp(KeyEventArgs)  
  
```csharp  
protected override void OnKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### OnLocationChanged()  
  
Raises the [ItemContainer.LocationChangedEvent](#itemcontainer-class#locationchangedevent) and sets [ItemContainer.IsPreviewingLocation](#itemcontainer-class#ispreviewinglocation) to false.  
  
```csharp  
protected void OnLocationChanged();  
```  
  
#### OnLostMouseCapture(MouseEventArgs)  
  
```csharp  
protected override void OnLostMouseCapture(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseWheel(MouseWheelEventArgs)  
  
```csharp  
protected override void OnMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  
#### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
#### OnSelectedChanged(Boolean)  
  
Raises the [ItemContainer.SelectedEvent](#itemcontainer-class#selectedevent) or [ItemContainer.UnselectedEvent](#itemcontainer-class#unselectedevent) based on newValue.
            Called when the [ItemContainer.IsSelected](#itemcontainer-class#isselected) value is changed.  
  
```csharp  
protected void OnSelectedChanged(bool newValue);  
```  
  
**Parameters**  
  
`newValue` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True if selected, false otherwise.  
  
#### PopAllStates()  
  
Pops all states from the container.  
  
```csharp  
public void PopAllStates();  
```  
  
#### PopState()  
  
Pops the current [ItemContainer.State](#itemcontainer-class#state) from the stack.  
  
```csharp  
public void PopState();  
```  
  
#### PushState(ContainerState)  
  
Pushes the given state to the stack.  
  
```csharp  
public void PushState(ContainerState state);  
```  
  
**Parameters**  
  
`state` [ContainerState](#containerstate-class): The new state of the container.  
  
### Events  
  
#### DragCompleted  
  
Occurs when this [ItemContainer](#itemcontainer-class) completed the drag operation.  
  
```csharp  
public event DragCompletedEventHandler DragCompleted;  
```  
  
**Event Type**  
  
[DragCompletedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.DragCompletedEventHandler)  
  
#### DragDelta  
  
Occurs when this [ItemContainer](#itemcontainer-class) is being dragged.  
  
```csharp  
public event DragDeltaEventHandler DragDelta;  
```  
  
**Event Type**  
  
[DragDeltaEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.DragDeltaEventHandler)  
  
#### DragStarted  
  
Occurs when this [ItemContainer](#itemcontainer-class) is the instigator of a drag operation.  
  
```csharp  
public event DragStartedEventHandler DragStarted;  
```  
  
**Event Type**  
  
[DragStartedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.DragStartedEventHandler)  
  
#### LocationChanged  
  
Occurs when the [ItemContainer.Location](#itemcontainer-class#location) of this [ItemContainer](#itemcontainer-class) is changed.  
  
```csharp  
public event RoutedEventHandler LocationChanged;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  
#### PreviewLocationChanged  
  
Occurs when the [ItemContainer](#itemcontainer-class) is previewing a new location.  
  
```csharp  
public event PreviewLocationChanged PreviewLocationChanged;  
```  
  
**Event Type**  
  
[PreviewLocationChanged](#previewlocationchanged-delegate)  
  
#### Selected  
  
Occurs when this [ItemContainer](#itemcontainer-class) is selected.  
  
```csharp  
public event RoutedEventHandler Selected;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  
#### Unselected  
  
Occurs when this [ItemContainer](#itemcontainer-class) is unselected.  
  
```csharp  
public event RoutedEventHandler Unselected;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  
## ItemContainerGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ItemContainerGestures](#itemcontainergestures-class)  
  
**References:** [EditorGestures](#editorgestures-class), [SelectionGestures](#selectiongestures-class), [InputGestureRef](#inputgestureref-class)  
  
```csharp  
public class ItemContainerGestures  
```  
  
### Constructors  
  
#### ItemContainerGestures()  
  
```csharp  
public ItemContainerGestures();  
```  
  
### Properties  
  
#### CancelAction  
  
```csharp  
public InputGestureRef CancelAction { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Drag  
  
```csharp  
public InputGestureRef Drag { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Selection  
  
```csharp  
public SelectionGestures Selection { get; set; }  
```  
  
**Property Value**  
  
[SelectionGestures](#selectiongestures-class)  
  
### Methods  
  
#### Apply(ItemContainerGestures)  
  
```csharp  
public void Apply(ItemContainerGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [ItemContainerGestures](#itemcontainergestures-class)  
  
## KnotNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [KnotNode](#knotnode-class)  
  
**References:** [Connector](#connector-class)  
  
Represents a control that owns a [Connector](#connector-class).  
  
```csharp  
public class KnotNode : ContentControl  
```  
  
### Constructors  
  
#### KnotNode()  
  
```csharp  
public KnotNode();  
```  
  
## LineConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class) → [LineConnection](#lineconnection-class)  
  
**Derived:** [CircuitConnection](#circuitconnection-class), [StepConnection](#stepconnection-class)  
  
**References:** [ConnectionDirection](#connectiondirection-enum), [BaseConnection](#baseconnection-class)  
  
Represents a line that has an arrow indicating its [BaseConnection.Direction](#baseconnection-class#direction).  
  
```csharp  
public class LineConnection : BaseConnection  
```  
  
### Constructors  
  
#### LineConnection()  
  
```csharp  
public LineConnection();  
```  
  
### Methods  
  
#### DrawDefaultArrowhead(StreamGeometryContext, Point, Point, ConnectionDirection, Orientation)  
  
```csharp  
protected override void DrawDefaultArrowhead(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = 0, Orientation orientation = 0);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`arrowDirection` [ConnectionDirection](#connectiondirection-enum)  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
#### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)
  
#### InterpolateLine(Point, Point, Point, Point, Double)  
  
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
  
[ValueTuple<ValueTuple<Point, Point>, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)
  
#### InterpolateLine(Point, Point, Point, Double)  
  
```csharp  
protected static ValueTuple<ValueTuple<Point, Point>, Point> InterpolateLine(Point p0, Point p1, Point p2, double t);  
```  
  
**Parameters**  
  
`p0` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p1` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p2` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`t` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
**Returns**  
  
[ValueTuple<ValueTuple<Point, Point>, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)
  
#### InterpolateLineSegment(Point, Point, Double)  
  
```csharp  
protected static Point InterpolateLineSegment(Point p0, Point p1, double t);  
```  
  
**Parameters**  
  
`p0` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`p1` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`t` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Match Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [MultiGesture](#multigesture-class)  
  
```csharp  
public enum Match  
```  
  
### Fields  
  
#### All  
  
```csharp  
All = 1;  
```  
  
#### Any  
  
```csharp  
Any = 0;  
```  
  
## Minimap Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [Minimap](#minimap-class)  
  
**References:** [ZoomEventHandler](#zoomeventhandler-delegate), [MinimapItem](#minimapitem-class), [NodifyEditor](#nodifyeditor-class), [ZoomEventArgs](#zoomeventargs-class)  
  
A minimap control that can position the viewport, and zoom in and out.  
  
```csharp  
public class Minimap : ItemsControl  
```  
  
### Constructors  
  
#### Minimap()  
  
```csharp  
public Minimap();  
```  
  
### Fields  
  
#### ElementItemsHost  
  
```csharp  
protected const string ElementItemsHost = "PART_ItemsHost";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### Properties  
  
#### Extent  
  
The area covered by the items and the viewport rectangle in graph space.  
  
```csharp  
public Rect Extent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
#### IsDragging  
  
```csharp  
protected bool IsDragging { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsReadOnly  
  
Whether the minimap can move and zoom the viewport.  
  
```csharp  
public bool IsReadOnly { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### ItemsExtent  
  
The area covered by the [MinimapItem](#minimapitem-class)s in graph space.  
  
```csharp  
public Rect ItemsExtent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
#### MaxViewportOffset  
  
The max position from the [NodifyEditor.ItemsExtent](#nodifyeditor-class#itemsextent) that the viewport can move to.  
  
```csharp  
public Size MaxViewportOffset { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### ResizeToViewport  
  
Whether the minimap should resize to also display the whole viewport.  
  
```csharp  
public bool ResizeToViewport { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### ViewportLocation  
  
```csharp  
public Point ViewportLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### ViewportSize  
  
```csharp  
public Size ViewportSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### ViewportStyle  
  
Gets or sets the style to use for the viewport rectangle.  
  
```csharp  
public Style ViewportStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### Methods  
  
#### GetContainerForItemOverride()  
  
```csharp  
protected override DependencyObject GetContainerForItemOverride();  
```  
  
**Returns**  
  
[DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
#### IsItemItsOwnContainerOverride(Object)  
  
```csharp  
protected override bool IsItemItsOwnContainerOverride(object item);  
```  
  
**Parameters**  
  
`item` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
#### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseWheel(MouseWheelEventArgs)  
  
```csharp  
protected override void OnMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  
### Events  
  
#### Zoom  
  
Triggered when zooming in or out using the mouse wheel.  
  
```csharp  
public event ZoomEventHandler Zoom;  
```  
  
**Event Type**  
  
[ZoomEventHandler](#zoomeventhandler-delegate)  
  
## MinimapGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [MinimapGestures](#minimapgestures-class)  
  
**References:** [EditorGestures](#editorgestures-class), [InputGestureRef](#inputgestureref-class)  
  
```csharp  
public class MinimapGestures  
```  
  
### Constructors  
  
#### MinimapGestures()  
  
```csharp  
public MinimapGestures();  
```  
  
### Properties  
  
#### DragViewport  
  
```csharp  
public InputGestureRef DragViewport { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### ZoomModifierKey  
  
```csharp  
public ModifierKeys ZoomModifierKey { get; set; }  
```  
  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
### Methods  
  
#### Apply(MinimapGestures)  
  
```csharp  
public void Apply(MinimapGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [MinimapGestures](#minimapgestures-class)  
  
## MinimapItem Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [MinimapItem](#minimapitem-class)  
  
**References:** [Minimap](#minimap-class)  
  
```csharp  
public class MinimapItem : ContentControl  
```  
  
### Constructors  
  
#### MinimapItem()  
  
```csharp  
public MinimapItem();  
```  
  
### Properties  
  
#### Location  
  
Gets or sets the location of this [MinimapItem](#minimapitem-class) inside the [Minimap](#minimap-class).  
  
```csharp  
public Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## MultiGesture Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [MultiGesture](#multigesture-class)  
  
**Derived:** [AnyGesture](#anygesture-class), [AllGestures](#allgestures-class)  
  
**References:** [Match](#match-enum)  
  
Combines multiple input gestures.  
  
```csharp  
public class MultiGesture : InputGesture  
```  
  
### Constructors  
  
#### MultiGesture(Match, InputGesture[])  
  
Constructs an instance of a [MultiGesture](#multigesture-class).  
  
```csharp  
public MultiGesture(Match match, InputGesture[] gestures);  
```  
  
**Parameters**  
  
`match` [Match](#match-enum): The matching strategy.  
  
`gestures` [InputGesture[]](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture[]): The input gestures.  
  
### Fields  
  
#### None  
  
```csharp  
public static MultiGesture None;  
```  
  
**Field Value**  
  
[MultiGesture](#multigesture-class)  
  
### Methods  
  
#### Matches(Object, InputEventArgs)  
  
```csharp  
public override bool Matches(object targetElement, InputEventArgs inputEventArgs);  
```  
  
**Parameters**  
  
`targetElement` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`inputEventArgs` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Node Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [Node](#node-class)  
  
**References:** [Connector](#connector-class), [NodeInput](#nodeinput-class), [NodeOutput](#nodeoutput-class)  
  
Represents a control that has a list of [Node.Input](#node-class#input)[Connector](#connector-class)s and a list of [Node.Output](#node-class#output)[Connector](#connector-class)s.  
  
```csharp  
public class Node : HeaderedContentControl  
```  
  
### Constructors  
  
#### Node()  
  
```csharp  
public Node();  
```  
  
### Properties  
  
#### ContentBrush  
  
Gets or sets the brush used for the background of the [ContentControl.Content](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl.content) of this [Node](#node-class).  
  
```csharp  
public Brush ContentBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### ContentContainerStyle  
  
Gets or sets the style for the content container.  
  
```csharp  
public Style ContentContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
#### Footer  
  
Gets or sets the data for the footer of this control.  
  
```csharp  
public object Footer { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### FooterBrush  
  
Gets or sets the brush used for the background of the [Node.Footer](#node-class#footer) of this [Node](#node-class).  
  
```csharp  
public Brush FooterBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### FooterContainerStyle  
  
Gets or sets the style for the footer container.  
  
```csharp  
public Style FooterContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
#### FooterTemplate  
  
Gets or sets the template used to display the content of the control's footer.  
  
```csharp  
public DataTemplate FooterTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### HasFooter  
  
Gets a value that indicates whether the [Node.Footer](#node-class#footer) is .  
  
```csharp  
public bool HasFooter { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### HeaderBrush  
  
Gets or sets the brush used for the background of the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header) of this [Node](#node-class).  
  
```csharp  
public Brush HeaderBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### HeaderContainerStyle  
  
Gets or sets the style for the header container.  
  
```csharp  
public Style HeaderContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
#### Input  
  
Gets or sets the data for the input [Connector](#connector-class)s of this control.  
  
```csharp  
public IEnumerable Input { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
#### InputConnectorTemplate  
  
Gets or sets the template used to display the content of the control's [Node.Input](#node-class#input) connectors.  
  
```csharp  
public DataTemplate InputConnectorTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### Output  
  
Gets or sets the data for the output [Connector](#connector-class)s of this control.  
  
```csharp  
public IEnumerable Output { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
#### OutputConnectorTemplate  
  
Gets or sets the template used to display the content of the control's [Node.Output](#node-class#output) connectors.  
  
```csharp  
public DataTemplate OutputConnectorTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
## NodeInput Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](#connector-class) → [NodeInput](#nodeinput-class)  
  
**References:** [Node](#node-class), [Connector](#connector-class)  
  
Represents the default control for the [Node.InputConnectorTemplate](#node-class#inputconnectortemplate).  
  
```csharp  
public class NodeInput : Connector  
```  
  
### Constructors  
  
#### NodeInput()  
  
```csharp  
public NodeInput();  
```  
  
### Properties  
  
#### ConnectorTemplate  
  
Gets or sets the template used to display the connecting point of this [Connector](#connector-class).  
  
```csharp  
public ControlTemplate ConnectorTemplate { get; set; }  
```  
  
**Property Value**  
  
[ControlTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ControlTemplate)  
  
#### Header  
  
Gets of sets the data used for the control's header.  
  
```csharp  
public object Header { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### HeaderTemplate  
  
Gets or sets the template used to display the content of the control's header.  
  
```csharp  
public DataTemplate HeaderTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### Orientation  
  
```csharp  
public Orientation Orientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
## NodeOutput Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](#connector-class) → [NodeOutput](#nodeoutput-class)  
  
**References:** [Node](#node-class), [Connector](#connector-class)  
  
Represents the default control for the [Node.OutputConnectorTemplate](#node-class#outputconnectortemplate).  
  
```csharp  
public class NodeOutput : Connector  
```  
  
### Constructors  
  
#### NodeOutput()  
  
```csharp  
public NodeOutput();  
```  
  
### Properties  
  
#### ConnectorTemplate  
  
Gets or sets the template used to display the connecting point of this [Connector](#connector-class).  
  
```csharp  
public ControlTemplate ConnectorTemplate { get; set; }  
```  
  
**Property Value**  
  
[ControlTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ControlTemplate)  
  
#### Header  
  
Gets of sets the data used for the control's header.  
  
```csharp  
public object Header { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### HeaderTemplate  
  
Gets or sets the template used to display the content of the control's header.  
  
```csharp  
public DataTemplate HeaderTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### Orientation  
  
```csharp  
public Orientation Orientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
## NodifyCanvas Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Panel](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Panel) → [NodifyCanvas](#nodifycanvas-class)  
  
**References:** [INodifyCanvasItem](#inodifycanvasitem-interface)  
  
A canvas like panel that works with [INodifyCanvasItem](#inodifycanvasitem-interface)s.  
  
```csharp  
public class NodifyCanvas : Panel  
```  
  
### Constructors  
  
#### NodifyCanvas()  
  
```csharp  
public NodifyCanvas();  
```  
  
### Properties  
  
#### Extent  
  
The area covered by the children of this panel.  
  
```csharp  
public Rect Extent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### Methods  
  
#### ArrangeOverride(Size)  
  
```csharp  
protected override Size ArrangeOverride(Size arrangeSize);  
```  
  
**Parameters**  
  
`arrangeSize` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
**Returns**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### MeasureOverride(Size)  
  
```csharp  
protected override Size MeasureOverride(Size constraint);  
```  
  
**Parameters**  
  
`constraint` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
**Returns**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
## NodifyEditor Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [Selector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.Selector) → [MultiSelector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.MultiSelector) → [NodifyEditor](#nodifyeditor-class)  
  
**Implements:** [IScrollInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.IScrollInfo)  
  
**References:** [ContainerState](#containerstate-class), [EditorCuttingState](#editorcuttingstate-class), [EditorDefaultState](#editordefaultstate-class), [EditorPanningState](#editorpanningstate-class), [EditorSelectingState](#editorselectingstate-class), [EditorState](#editorstate-class), [SelectionHelper](#selectionhelper-class), [ItemContainer](#itemcontainer-class), [PendingConnection](#pendingconnection-class), [GroupingNode](#groupingnode-class), [Connector](#connector-class), [CuttingLine](#cuttingline-class), [DecoratorContainer](#decoratorcontainer-class), [EditorCommands](#editorcommands-class), [EditorGestures](#editorgestures-class), [Minimap](#minimap-class), [Connection](#connection-class), [BaseConnection](#baseconnection-class)  
  
Groups [ItemContainer](#itemcontainer-class)s and [Connection](#connection-class)s in an area that you can drag, zoom and select.  
  
```csharp  
public class NodifyEditor : MultiSelector, IScrollInfo  
```  
  
### Constructors  
  
#### NodifyEditor()  
  
Initializes a new instance of the [NodifyEditor](#nodifyeditor-class) class.  
  
```csharp  
public NodifyEditor();  
```  
  
### Fields  
  
#### CuttingConnectionTypes  
  
```csharp  
public static HashSet<Type> CuttingConnectionTypes;  
```  
  
**Field Value**  
  
[HashSet<Type>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.HashSet)  
  
#### CuttingLineEndPropertyKey  
  
```csharp  
protected static DependencyPropertyKey CuttingLineEndPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### CuttingLineStartPropertyKey  
  
```csharp  
protected static DependencyPropertyKey CuttingLineStartPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### ElementConnectionsHost  
  
```csharp  
protected const string ElementConnectionsHost = "PART_ConnectionsHost";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
#### ElementItemsHost  
  
```csharp  
protected const string ElementItemsHost = "PART_ItemsHost";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
#### IsCuttingPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsCuttingPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### IsPanningPropertyKey  
  
```csharp  
public static DependencyPropertyKey IsPanningPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### IsSelectingPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsSelectingPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### MouseLocationPropertyKey  
  
```csharp  
protected static DependencyPropertyKey MouseLocationPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### ScaleTransform  
  
Gets the transform used to zoom on the viewport.  
  
```csharp  
protected readonly ScaleTransform ScaleTransform;  
```  
  
**Field Value**  
  
[ScaleTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.ScaleTransform)  
  
#### SelectedAreaPropertyKey  
  
```csharp  
protected static DependencyPropertyKey SelectedAreaPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
#### TranslateTransform  
  
Gets the transform used to offset the viewport.  
  
```csharp  
protected readonly TranslateTransform TranslateTransform;  
```  
  
**Field Value**  
  
[TranslateTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.TranslateTransform)  
  
### Properties  
  
#### AutoPanEdgeDistance  
  
Gets or sets the maximum distance in pixels from the edge of the editor that will trigger auto-panning.  
  
```csharp  
public double AutoPanEdgeDistance { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### AutoPanningTickRate  
  
Gets or sets how often the new [NodifyEditor.ViewportLocation](#nodifyeditor-class#viewportlocation) is calculated in milliseconds when [NodifyEditor.DisableAutoPanning](#nodifyeditor-class#disableautopanning) is false.  
  
```csharp  
public static double AutoPanningTickRate { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### AutoPanSpeed  
  
Gets or sets the speed used when auto-panning scaled by [NodifyEditor.AutoPanningTickRate](#nodifyeditor-class#autopanningtickrate)  
  
```csharp  
public double AutoPanSpeed { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### BringIntoViewMaxDuration  
  
Gets or sets the maximum animation duration in seconds for bringing a location into view.  
  
```csharp  
public double BringIntoViewMaxDuration { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### BringIntoViewSpeed  
  
Gets or sets the animation speed in pixels per second for bringing a location into view.  
  
```csharp  
public double BringIntoViewSpeed { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### CanSelectMultipleConnections  
  
Gets or sets whether multiple connections can be selected.  
  
```csharp  
public bool CanSelectMultipleConnections { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### CanSelectMultipleItems  
  
Gets or sets whether multiple [ItemContainer](#itemcontainer-class)s can be selected.  
  
```csharp  
public bool CanSelectMultipleItems { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### ConnectionCompletedCommand  
  
Invoked when the [PendingConnection](#pendingconnection-class) is completed. 
            Use [PendingConnection.CompletedCommand](#pendingconnection-class#completedcommand) if you want to control the visibility of the connection from the viewmodel. 
            Parameter is System.Tuple`2 where System.Tuple`2.Item1 is the [PendingConnection.Source](#pendingconnection-class#source) and System.Tuple`2.Item2 is [PendingConnection.Target](#pendingconnection-class#target).  
  
```csharp  
public ICommand ConnectionCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### Connections  
  
Gets or sets the data source that [BaseConnection](#baseconnection-class)s will be generated for.  
  
```csharp  
public IEnumerable Connections { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
#### ConnectionStartedCommand  
  
Invoked when the [PendingConnection](#pendingconnection-class) is completed. 
            Use [PendingConnection.StartedCommand](#pendingconnection-class#startedcommand) if you want to control the visibility of the connection from the viewmodel. 
            Parameter is [PendingConnection.Source](#pendingconnection-class#source).  
  
```csharp  
public ICommand ConnectionStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### ConnectionTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use when generating a new [BaseConnection](#baseconnection-class).  
  
```csharp  
public DataTemplate ConnectionTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### CuttingCompletedCommand  
  
Invoked when a cutting operation is completed.  
  
```csharp  
public ICommand CuttingCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### CuttingLineEnd  
  
Gets the end point of the [CuttingLine](#cuttingline-class) while [NodifyEditor.IsCutting](#nodifyeditor-class#iscutting) is true.  
  
```csharp  
public Point CuttingLineEnd { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### CuttingLineStart  
  
Gets the start point of the [CuttingLine](#cuttingline-class) while [NodifyEditor.IsCutting](#nodifyeditor-class#iscutting) is true.  
  
```csharp  
public Point CuttingLineStart { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### CuttingLineStyle  
  
Gets or sets the style to use for the cutting line.  
  
```csharp  
public Style CuttingLineStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
#### CuttingStartedCommand  
  
Invoked when a cutting operation is started.  
  
```csharp  
public ICommand CuttingStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### DecoratorContainerStyle  
  
Gets or sets the style to use for the [DecoratorContainer](#decoratorcontainer-class).  
  
```csharp  
public Style DecoratorContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
#### Decorators  
  
Gets or sets the items that will be rendered in the decorators layer via [DecoratorContainer](#decoratorcontainer-class)s.  
  
```csharp  
public IEnumerable Decorators { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
#### DecoratorsExtent  
  
The area covered by the [DecoratorContainer](#decoratorcontainer-class)s.  
  
```csharp  
public Rect DecoratorsExtent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
#### DecoratorTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use when generating a new [DecoratorContainer](#decoratorcontainer-class).  
  
```csharp  
public DataTemplate DecoratorTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### DisableAutoPanning  
  
Gets or sets whether to disable the auto panning when selecting or dragging near the edge of the editor configured by [NodifyEditor.AutoPanEdgeDistance](#nodifyeditor-class#autopanedgedistance).  
  
```csharp  
public bool DisableAutoPanning { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### DisablePanning  
  
Gets or sets whether panning should be disabled.  
  
```csharp  
public bool DisablePanning { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### DisableZooming  
  
Gets or sets whether zooming should be disabled.  
  
```csharp  
public bool DisableZooming { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### DisconnectConnectorCommand  
  
Invoked when the [Connector.Disconnect](#connector-class#disconnect) event is raised. 
            Can also be handled at the [Connector](#connector-class) level using the [Connector.DisconnectCommand](#connector-class#disconnectcommand) command. 
            Parameter is the [Connector](#connector-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext).  
  
```csharp  
public ICommand DisconnectConnectorCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### DisplayConnectionsOnTop  
  
Gets or sets whether to display connections on top of [ItemContainer](#itemcontainer-class)s or not.  
  
```csharp  
public bool DisplayConnectionsOnTop { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### EnableCuttingLinePreview  
  
Gets or sets whether the cutting line should apply the preview style to the interesected elements.  
  
```csharp  
public static bool EnableCuttingLinePreview { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### EnableDraggingContainersOptimizations  
  
Gets or sets if the current position of containers that are being dragged should not be committed until the end of the dragging operation.  
  
```csharp  
public static bool EnableDraggingContainersOptimizations { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### EnableRealtimeSelection  
  
Enables selecting and deselecting items while the [NodifyEditor.SelectedArea](#nodifyeditor-class#selectedarea) changes.
            Disable for maximum performance when hundreds of items are generated.  
  
```csharp  
public bool EnableRealtimeSelection { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### EnableRenderingContainersOptimizations  
  
Gets or sets if [NodifyEditor](#nodifyeditor-class)s should enable optimizations based on [NodifyEditor.OptimizeRenderingMinimumContainers](#nodifyeditor-class#optimizerenderingminimumcontainers) and [NodifyEditor.OptimizeRenderingZoomOutPercent](#nodifyeditor-class#optimizerenderingzoomoutpercent).  
  
```csharp  
public static bool EnableRenderingContainersOptimizations { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### EnableSnappingCorrection  
  
Correct [ItemContainer](#itemcontainer-class)'s position after moving if starting position is not snapped to grid.  
  
```csharp  
public static bool EnableSnappingCorrection { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### FitToScreenExtentMargin  
  
Gets or sets the margin to add in all directions to the [NodifyEditor.ItemsExtent](#nodifyeditor-class#itemsextent) or area parameter when using Nodify.NodifyEditor.FitToScreen(System.Nullable{System.Windows.Rect}).  
  
```csharp  
public static double FitToScreenExtentMargin { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### GridCellSize  
  
Gets or sets the value of an invisible grid used to adjust locations (snapping) of [ItemContainer](#itemcontainer-class)s.  
  
```csharp  
public uint GridCellSize { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
#### HandleRightClickAfterPanningThreshold  
  
Gets or sets the maximum number of pixels allowed to move the mouse before cancelling the mouse event.
            Useful for System.Windows.Controls.ContextMenus to appear if mouse only moved a bit or not at all.  
  
```csharp  
public static double HandleRightClickAfterPanningThreshold { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### IsBulkUpdatingItems  
  
Tells if the [NodifyEditor](#nodifyeditor-class) is doing operations on multiple items at once.  
  
```csharp  
public bool IsBulkUpdatingItems { get; protected set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsCutting  
  
Gets a value that indicates whether a cutting operation is in progress.  
  
```csharp  
public bool IsCutting { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsPanning  
  
Gets a value that indicates whether a panning operation is in progress.  
  
```csharp  
public bool IsPanning { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsSelecting  
  
Gets a value that indicates whether a selection operation is in progress.  
  
```csharp  
public bool IsSelecting { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### ItemsDragCompletedCommand  
  
Invoked when a drag operation is completed for the [NodifyEditor.SelectedItems](#nodifyeditor-class#selecteditems).  
  
```csharp  
public ICommand ItemsDragCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### ItemsDragStartedCommand  
  
Invoked when a drag operation starts for the [NodifyEditor.SelectedItems](#nodifyeditor-class#selecteditems).  
  
```csharp  
public ICommand ItemsDragStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### ItemsExtent  
  
The area covered by the [ItemContainer](#itemcontainer-class)s.  
  
```csharp  
public Rect ItemsExtent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
#### ItemsSelectCompletedCommand  
  
Invoked when a selection operation is completed.  
  
```csharp  
public ICommand ItemsSelectCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### ItemsSelectStartedCommand  
  
Invoked when a selection operation is started.  
  
```csharp  
public ICommand ItemsSelectStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### MaxViewportZoom  
  
Gets or sets the maximum zoom factor of the viewport  
  
```csharp  
public double MaxViewportZoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### MinViewportZoom  
  
Gets or sets the minimum zoom factor of the viewport  
  
```csharp  
public double MinViewportZoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### MouseLocation  
  
Gets the current mouse location in graph space coordinates (relative to the [NodifyEditor.ItemsHost](#nodifyeditor-class#itemshost)).  
  
```csharp  
public Point MouseLocation { get; protected set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### OptimizeRenderingMinimumContainers  
  
Gets or sets the minimum number of [ItemContainer](#itemcontainer-class)s needed to trigger optimizations when reaching the [NodifyEditor.OptimizeRenderingZoomOutPercent](#nodifyeditor-class#optimizerenderingzoomoutpercent).  
  
```csharp  
public static uint OptimizeRenderingMinimumContainers { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
#### OptimizeRenderingZoomOutPercent  
  
Gets or sets the minimum zoom out percent needed to start optimizing the rendering for [ItemContainer](#itemcontainer-class)s.
            Value is between 0 and 1.  
  
```csharp  
public static double OptimizeRenderingZoomOutPercent { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### PendingConnection  
  
Gets of sets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the [PendingConnection](#pendingconnection-class).  
  
```csharp  
public object PendingConnection { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### PendingConnectionTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use for the [NodifyEditor.PendingConnection](#nodifyeditor-class#pendingconnection).  
  
```csharp  
public DataTemplate PendingConnectionTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### RemoveConnectionCommand  
  
Invoked when the [BaseConnection.Disconnect](#baseconnection-class#disconnect) event is raised. 
            Can also be handled at the [BaseConnection](#baseconnection-class) level using the [BaseConnection.DisconnectCommand](#baseconnection-class#disconnectcommand) command. 
            Parameter is the [BaseConnection](#baseconnection-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext).  
  
```csharp  
public ICommand RemoveConnectionCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### ScrollIncrement  
  
The number of units the mouse wheel is rotated to scroll one line.  
  
```csharp  
public static double ScrollIncrement { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### SelectedArea  
  
Gets the currently selected area while [NodifyEditor.IsSelecting](#nodifyeditor-class#isselecting) is true.  
  
```csharp  
public Rect SelectedArea { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
#### SelectedConnection  
  
Gets or sets the selected connection.  
  
```csharp  
public object SelectedConnection { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### SelectedConnections  
  
Gets or sets the selected connections in the [NodifyEditor](#nodifyeditor-class).  
  
```csharp  
public IList SelectedConnections { get; set; }  
```  
  
**Property Value**  
  
[IList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IList)  
  
#### SelectedItems  
  
Gets or sets the selected items in the [NodifyEditor](#nodifyeditor-class).  
  
```csharp  
public IList SelectedItems { get; set; }  
```  
  
**Property Value**  
  
[IList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IList)  
  
#### SelectionRectangleStyle  
  
Gets or sets the style to use for the selection rectangle.  
  
```csharp  
public Style SelectionRectangleStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
#### State  
  
The current state of the editor.  
  
```csharp  
public EditorState State { get; set; }  
```  
  
**Property Value**  
  
[EditorState](#editorstate-class)  
  
#### ViewportLocation  
  
Gets or sets the viewport's top-left coordinates in graph space coordinates.  
  
```csharp  
public Point ViewportLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### ViewportSize  
  
Gets the size of the viewport in graph space (scaled by the [NodifyEditor.ViewportZoom](#nodifyeditor-class#viewportzoom)).  
  
```csharp  
public Size ViewportSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### ViewportTransform  
  
Gets the transform that is applied to all child controls.  
  
```csharp  
public Transform ViewportTransform { get; set; }  
```  
  
**Property Value**  
  
[Transform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Transform)  
  
#### ViewportZoom  
  
Gets or sets the zoom factor of the viewport.  
  
```csharp  
public double ViewportZoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Methods  
  
#### BringIntoView(Point, Boolean, Action)  
  
Moves the viewport center at the specified location.  
  
```csharp  
public void BringIntoView(Point point, bool animated = true, Action onFinish = null);  
```  
  
**Parameters**  
  
`point` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location in graph space coordinates.  
  
`animated` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to animate the movement.  
  
`onFinish` [Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action): The callback invoked when movement is finished.  
  
#### BringIntoView(Rect)  
  
Moves the viewport center at the center of the specified area.  
  
```csharp  
public void BringIntoView(Rect area);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The location in graph space coordinates.  
  
#### FitToScreen(Rect?)  
  
Scales the viewport to fit the specified area or all the [ItemContainer](#itemcontainer-class)s if that's possible.  
  
```csharp  
public void FitToScreen(Rect? area = null);  
```  
  
**Parameters**  
  
`area` [Rect?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable)  
  
#### GetContainerForItemOverride()  
  
```csharp  
protected override DependencyObject GetContainerForItemOverride();  
```  
  
**Returns**  
  
[DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
#### GetInitialState()  
  
Creates the initial state of the editor.  
  
```csharp  
protected virtual EditorState GetInitialState();  
```  
  
**Returns**  
  
[EditorState](#editorstate-class): The initial state.  
  
#### GetLocationInsideEditor(Point, UIElement)  
  
Translates the specified location to graph space coordinates (relative to the [NodifyEditor.ItemsHost](#nodifyeditor-class#itemshost)).  
  
```csharp  
public Point GetLocationInsideEditor(Point location, UIElement relativeTo);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location coordinates relative to relativeTo  
  
`relativeTo` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement): The element where the location was calculated from.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the graph.  
  
#### GetLocationInsideEditor(DragEventArgs)  
  
Translates the event location to graph space coordinates (relative to the [NodifyEditor.ItemsHost](#nodifyeditor-class#itemshost)).  
  
```csharp  
public Point GetLocationInsideEditor(DragEventArgs args);  
```  
  
**Parameters**  
  
`args` [DragEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DragEventArgs): The drag event.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the graph  
  
#### GetLocationInsideEditor(MouseEventArgs)  
  
Translates the event location to graph space coordinates (relative to the [NodifyEditor.ItemsHost](#nodifyeditor-class#itemshost)).  
  
```csharp  
public Point GetLocationInsideEditor(MouseEventArgs args);  
```  
  
**Parameters**  
  
`args` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs): The mouse event.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the graph  
  
#### InvertSelection(Rect, Boolean)  
  
Inverts the [ItemContainer](#itemcontainer-class)s selection in the specified area.  
  
```csharp  
public void InvertSelection(Rect area, bool fit = false);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](#itemcontainer-class)s.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](#itemcontainer-class). False to check if area intersects the [ItemContainer](#itemcontainer-class).  
  
#### IsItemItsOwnContainerOverride(Object)  
  
```csharp  
protected override bool IsItemItsOwnContainerOverride(object item);  
```  
  
**Parameters**  
  
`item` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
#### OnDisableAutoPanningChanged(Boolean)  
  
Called when the [NodifyEditor.DisableAutoPanning](#nodifyeditor-class#disableautopanning) changes.  
  
```csharp  
protected virtual void OnDisableAutoPanningChanged(bool shouldDisable);  
```  
  
**Parameters**  
  
`shouldDisable` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Whether to enable or disable auto panning.  
  
#### OnKeyDown(KeyEventArgs)  
  
```csharp  
protected override void OnKeyDown(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### OnKeyUp(KeyEventArgs)  
  
```csharp  
protected override void OnKeyUp(KeyEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyEventArgs)  
  
#### OnLostMouseCapture(MouseEventArgs)  
  
```csharp  
protected override void OnLostMouseCapture(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
#### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseWheel(MouseWheelEventArgs)  
  
```csharp  
protected override void OnMouseWheel(MouseWheelEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseWheelEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseWheelEventArgs)  
  
#### OnRemoveConnection(Object)  
  
```csharp  
protected void OnRemoveConnection(object dataContext);  
```  
  
**Parameters**  
  
`dataContext` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
#### OnSelectionChanged(SelectionChangedEventArgs)  
  
```csharp  
protected override void OnSelectionChanged(SelectionChangedEventArgs e);  
```  
  
**Parameters**  
  
`e` [SelectionChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.SelectionChangedEventArgs)  
  
#### OnViewportUpdated()  
  
Updates the [NodifyEditor.ViewportSize](#nodifyeditor-class#viewportsize) and raises the [NodifyEditor.ViewportUpdatedEvent](#nodifyeditor-class#viewportupdatedevent).
            Called when the [UIElement.RenderSize](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement.rendersize) or [NodifyEditor.ViewportZoom](#nodifyeditor-class#viewportzoom) is changed.  
  
```csharp  
protected void OnViewportUpdated();  
```  
  
#### PopAllStates()  
  
Pops all states from the editor.  
  
```csharp  
public void PopAllStates();  
```  
  
#### PopState()  
  
Pops the current [NodifyEditor.State](#nodifyeditor-class#state) from the stack.  
  
```csharp  
public void PopState();  
```  
  
#### PushState(EditorState)  
  
Pushes the given state to the stack.  
  
```csharp  
public void PushState(EditorState state);  
```  
  
**Parameters**  
  
`state` [EditorState](#editorstate-class): The new state of the editor.  
  
#### SelectAllConnections()  
  
Select all [NodifyEditor.Connections](#nodifyeditor-class#connections).  
  
```csharp  
public void SelectAllConnections();  
```  
  
#### SelectArea(Rect, Boolean, Boolean)  
  
Selects the [ItemContainer](#itemcontainer-class)s in the specified area.  
  
```csharp  
public void SelectArea(Rect area, bool append = false, bool fit = false);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](#itemcontainer-class)s.  
  
`append` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): If true, it will add to the existing selection.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](#itemcontainer-class). False to check if area intersects the [ItemContainer](#itemcontainer-class).  
  
#### UnselectAllConnection()  
  
Unselect all [NodifyEditor.Connections](#nodifyeditor-class#connections).  
  
```csharp  
public void UnselectAllConnection();  
```  
  
#### UnselectArea(Rect, Boolean)  
  
Unselect the [ItemContainer](#itemcontainer-class)s in the specified area.  
  
```csharp  
public void UnselectArea(Rect area, bool fit = false);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](#itemcontainer-class)s.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](#itemcontainer-class). False to check if area intersects the [ItemContainer](#itemcontainer-class).  
  
#### ZoomAtPosition(Double, Point)  
  
Zoom at the specified location in graph space coordinates.  
  
```csharp  
public void ZoomAtPosition(double zoom, Point location);  
```  
  
**Parameters**  
  
`zoom` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The zoom factor.  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location to focus when zooming.  
  
#### ZoomIn()  
  
Zoom in at the viewports center  
  
```csharp  
public void ZoomIn();  
```  
  
#### ZoomOut()  
  
Zoom out at the viewports center  
  
```csharp  
public void ZoomOut();  
```  
  
### Events  
  
#### ViewportUpdated  
  
Occurs whenever the viewport updates.  
  
```csharp  
public event RoutedEventHandler ViewportUpdated;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  
## NodifyEditorGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [NodifyEditorGestures](#nodifyeditorgestures-class)  
  
**References:** [EditorGestures](#editorgestures-class), [SelectionGestures](#selectiongestures-class), [InputGestureRef](#inputgestureref-class)  
  
```csharp  
public class NodifyEditorGestures  
```  
  
### Constructors  
  
#### NodifyEditorGestures()  
  
```csharp  
public NodifyEditorGestures();  
```  
  
### Properties  
  
#### CancelAction  
  
```csharp  
public InputGestureRef CancelAction { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Cutting  
  
```csharp  
public InputGestureRef Cutting { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### FitToScreen  
  
```csharp  
public InputGestureRef FitToScreen { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Pan  
  
```csharp  
public InputGestureRef Pan { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### ResetViewportLocation  
  
```csharp  
public InputGestureRef ResetViewportLocation { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Selection  
  
```csharp  
public SelectionGestures Selection { get; set; }  
```  
  
**Property Value**  
  
[SelectionGestures](#selectiongestures-class)  
  
#### ZoomIn  
  
```csharp  
public InputGestureRef ZoomIn { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### ZoomModifierKey  
  
```csharp  
public ModifierKeys ZoomModifierKey { get; set; }  
```  
  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
#### ZoomOut  
  
```csharp  
public InputGestureRef ZoomOut { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
### Methods  
  
#### Apply(NodifyEditorGestures)  
  
```csharp  
public void Apply(NodifyEditorGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [NodifyEditorGestures](#nodifyeditorgestures-class)  
  
## PendingConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [PendingConnection](#pendingconnection-class)  
  
**References:** [PendingConnectionEventArgs](#pendingconnectioneventargs-class), [ConnectionDirection](#connectiondirection-enum), [NodifyEditor](#nodifyeditor-class), [Connector](#connector-class), [ItemContainer](#itemcontainer-class), [PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate), [StateNode](#statenode-class)  
  
Represents a pending connection usually started by a [Connector](#connector-class) which invokes the [PendingConnection.CompletedCommand](#pendingconnection-class#completedcommand) when completed.  
  
```csharp  
public class PendingConnection : ContentControl  
```  
  
### Constructors  
  
#### PendingConnection()  
  
```csharp  
public PendingConnection();  
```  
  
### Properties  
  
#### AllowOnlyConnectors  
  
If true will preview and connect only to [Connector](#connector-class)s, otherwise will also enable [ItemContainer](#itemcontainer-class)s.  
  
```csharp  
public bool AllowOnlyConnectors { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### CompletedCommand  
  
Gets or sets the command to invoke when the pending connection is completed.
            Will not be invoked if [NodifyEditor.ConnectionCompletedCommand](#nodifyeditor-class#connectioncompletedcommand) is used.
            [PendingConnection.Target](#pendingconnection-class#target) will be set to the desired [Connector](#connector-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) and will also be the command's parameter.  
  
```csharp  
public ICommand CompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### Direction  
  
Gets or sets the direction of this connection.  
  
```csharp  
public ConnectionDirection Direction { get; set; }  
```  
  
**Property Value**  
  
[ConnectionDirection](#connectiondirection-enum)  
  
#### Editor  
  
Gets the [NodifyEditor](#nodifyeditor-class) that owns this [PendingConnection](#pendingconnection-class).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](#nodifyeditor-class)  
  
#### EnablePreview  
  
[PendingConnection.PreviewTarget](#pendingconnection-class#previewtarget) will be updated with a potential [Connector](#connector-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) if this is true.  
  
```csharp  
public bool EnablePreview { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### EnableSnapping  
  
Enables snapping the [PendingConnection.TargetAnchor](#pendingconnection-class#targetanchor) to a possible [PendingConnection.Target](#pendingconnection-class#target) connector.  
  
```csharp  
public bool EnableSnapping { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### IsVisible  
  
Gets or sets the visibility of the connection.  
  
```csharp  
public bool IsVisible { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### PreviewTarget  
  
Gets or sets the [Connector](#connector-class) or the [ItemContainer](#itemcontainer-class) (if [PendingConnection.AllowOnlyConnectors](#pendingconnection-class#allowonlyconnectors) is false) that we're previewing.  
  
```csharp  
public object PreviewTarget { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### Source  
  
Gets or sets the [Connector](#connector-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) that started this pending connection.  
  
```csharp  
public object Source { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### SourceAnchor  
  
Gets or sets the starting point for the connection.  
  
```csharp  
public Point SourceAnchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### StartedCommand  
  
Gets or sets the command to invoke when the pending connection is started.
            Will not be invoked if [NodifyEditor.ConnectionStartedCommand](#nodifyeditor-class#connectionstartedcommand) is used.
            [PendingConnection.Source](#pendingconnection-class#source) will be set to the [Connector](#connector-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) that started this connection and will also be the command's parameter.  
  
```csharp  
public ICommand StartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
#### Stroke  
  
Gets or sets the stroke color of the connection.  
  
```csharp  
public Brush Stroke { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
#### StrokeDashArray  
  
Gets or sets the pattern of dashes and gaps that is used to outline the connection.  
  
```csharp  
public DoubleCollection StrokeDashArray { get; set; }  
```  
  
**Property Value**  
  
[DoubleCollection](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.DoubleCollection)  
  
#### StrokeThickness  
  
Gets or set the connection thickness.  
  
```csharp  
public double StrokeThickness { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### Target  
  
Gets or sets the [Connector](#connector-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) (or potentially an [ItemContainer](#itemcontainer-class)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) if [PendingConnection.AllowOnlyConnectors](#pendingconnection-class#allowonlyconnectors) is false) that the [PendingConnection.Source](#pendingconnection-class#source) can connect to.
            Only set when the connection is completed (see [PendingConnection.CompletedCommand](#pendingconnection-class#completedcommand)).  
  
```csharp  
public object Target { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### TargetAnchor  
  
Gets or sets the end point for the connection.  
  
```csharp  
public Point TargetAnchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Methods  
  
#### GetIsOverElement(UIElement)  
  
```csharp  
public static bool GetIsOverElement(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
#### OnPendingConnectionCompleted(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionCompleted(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](#pendingconnectioneventargs-class)  
  
#### OnPendingConnectionDrag(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionDrag(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](#pendingconnectioneventargs-class)  
  
#### OnPendingConnectionStarted(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionStarted(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](#pendingconnectioneventargs-class)  
  
#### SetIsOverElement(UIElement, Boolean)  
  
```csharp  
public static void SetIsOverElement(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## PendingConnectionEventArgs Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [PendingConnectionEventArgs](#pendingconnectioneventargs-class)  
  
**References:** [PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate), [PendingConnection](#pendingconnection-class), [Connector](#connector-class)  
  
Provides data for [PendingConnection](#pendingconnection-class) related routed events.  
  
```csharp  
public class PendingConnectionEventArgs : RoutedEventArgs  
```  
  
### Constructors  
  
#### PendingConnectionEventArgs(Object)  
  
Initializes a new instance of the [PendingConnectionEventArgs](#pendingconnectioneventargs-class) class using the specified [PendingConnectionEventArgs.SourceConnector](#pendingconnectioneventargs-class#sourceconnector).  
  
```csharp  
public PendingConnectionEventArgs(object sourceConnector);  
```  
  
**Parameters**  
  
`sourceConnector` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of a related [Connector](#connector-class).  
  
### Properties  
  
#### Anchor  
  
Gets or sets the [Connector.Anchor](#connector-class#anchor) of the [Connector](#connector-class) that raised this event.  
  
```csharp  
public Point Anchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### Canceled  
  
Gets or sets a value that indicates whether this [PendingConnection](#pendingconnection-class) was cancelled.  
  
```csharp  
public bool Canceled { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
#### OffsetX  
  
Gets or sets the distance from the [PendingConnectionEventArgs.SourceConnector](#pendingconnectioneventargs-class#sourceconnector) in the X axis.  
  
```csharp  
public double OffsetX { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### OffsetY  
  
Gets or sets the distance from the [PendingConnectionEventArgs.SourceConnector](#pendingconnectioneventargs-class#sourceconnector) in the Y axis.  
  
```csharp  
public double OffsetY { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
#### SourceConnector  
  
Gets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the [Connector](#connector-class) that started this [PendingConnection](#pendingconnection-class).  
  
```csharp  
public object SourceConnector { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### TargetConnector  
  
Gets or sets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the target [Connector](#connector-class) when the [PendingConnection](#pendingconnection-class) is completed.  
  
```csharp  
public object TargetConnector { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### Methods  
  
#### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## PendingConnectionEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate)  
  
**References:** [PendingConnectionEventArgs](#pendingconnectioneventargs-class), [Connector](#connector-class), [PendingConnection](#pendingconnection-class)  
  
Represents the method that will handle [PendingConnection](#pendingconnection-class) related routed events.  
  
```csharp  
public delegate void PendingConnectionEventHandler(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [PendingConnectionEventArgs](#pendingconnectioneventargs-class): The event data.  
  
## PreviewLocationChanged Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [PreviewLocationChanged](#previewlocationchanged-delegate)  
  
**References:** [ItemContainer](#itemcontainer-class)  
  
Delegate used to notify when an [ItemContainer](#itemcontainer-class) is previewing a new location.  
  
```csharp  
public delegate void PreviewLocationChanged(Point newLocation);  
```  
  
**Parameters**  
  
`newLocation` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The new location.  
  
## ResizeEventArgs Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ResizeEventArgs](#resizeeventargs-class)  
  
**References:** [ResizeEventHandler](#resizeeventhandler-delegate)  
  
Provides data for resize related routed events.  
  
```csharp  
public class ResizeEventArgs : RoutedEventArgs  
```  
  
### Constructors  
  
#### ResizeEventArgs(Size, Size)  
  
Initializes a new instance of the [ResizeEventArgs](#resizeeventargs-class) class with the previous and the new [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size).  
  
```csharp  
public ResizeEventArgs(Size previousSize, Size newSize);  
```  
  
**Parameters**  
  
`previousSize` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size): The previous size associated with this event.  
  
`newSize` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size): The new size associated with this event.  
  
### Properties  
  
#### NewSize  
  
Gets the new size of the object.  
  
```csharp  
public Size NewSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
#### PreviousSize  
  
Gets the previous size of the object.  
  
```csharp  
public Size PreviousSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### Methods  
  
#### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## ResizeEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ResizeEventHandler](#resizeeventhandler-delegate)  
  
**References:** [ResizeEventArgs](#resizeeventargs-class), [GroupingNode](#groupingnode-class)  
  
Represents the method that will handle resize related routed events.  
  
```csharp  
public delegate void ResizeEventHandler(object sender, ResizeEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The sender of this event.  
  
`e` [ResizeEventArgs](#resizeeventargs-class): The event data.  
  
## SelectionGestures Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [SelectionGestures](#selectiongestures-class)  
  
**References:** [InputGestureRef](#inputgestureref-class), [ItemContainerGestures](#itemcontainergestures-class), [NodifyEditorGestures](#nodifyeditorgestures-class), [ConnectionGestures](#connectiongestures-class)  
  
```csharp  
public class SelectionGestures  
```  
  
### Constructors  
  
#### SelectionGestures(MouseAction)  
  
```csharp  
public SelectionGestures(MouseAction mouseAction);  
```  
  
**Parameters**  
  
`mouseAction` [MouseAction](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseAction)  
  
#### SelectionGestures()  
  
```csharp  
public SelectionGestures();  
```  
  
### Fields  
  
#### None  
  
```csharp  
public static SelectionGestures None;  
```  
  
**Field Value**  
  
[SelectionGestures](#selectiongestures-class)  
  
### Properties  
  
#### Append  
  
```csharp  
public InputGestureRef Append { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Cancel  
  
```csharp  
public InputGestureRef Cancel { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Invert  
  
```csharp  
public InputGestureRef Invert { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Remove  
  
```csharp  
public InputGestureRef Remove { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Replace  
  
```csharp  
public InputGestureRef Replace { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
#### Select  
  
```csharp  
public InputGestureRef Select { get; set; }  
```  
  
**Property Value**  
  
[InputGestureRef](#inputgestureref-class)  
  
### Methods  
  
#### Apply(SelectionGestures)  
  
```csharp  
public void Apply(SelectionGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [SelectionGestures](#selectiongestures-class)  
  
## SelectionHelper Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [SelectionHelper](#selectionhelper-class)  
  
**References:** [EditorSelectingState](#editorselectingstate-class), [NodifyEditor](#nodifyeditor-class), [SelectionType](#selectiontype-enum), [ItemContainer](#itemcontainer-class)  
  
Helps with selecting [ItemContainer](#itemcontainer-class)s and updating the [NodifyEditor.SelectedArea](#nodifyeditor-class#selectedarea) and [NodifyEditor.IsSelecting](#nodifyeditor-class#isselecting) properties.  
  
```csharp  
public sealed class SelectionHelper  
```  
  
### Constructors  
  
#### SelectionHelper(NodifyEditor)  
  
Constructs a new instance of a [SelectionHelper](#selectionhelper-class).  
  
```csharp  
public SelectionHelper(NodifyEditor host);  
```  
  
**Parameters**  
  
`host` [NodifyEditor](#nodifyeditor-class): The editor to select items from.  
  
### Methods  
  
#### Abort()  
  
Aborts the current selection.  
  
```csharp  
public void Abort();  
```  
  
#### End()  
  
Commits the current selection to the editor.  
  
```csharp  
public void End();  
```  
  
#### Start(Point, SelectionType)  
  
Attempts to start a new selection.  
  
```csharp  
public void Start(Point location, SelectionType selectionType);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location inside the graph.  
  
`selectionType` [SelectionType](#selectiontype-enum): The type of selection.  
  
#### Update(Point)  
  
Update the end location for the selection.  
  
```csharp  
public void Update(Point endLocation);  
```  
  
**Parameters**  
  
`endLocation` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): An absolute location.  
  
## SelectionType Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [EditorSelectingState](#editorselectingstate-class), [SelectionHelper](#selectionhelper-class)  
  
```csharp  
public enum SelectionType  
```  
  
### Fields  
  
#### Append  
  
```csharp  
Append = 2;  
```  
  
#### Invert  
  
```csharp  
Invert = 3;  
```  
  
#### Remove  
  
```csharp  
Remove = 1;  
```  
  
#### Replace  
  
```csharp  
Replace = 0;  
```  
  
## StateNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](#connector-class) → [StateNode](#statenode-class)  
  
**References:** [Connector](#connector-class), [PendingConnection](#pendingconnection-class)  
  
Represents a control that acts as a [Connector](#connector-class).  
  
```csharp  
public class StateNode : Connector  
```  
  
### Constructors  
  
#### StateNode()  
  
```csharp  
public StateNode();  
```  
  
### Fields  
  
#### ElementContent  
  
```csharp  
protected const string ElementContent = "PART_Content";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### Properties  
  
#### Content  
  
Gets or sets the data for the control's content.  
  
```csharp  
public object Content { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
#### ContentControl  
  
Gets the [StateNode.ContentControl](#statenode-class#contentcontrol) control of this [StateNode](#statenode-class).  
  
```csharp  
protected UIElement ContentControl { get; set; }  
```  
  
**Property Value**  
  
[UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
#### ContentTemplate  
  
Gets or sets the template used to display the content of the control's header.  
  
```csharp  
public DataTemplate ContentTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
#### CornerRadius  
  
Gets or sets a value that represents the degree to which the corners of the [StateNode](#statenode-class) are rounded.  
  
```csharp  
public CornerRadius CornerRadius { get; set; }  
```  
  
**Property Value**  
  
[CornerRadius](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.CornerRadius)  
  
#### HighlightBrush  
  
Gets or sets the brush used when the [PendingConnection.IsOverElementProperty](#pendingconnection-class#isoverelementproperty) attached property is true for this [StateNode](#statenode-class).  
  
```csharp  
public Brush HighlightBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### Methods  
  
#### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
#### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
#### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
## StepConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class) → [LineConnection](#lineconnection-class) → [StepConnection](#stepconnection-class)  
  
**References:** [ConnectorPosition](#connectorposition-enum)  
  
```csharp  
public class StepConnection : LineConnection  
```  
  
### Constructors  
  
#### StepConnection()  
  
```csharp  
public StepConnection();  
```  
  
### Properties  
  
#### SourcePosition  
  
Gets or sets the position of the source connector.  
  
```csharp  
public ConnectorPosition SourcePosition { get; set; }  
```  
  
**Property Value**  
  
[ConnectorPosition](#connectorposition-enum)  
  
#### TargetPosition  
  
Gets or sets the position of the target connector.  
  
```csharp  
public ConnectorPosition TargetPosition { get; set; }  
```  
  
**Property Value**  
  
[ConnectorPosition](#connectorposition-enum)  
  
### Methods  
  
#### DrawDirectionalArrowsGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override void DrawDirectionalArrowsGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### DrawLineGeometry(StreamGeometryContext, Point, Point)  
  
```csharp  
protected override ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);  
```  
  
**Parameters**  
  
`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[ValueTuple<ValueTuple<Point, Point>, ValueTuple<Point, Point>>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)
  
#### GetTextPosition(FormattedText, Point, Point)  
  
```csharp  
protected override Point GetTextPosition(FormattedText text, Point source, Point target);  
```  
  
**Parameters**  
  
`text` [FormattedText](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.FormattedText)  
  
`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## ZoomEventArgs Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ZoomEventArgs](#zoomeventargs-class)  
  
**References:** [ZoomEventHandler](#zoomeventhandler-delegate), [Minimap](#minimap-class)  
  
Provides data for [Minimap.Zoom](#minimap-class#zoom) routed event.  
  
```csharp  
public class ZoomEventArgs : RoutedEventArgs  
```  
  
### Constructors  
  
#### ZoomEventArgs(Double, Point)  
  
Initializes a new instance of the [ZoomEventArgs](#zoomeventargs-class) class using the specified [ZoomEventArgs.Zoom](#zoomeventargs-class#zoom) and [ZoomEventArgs.Location](#zoomeventargs-class#location).  
  
```csharp  
public ZoomEventArgs(double zoom, Point location);  
```  
  
**Parameters**  
  
`zoom` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Properties  
  
#### Location  
  
Gets the location where the editor should zoom in.  
  
```csharp  
public Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
#### Zoom  
  
Gets the zoom amount.  
  
```csharp  
public double Zoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Methods  
  
#### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
## ZoomEventHandler Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [ZoomEventHandler](#zoomeventhandler-delegate)  
  
**References:** [ZoomEventArgs](#zoomeventargs-class), [Minimap](#minimap-class)  
  
Represents the method that will handle [Minimap.Zoom](#minimap-class#zoom) routed event.  
  
```csharp  
public delegate void ZoomEventHandler(object sender, ZoomEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.  
  
`e` [ZoomEventArgs](#zoomeventargs-class): The event data.  
  
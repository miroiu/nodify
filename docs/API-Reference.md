- [Alignment Enum](#alignment-enum)
- [BaseConnection Class](#baseconnection-class)
- [BoxValue Class](#boxvalue-class)
- [CircuitConnection Class](#circuitconnection-class)
- [Connection Class](#connection-class)
- [Connection Class](#connection-class)
- [ConnectionDirection Enum](#connectiondirection-enum)
- [ConnectionEventArgs Class](#connectioneventargs-class)
- [ConnectionEventHandler Delegate](#connectioneventhandler-delegate)
- [ConnectionOffsetMode Enum](#connectionoffsetmode-enum)
- [Connector Class](#connector-class)
- [Connector Class](#connector-class)
- [ConnectorEventArgs Class](#connectoreventargs-class)
- [ConnectorEventHandler Delegate](#connectoreventhandler-delegate)
- [ContainerDefaultState Class](#containerdefaultstate-class)
- [ContainerDraggingState Class](#containerdraggingstate-class)
- [ContainerState Class](#containerstate-class)
- [DecoratorContainer Class](#decoratorcontainer-class)
- [EditorCommands Class](#editorcommands-class)
- [EditorDefaultState Class](#editordefaultstate-class)
- [EditorGestures Class](#editorgestures-class)
- [EditorPanningState Class](#editorpanningstate-class)
- [EditorSelectingState Class](#editorselectingstate-class)
- [EditorState Class](#editorstate-class)
- [GeneratedInternalTypeHelper Class](#generatedinternaltypehelper-class)
- [GroupingMovementMode Enum](#groupingmovementmode-enum)
- [GroupingNode Class](#groupingnode-class)
- [GroupingNode Class](#groupingnode-class)
- [INodifyCanvasItem Interface](#inodifycanvasitem-interface)
- [ItemContainer Class](#itemcontainer-class)
- [ItemContainer Class](#itemcontainer-class)
- [KnotNode Class](#knotnode-class)
- [LineConnection Class](#lineconnection-class)
- [Match Enum](#match-enum)
- [MultiGesture Class](#multigesture-class)
- [Node Class](#node-class)
- [NodeInput Class](#nodeinput-class)
- [NodeOutput Class](#nodeoutput-class)
- [NodifyCanvas Class](#nodifycanvas-class)
- [NodifyEditor Class](#nodifyeditor-class)
- [PendingConnection Class](#pendingconnection-class)
- [PendingConnectionEventArgs Class](#pendingconnectioneventargs-class)
- [PendingConnectionEventHandler Delegate](#pendingconnectioneventhandler-delegate)
- [PreviewLocationChanged Delegate](#previewlocationchanged-delegate)
- [ResizeEventArgs Class](#resizeeventargs-class)
- [ResizeEventHandler Delegate](#resizeeventhandler-delegate)
- [Selection Class](#selection-class)
- [SelectionHelper Class](#selectionhelper-class)
- [SelectionType Enum](#selectiontype-enum)
- [StateNode Class](#statenode-class)

## Alignment Enum

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [Alignment](#alignment-enum)

```csharp
public enum Alignment
```

### Fields

#### Bottom

```csharp
public const Alignment Bottom = 2;
```

**Field Value**

[Alignment](#alignment-enum)

#### Center

```csharp
public const Alignment Center = 5;
```

**Field Value**

[Alignment](#alignment-enum)

#### Left

```csharp
public const Alignment Left = 1;
```

**Field Value**

[Alignment](#alignment-enum)

#### Middle

```csharp
public const Alignment Middle = 4;
```

**Field Value**

[Alignment](#alignment-enum)

#### Right

```csharp
public const Alignment Right = 3;
```

**Field Value**

[Alignment](#alignment-enum)

#### Top

```csharp
public const Alignment Top = 0;
```

**Field Value**

[Alignment](#alignment-enum)

## BaseConnection Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class)

**Derived:** [LineConnection](#lineconnection-class), [Connection](#connection-class)

**References:** [ConnectionEventHandler](#connectioneventhandler-delegate), [ConnectionOffsetMode](#connectionoffsetmode-enum), [ConnectionDirection](#connectiondirection-enum), [LineConnection](#lineconnection-class), [ConnectionEventArgs](#connectioneventargs-class), [NodifyEditor](#nodifyeditor-class)

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

#### ArrowSizeProperty

```csharp
public static DependencyProperty ArrowSizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DirectionProperty

```csharp
public static DependencyProperty DirectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisconnectCommandProperty

```csharp
public static DependencyProperty DisconnectCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisconnectEvent

```csharp
public static RoutedEvent DisconnectEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### OffsetModeProperty

```csharp
public static DependencyProperty OffsetModeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SourceOffsetProperty

```csharp
public static DependencyProperty SourceOffsetProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SourceProperty

```csharp
public static DependencyProperty SourceProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SpacingProperty

```csharp
public static DependencyProperty SpacingProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SplitCommandProperty

```csharp
public static DependencyProperty SplitCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SplitEvent

```csharp
public static RoutedEvent SplitEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### TargetOffsetProperty

```csharp
public static DependencyProperty TargetOffsetProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### TargetProperty

```csharp
public static DependencyProperty TargetProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ZeroVector

Gets a vector that has its coordinates set to 0.

```csharp
protected static Vector ZeroVector;
```

**Field Value**

[Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)

### Properties

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

Gets or sets the direction in which this connection is oriented.

```csharp
public ConnectionDirection Direction { get; set; }
```

**Property Value**

[ConnectionDirection](#connectiondirection-enum)

#### DisconnectCommand

Removes this connection. Triggered by Nodify.EditorGestures.Connection.Disconnect gesture.
            Parameter is the location where the disconnect ocurred.

```csharp
public ICommand DisconnectCommand { get; set; }
```

**Property Value**

[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)

#### OffsetMode

Gets or sets the [ConnectionOffsetMode](#connectionoffsetmode-enum) to apply when drawing the connection.

```csharp
public ConnectionOffsetMode OffsetMode { get; set; }
```

**Property Value**

[ConnectionOffsetMode](#connectionoffsetmode-enum)

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

#### Spacing

The distance between the start point and the where the angle breaks.

```csharp
public double Spacing { get; set; }
```

**Property Value**

[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)

#### SplitCommand

Splits the connection. Triggered by Nodify.EditorGestures.Connection.Split gesture.
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

### Methods

#### DrawArrowGeometry(StreamGeometryContext, Point, Point)

```csharp
protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target);
```

**Parameters**

`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

#### DrawLineGeometry(StreamGeometryContext, Point, Point)

```csharp
protected virtual ValueTuple<Point, Point> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);
```

**Parameters**

`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[ValueTuple<Point, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

#### GetArrowHeadPoints(Point, Point)

```csharp
protected virtual ValueTuple<Point, Point> GetArrowHeadPoints(Point source, Point target);
```

**Parameters**

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[ValueTuple<Point, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

#### GetOffset()

Gets the resulting offset after applying the [BaseConnection.OffsetMode](#baseconnection-class#offsetmode).

```csharp
protected virtual ValueTuple<Vector, Vector> GetOffset();
```

**Returns**

[ValueTuple<Vector, Vector>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

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

### Events

#### Disconnect

Triggered by the Nodify.EditorGestures.Connection.Disconnect gesture.

```csharp
public event ConnectionEventHandler Disconnect;
```

**Event Type**

[ConnectionEventHandler](#connectioneventhandler-delegate)

#### Split

Triggered by the Nodify.EditorGestures.Connection.Split gesture.

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

#### True

```csharp
public static object True;
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

#### AngleProperty

```csharp
public static DependencyProperty AngleProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

#### DrawLineGeometry(StreamGeometryContext, Point, Point)

```csharp
protected override ValueTuple<Point, Point> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);
```

**Parameters**

`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[ValueTuple<Point, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

## Connection Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](#baseconnection-class) → [Connection](#connection-class)

**References:** [Connector](#connector-class), [NodifyEditor](#nodifyeditor-class)

Represents a quadratic curve.

```csharp
public class Connection : BaseConnection
```

### Constructors

#### Connection()

```csharp
public Connection();
```

### Methods

#### DrawLineGeometry(StreamGeometryContext, Point, Point)

```csharp
protected override ValueTuple<Point, Point> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);
```

**Parameters**

`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[ValueTuple<Point, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

## Connection Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Connection](#connection-class)

```csharp
public static class Connection
```

### Properties

#### Disconnect

```csharp
public static InputGesture Disconnect { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Split

```csharp
public static InputGesture Split { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

## ConnectionDirection Enum

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [ConnectionDirection](#connectiondirection-enum)

**References:** [BaseConnection](#baseconnection-class), [PendingConnection](#pendingconnection-class)

The direction in which a connection is oriented.

```csharp
public enum ConnectionDirection
```

### Fields

#### Backward

From [BaseConnection.Target](#baseconnection-class#target) to [BaseConnection.Source](#baseconnection-class#source).

```csharp
public const ConnectionDirection Backward = 1;
```

**Field Value**

[ConnectionDirection](#connectiondirection-enum)

#### Forward

From [BaseConnection.Source](#baseconnection-class#source) to [BaseConnection.Target](#baseconnection-class#target).

```csharp
public const ConnectionDirection Forward = 0;
```

**Field Value**

[ConnectionDirection](#connectiondirection-enum)

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

## ConnectionOffsetMode Enum

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [ConnectionOffsetMode](#connectionoffsetmode-enum)

**References:** [BaseConnection](#baseconnection-class)

Specifies the offset type that can be applied to a [BaseConnection](#baseconnection-class) using the [BaseConnection.SourceOffset](#baseconnection-class#sourceoffset) and the [BaseConnection.TargetOffset](#baseconnection-class#targetoffset) values.

```csharp
public enum ConnectionOffsetMode
```

### Fields

#### Circle

The offset is applied in a circle around the point.

```csharp
public const ConnectionOffsetMode Circle = 1;
```

**Field Value**

[ConnectionOffsetMode](#connectionoffsetmode-enum)

#### Edge

The offset is applied in a rectangle shape around the point, perpendicular to the edges.

```csharp
public const ConnectionOffsetMode Edge = 3;
```

**Field Value**

[ConnectionOffsetMode](#connectionoffsetmode-enum)

#### None

No offset applied.

```csharp
public const ConnectionOffsetMode None = 0;
```

**Field Value**

[ConnectionOffsetMode](#connectionoffsetmode-enum)

#### Rectangle

The offset is applied in a rectangle shape around the point.

```csharp
public const ConnectionOffsetMode Rectangle = 2;
```

**Field Value**

[ConnectionOffsetMode](#connectionoffsetmode-enum)

## Connector Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Connector](#connector-class)

**References:** [PendingConnection](#pendingconnection-class), [Connection](#connection-class), [ItemContainer](#itemcontainer-class), [NodifyEditor](#nodifyeditor-class), [ConnectorEventHandler](#connectoreventhandler-delegate), [ConnectorEventArgs](#connectoreventargs-class), [PendingConnectionEventArgs](#pendingconnectioneventargs-class), [KnotNode](#knotnode-class), [Node](#node-class), [NodeInput](#nodeinput-class), [NodeOutput](#nodeoutput-class), [StateNode](#statenode-class)

Represents a connector control that can start and complete a [PendingConnection](#pendingconnection-class).
            Has a [Connector.ElementConnector](#connector-class#elementconnector) that the [Connector.Anchor](#connector-class#anchor) is calculated from for the [PendingConnection](#pendingconnection-class). Center of this control is used if missing.

```csharp
public static class Connector
```

### Properties

#### CancelAction

```csharp
public static InputGesture CancelAction { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Connect

```csharp
public static InputGesture Connect { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Disconnect

Triggered by the Nodify.EditorGestures.Connector.Disconnect gesture.

```csharp
public static InputGesture Disconnect { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

## Connector Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](#connector-class)

**Derived:** [NodeInput](#nodeinput-class), [NodeOutput](#nodeoutput-class), [StateNode](#statenode-class)

**References:** [PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate), [ConnectorEventHandler](#connectoreventhandler-delegate), [ItemContainer](#itemcontainer-class)

```csharp
public class Connector : Control
```

### Constructors

#### Connector()

```csharp
public Connector();
```

### Fields

#### AnchorProperty

```csharp
public static DependencyProperty AnchorProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisconnectCommandProperty

```csharp
public static DependencyProperty DisconnectCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisconnectEvent

```csharp
public static RoutedEvent DisconnectEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### ElementConnector

```csharp
protected const string ElementConnector = "PART_Connector";
```

**Field Value**

[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)

#### EnableOptimizations

```csharp
public static bool EnableOptimizations;
```

**Field Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsConnectedProperty

```csharp
public static DependencyProperty IsConnectedProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsPendingConnectionProperty

```csharp
public static DependencyProperty IsPendingConnectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### OptimizeMinimumSelectedItems

```csharp
public static uint OptimizeMinimumSelectedItems;
```

**Field Value**

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)

#### OptimizeSafeZone

```csharp
public static double OptimizeSafeZone;
```

**Field Value**

[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)

#### PendingConnectionCompletedEvent

```csharp
public static RoutedEvent PendingConnectionCompletedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### PendingConnectionDragEvent

```csharp
public static RoutedEvent PendingConnectionDragEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### PendingConnectionStartedEvent

```csharp
public static RoutedEvent PendingConnectionStartedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

### Properties

#### AllowPendingConnectionCancellation

```csharp
public static bool AllowPendingConnectionCancellation { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### Anchor

```csharp
public Point Anchor { get; set; }
```

**Property Value**

[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

#### Container

```csharp
protected ItemContainer Container { get; set; }
```

**Property Value**

[ItemContainer](#itemcontainer-class)

#### DisconnectCommand

```csharp
public ICommand DisconnectCommand { get; set; }
```

**Property Value**

[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)

#### EnableStickyConnections

```csharp
public static bool EnableStickyConnections { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsConnected

```csharp
public bool IsConnected { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsPendingConnection

```csharp
public bool IsPendingConnection { get; protected set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### Thumb

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

```csharp
protected void UpdateAnchor(Point location);
```

**Parameters**

`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

#### UpdateAnchor()

```csharp
public void UpdateAnchor();
```

#### UpdateAnchorOptimized(Point)

```csharp
protected void UpdateAnchorOptimized(Point location);
```

**Parameters**

`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

### Events

#### Disconnect

```csharp
public event ConnectorEventHandler Disconnect;
```

**Event Type**

[ConnectorEventHandler](#connectoreventhandler-delegate)

#### PendingConnectionCompleted

```csharp
public event PendingConnectionEventHandler PendingConnectionCompleted;
```

**Event Type**

[PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate)

#### PendingConnectionDrag

```csharp
public event PendingConnectionEventHandler PendingConnectionDrag;
```

**Event Type**

[PendingConnectionEventHandler](#pendingconnectioneventhandler-delegate)

#### PendingConnectionStarted

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

**References:** [ConnectorEventArgs](#connectoreventargs-class), [Connector](#connector-class), [Connector](#connector-class)

Represents the method that will handle [Connector](#connector-class) related routed events.

```csharp
public delegate void ConnectorEventHandler(object sender, ConnectorEventArgs e);
```

**Parameters**

`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object): The object where the event handler is attached.

`e` [ConnectorEventArgs](#connectoreventargs-class): The event data.

## ContainerDefaultState Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ContainerState](#containerstate-class) → [ContainerDefaultState](#containerdefaultstate-class)

**References:** [ItemContainer](#itemcontainer-class), [ContainerState](#containerstate-class), [ItemContainer](#itemcontainer-class)

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

### Fields

#### ActualSizeProperty

```csharp
public static DependencyProperty ActualSizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### LocationChangedEvent

```csharp
public static RoutedEvent LocationChangedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### LocationProperty

```csharp
public static DependencyProperty LocationProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

**References:** [ItemContainer](#itemcontainer-class), [NodifyEditor](#nodifyeditor-class)

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

## EditorDefaultState Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorState](#editorstate-class) → [EditorDefaultState](#editordefaultstate-class)

**References:** [NodifyEditor](#nodifyeditor-class)

The default state of the editor.
              Default State
               - mouse left down   -> Selecting State
               - mouse right down  -> Panning State
              Selecting State
               - mouse left up  -> Default State
               - mouse right down  -> Panning State
              Panning State
               - mouse right up -> previous state (Selecting State or Default State)
               - mouse left up  -> Panning State

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

**References:** [NodifyEditor](#nodifyeditor-class)

Gestures used by the [NodifyEditor](#nodifyeditor-class).

```csharp
public static class EditorGestures
```

### Properties

#### FitToScreen

Gesture used to fit as many containers as possible into the viewport.

```csharp
public static InputGesture FitToScreen { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Pan

Gesture used to start panning.

```csharp
public static InputGesture Pan { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### ResetViewportLocation

Gesture used to move the editor's viewport location to (0, 0).

```csharp
public static InputGesture ResetViewportLocation { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Select

Gesture used to start selecting using a Nodify.EditorGestures.Selection strategy.

```csharp
public static InputGesture Select { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Zoom

The key modifier required to start zooming by mouse wheel.

```csharp
public static ModifierKeys Zoom { get; set; }
```

**Property Value**

[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)

#### ZoomIn

Gesture used to zoom in.

```csharp
public static InputGesture ZoomIn { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### ZoomOut

Gesture used to zoom out.

```csharp
public static InputGesture ZoomOut { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

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

`type` [SelectionType](#selectiontype-enum)

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

**Derived:** [EditorDefaultState](#editordefaultstate-class), [EditorPanningState](#editorpanningstate-class), [EditorSelectingState](#editorselectingstate-class)

**References:** [EditorPanningState](#editorpanningstate-class), [EditorSelectingState](#editorselectingstate-class), [NodifyEditor](#nodifyeditor-class)

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

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [GroupingMovementMode](#groupingmovementmode-enum)

**References:** [GroupingNode](#groupingnode-class), [GroupingNode](#groupingnode-class)

Specifies the possible movement modes of a [GroupingNode](#groupingnode-class).

```csharp
public enum GroupingMovementMode
```

### Fields

#### Group

The [GroupingNode](#groupingnode-class) will move its content when moved.

```csharp
public const GroupingMovementMode Group = 0;
```

**Field Value**

[GroupingMovementMode](#groupingmovementmode-enum)

#### Self

The [GroupingNode](#groupingnode-class) will not move its content when moved.

```csharp
public const GroupingMovementMode Self = 1;
```

**Field Value**

[GroupingMovementMode](#groupingmovementmode-enum)

## GroupingNode Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [GroupingNode](#groupingnode-class)

**References:** [GroupingMovementMode](#groupingmovementmode-enum), [ItemContainer](#itemcontainer-class), [NodifyEditor](#nodifyeditor-class)

Defines a panel with a header that groups [ItemContainer](#itemcontainer-class)s inside it and can be resized.

```csharp
public static class GroupingNode
```

### Properties

#### SwitchMovementMode

```csharp
public static ModifierKeys SwitchMovementMode { get; set; }
```

**Property Value**

[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)

## GroupingNode Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [GroupingNode](#groupingnode-class)

**References:** [ResizeEventHandler](#resizeeventhandler-delegate), [GroupingMovementMode](#groupingmovementmode-enum), [NodifyEditor](#nodifyeditor-class), [ItemContainer](#itemcontainer-class)

```csharp
public class GroupingNode : HeaderedContentControl
```

### Constructors

#### GroupingNode()

```csharp
public GroupingNode();
```

### Fields

#### ActualSizeProperty

```csharp
public static DependencyProperty ActualSizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### CanResizeProperty

```csharp
public static DependencyProperty CanResizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ContentControl

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

#### HeaderBrushProperty

```csharp
public static DependencyProperty HeaderBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HeaderControl

```csharp
protected FrameworkElement HeaderControl;
```

**Field Value**

[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)

#### MovementModeProperty

```csharp
public static DependencyProperty MovementModeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ResizeCompletedCommandProperty

```csharp
public static DependencyProperty ResizeCompletedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ResizeCompletedEvent

```csharp
public static RoutedEvent ResizeCompletedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### ResizeStartedCommandProperty

```csharp
public static DependencyProperty ResizeStartedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ResizeStartedEvent

```csharp
public static RoutedEvent ResizeStartedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### ResizeThumb

```csharp
protected FrameworkElement ResizeThumb;
```

**Field Value**

[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)

### Properties

#### ActualSize

```csharp
public Size ActualSize { get; set; }
```

**Property Value**

[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)

#### CanResize

```csharp
public bool CanResize { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### Container

```csharp
protected ItemContainer Container { get; set; }
```

**Property Value**

[ItemContainer](#itemcontainer-class)

#### Editor

```csharp
protected NodifyEditor Editor { get; set; }
```

**Property Value**

[NodifyEditor](#nodifyeditor-class)

#### HeaderBrush

```csharp
public Brush HeaderBrush { get; set; }
```

**Property Value**

[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)

#### MovementMode

```csharp
public GroupingMovementMode MovementMode { get; set; }
```

**Property Value**

[GroupingMovementMode](#groupingmovementmode-enum)

#### ResizeCompletedCommand

```csharp
public ICommand ResizeCompletedCommand { get; set; }
```

**Property Value**

[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)

#### ResizeStartedCommand

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

```csharp
public event ResizeEventHandler ResizeCompleted;
```

**Event Type**

[ResizeEventHandler](#resizeeventhandler-delegate)

#### ResizeStarted

```csharp
public event ResizeEventHandler ResizeStarted;
```

**Event Type**

[ResizeEventHandler](#resizeeventhandler-delegate)

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

## ItemContainer Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ItemContainer](#itemcontainer-class)

**References:** [Connector](#connector-class), [PendingConnection](#pendingconnection-class), [EditorCommands](#editorcommands-class), [ContainerDefaultState](#containerdefaultstate-class), [SelectionHelper](#selectionhelper-class), [PreviewLocationChanged](#previewlocationchanged-delegate), [NodifyEditor](#nodifyeditor-class), [GroupingNode](#groupingnode-class)

The container for all the items generated by the [ItemsControl.ItemsSource](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl.itemssource) of the [NodifyEditor](#nodifyeditor-class).

```csharp
public static class ItemContainer
```

### Properties

#### CancelAction

```csharp
public static InputGesture CancelAction { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Drag

```csharp
public static InputGesture Drag { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Select

```csharp
public static InputGesture Select { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

## ItemContainer Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [ItemContainer](#itemcontainer-class)

**Implements:** [INodifyCanvasItem](#inodifycanvasitem-interface)

**References:** [ContainerDefaultState](#containerdefaultstate-class), [ContainerDraggingState](#containerdraggingstate-class), [ContainerState](#containerstate-class), [Connector](#connector-class), [NodifyEditor](#nodifyeditor-class), [GroupingNode](#groupingnode-class), [PreviewLocationChanged](#previewlocationchanged-delegate)

```csharp
public class ItemContainer : ContentControl, INodifyCanvasItem
```

### Constructors

#### ItemContainer(NodifyEditor)

```csharp
public ItemContainer(NodifyEditor editor);
```

**Parameters**

`editor` [NodifyEditor](#nodifyeditor-class)

### Fields

#### ActualSizeProperty

```csharp
public static DependencyProperty ActualSizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DesiredSizeForSelectionProperty

```csharp
public static DependencyProperty DesiredSizeForSelectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DragCompletedEvent

```csharp
public static RoutedEvent DragCompletedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### DragDeltaEvent

```csharp
public static RoutedEvent DragDeltaEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### DragStartedEvent

```csharp
public static RoutedEvent DragStartedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### HighlightBrushProperty

```csharp
public static DependencyProperty HighlightBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsDraggableProperty

```csharp
public static DependencyProperty IsDraggableProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsPreviewingLocationProperty

```csharp
public static DependencyProperty IsPreviewingLocationProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsPreviewingLocationPropertyKey

```csharp
public static DependencyPropertyKey IsPreviewingLocationPropertyKey;
```

**Field Value**

[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)

#### IsPreviewingSelectionProperty

```csharp
public static DependencyProperty IsPreviewingSelectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsPreviewingSelectionPropertyKey

```csharp
public static DependencyPropertyKey IsPreviewingSelectionPropertyKey;
```

**Field Value**

[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)

#### IsSelectableProperty

```csharp
public static DependencyProperty IsSelectableProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsSelectedProperty

```csharp
public static DependencyProperty IsSelectedProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### LocationChangedEvent

```csharp
public static RoutedEvent LocationChangedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### LocationProperty

```csharp
public static DependencyProperty LocationProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SelectedBrushProperty

```csharp
public static DependencyProperty SelectedBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SelectedEvent

```csharp
public static RoutedEvent SelectedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### UnselectedEvent

```csharp
public static RoutedEvent UnselectedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

### Properties

#### ActualSize

```csharp
public Size ActualSize { get; set; }
```

**Property Value**

[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)

#### AllowDraggingCancellation

```csharp
public static bool AllowDraggingCancellation { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### DesiredSizeForSelection

```csharp
public Size? DesiredSizeForSelection { get; set; }
```

**Property Value**

[Size?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable)

#### Editor

```csharp
public NodifyEditor Editor { get; set; }
```

**Property Value**

[NodifyEditor](#nodifyeditor-class)

#### HighlightBrush

```csharp
public Brush HighlightBrush { get; set; }
```

**Property Value**

[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)

#### IsDraggable

```csharp
public bool IsDraggable { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsPreviewingLocation

```csharp
public bool IsPreviewingLocation { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsPreviewingSelection

```csharp
public Boolean? IsPreviewingSelection { get; set; }
```

**Property Value**

[Boolean?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable)

#### IsSelectable

```csharp
public bool IsSelectable { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsSelected

```csharp
public bool IsSelected { get; set; }
```

**Property Value**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### Location

```csharp
public virtual Point Location { get; set; }
```

**Property Value**

[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

#### SelectedBrush

```csharp
public Brush SelectedBrush { get; set; }
```

**Property Value**

[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)

#### State

```csharp
public ContainerState State { get; set; }
```

**Property Value**

[ContainerState](#containerstate-class)

### Methods

#### GetInitialState()

```csharp
protected virtual ContainerState GetInitialState();
```

**Returns**

[ContainerState](#containerstate-class)

#### IsSelectableInArea(Rect, Boolean)

```csharp
public virtual bool IsSelectableInArea(Rect area, bool isContained);
```

**Parameters**

`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)

`isContained` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

**Returns**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### IsSelectableLocation(Point)

```csharp
protected virtual bool IsSelectableLocation(Point position);
```

**Parameters**

`position` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

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

```csharp
protected void OnSelectedChanged(bool newValue);
```

**Parameters**

`newValue` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)

#### PopAllStates()

```csharp
public void PopAllStates();
```

#### PopState()

```csharp
public void PopState();
```

#### PushState(ContainerState)

```csharp
public void PushState(ContainerState state);
```

**Parameters**

`state` [ContainerState](#containerstate-class)

### Events

#### DragCompleted

```csharp
public event DragCompletedEventHandler DragCompleted;
```

**Event Type**

[DragCompletedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.DragCompletedEventHandler)

#### DragDelta

```csharp
public event DragDeltaEventHandler DragDelta;
```

**Event Type**

[DragDeltaEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.DragDeltaEventHandler)

#### DragStarted

```csharp
public event DragStartedEventHandler DragStarted;
```

**Event Type**

[DragStartedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.DragStartedEventHandler)

#### LocationChanged

```csharp
public event RoutedEventHandler LocationChanged;
```

**Event Type**

[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)

#### PreviewLocationChanged

```csharp
public event PreviewLocationChanged PreviewLocationChanged;
```

**Event Type**

[PreviewLocationChanged](#previewlocationchanged-delegate)

#### Selected

```csharp
public event RoutedEventHandler Selected;
```

**Event Type**

[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)

#### Unselected

```csharp
public event RoutedEventHandler Unselected;
```

**Event Type**

[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)

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

**Derived:** [CircuitConnection](#circuitconnection-class)

**References:** [BaseConnection](#baseconnection-class)

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

#### DrawLineGeometry(StreamGeometryContext, Point, Point)

```csharp
protected override ValueTuple<Point, Point> DrawLineGeometry(StreamGeometryContext context, Point source, Point target);
```

**Parameters**

`context` [StreamGeometryContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.StreamGeometryContext)

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[ValueTuple<Point, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

#### GetArrowHeadPoints(Point, Point)

```csharp
protected override ValueTuple<Point, Point> GetArrowHeadPoints(Point source, Point target);
```

**Parameters**

`source` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

`target` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)

**Returns**

[ValueTuple<Point, Point>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)

## Match Enum

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [Match](#match-enum)

**References:** [MultiGesture](#multigesture-class)

```csharp
public enum Match
```

### Fields

#### All

```csharp
public const Match All = 1;
```

**Field Value**

[Match](#match-enum)

#### Any

```csharp
public const Match Any = 0;
```

**Field Value**

[Match](#match-enum)

## MultiGesture Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [MultiGesture](#multigesture-class)

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

### Fields

#### ContentBrushProperty

```csharp
public static DependencyProperty ContentBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### FooterBrushProperty

```csharp
public static DependencyProperty FooterBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### FooterProperty

```csharp
public static DependencyProperty FooterProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### FooterTemplateProperty

```csharp
public static DependencyProperty FooterTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HasFooterProperty

```csharp
public static DependencyProperty HasFooterProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HeaderBrushProperty

```csharp
public static DependencyProperty HeaderBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### InputConnectorTemplateProperty

```csharp
public static DependencyProperty InputConnectorTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### InputProperty

```csharp
public static DependencyProperty InputProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### OutputConnectorTemplateProperty

```csharp
public static DependencyProperty OutputConnectorTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### OutputProperty

```csharp
public static DependencyProperty OutputProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

### Properties

#### ContentBrush

Gets or sets the brush used for the background of the [ContentControl.Content](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl.content) of this [Node](#node-class).

```csharp
public Brush ContentBrush { get; set; }
```

**Property Value**

[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)

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

### Fields

#### ConnectorTemplateProperty

```csharp
public static DependencyProperty ConnectorTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HeaderProperty

```csharp
public static DependencyProperty HeaderProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HeaderTemplateProperty

```csharp
public static DependencyProperty HeaderTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

### Fields

#### ConnectorTemplateProperty

```csharp
public static DependencyProperty ConnectorTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HeaderProperty

```csharp
public static DependencyProperty HeaderProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### HeaderTemplateProperty

```csharp
public static DependencyProperty HeaderTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

### Fields

#### ExtentProperty

```csharp
public static DependencyProperty ExtentProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

**References:** [ContainerState](#containerstate-class), [EditorDefaultState](#editordefaultstate-class), [EditorPanningState](#editorpanningstate-class), [EditorSelectingState](#editorselectingstate-class), [EditorState](#editorstate-class), [SelectionHelper](#selectionhelper-class), [ItemContainer](#itemcontainer-class), [PendingConnection](#pendingconnection-class), [GroupingNode](#groupingnode-class), [Connector](#connector-class), [DecoratorContainer](#decoratorcontainer-class), [EditorCommands](#editorcommands-class), [EditorGestures](#editorgestures-class), [ItemContainer](#itemcontainer-class), [GroupingNode](#groupingnode-class), [Connection](#connection-class), [BaseConnection](#baseconnection-class)

Groups [ItemContainer](#itemcontainer-class)s and [Connection](#connection-class)s in an area that you can drag, zoom and select.

```csharp
public class NodifyEditor : MultiSelector
```

### Constructors

#### NodifyEditor()

Initializes a new instance of the [NodifyEditor](#nodifyeditor-class) class.

```csharp
public NodifyEditor();
```

### Fields

#### AutoPanEdgeDistanceProperty

```csharp
public static DependencyProperty AutoPanEdgeDistanceProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### AutoPanSpeedProperty

```csharp
public static DependencyProperty AutoPanSpeedProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### BringIntoViewMaxDurationProperty

```csharp
public static DependencyProperty BringIntoViewMaxDurationProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### BringIntoViewSpeedProperty

```csharp
public static DependencyProperty BringIntoViewSpeedProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ConnectionCompletedCommandProperty

```csharp
public static DependencyProperty ConnectionCompletedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ConnectionsProperty

```csharp
public static DependencyProperty ConnectionsProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ConnectionStartedCommandProperty

```csharp
public static DependencyProperty ConnectionStartedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ConnectionTemplateProperty

```csharp
public static DependencyProperty ConnectionTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DecoratorContainerStyleProperty

```csharp
public static DependencyProperty DecoratorContainerStyleProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DecoratorsExtentProperty

```csharp
public static DependencyProperty DecoratorsExtentProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DecoratorsProperty

```csharp
public static DependencyProperty DecoratorsProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DecoratorTemplateProperty

```csharp
public static DependencyProperty DecoratorTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisableAutoPanningProperty

```csharp
public static DependencyProperty DisableAutoPanningProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisablePanningProperty

```csharp
public static DependencyProperty DisablePanningProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisableZoomingProperty

```csharp
public static DependencyProperty DisableZoomingProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisconnectConnectorCommandProperty

```csharp
public static DependencyProperty DisconnectConnectorCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DisplayConnectionsOnTopProperty

```csharp
public static DependencyProperty DisplayConnectionsOnTopProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ElementItemsHost

```csharp
protected const string ElementItemsHost = "PART_ItemsHost";
```

**Field Value**

[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)

#### EnableRealtimeSelectionProperty

```csharp
public static DependencyProperty EnableRealtimeSelectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### GridCellSizeProperty

```csharp
public static DependencyProperty GridCellSizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsPanningProperty

```csharp
public static DependencyProperty IsPanningProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsPanningPropertyKey

```csharp
public static DependencyPropertyKey IsPanningPropertyKey;
```

**Field Value**

[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)

#### IsSelectingProperty

```csharp
public static DependencyProperty IsSelectingProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsSelectingPropertyKey

```csharp
protected static DependencyPropertyKey IsSelectingPropertyKey;
```

**Field Value**

[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)

#### ItemsDragCompletedCommandProperty

```csharp
public static DependencyProperty ItemsDragCompletedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ItemsDragStartedCommandProperty

```csharp
public static DependencyProperty ItemsDragStartedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ItemsExtentProperty

```csharp
public static DependencyProperty ItemsExtentProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### MaxViewportZoomProperty

```csharp
public static DependencyProperty MaxViewportZoomProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### MinViewportZoomProperty

```csharp
public static DependencyProperty MinViewportZoomProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### MouseLocationProperty

```csharp
public static DependencyProperty MouseLocationProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### MouseLocationPropertyKey

```csharp
protected static DependencyPropertyKey MouseLocationPropertyKey;
```

**Field Value**

[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)

#### PendingConnectionProperty

```csharp
public static DependencyProperty PendingConnectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### PendingConnectionTemplateProperty

```csharp
public static DependencyProperty PendingConnectionTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### RemoveConnectionCommandProperty

```csharp
public static DependencyProperty RemoveConnectionCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ScaleTransform

Gets the transform used to zoom on the viewport.

```csharp
protected readonly ScaleTransform ScaleTransform;
```

**Field Value**

[ScaleTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.ScaleTransform)

#### SelectedAreaProperty

```csharp
public static DependencyProperty SelectedAreaProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SelectedAreaPropertyKey

```csharp
protected static DependencyPropertyKey SelectedAreaPropertyKey;
```

**Field Value**

[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)

#### SelectedItemsProperty

```csharp
public static DependencyProperty SelectedItemsProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SelectionRectangleStyleProperty

```csharp
public static DependencyProperty SelectionRectangleStyleProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### TranslateTransform

Gets the transform used to offset the viewport.

```csharp
protected readonly TranslateTransform TranslateTransform;
```

**Field Value**

[TranslateTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.TranslateTransform)

#### ViewportLocationProperty

```csharp
public static DependencyProperty ViewportLocationProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ViewportSizeProperty

```csharp
public static DependencyProperty ViewportSizeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ViewportTransformProperty

```csharp
public static DependencyProperty ViewportTransformProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ViewportUpdatedEvent

```csharp
public static RoutedEvent ViewportUpdatedEvent;
```

**Field Value**

[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)

#### ViewportZoomProperty

```csharp
public static DependencyProperty ViewportZoomProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

Gets or sets the minimum selected [ItemContainer](#itemcontainer-class)s needed to trigger optimizations when reaching the [NodifyEditor.OptimizeRenderingZoomOutPercent](#nodifyeditor-class#optimizerenderingzoomoutpercent).

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

#### SelectedArea

Gets the currently selected area while [NodifyEditor.IsSelecting](#nodifyeditor-class#isselecting) is true.

```csharp
public Rect SelectedArea { get; set; }
```

**Property Value**

[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)

#### SelectedItems

Gets or sets the items in the [NodifyEditor](#nodifyeditor-class) that are selected.

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

Gets the size of the viewport.

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

#### InvertSelection(Rect, Boolean)

Inverts the [ItemContainer](#itemcontainer-class)s selection in the specified area.

```csharp
public void InvertSelection(Rect area, bool fit = false);
```

**Parameters**

`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](#itemcontainer-class)s.

`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](#itemcontainer-class).  False to check if area intersects the [ItemContainer](#itemcontainer-class).

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

#### SelectArea(Rect, Boolean, Boolean)

Selects the [ItemContainer](#itemcontainer-class)s in the specified area.

```csharp
public void SelectArea(Rect area, bool append = false, bool fit = false);
```

**Parameters**

`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](#itemcontainer-class)s.

`append` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): If true, it will add to the existing selection.

`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](#itemcontainer-class).  False to check if area intersects the [ItemContainer](#itemcontainer-class).

#### UnselectArea(Rect, Boolean)

Unselect the [ItemContainer](#itemcontainer-class)s in the specified area.

```csharp
public void UnselectArea(Rect area, bool fit = false);
```

**Parameters**

`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](#itemcontainer-class)s.

`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](#itemcontainer-class).  False to check if area intersects the [ItemContainer](#itemcontainer-class).

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

### Fields

#### AllowOnlyConnectorsProperty

```csharp
public static DependencyProperty AllowOnlyConnectorsProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### CompletedCommandProperty

```csharp
public static DependencyProperty CompletedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### DirectionProperty

```csharp
public static DependencyProperty DirectionProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### EnablePreviewProperty

```csharp
public static DependencyProperty EnablePreviewProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### EnableSnappingProperty

```csharp
public static DependencyProperty EnableSnappingProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsOverElementProperty

Will be set for [Connector](#connector-class)s and [ItemContainer](#itemcontainer-class)s when the pending connection is over the element if [PendingConnection.EnablePreview](#pendingconnection-class#enablepreview) or [PendingConnection.EnableSnapping](#pendingconnection-class#enablesnapping) is true.

```csharp
public static DependencyProperty IsOverElementProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### IsVisibleProperty

```csharp
public static DependencyProperty IsVisibleProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### PreviewTargetProperty

```csharp
public static DependencyProperty PreviewTargetProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SourceAnchorProperty

```csharp
public static DependencyProperty SourceAnchorProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### SourceProperty

```csharp
public static DependencyProperty SourceProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### StartedCommandProperty

```csharp
public static DependencyProperty StartedCommandProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### StrokeDashArrayProperty

```csharp
public static DependencyProperty StrokeDashArrayProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### StrokeProperty

```csharp
public static DependencyProperty StrokeProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### StrokeThicknessProperty

```csharp
public static DependencyProperty StrokeThicknessProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### TargetAnchorProperty

```csharp
public static DependencyProperty TargetAnchorProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### TargetProperty

```csharp
public static DependencyProperty TargetProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

**References:** [ItemContainer](#itemcontainer-class), [ItemContainer](#itemcontainer-class)

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

## Selection Class

**Namespace:** Nodify

**Assembly:** Nodify

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Selection](#selection-class)

```csharp
public static class Selection
```

### Properties

#### Append

```csharp
public static InputGesture Append { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Invert

```csharp
public static InputGesture Invert { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Remove

```csharp
public static InputGesture Remove { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

#### Replace

```csharp
public static InputGesture Replace { get; set; }
```

**Property Value**

[InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture)

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

**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [SelectionType](#selectiontype-enum)

**References:** [EditorSelectingState](#editorselectingstate-class), [SelectionHelper](#selectionhelper-class)

```csharp
public enum SelectionType
```

### Fields

#### Append

```csharp
public const SelectionType Append = 2;
```

**Field Value**

[SelectionType](#selectiontype-enum)

#### Invert

```csharp
public const SelectionType Invert = 3;
```

**Field Value**

[SelectionType](#selectiontype-enum)

#### Remove

```csharp
public const SelectionType Remove = 1;
```

**Field Value**

[SelectionType](#selectiontype-enum)

#### Replace

```csharp
public const SelectionType Replace = 0;
```

**Field Value**

[SelectionType](#selectiontype-enum)

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

#### ContentProperty

```csharp
public static DependencyProperty ContentProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ContentTemplateProperty

```csharp
public static DependencyProperty ContentTemplateProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### CornerRadiusProperty

```csharp
public static DependencyProperty CornerRadiusProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

#### ElementContent

```csharp
protected const string ElementContent = "PART_Content";
```

**Field Value**

[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)

#### HighlightBrushProperty

```csharp
public static DependencyProperty HighlightBrushProperty;
```

**Field Value**

[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)

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

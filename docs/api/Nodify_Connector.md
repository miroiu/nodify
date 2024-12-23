# Connector Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](Nodify_Connector)  
  
**Derived:** [NodeInput](Nodify_NodeInput), [NodeOutput](Nodify_NodeOutput), [StateNode](Nodify_StateNode)  
  
**References:** [ConnectorState.Connecting](Nodify_Interactivity_ConnectorState_Connecting), [Connection](Nodify_Connection), [ConnectorEventArgs](Nodify_Events_ConnectorEventArgs), [ConnectorEventHandler](Nodify_Events_ConnectorEventHandler), [ConnectorState.Default](Nodify_Interactivity_ConnectorState_Default), [ConnectorState.Disconnect](Nodify_Interactivity_ConnectorState_Disconnect), [InputProcessor](Nodify_Interactivity_InputProcessor), [ItemContainer](Nodify_ItemContainer), [KnotNode](Nodify_KnotNode), [Node](Nodify_Node), [NodifyEditor](Nodify_NodifyEditor), [PendingConnection](Nodify_PendingConnection), [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs), [PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler)  
  
Represents a connector control that can start and complete a [PendingConnection](Nodify_PendingConnection).
            Has a [Connector.ElementConnector](Nodify_Connector#elementconnector) that the [Connector.Anchor](Nodify_Connector#anchor) is calculated from for the [PendingConnection](Nodify_PendingConnection). Center of this control is used if missing.  
  
```csharp  
public class Connector : Control  
```  
  
## Constructors  
  
### Connector()  
  
```csharp  
public Connector();  
```  
  
## Fields  
  
### ElementConnector  
  
```csharp  
protected const string ElementConnector = "PART_Connector";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### EnableOptimizations  
  
Gets or sets if [Connector](Nodify_Connector)s should enable optimizations based on [Connector.OptimizeSafeZone](Nodify_Connector#optimizesafezone) and [Connector.OptimizeMinimumSelectedItems](Nodify_Connector#optimizeminimumselecteditems).  
  
```csharp  
public static bool EnableOptimizations;  
```  
  
**Field Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OptimizeMinimumSelectedItems  
  
Gets or sets the minimum selected items needed to trigger optimizations when outside of the [Connector.OptimizeSafeZone](Nodify_Connector#optimizesafezone).  
  
```csharp  
public static uint OptimizeMinimumSelectedItems;  
```  
  
**Field Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### OptimizeSafeZone  
  
Gets or sets the safe zone outside the editor's viewport that will not trigger optimizations.  
  
```csharp  
public static double OptimizeSafeZone;  
```  
  
**Field Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Properties  
  
### AllowPendingConnectionCancellation  
  
Gets or sets whether cancelling a pending connection is allowed.  
  
```csharp  
public static bool AllowPendingConnectionCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Anchor  
  
Gets the location in graph space coordinates where [Connection](Nodify_Connection)s can be attached to. 
            Bind with System.Windows.Data.BindingMode.OneWayToSource  
  
```csharp  
public Point Anchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Container  
  
Gets the [ItemContainer](Nodify_ItemContainer) that contains this [Connector](Nodify_Connector).  
  
```csharp  
public ItemContainer Container { get; set; }  
```  
  
**Property Value**  
  
[ItemContainer](Nodify_ItemContainer)  
  
### DisconnectCommand  
  
Invoked if the [Connector.Disconnect](Nodify_Connector#disconnect) event is not handled.
            Parameter is the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of this control.  
  
```csharp  
public ICommand DisconnectCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Editor  
  
Gets the [NodifyEditor](Nodify_NodifyEditor) that owns this [Connector.Container](Nodify_Connector#container).  
  
```csharp  
public NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](Nodify_NodifyEditor)  
  
### HasContextMenu  
  
Gets a value indicating whether the connector has a context menu.  
  
```csharp  
public bool HasContextMenu { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### HasCustomContextMenu  
  
Gets or sets a value indicating whether the connector uses a custom context menu.  
  
```csharp  
public bool HasCustomContextMenu { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### InputProcessor  
  
```csharp  
protected InputProcessor InputProcessor { get; set; }  
```  
  
**Property Value**  
  
[InputProcessor](Nodify_Interactivity_InputProcessor)  
  
### IsConnected  
  
If this is set to false, the [Connector.Disconnect](Nodify_Connector#disconnect) event will not be invoked and the connector will stop updating its [Connector.Anchor](Nodify_Connector#anchor) when moved, resized etc.  
  
```csharp  
public bool IsConnected { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsPendingConnection  
  
Gets a value that indicates whether a [PendingConnection](Nodify_PendingConnection) is in progress for this [Connector](Nodify_Connector).  
  
```csharp  
public bool IsPendingConnection { get; protected set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
## Methods  
  
### BeginConnecting()  
  
Initiates a new pending connection from this connector with the specified offset (see [Connector.IsPendingConnection](Nodify_Connector#ispendingconnection)).  
  
```csharp  
public void BeginConnecting();  
```  
  
### BeginConnecting(Vector)  
  
```csharp  
public void BeginConnecting(Vector offset);  
```  
  
**Parameters**  
  
`offset` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
### CancelConnecting()  
  
Cancels the current pending connection without completing it if [Connector.AllowPendingConnectionCancellation](Nodify_Connector#allowpendingconnectioncancellation) is true.
            Otherwise, it completes the pending connection by calling Nodify.Connector.EndConnecting.  
  
```csharp  
public void CancelConnecting();  
```  
  
### EndConnecting()  
  
Completes the current pending connection using the specified connector as the target.  
  
```csharp  
public void EndConnecting();  
```  
  
### EndConnecting(Connector)  
  
```csharp  
public void EndConnecting(Connector connector);  
```  
  
**Parameters**  
  
`connector` [Connector](Nodify_Connector)  
  
### FindConnectionTarget(Point)  
  
Searches for a potential [Connector](Nodify_Connector) or [ItemContainer](Nodify_ItemContainer) at the specified position within the editor.  
  
```csharp  
public FrameworkElement FindConnectionTarget(Point position);  
```  
  
**Parameters**  
  
`position` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The position in the editor to check for a potential connection target.  
  
**Returns**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### FindTargetConnector(Point)  
  
Searches for a [Connector](Nodify_Connector) at the specified position.  
  
```csharp  
public Connector FindTargetConnector(Point position);  
```  
  
**Parameters**  
  
`position` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The position in the editor to check for a connector.  
  
**Returns**  
  
[Connector](Nodify_Connector)  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
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
  
### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
### RemoveConnections()  
  
Removes all connections associated with this connector.  
  
```csharp  
public void RemoveConnections();  
```  
  
### UpdateAnchor(Point)  
  
Updates the [Connector.Anchor](Nodify_Connector#anchor) relative to a location. (usually [Connector.Container](Nodify_Connector#container)'s location)  
  
```csharp  
protected void UpdateAnchor(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The relative location  
  
### UpdateAnchor()  
  
Updates the [Connector.Anchor](Nodify_Connector#anchor) based on [Connector.Container](Nodify_Connector#container)'s location.  
  
```csharp  
public void UpdateAnchor();  
```  
  
### UpdateAnchorOptimized(Point)  
  
Updates the [Connector.Anchor](Nodify_Connector#anchor) and applies optimizations if needed based on [Connector.EnableOptimizations](Nodify_Connector#enableoptimizations) flag  
  
```csharp  
protected void UpdateAnchorOptimized(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### UpdatePendingConnection(Vector)  
  
Updates the endpoint of the pending connection by adjusting its position with the specified offset.  
  
```csharp  
public void UpdatePendingConnection(Vector offset);  
```  
  
**Parameters**  
  
`offset` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The amount to adjust the pending connection's endpoint.  
  
### UpdatePendingConnection(Point)  
  
Updates the endpoint of the pending connection to the specified position.  
  
```csharp  
public void UpdatePendingConnection(Point position);  
```  
  
**Parameters**  
  
`position` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The new position for the connection's endpoint.  
  
## Events  
  
### Disconnect  
  
Triggered by the Nodify.Interactivity.EditorGestures.ConnectorGestures.Disconnect gesture.  
  
```csharp  
public event ConnectorEventHandler Disconnect;  
```  
  
**Event Type**  
  
[ConnectorEventHandler](Nodify_Events_ConnectorEventHandler)  
  
### PendingConnectionCompleted  
  
Triggered by the Nodify.Interactivity.EditorGestures.ConnectorGestures.Connect gesture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionCompleted;  
```  
  
**Event Type**  
  
[PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler)  
  
### PendingConnectionDrag  
  
Occurs when the mouse is changing position and the [Connector](Nodify_Connector) has mouse capture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionDrag;  
```  
  
**Event Type**  
  
[PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler)  
  
### PendingConnectionStarted  
  
Triggered by the Nodify.Interactivity.EditorGestures.ConnectorGestures.Connect gesture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionStarted;  
```  
  
**Event Type**  
  
[PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler)  
  

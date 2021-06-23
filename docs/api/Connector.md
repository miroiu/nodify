# Connector Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](Connector)  
  
**Derived:** [NodeInput](NodeInput), [NodeOutput](NodeOutput), [StateNode](StateNode)  
  
**References:** [ItemContainer](ItemContainer), [NodifyEditor](NodifyEditor), [PendingConnectionEventHandler](PendingConnectionEventHandler), [ConnectorEventHandler](ConnectorEventHandler), [PendingConnection](PendingConnection), [Connection](Connection), [ConnectorEventArgs](ConnectorEventArgs), [PendingConnectionEventArgs](PendingConnectionEventArgs), [KnotNode](KnotNode), [Node](Node), [NodeInput](NodeInput), [NodeOutput](NodeOutput), [StateNode](StateNode)  
  
Represents a connector control which starts a [PendingConnection](PendingConnection) when being dragged and completes it when released.
            Has a [Connector.ElementConnector](Connector#elementconnector) that the [Connector.Anchor](Connector#anchor) is calculated from for the [PendingConnection](PendingConnection). Center of this control is used if missing.  
  
```csharp  
public class Connector : Control  
```  
## Constructors  
  
### Connector()  
  
```csharp  
public Connector();  
```  
## Fields  
  
### AnchorProperty  
  
```csharp  
public static DependencyProperty AnchorProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DisconnectCommandProperty  
  
```csharp  
public static DependencyProperty DisconnectCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DisconnectEvent  
  
```csharp  
public static RoutedEvent DisconnectEvent;  
```  
**Field Value**  
  
[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)  
  
### ElementConnector  
  
```csharp  
protected const string ElementConnector = "PART_Connector";  
```  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### EnableOptimizations  
  
Gets or sets if [Connector](Connector)s should enable optimizations based on [Connector.OptimizeSafeZone](Connector#optimizesafezone) and [Connector.OptimizeMinimumSelectedItems](Connector#optimizeminimumselecteditems).  
  
```csharp  
public static bool EnableOptimizations;  
```  
**Field Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsConnectedProperty  
  
```csharp  
public static DependencyProperty IsConnectedProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### IsPendingConnectionProperty  
  
```csharp  
public static DependencyProperty IsPendingConnectionProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### OptimizeMinimumSelectedItems  
  
Gets or sets the minimum selected items needed to trigger optimizations when outside of the [Connector.OptimizeSafeZone](Connector#optimizesafezone).  
  
```csharp  
public static uint OptimizeMinimumSelectedItems;  
```  
**Field Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### OptimizeSafeZone  
  
Gets or sets the safe zone outside the [NodifyEditor.Viewport](NodifyEditor#viewport) that will not trigger optimizations.  
  
```csharp  
public static double OptimizeSafeZone;  
```  
**Field Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### PendingConnectionCompletedEvent  
  
```csharp  
public static RoutedEvent PendingConnectionCompletedEvent;  
```  
**Field Value**  
  
[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)  
  
### PendingConnectionDragEvent  
  
```csharp  
public static RoutedEvent PendingConnectionDragEvent;  
```  
**Field Value**  
  
[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)  
  
### PendingConnectionStartedEvent  
  
```csharp  
public static RoutedEvent PendingConnectionStartedEvent;  
```  
**Field Value**  
  
[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)  
  
## Properties  
  
### AllowPendingConnectionCancellation  
  
Gets or sets whether cancelling a pending connection is allowed.  
  
```csharp  
public static bool AllowPendingConnectionCancellation { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Anchor  
  
Gets the location where [Connection](Connection)s can be attached to. 
            Bind with System.Windows.Data.BindingMode.OneWayToSource  
  
```csharp  
public Point Anchor { get; set; }  
```  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Container  
  
Gets the [ItemContainer](ItemContainer) that contains this [Connector](Connector).  
  
```csharp  
protected ItemContainer Container { get; set; }  
```  
**Property Value**  
  
[ItemContainer](ItemContainer)  
  
### DisconnectCommand  
  
Invoked if the [Connector.Disconnect](Connector#disconnect) event is not handled.
            Parameter is the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of this control.  
  
```csharp  
public ICommand DisconnectCommand { get; set; }  
```  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Editor  
  
Gets the [NodifyEditor](NodifyEditor) that owns this [Connector.Container](Connector#container).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
**Property Value**  
  
[NodifyEditor](NodifyEditor)  
  
### IsConnected  
  
If this is set to false, the [Connector.Disconnect](Connector#disconnect) event will not be invoked and the connector will stop updating its [Connector.Anchor](Connector#anchor) when moved, resized etc.  
  
```csharp  
public bool IsConnected { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsPendingConnection  
  
Gets a value that indicates whether a [PendingConnection](PendingConnection) is in progress for this [Connector](Connector).  
  
```csharp  
public bool IsPendingConnection { get; protected set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Thumb  
  
Gets the [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) used to calculate the [Connector.Anchor](Connector#anchor).  
  
```csharp  
protected FrameworkElement Thumb { get; set; }  
```  
**Property Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
## Methods  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
### OnConnectorDrag(Vector)  
  
```csharp  
protected virtual void OnConnectorDrag(Vector offset);  
```  
**Parameters**  
  
`offset` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
### OnConnectorDragCompleted(Boolean)  
  
```csharp  
protected virtual void OnConnectorDragCompleted(bool cancel = false);  
```  
**Parameters**  
  
`cancel` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OnConnectorDragStarted()  
  
```csharp  
protected virtual void OnConnectorDragStarted();  
```  
### OnDisconnect()  
  
```csharp  
protected virtual void OnDisconnect();  
```  
### OnLostMouseCapture(MouseEventArgs)  
  
```csharp  
protected override void OnLostMouseCapture(MouseEventArgs e);  
```  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
### OnMouseLeftButtonDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e);  
```  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseLeftButtonUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e);  
```  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseMove(MouseEventArgs)  
  
```csharp  
protected override void OnMouseMove(MouseEventArgs e);  
```  
**Parameters**  
  
`e` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs)  
  
### OnMouseRightButtonUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseRightButtonUp(MouseButtonEventArgs e);  
```  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
### UpdateAnchor(Point)  
  
Updates the [Connector.Anchor](Connector#anchor) relative to a location. (usually [Connector.Container](Connector#container)'s location)  
  
```csharp  
protected void UpdateAnchor(Point location);  
```  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The relative location  
  
### UpdateAnchor()  
  
Updates the [Connector.Anchor](Connector#anchor) based on [Connector.Container](Connector#container)'s location.  
  
```csharp  
public void UpdateAnchor();  
```  
### UpdateAnchorOptimized(Point)  
  
Updates the [Connector.Anchor](Connector#anchor) and applies optimizations if needed based on [Connector.EnableOptimizations](Connector#enableoptimizations) flag  
  
```csharp  
protected void UpdateAnchorOptimized(Point location);  
```  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Events  
  
### Disconnect  
  
Occurs when the [ModifierKeys.Alt](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys.alt) key is held and the [Connector](Connector) is clicked.  
  
```csharp  
public event ConnectorEventHandler Disconnect;  
```  
**Event Type**  
  
[ConnectorEventHandler](ConnectorEventHandler)  
  
### PendingConnectionCompleted  
  
Occurs when the [Connector](Connector) loses mouse capture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionCompleted;  
```  
**Event Type**  
  
[PendingConnectionEventHandler](PendingConnectionEventHandler)  
  
### PendingConnectionDrag  
  
Occurs when the mouse is changing position and the [Connector](Connector) has mouse capture.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionDrag;  
```  
**Event Type**  
  
[PendingConnectionEventHandler](PendingConnectionEventHandler)  
  
### PendingConnectionStarted  
  
Occurs when the [Connector](Connector) is clicked.  
  
```csharp  
public event PendingConnectionEventHandler PendingConnectionStarted;  
```  
**Event Type**  
  
[PendingConnectionEventHandler](PendingConnectionEventHandler)  
  

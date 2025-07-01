# PendingConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [PendingConnection](Nodify_PendingConnection)  
  
**References:** [ConnectionDirection](Nodify_ConnectionDirection), [Connector](Nodify_Connector), [HotKeysDisplayMode](Nodify_HotKeysDisplayMode), [ItemContainer](Nodify_ItemContainer), [NodifyEditor](Nodify_NodifyEditor), [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs), [PendingConnectionEventHandler](Nodify_Events_PendingConnectionEventHandler), [StateNode](Nodify_StateNode)  
  
Represents a pending connection usually started by a [Connector](Nodify_Connector) which invokes the [PendingConnection.CompletedCommand](Nodify_PendingConnection#completedcommand) when completed.  
  
```csharp  
public class PendingConnection : ContentControl  
```  
  
## Constructors  
  
### PendingConnection()  
  
```csharp  
public PendingConnection();  
```  
  
## Properties  
  
### AllowOnlyConnectors  
  
If true will preview and connect only to [Connector](Nodify_Connector)s, otherwise will also enable [ItemContainer](Nodify_ItemContainer)s.  
  
```csharp  
public bool AllowOnlyConnectors { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CompletedCommand  
  
Gets or sets the command to invoke when the pending connection is completed.
            Will not be invoked if [NodifyEditor.ConnectionCompletedCommand](Nodify_NodifyEditor#connectioncompletedcommand) is used.
            [PendingConnection.Target](Nodify_PendingConnection#target) will be set to the desired [Connector](Nodify_Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) and will also be the command's parameter.  
  
```csharp  
public ICommand CompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Direction  
  
Gets or sets the direction of this connection.  
  
```csharp  
public ConnectionDirection Direction { get; set; }  
```  
  
**Property Value**  
  
[ConnectionDirection](Nodify_ConnectionDirection)  
  
### Editor  
  
Gets the [NodifyEditor](Nodify_NodifyEditor) that owns this [PendingConnection](Nodify_PendingConnection).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](Nodify_NodifyEditor)  
  
### EnableHitTesting  
  
Gets or sets whether hit testing is enabled for pending connections.  
  
```csharp  
public static bool EnableHitTesting { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnablePreview  
  
[PendingConnection.PreviewTarget](Nodify_PendingConnection#previewtarget) will be updated with a potential [Connector](Nodify_Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) if this is true.  
  
```csharp  
public bool EnablePreview { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableSnapping  
  
Enables snapping the [PendingConnection.TargetAnchor](Nodify_PendingConnection#targetanchor) to a possible [PendingConnection.Target](Nodify_PendingConnection#target) connector.  
  
```csharp  
public bool EnableSnapping { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### HotKeysDisplayMode  
  
Gets or sets whether hotkeys are enabled for pending connections.  
  
```csharp  
public static HotKeysDisplayMode HotKeysDisplayMode { get; set; }  
```  
  
**Property Value**  
  
[HotKeysDisplayMode](Nodify_HotKeysDisplayMode)  
  
### IsVisible  
  
Gets or sets the visibility of the connection.  
  
```csharp  
public bool IsVisible { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### MaxHotKeys  
  
Gets or sets the maximum number of hotkeys that can be displayed for a pending connection.  
  
```csharp  
public static uint MaxHotKeys { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### PreviewTarget  
  
Gets or sets the [Connector](Nodify_Connector) or the [ItemContainer](Nodify_ItemContainer) (if [PendingConnection.AllowOnlyConnectors](Nodify_PendingConnection#allowonlyconnectors) is false) that we're previewing. See [PendingConnection.EnablePreview](Nodify_PendingConnection#enablepreview).  
  
```csharp  
public object PreviewTarget { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### Source  
  
Gets or sets the [Connector](Nodify_Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) that started this pending connection.  
  
```csharp  
public object Source { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### SourceAnchor  
  
Gets or sets the starting point for the connection.  
  
```csharp  
public Point SourceAnchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### StartedCommand  
  
Gets or sets the command to invoke when the pending connection is started.
            Will not be invoked if [NodifyEditor.ConnectionStartedCommand](Nodify_NodifyEditor#connectionstartedcommand) is used.
            [PendingConnection.Source](Nodify_PendingConnection#source) will be set to the [Connector](Nodify_Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) that started this connection and will also be the command's parameter.  
  
```csharp  
public ICommand StartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Stroke  
  
Gets or sets the stroke color of the connection.  
  
```csharp  
public Brush Stroke { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### StrokeDashArray  
  
Gets or sets the pattern of dashes and gaps that is used to outline the connection.  
  
```csharp  
public DoubleCollection StrokeDashArray { get; set; }  
```  
  
**Property Value**  
  
[DoubleCollection](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.DoubleCollection)  
  
### StrokeThickness  
  
Gets or set the connection thickness.  
  
```csharp  
public double StrokeThickness { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### Target  
  
Gets or sets the [Connector](Nodify_Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) (or potentially an [ItemContainer](Nodify_ItemContainer)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) if [PendingConnection.AllowOnlyConnectors](Nodify_PendingConnection#allowonlyconnectors) is false) that the [PendingConnection.Source](Nodify_PendingConnection#source) can connect to.
            Only set when the connection is completed (see [PendingConnection.CompletedCommand](Nodify_PendingConnection#completedcommand)).  
  
```csharp  
public object Target { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### TargetAnchor  
  
Gets or sets the end point for the connection.  
  
```csharp  
public Point TargetAnchor { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Methods  
  
### FindConnectionTarget(Point)  
  
Searches for a potential [Connector](Nodify_Connector) or [ItemContainer](Nodify_ItemContainer) at the specified position within the editor.  
  
```csharp  
public FrameworkElement FindConnectionTarget(Point position);  
```  
  
**Parameters**  
  
`position` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
**Returns**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### GetIsOverElement(UIElement)  
  
```csharp  
public static bool GetIsOverElement(UIElement elem);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
### OnPendingConnectionCompleted(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionCompleted(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs)  
  
### OnPendingConnectionDrag(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionDrag(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs)  
  
### OnPendingConnectionStarted(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionStarted(object sender, PendingConnectionEventArgs e);  
```  
  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](Nodify_Events_PendingConnectionEventArgs)  
  
### SetIsOverElement(UIElement, Boolean)  
  
```csharp  
public static void SetIsOverElement(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  

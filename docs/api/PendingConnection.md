# PendingConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [PendingConnection](PendingConnection)  
  
**References:** [PendingConnectionEventArgs](PendingConnectionEventArgs), [ConnectionDirection](ConnectionDirection), [NodifyEditor](NodifyEditor), [Connector](Connector), [ItemContainer](ItemContainer), [PendingConnectionEventHandler](PendingConnectionEventHandler), [StateNode](StateNode)  
  
Represents a pending connection usually started by a [Connector](Connector) which invokes the [PendingConnection.CompletedCommand](PendingConnection#completedcommand) when completed.  
  
```csharp  
public class PendingConnection : ContentControl  
```  
## Constructors  
  
### PendingConnection()  
  
```csharp  
public PendingConnection();  
```  
## Fields  
  
### AllowOnlyConnectorsProperty  
  
```csharp  
public static DependencyProperty AllowOnlyConnectorsProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### CompletedCommandProperty  
  
```csharp  
public static DependencyProperty CompletedCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DirectionProperty  
  
```csharp  
public static DependencyProperty DirectionProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### EnablePreviewProperty  
  
```csharp  
public static DependencyProperty EnablePreviewProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### EnableSnappingProperty  
  
```csharp  
public static DependencyProperty EnableSnappingProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### IsOverElementProperty  
  
Will be set for [Connector](Connector)s and [ItemContainer](ItemContainer)s when the pending connection is over the element if [PendingConnection.EnablePreview](PendingConnection#enablepreview) or [PendingConnection.EnableSnapping](PendingConnection#enablesnapping) is true.  
  
```csharp  
public static DependencyProperty IsOverElementProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### PreviewTargetProperty  
  
```csharp  
public static DependencyProperty PreviewTargetProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### SourceAnchorProperty  
  
```csharp  
public static DependencyProperty SourceAnchorProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### SourceProperty  
  
```csharp  
public static DependencyProperty SourceProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### StrokeDashArrayProperty  
  
```csharp  
public static DependencyProperty StrokeDashArrayProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### StrokeProperty  
  
```csharp  
public static DependencyProperty StrokeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### StrokeThicknessProperty  
  
```csharp  
public static DependencyProperty StrokeThicknessProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### TargetAnchorProperty  
  
```csharp  
public static DependencyProperty TargetAnchorProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### TargetProperty  
  
```csharp  
public static DependencyProperty TargetProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
## Properties  
  
### AllowOnlyConnectors  
  
If true will preview and connect only to [Connector](Connector)s, otherwise will also enable [ItemContainer](ItemContainer)s.  
  
```csharp  
public bool AllowOnlyConnectors { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CompletedCommand  
  
Gets or sets the command to invoke when the pending connection is completed.
            Will not be invoked if [NodifyEditor.ConnectionCompletedCommand](NodifyEditor#connectioncompletedcommand) is used.
            [PendingConnection.Target](PendingConnection#target) will be set to the desired [Connector](Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) and will also be the command's parameter.  
  
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
  
[ConnectionDirection](ConnectionDirection)  
  
### Editor  
  
Gets the [NodifyEditor](NodifyEditor) that owns this [PendingConnection](PendingConnection).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
**Property Value**  
  
[NodifyEditor](NodifyEditor)  
  
### EnablePreview  
  
[PendingConnection.PreviewTarget](PendingConnection#previewtarget) will be updated with a potential [Connector](Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) if this is true.  
  
```csharp  
public bool EnablePreview { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableSnapping  
  
Enables snapping the [PendingConnection.TargetAnchor](PendingConnection#targetanchor) to a possible [PendingConnection.Target](PendingConnection#target) connector.  
  
```csharp  
public bool EnableSnapping { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsVisible  
  
Gets or sets the visibility of this connection.  
  
```csharp  
public bool IsVisible { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### PreviewTarget  
  
Gets or sets the [Connector](Connector) or the [ItemContainer](ItemContainer) (if [PendingConnection.AllowOnlyConnectors](PendingConnection#allowonlyconnectors) is false) that we're previewing.  
  
```csharp  
public object PreviewTarget { get; set; }  
```  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### Source  
  
Gets or sets the [Connector](Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) that started this pending connection.  
  
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
  
Gets or sets the [Connector](Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) (or potentially an [ItemContainer](ItemContainer)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) if [PendingConnection.AllowOnlyConnectors](PendingConnection#allowonlyconnectors) is false) that the [PendingConnection.Source](PendingConnection#source) can connect to.
            Only set when the connection is completed (see [PendingConnection.CompletedCommand](PendingConnection#completedcommand)).  
  
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
  
`e` [PendingConnectionEventArgs](PendingConnectionEventArgs)  
  
### OnPendingConnectionDrag(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionDrag(object sender, PendingConnectionEventArgs e);  
```  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](PendingConnectionEventArgs)  
  
### OnPendingConnectionStarted(Object, PendingConnectionEventArgs)  
  
```csharp  
protected virtual void OnPendingConnectionStarted(object sender, PendingConnectionEventArgs e);  
```  
**Parameters**  
  
`sender` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`e` [PendingConnectionEventArgs](PendingConnectionEventArgs)  
  
### SetIsOverElement(UIElement, Boolean)  
  
```csharp  
public static void SetIsOverElement(UIElement elem, bool value);  
```  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  

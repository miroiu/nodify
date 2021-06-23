# NodifyEditor Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [Selector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.Selector) → [MultiSelector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.MultiSelector) → [NodifyEditor](NodifyEditor)  
  
**References:** [PendingConnection](PendingConnection), [Connector](Connector), [GroupingNode](GroupingNode), [ItemContainer](ItemContainer), [SelectionHelper](SelectionHelper), [EditorCommands](EditorCommands), [Connection](Connection), [BaseConnection](BaseConnection)  
  
Groups [ItemContainer](ItemContainer)s and [Connection](Connection)s in an area that you can drag, scale and select.  
  
```csharp  
public class NodifyEditor : MultiSelector  
```  
## Constructors  
  
### NodifyEditor()  
  
Initializes a new instance of the [NodifyEditor](NodifyEditor) class.  
  
```csharp  
public NodifyEditor();  
```  
## Fields  
  
### AppliedTransformProperty  
  
```csharp  
public static DependencyProperty AppliedTransformProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### AutoPanEdgeDistanceProperty  
  
```csharp  
public static DependencyProperty AutoPanEdgeDistanceProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### AutoPanSpeedProperty  
  
```csharp  
public static DependencyProperty AutoPanSpeedProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### BringIntoViewAnimationDurationProperty  
  
```csharp  
public static DependencyProperty BringIntoViewAnimationDurationProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ConnectionCompletedCommandProperty  
  
```csharp  
public static DependencyProperty ConnectionCompletedCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ConnectionsProperty  
  
```csharp  
public static DependencyProperty ConnectionsProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ConnectionTemplateProperty  
  
```csharp  
public static DependencyProperty ConnectionTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### CurrentMousePosition  
  
Gets where the mouse cursor is right now relative to the [NodifyEditor](NodifyEditor).
            Check [NodifyEditor.MouseLocation](NodifyEditor#mouselocation) for a transformed position.  
  
```csharp  
protected Point CurrentMousePosition;  
```  
**Field Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### DisableAutoPanningProperty  
  
```csharp  
public static DependencyProperty DisableAutoPanningProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DisablePanningProperty  
  
```csharp  
public static DependencyProperty DisablePanningProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DisableZoomingProperty  
  
```csharp  
public static DependencyProperty DisableZoomingProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DisconnectConnectorCommandProperty  
  
```csharp  
public static DependencyProperty DisconnectConnectorCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### DisplayConnectionsOnTopProperty  
  
```csharp  
public static DependencyProperty DisplayConnectionsOnTopProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ElementItemsHost  
  
```csharp  
protected const string ElementItemsHost = "PART_ItemsHost";  
```  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### EnableRealtimeSelectionProperty  
  
```csharp  
public static DependencyProperty EnableRealtimeSelectionProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### GridCellSizeProperty  
  
```csharp  
public static DependencyProperty GridCellSizeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### InitialMousePosition  
  
Gets where the mouse cursor was relative to the [NodifyEditor](NodifyEditor) when a mouse button event occurred.
            Check [NodifyEditor.MouseLocation](NodifyEditor#mouselocation) for a transformed position.  
  
```csharp  
protected Point InitialMousePosition;  
```  
**Field Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### IsPanningProperty  
  
```csharp  
public static DependencyProperty IsPanningProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### IsPanningPropertyKey  
  
```csharp  
public static DependencyPropertyKey IsPanningPropertyKey;  
```  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### IsSelectingProperty  
  
```csharp  
public static DependencyProperty IsSelectingProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### IsSelectingPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsSelectingPropertyKey;  
```  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### ItemsDragCompletedCommandProperty  
  
```csharp  
public static DependencyProperty ItemsDragCompletedCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ItemsDragStartedCommandProperty  
  
```csharp  
public static DependencyProperty ItemsDragStartedCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### MaxScaleProperty  
  
```csharp  
public static DependencyProperty MaxScaleProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### MinScaleProperty  
  
```csharp  
public static DependencyProperty MinScaleProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### MouseLocationProperty  
  
```csharp  
public static DependencyProperty MouseLocationProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### MouseLocationPropertyKey  
  
```csharp  
protected static DependencyPropertyKey MouseLocationPropertyKey;  
```  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### OffsetProperty  
  
```csharp  
public static DependencyProperty OffsetProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### PendingConnectionProperty  
  
```csharp  
public static DependencyProperty PendingConnectionProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### PendingConnectionTemplateProperty  
  
```csharp  
public static DependencyProperty PendingConnectionTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### PreviousMousePosition  
  
Gets where the mouse cursor was the previous time it moved relative to the [NodifyEditor](NodifyEditor).
            Check [NodifyEditor.MouseLocation](NodifyEditor#mouselocation) for a transformed position.  
  
```csharp  
protected Point PreviousMousePosition;  
```  
**Field Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### ScaleProperty  
  
```csharp  
public static DependencyProperty ScaleProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ScaleTransform  
  
Gets the transform used to scale the [NodifyEditor.Viewport](NodifyEditor#viewport).  
  
```csharp  
protected readonly ScaleTransform ScaleTransform;  
```  
**Field Value**  
  
[ScaleTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.ScaleTransform)  
  
### SelectedAreaProperty  
  
```csharp  
public static DependencyProperty SelectedAreaProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### SelectedAreaPropertyKey  
  
```csharp  
protected static DependencyPropertyKey SelectedAreaPropertyKey;  
```  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### SelectedItemsProperty  
  
```csharp  
public static DependencyProperty SelectedItemsProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### SelectionRectangleStyleProperty  
  
```csharp  
public static DependencyProperty SelectionRectangleStyleProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### TranslateTransform  
  
Gets the transform used to offset the [NodifyEditor.Viewport](NodifyEditor#viewport).  
  
```csharp  
protected readonly TranslateTransform TranslateTransform;  
```  
**Field Value**  
  
[TranslateTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.TranslateTransform)  
  
### ViewportProperty  
  
```csharp  
public static DependencyProperty ViewportProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ViewportUpdatedEvent  
  
```csharp  
public static RoutedEvent ViewportUpdatedEvent;  
```  
**Field Value**  
  
[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)  
  
## Properties  
  
### AppliedTransform  
  
Gets the transform that is applied to all child controls.  
  
```csharp  
public Transform AppliedTransform { get; set; }  
```  
**Property Value**  
  
[Transform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Transform)  
  
### AutoPanEdgeDistance  
  
Gets or sets the maximum distance in pixels from the edge of the editor that will trigger auto-panning.  
  
```csharp  
public double AutoPanEdgeDistance { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### AutoPanningTickRate  
  
Gets or sets how often the new [NodifyEditor.Offset](NodifyEditor#offset) is calculated in milliseconds when [NodifyEditor.DisableAutoPanning](NodifyEditor#disableautopanning) is false.  
  
```csharp  
public static double AutoPanningTickRate { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### AutoPanSpeed  
  
Gets or sets the speed used when auto-panning scaled by [NodifyEditor.AutoPanningTickRate](NodifyEditor#autopanningtickrate)  
  
```csharp  
public double AutoPanSpeed { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### BringIntoViewAnimationDuration  
  
Gets or sets the animation duration in seconds when bringing a location into view.  
  
```csharp  
public double BringIntoViewAnimationDuration { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### ConnectionCompletedCommand  
  
Invoked when the [PendingConnection](PendingConnection) is completed. 
            If you override the [PendingConnection](PendingConnection) style or [NodifyEditor.PendingConnectionTemplate](NodifyEditor#pendingconnectiontemplate), please use the [PendingConnection.CompletedCommand](PendingConnection#completedcommand) instead. 
            Parameters is Tuple<object, object> where Tuple<object, object>.Item1 is the [PendingConnection.Source](PendingConnection#source) and Tuple<object, object>.Item2 is [PendingConnection.Target](PendingConnection#target).  
  
```csharp  
public ICommand ConnectionCompletedCommand { get; set; }  
```  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Connections  
  
Gets or sets the data source that [BaseConnection](BaseConnection)s will be generated for.  
  
```csharp  
public IEnumerable Connections { get; set; }  
```  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### ConnectionTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use when generating a new [BaseConnection](BaseConnection).  
  
```csharp  
public DataTemplate ConnectionTemplate { get; set; }  
```  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### DisableAutoPanning  
  
Gets or sets whether to disable the auto panning when selecting or dragging near the edge of the editor configured by [NodifyEditor.AutoPanEdgeDistance](NodifyEditor#autopanedgedistance).  
  
```csharp  
public bool DisableAutoPanning { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### DisablePanning  
  
Gets or sets whether panning should be disabled.  
  
```csharp  
public bool DisablePanning { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### DisableZooming  
  
Gets or sets whether zooming should be disabled.  
  
```csharp  
public bool DisableZooming { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### DisconnectConnectorCommand  
  
Invoked when the [Connector.Disconnect](Connector#disconnect) event is raised. 
            Can also be handled at the [Connector](Connector) level using the [Connector.DisconnectCommand](Connector#disconnectcommand) command. 
            Parameter is the [Connector](Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext).  
  
```csharp  
public ICommand DisconnectConnectorCommand { get; set; }  
```  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### DisplayConnectionsOnTop  
  
Gets or sets whether to display connections on top of [ItemContainer](ItemContainer)s or not.  
  
```csharp  
public bool DisplayConnectionsOnTop { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableRealtimeSelection  
  
Enables selecting and deselecting items while the [NodifyEditor.SelectedArea](NodifyEditor#selectedarea) changes.
            Disable for maximum performance when hundreds of items are generated.  
  
```csharp  
public bool EnableRealtimeSelection { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableRenderingContainersOptimizations  
  
Gets or sets if [NodifyEditor](NodifyEditor)s should enable optimizations based on [NodifyEditor.OptimizeRenderingMinimumContainers](NodifyEditor#optimizerenderingminimumcontainers) and [NodifyEditor.OptimizeRenderingZoomOutPercent](NodifyEditor#optimizerenderingzoomoutpercent).  
  
```csharp  
public static bool EnableRenderingContainersOptimizations { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableSnappingCorrection  
  
Correct [ItemContainer](ItemContainer)'s position after moving if starting position is not snapped to grid.  
  
```csharp  
public static bool EnableSnappingCorrection { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### GridCellSize  
  
Gets or sets the value of an invisible grid used to adjust locations (snapping) of [ItemContainer](ItemContainer)s.  
  
```csharp  
public uint GridCellSize { get; set; }  
```  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### HandleRightClickAfterPanningThreshold  
  
Gets or sets the maximum number of pixels allowed to move the mouse before cancelling the mouse event.
            Useful for System.Windows.Controls.ContextMenus to appear if mouse only moved a bit or not at all.  
  
```csharp  
public static double HandleRightClickAfterPanningThreshold { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### IsBulkUpdatingItems  
  
Tells if the [NodifyEditor](NodifyEditor) is doing operations on multiple items at once.  
  
```csharp  
public bool IsBulkUpdatingItems { get; protected set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsPanning  
  
Gets a value that indicates whether a panning operation is in progress.  
  
```csharp  
public bool IsPanning { get; protected set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsSelecting  
  
Gets a value that indicates whether a selection operation is in progress.  
  
```csharp  
public bool IsSelecting { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ItemsDragCompletedCommand  
  
Invoked when a drag operation is completed for the [NodifyEditor.SelectedItems](NodifyEditor#selecteditems).  
  
```csharp  
public ICommand ItemsDragCompletedCommand { get; set; }  
```  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ItemsDragStartedCommand  
  
Invoked when a drag operation starts for the [NodifyEditor.SelectedItems](NodifyEditor#selecteditems).  
  
```csharp  
public ICommand ItemsDragStartedCommand { get; set; }  
```  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### MaxScale  
  
Gets or sets the maximum zoom factor of the [NodifyEditor.Viewport](NodifyEditor#viewport)  
  
```csharp  
public double MaxScale { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### MinScale  
  
Gets or sets the minimum zoom factor of the [NodifyEditor.Viewport](NodifyEditor#viewport)  
  
```csharp  
public double MinScale { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### MouseLocation  
  
Gets the current transformed mouse location using the [NodifyEditor.AppliedTransform](NodifyEditor#appliedtransform).  
  
```csharp  
public Point MouseLocation { get; protected set; }  
```  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Offset  
  
Gets or sets the [NodifyEditor.Viewport](NodifyEditor#viewport)'s top and left coordinates.  
  
```csharp  
public Point Offset { get; set; }  
```  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### OptimizeRenderingMinimumContainers  
  
Gets or sets the minimum selected [ItemContainer](ItemContainer)s needed to trigger optimizations when reaching the [NodifyEditor.OptimizeRenderingZoomOutPercent](NodifyEditor#optimizerenderingzoomoutpercent).  
  
```csharp  
public static uint OptimizeRenderingMinimumContainers { get; set; }  
```  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### OptimizeRenderingZoomOutPercent  
  
Gets or sets the minimum zoom out percent needed to start optimizing the rendering for [ItemContainer](ItemContainer)s.  
  
```csharp  
public static double OptimizeRenderingZoomOutPercent { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### PendingConnection  
  
Gets of sets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement.datacontext) of the [PendingConnection](PendingConnection).  
  
```csharp  
public object PendingConnection { get; set; }  
```  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### PendingConnectionTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use for the [NodifyEditor.PendingConnection](NodifyEditor#pendingconnection).  
  
```csharp  
public DataTemplate PendingConnectionTemplate { get; set; }  
```  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### Scale  
  
Gets or sets the zoom factor of the [NodifyEditor.Viewport](NodifyEditor#viewport).  
  
```csharp  
public double Scale { get; set; }  
```  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### SelectedArea  
  
Gets the currently selected area while [NodifyEditor.IsSelecting](NodifyEditor#isselecting) is true.  
  
```csharp  
public Rect SelectedArea { get; set; }  
```  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### SelectedItems  
  
Gets or sets the items in the [NodifyEditor](NodifyEditor) that are selected.  
  
```csharp  
public IList SelectedItems { get; set; }  
```  
**Property Value**  
  
[IList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IList)  
  
### Selection  
  
Helps with selecting [ItemContainer](ItemContainer)s and updating the [NodifyEditor.SelectedArea](NodifyEditor#selectedarea) and [NodifyEditor.IsSelecting](NodifyEditor#isselecting) properties.  
  
```csharp  
protected SelectionHelper Selection { get; set; }  
```  
**Property Value**  
  
[SelectionHelper](SelectionHelper)  
  
### SelectionRectangleStyle  
  
Gets or sets the style to use for the selection rectangle.  
  
```csharp  
public Style SelectionRectangleStyle { get; set; }  
```  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### Viewport  
  
Gets the area of the [NodifyEditor](NodifyEditor) that is seen on the screen. ([NodifyEditor.Offset](NodifyEditor#offset) and [NodifyEditor.Scale](NodifyEditor#scale) applied)  
  
```csharp  
public Rect Viewport { get; set; }  
```  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
## Methods  
  
### BringIntoView(Point, Boolean)  
  
Moves the [NodifyEditor.Viewport](NodifyEditor#viewport) at the specified location.  
  
```csharp  
public void BringIntoView(Point point, bool animated = true);  
```  
**Parameters**  
  
`point` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location where to move the [NodifyEditor.Viewport](NodifyEditor#viewport).  
  
`animated` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to animate the movement.  
  
### GetContainerForItemOverride()  
  
```csharp  
protected override DependencyObject GetContainerForItemOverride();  
```  
**Returns**  
  
[DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
### InvertSelection(Rect, Boolean)  
  
Inverts the [ItemContainer](ItemContainer)s selection in the specified area.  
  
```csharp  
public void InvertSelection(Rect area, bool fit = false);  
```  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](ItemContainer)s.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](ItemContainer).  False to check if area intersects the [ItemContainer](ItemContainer).  
  
### IsItemItsOwnContainerOverride(Object)  
  
```csharp  
protected override bool IsItemItsOwnContainerOverride(object item);  
```  
**Parameters**  
  
`item` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
### OnDisableAutoPanningChanged(Boolean)  
  
Called when the [NodifyEditor.DisableAutoPanning](NodifyEditor#disableautopanning) changes.  
  
```csharp  
protected virtual void OnDisableAutoPanningChanged(bool shouldDisable);  
```  
**Parameters**  
  
`shouldDisable` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Whether to enable or disable auto panning.  
  
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
  
### OnMouseRightButtonDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseRightButtonDown(MouseButtonEventArgs e);  
```  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseRightButtonUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseRightButtonUp(MouseButtonEventArgs e);  
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
  
### OnSelectionChanged(SelectionChangedEventArgs)  
  
```csharp  
protected override void OnSelectionChanged(SelectionChangedEventArgs e);  
```  
**Parameters**  
  
`e` [SelectionChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.SelectionChangedEventArgs)  
  
### OnViewportUpdated()  
  
Raises the [NodifyEditor.ViewportUpdatedEvent](NodifyEditor#viewportupdatedevent).
            Called when the [NodifyEditor.Offset](NodifyEditor#offset) or [NodifyEditor.Scale](NodifyEditor#scale) is changed.  
  
```csharp  
protected void OnViewportUpdated();  
```  
### SelectArea(Rect, Boolean, Boolean)  
  
Selects the [ItemContainer](ItemContainer)s in the specified area.  
  
```csharp  
public void SelectArea(Rect area, bool append = false, bool fit = false);  
```  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](ItemContainer)s.  
  
`append` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): If true, it will add to the existing  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](ItemContainer).  False to check if area intersects the [ItemContainer](ItemContainer).  
  
### TransformPosition(Point)  
  
Transforms the point to a location relative to the [NodifyEditor.ItemsHost](NodifyEditor#itemshost) panel.  
  
```csharp  
protected Point TransformPosition(Point point);  
```  
**Parameters**  
  
`point` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location to transform.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The relative location.  
  
### TransformToViewportCenter(Point)  
  
Transforms the point to a location relative to the [NodifyEditor.Viewport](NodifyEditor#viewport)'s center.  
  
```csharp  
protected Point TransformToViewportCenter(Point point);  
```  
**Parameters**  
  
`point` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The point to transform.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The center location.  
  
### UnselectArea(Rect, Boolean)  
  
Unselect the [ItemContainer](ItemContainer)s in the specified area.  
  
```csharp  
public void UnselectArea(Rect area, bool fit = false);  
```  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](ItemContainer)s.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](ItemContainer).  False to check if area intersects the [ItemContainer](ItemContainer).  
  
### ZoomAtPosition(Double, Point)  
  
Zoom at the specified location.  
  
```csharp  
public void ZoomAtPosition(double zoom, Point pos);  
```  
**Parameters**  
  
`zoom` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The zoom factor.  
  
`pos` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location to focus when zooming.  
  
### ZoomIn()  
  
Zoom in at the viewports center  
  
```csharp  
public void ZoomIn();  
```  
### ZoomOut()  
  
Zoom out at the viewports center  
  
```csharp  
public void ZoomOut();  
```  
## Events  
  
### ViewportUpdated  
  
Occurs whenever the [NodifyEditor.Viewport](NodifyEditor#viewport) changes.  
  
```csharp  
public event RoutedEventHandler ViewportUpdated;  
```  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  

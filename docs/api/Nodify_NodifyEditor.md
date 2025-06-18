# NodifyEditor Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [Selector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.Selector) → [MultiSelector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.MultiSelector) → [NodifyEditor](Nodify_NodifyEditor)  
  
**Implements:** [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer), [IKeyboardNavigationLayerGroup](Nodify_Interactivity_IKeyboardNavigationLayerGroup), [IScrollInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.IScrollInfo)  
  
**References:** [Alignment](Nodify_Alignment), [BaseConnection](Nodify_BaseConnection), [Connection](Nodify_Connection), [ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector), [Connector](Nodify_Connector), [EditorState.Cutting](Nodify_Interactivity_EditorState_Cutting), [CuttingLine](Nodify_CuttingLine), [DecoratorContainer](Nodify_DecoratorContainer), [DecoratorsControl](Nodify_DecoratorsControl), [EditorCommands](Nodify_EditorCommands), [EditorGestures](Nodify_Interactivity_EditorGestures), [GroupingNode](Nodify_GroupingNode), [IKeyboardFocusTarget\<ItemContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_), [InputProcessor](Nodify_Interactivity_InputProcessor), [ItemContainer](Nodify_ItemContainer), [ItemsMovedEventArgs](Nodify_Events_ItemsMovedEventArgs), [ItemsMovedEventHandler](Nodify_Events_ItemsMovedEventHandler), [EditorState.KeyboardNavigation](Nodify_Interactivity_EditorState_KeyboardNavigation), [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId), [Minimap](Nodify_Minimap), [EditorState.Panning](Nodify_Interactivity_EditorState_Panning), [EditorState.PanningWithMouseWheel](Nodify_Interactivity_EditorState_PanningWithMouseWheel), [PendingConnection](Nodify_PendingConnection), [EditorState.PushingItems](Nodify_Interactivity_EditorState_PushingItems), [EditorState.Selecting](Nodify_Interactivity_EditorState_Selecting), [SelectionType](Nodify_SelectionType), [EditorState.Zooming](Nodify_Interactivity_EditorState_Zooming)  
  
Groups [ItemContainer](Nodify_ItemContainer)s and [Connection](Nodify_Connection)s in an area that you can drag, zoom and select.  
  
```csharp  
public class NodifyEditor : MultiSelector, IKeyboardNavigationLayer, IKeyboardNavigationLayerGroup, IScrollInfo  
```  
  
## Constructors  
  
### NodifyEditor()  
  
Initializes a new instance of the [NodifyEditor](Nodify_NodifyEditor) class.  
  
```csharp  
public NodifyEditor();  
```  
  
## Fields  
  
### CuttingConnectionTypes  
  
The list of supported connection types for cutting. Type must be derived from [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement).  
  
```csharp  
public static HashSet<Type> CuttingConnectionTypes;  
```  
  
**Field Value**  
  
[HashSet\<Type\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.HashSet-1)  
  
### CuttingLineEndPropertyKey  
  
```csharp  
protected static DependencyPropertyKey CuttingLineEndPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### CuttingLineStartPropertyKey  
  
```csharp  
protected static DependencyPropertyKey CuttingLineStartPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### ElementConnectionsHost  
  
```csharp  
protected const string ElementConnectionsHost = "PART_ConnectionsHost";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### ElementItemsHost  
  
```csharp  
protected const string ElementItemsHost = "PART_ItemsHost";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### IsCuttingPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsCuttingPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### IsDraggingPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsDraggingPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### IsPanningPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsPanningPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### IsPushingItemsPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsPushingItemsPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### IsSelectingPropertyKey  
  
```csharp  
protected static DependencyPropertyKey IsSelectingPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### PushedAreaOrientationPropertyKey  
  
```csharp  
protected static DependencyPropertyKey PushedAreaOrientationPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### PushedAreaPropertyKey  
  
```csharp  
protected static DependencyPropertyKey PushedAreaPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### ScaleTransform  
  
Gets the transform used to zoom on the viewport.  
  
```csharp  
protected readonly ScaleTransform ScaleTransform;  
```  
  
**Field Value**  
  
[ScaleTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.ScaleTransform)  
  
### SelectedAreaPropertyKey  
  
```csharp  
protected static DependencyPropertyKey SelectedAreaPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
### TranslateTransform  
  
Gets the transform used to offset the viewport.  
  
```csharp  
protected readonly TranslateTransform TranslateTransform;  
```  
  
**Field Value**  
  
[TranslateTransform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.TranslateTransform)  
  
### ViewportTransformPropertyKey  
  
```csharp  
protected static DependencyPropertyKey ViewportTransformPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
## Properties  
  
### ActiveNavigationLayer  
  
```csharp  
public virtual IKeyboardNavigationLayer ActiveNavigationLayer { get; set; }  
```  
  
**Property Value**  
  
[IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
### AllowCuttingCancellation  
  
Gets or sets whether cancelling a cutting operation is allowed (see Nodify.Interactivity.EditorGestures.NodifyEditorGestures.CancelAction).  
  
```csharp  
public static bool AllowCuttingCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AllowDraggingCancellation  
  
Gets or sets whether cancelling a dragging operation is allowed.  
  
```csharp  
public static bool AllowDraggingCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AllowPanningCancellation  
  
Gets or sets whether panning cancellation is allowed (see Nodify.Interactivity.EditorGestures.NodifyEditorGestures.CancelAction).  
  
```csharp  
public static bool AllowPanningCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AllowPushItemsCancellation  
  
Gets or sets whether push items cancellation is allowed (see Nodify.Interactivity.EditorGestures.NodifyEditorGestures.CancelAction).  
  
```csharp  
public static bool AllowPushItemsCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AllowSelectionCancellation  
  
Gets or sets whether cancelling a selection operation is allowed (see Nodify.Interactivity.EditorGestures.SelectionGestures.Cancel).  
  
```csharp  
public static bool AllowSelectionCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AutoFocusFirstElement  
  
Automatically focus the first container when the navigation layer changes or the editor gets focused.  
  
```csharp  
public static bool AutoFocusFirstElement { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AutoPanEdgeDistance  
  
Gets or sets the maximum distance in pixels from the edge of the editor that will trigger auto-panning.  
  
```csharp  
public double AutoPanEdgeDistance { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### AutoPanningTickRate  
  
Gets or sets how often the new [NodifyEditor.ViewportLocation](Nodify_NodifyEditor#viewportlocation) is calculated in milliseconds when [NodifyEditor.DisableAutoPanning](Nodify_NodifyEditor#disableautopanning) is false.  
  
```csharp  
public static double AutoPanningTickRate { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### AutoPanOnNodeFocus  
  
Automatically pan the viewport when a node is focused via keyboard navigation.  
  
```csharp  
public static bool AutoPanOnNodeFocus { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AutoPanSpeed  
  
Gets or sets the speed used when auto-panning scaled by [NodifyEditor.AutoPanningTickRate](Nodify_NodifyEditor#autopanningtickrate)  
  
```csharp  
public double AutoPanSpeed { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### AutoRegisterConnectionsLayer  
  
Automatically registers the connectors layer for keyboard navigation.  
  
```csharp  
public static bool AutoRegisterConnectionsLayer { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AutoRegisterDecoratorsLayer  
  
Automatically registers the decorators layer for keyboard navigation.  
  
```csharp  
public static bool AutoRegisterDecoratorsLayer { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### BringIntoViewEdgeOffset  
  
Gets or sets the default viewport edge offset applied when bringing an item into view as a result of keyboard focus.  
  
```csharp  
public static double BringIntoViewEdgeOffset { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### BringIntoViewMaxDuration  
  
Gets or sets the maximum animation duration in seconds for bringing a location into view.  
  
```csharp  
public double BringIntoViewMaxDuration { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### BringIntoViewSpeed  
  
Gets or sets the animation speed in pixels per second for bringing a location into view.  
  
```csharp  
public double BringIntoViewSpeed { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### CanSelectMultipleConnections  
  
Gets or sets whether multiple connections can be selected.  
  
```csharp  
public bool CanSelectMultipleConnections { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### CanSelectMultipleItems  
  
Gets or sets whether multiple [ItemContainer](Nodify_ItemContainer)s can be selected.  
  
```csharp  
public bool CanSelectMultipleItems { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ConnectionCompletedCommand  
  
Invoked when the [PendingConnection](Nodify_PendingConnection) is completed. 
            Use [PendingConnection.CompletedCommand](Nodify_PendingConnection#completedcommand) if you want to control the visibility of the connection from the viewmodel. 
            Parameter is System.Tuple`2 where System.Tuple`2.Item1 is the [PendingConnection.Source](Nodify_PendingConnection#source) and System.Tuple`2.Item2 is [PendingConnection.Target](Nodify_PendingConnection#target).  
  
```csharp  
public ICommand ConnectionCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### Connections  
  
Gets or sets the data source that [BaseConnection](Nodify_BaseConnection)s will be generated for.  
  
```csharp  
public IEnumerable Connections { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### ConnectionStartedCommand  
  
Invoked when the [PendingConnection](Nodify_PendingConnection) is completed. 
            Use [PendingConnection.StartedCommand](Nodify_PendingConnection#startedcommand) if you want to control the visibility of the connection from the viewmodel. 
            Parameter is [PendingConnection.Source](Nodify_PendingConnection#source).  
  
```csharp  
public ICommand ConnectionStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ConnectionTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use when generating a new [BaseConnection](Nodify_BaseConnection).  
  
```csharp  
public DataTemplate ConnectionTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### CuttingCompletedCommand  
  
Invoked when a cutting operation is completed.  
  
```csharp  
public ICommand CuttingCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### CuttingLineEnd  
  
Gets the end point of the [CuttingLine](Nodify_CuttingLine) while [NodifyEditor.IsCutting](Nodify_NodifyEditor#iscutting) is true.  
  
```csharp  
public Point CuttingLineEnd { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### CuttingLineStart  
  
Gets the start point of the [CuttingLine](Nodify_CuttingLine) while [NodifyEditor.IsCutting](Nodify_NodifyEditor#iscutting) is true.  
  
```csharp  
public Point CuttingLineStart { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### CuttingLineStyle  
  
Gets or sets the style to use for the cutting line.  
  
```csharp  
public Style CuttingLineStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### CuttingStartedCommand  
  
Invoked when a cutting operation is started.  
  
```csharp  
public ICommand CuttingStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### DecoratorContainerStyle  
  
Gets or sets the style to use for the [DecoratorContainer](Nodify_DecoratorContainer).  
  
```csharp  
public Style DecoratorContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### Decorators  
  
Gets or sets the items that will be rendered in the decorators layer via [DecoratorContainer](Nodify_DecoratorContainer)s.  
  
```csharp  
public IEnumerable Decorators { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### DecoratorsExtent  
  
The area covered by the [DecoratorContainer](Nodify_DecoratorContainer)s.  
  
```csharp  
public Rect DecoratorsExtent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### DecoratorTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use when generating a new [DecoratorContainer](Nodify_DecoratorContainer).  
  
```csharp  
public DataTemplate DecoratorTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### DisableAutoPanning  
  
Gets or sets whether to disable the auto panning when selecting or dragging near the edge of the editor configured by [NodifyEditor.AutoPanEdgeDistance](Nodify_NodifyEditor#autopanedgedistance).  
  
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
  
Invoked when the [Connector.Disconnect](Nodify_Connector#disconnect) event is raised. 
            Can also be handled at the [Connector](Nodify_Connector) level using the [Connector.DisconnectCommand](Nodify_Connector#disconnectcommand) command. 
            Parameter is the [Connector](Nodify_Connector)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext).  
  
```csharp  
public ICommand DisconnectConnectorCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### DisplayConnectionsOnTop  
  
Gets or sets whether to display connections on top of [ItemContainer](Nodify_ItemContainer)s or not.  
  
```csharp  
public bool DisplayConnectionsOnTop { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableCuttingLinePreview  
  
Gets or sets whether the cutting line should apply the preview style to the interesected elements.  
  
```csharp  
public static bool EnableCuttingLinePreview { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableDraggingContainersOptimizations  
  
Gets or sets if the current position of containers that are being dragged should not be committed until the end of the dragging operation.  
  
```csharp  
public static bool EnableDraggingContainersOptimizations { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableRealtimeSelection  
  
Enables selecting and deselecting items while the [NodifyEditor.SelectedArea](Nodify_NodifyEditor#selectedarea) changes.
            Disable for maximum performance when hundreds of items are generated.  
  
```csharp  
public bool EnableRealtimeSelection { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableRenderingContainersOptimizations  
  
Gets or sets if [NodifyEditor](Nodify_NodifyEditor)s should enable optimizations based on [NodifyEditor.OptimizeRenderingMinimumContainers](Nodify_NodifyEditor#optimizerenderingminimumcontainers) and [NodifyEditor.OptimizeRenderingZoomOutPercent](Nodify_NodifyEditor#optimizerenderingzoomoutpercent).  
  
```csharp  
public static bool EnableRenderingContainersOptimizations { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### EnableSnappingCorrection  
  
Correct [ItemContainer](Nodify_ItemContainer)'s position after moving if starting position is not snapped to grid.  
  
```csharp  
public static bool EnableSnappingCorrection { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### FitToScreenExtentMargin  
  
Gets or sets the margin to add in all directions to the [NodifyEditor.ItemsExtent](Nodify_NodifyEditor#itemsextent) or area parameter when using Nodify.NodifyEditor.FitToScreen(System.Nullable{System.Windows.Rect}).  
  
```csharp  
public static double FitToScreenExtentMargin { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### GridCellSize  
  
Gets or sets the value of an invisible grid used to adjust locations (snapping) of [ItemContainer](Nodify_ItemContainer)s.  
  
```csharp  
public uint GridCellSize { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### HasContextMenu  
  
Gets a value indicating whether the editor has a context menu.  
  
```csharp  
public bool HasContextMenu { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### HasCustomContextMenu  
  
Gets or sets a value indicating whether the editor uses a custom context menu.  
  
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
  
### IsBulkUpdatingItems  
  
Tells if the [NodifyEditor](Nodify_NodifyEditor) is doing operations on multiple items at once.  
  
```csharp  
public bool IsBulkUpdatingItems { get; protected set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsCutting  
  
Gets a value that indicates whether a cutting operation is in progress.  
  
```csharp  
public bool IsCutting { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsDragging  
  
Gets a value that indicates whether a dragging operation is in progress.  
  
```csharp  
public bool IsDragging { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsPanning  
  
Gets a value that indicates whether a panning operation is in progress.  
  
```csharp  
public bool IsPanning { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsPushingItems  
  
Gets a value that indicates whether a pushing operation is in progress.  
  
```csharp  
public bool IsPushingItems { get; set; }  
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
  
Invoked when a drag operation is completed for the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers), or when [NodifyEditor.IsPushingItems](Nodify_NodifyEditor#ispushingitems) is set to false.  
  
```csharp  
public ICommand ItemsDragCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ItemsDragStartedCommand  
  
Invoked when a drag operation starts for the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers), or when [NodifyEditor.IsPushingItems](Nodify_NodifyEditor#ispushingitems) is set to true.  
  
```csharp  
public ICommand ItemsDragStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ItemsExtent  
  
The area covered by the [ItemContainer](Nodify_ItemContainer)s.  
  
```csharp  
public Rect ItemsExtent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### ItemsSelectCompletedCommand  
  
Invoked when a selection operation is completed (see Nodify.NodifyEditor.EndSelecting).  
  
```csharp  
public ICommand ItemsSelectCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ItemsSelectStartedCommand  
  
Invoked when a selection operation is started (see Nodify.NodifyEditor.BeginSelecting(Nodify.SelectionType)).  
  
```csharp  
public ICommand ItemsSelectStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### KeyboardNavigationLayer  
  
```csharp  
public IKeyboardNavigationLayer KeyboardNavigationLayer { get; set; }  
```  
  
**Property Value**  
  
[IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
### MaxViewportZoom  
  
Gets or sets the maximum zoom factor of the viewport  
  
```csharp  
public double MaxViewportZoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### MinimumNavigationStepSize  
  
Defines the minimum distance to move or navigate when using directional input (such as arrow keys), scaled by the [NodifyEditor.ViewportZoom](Nodify_NodifyEditor#viewportzoom).
            If the [NodifyEditor.GridCellSize](Nodify_NodifyEditor#gridcellsize) is smaller than this value, the movement step is increased to the nearest greater multiple of the [NodifyEditor.GridCellSize](Nodify_NodifyEditor#gridcellsize).  
  
```csharp  
public static double MinimumNavigationStepSize { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### MinViewportZoom  
  
Gets or sets the minimum zoom factor of the viewport  
  
```csharp  
public double MinViewportZoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### MouseActionSuppressionThreshold  
  
Gets or sets the maximum distance, in pixels, that the mouse can move before suppressing certain mouse actions. 
            This is useful for suppressing actions like showing a System.Windows.Controls.ContextMenu if the mouse has moved significantly.  
  
```csharp  
public static double MouseActionSuppressionThreshold { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### MouseLocation  
  
Gets the current mouse location in graph space coordinates (relative to the [NodifyEditor.ItemsHost](Nodify_NodifyEditor#itemshost)).  
  
```csharp  
public Point MouseLocation { get; protected set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### OptimizeRenderingMinimumContainers  
  
Gets or sets the minimum number of [ItemContainer](Nodify_ItemContainer)s needed to trigger optimizations when reaching the [NodifyEditor.OptimizeRenderingZoomOutPercent](Nodify_NodifyEditor#optimizerenderingzoomoutpercent).  
  
```csharp  
public static uint OptimizeRenderingMinimumContainers { get; set; }  
```  
  
**Property Value**  
  
[UInt32](https://docs.microsoft.com/en-us/dotnet/api/System.UInt32)  
  
### OptimizeRenderingZoomOutPercent  
  
Gets or sets the minimum zoom out percent needed to start optimizing the rendering for [ItemContainer](Nodify_ItemContainer)s.
            Value is between 0 and 1.  
  
```csharp  
public static double OptimizeRenderingZoomOutPercent { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### PanViewportOnKeyboardDrag  
  
Indicates whether the viewport should automatically pan to follow elements moved via keyboard dragging.  
  
```csharp  
public static bool PanViewportOnKeyboardDrag { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### PendingConnection  
  
Gets of sets the [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext) of the [PendingConnection](Nodify_PendingConnection).  
  
```csharp  
public object PendingConnection { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### PendingConnectionTemplate  
  
Gets or sets the [DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate) to use for the [NodifyEditor.PendingConnection](Nodify_NodifyEditor#pendingconnection).  
  
```csharp  
public DataTemplate PendingConnectionTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### PushedArea  
  
Gets the currently pushed area while [NodifyEditor.IsPushingItems](Nodify_NodifyEditor#ispushingitems) is true.  
  
```csharp  
public Rect PushedArea { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### PushedAreaOrientation  
  
Gets the orientation of the [NodifyEditor.PushedArea](Nodify_NodifyEditor#pushedarea).  
  
```csharp  
public Orientation PushedAreaOrientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  
### PushedAreaStyle  
  
Gets or sets the style to use for the pushed area.  
  
```csharp  
public Style PushedAreaStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### RemoveConnectionCommand  
  
Invoked when the [BaseConnection.Disconnect](Nodify_BaseConnection#disconnect) event is raised. 
            Can also be handled at the [BaseConnection](Nodify_BaseConnection) level using the [BaseConnection.DisconnectCommand](Nodify_BaseConnection#disconnectcommand) command. 
            Parameter is the [BaseConnection](Nodify_BaseConnection)'s [FrameworkElement.DataContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement#datacontext).  
  
```csharp  
public ICommand RemoveConnectionCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ScrollIncrement  
  
The number of units the mouse wheel is rotated to scroll one line.  
  
```csharp  
public static double ScrollIncrement { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### SelectedArea  
  
Gets the currently selected area while [NodifyEditor.IsSelecting](Nodify_NodifyEditor#isselecting) is true.  
  
```csharp  
public Rect SelectedArea { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### SelectedConnection  
  
Gets or sets the selected connection.  
  
```csharp  
public object SelectedConnection { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### SelectedConnections  
  
Gets or sets the selected connections in the [NodifyEditor](Nodify_NodifyEditor).  
  
```csharp  
public IList SelectedConnections { get; set; }  
```  
  
**Property Value**  
  
[IList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IList)  
  
### SelectedContainersCount  
  
Gets the number of selected containers, without allocating (see [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers)).  
  
```csharp  
public int SelectedContainersCount { get; set; }  
```  
  
**Property Value**  
  
[Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32)  
  
### SelectedItems  
  
Gets or sets the selected items in the [NodifyEditor](Nodify_NodifyEditor).  
  
```csharp  
public IList SelectedItems { get; set; }  
```  
  
**Property Value**  
  
[IList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IList)  
  
### SelectionRectangleStyle  
  
Gets or sets the style to use for the selection rectangle.  
  
```csharp  
public Style SelectionRectangleStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### ViewportLocation  
  
Gets or sets the viewport's top-left coordinates in graph space coordinates.  
  
```csharp  
public Point ViewportLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### ViewportSize  
  
Gets the size of the viewport in graph space (scaled by the [NodifyEditor.ViewportZoom](Nodify_NodifyEditor#viewportzoom)).  
  
```csharp  
public Size ViewportSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### ViewportTransform  
  
Gets the transform that is applied to all child controls.  
  
```csharp  
public Transform ViewportTransform { get; set; }  
```  
  
**Property Value**  
  
[Transform](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Transform)  
  
### ViewportZoom  
  
Gets or sets the zoom factor of the viewport.  
  
```csharp  
public double ViewportZoom { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
## Methods  
  
### ActivateNavigationLayer(KeyboardNavigationLayerId)  
  
```csharp  
public virtual bool ActivateNavigationLayer(KeyboardNavigationLayerId layerId);  
```  
  
**Parameters**  
  
`layerId` [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ActivateNextNavigationLayer()  
  
```csharp  
public virtual bool ActivateNextNavigationLayer();  
```  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ActivatePreviousNavigationLayer()  
  
```csharp  
public virtual bool ActivatePreviousNavigationLayer();  
```  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### AlignContainers(IEnumerable\<ItemContainer\>, Alignment, ItemContainer)  
  
Aligns a collection of containers based on the specified alignment.  
  
```csharp  
public void AlignContainers(IEnumerable<ItemContainer> containers, Alignment alignment, ItemContainer relativeTo = null);  
```  
  
**Parameters**  
  
`containers` [IEnumerable\<ItemContainer\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1): The collection of item containers to align.  
  
`alignment` [Alignment](Nodify_Alignment): The alignment type to apply to the containers.  
  
`relativeTo` [ItemContainer](Nodify_ItemContainer): An optional container to use as a reference for alignment. If null, the alignment is based on the containers themselves.  
  
### AlignSelection(Alignment, ItemContainer)  
  
Aligns the selected containers based on the specified alignment.  
  
```csharp  
public void AlignSelection(Alignment alignment, ItemContainer relativeTo = null);  
```  
  
**Parameters**  
  
`alignment` [Alignment](Nodify_Alignment): The alignment type to apply to the selected containers.  
  
`relativeTo` [ItemContainer](Nodify_ItemContainer): An optional container to use as a reference for alignment. If null, the alignment is based on the containers themselves.  
  
### BeginCutting()  
  
Starts the cutting operation at the specified location. Call Nodify.NodifyEditor.EndCutting to complete the operation or Nodify.NodifyEditor.CancelCutting to abort it.  
  
```csharp  
public void BeginCutting();  
```  
  
### BeginCutting(Point)  
  
```csharp  
public void BeginCutting(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### BeginDragging()  
  
Initiates the dragging operation for the specified [ItemContainer](Nodify_ItemContainer)s. Call Nodify.NodifyEditor.EndDragging to complete the operation or Nodify.NodifyEditor.CancelDragging to abort it.  
  
```csharp  
public void BeginDragging();  
```  
  
### BeginDragging(IEnumerable\<ItemContainer\>)  
  
```csharp  
public void BeginDragging(IEnumerable<ItemContainer> containers);  
```  
  
**Parameters**  
  
`containers` [IEnumerable\<ItemContainer\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)  
  
### BeginPanning(Point)  
  
Starts the panning operation from the specified location. Call Nodify.NodifyEditor.EndPanning to end the panning operation.  
  
```csharp  
public void BeginPanning(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The initial location where panning starts, in graph space coordinates.  
  
### BeginPanning()  
  
Starts the panning operation from the current [NodifyEditor.ViewportLocation](Nodify_NodifyEditor#viewportlocation).  
  
```csharp  
public void BeginPanning();  
```  
  
### BeginPushingItems(Point, Orientation)  
  
Starts the pushing items operation at the specified location with the specified orientation.  
  
```csharp  
public void BeginPushingItems(Point location, Orientation orientation);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The starting location for pushing items, in graph space coordinates.  
  
`orientation` [Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation): The orientation of the [NodifyEditor.PushedArea](Nodify_NodifyEditor#pushedarea).  
  
### BeginSelecting(SelectionType)  
  
Initiates a selection operation from the specified location.  
  
```csharp  
public void BeginSelecting(SelectionType type = 0);  
```  
  
**Parameters**  
  
`type` [SelectionType](Nodify_SelectionType): The type of selection to perform. Defaults to [SelectionType.Replace](Nodify_SelectionType#replace).  
  
### BeginSelecting(Point, SelectionType)  
  
```csharp  
public void BeginSelecting(Point location, SelectionType type = 0);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`type` [SelectionType](Nodify_SelectionType)  
  
### BringIntoView(Point, Boolean, Action)  
  
Moves the viewport center at the specified location.  
  
```csharp  
public void BringIntoView(Point point, bool animated = true, Action onFinish = null);  
```  
  
**Parameters**  
  
`point` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location in graph space coordinates.  
  
`animated` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to animate the movement.  
  
`onFinish` [Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action): The callback invoked when movement is finished.  
  
### BringIntoView(Rect)  
  
Ensures the specified item container is fully visible within the viewport, optionally with padding around the edges.  
  
```csharp  
public void BringIntoView(Rect area);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The location in graph space coordinates.  
  
### BringIntoView(Rect, Double)  
  
```csharp  
public void BringIntoView(Rect area, double offsetFromEdge = 32d);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
`offsetFromEdge` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### CancelCutting()  
  
Cancels the current cutting operation without applying any changes if [NodifyEditor.AllowCuttingCancellation](Nodify_NodifyEditor#allowcuttingcancellation) is true.
            Otherwise, it ends the cutting operation by calling Nodify.NodifyEditor.EndCutting.  
  
```csharp  
public void CancelCutting();  
```  
  
### CancelDragging()  
  
Cancels the ongoing dragging operation, reverting any changes made to the positions of the dragged items if [NodifyEditor.AllowDraggingCancellation](Nodify_NodifyEditor#allowdraggingcancellation) is true.
            Otherwise, it ends the dragging operation by calling Nodify.NodifyEditor.EndDragging.  
  
```csharp  
public void CancelDragging();  
```  
  
### CancelPanning()  
  
Cancels the current panning operation and reverts the viewport to its initial location if [NodifyEditor.AllowPanningCancellation](Nodify_NodifyEditor#allowpanningcancellation) is true.
            Otherwise, it ends the panning operation by calling Nodify.NodifyEditor.EndPanning.  
  
```csharp  
public void CancelPanning();  
```  
  
### CancelPushingItems()  
  
Cancels the current pushing operation and reverts the [NodifyEditor.PushedArea](Nodify_NodifyEditor#pushedarea) to its initial state if [NodifyEditor.AllowPushItemsCancellation](Nodify_NodifyEditor#allowpushitemscancellation) is true.
            Otherwise, it ends the pushing operation by calling Nodify.NodifyEditor.EndPushingItems.  
  
```csharp  
public void CancelPushingItems();  
```  
  
### CancelSelecting()  
  
Cancels the current selection operation and reverts any changes made during the selection process if [NodifyEditor.AllowSelectionCancellation](Nodify_NodifyEditor#allowselectioncancellation) is true.
            Otherwise, it ends the selection operation by calling Nodify.NodifyEditor.EndSelecting.  
  
```csharp  
public void CancelSelecting();  
```  
  
### EndCutting()  
  
Completes the cutting operation and applies the changes.  
  
```csharp  
public void EndCutting();  
```  
  
### EndDragging()  
  
Completes the dragging operation, finalizing the position of the dragged items. Raises the [NodifyEditor.ItemsMoved](Nodify_NodifyEditor#itemsmoved) event.  
  
```csharp  
public void EndDragging();  
```  
  
### EndPanning()  
  
Ends the current panning operation, retaining the current [NodifyEditor.ViewportLocation](Nodify_NodifyEditor#viewportlocation).  
  
```csharp  
public void EndPanning();  
```  
  
### EndPushingItems()  
  
Ends the current pushing operation and finalizes the pushed area state.  
  
```csharp  
public void EndPushingItems();  
```  
  
### EndSelecting()  
  
Completes the selection operation and applies any pending changes.  
  
```csharp  
public void EndSelecting();  
```  
  
### FindNextFocusTarget(ItemContainer, TraversalRequest)  
  
```csharp  
protected virtual ItemContainer FindNextFocusTarget(ItemContainer currentContainer, TraversalRequest request);  
```  
  
**Parameters**  
  
`currentContainer` [ItemContainer](Nodify_ItemContainer)  
  
`request` [TraversalRequest](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.TraversalRequest)  
  
**Returns**  
  
[ItemContainer](Nodify_ItemContainer)  
  
### FitToScreen(Rect?)  
  
Scales the viewport to fit the specified area or all the [ItemContainer](Nodify_ItemContainer)s if that's possible.  
  
```csharp  
public void FitToScreen(Rect? area = null);  
```  
  
**Parameters**  
  
`area` [Rect?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1)  
  
### GetContainerForItemOverride()  
  
```csharp  
protected override DependencyObject GetContainerForItemOverride();  
```  
  
**Returns**  
  
[DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
### GetEnumerator()  
  
```csharp  
public virtual IEnumerator<IKeyboardNavigationLayer> GetEnumerator();  
```  
  
**Returns**  
  
[IEnumerator\<IKeyboardNavigationLayer\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerator-1)  
  
### GetLocationInsideEditor(Point, UIElement)  
  
Translates the specified location to graph space coordinates (relative to the [NodifyEditor.ItemsHost](Nodify_NodifyEditor#itemshost)).  
  
```csharp  
public Point GetLocationInsideEditor(Point location, UIElement relativeTo);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location coordinates relative to relativeTo  
  
`relativeTo` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement): The element where the location was calculated from.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the graph.  
  
### GetLocationInsideEditor(DragEventArgs)  
  
Translates the event location to graph space coordinates (relative to the [NodifyEditor.ItemsHost](Nodify_NodifyEditor#itemshost)).  
  
```csharp  
public Point GetLocationInsideEditor(DragEventArgs args);  
```  
  
**Parameters**  
  
`args` [DragEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DragEventArgs): The drag event.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the graph  
  
### GetLocationInsideEditor(MouseEventArgs)  
  
Translates the event location to graph space coordinates (relative to the [NodifyEditor.ItemsHost](Nodify_NodifyEditor#itemshost)).  
  
```csharp  
public Point GetLocationInsideEditor(MouseEventArgs args);  
```  
  
**Parameters**  
  
`args` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs): The mouse event.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the graph  
  
### InvertSelection(Rect, Boolean)  
  
Inverts the [ItemContainer](Nodify_ItemContainer)s selection in the specified area.  
  
```csharp  
public void InvertSelection(Rect area, bool fit = false);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](Nodify_ItemContainer)s.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](Nodify_ItemContainer). False to check if area intersects the [ItemContainer](Nodify_ItemContainer).  
  
### IsItemItsOwnContainerOverride(Object)  
  
```csharp  
protected override bool IsItemItsOwnContainerOverride(object item);  
```  
  
**Parameters**  
  
`item` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### LockSelection()  
  
Locks the position of the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers).  
  
```csharp  
public void LockSelection();  
```  
  
### MoveFocus(FocusNavigationDirection)  
  
```csharp  
public bool MoveFocus(FocusNavigationDirection direction);  
```  
  
**Parameters**  
  
`direction` [FocusNavigationDirection](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.FocusNavigationDirection)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### MoveFocus(TraversalRequest)  
  
```csharp  
public bool MoveFocus(TraversalRequest request);  
```  
  
**Parameters**  
  
`request` [TraversalRequest](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.TraversalRequest)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
### OnElementFocused(IKeyboardFocusTarget\<ItemContainer\>)  
  
```csharp  
protected virtual void OnElementFocused(IKeyboardFocusTarget<ItemContainer> target);  
```  
  
**Parameters**  
  
`target` [IKeyboardFocusTarget\<ItemContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
### OnGotKeyboardFocus(KeyboardFocusChangedEventArgs)  
  
```csharp  
protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyboardFocusChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyboardFocusChangedEventArgs)  
  
### OnKeyboardNavigationLayerActivated(IKeyboardNavigationLayer)  
  
```csharp  
protected virtual void OnKeyboardNavigationLayerActivated(IKeyboardNavigationLayer activeLayer);  
```  
  
**Parameters**  
  
`activeLayer` [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
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
  
### OnLostKeyboardFocus(KeyboardFocusChangedEventArgs)  
  
```csharp  
protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e);  
```  
  
**Parameters**  
  
`e` [KeyboardFocusChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.KeyboardFocusChangedEventArgs)  
  
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
  
### OnRemoveConnection(Object)  
  
```csharp  
protected void OnRemoveConnection(object dataContext);  
```  
  
**Parameters**  
  
`dataContext` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
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
  
Updates the [NodifyEditor.ViewportSize](Nodify_NodifyEditor#viewportsize) and raises the [NodifyEditor.ViewportUpdatedEvent](Nodify_NodifyEditor#viewportupdatedevent).
            Called when the [UIElement.RenderSize](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement#rendersize) or [NodifyEditor.ViewportZoom](Nodify_NodifyEditor#viewportzoom) is changed.  
  
```csharp  
protected void OnViewportUpdated();  
```  
  
### RegisterNavigationLayer(IKeyboardNavigationLayer)  
  
```csharp  
public virtual bool RegisterNavigationLayer(IKeyboardNavigationLayer layer);  
```  
  
**Parameters**  
  
`layer` [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### RemoveNavigationLayer(KeyboardNavigationLayerId)  
  
```csharp  
public virtual bool RemoveNavigationLayer(KeyboardNavigationLayerId layerId);  
```  
  
**Parameters**  
  
`layerId` [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ResetViewport(Boolean, Action)  
  
Reset the viewport location to (0, 0) and the viewport zoom to 1.  
  
```csharp  
public void ResetViewport(bool animated = true, Action onFinish = null);  
```  
  
**Parameters**  
  
`animated` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Whether the viewport transition is animated.  
  
`onFinish` [Action](https://docs.microsoft.com/en-us/dotnet/api/System.Action): The callback invoked when the viewport transition is finished.  
  
### Select(ItemContainer)  
  
Clears the current selection and selects the specified [ItemContainer](Nodify_ItemContainer) within the same selection transaction.  
  
```csharp  
public void Select(ItemContainer container);  
```  
  
**Parameters**  
  
`container` [ItemContainer](Nodify_ItemContainer)  
  
### SelectAllConnections()  
  
Select all [NodifyEditor.Connections](Nodify_NodifyEditor#connections).  
  
```csharp  
public void SelectAllConnections();  
```  
  
### SelectArea(Rect, Boolean, Boolean)  
  
Selects the [ItemContainer](Nodify_ItemContainer)s in the specified area.  
  
```csharp  
public void SelectArea(Rect area, bool append = false, bool fit = false);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](Nodify_ItemContainer)s.  
  
`append` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): If true, it will add to the existing selection.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](Nodify_ItemContainer). False to check if area intersects the [ItemContainer](Nodify_ItemContainer).  
  
### SnapToGrid(Double)  
  
Snaps the given value down to the nearest multiple of the grid cell size.  
  
```csharp  
public double SnapToGrid(double value);  
```  
  
**Parameters**  
  
`value` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The value to be snapped to the grid.  
  
**Returns**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The largest multiple of the grid cell size less than or equal to the value.  
  
### UnlockSelection()  
  
Unlocks the position of the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers).  
  
```csharp  
public void UnlockSelection();  
```  
  
### UnselectAllConnections()  
  
Unselect all [NodifyEditor.Connections](Nodify_NodifyEditor#connections).  
  
```csharp  
public void UnselectAllConnections();  
```  
  
### UnselectArea(Rect, Boolean)  
  
Unselect the [ItemContainer](Nodify_ItemContainer)s in the specified area.  
  
```csharp  
public void UnselectArea(Rect area, bool fit = false);  
```  
  
**Parameters**  
  
`area` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect): The area to look for [ItemContainer](Nodify_ItemContainer)s.  
  
`fit` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): True to check if the area contains the [ItemContainer](Nodify_ItemContainer). False to check if area intersects the [ItemContainer](Nodify_ItemContainer).  
  
### UpdateCuttingLine(Vector)  
  
Updates the current cutting line position and the style for the intersecting elements if [NodifyEditor.EnableCuttingLinePreview](Nodify_NodifyEditor#enablecuttinglinepreview) is true.  
  
```csharp  
public void UpdateCuttingLine(Vector amount);  
```  
  
**Parameters**  
  
`amount` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The amount to adjust the cutting line's endpoint.  
  
### UpdateCuttingLine(Point)  
  
Updates the current cutting line position and the style for the intersecting elements if [NodifyEditor.EnableCuttingLinePreview](Nodify_NodifyEditor#enablecuttinglinepreview) is true.  
  
```csharp  
public void UpdateCuttingLine(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location of the cutting line's endpoint.  
  
### UpdateDragging(Vector)  
  
Updates the position of the items being dragged by a specified offset.  
  
```csharp  
public void UpdateDragging(Vector amount);  
```  
  
**Parameters**  
  
`amount` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The vector by which to adjust the position of the dragged items.  
  
### UpdatePanning(Vector)  
  
Pans the viewport by the specified amount.  
  
```csharp  
public void UpdatePanning(Vector amount);  
```  
  
**Parameters**  
  
`amount` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The amount to pan the viewport.  
  
### UpdatePushedArea(Vector)  
  
Updates the pushed area based on the specified amount taking the [NodifyEditor.PushedAreaOrientation](Nodify_NodifyEditor#pushedareaorientation) into account.  
  
```csharp  
public void UpdatePushedArea(Vector amount);  
```  
  
**Parameters**  
  
`amount` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The amount to adjust the pushed area by.  
  
### UpdateSelection(Vector)  
  
Expands or modifies the selection area by the specified amount.  
  
```csharp  
public void UpdateSelection(Vector amount);  
```  
  
**Parameters**  
  
`amount` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): Rrepresents the change to apply to the selection area.  
  
### UpdateSelection(Point)  
  
Expands or modifies the selection area to the specified location.  
  
```csharp  
public void UpdateSelection(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The point, in graph space coordinates, to extend or adjust the selection area to.  
  
### ZoomAtPosition(Double, Point)  
  
Zoom at the specified location in graph space coordinates.  
  
```csharp  
public void ZoomAtPosition(double zoom, Point location);  
```  
  
**Parameters**  
  
`zoom` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The zoom factor to apply. A value greater than 1 zooms in, while a value between 0 and 1 zooms out.  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The point in graph space coordinates where the zoom should be centered.  
  
### ZoomIn()  
  
Zoom in at the viewport's center.  
  
```csharp  
public void ZoomIn();  
```  
  
### ZoomOut()  
  
Zoom out at the viewport's center.  
  
```csharp  
public void ZoomOut();  
```  
  
## Events  
  
### ActiveNavigationLayerChanged  
  
```csharp  
public virtual event Action<KeyboardNavigationLayerId> ActiveNavigationLayerChanged;  
```  
  
**Event Type**  
  
[Action\<KeyboardNavigationLayerId\>](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1)  
  
### ItemsMoved  
  
Occurs when items are moved within the editor (see Nodify.NodifyEditor.BeginDragging, Nodify.NodifyEditor.BeginPushingItems(System.Windows.Point,System.Windows.Controls.Orientation)).  
  
```csharp  
public event ItemsMovedEventHandler ItemsMoved;  
```  
  
**Event Type**  
  
[ItemsMovedEventHandler](Nodify_Events_ItemsMovedEventHandler)  
  
### ViewportUpdated  
  
Occurs whenever the viewport updates.  
  
```csharp  
public event RoutedEventHandler ViewportUpdated;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  

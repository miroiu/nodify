# Minimap Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [Minimap](Nodify_Minimap)  
  
**References:** [InputProcessor](Nodify_Interactivity_InputProcessor), [MinimapState.KeyboardNavigation](Nodify_Interactivity_MinimapState_KeyboardNavigation), [MinimapItem](Nodify_MinimapItem), [NodifyEditor](Nodify_NodifyEditor), [MinimapState.Panning](Nodify_Interactivity_MinimapState_Panning), [ZoomEventArgs](Nodify_Events_ZoomEventArgs), [ZoomEventHandler](Nodify_Events_ZoomEventHandler), [MinimapState.Zooming](Nodify_Interactivity_MinimapState_Zooming)  
  
A minimap control that can position the viewport, and zoom in and out.  
  
```csharp  
public class Minimap : ItemsControl  
```  
  
## Constructors  
  
### Minimap()  
  
```csharp  
public Minimap();  
```  
  
## Properties  
  
### AllowPanningCancellation  
  
Gets or sets whether panning cancellation is allowed (see Nodify.Interactivity.EditorGestures.MinimapGestures.CancelAction).  
  
```csharp  
public static bool AllowPanningCancellation { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Extent  
  
The area covered by the items and the viewport rectangle in graph space.  
  
```csharp  
public Rect Extent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### InputProcessor  
  
```csharp  
protected InputProcessor InputProcessor { get; set; }  
```  
  
**Property Value**  
  
[InputProcessor](Nodify_Interactivity_InputProcessor)  
  
### IsPanning  
  
Whether the user is currently panning the minimap.  
  
```csharp  
protected bool IsPanning { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsReadOnly  
  
Whether the minimap can move and zoom the viewport.  
  
```csharp  
public bool IsReadOnly { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ItemsExtent  
  
The area covered by the [MinimapItem](Nodify_MinimapItem)s in graph space.  
  
```csharp  
public Rect ItemsExtent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### ItemsHost  
  
Gets the panel that holds all the [MinimapItem](Nodify_MinimapItem)s.  
  
```csharp  
protected Panel ItemsHost { get; set; }  
```  
  
**Property Value**  
  
[Panel](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Panel)  
  
### MaxViewportOffset  
  
The max position from the [NodifyEditor.ItemsExtent](Nodify_NodifyEditor#itemsextent) that the viewport can move to.  
  
```csharp  
public Size MaxViewportOffset { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### MouseLocation  
  
Gets the current mouse location in graph space coordinates (relative to the [Minimap.ItemsHost](Nodify_Minimap#itemshost)).  
  
```csharp  
public Point MouseLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### NavigationStepSize  
  
Defines the distance to pan when using directional input (such as arrow keys).  
  
```csharp  
public static double NavigationStepSize { get; set; }  
```  
  
**Property Value**  
  
[Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
### ResizeToViewport  
  
Whether the minimap should resize to also display the whole viewport.  
  
```csharp  
public bool ResizeToViewport { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### ViewportLocation  
  
```csharp  
public Point ViewportLocation { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### ViewportSize  
  
```csharp  
public Size ViewportSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### ViewportStyle  
  
Gets or sets the style to use for the viewport rectangle.  
  
```csharp  
public Style ViewportStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
## Methods  
  
### BeginPanning()  
  
Starts the panning operation from the specified location. Call Nodify.Minimap.EndPanning to end the panning operation.  
  
```csharp  
public void BeginPanning();  
```  
  
### BeginPanning(Point)  
  
```csharp  
public void BeginPanning(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### CancelPanning()  
  
Cancels the current panning operation and reverts the viewport to its initial location if [Minimap.AllowPanningCancellation](Nodify_Minimap#allowpanningcancellation) is true.
            Otherwise, it ends the panning operation by calling Nodify.Minimap.EndPanning.  
  
```csharp  
public void CancelPanning();  
```  
  
### EndPanning()  
  
Ends the current panning operation, retaining the current [Minimap.ViewportLocation](Nodify_Minimap#viewportlocation).  
  
```csharp  
public void EndPanning();  
```  
  
### GetContainerForItemOverride()  
  
```csharp  
protected override DependencyObject GetContainerForItemOverride();  
```  
  
**Returns**  
  
[DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
### GetLocationInsideMinimap(MouseEventArgs)  
  
Translates the event location to graph space coordinates (relative to the [Minimap.ItemsHost](Nodify_Minimap#itemshost)).  
  
```csharp  
public Point GetLocationInsideMinimap(MouseEventArgs args);  
```  
  
**Parameters**  
  
`args` [MouseEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseEventArgs): The mouse event.  
  
**Returns**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): A location inside the minimap  
  
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
  
### ResetViewport()  
  
```csharp  
public void ResetViewport();  
```  
  
### SetViewportLocation(Point)  
  
```csharp  
protected void SetViewportLocation(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### UpdatePanning(Point)  
  
Sets the viewport location to the specified location.  
  
```csharp  
public void UpdatePanning(Point location);  
```  
  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location to pan the viewport to.  
  
### UpdatePanning(Vector)  
  
Pans the viewport by the specified amount.  
  
```csharp  
public void UpdatePanning(Vector amount);  
```  
  
**Parameters**  
  
`amount` [Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector): The amount to pan the viewport.  
  
### ZoomAtPosition(Double, Point)  
  
Zoom at the specified location in graph space coordinates.  
  
```csharp  
public void ZoomAtPosition(double zoom, Point location);  
```  
  
**Parameters**  
  
`zoom` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double): The zoom factor.  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The location to focus when zooming.  
  
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
  
### Zoom  
  
Triggered when zooming in or out using the mouse wheel.  
  
```csharp  
public event ZoomEventHandler Zoom;  
```  
  
**Event Type**  
  
[ZoomEventHandler](Nodify_Events_ZoomEventHandler)  
  

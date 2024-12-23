# NodifyCanvas Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Panel](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Panel) → [NodifyCanvas](Nodify_NodifyCanvas)  
  
**References:** [INodifyCanvasItem](Nodify_INodifyCanvasItem)  
  
A canvas like panel that works with [INodifyCanvasItem](Nodify_INodifyCanvasItem)s.  
  
```csharp  
public class NodifyCanvas : Panel  
```  
  
## Constructors  
  
### NodifyCanvas()  
  
```csharp  
public NodifyCanvas();  
```  
  
## Properties  
  
### Extent  
  
The area covered by the children of this panel.  
  
```csharp  
public Rect Extent { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
## Methods  
  
### ArrangeOverride(Size)  
  
```csharp  
protected override Size ArrangeOverride(Size arrangeSize);  
```  
  
**Parameters**  
  
`arrangeSize` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
**Returns**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### MeasureOverride(Size)  
  
```csharp  
protected override Size MeasureOverride(Size constraint);  
```  
  
**Parameters**  
  
`constraint` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
**Returns**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  

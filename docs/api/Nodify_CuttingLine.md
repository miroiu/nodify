# CuttingLine Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [CuttingLine](Nodify_CuttingLine)  
  
**References:** [BaseConnection](Nodify_BaseConnection), [NodifyEditor](Nodify_NodifyEditor)  
  
```csharp  
public class CuttingLine : Shape  
```  
  
## Constructors  
  
### CuttingLine()  
  
```csharp  
public CuttingLine();  
```  
  
## Properties  
  
### DefiningGeometry  
  
```csharp  
protected override Geometry DefiningGeometry { get; set; }  
```  
  
**Property Value**  
  
[Geometry](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Geometry)  
  
### EndPoint  
  
Gets or sets the end point.  
  
```csharp  
public Point EndPoint { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### StartPoint  
  
Gets or sets the start point.  
  
```csharp  
public Point StartPoint { get; set; }  
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
  
### OnRender(DrawingContext)  
  
```csharp  
protected override void OnRender(DrawingContext drawingContext);  
```  
  
**Parameters**  
  
`drawingContext` [DrawingContext](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.DrawingContext)  
  
### SetIsOverElement(UIElement, Boolean)  
  
```csharp  
public static void SetIsOverElement(UIElement elem, bool value);  
```  
  
**Parameters**  
  
`elem` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  

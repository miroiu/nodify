# DecoratorContainer Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [DecoratorContainer](Nodify_DecoratorContainer)  
  
**Implements:** [INodifyCanvasItem](Nodify_INodifyCanvasItem)  
  
**References:** [NodifyEditor](Nodify_NodifyEditor)  
  
The container for all the items generated from the [NodifyEditor.Decorators](Nodify_NodifyEditor#decorators) collection.  
  
```csharp  
public class DecoratorContainer : ContentControl, INodifyCanvasItem  
```  
  
## Constructors  
  
### DecoratorContainer()  
  
```csharp  
public DecoratorContainer();  
```  
  
## Properties  
  
### ActualSize  
  
Gets the actual size of this [DecoratorContainer](Nodify_DecoratorContainer).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### Location  
  
Gets or sets the location of this [DecoratorContainer](Nodify_DecoratorContainer) inside the NodifyEditor.DecoratorsHost.  
  
```csharp  
public virtual Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Methods  
  
### OnLocationChanged()  
  
Raises the [DecoratorContainer.LocationChangedEvent](Nodify_DecoratorContainer#locationchangedevent).  
  
```csharp  
protected void OnLocationChanged();  
```  
  
### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  
## Events  
  
### LocationChanged  
  
Occurs when the [DecoratorContainer.Location](Nodify_DecoratorContainer#location) of this [DecoratorContainer](Nodify_DecoratorContainer) is changed.  
  
```csharp  
public event RoutedEventHandler LocationChanged;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  

# DecoratorContainer Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [DecoratorContainer](Nodify_DecoratorContainer)  
  
**Implements:** [INodifyCanvasItem](Nodify_INodifyCanvasItem), [IKeyboardFocusTarget\<DecoratorContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
**References:** [DecoratorsControl](Nodify_DecoratorsControl), [NodifyEditor](Nodify_NodifyEditor)  
  
The container for all the items generated from the [NodifyEditor.Decorators](Nodify_NodifyEditor#decorators) collection.  
  
```csharp  
public class DecoratorContainer : ContentControl, INodifyCanvasItem, IKeyboardFocusTarget<DecoratorContainer>  
```  
  
## Constructors  
  
### DecoratorContainer(DecoratorsControl)  
  
```csharp  
public DecoratorContainer(DecoratorsControl parent);  
```  
  
**Parameters**  
  
`parent` [DecoratorsControl](Nodify_DecoratorsControl)  
  
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
  
### Bounds  
  
```csharp  
public virtual Rect Bounds { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### Location  
  
Gets or sets the location of this [DecoratorContainer](Nodify_DecoratorContainer) inside the NodifyEditor.DecoratorsHost.  
  
```csharp  
public virtual Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### Owner  
  
```csharp  
public DecoratorsControl Owner { get; set; }  
```  
  
**Property Value**  
  
[DecoratorsControl](Nodify_DecoratorsControl)  
  
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
  
### OnVisualParentChanged(DependencyObject)  
  
```csharp  
protected override void OnVisualParentChanged(DependencyObject oldParent);  
```  
  
**Parameters**  
  
`oldParent` [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
## Events  
  
### LocationChanged  
  
Occurs when the [DecoratorContainer.Location](Nodify_DecoratorContainer#location) of this [DecoratorContainer](Nodify_DecoratorContainer) is changed.  
  
```csharp  
public event RoutedEventHandler LocationChanged;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  

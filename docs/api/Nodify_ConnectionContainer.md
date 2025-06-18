# ConnectionContainer Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [ContentPresenter](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentPresenter) → [ConnectionContainer](Nodify_ConnectionContainer)  
  
**Implements:** [IKeyboardFocusTarget\<ConnectionContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
**References:** [ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector), [SelectionType](Nodify_SelectionType)  
  
```csharp  
public class ConnectionContainer : ContentPresenter, IKeyboardFocusTarget<ConnectionContainer>  
```  
  
## Constructors  
  
### ConnectionContainer(ConnectionsMultiSelector)  
  
```csharp  
public ConnectionContainer(ConnectionsMultiSelector selector);  
```  
  
**Parameters**  
  
`selector` [ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector)  
  
## Properties  
  
### Bounds  
  
```csharp  
public virtual Rect Bounds { get; set; }  
```  
  
**Property Value**  
  
[Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  
### Connection  
  
```csharp  
public FrameworkElement Connection { get; set; }  
```  
  
**Property Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### IsSelectable  
  
Gets or sets whether this [ConnectionContainer](Nodify_ConnectionContainer) can be selected.  
  
```csharp  
public bool IsSelectable { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### IsSelected  
  
Gets or sets a value that indicates whether this [ConnectionContainer](Nodify_ConnectionContainer) is selected.
            Can only be set if [ConnectionContainer.IsSelectable](Nodify_ConnectionContainer#isselectable) is true.  
  
```csharp  
public bool IsSelected { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Selector  
  
```csharp  
public ConnectionsMultiSelector Selector { get; set; }  
```  
  
**Property Value**  
  
[ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector)  
  
## Methods  
  
### OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs)  
  
```csharp  
protected override void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e);  
```  
  
**Parameters**  
  
`e` [DependencyPropertyChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyChangedEventArgs)  
  
### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnVisualParentChanged(DependencyObject)  
  
```csharp  
protected override void OnVisualParentChanged(DependencyObject oldParent);  
```  
  
**Parameters**  
  
`oldParent` [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
### Select(SelectionType)  
  
Modifies the selection state of the current item based on the specified selection type.  
  
```csharp  
public void Select(SelectionType type);  
```  
  
**Parameters**  
  
`type` [SelectionType](Nodify_SelectionType): The type of selection to perform.  
  
## Events  
  
### Selected  
  
Occurs when this [ConnectionContainer](Nodify_ConnectionContainer) is selected.  
  
```csharp  
public event RoutedEventHandler Selected;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  
### Unselected  
  
Occurs when this [ConnectionContainer](Nodify_ConnectionContainer) is unselected.  
  
```csharp  
public event RoutedEventHandler Unselected;  
```  
  
**Event Type**  
  
[RoutedEventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventHandler)  
  

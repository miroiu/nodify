# ConnectionsMultiSelector Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [Selector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.Selector) → [MultiSelector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Primitives.MultiSelector) → [ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector)  
  
**Implements:** [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
**References:** [ConnectionContainer](Nodify_ConnectionContainer), [IKeyboardFocusTarget\<ConnectionContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_), [IKeyboardFocusTarget\<UIElement\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_), [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId), [NodifyEditor](Nodify_NodifyEditor)  
  
```csharp  
public class ConnectionsMultiSelector : MultiSelector, IKeyboardNavigationLayer  
```  
  
## Constructors  
  
### ConnectionsMultiSelector()  
  
```csharp  
public ConnectionsMultiSelector();  
```  
  
## Properties  
  
### CanSelectMultipleItems  
  
Gets or sets whether multiple connections can be selected.  
  
```csharp  
public bool CanSelectMultipleItems { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Editor  
  
Gets the [NodifyEditor](Nodify_NodifyEditor) that owns this [ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector).  
  
```csharp  
public NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](Nodify_NodifyEditor)  
  
### Id  
  
```csharp  
public virtual KeyboardNavigationLayerId Id { get; set; }  
```  
  
**Property Value**  
  
[KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId)  
  
### LastFocusedElement  
  
```csharp  
public virtual IKeyboardFocusTarget<UIElement> LastFocusedElement { get; set; }  
```  
  
**Property Value**  
  
[IKeyboardFocusTarget\<UIElement\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
### SelectedItems  
  
Gets or sets the selected connections in the [NodifyEditor](Nodify_NodifyEditor).  
  
```csharp  
public IList SelectedItems { get; set; }  
```  
  
**Property Value**  
  
[IList](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IList)  
  
## Methods  
  
### FindNextFocusTarget(ConnectionContainer, TraversalRequest)  
  
```csharp  
protected virtual ConnectionContainer FindNextFocusTarget(ConnectionContainer currentContainer, TraversalRequest request);  
```  
  
**Parameters**  
  
`currentContainer` [ConnectionContainer](Nodify_ConnectionContainer)  
  
`request` [TraversalRequest](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.TraversalRequest)  
  
**Returns**  
  
[ConnectionContainer](Nodify_ConnectionContainer)  
  
### GetContainerForItemOverride()  
  
```csharp  
protected override DependencyObject GetContainerForItemOverride();  
```  
  
**Returns**  
  
[DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
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
  
### OnElementFocused(IKeyboardFocusTarget\<ConnectionContainer\>)  
  
```csharp  
protected virtual void OnElementFocused(IKeyboardFocusTarget<ConnectionContainer> target);  
```  
  
**Parameters**  
  
`target` [IKeyboardFocusTarget\<ConnectionContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
### OnSelectionChanged(SelectionChangedEventArgs)  
  
```csharp  
protected override void OnSelectionChanged(SelectionChangedEventArgs e);  
```  
  
**Parameters**  
  
`e` [SelectionChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.SelectionChangedEventArgs)  
  
### Select(ConnectionContainer)  
  
```csharp  
public void Select(ConnectionContainer container);  
```  
  
**Parameters**  
  
`container` [ConnectionContainer](Nodify_ConnectionContainer)  
  
### TryMoveFocus(TraversalRequest)  
  
```csharp  
public virtual bool TryMoveFocus(TraversalRequest request);  
```  
  
**Parameters**  
  
`request` [TraversalRequest](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.TraversalRequest)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### TryRestoreFocus()  
  
```csharp  
public virtual bool TryRestoreFocus();  
```  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  

# DecoratorsControl Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) → [DecoratorsControl](Nodify_DecoratorsControl)  
  
**Implements:** [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
**References:** [DecoratorContainer](Nodify_DecoratorContainer), [IKeyboardFocusTarget\<DecoratorContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_), [IKeyboardFocusTarget\<UIElement\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_), [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId), [NodifyEditor](Nodify_NodifyEditor)  
  
An [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl) that works with [DecoratorContainer](Nodify_DecoratorContainer)s.  
  
```csharp  
public class DecoratorsControl : ItemsControl, IKeyboardNavigationLayer  
```  
  
## Constructors  
  
### DecoratorsControl()  
  
```csharp  
public DecoratorsControl();  
```  
  
## Properties  
  
### Editor  
  
Gets the [NodifyEditor](Nodify_NodifyEditor) that owns this [DecoratorsControl](Nodify_DecoratorsControl).  
  
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
  
## Methods  
  
### FindNextFocusTarget(DecoratorContainer, TraversalRequest)  
  
```csharp  
protected virtual DecoratorContainer FindNextFocusTarget(DecoratorContainer currentContainer, TraversalRequest request);  
```  
  
**Parameters**  
  
`currentContainer` [DecoratorContainer](Nodify_DecoratorContainer)  
  
`request` [TraversalRequest](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.TraversalRequest)  
  
**Returns**  
  
[DecoratorContainer](Nodify_DecoratorContainer)  
  
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
  
### OnElementFocused(IKeyboardFocusTarget\<DecoratorContainer\>)  
  
```csharp  
protected virtual void OnElementFocused(IKeyboardFocusTarget<DecoratorContainer> target);  
```  
  
**Parameters**  
  
`target` [IKeyboardFocusTarget\<DecoratorContainer\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
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
  

# IKeyboardNavigationLayer Interface  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Derived:** [ConnectionsMultiSelector](Nodify_ConnectionsMultiSelector), [NodifyEditor](Nodify_NodifyEditor), [DecoratorsControl](Nodify_DecoratorsControl)  
  
**References:** [IKeyboardFocusTarget\<UIElement\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_), [IKeyboardNavigationLayerGroup](Nodify_Interactivity_IKeyboardNavigationLayerGroup), [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId), [NodifyEditor](Nodify_NodifyEditor)  
  
Represents a layer of keyboard navigation that can handle focus movement and restoration.  
  
```csharp  
public interface IKeyboardNavigationLayer  
```  
  
## Properties  
  
### Id  
  
Gets the unique identifier for this keyboard navigation layer.  
  
```csharp  
public virtual KeyboardNavigationLayerId Id { get; set; }  
```  
  
**Property Value**  
  
[KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId)  
  
### LastFocusedElement  
  
Gets the last focused element within this layer, if any.  
  
```csharp  
public virtual IKeyboardFocusTarget<UIElement> LastFocusedElement { get; set; }  
```  
  
**Property Value**  
  
[IKeyboardFocusTarget\<UIElement\>](Nodify_Interactivity_IKeyboardFocusTarget_TElement_)  
  
## Methods  
  
### OnActivated()  
  
Called when the layer is activated, allowing for any necessary setup or focus management.  
  
```csharp  
public virtual void OnActivated();  
```  
  
### OnDeactivated()  
  
Called when the layer is deactivated, allowing for any necessary cleanup or focus management.  
  
```csharp  
public virtual void OnDeactivated();  
```  
  
### TryMoveFocus(TraversalRequest)  
  
Attempts to move focus within this layer based on the provided traversal request.  
  
```csharp  
public virtual bool TryMoveFocus(TraversalRequest request);  
```  
  
**Parameters**  
  
`request` [TraversalRequest](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.TraversalRequest): The traversal request.  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Returns true if the focus was moved, false otherwise.  
  
### TryRestoreFocus()  
  
Attempts to restore focus to the last focused element within this layer.  
  
```csharp  
public virtual bool TryRestoreFocus();  
```  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Returns true if the focus was restored, false otherwise.  
  

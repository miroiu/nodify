# IKeyboardNavigationLayerGroup Interface  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Implements:** [IReadOnlyCollection\<IKeyboardNavigationLayer\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1)  
  
**Derived:** [NodifyEditor](Nodify_NodifyEditor)  
  
**References:** [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer), [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId)  
  
Represents a group of keyboard navigation layers that can be activated and navigated through.  
  
```csharp  
public interface IKeyboardNavigationLayerGroup : IReadOnlyCollection<IKeyboardNavigationLayer>  
```  
  
## Properties  
  
### ActiveNavigationLayer  
  
The current active keyboard navigation layer in the group, if any.  
  
```csharp  
public virtual IKeyboardNavigationLayer ActiveNavigationLayer { get; set; }  
```  
  
**Property Value**  
  
[IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer)  
  
## Methods  
  
### ActivateNavigationLayer(KeyboardNavigationLayerId)  
  
Activates the specified keyboard navigation layer, making it the active layer for focus management.  
  
```csharp  
public virtual bool ActivateNavigationLayer(KeyboardNavigationLayerId layerId);  
```  
  
**Parameters**  
  
`layerId` [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId): The navigation layer id to activate.  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Returns true if the navigation layer was activated, false otherwise.  
  
### ActivateNextNavigationLayer()  
  
Activates the next keyboard navigation layer in the group, allowing focus to be restored to the last focused element in that layer.  
  
```csharp  
public virtual bool ActivateNextNavigationLayer();  
```  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Returns true if the navigation layer was activated, false otherwise.  
  
### ActivatePreviousNavigationLayer()  
  
Activates the previous keyboard navigation layer in the group, allowing focus to be restored to the last focused element in that layer.  
  
```csharp  
public virtual bool ActivatePreviousNavigationLayer();  
```  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Returns true if the navigation layer was activated, false otherwise.  
  
### RegisterNavigationLayer(IKeyboardNavigationLayer)  
  
Registers a new keyboard navigation layer to the group, allowing it to handle focus movement and restoration.  
  
```csharp  
public virtual bool RegisterNavigationLayer(IKeyboardNavigationLayer layer);  
```  
  
**Parameters**  
  
`layer` [IKeyboardNavigationLayer](Nodify_Interactivity_IKeyboardNavigationLayer): The navigation layer.  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### RemoveNavigationLayer(KeyboardNavigationLayerId)  
  
Removes the specified keyboard navigation layer from the group.  
  
```csharp  
public virtual bool RemoveNavigationLayer(KeyboardNavigationLayerId layerId);  
```  
  
**Parameters**  
  
`layerId` [KeyboardNavigationLayerId](Nodify_Interactivity_KeyboardNavigationLayerId): The navigation layer id.  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean): Returns true if the layer was removed, false otherwise.  
  
## Events  
  
### ActiveNavigationLayerChanged  
  
Event that is raised when the active keyboard navigation layer changes.  
  
```csharp  
public virtual event Action<KeyboardNavigationLayerId> ActiveNavigationLayerChanged;  
```  
  
**Event Type**  
  
[Action\<KeyboardNavigationLayerId\>](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1)  
  

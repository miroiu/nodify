# INodifyCanvasItem Interface  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Derived:** [ItemContainer](Nodify_ItemContainer), [DecoratorContainer](Nodify_DecoratorContainer)  
  
**References:** [NodifyCanvas](Nodify_NodifyCanvas)  
  
Interface for items inside a [NodifyCanvas](Nodify_NodifyCanvas).  
  
```csharp  
public interface INodifyCanvasItem  
```  
  
## Properties  
  
### DesiredSize  
  
The desired size of the item.  
  
```csharp  
public virtual Size DesiredSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### Location  
  
The location of the item.  
  
```csharp  
public virtual Point Location { get; set; }  
```  
  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
## Methods  
  
### Arrange(Rect)  
  
```csharp  
public virtual void Arrange(Rect rect);  
```  
  
**Parameters**  
  
`rect` [Rect](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Rect)  
  

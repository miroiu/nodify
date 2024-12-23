# PreviewLocationChanged Delegate  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [PreviewLocationChanged](Nodify_Events_PreviewLocationChanged)  
  
**References:** [ItemContainer](Nodify_ItemContainer)  
  
Delegate used to notify when an [ItemContainer](Nodify_ItemContainer) is previewing a new location.  
  
```csharp  
public delegate void PreviewLocationChanged(Point newLocation);  
```  
  
**Parameters**  
  
`newLocation` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The new location.  
  

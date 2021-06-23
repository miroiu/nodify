# PreviewLocationChangedDelegate Delegate  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate) → [MulticastDelegate](https://docs.microsoft.com/en-us/dotnet/api/System.MulticastDelegate) → [PreviewLocationChangedDelegate](PreviewLocationChangedDelegate)  
  
**References:** [ItemContainer](ItemContainer)  
  
Delegate used to notify when an [ItemContainer](ItemContainer) is previewing a new location.  
  
```csharp  
public delegate void PreviewLocationChangedDelegate(Point newLocation);  
```  
**Parameters**  
  
`newLocation` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point): The new location.  
  

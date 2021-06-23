# SelectionHelper Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [SelectionHelper](SelectionHelper)  
  
**References:** [NodifyEditor](NodifyEditor)  
  
```csharp  
public sealed class SelectionHelper  
```  
## Constructors  
  
### SelectionHelper(NodifyEditor)  
  
```csharp  
public SelectionHelper(NodifyEditor host);  
```  
**Parameters**  
  
`host` [NodifyEditor](NodifyEditor)  
  
## Methods  
  
### End()  
  
```csharp  
public void End();  
```  
### Start(Point, SelectionType?)  
  
```csharp  
public void Start(Point location, SelectionType? selectionType = null);  
```  
**Parameters**  
  
`location` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`selectionType` [SelectionType?](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable)  
  
### Update(Point)  
  
```csharp  
public void Update(Point endLocation);  
```  
**Parameters**  
  
`endLocation` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  

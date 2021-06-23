# Connection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](BaseConnection) → [Connection](Connection)  
  
**References:** [Connector](Connector), [NodifyEditor](NodifyEditor)  
  
Represents a quadratic curve.  
  
```csharp  
public class Connection : BaseConnection  
```  
## Constructors  
  
### Connection()  
  
Initializes a new instance of the [Connection](Connection) class.  
  
```csharp  
public Connection();  
```  
## Properties  
  
### DefiningGeometry  
  
```csharp  
protected override Geometry DefiningGeometry { get; set; }  
```  
**Property Value**  
  
[Geometry](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Geometry)  
  

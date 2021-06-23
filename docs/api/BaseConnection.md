# BaseConnection Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Shape](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Shapes.Shape) → [BaseConnection](BaseConnection)  
  
**Derived:** [DirectionalConnection](DirectionalConnection), [Connection](Connection)  
  
**References:** [ConnectionOffsetMode](ConnectionOffsetMode), [ConnectionDirection](ConnectionDirection), [DirectionalConnection](DirectionalConnection), [NodifyEditor](NodifyEditor)  
  
Represents the base class for shapes that are drawn from a [BaseConnection.Source](BaseConnection#source) point to a [BaseConnection.Target](BaseConnection#target) point.  
  
```csharp  
public abstract class BaseConnection : Shape  
```  
## Constructors  
  
### BaseConnection()  
  
```csharp  
protected BaseConnection();  
```  
## Fields  
  
### DirectionProperty  
  
```csharp  
public static DependencyProperty DirectionProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### OffsetModeProperty  
  
```csharp  
public static DependencyProperty OffsetModeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### SourceOffsetProperty  
  
```csharp  
public static DependencyProperty SourceOffsetProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### SourceProperty  
  
```csharp  
public static DependencyProperty SourceProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### TargetOffsetProperty  
  
```csharp  
public static DependencyProperty TargetOffsetProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### TargetProperty  
  
```csharp  
public static DependencyProperty TargetProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ZeroVector  
  
Gets a vector that has its coordinates set to 0.  
  
```csharp  
protected static Vector ZeroVector;  
```  
**Field Value**  
  
[Vector](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Vector)  
  
## Properties  
  
### Direction  
  
Gets or sets the direction in which this connection is oriented.  
  
```csharp  
public ConnectionDirection Direction { get; set; }  
```  
**Property Value**  
  
[ConnectionDirection](ConnectionDirection)  
  
### OffsetMode  
  
Gets or sets the [ConnectionOffsetMode](ConnectionOffsetMode) to apply when drawing the connection.  
  
```csharp  
public ConnectionOffsetMode OffsetMode { get; set; }  
```  
**Property Value**  
  
[ConnectionOffsetMode](ConnectionOffsetMode)  
  
### Source  
  
Gets or sets the start point of this connection.  
  
```csharp  
public Point Source { get; set; }  
```  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### SourceOffset  
  
Gets or sets the offset from the [BaseConnection.Source](BaseConnection#source) point.  
  
```csharp  
public Size SourceOffset { get; set; }  
```  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### Target  
  
Gets or sets the end point of this connection.  
  
```csharp  
public Point Target { get; set; }  
```  
**Property Value**  
  
[Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
### TargetOffset  
  
Gets or sets the offset from the [BaseConnection.Target](BaseConnection#target) point.  
  
```csharp  
public Size TargetOffset { get; set; }  
```  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
## Methods  
  
### GetOffset()  
  
Gets the resulting offset after applying the [BaseConnection.OffsetMode](BaseConnection#offsetmode).  
  
```csharp  
protected virtual ValueTuple<Vector, Vector> GetOffset();  
```  
**Returns**  
  
[ValueTuple<Vector, Vector>](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple)  
  
### OnMouseLeftButtonDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e);  
```  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseLeftButtonUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e);  
```  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  

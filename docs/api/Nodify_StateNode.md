# StateNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](Nodify_Connector) → [StateNode](Nodify_StateNode)  
  
**References:** [PendingConnection](Nodify_PendingConnection)  
  
Represents a control that acts as a [Connector](Nodify_Connector).  
  
```csharp  
public class StateNode : Connector  
```  
  
## Constructors  
  
### StateNode()  
  
```csharp  
public StateNode();  
```  
  
## Fields  
  
### ElementContent  
  
```csharp  
protected const string ElementContent = "PART_Content";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
## Properties  
  
### Content  
  
Gets or sets the data for the control's content.  
  
```csharp  
public object Content { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### ContentControl  
  
Gets the [StateNode.ContentControl](Nodify_StateNode#contentcontrol) control of this [StateNode](Nodify_StateNode).  
  
```csharp  
protected UIElement ContentControl { get; set; }  
```  
  
**Property Value**  
  
[UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
### ContentTemplate  
  
Gets or sets the template used to display the content of the control's header.  
  
```csharp  
public DataTemplate ContentTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### CornerRadius  
  
Gets or sets a value that represents the degree to which the corners of the [StateNode](Nodify_StateNode) are rounded.  
  
```csharp  
public CornerRadius CornerRadius { get; set; }  
```  
  
**Property Value**  
  
[CornerRadius](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.CornerRadius)  
  
### HighlightBrush  
  
Gets or sets the brush used when the [PendingConnection.IsOverElementProperty](Nodify_PendingConnection#isoverelementproperty) attached property is true for this [StateNode](Nodify_StateNode).  
  
```csharp  
public Brush HighlightBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
## Methods  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
### OnMouseDown(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseDown(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  
### OnMouseUp(MouseButtonEventArgs)  
  
```csharp  
protected override void OnMouseUp(MouseButtonEventArgs e);  
```  
  
**Parameters**  
  
`e` [MouseButtonEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.MouseButtonEventArgs)  
  

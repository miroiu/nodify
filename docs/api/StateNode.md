# StateNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](Connector) → [StateNode](StateNode)  
  
**References:** [Connector](Connector), [PendingConnection](PendingConnection)  
  
Represents a control that acts as a [Connector](Connector).  
  
```csharp  
public class StateNode : Connector  
```  
## Constructors  
  
### StateNode()  
  
```csharp  
public StateNode();  
```  
## Fields  
  
### ActualSizeProperty  
  
```csharp  
public static DependencyProperty ActualSizeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ContentProperty  
  
```csharp  
public static DependencyProperty ContentProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ContentTemplateProperty  
  
```csharp  
public static DependencyProperty ContentTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### CornerRadiusProperty  
  
```csharp  
public static DependencyProperty CornerRadiusProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ElementContent  
  
```csharp  
protected const string ElementContent = "PART_Content";  
```  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### HighlightBrushProperty  
  
```csharp  
public static DependencyProperty HighlightBrushProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
## Properties  
  
### ActualSize  
  
Gets or sets the actual size of this [StateNode](StateNode).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### Content  
  
Gets or sets the data for the control's content.  
  
```csharp  
public object Content { get; set; }  
```  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### ContentControl  
  
Gets the [StateNode.ContentControl](StateNode#contentcontrol) control of this [StateNode](StateNode).  
  
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
  
Gets or sets a value that represents the degree to which the corners of the [StateNode](StateNode) are rounded.  
  
```csharp  
public CornerRadius CornerRadius { get; set; }  
```  
**Property Value**  
  
[CornerRadius](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.CornerRadius)  
  
### HighlightBrush  
  
Gets or sets the brush used when the [PendingConnection.IsOverElementProperty](PendingConnection#isoverelementproperty) attached property is true for this [StateNode](StateNode).  
  
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
  
### OnRenderSizeChanged(SizeChangedInfo)  
  
```csharp  
protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);  
```  
**Parameters**  
  
`sizeInfo` [SizeChangedInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.SizeChangedInfo)  
  

# Node Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [Node](Node)  
  
**References:** [Connector](Connector), [GroupingNode](GroupingNode), [NodeInput](NodeInput), [NodeOutput](NodeOutput)  
  
Represents a control that has a list of [Node.Input](Node#input)[Connector](Connector)s and a list of [Node.Output](Node#output)[Connector](Connector)s.  
  
```csharp  
public class Node : HeaderedContentControl  
```  
## Constructors  
  
### Node()  
  
```csharp  
public Node();  
```  
## Fields  
  
### FooterBrushProperty  
  
```csharp  
public static DependencyProperty FooterBrushProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### FooterProperty  
  
```csharp  
public static DependencyProperty FooterProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### FooterTemplateProperty  
  
```csharp  
public static DependencyProperty FooterTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### HeaderBrushProperty  
  
```csharp  
public static DependencyProperty HeaderBrushProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### IconProperty  
  
```csharp  
public static DependencyProperty IconProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### InputConnectorTemplateProperty  
  
```csharp  
public static DependencyProperty InputConnectorTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### InputProperty  
  
```csharp  
public static DependencyProperty InputProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### OutputConnectorTemplateProperty  
  
```csharp  
public static DependencyProperty OutputConnectorTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### OutputProperty  
  
```csharp  
public static DependencyProperty OutputProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
## Properties  
  
### Footer  
  
Gets or sets the data for the footer of this control.  
  
```csharp  
public object Footer { get; set; }  
```  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### FooterBrush  
  
Gets or sets the brush used for the background of the [Node.Footer](Node#footer) of this [GroupingNode](GroupingNode).  
  
```csharp  
public Brush FooterBrush { get; set; }  
```  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### FooterTemplate  
  
Gets or sets the template used to display the content of the control's footer.  
  
```csharp  
public DataTemplate FooterTemplate { get; set; }  
```  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### HeaderBrush  
  
Gets or sets the brush used for the background of the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header) of this [GroupingNode](GroupingNode).  
  
```csharp  
public Brush HeaderBrush { get; set; }  
```  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### Icon  
  
Gets or sets the icon to display in the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header).  
  
```csharp  
public object Icon { get; set; }  
```  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### Input  
  
Gets or sets the data for the input [Connector](Connector)s of this control.  
  
```csharp  
public IEnumerable Input { get; set; }  
```  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### InputConnectorTemplate  
  
Gets or sets the template used to display the content of the control's [Node.Input](Node#input) connectors.  
  
```csharp  
public DataTemplate InputConnectorTemplate { get; set; }  
```  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### Output  
  
Gets or sets the data for the output [Connector](Connector)s of this control.  
  
```csharp  
public IEnumerable Output { get; set; }  
```  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### OutputConnectorTemplate  
  
Gets or sets the template used to display the content of the control's [Node.Output](Node#output) connectors.  
  
```csharp  
public DataTemplate OutputConnectorTemplate { get; set; }  
```  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  

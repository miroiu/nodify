# Node Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [Node](Nodify_Node)  
  
**References:** [Connector](Nodify_Connector), [NodeInput](Nodify_NodeInput), [NodeOutput](Nodify_NodeOutput)  
  
Represents a control that has a list of [Node.Input](Nodify_Node#input)[Connector](Nodify_Connector)s and a list of [Node.Output](Nodify_Node#output)[Connector](Nodify_Connector)s.  
  
```csharp  
public class Node : HeaderedContentControl  
```  
  
## Constructors  
  
### Node()  
  
```csharp  
public Node();  
```  
  
## Fields  
  
### ElementInputItemsControl  
  
```csharp  
protected const string ElementInputItemsControl = "PART_Input";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### ElementOutputItemsControl  
  
```csharp  
protected const string ElementOutputItemsControl = "PART_Output";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### HasFooterPropertyKey  
  
```csharp  
protected static DependencyPropertyKey HasFooterPropertyKey;  
```  
  
**Field Value**  
  
[DependencyPropertyKey](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyPropertyKey)  
  
## Properties  
  
### ContentBrush  
  
Gets or sets the brush used for the background of the [ContentControl.Content](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl#content) of this [Node](Nodify_Node).  
  
```csharp  
public Brush ContentBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### ContentContainerStyle  
  
Gets or sets the style for the content container.  
  
```csharp  
public Style ContentContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### Footer  
  
Gets or sets the data for the footer of this control.  
  
```csharp  
public object Footer { get; set; }  
```  
  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### FooterBrush  
  
Gets or sets the brush used for the background of the [Node.Footer](Nodify_Node#footer) of this [Node](Nodify_Node).  
  
```csharp  
public Brush FooterBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### FooterContainerStyle  
  
Gets or sets the style for the footer container.  
  
```csharp  
public Style FooterContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### FooterTemplate  
  
Gets or sets the template used to display the content of the control's footer.  
  
```csharp  
public DataTemplate FooterTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### HasFooter  
  
Gets a value that indicates whether the [Node.Footer](Nodify_Node#footer) is .  
  
```csharp  
public bool HasFooter { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### HeaderBrush  
  
Gets or sets the brush used for the background of the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl#header) of this [Node](Nodify_Node).  
  
```csharp  
public Brush HeaderBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### HeaderContainerStyle  
  
Gets or sets the style for the header container.  
  
```csharp  
public Style HeaderContainerStyle { get; set; }  
```  
  
**Property Value**  
  
[Style](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Style)  
  
### Input  
  
Gets or sets the data for the input [Connector](Nodify_Connector)s of this control.  
  
```csharp  
public IEnumerable Input { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### InputConnectorTemplate  
  
Gets or sets the template used to display the content of the control's [Node.Input](Nodify_Node#input) connectors.  
  
```csharp  
public DataTemplate InputConnectorTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### InputGroupStyle  
  
```csharp  
public ObservableCollection<GroupStyle> InputGroupStyle { get; set; }  
```  
  
**Property Value**  
  
[ObservableCollection\<GroupStyle\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.ObjectModel.ObservableCollection-1)  
  
### InputItemsControl  
  
```csharp  
protected ItemsControl InputItemsControl { get; set; }  
```  
  
**Property Value**  
  
[ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl)  
  
### Output  
  
Gets or sets the data for the output [Connector](Nodify_Connector)s of this control.  
  
```csharp  
public IEnumerable Output { get; set; }  
```  
  
**Property Value**  
  
[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable)  
  
### OutputConnectorTemplate  
  
Gets or sets the template used to display the content of the control's [Node.Output](Nodify_Node#output) connectors.  
  
```csharp  
public DataTemplate OutputConnectorTemplate { get; set; }  
```  
  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  
### OutputGroupStyle  
  
```csharp  
public ObservableCollection<GroupStyle> OutputGroupStyle { get; set; }  
```  
  
**Property Value**  
  
[ObservableCollection\<GroupStyle\>](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.ObjectModel.ObservableCollection-1)  
  
### OutputItemsControl  
  
```csharp  
protected ItemsControl OutputItemsControl { get; set; }  
```  
  
**Property Value**  
  
[ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ItemsControl)  
  
## Methods  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  

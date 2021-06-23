# NodeOutput Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](Connector) → [NodeOutput](NodeOutput)  
  
**References:** [Node](Node), [Connector](Connector)  
  
Represents the default control for the [Node.OutputConnectorTemplate](Node#outputconnectortemplate).  
  
```csharp  
public class NodeOutput : Connector  
```  
## Constructors  
  
### NodeOutput()  
  
```csharp  
public NodeOutput();  
```  
## Fields  
  
### ConnectorTemplateProperty  
  
```csharp  
public static DependencyProperty ConnectorTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### HeaderProperty  
  
```csharp  
public static DependencyProperty HeaderProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### HeaderTemplateProperty  
  
```csharp  
public static DependencyProperty HeaderTemplateProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
## Properties  
  
### ConnectorTemplate  
  
Gets or sets the template used to display the connecting point of this [Connector](Connector).  
  
```csharp  
public ControlTemplate ConnectorTemplate { get; set; }  
```  
**Property Value**  
  
[ControlTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ControlTemplate)  
  
### Header  
  
Gets of sets the data used for the control's header.  
  
```csharp  
public object Header { get; set; }  
```  
**Property Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### HeaderTemplate  
  
Gets or sets the template used to display the content of the control's header.  
  
```csharp  
public DataTemplate HeaderTemplate { get; set; }  
```  
**Property Value**  
  
[DataTemplate](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DataTemplate)  
  

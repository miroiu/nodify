# NodeOutput Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [Connector](Nodify_Connector) → [NodeOutput](Nodify_NodeOutput)  
  
**References:** [Node](Nodify_Node)  
  
Represents the default control for the [Node.OutputConnectorTemplate](Nodify_Node#outputconnectortemplate).  
  
```csharp  
public class NodeOutput : Connector  
```  
  
## Constructors  
  
### NodeOutput()  
  
```csharp  
public NodeOutput();  
```  
  
## Properties  
  
### ConnectorTemplate  
  
Gets or sets the template used to display the connecting point of this [Connector](Nodify_Connector).  
  
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
  
### Orientation  
  
```csharp  
public Orientation Orientation { get; set; }  
```  
  
**Property Value**  
  
[Orientation](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Orientation)  
  

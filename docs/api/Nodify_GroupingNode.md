# GroupingNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [GroupingNode](Nodify_GroupingNode)  
  
**References:** [GroupingMovementMode](Nodify_GroupingMovementMode), [ItemContainer](Nodify_ItemContainer), [NodifyEditor](Nodify_NodifyEditor), [ResizeEventHandler](Nodify_Events_ResizeEventHandler)  
  
Defines a panel with a header that groups [ItemContainer](Nodify_ItemContainer)s inside it and can be resized.  
  
```csharp  
public class GroupingNode : HeaderedContentControl  
```  
  
## Constructors  
  
### GroupingNode()  
  
Initializes a new instance of the [GroupingNode](Nodify_GroupingNode) class.  
  
```csharp  
public GroupingNode();  
```  
  
## Fields  
  
### ContentControl  
  
Gets the [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) control of this [GroupingNode](Nodify_GroupingNode).  
  
```csharp  
protected FrameworkElement ContentControl;  
```  
  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### ElementContent  
  
```csharp  
protected const string ElementContent = "PART_Content";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### ElementHeader  
  
```csharp  
protected const string ElementHeader = "PART_Header";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### ElementResizeThumb  
  
```csharp  
protected const string ElementResizeThumb = "PART_ResizeThumb";  
```  
  
**Field Value**  
  
[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)  
  
### GroupMovementBoxed  
  
```csharp  
protected static object GroupMovementBoxed;  
```  
  
**Field Value**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### HeaderControl  
  
Gets the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl#header) control of this [GroupingNode](Nodify_GroupingNode).  
  
```csharp  
protected FrameworkElement HeaderControl;  
```  
  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### ResizeThumb  
  
Gets the [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) used to resize this [GroupingNode](Nodify_GroupingNode).  
  
```csharp  
protected FrameworkElement ResizeThumb;  
```  
  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
## Properties  
  
### ActualSize  
  
Gets or sets the actual size of this [GroupingNode](Nodify_GroupingNode).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### CanResize  
  
Gets or sets a value that indicates whether this [GroupingNode](Nodify_GroupingNode) can be resized.  
  
```csharp  
public bool CanResize { get; set; }  
```  
  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Container  
  
Gets the [NodifyEditor](Nodify_NodifyEditor) that owns this [GroupingNode.Container](Nodify_GroupingNode#container).  
  
```csharp  
protected ItemContainer Container { get; set; }  
```  
  
**Property Value**  
  
[ItemContainer](Nodify_ItemContainer)  
  
### Editor  
  
Gets the [NodifyEditor](Nodify_NodifyEditor) that owns this [GroupingNode](Nodify_GroupingNode).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
  
**Property Value**  
  
[NodifyEditor](Nodify_NodifyEditor)  
  
### HeaderBrush  
  
Gets or sets the brush used for the background of the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl#header) of this [GroupingNode](Nodify_GroupingNode).  
  
```csharp  
public Brush HeaderBrush { get; set; }  
```  
  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### MovementMode  
  
Gets or sets the default movement mode which can be temporarily changed by holding the SwitchMovementModeModifierKey while dragging by the header.  
  
```csharp  
public GroupingMovementMode MovementMode { get; set; }  
```  
  
**Property Value**  
  
[GroupingMovementMode](Nodify_GroupingMovementMode)  
  
### ResizeCompletedCommand  
  
Invoked when the [GroupingNode.ResizeCompleted](Nodify_GroupingNode#resizecompleted) event is not handled.
            Parameter is the [ItemContainer.ActualSize](Nodify_ItemContainer#actualsize) of the container.  
  
```csharp  
public ICommand ResizeCompletedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### ResizeStartedCommand  
  
Invoked when the [GroupingNode.ResizeStarted](Nodify_GroupingNode#resizestarted) event is not handled.
            Parameter is the [ItemContainer.ActualSize](Nodify_ItemContainer#actualsize) of the container.  
  
```csharp  
public ICommand ResizeStartedCommand { get; set; }  
```  
  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
## Methods  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
  
### ToggleContentSelection()  
  
Toggles the selection of nodes inside this group.
            If any contained nodes are selected, all will be unselected.
            If none are selected, all will be selected.  
  
```csharp  
public void ToggleContentSelection();  
```  
  
## Events  
  
### ResizeCompleted  
  
Occurs when the node finished resizing.  
  
```csharp  
public event ResizeEventHandler ResizeCompleted;  
```  
  
**Event Type**  
  
[ResizeEventHandler](Nodify_Events_ResizeEventHandler)  
  
### ResizeStarted  
  
Occurs when the node started resizing.  
  
```csharp  
public event ResizeEventHandler ResizeStarted;  
```  
  
**Event Type**  
  
[ResizeEventHandler](Nodify_Events_ResizeEventHandler)  
  

# GroupingNode Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DispatcherObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Threading.DispatcherObject) → [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject) → [Visual](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Visual) → [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement) → [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) → [Control](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.Control) → [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) → [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl) → [GroupingNode](GroupingNode)  
  
**References:** [ResizeEventHandler](ResizeEventHandler), [GroupingMovementMode](GroupingMovementMode), [NodifyEditor](NodifyEditor), [ItemContainer](ItemContainer), [Node](Node)  
  
Defines a panel with a header that groups [ItemContainer](ItemContainer)s inside it and can be resized.  
  
```csharp  
public class GroupingNode : HeaderedContentControl  
```  
## Constructors  
  
### GroupingNode()  
  
Initializes a new instance of the [GroupingNode](GroupingNode) class.  
  
```csharp  
public GroupingNode();  
```  
## Fields  
  
### ActualSizeProperty  
  
```csharp  
public static DependencyProperty ActualSizeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### CanResizeProperty  
  
```csharp  
public static DependencyProperty CanResizeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ContentControl  
  
Gets the [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.ContentControl) control of this [GroupingNode](GroupingNode).  
  
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
  
### HeaderBrushProperty  
  
```csharp  
public static DependencyProperty HeaderBrushProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### HeaderControl  
  
Gets the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header) control of this [GroupingNode](GroupingNode).  
  
```csharp  
protected FrameworkElement HeaderControl;  
```  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
### MovementModeProperty  
  
```csharp  
public static DependencyProperty MovementModeProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ResizeCompletedCommandProperty  
  
```csharp  
public static DependencyProperty ResizeCompletedCommandProperty;  
```  
**Field Value**  
  
[DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### ResizeCompletedEvent  
  
```csharp  
public static RoutedEvent ResizeCompletedEvent;  
```  
**Field Value**  
  
[RoutedEvent](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEvent)  
  
### ResizeThumb  
  
Gets the [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement) used to resize this [GroupingNode](GroupingNode).  
  
```csharp  
protected FrameworkElement ResizeThumb;  
```  
**Field Value**  
  
[FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.FrameworkElement)  
  
## Properties  
  
### ActualSize  
  
Gets or sets the actual size of this [GroupingNode](GroupingNode).  
  
```csharp  
public Size ActualSize { get; set; }  
```  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### CanResize  
  
Gets or sets a value that indicates whether this [GroupingNode](GroupingNode) can be resized.  
  
```csharp  
public bool CanResize { get; set; }  
```  
**Property Value**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  
### Container  
  
Gets the [NodifyEditor](NodifyEditor) that owns this [GroupingNode.Container](GroupingNode#container).  
  
```csharp  
protected ItemContainer Container { get; set; }  
```  
**Property Value**  
  
[ItemContainer](ItemContainer)  
  
### Editor  
  
Gets the [NodifyEditor](NodifyEditor) that owns this [GroupingNode](GroupingNode).  
  
```csharp  
protected NodifyEditor Editor { get; set; }  
```  
**Property Value**  
  
[NodifyEditor](NodifyEditor)  
  
### HeaderBrush  
  
Gets or sets the brush used for the background of the [HeaderedContentControl.Header](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Controls.HeaderedContentControl.header) of this [GroupingNode](GroupingNode).  
  
```csharp  
public Brush HeaderBrush { get; set; }  
```  
**Property Value**  
  
[Brush](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Media.Brush)  
  
### MovementMode  
  
Gets or sets the default movement mode which can be temporarily changed by holding the [GroupingNode.SwitchMovementModeModifierKey](GroupingNode#switchmovementmodemodifierkey) while dragging by the header.  
  
```csharp  
public GroupingMovementMode MovementMode { get; set; }  
```  
**Property Value**  
  
[GroupingMovementMode](GroupingMovementMode)  
  
### ResizeCompletedCommand  
  
Invoked when the [GroupingNode.ResizeCompleted](GroupingNode#resizecompleted) event is not handled.
            Parameter is the [GroupingNode.ActualSize](GroupingNode#actualsize) of this control.  
  
```csharp  
public ICommand ResizeCompletedCommand { get; set; }  
```  
**Property Value**  
  
[ICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ICommand)  
  
### SwitchMovementModeModifierKey  
  
Gets or sets the key that will toggle between [GroupingMovementMode](GroupingMovementMode)s.  
  
```csharp  
public static ModifierKeys SwitchMovementModeModifierKey { get; set; }  
```  
**Property Value**  
  
[ModifierKeys](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.ModifierKeys)  
  
## Methods  
  
### OnApplyTemplate()  
  
```csharp  
public override void OnApplyTemplate();  
```  
## Events  
  
### ResizeCompleted  
  
Occurs when the node is resized and [GroupingNode.ActualSize](GroupingNode#actualsize) is set.  
  
```csharp  
public event ResizeEventHandler ResizeCompleted;  
```  
**Event Type**  
  
[ResizeEventHandler](ResizeEventHandler)  
  

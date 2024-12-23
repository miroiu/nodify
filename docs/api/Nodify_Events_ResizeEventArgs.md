# ResizeEventArgs Class  
  
**Namespace:** Nodify.Events  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.EventArgs) → [RoutedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.RoutedEventArgs) → [ResizeEventArgs](Nodify_Events_ResizeEventArgs)  
  
**References:** [ResizeEventHandler](Nodify_Events_ResizeEventHandler)  
  
Provides data for resize related routed events.  
  
```csharp  
public class ResizeEventArgs : RoutedEventArgs  
```  
  
## Constructors  
  
### ResizeEventArgs(Size, Size)  
  
Initializes a new instance of the [ResizeEventArgs](Nodify_Events_ResizeEventArgs) class with the previous and the new [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size).  
  
```csharp  
public ResizeEventArgs(Size previousSize, Size newSize);  
```  
  
**Parameters**  
  
`previousSize` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size): The previous size associated with this event.  
  
`newSize` [Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size): The new size associated with this event.  
  
## Properties  
  
### NewSize  
  
Gets the new size of the object.  
  
```csharp  
public Size NewSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
### PreviousSize  
  
Gets the previous size of the object.  
  
```csharp  
public Size PreviousSize { get; set; }  
```  
  
**Property Value**  
  
[Size](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Size)  
  
## Methods  
  
### InvokeEventHandler(Delegate, Object)  
  
```csharp  
protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget);  
```  
  
**Parameters**  
  
`genericHandler` [Delegate](https://docs.microsoft.com/en-us/dotnet/api/System.Delegate)  
  
`genericTarget` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  

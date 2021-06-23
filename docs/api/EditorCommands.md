# EditorCommands Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorCommands](EditorCommands)  
  
**References:** [NodifyEditor](NodifyEditor), [ItemContainer](ItemContainer)  
  
```csharp  
public static class EditorCommands  
```  
## Properties  
  
### Align  
  
Aligns [NodifyEditor.SelectedItems](NodifyEditor#selecteditems) using the specified alignment method.
            Parameter is of type Nodify.EditorCommands.Alignment or a string that can be converted to an alignment.  
  
```csharp  
public static RoutedUICommand Align { get; set; }  
```  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### BringIntoView  
  
Moves the [NodifyEditor.Viewport](NodifyEditor#viewport) to the specified location.
            Parameter is a [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point) or a string that can be converted to a point.  
  
```csharp  
public static RoutedUICommand BringIntoView { get; set; }  
```  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### Delete  
  
Delete [NodifyEditor.SelectedItems](NodifyEditor#selecteditems) if the ItemsSource is not bound.  
  
```csharp  
public static RoutedUICommand Delete { get; set; }  
```  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### SelectAll  
  
Select all [ItemContainer](ItemContainer)s in the [NodifyEditor](NodifyEditor).  
  
```csharp  
public static RoutedUICommand SelectAll { get; set; }  
```  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### ZoomIn  
  
Zoom in relative to the [NodifyEditor.Viewport](NodifyEditor#viewport)'s center.  
  
```csharp  
public static RoutedUICommand ZoomIn { get; set; }  
```  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### ZoomOut  
  
Zoom out relative to the [NodifyEditor.Viewport](NodifyEditor#viewport)'s center.  
  
```csharp  
public static RoutedUICommand ZoomOut { get; set; }  
```  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  

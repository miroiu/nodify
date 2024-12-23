# EditorCommands Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorCommands](Nodify_EditorCommands)  
  
**References:** [Alignment](Nodify_Alignment), [InputGestureRef](Nodify_Interactivity_InputGestureRef), [ItemContainer](Nodify_ItemContainer), [NodifyEditor](Nodify_NodifyEditor)  
  
Provides common commands for the [NodifyEditor](Nodify_NodifyEditor).  
  
```csharp  
public static class EditorCommands  
```  
  
## Properties  
  
### Align  
  
Aligns the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers) using the specified alignment method.
            Parameter is of type [Alignment](Nodify_Alignment) or a string that can be converted to an alignment.  
  
```csharp  
public static RoutedUICommand Align { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### BringIntoView  
  
Moves the [NodifyEditor.ViewportLocation](Nodify_NodifyEditor#viewportlocation) to the specified location.
            Parameter is a [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point) or a string that can be converted to a point.  
  
```csharp  
public static RoutedUICommand BringIntoView { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### FitToScreen  
  
Scales the editor's viewport to fit all the [ItemContainer](Nodify_ItemContainer)s if that's possible.  
  
```csharp  
public static RoutedUICommand FitToScreen { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### LockSelection  
  
Locks the position of the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers).  
  
```csharp  
public static RoutedUICommand LockSelection { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### SelectAll  
  
Select all [ItemContainer](Nodify_ItemContainer)s in the [NodifyEditor](Nodify_NodifyEditor).  
  
```csharp  
public static RoutedUICommand SelectAll { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### UnlockSelection  
  
Unlocks the position of the [NodifyEditor.SelectedContainers](Nodify_NodifyEditor#selectedcontainers).  
  
```csharp  
public static RoutedUICommand UnlockSelection { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### ZoomIn  
  
Zoom in relative to the editor's viewport center.  
  
```csharp  
public static RoutedUICommand ZoomIn { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  
### ZoomOut  
  
Zoom out relative to the editor's viewport center.  
  
```csharp  
public static RoutedUICommand ZoomOut { get; set; }  
```  
  
**Property Value**  
  
[RoutedUICommand](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.RoutedUICommand)  
  

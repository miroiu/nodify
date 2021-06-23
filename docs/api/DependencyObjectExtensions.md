# DependencyObjectExtensions Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [DependencyObjectExtensions](DependencyObjectExtensions)  
  
```csharp  
public static class DependencyObjectExtensions  
```  
## Methods  
  
### CancelAnimation(UIElement, DependencyProperty)  
  
```csharp  
public static void CancelAnimation(UIElement animatableElement, DependencyProperty dependencyProperty);  
```  
**Parameters**  
  
`animatableElement` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`dependencyProperty` [DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
### GetElementUnderMouse(UIElement)  
  
```csharp  
public static T GetElementUnderMouse<T>(UIElement container);  
```  
**Parameters**  
  
`container` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
**Returns**  
  
T  
  
### GetParentOfType(DependencyObject)  
  
```csharp  
public static T GetParentOfType<T>(DependencyObject child);  
```  
**Parameters**  
  
`child` [DependencyObject](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyObject)  
  
**Returns**  
  
T  
  
### StartAnimation(UIElement, DependencyProperty, Point, Double, EventHandler)  
  
```csharp  
public static void StartAnimation(UIElement animatableElement, DependencyProperty dependencyProperty, Point toValue, double animationDurationSeconds, EventHandler completedEvent = null);  
```  
**Parameters**  
  
`animatableElement` [UIElement](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.UIElement)  
  
`dependencyProperty` [DependencyProperty](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.DependencyProperty)  
  
`toValue` [Point](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Point)  
  
`animationDurationSeconds` [Double](https://docs.microsoft.com/en-us/dotnet/api/System.Double)  
  
`completedEvent` [EventHandler](https://docs.microsoft.com/en-us/dotnet/api/System.EventHandler)  
  

# DebugConverter Class  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [MarkupExtension](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Markup.MarkupExtension) → [DebugConverter](DebugConverter)  
  
**Implements:** [IValueConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Data.IValueConverter)  
  
```csharp  
public class DebugConverter : MarkupExtension, IValueConverter  
```  
## Constructors  
  
### DebugConverter()  
  
```csharp  
public DebugConverter();  
```  
## Methods  
  
### Convert(Object, Type, Object, CultureInfo)  
  
```csharp  
public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture);  
```  
**Parameters**  
  
`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`targetType` [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type)  
  
`parameter` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Globalization.CultureInfo)  
  
**Returns**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### ConvertBack(Object, Type, Object, CultureInfo)  
  
```csharp  
public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);  
```  
**Parameters**  
  
`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`targetType` [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type)  
  
`parameter` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Globalization.CultureInfo)  
  
**Returns**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
### ProvideValue(IServiceProvider)  
  
```csharp  
public override object ProvideValue(IServiceProvider serviceProvider);  
```  
**Parameters**  
  
`serviceProvider` [IServiceProvider](https://docs.microsoft.com/en-us/dotnet/api/System.IServiceProvider)  
  
**Returns**  
  
[Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  

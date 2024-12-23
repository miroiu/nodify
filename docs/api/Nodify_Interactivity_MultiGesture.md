# MultiGesture Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [InputGesture](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture) → [MultiGesture](Nodify_Interactivity_MultiGesture)  
  
**Derived:** [AllGestures](Nodify_Interactivity_AllGestures), [AnyGesture](Nodify_Interactivity_AnyGesture)  
  
**References:** [MultiGesture.Match](Nodify_Interactivity_MultiGesture_Match)  
  
Combines multiple input gestures.  
  
```csharp  
public class MultiGesture : InputGesture  
```  
  
## Constructors  
  
### MultiGesture(MultiGesture.Match, InputGesture[])  
  
Constructs an instance of a [MultiGesture](Nodify_Interactivity_MultiGesture).  
  
```csharp  
public MultiGesture(Match match, InputGesture[] gestures);  
```  
  
**Parameters**  
  
`match` [MultiGesture.Match](Nodify_Interactivity_MultiGesture_Match): The matching strategy.  
  
`gestures` [InputGesture[]](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputGesture[]): The input gestures.  
  
## Fields  
  
### None  
  
```csharp  
public static MultiGesture None;  
```  
  
**Field Value**  
  
[MultiGesture](Nodify_Interactivity_MultiGesture)  
  
## Methods  
  
### Matches(Object, InputEventArgs)  
  
```csharp  
public override bool Matches(object targetElement, InputEventArgs inputEventArgs);  
```  
  
**Parameters**  
  
`targetElement` [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object)  
  
`inputEventArgs` [InputEventArgs](https://docs.microsoft.com/en-us/dotnet/api/System.Windows.Input.InputEventArgs)  
  
**Returns**  
  
[Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean)  
  

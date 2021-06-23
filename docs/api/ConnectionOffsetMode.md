# ConnectionOffsetMode Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [ConnectionOffsetMode](ConnectionOffsetMode)  
  
**References:** [BaseConnection](BaseConnection)  
  
Specifies the offset type that can be applied to a [BaseConnection](BaseConnection) using the [BaseConnection.SourceOffset](BaseConnection#sourceoffset) and the [BaseConnection.TargetOffset](BaseConnection#targetoffset) values.  
  
```csharp  
public enum ConnectionOffsetMode  
```  
## Fields  
  
### Circle  
  
The offset is applied in a circle around the point.  
  
```csharp  
public const ConnectionOffsetMode Circle = 1;  
```  
**Field Value**  
  
[ConnectionOffsetMode](ConnectionOffsetMode)  
  
### Edge  
  
The offset is applied in a rectangle shape around the point, perpendicular to the edges.  
  
```csharp  
public const ConnectionOffsetMode Edge = 3;  
```  
**Field Value**  
  
[ConnectionOffsetMode](ConnectionOffsetMode)  
  
### None  
  
No offset applied.  
  
```csharp  
public const ConnectionOffsetMode None = 0;  
```  
**Field Value**  
  
[ConnectionOffsetMode](ConnectionOffsetMode)  
  
### Rectangle  
  
The offset is applied in a rectangle shape around the point.  
  
```csharp  
public const ConnectionOffsetMode Rectangle = 2;  
```  
**Field Value**  
  
[ConnectionOffsetMode](ConnectionOffsetMode)  
  

# ConnectionOffsetMode Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [BaseConnection](Nodify_BaseConnection)  
  
Specifies the offset type that can be applied to a [BaseConnection](Nodify_BaseConnection) using the [BaseConnection.SourceOffset](Nodify_BaseConnection#sourceoffset) and the [BaseConnection.TargetOffset](Nodify_BaseConnection#targetoffset) values.  
  
```csharp  
public enum ConnectionOffsetMode  
```  
  
## Fields  
  
### Circle  
  
The offset is applied in a circle around the point.  
  
```csharp  
Circle = 1;  
```  
  
### Edge  
  
The offset is applied in a rectangle shape around the point, perpendicular to the edges.  
  
```csharp  
Edge = 3;  
```  
  
### None  
  
No offset applied.  
  
```csharp  
None = 0;  
```  
  
### Rectangle  
  
The offset is applied in a rectangle shape around the point.  
  
```csharp  
Rectangle = 2;  
```  
  
### Static  
  
The offset is applied as a fixed margin.  
  
```csharp  
Static = 4;  
```  
  

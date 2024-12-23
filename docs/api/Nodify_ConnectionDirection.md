# ConnectionDirection Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**References:** [BaseConnection](Nodify_BaseConnection), [LineConnection](Nodify_LineConnection), [PendingConnection](Nodify_PendingConnection)  
  
The direction in which a connection is oriented.  
  
```csharp  
public enum ConnectionDirection  
```  
  
## Fields  
  
### Backward  
  
From [BaseConnection.Target](Nodify_BaseConnection#target) to [BaseConnection.Source](Nodify_BaseConnection#source).  
  
```csharp  
Backward = 1;  
```  
  
### Forward  
  
From [BaseConnection.Source](Nodify_BaseConnection#source) to [BaseConnection.Target](Nodify_BaseConnection#target).  
  
```csharp  
Forward = 0;  
```  
  

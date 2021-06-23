# ConnectionDirection Enum  
  
**Namespace:** Nodify  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/System.ValueType) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/System.Enum) → [ConnectionDirection](ConnectionDirection)  
  
**References:** [BaseConnection](BaseConnection), [PendingConnection](PendingConnection)  
  
The direction in which a connection is oriented.  
  
```csharp  
public enum ConnectionDirection  
```  
## Fields  
  
### Backward  
  
From [BaseConnection.Target](BaseConnection#target) to [BaseConnection.Source](BaseConnection#source).  
  
```csharp  
public const ConnectionDirection Backward = 1;  
```  
**Field Value**  
  
[ConnectionDirection](ConnectionDirection)  
  
### Forward  
  
From [BaseConnection.Source](BaseConnection#source) to [BaseConnection.Target](BaseConnection#target).  
  
```csharp  
public const ConnectionDirection Forward = 0;  
```  
**Field Value**  
  
[ConnectionDirection](ConnectionDirection)  
  

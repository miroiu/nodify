# EditorGestures Class  
  
**Namespace:** Nodify.Interactivity  
  
**Assembly:** Nodify  
  
**Inheritance:** [Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object) → [EditorGestures](Nodify_Interactivity_EditorGestures)  
  
**References:** [EditorGestures.ConnectionGestures](Nodify_Interactivity_EditorGestures_ConnectionGestures), [EditorGestures.ConnectorGestures](Nodify_Interactivity_EditorGestures_ConnectorGestures), [EditorGestures.GroupingNodeGestures](Nodify_Interactivity_EditorGestures_GroupingNodeGestures), [EditorGestures.ItemContainerGestures](Nodify_Interactivity_EditorGestures_ItemContainerGestures), [EditorGestures.MinimapGestures](Nodify_Interactivity_EditorGestures_MinimapGestures), [NodifyEditor](Nodify_NodifyEditor), [EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures)  
  
Gestures used by built-in controls inside the [NodifyEditor](Nodify_NodifyEditor).  
  
```csharp  
public class EditorGestures  
```  
  
## Constructors  
  
### EditorGestures()  
  
```csharp  
public EditorGestures();  
```  
  
## Fields  
  
### Mappings  
  
```csharp  
public static EditorGestures Mappings;  
```  
  
**Field Value**  
  
[EditorGestures](Nodify_Interactivity_EditorGestures)  
  
## Properties  
  
### Connection  
  
Gestures for the connection.  
  
```csharp  
public ConnectionGestures Connection { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.ConnectionGestures](Nodify_Interactivity_EditorGestures_ConnectionGestures)  
  
### Connector  
  
Gestures for the connector.  
  
```csharp  
public ConnectorGestures Connector { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.ConnectorGestures](Nodify_Interactivity_EditorGestures_ConnectorGestures)  
  
### Editor  
  
Gestures for the editor.  
  
```csharp  
public NodifyEditorGestures Editor { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.NodifyEditorGestures](Nodify_Interactivity_EditorGestures_NodifyEditorGestures)  
  
### GroupingNode  
  
Gestures for the grouping node.  
  
```csharp  
public GroupingNodeGestures GroupingNode { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.GroupingNodeGestures](Nodify_Interactivity_EditorGestures_GroupingNodeGestures)  
  
### ItemContainer  
  
Gestures for the item container.  
  
```csharp  
public ItemContainerGestures ItemContainer { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.ItemContainerGestures](Nodify_Interactivity_EditorGestures_ItemContainerGestures)  
  
### Minimap  
  
Gestures for the minimap.  
  
```csharp  
public MinimapGestures Minimap { get; set; }  
```  
  
**Property Value**  
  
[EditorGestures.MinimapGestures](Nodify_Interactivity_EditorGestures_MinimapGestures)  
  
## Methods  
  
### Apply(EditorGestures)  
  
Copies from the specified gestures.  
  
```csharp  
public void Apply(EditorGestures gestures);  
```  
  
**Parameters**  
  
`gestures` [EditorGestures](Nodify_Interactivity_EditorGestures): The gestures to copy.  
  
### Unbind()  
  
Unbinds all the gestures used by the editor and its controls.  
  
```csharp  
public void Unbind();  
```  
  

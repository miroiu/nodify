Nodes are the building blocks of a node editor. They are wrapped in [`ItemContainer`s](ItemContainer-Overview) and can be any custom control. (e.g. a TextBlock)

The following nodes are part of the library:

### 1. The ```Node``` control
This type of node supports both ```Input``` and ```Output``` connectors and can be moved around.

```xml
<nodify:NodifyEditor xmlns:sys="clr-namespace:System;assembly=mscorlib">
  <nodify:NodifyEditor.ItemsSource>
    <CompositeCollection>
        <nodify:Node Header="My Node">
            <nodify:Node.Input>
                <CompositeCollection>
                    <sys:String>In 0</sys:String>
                    <sys:String>In 1</sys:String>
                </CompositeCollection>
            </nodify:Node.Input>
            <nodify:Node.Output>
                <CompositeCollection>
                    <sys:String>Out 0</sys:String>
                    <sys:String>Out 1</sys:String>
                </CompositeCollection>
            </nodify:Node.Output>
            <nodify:Node.InputConnectorTemplate>
                <DataTemplate>
                    <nodify:NodeInput Header="{Binding}" />
                </DataTemplate>
            </nodify:Node.InputConnectorTemplate>
            <nodify:Node.OutputConnectorTemplate>
                <DataTemplate>
                    <nodify:NodeOutput Header="{Binding}" />
                </DataTemplate>
            </nodify:Node.OutputConnectorTemplate>
        </nodify:Node>
    </CompositeCollection>
  </nodify:NodifyEditor.ItemsSource>
</nodify:NodifyEditor>
```

The `Header` of the node can be customized using the `HeaderTemplate`. Respectively, the `Footer` of the node can be customized using the `FooterTemplate`.

Each item in the `Input` collection can be customized using the `InputConnectorTemplate`. Respectively the `Output` can be customized using the `OutputConnectorTemplate`.

![Node](https://i.imgur.com/VwAYlX3.gif)

### 2. The ```GroupingNode``` control

This type of node can be resized and will move nodes that are inside it if dragged by the ```Header```.

If the ```SwitchMovementModeModifierKey``` (**Shift** key by default) is held, it will move without moving its children.

```xml
<nodify:NodifyEditor>
    <nodify:NodifyEditor.ItemsSource>
        <CompositeCollection>
            <nodify:GroupingNode Header="Grouping node"
                            Width="300"
                            Height="250" />            
            <nodify:Node Header="My node" />
            <nodify:Node Header="My other node" />
        </CompositeCollection>
    </nodify:NodifyEditor.ItemsSource>
</nodify:NodifyEditor>
```

The `Header` of the node can be customized using the `HeaderTemplate`. Respectively the `Content` of the node can be customized using the `ContentTemplate`.

The size of the node can be set programmatically by changing the `ActualSize` dependency property value. 

#### Default values

* CanResize: true
* MovementMode: Grouped

#### Commands

* ResizeCompleted
* ResizeStarted

![Grouping Node](https://i.imgur.com/HYxt2cs.gif)

### 3. The ```KnotNode``` control

This type of control can be used to reroute ```Connection```s as it supports only one ```Connector```. 

The `Content` of the node can be customized using the `ContentTemplate`. 

```xml
<nodify:NodifyEditor>
    <nodify:NodifyEditor.ItemsSource>
        <CompositeCollection>
            <nodify:KnotNode />
        </CompositeCollection>
    </nodify:NodifyEditor.ItemsSource>
</nodify:NodifyEditor>
```

![Knot Node](https://i.imgur.com/fMrEqY1.gif)

### 4. The ```StateNode``` control

This type of node is a ```Connector``` itself, meaning that it will raise ```PendingConnection``` events on interaction. Because it inherits from `Connector`, you need to bind the `Anchor` property and `IsConnected` to the corresponding state. (if IsConnected is set to false, the connections won't update)

```xml
<nodify:NodifyEditor>
    <nodify:NodifyEditor.ItemsSource>
        <CompositeCollection>
            <nodify:StateNode Content="My node" />
        </CompositeCollection>
    </nodify:NodifyEditor.ItemsSource>
</nodify:NodifyEditor>
```

The `Content` of the node can be customized using the `ContentTemplate`.

![State Node](https://i.imgur.com/FrI2epL.gif)
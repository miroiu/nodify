
# Nodify
[![NuGet](https://img.shields.io/nuget/dt/Nodify?label=nuget&style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/Nodify)
[![Build](https://img.shields.io/github/workflow/status/miroiu/nodify/Build?style=for-the-badge&logo=.net)](https://github.com/miroiu/nodify/actions)

## The core controls for a node based editor (designed for MVVM)
![Example](https://i.imgur.com/xSPN1s2.png)

### Minimal XAML:
```xml
<nodify:NodifyEditor ItemsSource="{Binding Nodes}"
                     Connections="{Binding Connections}"
                     ConnectionCompletedCommand="{Binding ConnectionCompletedCommand}">
    <nodify:NodifyEditor.ItemTemplate>
        <DataTemplate>
            <nodify:Node Header="{Binding Title}"
                         Input="{Binding Input}"
                         Output="{Binding Output}">
                <nodify:Node.InputConnectorTemplate>
                    <DataTemplate>
                        <nodify:NodeInput Header="{Binding Title}"
                                          Anchor="{Binding Anchor, Mode=OneWayToSource}"
                                          IsConnected="{Binding IsConnected}" />
                    </DataTemplate>
                </nodify:Node.InputConnectorTemplate>
                <nodify:Node.OutputConnectorTemplate>
                    <DataTemplate>
                        <nodify:NodeOutput Header="{Binding Title}"
                                           Anchor="{Binding Anchor, Mode=OneWayToSource}"
                                           IsConnected="{Binding IsConnected}" />
                    </DataTemplate>
                </nodify:Node.OutputConnectorTemplate>
            </nodify:Node>
        </DataTemplate>
    </nodify:NodifyEditor.ItemTemplate>
    <nodify:NodifyEditor.ConnectionTemplate>
        <DataTemplate>
            <nodify:Connection Source="{Binding Input.Anchor}"
                               Target="{Binding Output.Anchor}" />
        </DataTemplate>
    </nodify:NodifyEditor.ConnectionTemplate>
    <nodify:NodifyEditor.ItemContainerStyle>
        <Style TargetType="{x:Type nodify:ItemContainer}">
            <Setter Property="Location"
                    Value="{Binding Location}" />
        </Style>
    </nodify:NodifyEditor.ItemContainerStyle>
</nodify:NodifyEditor>
```

<details>
  <summary>No data binding</summary>
  
***Note: Not much you can do with this at the moment***
```xml
<nodify:NodifyEditor>
    <nodify:Node Header="My node"
                 nodify:ItemContainer.LocationOverride="100 100" />
    <nodify:Node Header="My other node"
                 nodify:ItemContainer.LocationOverride="200 100" />
    <nodify:GroupingNode Header="Grouping node"
                         Width="300"
                         Height="150"
                         nodify:ItemContainer.LocationOverride="50 50" />
    <nodify:KnotNode nodify:ItemContainer.LocationOverride="400 100" />
</nodify:NodifyEditor>
```
</details>

## Features
 
 - Built for **MVVM**
 - Single assembly targeting **.NET Core 3.1**
 - **No** external libraries **dependency**
 - **High performance** controls **interactions**
 - A playground [**application example**](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground)
 - Customizable through **styles** and **templates**
 - Lots of **configurable** dependency properties
 - **Pan**, **zoom**, **select** area, **auto pan** when close to edge
 - **Select** and **move** items
 - Ready for Undo/Redo
 - Expanding Controls library (Node, GroupingNode, Connection etc.)
 
## Want to say thanks?
Hit the ⭐ Star ⭐ button
 
## Documentation
https://github.com/miroiu/nodify/wiki

https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground

![Playground](https://i.imgur.com/aqrUpuP.gif)

https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine

![StateMachine](https://i.imgur.com/nVKV5ly.gif)

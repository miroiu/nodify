
# Nodify
[![NuGet](https://img.shields.io/nuget/dt/Nodify?label=nuget&style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/Nodify)
[![Build](https://img.shields.io/github/workflow/status/miroiu/nodify/Build?style=for-the-badge&logo=.net)](https://github.com/miroiu/nodify/actions)

## The core controls for a node based editor (designed for MVVM)
![Example](https://i.imgur.com/xSPN1s2.png)

## Installation
Use the nuget package manager to install Nodify.

```
Install-Package Nodify
```

## Minimal XAML:
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

## Features
 
 - Built with **MVVM** in mind
 - Single assembly targeting **.NET Core 3.1**
 - **No dependencies** other than WPF
 - **High performance** controls interactions
 - A playground [**application example**](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground)
 - A state machine [**application example**](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine)
 - **Customizable** through styles and templates
 - Dark and light **themes**
 - Lots of **configurable** dependency properties
 - **Pan**, **zoom**, **select** area, **auto pan** when close to edge
 - **Select**, **move** and **connect** items
 - Ready for undo/redo
 - Expanding Controls library (Node, GroupingNode, Connection etc.)
 
 
## Documentation
https://github.com/miroiu/nodify/wiki

https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground

![Playground](https://i.imgur.com/aqrUpuP.gif)

https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine

![StateMachine](https://i.imgur.com/nVKV5ly.gif)


## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)

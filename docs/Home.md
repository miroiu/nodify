![editor-interaction](https://user-images.githubusercontent.com/12727904/192004838-ec6dd997-5e64-4c01-940c-1cd1b8d27837.gif)

# Welcome to Nodify!

Nodify is a WPF node-based [editor control](Editor-Overview) featuring a collection of [nodes](Nodes-Overview), [connections](Connections-Overview), and [connectors](Connectors-Overview) components aiming to simplify the process of building node-based tools.

It is inspired by Unreal Engine's [Blueprints Visual Scripting](https://docs.unrealengine.com/en-US/ProgrammingAndScripting/Blueprints/index.html) system but focuses only on the user interface and user interaction part. Unlike Blueprints, Nodify is a general-purpose library that offers a node graph editor component that can be embedded in any WPF application.

The graph editor is an infinite area where you can place and move nodes around, select and drag groups of nodes, connect and disconnect nodes or connectors, zoom in and out, and automatically move the screen when dragging a node or a wire near the edges and much more.

Nodify is feature-rich and optimized for interaction with hundreds of nodes at once, and... it is built from the ground up to work with MVVM.

### Requirements

![IDE](https://img.shields.io/static/v1?label=%20&message=VS%202019%20or%20greater&color=informational&style=for-the-badge&logo=visual-studio)
![C#](https://img.shields.io/static/v1?label=%20&message=8.0&color=239120&style=for-the-badge&logo=c-sharp)
![.NET](https://img.shields.io/static/v1?label=%20&message=Framework%204.7.2%20to%20NET%206&color=5C2D91&style=for-the-badge&logo=.net)

### Install Nodify from Nuget

[![Download Package](https://img.shields.io/nuget/v/Nodify?label=Download&logo=nuget&style=for-the-badge)](https://www.nuget.org/packages/Nodify/)

```xml
Install-Package Nodify
```

### Application examples

- [Playground](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground)
- [Shapes](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Shapes)
- [State machine](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.StateMachine)
- [Calculator](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator)

[![IDE](https://img.shields.io/static/v1?label=%20&message=GET%20STARTED&color=9cf&style=for-the-badge)](Getting-Started)

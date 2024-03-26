
# Nodify <img src="https://user-images.githubusercontent.com/12727904/195416464-cbe7e3be-a372-4a17-a4be-a868059b9d7e.png" width="120px" alt="Nodify" align="right">

[![NuGet](https://img.shields.io/nuget/v/Nodify?style=for-the-badge&logo=nuget&label=release)](https://www.nuget.org/packages/Nodify/)
[![NuGet](https://img.shields.io/nuget/dt/Nodify?label=downloads&style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/Nodify)
[![License](https://img.shields.io/github/license/miroiu/nodify?style=for-the-badge)](https://github.com/miroiu/nodify/blob/master/LICENSE)
[![C#](https://img.shields.io/static/v1?label=docs&message=WIP&color=orange&style=for-the-badge)](https://github.com/miroiu/nodify/wiki)

 A collection of highly performant controls for node based editors designed for MVVM.

> [!TIP]
> There is now a fantastic Avalonia port available! You can check it out [here](https://github.com/BAndysc/nodify-avalonia). Huge thanks to [BAndysc](https://github.com/BAndysc) who made this possible!

## 🚀 Examples of node-based applications

🎨 A playground application where you can try all the available settings.

> [Examples/Nodify.Playground](Examples/Nodify.Playground)

![Playground](https://i.imgur.com/aqrUpuP.gif)


🌓 A state machine where each state represents an executable action, and each transition represents a condition for the next action to execute.

> [Examples/Nodify.StateMachine](Examples/Nodify.StateMachine)

![StateMachine](https://i.imgur.com/nVKV5ly.gif)

💻 A simple "real-time" calculator where each node represents an operation that takes input and feeds its output into other nodes input.

> [Examples/Nodify.Calculator](Examples/Nodify.Calculator)

![Calculator](https://i.imgur.com/jonrZAq.gif)

## 📥 Installation
Use the nuget package manager to install Nodify.

```
Install-Package Nodify
```

## ⭐️ Features
 
 - Designed from the start to work with **MVVM**
 - **No dependencies** other than WPF
 - **Optimized** for interactions with hundreds of nodes at once
 - Built-in dark and light **themes**
 - **Selecting**, **zooming**, **panning** with **auto panning** when close to edge
 - **Select**, **move** and **connect** nodes
 - Lots of **configurable** dependency properties
 - Ready for undo/redo
 - Example applications: 🎨 [**Playground**](Examples/Nodify.Playground), 🌓 [**State machine**](Examples/Nodify.StateMachine), 💻 [**Calculator**](Examples/Nodify.Calculator)
 
## 📝 Documentation

Check out the [wiki](https://github.com/miroiu/nodify/wiki) and the [changelog](CHANGELOG.md) in github.

## ❤️ [Contributing](CONTRIBUTING.md)

Helping with documentation, bug reports, pull requests or anything else is very welcome. 

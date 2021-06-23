
# Nodify
[![NuGet](https://img.shields.io/nuget/v/Nodify?style=for-the-badge&logo=nuget&label=release)](https://www.nuget.org/packages/Nodify/)
[![NuGet](https://img.shields.io/nuget/dt/Nodify?label=downloads&style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/Nodify)
[![Build](https://img.shields.io/github/workflow/status/miroiu/nodify/Build?style=for-the-badge&logo=.net)](https://github.com/miroiu/nodify/actions)
[![License](https://img.shields.io/github/license/miroiu/nodify?style=for-the-badge)](https://github.com/miroiu/nodify/blob/master/LICENSE)
[![C#](https://img.shields.io/static/v1?label=docs&message=WIP&color=orange&style=for-the-badge)](https://github.com/miroiu/nodify/wiki)

 A collection of high performance controls for node based editors designed for MVVM.

## 🚀 Examples of different node-based applications

🌓 A state machine where each state represents an executable action, and each transition represents a condition for the next action to execute.

> [Examples/Nodify.StateMachine](Examples/Nodify.StateMachine)

![StateMachine](https://i.imgur.com/nVKV5ly.gif)

💻 A simple "real-time" calculator where each node represents an operation that takes input and feeds its output into other nodes input.

> [Examples/Nodify.Calculator](Examples/Nodify.Calculator)

![Calculator](https://i.imgur.com/jonrZAq.gif)

🎨 A playground application where you can try all the available settings.

> [Examples/Nodify.Playground](Examples/Nodify.Playground)

![Playground](https://i.imgur.com/aqrUpuP.gif)

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
 - **Area selection**, **zoom**, **pan** with **auto panning** when close to edge
 - **Select**, **move** and **connect** nodes
 - Lots of **configurable** dependency properties
 - Ready for undo/redo
 - Example applications: 🎨 [**Playground**](Examples/Nodify.Playground), 🌓 [**State machine**](Examples/Nodify.StateMachine), 💻 [**Calculator**](Examples/Nodify.Calculator)
 
## 📝 Documentation

API Reference can be found [here](https://miroiu.github.io/nodify/docs/api/Alignment).

Documentation is moving [here](https://miroiu.github.io/nodify). If you want to help, you can do so by creating a pull-request to the [docs branch](https://github.com/miroiu/nodify/tree/docs) (the easiest way is to click 'Edit this page' in the website).

[Wiki](https://github.com/miroiu/nodify/wiki) - **check it out while it's there**

## ❤️ [Contributing](CONTRIBUTING.md)

Helping with documentation, bug reports, pull requests or anything else is very welcome. 

## 🎉 Showcase

Consider showing off your project to the rest of the world by using [this link](https://miroiu.github.io/nodify).

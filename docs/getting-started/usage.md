---
title: Setting up the editor
sidebar_label: Editor Setup
---

Make sure you have [installed Nodify](/docs/getting-started/installation) into your WPF project.

## Creating the view

Add the following namespace to the xaml file where you want to create an editor:

```xml
xmlns:nodify="https://miroiu.github.io/nodify"
```

or

```xml
xmlns:nodify="clr-namespace:Nodify;assembly=Nodify"
```

Create an editor:

```xml
<nodify:NodifyEditor />
```

Now you should have something like this:

```xml
<Window x:Class="MyProject.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:nodify="https://miroiu.github.io/nodify"
        mc:Ignorable="d">

    <nodify:NodifyEditor />

</Window>
```

Start the application and left-click and drag the mouse to create a selection rectangle.

:::tip
Drag the selection rectangle near the edge of the editor area and the screen will automatically move in that direction.
:::

### Choosing a theme

:::caution
You don't need to do this if you want to use the default theme.
:::

Merge one of the following themes into your resource dictionary in App.xaml:

```xml title="Dark theme (enabled by default)"
<ResourceDictionary Source="pack://application:,,,/Nodify;component/Themes/Dark.xaml" />
```

```xml title="Light theme"
<ResourceDictionary Source="pack://application:,,,/Nodify;component/Themes/Light.xaml" />
```

```xml title="Nodify theme"
<ResourceDictionary Source="pack://application:,,,/Nodify;component/Themes/Nodify.xaml" />
```

Your code should be similar to this:

```xml
<Application x:Class="MyProject.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/Nodify;component/Themes/Dark.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### Displaying a grid

Drawing a simple grid is just a matter of creating a grid brush and applying the editor transform to it, then use the brush as the `Background` of the editor.

Because the grid we are drawing is made of lines and has holes in it, the `Background` of the editor will have some transparency, meaning that we'll see the background color of the control below. To solve this, wrap the editor in a `Grid` and set its `Background` or set the `Background` of the `Window`.

Use the `AppliedTransform` dependency property to have the grid follow the view.

```xml
<Window x:Class="MyProject.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:nodify="https://miroiu.github.io/nodify"
        mc:Ignorable="d">

    <Window.Resources>
        <DrawingBrush x:Key="GridDrawingBrush"
                      TileMode="Tile"
                      ViewportUnits="Absolute"
                      Viewport="0 0 15 15"
                      Transform="{Binding AppliedTransform, ElementName=Editor}">
            <DrawingBrush.Drawing>
                <GeometryDrawing Geometry="M0,0 L0,1 0.03,1 0.03,0.03 1,0.03 1,0 Z"
                                 Brush="#333337" />
            </DrawingBrush.Drawing>
        </DrawingBrush>
    </Window.Resources>

    <Grid Background="#1E1E1E">
        <nodify:NodifyEditor x:Name="Editor"
                             Background="{StaticResource GridDrawingBrush}" />
    </Grid>
</Window>
```

And if you're using a custom theme you can replace

```xml
<Grid Background="#1E1E1E">
```

with

```xml
<Grid Background="{StaticResource NodifyEditor.BackgroundBrush}">
```

:::tip
Right-click and drag the screen around to move the view and use the mouse wheel to zoom in and out.
:::

## Creating the viewmodels

I will keep it simple and use only the built-in components for now. We will go more in-depth in the next sections.

### Editor viewmodel

Let's define a collection of items that we want to display.

I will use a list of `string`s for now just to have something on the screen. Your node viewmodel can be of **any shape**, but the built-in nodes have some dependency properties that you could take advantage of by defining a more realistic viewmodel.

```csharp
public class EditorViewModel
{
    public IEnumerable<string> Items { get; } = new List<string>
    {
        "My first item",
        "My second item"
    };
}
```

Now create an instance of the viewmodel and bind the `Items` property:

```xml {2}
<Window.DataContext>
    <local:EditorViewModel />
</Window.DataContext>

<nodify:NodifyEditor ItemsSource="{Binding Items}" />
```

Start the application and you'll see two nodes overlapping in the top left corner. You can select and drag them around. As you can see, they have a border and a transparent background. This is because every node is wrapped in an [ItemContainer](/docs/components/item-container).

Now that you know how to define and display nodes, let's create something more useful.

### Node viewmodel

### Connection viewmodel

## Table of contents

- [Moving the viewport](#panning)
- [Zooming](#zooming)
- [Customization](#customization)

The `Minimap` control is a custom control designed to provide a synchronized and miniature view of items in a `NodifyEditor`. It inherits from `ItemsControl` and displays items through the `ItemsSource` property. Each item is wrapped in a `MinimapItem` container that requires the `Location`, `Width`, and `Height` properties to be set in the `ItemContainerStyle`.

> [!TIP]
> For real-time movement of nodes inside the minimap, it's required to set `NodifyEditor.EnableDraggingContainersOptimizations` to `false`.

The control also displays a viewport rectangle that represents the visible area in the editor and requires the `ViewportLocation` and `ViewportSize` properties to be set.

```xml
<nodify:Minimap
    ItemsSource="{Binding ItemsSource, ElementName=Editor}"
    ViewportLocation="{Binding ViewportLocation, ElementName=Editor}"
    ViewportSize="{Binding ViewportSize, ElementName=Editor}"
    Zoom="OnMinimapZoom">
    <nodify:Minimap.ItemContainerStyle>
        <Style TargetType="nodify:MinimapItem">
            <Setter Property="Location" Value="{Binding MyItemLocation}" />
        </Style>
    </nodify:Minimap.ItemContainerStyle>
</nodify:Minimap>
```

```csharp
private void OnMinimapZoom(object sender, ZoomEventArgs e)
{
    Editor.ZoomAtPosition(e.Zoom, e.Location);
}
```

> [!IMPORTANT]
> The `Width` and `Height` should be constrained by the parent container or set to constant values on the `Minimap` to prevent resizing to fit the content.

## Panning

Panning is done by holding click and dragging and can be disabled by setting the `IsReadOnly` property to `true`. The `ViewportLocation` is updated during panning, therefore it must be a two-way binding (binds two ways by default).

The panning gesture can be configured by setting `EditorGestures.Mappings.Minimap.DragViewport` to the desired gesture.

## Zooming

Zooming is done by scrolling the mouse wheel and can be disabled by setting the `IsReadOnly` property to `true` or by not handling the `Zoom` event.

The zooming modifier key can be configured by setting `EditorGestures.Mappings.Minimap.ZoomModifierKey` to the desired value.

## Customization

The `ViewportStyle` is used to customize the viewport rectangle.

```xml
<Style x:Key="MyViewportStyle" TargetType="Rectangle">
    <Setter Property="Fill" Value="Transparent"/>
    <Setter Property="Stroke" Value="White"/>
    <Setter Property="StrokeThickness" Value="3"/>
</Style>

<nodify:Minimap ViewportStyle="{StaticResource MyViewportStyle}" ... />
```

The `MaxViewportOffset` property is used to restrict how far the viewport can be moved away from the items when [panning](#panning).

The `ResizeToViewport` property changes the resizing behavior of the minimap.
If `true`, the minimap will resize to always display the viewport alongside the items.

![scale-with-viewport](https://github.com/user-attachments/assets/7a8887bf-f3f4-44d7-8311-6d9ba7869d78)

If `false`, the minimap will resize to only include the items, allowing the viewport to go out of bounds.

![viewport-out-of-bounds](https://github.com/user-attachments/assets/5d3b388e-8e40-4bfe-af3b-4c12fb47548d)

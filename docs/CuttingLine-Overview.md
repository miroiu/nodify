## Table of contents

- [Enabling preview](#enabling-preview)
- [Custom connections](#custom-connections)
- [Customization](#customization)

The `CuttingLine` control is a custom control used to remove intersecting connections.

The default gesture to start cutting is `SHIFT+ALT+LeftClick` and can be configured by setting the `EditorGestures.Editor.Cutting` gesture. The cutting can be canceled by pressing the `Escape` key or the right mouse button which can be configured by setting the `EditorGestures.Editor.CancelAction` gesture.

Relevant commands available in `NodifyEditor`:

- CuttingStartedCommand
- CuttingCompletedCommand
- RemoveConnectionCommand - is called for each intersecting connection when the cutting operation is completed

## Enabling preview

For the connection style to change when intersecting with the cutting line, it's required to set `NodifyEditor.EnableCuttingLinePreview` to `true`.

> [!WARNING]
> Depending on the number of connections and the complexity of their geometry, this may have a great performance impact.

![cutting](https://github.com/user-attachments/assets/22f705c8-3bf1-466b-8bbd-da007f30deb2)

## Custom connections

To enable cutting custom connections you must add the connection type to `NodifyEditor.CuttingConnectionTypes`:

```csharp
// example adding the Line shape to the connection types that can be cut
NodifyEditor.CuttingConnectionTypes.Add(typeof(System.Windows.Shapes.Line));
```

The style of the intersecting custom connection can be customized as follows:

```xml
<Style TargetType="Line">
    <Style.Triggers>
        <Trigger Property="nodify:CuttingLine.IsOverElement" Value="True">
            <Setter Property="Opacity"
                    Value="0.4" />
        </Trigger>
    </Style.Triggers>
</Style>
```

## Customization

The `CuttingLineStyle` is used to customize the cutting line:

```xml
<Style x:Key="CuttingLineStyle"
       TargetType="{x:Type nodify:CuttingLine}"
       BasedOn="{StaticResource {x:Type nodify:CuttingLine}}">
    <Setter Property="StrokeDashArray"
            Value="1 1" />
    <Setter Property="StrokeThickness"
            Value="2" />
</Style>

<nodify:NodifyEditor CuttingLineStyle="{StaticResource CuttingLineStyle}" ... />
```

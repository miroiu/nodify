# Creating Custom Connectors

Connectors are the interactive endpoints of nodes where users can click-and-drag to make connections. Nodify provides `Connector` as the base class, along with standard implementations: `NodeInput` (inputs with left alignment and a custom header) and `NodeOutput` (outputs with right alignment).

To customize how connectors look, how they behave, or how they are positioned, you can customize the templates of the built-in connectors or create custom style/control definitions.

---

## Minimal Example: Customizing Connector Templates

You can easily change the shape of the connector point (such as rendering a square or a triangle instead of the default circle) by setting the `ConnectorTemplate` property.

Here is a WPF `ControlTemplate` that renders a custom square connector:

```xml
<ControlTemplate x:Key="SquareConnectorTemplate" TargetType="Control">
    <Rectangle Width="12"
               Height="12"
               Stroke="{TemplateBinding BorderBrush}"
               Fill="{TemplateBinding Background}"
               StrokeThickness="2"
               RadiusX="2"
               RadiusY="2" />
</ControlTemplate>
```

You can then apply this template directly to your `NodeInput` or `NodeOutput`:

```xml
<nodify:NodeInput Header="Input 1"
                  ConnectorTemplate="{StaticResource SquareConnectorTemplate}"
                  Background="#FF2F2F2F"
                  BorderBrush="#FF107C41" />
```

---

## Customizing the Header Layout

`NodeInput` and `NodeOutput` inherit from `HeaderedContentControl`, which means they have `Header` and `HeaderTemplate` properties. You can customize the `HeaderTemplate` to contain any interactive or static controls (e.g., adding input boxes or drop-down menus directly on the connector).

```xml
<DataTemplate x:Key="InteractiveConnectorHeaderTemplate">
    <StackPanel Orientation="Horizontal" Margin="0,2">
        <TextBlock Text="{Binding Title}" VerticalAlignment="Center" Margin="0,0,6,0" />
        <TextBox Text="{Binding DefaultValue, UpdateSourceTrigger=PropertyChanged}" 
                 Width="40" 
                 Height="18" 
                 FontSize="10" 
                 Background="#333333" 
                 Foreground="White" 
                 BorderThickness="0" />
    </StackPanel>
</DataTemplate>
```

Use it in your editor:

```xml
<nodify:NodeInput Header="{Binding}" 
                  HeaderTemplate="{StaticResource InteractiveConnectorHeaderTemplate}" />
```

---

## Advanced: Designing Custom Connector Behavior & Style Triggers

In complex applications, you might want connectors to dynamically change their shape, color, or visibility depending on the type of data they carry (e.g., integers, booleans, trigger signals) or whether they are currently connected.

Using WPF `DataTrigger`s makes this simple:

```xml
<Style TargetType="{x:Type nodify:NodeInput}" BasedOn="{StaticResource {x:Type nodify:NodeInput}}">
    <!-- Default properties -->
    <Setter Property="Anchor" Value="{Binding Anchor, Mode=OneWayToSource}" />
    <Setter Property="IsConnected" Value="{Binding IsConnected}" />
    <Setter Property="Header" Value="{Binding}" />
    
    <Style.Triggers>
        <!-- If the data type of the connector is 'Trigger' -->
        <DataTrigger Binding="{Binding DataType}" Value="Trigger">
            <Setter Property="ConnectorTemplate">
                <Setter.Value>
                    <!-- Chevron / Arrowhead look -->
                    <ControlTemplate TargetType="Control">
                        <Polygon Points="0,0 8,6 0,12" 
                                 Fill="{TemplateBinding Background}" 
                                 Stroke="{TemplateBinding BorderBrush}" 
                                 StrokeThickness="1.5" />
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Setter Property="BorderBrush" Value="#FFA3A3A3" />
            <Setter Property="Background" Value="Transparent" />
        </DataTrigger>
        
        <!-- If the connector is connected, fill the inside -->
        <DataTrigger Binding="{Binding IsConnected}" Value="True">
            <Setter Property="Background" Value="{Binding BorderBrush, RelativeSource={RelativeSource TemplatedParent}}" />
        </DataTrigger>
    </Style.Triggers>
</Style>
```

---

## Performance & Interaction Notes

1. **The `Anchor` Point**: The `Anchor` property is critical. It defines the point from which connections start or end. If you are using a custom `ControlTemplate` for the connector, ensure the outer layout of your node doesn't prevent the `Connector` from computing its relative location. The `Anchor` coordinates are automatically updated by the library when `IsConnected` is `True`.
2. **`IsConnected` Setting**: Setting `IsConnected` to `True` enables updates for the connector's `Anchor` dependency property. Make sure your custom styles or bindings preserve this logic.
3. **Hit Testing / Focus**: Connectors are highly interactive. If you put clickable elements (like textboxes) inside the `HeaderTemplate` of a connector, users can focus and interact with them, but dragging starting on those sub-controls might be intercepted depending on routing. Keep interactive elements clean and clearly distinct from the connector point itself.

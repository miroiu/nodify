Connections are created between two points. The `Source` and `Target` dependency properties are of type `Point` and are usually bound to a connector's `Anchor` point.

## Table of contents

- [Base connection](#base-connection)
- [Line connection](#line-connection)
- [Circuit connection](#circuit-connection)
- [Bezier connection](#connection)
- [Step connection](#step-connection)
- [Pending connection](#pending-connection)
- [Custom connection](#custom-connection)

## Using connections

In the `NodifyEditor`, you can customize both the default connection and the pending connection by assigning a custom `DataTemplate` to the `ConnectionTemplate` and `PendingConnectionTemplate` properties, respectively.

- `ConnectionTemplate`: defines the appearance of established connections between nodes.
- `PendingConnectionTemplate`: specifies the visual appearance of connections that are in the process of being created (i.e., while the connection is being dragged but not yet completed).

To customize these, simply set your desired `DataTemplate` on the `NodifyEditor` instance.

## Base connection

The base class for all connections provided by the library is `BaseConnection` which derives from `Shape`. There's no restriction to derive from `BaseConnection` when you create a custom connection.

It exposes two commands with their corresponding events:

- `DisconnectCommand`, respectively `DisconnectEvent` - fired when the connection is clicked while holding `ALT`
- `SplitCommand`, respectively `SplitEvent` - fired when the connection is double-clicked

The `Direction` of a connection can have two values:

- `Forward`

![image](https://user-images.githubusercontent.com/12727904/192101918-af9b0da6-ecc8-48f7-bf4d-8f9fdd005153.png)
![image](https://user-images.githubusercontent.com/12727904/192101959-2cb9a837-1642-4e96-b2ef-eea5502a587f.png)

- `Backward`

![image](https://user-images.githubusercontent.com/12727904/192101941-a00e23db-07ae-49ac-a907-72e35ef67877.png)
![image](https://user-images.githubusercontent.com/12727904/192101977-1afd69f1-dab0-478e-9c3d-7d601486c289.png)

The `SourceOffset` and the `TargetOffset` work together with `OffsetMode` and will keep a distance from the anchor point:

![image](https://user-images.githubusercontent.com/12727904/192102096-b20887d5-b7ba-450f-9cf3-7fa4086d9637.png)

Connections also have a `Spacing` which will make the connection break the angle at a certain distance from the `Source` and `Target` points:

- With spacing:

![image](https://user-images.githubusercontent.com/12727904/192102286-9a79da8e-5e87-4f60-9e82-979bfabcd6f3.png)

- Without spacing:

![image](https://user-images.githubusercontent.com/12727904/192102302-4125b44a-dfad-4d9e-9131-efb7c17cefbe.png)

Settings the `ArrowSize` to "0, 0" will remove the arrowhead.

## Line connection

A straight line from `Source` to `Target`.

![image](https://user-images.githubusercontent.com/12727904/192115137-d8d2145b-a769-4ee9-b4e0-8a362c94e9e7.png)

## Circuit connection

It has an `Angle` dependency property to control where it breaks. Angle is in degrees.

![image](https://user-images.githubusercontent.com/12727904/192115226-b0e515b4-5a21-46aa-956a-401f07b7d308.png)

## Connection

A bezier curve between `Source` and `Target`.

![image](https://user-images.githubusercontent.com/12727904/192115259-2fe56a68-b3e4-4f5d-aa5c-5ab83e84a84d.png)

## Step connection

A multi-segment angled wire between `Source` and `Target`. It has two additional parameters: `SourcePosition` and `TargetPosition`.

![image](https://github.com/user-attachments/assets/c63d620e-af34-460e-ad9e-b2d9adb748bf)

## Pending Connection

A pending connection can be created from a connector and dropped on either an `ItemContainer` (if `AllowOnlyConnectors` is false) or a `Connector`.

`Content` of a pending connection can be customized using the `ContentTemplate`. If `EnablePreview` is true, the `PreviewTarget` will be updated with the connector or item container under the mouse cursor or `null` if there's no such element.

![image](https://user-images.githubusercontent.com/12727904/192115698-fbe29101-884f-4cec-9c25-e318701d30b1.png)

The visibility of pending connections can be controlled using the `IsVisible` dependency property.

Connection snapping to connectors can be enabled using the `EnableSnapping` dependency property.

The `Source` and the `Target` properties are data contexts of connectors and the `Target` will be updated when the pending connection is completed.

There's also a `StartedCommand` which takes the `Source` as the parameter, respectively a `CompletedCommand` which takes the `Target` as the parameter.

> [!TIP]
> Canceling a pending connection is done by releasing the right mouse button.

## Custom connection

In some cases, the built-in connections may not provide all the features you need, or your application may need to display hundreds of connections simultaneously. While the built-in connections are feature-rich, they may introduce some overhead that could impact performance. In such cases, you can implement a custom connection to better suit your needs.

There are several ways to implement a custom connection:

- Derive from one of the existing connection classes and override the appropriate methods.
- Create a custom control or user control that wraps a built-in connection.
- Implement a custom connection from scratch.

**Considerations for Implementing a Custom Connection from Scratch**

- The cutting line feature requires the connection type to be added to the `NodifyEditor.CuttingConnectionTypes` collection (see [Cuttline Line - Custom Connections](CuttingLine-Overview#custom-connections))
- For selection functionality, you must set the `nodify:BaseConnection.IsSelectable` attached property to `true` on the root element.
- To control the selection state, bind to the `nodify:BaseConnection.IsSelected` attached property on the root element.

Example:

```xml
<Line X1="{Binding Source.Anchor.X}"
      X2="{Binding Target.Anchor.X}"
      Y1="{Binding Source.Anchor.Y}"
      Y2="{Binding Target.Anchor.Y}"
      StrokeThickness="5"
      nodify:BaseConnection.IsSelectable="True"
      nodify:BaseConnection.IsSelected="{Binding IsSelected}">
    <Line.Style>
        <Style TargetType="Line">
            <Setter Property="Stroke"
                    Value="Red" />
            <Style.Triggers>
                <Trigger Property="nodify:BaseConnection.IsSelected"
                         Value="True">
                    <Setter Property="Stroke"
                            Value="Green" />
                </Trigger>
                <Trigger Property="nodify:BaseConnection.IsSelectable"
                         Value="True">
                    <Setter Property="Cursor"
                            Value="Hand" />
                </Trigger>
            </Style.Triggers>
        </Style>
    </Line.Style>
</Line>
```

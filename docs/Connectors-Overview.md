The connector creates a pending connection by raising the `PendingConnectionStartedEvent` event. Connectors have an `Anchor` dependency property that **must** be bound in order to be able to create connections between them that are updated in real-time when the node's position changes. The `IsConnected` dependency property **must** be set to true in order to receive `Anchor` updates.

ALT+Click on a connector fires the `DisconnectCommand`.

## NodeInput and NodeOutput

Node input and node output are implementations of `Connector` with a `Header` that can be used to display text or input boxes by customizing the `HeaderTemplate`. They also expose a `ConnectorTemplate` to customize the connector itself. They are usually used in conjunction with `Node.InputConnectorTemplate` and `Node.OutputConnectorTemplate`.

![image](https://user-images.githubusercontent.com/12727904/192117525-a7e1b309-70d6-4ed7-bcd7-8210dbd680ce.png)


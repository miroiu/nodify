Nodify provides several built-in [node controls](Nodes-Overview), but most applications need nodes tailored to their domain. This guide covers how to customize existing nodes and create entirely new ones.

## Table of contents

- [Approaches](#approaches)
- [Customizing the built-in Node](#customizing-the-built-in-node)
  - [Overriding templates](#overriding-templates)
  - [Overriding the ControlTemplate](#overriding-the-controltemplate)
- [Using DataTemplates for different node types](#using-datatemplates-for-different-node-types)
- [Adding typed connectors](#adding-typed-connectors)
- [Responding to connection events](#responding-to-connection-events)
- [Worked example: math nodes](#worked-example-math-nodes)
  - [The operation interface](#the-operation-interface)
  - [Node ViewModels](#node-viewmodels)
  - [Connector ViewModel](#connector-viewmodel)
  - [Connection ViewModel](#connection-viewmodel)
  - [The editor ViewModel](#the-editor-viewmodel)
  - [The view](#the-view)

## Approaches

There are several ways to create custom nodes, depending on how much control you need:

1. **Template customization** — Override the `InputConnectorTemplate`, `OutputConnectorTemplate`, `HeaderTemplate`, `FooterTemplate`, or `ContentTemplate` on the built-in `Node` control. Best when you want to change what's _inside_ a node without changing its structure.

2. **ControlTemplate override** — Replace the entire `ControlTemplate` of the `Node` control via a custom `Style`. You must preserve the `PART_Input` and `PART_Output` named parts for the connectors to work.

3. **DataTemplate per ViewModel** — Define different `DataTemplate`s inside `NodifyEditor.Resources` for different ViewModel types. The editor will automatically pick the right template based on the data type. This is the most common approach for applications with multiple node types.

4. **Custom control** — Create an entirely new control (e.g. deriving from `ContentControl` or `HeaderedContentControl`). Place `Connector`-derived controls inside it if it needs to create connections.

5. **Derive from Connector** — For nodes that _are_ connectors (like [StateNode](Nodes-Overview#4-the-statenode-control)), derive from `Connector` directly so the entire node surface can initiate pending connections.

Most applications use a combination of approaches 1 and 3: a shared `Node` control with customized templates, and `DataTemplate`s to render different ViewModel types.

## Customizing the built-in Node

The `Node` control has a three-row layout: **Header**, **Body** (with input connectors, content area, and output connectors), and **Footer**. Each section can be customized independently.

### Overriding templates

The simplest way to customize a node is to override its templates. Here's a node with a custom header that shows an icon and title, editable input connectors, and a footer with a status indicator:

```xml
<DataTemplate DataType="{x:Type local:MyNodeViewModel}">
    <nodify:Node Input="{Binding Input}"
                 Output="{Binding Output}">
        <nodify:Node.HeaderTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal">
                    <Ellipse Width="8" Height="8"
                             Fill="{Binding StatusColor}"
                             Margin="0 0 6 0" />
                    <TextBlock Text="{Binding Title}"
                               FontWeight="SemiBold" />
                </StackPanel>
            </DataTemplate>
        </nodify:Node.HeaderTemplate>

        <nodify:Node.InputConnectorTemplate>
            <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                <nodify:NodeInput Header="{Binding Title}"
                                  IsConnected="{Binding IsConnected}"
                                  Anchor="{Binding Anchor, Mode=OneWayToSource}" />
            </DataTemplate>
        </nodify:Node.InputConnectorTemplate>

        <nodify:Node.OutputConnectorTemplate>
            <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                <nodify:NodeOutput Header="{Binding Title}"
                                   IsConnected="{Binding IsConnected}"
                                   Anchor="{Binding Anchor, Mode=OneWayToSource}" />
            </DataTemplate>
        </nodify:Node.OutputConnectorTemplate>

        <nodify:Node.Footer>
            <TextBlock Text="{Binding Status}"
                       FontSize="10"
                       Opacity="0.7" />
        </nodify:Node.Footer>
    </nodify:Node>
</DataTemplate>
```

The `InputConnectorTemplate` and `OutputConnectorTemplate` control how each item in the `Input` and `Output` collections is rendered. You can use the built-in `NodeInput` and `NodeOutput` controls, or any custom control that contains a `Connector`.

> **Important:** Always bind `IsConnected` and `Anchor` (with `Mode=OneWayToSource`) on your connectors. `IsConnected` **must** be set to `true` for the connector to receive `Anchor` updates. See the [Connectors overview](Connectors-Overview) for details.

### Overriding the ControlTemplate

For more structural changes — such as placing connectors on the top and bottom instead of left and right — you can replace the `Node`'s `ControlTemplate` entirely.

The default template uses a three-row `Grid`:

| Row | Content |
| --- | --- |
| 0 (Auto) | Header |
| 1 (Star) | Body: `PART_Input` (left) \| Content \| `PART_Output` (right) |
| 2 (Auto) | Footer |

Here's an example that places all connectors at the bottom:

```xml
<Style x:Key="BottomConnectorsNodeStyle"
       TargetType="{x:Type nodify:Node}"
       BasedOn="{StaticResource {x:Type nodify:Node}}">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type nodify:Node}">
                <Border Background="{TemplateBinding Background}"
                        BorderBrush="{TemplateBinding BorderBrush}"
                        BorderThickness="{TemplateBinding BorderThickness}"
                        CornerRadius="3"
                        Padding="{TemplateBinding Padding}">
                    <Grid>
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto" />
                            <RowDefinition Height="*" />
                            <RowDefinition Height="Auto" />
                        </Grid.RowDefinitions>

                        <!-- Header -->
                        <Border Background="{TemplateBinding HeaderBrush}"
                                CornerRadius="3 3 0 0"
                                Padding="6 4">
                            <ContentPresenter ContentSource="Header" />
                        </Border>

                        <!-- Content -->
                        <ContentPresenter Grid.Row="1"
                                          Margin="{TemplateBinding ContentPadding}" />

                        <!-- All connectors at the bottom -->
                        <StackPanel Grid.Row="2" Orientation="Horizontal">
                            <ItemsControl x:Name="PART_Input"
                                          ItemsSource="{TemplateBinding Input}"
                                          ItemTemplate="{TemplateBinding InputConnectorTemplate}" />
                            <ItemsControl x:Name="PART_Output"
                                          ItemsSource="{TemplateBinding Output}"
                                          ItemTemplate="{TemplateBinding OutputConnectorTemplate}" />
                        </StackPanel>
                    </Grid>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

> **Important:** You **must** keep the template part names `PART_Input` and `PART_Output` for the `Node` control to find the `ItemsControl`s that host the connectors.

## Using DataTemplates for different node types

When your application has multiple node types, define a `DataTemplate` for each ViewModel type inside `NodifyEditor.Resources`. The editor will automatically select the correct template based on the item's type.

```xml
<nodify:NodifyEditor ItemsSource="{Binding Nodes}"
                     Connections="{Binding Connections}">
    <nodify:NodifyEditor.Resources>
        <!-- Default node -->
        <DataTemplate DataType="{x:Type local:StandardNodeViewModel}">
            <nodify:Node Header="{Binding Title}"
                         Input="{Binding Input}"
                         Output="{Binding Output}" />
        </DataTemplate>

        <!-- Comment node (no connectors) -->
        <DataTemplate DataType="{x:Type local:CommentNodeViewModel}">
            <Border Background="#3A3A3C"
                    CornerRadius="4"
                    Padding="12">
                <TextBlock Text="{Binding Text}"
                           TextWrapping="Wrap"
                           Foreground="#CCCCCC"
                           MaxWidth="200" />
            </Border>
        </DataTemplate>

        <!-- Group node -->
        <DataTemplate DataType="{x:Type local:GroupNodeViewModel}">
            <nodify:GroupingNode Header="{Binding Title}"
                                ActualSize="{Binding Size, Mode=TwoWay}" />
        </DataTemplate>
    </nodify:NodifyEditor.Resources>
</nodify:NodifyEditor>
```

Each item in `ItemsSource` is wrapped in an [ItemContainer](ItemContainer-Overview), so every node type — even a plain `Border` or `TextBlock` — automatically gets selection, dragging, and location support.

This is how the [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator) works. It defines `DataTemplate`s for `OperationViewModel`, `ExpandoOperationViewModel`, `ExpressionOperationViewModel`, `CalculatorOperationViewModel`, and `OperationGroupViewModel`, each rendering a different node layout.

## Adding typed connectors

A common requirement is connectors with different data types — for example, a `string` input shown as a text box and a `number` input shown as a slider. The approach is to use a `DataTemplateSelector` or multiple `DataTemplate`s keyed by connector type.

First, add a type discriminator to the connector ViewModel:

```csharp
public enum ConnectorType
{
    Default,
    Text,
    Number,
    Boolean
}

public class TypedConnectorViewModel : INotifyPropertyChanged
{
    public string Title { get; set; }
    public ConnectorType Type { get; set; }

    private object _value;
    public object Value
    {
        get => _value;
        set { _value = value; OnPropertyChanged(); }
    }

    private bool _isConnected;
    public bool IsConnected
    {
        get => _isConnected;
        set { _isConnected = value; OnPropertyChanged(); }
    }

    private Point _anchor;
    public Point Anchor
    {
        get => _anchor;
        set { _anchor = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

Then create a `DataTemplateSelector` that chooses the template based on the connector type:

```csharp
public class ConnectorTemplateSelector : DataTemplateSelector
{
    public DataTemplate DefaultTemplate { get; set; }
    public DataTemplate TextTemplate { get; set; }
    public DataTemplate NumberTemplate { get; set; }
    public DataTemplate BooleanTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is TypedConnectorViewModel connector)
        {
            return connector.Type switch
            {
                ConnectorType.Text => TextTemplate,
                ConnectorType.Number => NumberTemplate,
                ConnectorType.Boolean => BooleanTemplate,
                _ => DefaultTemplate,
            };
        }

        return DefaultTemplate;
    }
}
```

And use it in the XAML:

```xml
<local:ConnectorTemplateSelector x:Key="InputConnectorSelector">
    <local:ConnectorTemplateSelector.DefaultTemplate>
        <DataTemplate DataType="{x:Type local:TypedConnectorViewModel}">
            <nodify:NodeInput Header="{Binding Title}"
                              IsConnected="{Binding IsConnected}"
                              Anchor="{Binding Anchor, Mode=OneWayToSource}" />
        </DataTemplate>
    </local:ConnectorTemplateSelector.DefaultTemplate>

    <local:ConnectorTemplateSelector.TextTemplate>
        <DataTemplate DataType="{x:Type local:TypedConnectorViewModel}">
            <nodify:NodeInput IsConnected="{Binding IsConnected}"
                              Anchor="{Binding Anchor, Mode=OneWayToSource}">
                <nodify:NodeInput.HeaderTemplate>
                    <DataTemplate>
                        <StackPanel Orientation="Horizontal">
                            <TextBlock Text="{Binding Title}" Margin="0 0 4 0" />
                            <TextBox Text="{Binding Value}"
                                     MinWidth="60"
                                     Visibility="{Binding IsConnected,
                                         Converter={StaticResource InverseBoolToVisibility}}" />
                        </StackPanel>
                    </DataTemplate>
                </nodify:NodeInput.HeaderTemplate>
            </nodify:NodeInput>
        </DataTemplate>
    </local:ConnectorTemplateSelector.TextTemplate>

    <local:ConnectorTemplateSelector.NumberTemplate>
        <DataTemplate DataType="{x:Type local:TypedConnectorViewModel}">
            <nodify:NodeInput IsConnected="{Binding IsConnected}"
                              Anchor="{Binding Anchor, Mode=OneWayToSource}">
                <nodify:NodeInput.HeaderTemplate>
                    <DataTemplate>
                        <StackPanel Orientation="Horizontal">
                            <TextBlock Text="{Binding Title}" Margin="0 0 4 0" />
                            <Slider Value="{Binding Value}"
                                    Minimum="0" Maximum="100" Width="80"
                                    Visibility="{Binding IsConnected,
                                        Converter={StaticResource InverseBoolToVisibility}}" />
                        </StackPanel>
                    </DataTemplate>
                </nodify:NodeInput.HeaderTemplate>
            </nodify:NodeInput>
        </DataTemplate>
    </local:ConnectorTemplateSelector.NumberTemplate>
</local:ConnectorTemplateSelector>

<nodify:Node Header="{Binding Title}"
             Input="{Binding Input}"
             Output="{Binding Output}"
             InputConnectorTemplate="{x:Null}">
    <nodify:Node.Resources>
        <Style TargetType="{x:Type ItemsControl}">
            <Setter Property="ItemTemplateSelector"
                    Value="{StaticResource InputConnectorSelector}" />
        </Style>
    </nodify:Node.Resources>
</nodify:Node>
```

> The [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator) uses a similar technique: input connectors show a `TextBox` when not connected (so users can type a value directly) and hide it when a connection supplies the value.

## Responding to connection events

Connectors raise several events during the connection lifecycle. You can handle these to validate connections, update state, or trigger side effects.

### Connection started

When a user begins dragging from a connector, the `PendingConnectionStartedEvent` is raised. Use the `StartedCommand` on `PendingConnection` to capture the source connector:

```csharp
public class PendingConnectionViewModel
{
    private ConnectorViewModel _source;

    public ICommand StartCommand { get; }
    public ICommand FinishCommand { get; }

    public PendingConnectionViewModel(EditorViewModel editor)
    {
        StartCommand = new DelegateCommand<ConnectorViewModel>(source =>
        {
            _source = source;
        });

        FinishCommand = new DelegateCommand<ConnectorViewModel>(target =>
        {
            if (target != null && _source != target && CanConnect(_source, target))
            {
                editor.Connect(_source, target);
            }
        });
    }

    private bool CanConnect(ConnectorViewModel source, ConnectorViewModel target)
    {
        // Prevent connecting a connector to itself or to the same node
        return source.Node != target.Node;
    }
}
```

### Connection completed

When the pending connection is dropped on a valid target, the `CompletedCommand` is executed. The parameter is the target connector (or `ItemContainer` if `AllowOnlyConnectors` is `false`, or `null` if dropped on empty space).

### Disconnecting

ALT+Click on a connector fires the `DisconnectCommand`. You can also handle this at the editor level with `NodifyEditor.DisconnectConnectorCommand`:

```csharp
DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(connector =>
{
    var connectionsToRemove = Connections
        .Where(c => c.Source == connector || c.Target == connector)
        .ToList();

    foreach (var connection in connectionsToRemove)
    {
        connection.Source.IsConnected = Connections
            .Any(c => c != connection && (c.Source == connection.Source || c.Target == connection.Source));
        connection.Target.IsConnected = Connections
            .Any(c => c != connection && (c.Source == connection.Target || c.Target == connection.Target));
        Connections.Remove(connection);
    }
});
```

> **Tip:** When removing connections, check whether the connector still has _other_ connections before setting `IsConnected` to `false`. The [Getting Started](Getting-Started#removing-connections) guide shows a simplified version; the example above handles the case where a connector has multiple connections.

### Connection validation

To validate connections before they are created, add logic to the `FinishCommand` or the `CanExecute` of the `CompletedCommand`. Common validations include:

- Preventing self-connections (source and target on the same node)
- Preventing duplicate connections
- Enforcing type compatibility between connectors
- Limiting the number of connections per connector

```csharp
private bool CanConnect(ConnectorViewModel source, ConnectorViewModel target)
{
    // Different nodes
    if (source.Node == target.Node)
        return false;

    // One input, one output
    if (source.IsInput == target.IsInput)
        return false;

    // No duplicates
    if (Connections.Any(c =>
        (c.Source == source && c.Target == target) ||
        (c.Source == target && c.Target == source)))
        return false;

    // Type compatibility
    if (source.DataType != null && target.DataType != null
        && source.DataType != target.DataType)
        return false;

    return true;
}
```

## Worked example: math nodes

Let's build a small math-node editor with Add and Multiply nodes. Each node takes numeric inputs, performs an operation, and outputs the result. This example demonstrates all the concepts covered above.

### The operation interface

First, define a simple interface for operations:

```csharp
public interface IMathOperation
{
    double Execute(double[] inputs);
}

public class AddOperation : IMathOperation
{
    public double Execute(double[] inputs) => inputs.Sum();
}

public class MultiplyOperation : IMathOperation
{
    public double Execute(double[] inputs)
    {
        double result = 1;
        foreach (var input in inputs)
            result *= input;
        return result;
    }
}
```

### Connector ViewModel

The connector ViewModel stores the value flowing through it and notifies downstream observers when the value changes:

```csharp
public class MathConnectorViewModel : INotifyPropertyChanged
{
    private string _title;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private double _value;
    public double Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
            foreach (var observer in ValueObservers)
                observer.Value = value;
        }
    }

    private bool _isConnected;
    public bool IsConnected
    {
        get => _isConnected;
        set { _isConnected = value; OnPropertyChanged(); }
    }

    private bool _isInput;
    public bool IsInput
    {
        get => _isInput;
        set { _isInput = value; OnPropertyChanged(); }
    }

    private Point _anchor;
    public Point Anchor
    {
        get => _anchor;
        set { _anchor = value; OnPropertyChanged(); }
    }

    public MathNodeViewModel Node { get; set; }
    public List<MathConnectorViewModel> ValueObservers { get; } = new List<MathConnectorViewModel>();

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

When a source connector's `Value` changes, it pushes the new value to every connector in its `ValueObservers` list. This is how data flows through connections.

### Node ViewModels

The base node ViewModel holds the operation, input connectors, and an output connector. When any input value changes, it re-executes the operation and updates the output:

```csharp
public class MathNodeViewModel : INotifyPropertyChanged
{
    public MathNodeViewModel()
    {
        // Subscribe to input value changes
        Input.CollectionChanged += (s, e) =>
        {
            if (e.NewItems != null)
            {
                foreach (MathConnectorViewModel connector in e.NewItems)
                {
                    connector.Node = this;
                    connector.IsInput = true;
                    connector.PropertyChanged += OnInputPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (MathConnectorViewModel connector in e.OldItems)
                    connector.PropertyChanged -= OnInputPropertyChanged;
            }
        };
    }

    private void OnInputPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MathConnectorViewModel.Value))
            Recalculate();
    }

    private void Recalculate()
    {
        if (Output != null && Operation != null)
        {
            var inputs = Input.Select(i => i.Value).ToArray();
            Output.Value = Operation.Execute(inputs);
        }
    }

    private Point _location;
    public Point Location
    {
        get => _location;
        set { _location = value; OnPropertyChanged(); }
    }

    private string _title;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    public IMathOperation Operation { get; set; }

    public ObservableCollection<MathConnectorViewModel> Input { get; } = new ObservableCollection<MathConnectorViewModel>();

    private MathConnectorViewModel _output;
    public MathConnectorViewModel Output
    {
        get => _output;
        set
        {
            _output = value;
            if (_output != null)
                _output.Node = this;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

Now create factory methods or specific subclasses for each operation:

```csharp
public static class MathNodeFactory
{
    public static MathNodeViewModel CreateAddNode()
    {
        var node = new MathNodeViewModel
        {
            Title = "Add",
            Operation = new AddOperation(),
            Output = new MathConnectorViewModel { Title = "Result" }
        };
        node.Input.Add(new MathConnectorViewModel { Title = "A" });
        node.Input.Add(new MathConnectorViewModel { Title = "B" });
        return node;
    }

    public static MathNodeViewModel CreateMultiplyNode()
    {
        var node = new MathNodeViewModel
        {
            Title = "Multiply",
            Operation = new MultiplyOperation(),
            Output = new MathConnectorViewModel { Title = "Result" }
        };
        node.Input.Add(new MathConnectorViewModel { Title = "A" });
        node.Input.Add(new MathConnectorViewModel { Title = "B" });
        return node;
    }
}
```

### Connection ViewModel

The connection ViewModel links a source connector to a target connector and sets up value propagation:

```csharp
public class MathConnectionViewModel
{
    public MathConnectionViewModel(MathConnectorViewModel source, MathConnectorViewModel target)
    {
        Source = source;
        Target = target;

        Source.IsConnected = true;
        Target.IsConnected = true;

        // Propagate values from source to target
        Source.ValueObservers.Add(Target);
        Target.Value = Source.Value;
    }

    public MathConnectorViewModel Source { get; }
    public MathConnectorViewModel Target { get; }
}
```

### The editor ViewModel

The editor ViewModel manages the collection of nodes and connections, and handles the pending connection workflow:

```csharp
public class MathEditorViewModel : INotifyPropertyChanged
{
    public ObservableCollection<MathNodeViewModel> Nodes { get; } = new ObservableCollection<MathNodeViewModel>();
    public ObservableCollection<MathConnectionViewModel> Connections { get; } = new ObservableCollection<MathConnectionViewModel>();
    public ICommand DisconnectConnectorCommand { get; }
    public ICommand StartConnectionCommand { get; }
    public ICommand CreateConnectionCommand { get; }

    private MathConnectorViewModel _pendingSource;

    public MathEditorViewModel()
    {
        StartConnectionCommand = new DelegateCommand<MathConnectorViewModel>(source =>
        {
            _pendingSource = source;
        });

        CreateConnectionCommand = new DelegateCommand<MathConnectorViewModel>(target =>
        {
            if (target != null && _pendingSource != null && CanConnect(_pendingSource, target))
            {
                Connect(_pendingSource, target);
            }
        });

        DisconnectConnectorCommand = new DelegateCommand<MathConnectorViewModel>(connector =>
        {
            var toRemove = Connections
                .Where(c => c.Source == connector || c.Target == connector)
                .ToList();

            foreach (var conn in toRemove)
            {
                conn.Source.ValueObservers.Remove(conn.Target);
                Connections.Remove(conn);

                conn.Source.IsConnected = Connections.Any(c => c.Source == conn.Source);
                conn.Target.IsConnected = Connections.Any(c => c.Target == conn.Target);
            }
        });

        // Create sample nodes
        var addNode = MathNodeFactory.CreateAddNode();
        addNode.Location = new Point(100, 100);

        var multiplyNode = MathNodeFactory.CreateMultiplyNode();
        multiplyNode.Location = new Point(400, 100);

        Nodes.Add(addNode);
        Nodes.Add(multiplyNode);

        // Connect Add result to Multiply input A
        Connect(addNode.Output, multiplyNode.Input[0]);
    }

    private bool CanConnect(MathConnectorViewModel source, MathConnectorViewModel target)
    {
        return source != target
            && source.Node != target.Node
            && source.IsInput != target.IsInput
            && !Connections.Any(c =>
                (c.Source == source && c.Target == target) ||
                (c.Source == target && c.Target == source));
    }

    private void Connect(MathConnectorViewModel source, MathConnectorViewModel target)
    {
        // Ensure source is always the output and target is the input
        if (source.IsInput)
            (source, target) = (target, source);

        Connections.Add(new MathConnectionViewModel(source, target));
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
```

### The view

Finally, the XAML view that brings it all together:

```xml
<Window x:Class="MathNodes.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:nodify="https://miroiu.github.io/nodify"
        xmlns:local="clr-namespace:MathNodes"
        Title="Math Nodes" Height="600" Width="800">
    <Window.DataContext>
        <local:MathEditorViewModel />
    </Window.DataContext>

    <Window.Resources>
        <BooleanToVisibilityConverter x:Key="BoolToVis" />
    </Window.Resources>

    <Grid Background="{StaticResource NodifyEditor.BackgroundBrush}">
        <nodify:NodifyEditor ItemsSource="{Binding Nodes}"
                             Connections="{Binding Connections}"
                             DisconnectConnectorCommand="{Binding DisconnectConnectorCommand}"
                             x:Name="Editor">

            <nodify:NodifyEditor.ItemContainerStyle>
                <Style TargetType="{x:Type nodify:ItemContainer}">
                    <Setter Property="Location"
                            Value="{Binding Location}" />
                </Style>
            </nodify:NodifyEditor.ItemContainerStyle>

            <nodify:NodifyEditor.Resources>
                <!-- Math node template -->
                <DataTemplate DataType="{x:Type local:MathNodeViewModel}">
                    <nodify:Node Header="{Binding Title}"
                                 Input="{Binding Input}"
                                 Output="{Binding Output}">
                        <nodify:Node.InputConnectorTemplate>
                            <DataTemplate DataType="{x:Type local:MathConnectorViewModel}">
                                <nodify:NodeInput IsConnected="{Binding IsConnected}"
                                                  Anchor="{Binding Anchor, Mode=OneWayToSource}">
                                    <nodify:NodeInput.HeaderTemplate>
                                        <DataTemplate>
                                            <StackPanel Orientation="Horizontal">
                                                <TextBlock Text="{Binding Title}"
                                                           Margin="0 0 4 0" />
                                                <TextBox Text="{Binding Value, UpdateSourceTrigger=PropertyChanged}"
                                                         MinWidth="40"
                                                         Visibility="{Binding IsConnected,
                                                             Converter={StaticResource BoolToVis},
                                                             ConverterParameter=Inverse}" />
                                            </StackPanel>
                                        </DataTemplate>
                                    </nodify:NodeInput.HeaderTemplate>
                                </nodify:NodeInput>
                            </DataTemplate>
                        </nodify:Node.InputConnectorTemplate>

                        <nodify:Node.OutputConnectorTemplate>
                            <DataTemplate DataType="{x:Type local:MathConnectorViewModel}">
                                <nodify:NodeOutput IsConnected="{Binding IsConnected}"
                                                   Anchor="{Binding Anchor, Mode=OneWayToSource}">
                                    <nodify:NodeOutput.HeaderTemplate>
                                        <DataTemplate>
                                            <TextBlock Text="{Binding Value, StringFormat=N2}"
                                                       MinWidth="40"
                                                       TextAlignment="Right" />
                                        </DataTemplate>
                                    </nodify:NodeOutput.HeaderTemplate>
                                </nodify:NodeOutput>
                            </DataTemplate>
                        </nodify:Node.OutputConnectorTemplate>
                    </nodify:Node>
                </DataTemplate>
            </nodify:NodifyEditor.Resources>

            <!-- Pending connection -->
            <nodify:NodifyEditor.PendingConnectionTemplate>
                <DataTemplate>
                    <nodify:PendingConnection
                        StartedCommand="{Binding DataContext.StartConnectionCommand,
                            RelativeSource={RelativeSource AncestorType={x:Type nodify:NodifyEditor}}}"
                        CompletedCommand="{Binding DataContext.CreateConnectionCommand,
                            RelativeSource={RelativeSource AncestorType={x:Type nodify:NodifyEditor}}}"
                        AllowOnlyConnectors="True" />
                </DataTemplate>
            </nodify:NodifyEditor.PendingConnectionTemplate>

            <!-- Connection template -->
            <nodify:NodifyEditor.ConnectionTemplate>
                <DataTemplate DataType="{x:Type local:MathConnectionViewModel}">
                    <nodify:Connection Source="{Binding Source.Anchor}"
                                       Target="{Binding Target.Anchor}" />
                </DataTemplate>
            </nodify:NodifyEditor.ConnectionTemplate>
        </nodify:NodifyEditor>
    </Grid>
</Window>
```

This gives you a working math-node editor where:

- **Add** computes the sum of its inputs
- **Multiply** computes the product of its inputs
- Unconnected inputs show a text box for manual value entry
- Connected inputs hide the text box and receive values from the upstream node
- Changing an input value propagates through the entire graph in real time

> **Tip:** To extend this with more operations, create new `IMathOperation` implementations and factory methods. The view automatically picks up the `MathNodeViewModel` `DataTemplate` for any new node.

For a full-featured version of this concept, see the [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator) which adds expression parsing, expandable input lists, nested sub-graphs, grouping nodes, and a drag-and-drop operation toolbox.

## See also

- [Getting Started](Getting-Started) — minimal editor setup, connecting nodes, pending connections
- [Nodes overview](Nodes-Overview) — built-in node controls (`Node`, `GroupingNode`, `KnotNode`, `StateNode`)
- [Connectors overview](Connectors-Overview) — `NodeInput`, `NodeOutput`, and the `Connector` base class
- [Connections overview](Connections-Overview) — connection types and [custom connections](Connections-Overview#custom-connection)
- [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator) — real-time calculator with multiple node types
- [Playground example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground) — interactive settings for all built-in controls

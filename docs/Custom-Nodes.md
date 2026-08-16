Nodify ships with several built-in node controls ([Node](Nodes-Overview#1-the-node-control), [GroupingNode](Nodes-Overview#2-the-groupingnode-control), [KnotNode](Nodes-Overview#3-the-knotnode-control), [StateNode](Nodes-Overview#4-the-statenode-control)), but you can create fully custom nodes tailored to your application. This guide walks through building a custom node from scratch using MVVM, adding typed connectors, customizing its appearance, handling connection events, and putting it all together with a worked example.

## Table of contents

- [Creating a basic custom node](#creating-a-basic-custom-node)
- [Adding typed input and output connectors](#adding-typed-input-and-output-connectors)
- [Customizing appearance with styles and templates](#customizing-appearance-with-styles-and-templates)
- [Responding to connection events](#responding-to-connection-events)
- [Worked example: math nodes](#worked-example-math-nodes)

## Creating a basic custom node

Nodes in Nodify can be **any** WPF control. When you add an item to the `NodifyEditor.ItemsSource`, it is automatically wrapped in an [ItemContainer](ItemContainer-Overview) that makes it selectable, draggable, and positionable on the canvas. This means you can use the built-in `Node` control, compose one from standard WPF elements, or create an entirely custom `UserControl`.

### ViewModel

Start with a simple ViewModel that holds a title and a position:

```csharp
public class CustomNodeViewModel : INotifyPropertyChanged
{
    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private Point _location;
    public Point Location
    {
        get => _location;
        set { _location = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

### XAML: using an ItemTemplate

The simplest way to display a custom node is through the editor's `ItemTemplate`. Anything you put inside the template becomes the node's visual representation:

```xml
<nodify:NodifyEditor ItemsSource="{Binding Nodes}">
    <nodify:NodifyEditor.ItemContainerStyle>
        <Style TargetType="{x:Type nodify:ItemContainer}">
            <Setter Property="Location" Value="{Binding Location}" />
        </Style>
    </nodify:NodifyEditor.ItemContainerStyle>

    <nodify:NodifyEditor.ItemTemplate>
        <DataTemplate DataType="{x:Type local:CustomNodeViewModel}">
            <Border Background="#2A3038" CornerRadius="5" Padding="16 8" BorderThickness="2"
                    BorderBrush="#4DA3FF">
                <TextBlock Text="{Binding Title}" Foreground="White" FontWeight="SemiBold" />
            </Border>
        </DataTemplate>
    </nodify:NodifyEditor.ItemTemplate>
</nodify:NodifyEditor>
```

### XAML: using implicit DataTemplates

When your editor contains multiple node types, use implicit `DataTemplate`s inside the editor's `Resources` instead of a single `ItemTemplate`. The editor will automatically select the correct template based on the ViewModel type:

```xml
<nodify:NodifyEditor ItemsSource="{Binding Nodes}">
    <nodify:NodifyEditor.Resources>
        <DataTemplate DataType="{x:Type local:CustomNodeViewModel}">
            <Border Background="#2A3038" CornerRadius="5" Padding="16 8">
                <TextBlock Text="{Binding Title}" Foreground="White" />
            </Border>
        </DataTemplate>

        <DataTemplate DataType="{x:Type local:AnotherNodeViewModel}">
            <nodify:Node Header="{Binding Title}" />
        </DataTemplate>
    </nodify:NodifyEditor.Resources>
</nodify:NodifyEditor>
```

> Tip: You can mix built-in node controls like `Node` or `GroupingNode` with fully custom visuals in the same editor by using different DataTemplates for each ViewModel type.

## Adding typed input and output connectors

To connect nodes together, you need **connectors** — interactive elements that produce [PendingConnection](Connections-Overview#pending-connection) events when the user clicks and drags. The built-in `Node` control has `Input` and `Output` collections with corresponding `InputConnectorTemplate` and `OutputConnectorTemplate` properties to render them, but you can also place connectors in your own custom layout.

### Connector ViewModel

A connector needs an `Anchor` point (where the connection wire attaches) and an `IsConnected` flag (which must be `true` for the anchor to receive updates):

```csharp
public class ConnectorViewModel : INotifyPropertyChanged
{
    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private Point _anchor;
    public Point Anchor
    {
        get => _anchor;
        set { _anchor = value; OnPropertyChanged(); }
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

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

### Node ViewModel with connectors

Add input and output collections to the node:

```csharp
public class CustomNodeViewModel : INotifyPropertyChanged
{
    // ... Title, Location properties as above ...

    public ObservableCollection<ConnectorViewModel> Input { get; } = new ObservableCollection<ConnectorViewModel>();
    public ObservableCollection<ConnectorViewModel> Output { get; } = new ObservableCollection<ConnectorViewModel>();
}
```

### Using the built-in Node control with connectors

The easiest way to add connectors is to use the built-in `Node` control and bind its `Input`/`Output` collections:

```xml
<DataTemplate DataType="{x:Type local:CustomNodeViewModel}">
    <nodify:Node Header="{Binding Title}"
                 Input="{Binding Input}"
                 Output="{Binding Output}">
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
    </nodify:Node>
</DataTemplate>
```

> Important: The `Anchor` binding **must** use `Mode=OneWayToSource` so the control writes its calculated position back to the ViewModel. The `IsConnected` property **must** be `true` for anchor updates to fire — bind it to your ViewModel's property or set it to `True` directly.

### Placing connectors in a custom layout

If you need full control over where connectors appear, skip the `Node` control and place `Connector`, `NodeInput`, or `NodeOutput` controls anywhere in your custom template:

```xml
<DataTemplate DataType="{x:Type local:CustomNodeViewModel}">
    <Border Background="#2A3038" CornerRadius="5" Padding="10">
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="Auto" />
            </Grid.ColumnDefinitions>

            <!-- Input connectors on the left -->
            <ItemsControl Grid.Column="0" ItemsSource="{Binding Input}">
                <ItemsControl.ItemTemplate>
                    <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                        <nodify:NodeInput Header="{Binding Title}"
                                          IsConnected="{Binding IsConnected}"
                                          Anchor="{Binding Anchor, Mode=OneWayToSource}" />
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>

            <!-- Node content in the center -->
            <TextBlock Grid.Column="1" Text="{Binding Title}"
                       VerticalAlignment="Center" Margin="10 0"
                       Foreground="White" />

            <!-- Output connectors on the right -->
            <ItemsControl Grid.Column="2" ItemsSource="{Binding Output}">
                <ItemsControl.ItemTemplate>
                    <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                        <nodify:NodeOutput Header="{Binding Title}"
                                           IsConnected="{Binding IsConnected}"
                                           Anchor="{Binding Anchor, Mode=OneWayToSource}" />
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>
        </Grid>
    </Border>
</DataTemplate>
```

## Customizing appearance with styles and templates

### Styling the Node control

The built-in `Node` control exposes several areas you can customize without replacing the entire template:

| Area | Property | Template Property |
|------|----------|-------------------|
| Header | `Header` | `HeaderTemplate` |
| Footer | `Footer` | `FooterTemplate` |
| Body | `Content` | `ContentTemplate` |
| Input connectors | `Input` | `InputConnectorTemplate` |
| Output connectors | `Output` | `OutputConnectorTemplate` |
| Header background | `HeaderBrush` | — |
| Body background | `ContentBrush` | — |
| Footer background | `FooterBrush` | — |
| Body padding | `ContentPadding` | — |

Example with a header, content, and footer:

```xml
<nodify:Node Header="{Binding Title}"
             Content="{Binding}"
             Footer="{Binding StatusText}"
             Input="{Binding Input}"
             Output="{Binding Output}">
    <nodify:Node.HeaderTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <Ellipse Width="8" Height="8" Fill="LimeGreen" Margin="0 0 6 0" />
                <TextBlock Text="{Binding}" FontWeight="Bold" />
            </StackPanel>
        </DataTemplate>
    </nodify:Node.HeaderTemplate>
    <nodify:Node.ContentTemplate>
        <DataTemplate>
            <TextBox Text="{Binding Expression}" MinWidth="100" />
        </DataTemplate>
    </nodify:Node.ContentTemplate>
</nodify:Node>
```

### Styling connectors

Override the connector header template to display extra information (e.g. the current value) or change the connector icon via `ConnectorTemplate`:

```xml
<nodify:NodeInput Header="{Binding}"
                  IsConnected="{Binding IsConnected}"
                  Anchor="{Binding Anchor, Mode=OneWayToSource}">
    <nodify:NodeInput.HeaderTemplate>
        <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="{Binding Title}" Margin="0 0 5 0" />
                <TextBox Text="{Binding Value}"
                         Visibility="{Binding IsConnected, Converter={StaticResource InverseBoolToVisibility}}" />
            </StackPanel>
        </DataTemplate>
    </nodify:NodeInput.HeaderTemplate>
</nodify:NodeInput>
```

### Customizing the ItemContainer

The [ItemContainer](ItemContainer-Overview) wrapping each node can be styled via `ItemContainerStyle` on the editor. Common properties to bind:

```xml
<nodify:NodifyEditor.ItemContainerStyle>
    <Style TargetType="{x:Type nodify:ItemContainer}"
           BasedOn="{StaticResource {x:Type nodify:ItemContainer}}">
        <Setter Property="Location" Value="{Binding Location}" />
        <Setter Property="IsSelected" Value="{Binding IsSelected}" />
        <Setter Property="ActualSize" Value="{Binding Size, Mode=OneWayToSource}" />
    </Style>
</nodify:NodifyEditor.ItemContainerStyle>
```

### Theme color overrides

You can override individual color keys to change the look of all nodes without replacing templates. See [Theming](Theming) for the full list of color keys. A quick example:

```xml
<ResourceDictionary>
    <Color x:Key="Node.HeaderColor">#1B2838</Color>
    <Color x:Key="Node.BackgroundColor">#2A3038</Color>
    <Color x:Key="Connector.BorderColor">#FF6B35</Color>
</ResourceDictionary>
```

## Responding to connection events

### Creating connections

When the user drags a wire from a connector, Nodify fires `PendingConnectionStarted` and `PendingConnectionCompleted` events. Handle these through commands bound on the `PendingConnection`:

```csharp
public class EditorViewModel
{
    public ObservableCollection<CustomNodeViewModel> Nodes { get; } = new();
    public ObservableCollection<ConnectionViewModel> Connections { get; } = new();
    public PendingConnectionViewModel PendingConnection { get; }

    public ICommand ConnectionStartedCommand { get; }
    public ICommand ConnectionCompletedCommand { get; }

    private ConnectorViewModel? _pendingSource;

    public EditorViewModel()
    {
        PendingConnection = new PendingConnectionViewModel();

        ConnectionStartedCommand = new DelegateCommand<ConnectorViewModel>(
            source => _pendingSource = source);

        ConnectionCompletedCommand = new DelegateCommand<ConnectorViewModel>(target =>
        {
            if (target != null && _pendingSource != null && _pendingSource != target)
            {
                var input = _pendingSource.IsInput ? _pendingSource : target;
                var output = target.IsInput ? _pendingSource : target;

                Connections.Add(new ConnectionViewModel(output, input));
            }
        });
    }
}
```

And bind the commands in XAML:

```xml
<nodify:NodifyEditor.PendingConnectionTemplate>
    <DataTemplate DataType="{x:Type local:PendingConnectionViewModel}">
        <nodify:PendingConnection
            StartedCommand="{Binding DataContext.ConnectionStartedCommand,
                RelativeSource={RelativeSource AncestorType={x:Type nodify:NodifyEditor}}}"
            CompletedCommand="{Binding DataContext.ConnectionCompletedCommand,
                RelativeSource={RelativeSource AncestorType={x:Type nodify:NodifyEditor}}}"
            AllowOnlyConnectors="True" />
    </DataTemplate>
</nodify:NodifyEditor.PendingConnectionTemplate>
```

### Disconnecting

Handle disconnects by binding the `DisconnectConnectorCommand` on the editor:

```csharp
public ICommand DisconnectCommand { get; }

// In constructor:
DisconnectCommand = new DelegateCommand<ConnectorViewModel>(connector =>
{
    var toRemove = Connections
        .Where(c => c.Source == connector || c.Target == connector)
        .ToList();

    foreach (var connection in toRemove)
    {
        connection.Source.IsConnected = Connections.Any(c => c != connection && (c.Source == connection.Source || c.Target == connection.Source));
        connection.Target.IsConnected = Connections.Any(c => c != connection && (c.Source == connection.Target || c.Target == connection.Target));
        Connections.Remove(connection);
    }
});
```

```xml
<nodify:NodifyEditor DisconnectConnectorCommand="{Binding DisconnectCommand}" />
```

### Connection validation

You can prevent invalid connections (e.g., connecting an input to another input) by adding a condition to the `CanExecute` of the completed command:

```csharp
ConnectionCompletedCommand = new DelegateCommand<ConnectorViewModel>(
    target => { /* create connection */ },
    target => target != null
              && _pendingSource != null
              && _pendingSource != target
              && _pendingSource.IsInput != target.IsInput  // must connect input to output
);
```

## Worked example: math nodes

This example builds a simple math-node editor with **Add** and **Multiply** operations. Each node takes two number inputs, computes the result, and exposes it as an output. This pattern is used extensively in the [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator).

### Step 1: Define the operation interface

```csharp
public interface IMathOperation
{
    double Execute(double a, double b);
}

public class AddOperation : IMathOperation
{
    public double Execute(double a, double b) => a + b;
}

public class MultiplyOperation : IMathOperation
{
    public double Execute(double a, double b) => a * b;
}
```

### Step 2: Create the math node ViewModel

```csharp
public class MathNodeViewModel : INotifyPropertyChanged
{
    public MathNodeViewModel(string title, IMathOperation operation)
    {
        Title = title;
        _operation = operation;

        InputA = new ConnectorViewModel { Title = "A", IsInput = true };
        InputB = new ConnectorViewModel { Title = "B", IsInput = true };
        Output = new ConnectorViewModel { Title = "Result", IsInput = false };

        InputA.PropertyChanged += OnInputChanged;
        InputB.PropertyChanged += OnInputChanged;
    }

    private readonly IMathOperation _operation;

    public string Title { get; }

    private Point _location;
    public Point Location
    {
        get => _location;
        set { _location = value; OnPropertyChanged(); }
    }

    public ConnectorViewModel InputA { get; }
    public ConnectorViewModel InputB { get; }
    public ConnectorViewModel Output { get; }

    public ObservableCollection<ConnectorViewModel> Inputs => new() { InputA, InputB };
    public ObservableCollection<ConnectorViewModel> Outputs => new() { Output };

    private void OnInputChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ConnectorViewModel.Value))
        {
            Output.Value = _operation.Execute(InputA.Value, InputB.Value);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

The `ConnectorViewModel` for this example extends the earlier version with a `Value` property and a list of observers that propagate value changes through connections:

```csharp
public class ConnectorViewModel : INotifyPropertyChanged
{
    // ... Title, Anchor, IsConnected, IsInput properties as above ...

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

    public List<ConnectorViewModel> ValueObservers { get; } = new();

    // ...
}
```

### Step 3: Wire up connections with value propagation

When a connection is created, register the input connector as an observer of the output connector:

```csharp
public void Connect(ConnectorViewModel source, ConnectorViewModel target)
{
    var output = source.IsInput ? target : source;
    var input = source.IsInput ? source : target;

    input.IsConnected = true;
    output.IsConnected = true;
    input.Value = output.Value;
    output.ValueObservers.Add(input);

    Connections.Add(new ConnectionViewModel(output, input));
}
```

### Step 4: Create the editor ViewModel

```csharp
public class MathEditorViewModel
{
    public ObservableCollection<MathNodeViewModel> Nodes { get; } = new();
    public ObservableCollection<ConnectionViewModel> Connections { get; } = new();

    public MathEditorViewModel()
    {
        var addNode = new MathNodeViewModel("Add", new AddOperation())
        {
            Location = new Point(100, 100)
        };

        var multiplyNode = new MathNodeViewModel("Multiply", new MultiplyOperation())
        {
            Location = new Point(400, 100)
        };

        Nodes.Add(addNode);
        Nodes.Add(multiplyNode);

        // Connect the Add output to the Multiply first input
        Connect(addNode.Output, multiplyNode.InputA);
    }

    // ... Connect, Disconnect, PendingConnection logic as above ...
}
```

### Step 5: Build the view

```xml
<nodify:NodifyEditor ItemsSource="{Binding Nodes}"
                     Connections="{Binding Connections}"
                     PendingConnection="{Binding PendingConnection}"
                     DisconnectConnectorCommand="{Binding DisconnectCommand}">

    <nodify:NodifyEditor.ItemContainerStyle>
        <Style TargetType="{x:Type nodify:ItemContainer}"
               BasedOn="{StaticResource {x:Type nodify:ItemContainer}}">
            <Setter Property="Location" Value="{Binding Location}" />
        </Style>
    </nodify:NodifyEditor.ItemContainerStyle>

    <nodify:NodifyEditor.Resources>
        <DataTemplate DataType="{x:Type local:MathNodeViewModel}">
            <nodify:Node Header="{Binding Title}"
                         Input="{Binding Inputs}"
                         Output="{Binding Outputs}">
                <nodify:Node.InputConnectorTemplate>
                    <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                        <nodify:NodeInput Header="{Binding Title}"
                                          IsConnected="{Binding IsConnected}"
                                          Anchor="{Binding Anchor, Mode=OneWayToSource}"
                                          ToolTip="{Binding Value}" />
                    </DataTemplate>
                </nodify:Node.InputConnectorTemplate>
                <nodify:Node.OutputConnectorTemplate>
                    <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                        <nodify:NodeOutput Header="{Binding Title}"
                                           IsConnected="{Binding IsConnected}"
                                           Anchor="{Binding Anchor, Mode=OneWayToSource}"
                                           ToolTip="{Binding Value}" />
                    </DataTemplate>
                </nodify:Node.OutputConnectorTemplate>
            </nodify:Node>
        </DataTemplate>
    </nodify:NodifyEditor.Resources>

    <nodify:NodifyEditor.ConnectionTemplate>
        <DataTemplate DataType="{x:Type local:ConnectionViewModel}">
            <nodify:Connection Source="{Binding Source.Anchor}"
                               Target="{Binding Target.Anchor}" />
        </DataTemplate>
    </nodify:NodifyEditor.ConnectionTemplate>

    <nodify:NodifyEditor.PendingConnectionTemplate>
        <DataTemplate DataType="{x:Type local:PendingConnectionViewModel}">
            <nodify:PendingConnection
                StartedCommand="{Binding DataContext.ConnectionStartedCommand,
                    RelativeSource={RelativeSource AncestorType={x:Type nodify:NodifyEditor}}}"
                CompletedCommand="{Binding DataContext.ConnectionCompletedCommand,
                    RelativeSource={RelativeSource AncestorType={x:Type nodify:NodifyEditor}}}"
                AllowOnlyConnectors="True" />
        </DataTemplate>
    </nodify:NodifyEditor.PendingConnectionTemplate>
</nodify:NodifyEditor>
```

### Result

Changing the value of an input on the Add node will compute `A + B`, push the result through the connection to the Multiply node's first input, and the Multiply node will recompute its output. This value propagation pattern is how the [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator) works — see `OperationViewModel` and `ConnectorViewModel` in that project for a production-ready implementation.

## See also

- [Nodes overview](Nodes-Overview) — built-in node controls
- [Connectors overview](Connectors-Overview) — built-in connector controls
- [Connections overview](Connections-Overview) — connection types and pending connections
- [Getting Started](Getting-Started) — full setup from scratch
- [Theming](Theming) — color customization and custom themes
- [Calculator example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Calculator) — a complete node-based calculator
- [Playground example](https://github.com/miroiu/nodify/tree/master/Examples/Nodify.Playground) — interactive testbed for Nodify features

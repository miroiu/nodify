# Creating Custom Nodes

In Nodify, nodes are the basic building blocks of a node graph. While Nodify provides built-in node controls such as `Node`, `GroupingNode`, `KnotNode`, and `StateNode`, you can easily create fully customized nodes to fit your application's unique user interface and experience.

Because Nodify is built from the ground up for MVVM, the `NodifyEditor` wraps every element in its `ItemsSource` inside an `ItemContainer`. This means that **any WPF control** can serve as a node! You are not restricted to subclassing Nodify controls; you can style or layout custom nodes using standard WPF panels (like `Grid` or `StackPanel`) and populate them with standard WPF elements.

---

## Minimal Example: Simple Custom Node

Let's build a simple custom node with:
- A custom title/header.
- An area for node settings or inputs (e.g. a `TextBox`).
- A list of input and output connectors.

### 1. The ViewModel representation

First, we will define our `ConnectorViewModel` and `CustomNodeViewModel` classes. To support WPF binding, these models implement `INotifyPropertyChanged`.

```csharp
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

public class ConnectorViewModel : INotifyPropertyChanged
{
    private Point _anchor;
    public Point Anchor
    {
        get => _anchor;
        set
        {
            _anchor = value;
            OnPropertyChanged(nameof(Anchor));
        }
    }

    private bool _isConnected;
    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            _isConnected = value;
            OnPropertyChanged(nameof(IsConnected));
        }
    }

    public string Title { get; set; } // Simplified for brevity; raise PropertyChanged if updating at runtime

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class CustomNodeViewModel : INotifyPropertyChanged
{
    private Point _location;
    public Point Location
    {
        get => _location;
        set
        {
            _location = value;
            OnPropertyChanged(nameof(Location));
        }
    }

    private string _nodeName;
    public string NodeName
    {
        get => _nodeName;
        set
        {
            _nodeName = value;
            OnPropertyChanged(nameof(NodeName));
        }
    }

    private string _customText;
    public string CustomText
    {
        get => _customText;
        set
        {
            _customText = value;
            OnPropertyChanged(nameof(CustomText));
        }
    }

    public ObservableCollection<ConnectorViewModel> Inputs { get; } = new ObservableCollection<ConnectorViewModel>();
    public ObservableCollection<ConnectorViewModel> Outputs { get; } = new ObservableCollection<ConnectorViewModel>();

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```

### 2. The XAML View: Styling and Templating the Custom Node

We bind the ViewModel to a View by defining a `DataTemplate` for our `CustomNodeViewModel`. This template can be placed in `NodifyEditor.ItemTemplate` or in a Resource Dictionary. We will lay out the header, internal controls, and lists of inputs and outputs inside standard WPF containers.

```xml
<DataTemplate DataType="{x:Type local:CustomNodeViewModel}">
    <Border Background="#2D2D30" 
            BorderBrush="#3F3F46" 
            BorderThickness="2" 
            CornerRadius="6" 
            Padding="8"
            Width="200">
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" /> <!-- Header -->
                <RowDefinition Height="Auto" /> <!-- Custom Content -->
                <RowDefinition Height="*" />    <!-- Connectors -->
            </Grid.RowDefinitions>

            <!-- Node Header -->
            <TextBlock Text="{Binding NodeName}" 
                       FontWeight="Bold" 
                       Foreground="White" 
                       FontSize="14"
                       Margin="0,0,0,8"/>

            <!-- Node Content / Internal Controls -->
            <StackPanel Grid.Row="1" Margin="0,0,0,8">
                <TextBlock Text="Parameter:" Foreground="LightGray" FontSize="11" />
                <TextBox Text="{Binding CustomText, UpdateSourceTrigger=PropertyChanged}" 
                         Background="#1E1E1E" 
                         Foreground="White" 
                         BorderBrush="#3F3F46" 
                         Padding="3"/>
            </StackPanel>

            <!-- Connectors (Inputs Left, Outputs Right) -->
            <Grid Grid.Row="2">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>

                <!-- Input Connectors (Left Aligned) -->
                <ItemsControl ItemsSource="{Binding Inputs}" Focusable="False">
                    <ItemsControl.ItemTemplate>
                        <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                            <!-- Use Nodify's built-in NodeInput control -->
                            <nodify:NodeInput Header="{Binding Title}" 
                                              Anchor="{Binding Anchor, Mode=OneWayToSource}"
                                              IsConnected="{Binding IsConnected}" />
                        </DataTemplate>
                    </ItemsControl.ItemTemplate>
                    <ItemsControl.ItemsPanel>
                        <ItemsPanelTemplate>
                            <StackPanel Orientation="Vertical" HorizontalAlignment="Left" />
                        </ItemsPanelTemplate>
                    </ItemsControl.ItemsPanel>
                </ItemsControl>

                <!-- Output Connectors (Right Aligned) -->
                <ItemsControl Grid.Column="1" ItemsSource="{Binding Outputs}" Focusable="False">
                    <ItemsControl.ItemTemplate>
                        <DataTemplate DataType="{x:Type local:ConnectorViewModel}">
                            <!-- Use Nodify's built-in NodeOutput control -->
                            <nodify:NodeOutput Header="{Binding Title}" 
                                               Anchor="{Binding Anchor, Mode=OneWayToSource}"
                                               IsConnected="{Binding IsConnected}" />
                        </DataTemplate>
                    </ItemsControl.ItemTemplate>
                    <ItemsControl.ItemsPanel>
                        <ItemsPanelTemplate>
                            <StackPanel Orientation="Vertical" HorizontalAlignment="Right" />
                        </ItemsPanelTemplate>
                    </ItemsControl.ItemsPanel>
                </ItemsControl>
            </Grid>
        </Grid>
    </Border>
</DataTemplate>
```

### 3. Registering/Adding the custom node

To display this custom node in the editor, simply add an instance of your `CustomNodeViewModel` to the nodes collection in your `EditorViewModel`:

```csharp
public class EditorViewModel
{
    public ObservableCollection<object> Nodes { get; } = new ObservableCollection<object>();

    public EditorViewModel()
    {
        var node = new CustomNodeViewModel
        {
            NodeName = "My Custom Node",
            CustomText = "Default Value",
            Location = new Point(100, 100)
        };
        node.Inputs.Add(new ConnectorViewModel { Title = "In Value" });
        node.Outputs.Add(new ConnectorViewModel { Title = "Out Result" });

        Nodes.Add(node);
    }
}
```

Then bind the `Location` of the node inside the `ItemContainerStyle` of the `NodifyEditor`:

```xml
<nodify:NodifyEditor ItemsSource="{Binding Nodes}">
    <nodify:NodifyEditor.ItemContainerStyle>
        <Style TargetType="{x:Type nodify:ItemContainer}">
            <Setter Property="Location" Value="{Binding Location}" />
        </Style>
    </nodify:NodifyEditor.ItemContainerStyle>
</nodify:NodifyEditor>
```

---

## Key Design Patterns & MVVM Bindings

1. **The Location Binding (`Location` property on `ItemContainer`)**: Because Nodify wraps each item in an `ItemContainer`, you must use `ItemContainerStyle` to bind the node's coordinate space (`Point`) from your view model.
2. **The Connector Anchors (`Anchor` property on `Connector`)**: If you want connections to follow dragging and move dynamically, you must bind the connector's `Anchor` property in a two-way fashion (`Mode=OneWayToSource`) to a `Point` property in your ViewModel, and set `IsConnected` to `True`.
3. **No Inheritance Necessary**: A custom node does not need to inherit from Nodify's `Node` class. Standard layout controls inside a `Border` or `Grid` are fully compatible. Inherit from `Node` only if you wish to reuse built-in header, footer, or collection templates.

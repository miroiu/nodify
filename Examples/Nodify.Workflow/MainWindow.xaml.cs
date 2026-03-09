using System.Windows;

namespace Nodify.Workflow;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        StateChanged += UpdateMainWindowVisuals;
    }

    private void UpdateMainWindowVisuals(object? sender, EventArgs args)
    {
        BorderThickness = WindowState is WindowState.Maximized ? new Thickness(8) : new Thickness(0);
    }
}
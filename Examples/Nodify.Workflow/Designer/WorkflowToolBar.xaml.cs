using System.Windows;
using System.Windows.Controls;

namespace Nodify.Workflow.Designer;

/// <summary>
/// Interaction logic for WorkflowToolBar.xaml
/// </summary>
public partial class WorkflowToolBar : UserControl
{
    public static DependencyProperty EditorProperty = DependencyProperty.Register(nameof(Editor), typeof(NodifyEditor), typeof(WorkflowToolBar));

    public NodifyEditor Editor
    {
        get => (NodifyEditor)GetValue(EditorProperty);
        set => SetValue(EditorProperty, value);
    }

    public WorkflowToolBar()
    {
        InitializeComponent();
    }
}

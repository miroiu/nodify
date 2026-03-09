using Nodify.Interactivity;
using Nodify.Workflow.Shell;
using ObservableCollections;
using R3;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Workflow.Designer;

internal class WorkflowDesignerViewModel
{
    public BindableReactiveProperty<string> Name { get; }
    public ClampedViewportProperty ViewportPosition { get; }
    public BindableReactiveProperty<Size> ViewportSize { get; } = new();

    public ObservableList<WorkflowStepViewModel> Steps { get; } = [];
    public ObservableList<WorkflowStepConnectionViewModel> Connections { get; } = [];

    public BindableReactiveProperty<WorkflowStepViewModel?> SelectedStep { get; } = new(null);

    public EditorGestures EditorGestures { get; } = new EditorGestures();

    public WorkflowDesignerViewModel(ApplicationSettingsViewModel appSettings)
    {
        Name = new BindableReactiveProperty<string>(string.Empty);
        ViewportPosition = new ClampedViewportProperty(this);

        ConfigureDefaultGestures();
    }

    protected virtual void ConfigureDefaultGestures()
    {
        EditorGestures.Editor.PanWithMouseWheel = true;
        EditorGestures.Editor.PanVerticalModifierKey = ModifierKeys.Shift;
        EditorGestures.Editor.PanHorizontalModifierKey = ModifierKeys.None;
        EditorGestures.Editor.ZoomModifierKey = ModifierKeys.Control;
    }
}

internal sealed class SubWorkflowDesignerViewModel(ApplicationSettingsViewModel appSettings)
    : WorkflowDesignerViewModel(appSettings)
{
}

using FluentIcons.Common;
using Nodify.Interactivity;
using Nodify.Workflow.Common;
using Nodify.Workflow.Shell;
using ObservableCollections;
using R3;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Workflow.Designer;

public interface IViewportSizeAware
{
    abstract static BindableReactiveProperty<Size> ViewportSize { get; }
}

internal abstract class WorkflowDesignerViewModel<T>
    where T : IViewportSizeAware
{
    public BindableReactiveProperty<string> Name { get; }
    public ClampedViewportProperty<T> ViewportPosition { get; }

    public ObservableList<WorkflowStepViewModel> Steps { get; } = [];
    public ObservableList<WorkflowStepConnectionViewModel> Connections { get; } = [];

    public BindableReactiveProperty<WorkflowStepViewModel?> SelectedStep { get; } = new(null);

    public EditorGestures EditorGestures { get; } = new EditorGestures();

    public WorkflowDesignerViewModel(ApplicationSettingsViewModel appSettings)
    {
        Name = new BindableReactiveProperty<string>(string.Empty);
        ViewportPosition = new ClampedViewportProperty<T>(Steps);

        EditorGestures.Apply(appSettings.EditorGestures);

        ConfigureDefaultGestures();
    }

    public virtual void OnPostInitialize()
    {
        ViewportPosition.ObserveStepChanges();
    }

    protected virtual void ConfigureDefaultGestures()
    {
        EditorGestures.Editor.PanWithMouseWheel = true;
        EditorGestures.Editor.ZoomModifierKey = ModifierKeys.Control;
    }
}

internal sealed class MainWorkflowDesignerViewModel(ApplicationSettingsViewModel appSettings)
    : WorkflowDesignerViewModel<MainWorkflowDesignerViewModel>(appSettings), IViewportSizeAware
{
    public static BindableReactiveProperty<Size> ViewportSize { get; } = new();

    protected override void ConfigureDefaultGestures()
    {
        base.ConfigureDefaultGestures();

        EditorGestures.LockEditing();
    }
}

internal sealed class SubWorkflowDesignerViewModel : WorkflowDesignerViewModel<SubWorkflowDesignerViewModel>, IViewportSizeAware
{
    public static BindableReactiveProperty<Size> ViewportSize { get; } = new();

    public BindableReactiveProperty<double> ZoomLevel { get; } = new(1);

    public CommandViewModel FitToViewCommand { get; }
    public ToggleCommandViewModel LockViewCommand { get; }
    public CommandViewModel ZoomInCommand { get; }
    public CommandViewModel ZoomOutCommand { get; }

    public SubWorkflowDesignerViewModel(ApplicationSettingsViewModel appSettings) : base(appSettings)
    {
        FitToViewCommand = new CommandViewModel(new ReactiveCommand())
        {
            ToolTip = { Value = "Fit to view" },
            Icon = { Value = Icon.FullScreenMaximize },
        };

        LockViewCommand = new ToggleCommandViewModel(new ReactiveCommand())
        {
            ToolTip = { Value = "Lock view" },
            Icon = { Value = Icon.LockOpen },
            IconChecked = { Value = Icon.LockClosed },
        };

        ZoomInCommand = new CommandViewModel(new ReactiveCommand())
        {
            ToolTip = { Value = "Zoom in" },
            Icon = { Value = Icon.ZoomIn },
        };

        ZoomOutCommand = new CommandViewModel(new ReactiveCommand())
        {
            ToolTip = { Value = "Zoom out" },
            Icon = { Value = Icon.ZoomOut },
        };

        LockViewCommand.IsChecked.Subscribe(value =>
        {
            if (value)
            {
                EditorGestures.LockEditing();
            }
            else
            {
                EditorGestures.Apply(appSettings.EditorGestures);
                ConfigureDefaultGestures();
            }
        });
    }

    public override void OnPostInitialize()
    {
        base.OnPostInitialize();

        ViewportPosition.EdgePadding = 200;
    }

    protected override void ConfigureDefaultGestures()
    {
        base.ConfigureDefaultGestures();

        EditorGestures.Editor.PanVerticalModifierKey = ModifierKeys.Shift;
        EditorGestures.Editor.PanHorizontalModifierKey = ModifierKeys.None;
    }
}

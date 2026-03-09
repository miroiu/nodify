using FluentIcons.Common;
using Nodify.Interactivity;
using Nodify.Workflow.Common;
using Nodify.Workflow.Designer;
using ObservableCollections;
using R3;
using System.Windows;
using System.Windows.Media;

namespace Nodify.Workflow.Shell;

internal sealed class ApplicationViewModel
{
    public string Title { get; } = "Workflow Designer";

    public ObservableList<SubWorkflowDesignerViewModel> Workflows { get; } = [];

    public BindableReactiveProperty<WorkflowDesignerViewModel> MainWorkflow { get; }
    public BindableReactiveProperty<SubWorkflowDesignerViewModel?> SelectedWorkflow { get; }
    public BindableReactiveProperty<bool> IsZenMode { get; } = new(false);

    public BindableReactiveProperty<bool> IsWorkflowsPanelOpen { get; } = new(true);
    public BindableReactiveProperty<bool> IsPropertiesPanelOpen { get; } = new(false);

    public ApplicationSettingsViewModel ApplicationSettings { get; } = new();

    public CommandViewModel RunWorkflowCommand { get; }
    public CommandViewModel SaveChangesCommand { get; }
    public CommandViewModel ToggleZenModeCommand { get; }
    public CommandViewModel OpenSettingsCommand { get; }

    public ApplicationViewModel()
    {
        Workflows.AddRange(CreateDefaultWorkflows(ApplicationSettings));
        MainWorkflow = new(CreateMainWorkflow(ApplicationSettings));

        SelectedWorkflow = new(Workflows[0]);

        RunWorkflowCommand = new(new ReactiveCommand())
        {
            Label = { Value = "Run" },
            Icon = { Value = Icon.Play },
            IconVariant = { Value = IconVariant.Filled },
            IconColor = { Value = Colors.LawnGreen }
        };

        SaveChangesCommand = new(new ReactiveCommand())
        {
            Label = { Value = "Save" },
            Icon = { Value = Icon.Save },
            IconColor = { Value = Colors.LightSkyBlue }
        };

        ToggleZenModeCommand = new(new ReactiveCommand())
        {
            Label = { Value = "Zen" },
            Icon = { Value = Icon.ArrowMaximize }
        };

        ToggleZenModeCommand.Command.Subscribe(_ => IsZenMode.Value = !IsZenMode.Value);
        IsZenMode.Subscribe(isZen => IsWorkflowsPanelOpen.Value = !isZen);

        OpenSettingsCommand = new(new ReactiveCommand())
        {
            Icon = { Value = Icon.Settings },
            IconVariant = { Value = IconVariant.Filled },
        };

        SelectedWorkflow
            .SelectMany(workflow => workflow?.SelectedStep ?? Observable.Return<WorkflowStepViewModel?>(null))
            .Subscribe(selectedStep => IsPropertiesPanelOpen.Value = selectedStep != null);
    }

    private static IReadOnlyList<SubWorkflowDesignerViewModel> CreateDefaultWorkflows(ApplicationSettingsViewModel settings)
    {
        return [CreateRunTestsWorkflow(settings), CreateBuildLibraryWorkflow(settings), CreatePublishToNugetWorkflow(settings)];
    }

    private static SubWorkflowDesignerViewModel CreateRunTestsWorkflow(ApplicationSettingsViewModel settings)
    {
        var workflow = new SubWorkflowDesignerViewModel(settings)
        {
            Name = { Value = "Run tests" },
            ViewportPosition = { Value = new Point(-100, -200) }
        };

        var checkoutRepository = new WorkflowStepViewModel("Checkout repository")
        {
            Icon = { Value = Icon.BranchFork },
            IconColor = { Value = Colors.Orange },
            Position = { Value = new Point(50, 50) }
        };
        var setupDotNet = new WorkflowStepViewModel("Setup .NET")
        {
            Icon = { Value = Icon.Settings },
            IconColor = { Value = Colors.MediumPurple },
            Position = { Value = new Point(250, 120) }
        };
        var installDependencies = new WorkflowStepViewModel("Install dependencies")
        {
            Icon = { Value = Icon.ArrowDownload },
            IconColor = { Value = Colors.DodgerBlue },
            Position = { Value = new Point(450, 50) }
        };
        var buildSolution = new WorkflowStepViewModel("Build solution")
        {
            Icon = { Value = Icon.Building },
            IconColor = { Value = Colors.SteelBlue },
            Position = { Value = new Point(650, 120) }
        };
        var runUnitTests = new WorkflowStepViewModel("Run unit tests")
        {
            Icon = { Value = Icon.Beaker },
            IconColor = { Value = Colors.LimeGreen },
            Position = { Value = new Point(850, 50) }
        };
        var publishTestResults = new WorkflowStepViewModel("Publish test results")
        {
            Icon = { Value = Icon.ClipboardTask },
            IconColor = { Value = Colors.MediumSeaGreen },
            Position = { Value = new Point(1050, 120) }
        };

        workflow.Steps.Add(checkoutRepository);
        workflow.Steps.Add(setupDotNet);
        workflow.Steps.Add(installDependencies);
        workflow.Steps.Add(buildSolution);
        workflow.Steps.Add(runUnitTests);
        workflow.Steps.Add(publishTestResults);

        workflow.Connections.Add(new WorkflowStepConnectionViewModel(checkoutRepository, setupDotNet));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(setupDotNet, installDependencies));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(installDependencies, buildSolution));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(buildSolution, runUnitTests));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(runUnitTests, publishTestResults));

        return workflow;
    }

    private static SubWorkflowDesignerViewModel CreateBuildLibraryWorkflow(ApplicationSettingsViewModel settings)
    {
        var workflow = new SubWorkflowDesignerViewModel(settings)
        {
            Name = { Value = "Build library" },
            ViewportPosition = { Value = new Point(-100, -200) }
        };

        var checkoutRepository = new WorkflowStepViewModel("Checkout repository")
        {
            Icon = { Value = Icon.BranchFork },
            IconColor = { Value = Colors.Orange },
            Position = { Value = new Point(50, 50) }
        };
        var setupDotNet = new WorkflowStepViewModel("Setup .NET")
        {
            Icon = { Value = Icon.Settings },
            IconColor = { Value = Colors.MediumPurple },
            Position = { Value = new Point(250, 120) }
        };
        var installDependencies = new WorkflowStepViewModel("Install dependencies")
        {
            Icon = { Value = Icon.ArrowDownload },
            IconColor = { Value = Colors.DodgerBlue },
            Position = { Value = new Point(450, 50) }
        };
        var buildSolution = new WorkflowStepViewModel("Build solution")
        {
            Icon = { Value = Icon.Building },
            IconColor = { Value = Colors.SteelBlue },
            Position = { Value = new Point(650, 120) }
        };
        var runCodeAnalysis = new WorkflowStepViewModel("Run static code analysis")
        {
            Icon = { Value = Icon.Search },
            IconColor = { Value = Colors.Goldenrod },
            Position = { Value = new Point(850, 50) }
        };
        var publishArtifacts = new WorkflowStepViewModel("Publish build artifacts")
        {
            Icon = { Value = Icon.CloudArrowUp },
            IconColor = { Value = Colors.LightSkyBlue },
            Position = { Value = new Point(1050, 120) }
        };

        workflow.Steps.Add(checkoutRepository);
        workflow.Steps.Add(setupDotNet);
        workflow.Steps.Add(installDependencies);
        workflow.Steps.Add(buildSolution);
        workflow.Steps.Add(runCodeAnalysis);
        workflow.Steps.Add(publishArtifacts);

        workflow.Connections.Add(new WorkflowStepConnectionViewModel(checkoutRepository, setupDotNet));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(setupDotNet, installDependencies));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(installDependencies, buildSolution));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(buildSolution, runCodeAnalysis));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(runCodeAnalysis, publishArtifacts));

        return workflow;
    }

    private static SubWorkflowDesignerViewModel CreatePublishToNugetWorkflow(ApplicationSettingsViewModel settings)
    {
        var workflow = new SubWorkflowDesignerViewModel(settings)
        {
            Name = { Value = "Publish NuGet package" },
            ViewportPosition = { Value = new Point(-100, -200) }
        };

        var downloadArtifacts = new WorkflowStepViewModel("Download build artifacts")
        {
            Icon = { Value = Icon.CloudArrowDown },
            IconColor = { Value = Colors.LightSkyBlue },
            Position = { Value = new Point(50, 50) }
        };
        var packNuGet = new WorkflowStepViewModel("Pack NuGet package")
        {
            Icon = { Value = Icon.Box },
            IconColor = { Value = Colors.Coral },
            Position = { Value = new Point(250, 120) }
        };
        var publishToNuGet = new WorkflowStepViewModel("Publish to NuGet.org")
        {
            Icon = { Value = Icon.Rocket },
            IconColor = { Value = Colors.Tomato },
            Position = { Value = new Point(450, 50) }
        };

        workflow.Steps.Add(downloadArtifacts);
        workflow.Steps.Add(packNuGet);
        workflow.Steps.Add(publishToNuGet);

        workflow.Connections.Add(new WorkflowStepConnectionViewModel(downloadArtifacts, packNuGet));
        workflow.Connections.Add(new WorkflowStepConnectionViewModel(packNuGet, publishToNuGet));

        return workflow;
    }

    private static WorkflowDesignerViewModel CreateMainWorkflow(ApplicationSettingsViewModel settings)
    {
        var mainWorkflow = new WorkflowDesignerViewModel(settings)
        {
            Name = { Value = "Release pipeline" }
        };

        return mainWorkflow;
    }
}

internal sealed class ApplicationSettingsViewModel
{
    public EditorGestures EditorGestures { get; } = new EditorGestures();
}

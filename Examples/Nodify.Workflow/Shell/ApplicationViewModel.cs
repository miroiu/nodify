using FluentIcons.Common;
using Nodify.Workflow.Common;
using Nodify.Workflow.Designer;
using Nodify.Workflow.Settings;
using ObservableCollections;
using R3;
using System.Windows;
using System.Windows.Media;

namespace Nodify.Workflow.Shell;

internal sealed class ApplicationViewModel
{
    public string Title { get; } = "Workflow Designer";

    public ObservableList<SubWorkflowDesignerViewModel> Workflows { get; } = [];

    public BindableReactiveProperty<MainWorkflowDesignerViewModel> MainWorkflow { get; }
    public BindableReactiveProperty<SubWorkflowDesignerViewModel?> SelectedWorkflow { get; }
    public BindableReactiveProperty<bool> IsZenMode { get; } = new(false);

    public CommandViewModel RunWorkflowCommand { get; }
    public CommandViewModel SaveChangesCommand { get; }
    public ToggleCommandViewModel ToggleZenModeCommand { get; }
    public CommandViewModel OpenSettingsCommand { get; }

    public ApplicationViewModel()
    {
        MainWorkflow = new(new MainWorkflowDesignerViewModel());

        CreateDefaultWorkflows();

        SelectedWorkflow = new(Workflows[0]);

        RunWorkflowCommand = new(new ReactiveCommand())
        {
            Label = { Value = "Run" },
            ToolTip = { Value = "Run the main workflow" },
            Icon = { Value = Icon.Play },
            IconVariant = { Value = IconVariant.Filled },
            IconColor = { Value = Colors.LawnGreen }
        };

        SaveChangesCommand = new(new ReactiveCommand())
        {
            Label = { Value = "Save" },
            ToolTip = { Value = "Save all changes" },
            Icon = { Value = Icon.Save },
            IconColor = { Value = Colors.LightSkyBlue }
        };

        ToggleZenModeCommand = new(new ReactiveCommand())
        {
            Label = { Value = "Zen" },
            ToolTip = { Value = "Toggle zen mode" },
            Icon = { Value = Icon.ArrowMaximize },
            IconChecked = { Value = Icon.ArrowMinimize }
        };

        ToggleZenModeCommand.IsChecked.Subscribe(value => IsZenMode.Value = value);

        OpenSettingsCommand = new(new ReactiveCommand(OpenAppSettingsDialog))
        {
            Icon = { Value = Icon.Settings },
            IconVariant = { Value = IconVariant.Filled },
            ToolTip = { Value = "Open application settings" },
        };
    }

    private void OpenAppSettingsDialog(Unit unit)
    {
        var applicationSettings = new ApplicationSettingsViewModel(SelectedWorkflow.Value?.EditorGestures);

        var window = new ApplicationSettingsWindow
        {
            DataContext = applicationSettings,
            Owner = Application.Current.MainWindow
        };

        window.ShowDialog();
    }

    private void CreateDefaultWorkflows()
    {
        var runTestsWorkflow = CreateRunTestsWorkflow();
        var buildLibraryWorkflow = CreateBuildLibraryWorkflow();
        var publishToNugetWorkflow = CreatePublishToNugetWorkflow();

        Workflows.Add(runTestsWorkflow);
        Workflows.Add(buildLibraryWorkflow);
        Workflows.Add(publishToNugetWorkflow);

        foreach (var workflow in Workflows)
        {
            workflow.OnPostInitialize();
        }

        var runTestsWorkflowStep = new WorkflowStepViewModel(runTestsWorkflow.Name.Value)
        {
            IconColor = { Value = Colors.LimeGreen },
            Position = { Value = new Point(130, 150) }
        };

        var buildLibraryWorkflowStep = new WorkflowStepViewModel(buildLibraryWorkflow.Name.Value)
        {
            IconColor = { Value = Colors.SteelBlue },
            Position = { Value = new Point(120, 300) }
        };

        var publishToNugetWorkflowStep = new WorkflowStepViewModel(publishToNugetWorkflow.Name.Value)
        {
            IconColor = { Value = Colors.Tomato },
            Position = { Value = new Point(85, 450) }
        };

        MainWorkflow.Value.Steps.Add(runTestsWorkflowStep);
        MainWorkflow.Value.Steps.Add(buildLibraryWorkflowStep);
        MainWorkflow.Value.Steps.Add(publishToNugetWorkflowStep);

        MainWorkflow.Value.Connections.Add(new WorkflowStepConnectionViewModel(runTestsWorkflowStep, buildLibraryWorkflowStep));
        MainWorkflow.Value.Connections.Add(new WorkflowStepConnectionViewModel(buildLibraryWorkflowStep, publishToNugetWorkflowStep));
        MainWorkflow.Value.Name.Value = "Release pipeline";

        MainWorkflow.Value.OnPostInitialize();
    }

    private static SubWorkflowDesignerViewModel CreateRunTestsWorkflow()
    {
        var workflow = new SubWorkflowDesignerViewModel()
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

    private static SubWorkflowDesignerViewModel CreateBuildLibraryWorkflow()
    {
        var workflow = new SubWorkflowDesignerViewModel()
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

    private static SubWorkflowDesignerViewModel CreatePublishToNugetWorkflow()
    {
        var workflow = new SubWorkflowDesignerViewModel()
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
}

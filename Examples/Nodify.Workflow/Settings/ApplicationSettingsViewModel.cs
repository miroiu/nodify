using FluentIcons.Common;
using Nodify.Interactivity;
using Nodify.Workflow.Common;
using Nodify.Workflow.Navigation;
using ObservableCollections;
using R3;

namespace Nodify.Workflow.Settings;

internal sealed class ApplicationSettingsViewModel
{
    public EditorGestures? EditorGestures { get; }

    public NavigationService NavigationService { get; }

    public NavigationBreadcrumbsViewModel Breadcrumbs { get; }

    public ObservableList<SettingsEntryViewModel> SettingsList { get; } = [];

    public BindableReactiveProperty<SettingsEntryViewModel?> SelectedCategory { get; set; }

    public CommandViewModel NavigateBackCommand { get; }

    public ApplicationSettingsViewModel(EditorGestures? gestures)
    {
        EditorGestures = gestures;

        NavigationService = new NavigationService(routeKey =>
        {
            // TODO: Automatically register view models for routes and use DI to resolve them instead of hardcoding
            if (routeKey == KeybindingsSettingsViewModel.RouteKey)
            {
                return new KeybindingsSettingsViewModel(EditorGestures!, NavigationService!);
            }

            if (routeKey == EditorKeybindingsListViewModel.RouteKey)
            {
                return new EditorKeybindingsListViewModel(EditorGestures!);
            }

            return routeKey switch
            {
                "General" => new KeybindingsSettingsViewModel(EditorGestures!, NavigationService!),
                "Appearance" => new KeybindingsSettingsViewModel(EditorGestures!, NavigationService!),
                "About" => new KeybindingsSettingsViewModel(EditorGestures!, NavigationService!),
                _ => throw new NotImplementedException($"No view model registered for route key: {routeKey}")
            };
        });
        Breadcrumbs = new(NavigationService);

        SettingsList.Add(new SettingsEntryViewModel("General", Icon.Settings));
        if (EditorGestures != null)
        {
            SettingsList.Add(new SettingsEntryViewModel(KeybindingsSettingsViewModel.RouteKey, Icon.Keyboard));
        }
        SettingsList.Add(new SettingsEntryViewModel("Appearance", Icon.Color));
        SettingsList.Add(new SettingsEntryViewModel("About", Icon.Info));

        SelectedCategory = new(SettingsList[1]);

        SelectedCategory.Subscribe(category =>
        {
            if (category != null)
            {
                NavigationService.Navigate(category.Name.Value);
            }
        });

        NavigateBackCommand = new CommandViewModel(NavigationService.CanNavigateBack.ToReactiveCommand(_ => NavigationService.NavigateBack()))
        {
            Icon = { Value = Icon.ArrowLeft },
            ToolTip = { Value = "Back" }
        };
    }
}

internal class SettingsEntryViewModel(string name, Icon icon)
{
    public BindableReactiveProperty<string> Name { get; } = new(name);
    public BindableReactiveProperty<Icon> Icon { get; } = new(icon);
}

public interface INavigatable
{
    abstract static string RouteKey { get; }
}

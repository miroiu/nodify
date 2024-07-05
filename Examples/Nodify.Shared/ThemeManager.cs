using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public static class ThemeManager
    {
        private static string? _assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;

        private static readonly Dictionary<string, List<Uri>> _themesUris = new Dictionary<string, List<Uri>>();
        private static readonly Dictionary<string, List<ResourceDictionary>> _themesResources = new Dictionary<string, List<ResourceDictionary>>();

        private const string DEFAULT_THEME = "Nodify"; // This needs to be the one listed in App.xaml as preview, for correct switching behaviour
        private static readonly string[] BUILTIN_THEMES = new string[]
        {
            "Dark",
            "Light",
            DEFAULT_THEME,
        };

        public static string? ActiveTheme { get; private set; } = DEFAULT_THEME;

        private static readonly List<string> _availableThemes = new List<string>();
        public static IReadOnlyCollection<string> AvailableThemes => _availableThemes;

        public static ICommand SetNextThemeCommand { get; }

        static ThemeManager()
        {
            PreloadThemes();

            SetNextThemeCommand = new DelegateCommand(SetNextTheme);
        }

        public static bool PreloadThemes(string? assemblyName = null, string[]? themes = null)
        {
            bool success = true;
            themes ??= BUILTIN_THEMES;

            if (assemblyName != null)
            {
                _assemblyName = assemblyName;
            }

            foreach (var themeName in themes)
            {
                success &= PreloadTheme(themeName);
            }

            return success;
        }

        private static List<ResourceDictionary> FindExistingResources(List<Uri> uris)
        {
            var result = new List<ResourceDictionary>();
            foreach (var d in Application.Current.Resources.MergedDictionaries)
            {
                if (d.Source != null && uris.Contains(d.Source))
                {
                    result.Add(d);
                }
            }

            return result;
        }

        private static bool PreloadTheme(string themeName)
        {
            bool success = false;

            if (_themesUris.ContainsKey(themeName))
            {
                // Cleanup if that one was already loaded, might be reloaded from external assembly
                _themesUris.Remove(themeName);
                _themesResources.Remove(themeName);
                _availableThemes.Remove(themeName);
            }

            if (!_themesUris.TryGetValue(themeName, out var preload))
            {
                preload = new List<Uri>()
                {
                    // These are in application merged resource list and need to be reloaded as default fallback
                    new Uri($"pack://application:,,,/Nodify;component/Themes/{DEFAULT_THEME}.xaml"),
                    new Uri($"pack://application:,,,/Nodify.Shared;component/Themes/Icons.xaml"),
                    new Uri($"pack://application:,,,/Nodify.Shared;component/Themes/{DEFAULT_THEME}.xaml"),
                };

                // Actual theme if part of Nodify library
                if (themeName != DEFAULT_THEME)
                {
                    preload.Add(new Uri($"pack://application:,,,/Nodify;component/Themes/{themeName}.xaml"));
                    preload.Add(new Uri($"pack://application:,,,/Nodify.Shared;component/Themes/{themeName}.xaml"));
                };

                // Actual theme if in application (external/other assembly)
                if (_assemblyName != null)
                {
                    preload.Add(new Uri($"pack://application:,,,/{_assemblyName};component/Themes/{themeName}.xaml"));
                }
            }

            var resources = FindExistingResources(preload);

            for (int i = 0; i < preload.Count; i++)
            {
                try
                {
                    resources.Add(new ResourceDictionary
                    {
                        Source = preload[i]
                    });
                    success = true;
                }
                catch
                {
                    // Debug note: Exception is OK if main application does not contain that theme
                    //             The correct assembly can be set later through a seperate external PreloadThemes call.
                }
            }

            if (success)
            {
                _themesUris.Add(themeName, preload);
                _themesResources.Add(themeName, resources);
                _availableThemes.Add(themeName);

                if (ActiveTheme == null)
                {
                    ActiveTheme = themeName;
                }
            }

            return success;
        }

        public static void SetNextTheme()
        {
            if (ActiveTheme != null)
            {
                var i = _availableThemes.IndexOf(ActiveTheme);
                var next = i + 1 == _availableThemes.Count ? 0 : i + 1;

                SetTheme(_availableThemes[next]);
            }
            else if (_availableThemes.Count > 0)
            {
                SetTheme(_availableThemes[0]);
            }
        }

        public static void SetTheme(string themeName)
        {
            if (!_themesResources.ContainsKey(themeName))
            {
                PreloadTheme(themeName);
            }

            // Load new theme if it is valid
            if (_themesResources.TryGetValue(themeName, out var resources))
            {
                // Unload current theme
                if (ActiveTheme != null)
                {
                    foreach (var res in _themesResources[ActiveTheme])
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(res);

                        // Additionally search by Source:
                        // as Theme resources in samples placed explicitly in App.xaml for xaml preview purposes
                        // (however ThemeManager doesn't find those as already preloaded on startup,
                        // because MainWindow not initialized yet)
                        var duplicates = Application.Current.Resources.MergedDictionaries.Where(r => r.Source == res.Source).ToList();
                        foreach (var resFallback in duplicates)
                        {
                            Application.Current.Resources.MergedDictionaries.Remove(resFallback);
                        }
                    }
                }

                // Load new theme
                foreach (var res in resources)
                {
                    Application.Current.Resources.MergedDictionaries.Add(res);
                }

                ActiveTheme = themeName;
            }
        }
    }
}

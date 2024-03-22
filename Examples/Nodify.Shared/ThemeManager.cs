using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public static class ThemeManager
    {
        private static readonly string? _assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;

        private static readonly Dictionary<string, List<(Uri baseUri, Uri source)>> _themesUris = new Dictionary<string, List<(Uri, Uri)>>();
        private static readonly Dictionary<string, List<ResourceInclude>> _themesResources = new Dictionary<string, List<ResourceInclude>>();

        public static string? ActiveTheme { get; private set; }

        private static readonly List<string> _availableThemes = new List<string>();
        public static IReadOnlyCollection<string> AvailableThemes => _availableThemes;

        public static ICommand SetNextThemeCommand { get; }

        static ThemeManager()
        {
            PreloadTheme("Dark");
            PreloadTheme("Light");
            PreloadTheme("Nodify");

            SetNextThemeCommand = new DelegateCommand(SetNextTheme);
        }

        private static List<ResourceInclude> FindExistingResources(List<(Uri baseUri, Uri source)> uris)
        {
            var result = new List<ResourceInclude>();
            foreach (var d in Application.Current.Resources.MergedDictionaries)
            {
                if (d is ResourceInclude resourceInclude && uris.Any(x => x.source == resourceInclude.Source))
                {
                    result.Add(resourceInclude);
                }
            }

            return result;
        }

        private static void PreloadTheme(string themeName)
        {
            if (!_themesUris.TryGetValue(themeName, out var preload))
            {
                preload = new List<(Uri, Uri)>(2)
                {
                    (new Uri("resm:Resources?assembly=Nodify.Shared"), new Uri($"avares://Nodify.Shared/Themes/{themeName}.xaml")),
                    (new Uri("resm:Resources?assembly=Nodify"), new Uri($"avares://Nodify/Themes/{themeName}.xaml")),
                };
                if (_assemblyName != null)
                {
                    preload.Add((new Uri($"resm:Resources?assembly={_assemblyName}"), new Uri($"avares://{_assemblyName}/Themes/{themeName}.xaml")));
                }

                _themesUris.Add(themeName, preload);
            }

            var resources = FindExistingResources(preload);
            if (resources.Count == 0)
            {
                for (int i = 0; i < preload.Count; i++)
                {
                    try
                    {
                        resources.Add(new ResourceInclude(preload[i].baseUri)
                        {
                            Source = preload[i].source
                        });
                    }
                    catch
                    {

                    }
                }
            }
            else if (ActiveTheme == null)
            {
                ActiveTheme = themeName;
            }

            _themesResources.Add(themeName, resources);
            _availableThemes.Add(themeName);
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
                foreach (var res in resources)
                {
                    Application.Current.Resources.MergedDictionaries.Add(res);
                }

                // Unload current theme
                if (ActiveTheme != null)
                {
                    foreach (var res in _themesResources[ActiveTheme])
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(res);
                    }
                }

                ActiveTheme = themeName;
            }
            
            Application.Current.RequestedThemeVariant = themeName == "Light" ? ThemeVariant.Light : ThemeVariant.Dark;
        }
    }
}

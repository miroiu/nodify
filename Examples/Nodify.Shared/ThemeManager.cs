using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public class ThemeDictionary : ResourceDictionary
    {
        public string? ThemeName { get; set; }
    }

    public static class ThemeManager
    {
        public static string? ActiveTheme { get; private set; }
        public static HashSet<string> AvailableThemes { get; } = new HashSet<string>
        {
            "Dark",
            "Light"
        };

        public static ICommand SetNextThemeCommand { get; }

        static ThemeManager()
        {
            var themes = new Dictionary<Uri, string>();
            AvailableThemes.ForEach(themeName => themes.Add(new Uri($"pack://application:,,,/Nodify;component/Themes/{themeName}.xaml"), themeName));

            foreach (var d in Application.Current.Resources.MergedDictionaries)
            {
                if (themes.TryGetValue(d.Source, out var theme))
                {
                    ActiveTheme = theme;
                }
            }

            SetNextThemeCommand = new DelegateCommand(SetNextTheme);
        }

        public static void SetNextTheme()
        {
            if (ActiveTheme != null)
            {
                var themes = AvailableThemes.ToList();
                var i = themes.IndexOf(ActiveTheme);

                var next = i + 1 == themes.Count ? 0 : i + 1;

                SetTheme(themes[next]);
            }
            else if (AvailableThemes.Count > 0)
            {
                SetTheme(AvailableThemes.First());
            }
        }

        public static void SetTheme(string themeName)
        {
            if (ActiveTheme != null)
            {
                UnloadTheme(ActiveTheme);
            }

            LoadTheme(themeName);
        }

        private static void LoadTheme(string themeName)
        {
            var resourcesToLoad = new List<Uri>(3)
            {
                new Uri($"pack://application:,,,/Nodify;component/Themes/{themeName}.xaml"),
                new Uri($"pack://application:,,,/Nodify.Shared;component/Themes/{themeName}.xaml")
            };

            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            if (assemblyName != null)
            {
                resourcesToLoad.Add(new Uri($"pack://application:,,,/{assemblyName};component/Themes/{themeName}.xaml"));
            }

            foreach (var theme in resourcesToLoad)
            {
                var resource = new ThemeDictionary
                {
                    Source = theme,
                    ThemeName = themeName
                };

                Application.Current.Resources.MergedDictionaries.Add(resource);
            }

            ActiveTheme = themeName;
            AvailableThemes.Add(themeName);
        }

        private static void UnloadTheme(string themeName)
        {
            var themes = Application.Current.Resources.MergedDictionaries.ToList();

            foreach (var d in themes)
            {
                if ((d is ThemeDictionary t && t.ThemeName == themeName) || d.Source.LocalPath.Contains($"{themeName}.xaml"))
                {
                    Application.Current.Resources.MergedDictionaries.Remove(d);
                }
            }
        }
    }
}

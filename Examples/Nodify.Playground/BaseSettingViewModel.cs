using System;

namespace Nodify.Playground
{
    public class BaseSettingViewModel<T> : ObservableObject, ISettingViewModel
    {
        public string Name { get; }
        public string? Description { get; }

        private object? _value;

        object? ISettingViewModel.Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public SettingsType Type { get;}

        public T Value
        {
            get => (T)((ISettingViewModel)this).Value!;
            set => ((ISettingViewModel)this).Value = value;
        }

        public BaseSettingViewModel(string name, string? description = default)
        {
            Name = name;
            Description = description;
            Type = typeof(T) switch
            {
                { } t when t == typeof(string) => SettingsType.Text,
                { } t when t == typeof(bool) => SettingsType.Boolean,
                { } t when t == typeof(uint) || t == typeof(double) => SettingsType.Number,
                { } t when t == typeof(PointEditor) => SettingsType.Point,
                { IsEnum: true } => SettingsType.Option,
                _ => throw new InvalidOperationException($"Type {typeof(T).Name} does not have a matching {nameof(SettingsType)}.")
            };
        }
    }
}
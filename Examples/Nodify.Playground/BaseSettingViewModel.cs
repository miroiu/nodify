namespace Nodify.Playground
{
    public class BaseSettingViewModel<T> : ObservableObject, ISettingViewModel
    {
        public string Name { get; }
        public string? Description { get; } // This is the tooltip

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
            if (typeof(T) == typeof(bool))
                Type = SettingsType.Boolean;
            else if (typeof(T) == typeof(double) || typeof(T) == typeof(uint))
                Type = SettingsType.Number;
            else if (typeof(T) == typeof(PointEditor))
                Type = SettingsType.Point;
            else
                Type = SettingsType.Option;
        }
    }
}
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

        public SettingsType Type { get; set; }

        public T Value
        {
            get => ((ISettingViewModel)this).Value is T val ? val : default!;
            set => ((ISettingViewModel)this).Value = value;
        }

        public BaseSettingViewModel(string name, SettingsType type, string? description = default)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
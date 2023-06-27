using System;

namespace Nodify.Playground
{
    public class ProxySettingViewModel<T> : BaseSettingViewModel<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;

        public ProxySettingViewModel(Func<T> getter, Action<T> setter, string name, string? description = default)
            : base(name, description)
        {
            _getter = getter;
            _setter = setter;
        }

        public new T Value
        {
            get => _getter();
            set => _setter(value);
        }
    }
}
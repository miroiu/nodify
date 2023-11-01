using System;

namespace Nodifier
{
    public class ValueOutput<T> : BaseConnector
    {
        public ValueOutput(INodeWidget node) : base(node)
        {
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }

        private T _value = default!;
        public T Value
        {
            get => _value;
            set
            {
                if (SetAndNotify(ref _value, value))
                {
                    ValueChanged?.Invoke(_value);
                }
            }
        }

        public Action<T>? ValueChanged;
    }
}

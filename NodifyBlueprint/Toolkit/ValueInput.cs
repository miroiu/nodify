using Stylet;

namespace NodifyBlueprint
{
    public class ValueEditor<T> : PropertyChangedBase
    {
        private T _value;
        public T Value
        {
            get => _value;
            set => SetAndNotify(ref _value, value);
        }
    }

    public class ValueInput<T> : BaseConnector, IInputConnector
    {
        public ValueInput(IGraphNode node) : base(node)
        {
        }

        public ValueEditor<T> ValueEditor { get; } = new ValueEditor<T>();

        public T Value
        {
            get => ValueEditor.Value;
            set => ValueEditor.Value = value;
        }
    }
}

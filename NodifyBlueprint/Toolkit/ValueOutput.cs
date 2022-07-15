namespace NodifyBlueprint
{
    public class ValueOutput<T> : BaseConnector
    {
        public ValueOutput(IGraphNode node) : base(node)
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
            set => SetAndNotify(ref _value, value);
        }
    }
}

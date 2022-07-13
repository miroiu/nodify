namespace NodifyBlueprint
{
    public class ValueOutput<T> : BlueprintConnector, IOutputConnector
    {
        public ValueOutput(IGraphNode node) : base(node)
        {
        }
        
        private T _value = default!;
        public T Value
        {
            get => _value;
            set => SetAndNotify(ref _value, value);
        }
    }
}

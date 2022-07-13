namespace NodifyBlueprint
{
    public static class BlueprintNodeExtensions
    {
        public static ValueInput<T> AddValueInput<T>(this IGraphNode node, string? title = default, T value = default!)
        {
            var input = new ValueInput<T>(node)
            {
                Title = title,
                Value = value
            };

            node.AddInput(input);
            return input;
        }
        
        public static ValueOutput<T> AddValueOutput<T>(this IGraphNode node, string? title = default, T value = default!)
        {
            var output = new ValueOutput<T>(node)
            {
                Title = title,
                Value = value
            };

            node.AddOutput(output);
            return output;
        }
    }
}

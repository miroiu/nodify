using Nodifier.Blueprint;

namespace Nodifier
{
    public static class BlueprintExtensions
    {
        public static ValueInput<T> AddValueInput<T>(this BPNode node, string? title = default, T value = default!)
        {
            var input = new ValueInput<T>(node.Widget)
            {
                Title = title,
                Value = value
            };

            node.Widget.AddInput(input);
            return input;
        }
        
        public static ValueOutput<T> AddValueOutput<T>(this BPNode node, string? title = default, T value = default!)
        {
            var output = new ValueOutput<T>(node.Widget)
            {
                Title = title,
                Value = value
            };

            node.Widget.AddOutput(output);
            return output;
        }

        public static FlowInput AddFlowInput(this BPNode node, string? title = default)
        {
            var input = new FlowInput(node.Widget) { Title = title };
            node.Widget.AddInput(input);
            return input;
        }

        public static FlowOutput AddFlowOutput(this BPNode node, string? title = default)
        {
            var output = new FlowOutput(node.Widget) { Title = title };
            node.Widget.AddOutput(output);
            return output;
        }
    }
}

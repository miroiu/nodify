using Microsoft.Extensions.Logging;
using Nodifier;
using Nodifier.Blueprint;

namespace Nodify.MinimalExample
{
    public class ForLoopNode : BPNode
    {
        public FlowInput In { get; }
        public FlowOutput Body { get; }
        public FlowOutput Completed { get; }
        public ValueInput<int> FirstIndex { get; }
        public ValueInput<int> LastIndex { get; }
        public ValueInput<int> Step { get; }
        public ValueOutput<int> Index { get; }

        public ForLoopNode(BPGraph graph, ILogger<ForLoopNode> logger) : base(graph)
        {
            Widget.Footer = "Custom Graph Node";
            Widget.Header = "Header ";

            In = this.AddFlowInput();

            FirstIndex = this.AddValueInput<int>("First Index");
            LastIndex = this.AddValueInput<int>("Last Index");
            Step = this.AddValueInput<int>("Step");

            Body = this.AddFlowOutput("Body");
            Index = this.AddValueOutput<int>("Index");
            Completed = this.AddFlowOutput("Completed");
        }
    }

    public class MinimalApp
    {
        public IBlueprintGraph Graph { get; }

        public MinimalApp(IGraphFactory graphFactory)
        {
            Graph = graphFactory.Get<IBlueprintGraph>();

            Graph.History.IsEnabled = false;

            //Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(100, 50) });
            //Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(200, 150) });
            //Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(100, 250) });
            //Editor.AddComment("Generated comment", Editor.Elements);

            Graph.AddNode<ForLoopNode>();

            Graph.History.IsEnabled = true;
        }

        public void Undo() => Graph.History.Undo();
        public void Redo() => Graph.History.Redo();
    }
}

using Microsoft.Extensions.Logging;
using Nodifier;
using Nodifier.Blueprint;

namespace Nodify.MinimalExample
{
    public class ForLoopSnapshot : INodeSnapshot
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsSelected { get; set; }
    }

    public class ForLoopNode : BPNode<ForLoopSnapshot>
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

            In = AddFlowInput();

            FirstIndex = AddValueInput<int>("First Index");
            LastIndex = AddValueInput<int>("Last Index");
            Step = AddValueInput<int>("Step");

            Body = AddFlowOutput("Body");
            Index = AddValueOutput<int>("Index");
            Completed = AddFlowOutput("Completed");
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
            var node = new NodeWidget(Graph.Widget);
            node.Content = "ADD";
            node.AddInput(new BaseConnector(node));
            node.AddInput(new BaseConnector(node));
            node.AddOutput(new BaseConnector(node));
            Graph.Widget.AddElement(node);

            Graph.History.IsEnabled = true;
        }

        public void Undo() => Graph.History.Undo();
        public void Redo() => Graph.History.Redo();
    }
}

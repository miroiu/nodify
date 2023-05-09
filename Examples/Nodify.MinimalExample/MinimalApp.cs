using Microsoft.Extensions.Logging;
using Nodifier;
using Nodifier.Blueprint;
using System.Text.Json;
using System.Windows;

namespace Nodify.MinimalExample
{
    // TODO: AddValueInput is not working for non generic BPNode
    public class ToStringNode : BPNode<BPNodeSnapshot>
    {
        public ValueInput<object> In { get; set; }
        public ValueOutput<string> Out { get; set; }

        public ToStringNode(IBlueprintGraph graph) : base(graph)
        {
            In = AddValueInput<object>();
            Out = AddValueOutput<string>();

            Widget.Content = "To String";
        }
    }


    public class ForLoopSnapshot : BPNodeSnapshot
    {
        public string? Header { get; set; }
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

        // Dependency injection is possible
        public ForLoopNode(IBlueprintGraph graph, ILogger<ForLoopNode> logger) : base(graph)
        {
            Widget.Header = "For Loop";

            In = AddFlowInput();

            FirstIndex = AddValueInput<int>("First Index");
            LastIndex = AddValueInput<int>("Last Index");
            Step = AddValueInput<int>("Step");

            Body = AddFlowOutput("Body");
            Index = AddValueOutput<int>("Index");
            Completed = AddFlowOutput("Completed");
        }

        protected override void OnSnapshotCreating(ForLoopSnapshot snapshot)
        {
            base.OnSnapshotCreating(snapshot);

            snapshot.Header = Widget.Header?.ToString();
        }
    }

    public class MinimalApp : PropertyChangedBase
    {
        private readonly IGraphFactory _graphFactory;

        private IBlueprintGraph _graph;
        public IBlueprintGraph Graph
        {
            get => _graph;
            set => SetAndNotify(ref _graph, value);
        }

        public MinimalApp(IGraphFactory graphFactory)
        {
            Graph = graphFactory.Get<IBlueprintGraph>();

            Graph.History.IsEnabled = false;

            //Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(100, 50) });
            //Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(200, 150) });
            //Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(100, 250) });
            //Editor.AddComment("Generated comment", Editor.Elements);

            var forLoop = Graph.AddNode<ForLoopNode>();
            var forLoop2 = Graph.AddNode<ForLoopNode>();
            forLoop2.Widget.Location = new Point(250, 100);

            Graph.AddNode<ToStringNode>().Widget.Location = new Point(250, 50);
            //var node = new NodeWidget(Graph.Widget)
            //{
            //    Content = "ADD",
            //    Location = new Point(300, 300)
            //};
            //node.AddInput(new BaseConnector(node));
            //node.AddInput(new BaseConnector(node));
            //node.AddOutput(new BaseConnector(node));
            //Graph.Widget.AddElement(node);

            Graph.History.IsEnabled = true;
            _graphFactory = graphFactory;
        }

        public void Undo() => Graph.History.Undo();
        public void Redo() => Graph.History.Redo();

        public void Copy() => BPClipboard.CopySelection(Graph);
        public void Paste() => BPClipboard.PasteSelection(Graph);

        public void SaveAndReload()
        {
            var saveSnapshot = ((IGraphMemento)Graph).CreateSnapshot();
            var result = JsonSerializer.Serialize(saveSnapshot);

            var restoreSnapshot = JsonSerializer.Deserialize<GraphSnapshot>(result)!;

            Graph = _graphFactory.Get<BPGraph>();
            ((IGraphMemento)Graph).RestoreSnapshot(restoreSnapshot);
        }
    }
}

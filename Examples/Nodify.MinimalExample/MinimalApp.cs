using Microsoft.Extensions.Logging;
using Nodifier;
using System;
using System.Windows;

namespace Nodify.MinimalExample
{
    public class CustomGraphNode : GraphNode.WithFooter<string>
    {
        public ValueInput<bool> In { get; }
        public ValueOutput<double> Out { get; }

        public CustomGraphNode(IGraph graph) : base(graph)
        {
            Footer = "Custom Graph Node";

            In = this.AddValueInput<bool>("Boolean");
            this.AddValueInput<bool>("Boolean 2");
            Out = this.AddValueOutput<double>("Double");
        }
    }

    public class MinimalApp
    {
        private readonly ILogger<MinimalApp> _logger;

        public IGraph Editor { get; }

        public MinimalApp(ILogger<MinimalApp> logger, Func<IGraph> createEditor)
        {
            Editor = createEditor();
            _logger = logger;


            Editor.History.IsEnabled = false;
            
            Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(100, 50) });
            Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(200, 150) });
            Editor.AddElement(new CustomGraphNode(Editor) { Location = new Point(100, 250) });
            Editor.AddComment("Generated comment", Editor.Elements);

            Editor.History.IsEnabled = true;
        }

        public void Undo() => Editor.History.Undo();
        public void Redo() => Editor.History.Redo();
    }
}

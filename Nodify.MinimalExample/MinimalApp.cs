using NodifyBlueprint;
using System;
using System.Linq;
using System.Windows;

namespace NodifyMinimalExample
{
    public class SubtractNode : GraphNode
        .WithContent<string>
        .WithFooter<string>
    {
        public ValueInput<int> A { get; }
        public ValueInput<int> B { get; }
        public ValueOutput<int> Out { get; }

        public SubtractNode(IGraph graph) : base(graph)
        {
            Content = "This is the content";
            Footer = "This is the footer";
            
            A = this.AddValueInput<int>("A");
            B = this.AddValueInput<int>("B");

            Out = this.AddValueOutput<int>();
        }
    }

    public class ToStringNode : GraphNode
        .WithHeader<string>
    {
        public ValueInput<object> In { get; }
        public ValueOutput<string> Out { get; }

        public ToStringNode(IGraph graph) : base(graph)
        {
            Header = "This is the header";

            In = this.AddValueInput<object>();
            Out = this.AddValueOutput<string>();
        }
    }

    public class MinimalApp
    {

        public IGraph Graph { get; } = new Graph();
        private static Random _random = new Random();

        public MinimalApp()
        {
            // ALT + Click on a connector to disconnect

            var primeNode = new GraphNode.WithContent<string>(Graph)
            {
                Location = new Point(100, 250),
                Content = "True"
            };
            primeNode.AddInput(new BaseConnector(primeNode));
            primeNode.AddOutput(new BaseConnector(primeNode));

            var subtractNode = new SubtractNode(Graph) { Location = new Point(100, 100) };
            var stringNode = new ToStringNode(Graph) { Location = new Point(250, 100) };

            var R = subtractNode.AddValueInput<bool>("R");
            subtractNode.AddValueInput(value: 15.3);

            Graph.AddElement(primeNode);
            Graph.AddElement(stringNode);
            Graph.AddElement(subtractNode);

            Graph.TryConnect(subtractNode.Out, stringNode.In);
            Graph.TryConnect(R, primeNode);
        }

        public void AddComment()
        {
            Graph.AddComment("Well, this is a comment sorounding all nodes", Graph.Elements);
        }

        public void FocusRandomNode()
        {
            int nodeIndex = _random.Next(Graph.Elements.Count);
            IGraphElement elem = Graph.Elements.ElementAt(nodeIndex);

            Graph.FocusNode(elem);
        }
    }
}

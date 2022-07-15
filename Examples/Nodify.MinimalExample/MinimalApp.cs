using Nodifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NodifyMinimalExample
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

    /// 
    ///  ALT + Click on a connector to disconnect
    ///  ALT + Click on a connection to disconnect
    ///  Double Click on a connection to split
    ///  
    ///  CTRL + Plus to zoom in
    ///  CTRL + Minus to zoom out
    ///  CTRL + A to select all
    /// 

    public class MinimalApp
    {
        public Graph Graph { get; } = new Graph();
        private static readonly Random _random = new Random();

        public MinimalApp()
        {
            SetupBaseNodes();
            SetupAdvancedNodes();
        }

        private void SetupAdvancedNodes()
        {
            var toString = new GraphNode.WithHeader<string>(Graph)
            {
                Location = new Point(100, 200),
                Header = "To String"
            };
            toString.AddValueInput<object>();
            toString.AddValueOutput<string>();

            var add = new GraphNode.WithContent<string>(Graph)
            {
                Location = new Point(300, 250),
                Content = "ADD"
            };

            var sub = new GraphNode.WithContent<string>.WithFooter<string>(Graph)
            {
                Location = new Point(400, 250),
                Content = "SUB",
                Footer = "Sub footer"
            };

            add.AddValueInput<int>("A");
            add.AddValueInput<int>("B", 9999);
            add.AddValueOutput<int>();

            var custom = new CustomGraphNode(Graph)
            {
                Location = new Point(100, 300)
            };

            Graph.AddElement(toString);
            Graph.AddElement(add);
            Graph.AddElement(custom);
            Graph.AddElement(sub);

            toString.TryConnect(add);
            custom.Out.TryConnectTo(add);

            Graph.AddComment("Generated comment", new List<IGraphElement> { add, sub });
        }

        private void SetupBaseNodes()
        {
            var toString = new GraphNode.WithHeader<string>(Graph)
            {
                Location = new Point(100, 100),
                Header = "To String"
            };
            toString.AddInput(new BaseConnector(toString));
            toString.AddOutput(new BaseConnector(toString));

            var add = new GraphNode.WithContent<string>(Graph)
            {
                Location = new Point(300, 100),
                Content = "ADD"
            };

            add.AddInput(new BaseConnector(add));
            add.AddInput(new BaseConnector(add));
            add.AddOutput(new BaseConnector(add));

            Graph.AddElement(toString);
            Graph.AddElement(add);

            if (toString.TryConnect(add))
            {
                IConnection con = Graph.Connections.First();
                con.Split(new Point(200, 150));
            }
        }

        public void AlignSelection()
        {
            Graph.AlignSelection(Alignment.Top);
        }

        public void AddComment()
        {
            Graph.AddComment("Well, this is a comment sorounding all nodes", Graph.Elements);
        }

        public void FocusRandomNode()
        {
            int nodeIndex = _random.Next(Graph.Elements.Count);
            IGraphElement elem = Graph.Elements.ElementAt(nodeIndex);

            Graph.FocusElement(elem);
        }
    }
}

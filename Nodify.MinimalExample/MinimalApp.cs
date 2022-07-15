using Nodifier;
using System;
using System.Linq;
using System.Windows;

namespace NodifyMinimalExample
{
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
            var toString = new GraphNode.WithHeader<string>(Graph)
            {
                Location = new Point(100, 250),
                Header = "To String"
            };
            toString.AddInput(new BaseConnector(toString));
            toString.AddOutput(new BaseConnector(toString));
            
            var add = new GraphNode.WithContent<string>(Graph)
            {
                Location = new Point(300, 250),
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

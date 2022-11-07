using Microsoft.Extensions.Logging;
using Nodifier;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.MinimalExample
{
    public class MinimalApp
    {
        private readonly ILogger<MinimalApp> _logger;

        public IGraphEditor Editor { get; }

        public MinimalApp(ILogger<MinimalApp> logger, Func<IGraphEditor> createEditor)
        {
            Editor = createEditor();
            _logger = logger;
            
            Editor.History.IsEnabled = false;

            var view = new Item("myText");
            var viewModel = new ItemViewModel(view);
            view.DataContext = viewModel;
            
            var node = new GraphNode(Editor)
            {
                Location = new Point(100, 100),
                Header = "Hello World",
                Footer = view,
                Content = "This is a content"
            };
            
            node.AddOutput(new BaseConnector(node));
            node.AddOutput(new BaseConnector(node));


            
            var node2 = new GraphNode(Editor)
            {
                Location = new Point(200, 200),
                Header = "Hello World",
                Footer = "This is a footer",
                Content = "This is a content"
            };
            node2.AddInput(new BaseConnector(node2));
            node2.AddInput(new BaseConnector(node2));

            
            Editor.AddElement(node);
            Editor.AddElement(node2);
            
            Editor.History.IsEnabled = true;
        }

        public void Undo() => Editor.History.Undo();
        public void Redo() => Editor.History.Redo();
    }
}

using Stylet;
using System;
using System.Collections.Generic;

namespace NodifyBlueprint
{
    public partial class GraphNode : GraphElement, IGraphNode
    {
        private readonly BindableCollection<IConnector> _input = new BindableCollection<IConnector>();
        public IReadOnlyCollection<IConnector> Input => _input;

        private readonly BindableCollection<IConnector> _output = new BindableCollection<IConnector>();
        public IReadOnlyCollection<IConnector> Output => _output;
        
        private object? _content = default!;
        public object? Content
        {
            get => _content;
            set => SetAndNotify(ref _content, value);
        }

        private object? _footer = default!;
        public object? Footer
        {
            get => _footer;
            set => SetAndNotify(ref _footer, value);
        }

        private object? _header = default!;
        public object? Header
        {
            get => _header;
            set => SetAndNotify(ref _header, value);
        }

        public GraphNode(IGraph graph) : base(graph)
        {
        }

        public void AddInput(IConnector input)
        {
            if (!_input.Contains(input))
            {
                _input.Add(input);
            }
            else
            {
                throw new InvalidOperationException("Input already exists");
            }
        }

        public void AddOutput(IConnector output)
        {
            if (!_output.Contains(output))
            {
                _output.Add(output);
            }
            else
            {
                throw new InvalidOperationException("Output already exists");
            }
        }
    }
}

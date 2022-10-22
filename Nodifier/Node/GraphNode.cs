using System.Collections.Generic;

namespace Nodifier
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

        public GraphNode(IGraphEditor graph) : base(graph)
        {
            RecordProperty(nameof(Content));
            RecordProperty(nameof(Footer));
            RecordProperty(nameof(Header));
        }

        public void AddInput(IConnector input)
        {
            if (_input.Contains(input))
            {
                throw new GraphException("Input already exists.");
            }

            _input.Add(input);
            History.Record(() => AddInput(input), () => RemoveInput(input), nameof(AddInput));
        }

        public void RemoveInput(IConnector input)
        {
            if (!_input.Remove(input))
            {
                throw new GraphException("Input does not exist.");
            }

            History.Record(() => RemoveInput(input), () => AddInput(input), nameof(RemoveInput));
        }

        public void AddOutput(IConnector output)
        {
            if (_output.Contains(output))
            {
                throw new GraphException("Output already exists.");
            }

            _output.Add(output);
            History.Record(() => AddOutput(output), () => RemoveOutput(output), nameof(AddOutput));
        }

        public void RemoveOutput(IConnector output)
        {
            if (!_output.Remove(output))
            {
                throw new GraphException("Input does not exist.");
            }

            History.Record(() => RemoveOutput(output), () => AddOutput(output), nameof(RemoveOutput));
        }

        public void Disconnect()
        {
            using (History.Batch(nameof(Disconnect)))
            {
                foreach (var input in _input)
                {
                    input.Disconnect();
                }
                foreach (var output in _output)
                {
                    output.Disconnect();
                }
            }
        }
    }
}

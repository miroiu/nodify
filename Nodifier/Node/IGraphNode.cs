using System.Collections.Generic;

namespace Nodifier
{
    public interface IGraphNode : IGraphElement
    {
        object? Content { get; set; }
        object? Footer { get; set; }
        object? Header { get; set; }

        IReadOnlyCollection<IConnector> Input { get; }
        IReadOnlyCollection<IConnector> Output { get; }

        void AddInput(IConnector input);
        void AddOutput(IConnector output);
    }
}

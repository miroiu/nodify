using System.Collections.Generic;

namespace Nodifier
{
    public interface ICanDisconnect
    {
        void Disconnect();
    }

    public interface IGraphNode : IGraphElement, ICanDisconnect
    {
        object? Content { get; set; }
        object? Footer { get; set; }
        object? Header { get; set; }

        IReadOnlyCollection<IConnector> Input { get; }
        IReadOnlyCollection<IConnector> Output { get; }

        void AddInput(IConnector input);
        void RemoveInput(IConnector input);

        void AddOutput(IConnector output);
        void RemoveOutput(IConnector output);
    }
}

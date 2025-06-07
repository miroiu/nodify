using System.Collections.Generic;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public class KeyboardNavigationLayerId
    {
        public static readonly KeyboardNavigationLayerId Nodes = new KeyboardNavigationLayerId();
        public static readonly KeyboardNavigationLayerId Connections = new KeyboardNavigationLayerId();
    }

    public interface INavigationLayerGroup : IReadOnlyCollection<IKeyboardNavigationLayer>
    {
        IKeyboardNavigationLayer ActiveLayer { get; }

        void MoveToNextLayer();

        void MoveToPrevLayer();

        void RegisterLayer(IKeyboardNavigationLayer layer);

        void RemoveLayer(KeyboardNavigationLayerId layerId);
    }

    public interface IKeyboardNavigationLayer
    {
        KeyboardNavigationLayerId Id { get; }

        bool TryMoveFocus(TraversalRequest request);

        bool IsActiveLayer { get; }
    }
}

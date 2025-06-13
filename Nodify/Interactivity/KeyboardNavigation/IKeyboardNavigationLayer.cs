using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public class KeyboardNavigationLayerId
    {
        public static readonly KeyboardNavigationLayerId Nodes = new KeyboardNavigationLayerId();
        public static readonly KeyboardNavigationLayerId Connections = new KeyboardNavigationLayerId();
        public static readonly KeyboardNavigationLayerId Decorators = new KeyboardNavigationLayerId();
    }

    public interface IKeyboardNavigationLayerGroup : IReadOnlyCollection<IKeyboardNavigationLayer>
    {
        IKeyboardNavigationLayer? ActiveLayer { get; }

        event Action<KeyboardNavigationLayerId>? ActiveLayerChanged;

        bool MoveToNextLayer();

        bool MoveToPrevLayer();

        bool RegisterLayer(IKeyboardNavigationLayer layer);

        bool RemoveLayer(KeyboardNavigationLayerId layerId);

        bool ActivateLayer(KeyboardNavigationLayerId layerId);
    }

    public interface IKeyboardNavigationLayer
    {
        KeyboardNavigationLayerId Id { get; }
        object? LastFocusedElement { get; }

        bool TryMoveFocus(TraversalRequest request);
        bool TryRestoreFocus();

        void OnActivate();
        void OnDeactivate();
    }

    public interface IKeyboardFocusTarget<TElement>
        where TElement : UIElement
    {
        Rect Bounds { get; }
        TElement Element { get; }
    }
}

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
        IKeyboardNavigationLayer? ActiveNavigationLayer { get; }

        event Action<KeyboardNavigationLayerId>? ActiveNavigationLayerChanged;

        bool ActivateNextNavigationLayer();

        bool ActivatePreviousNavigationLayer();

        bool RegisterNavigationLayer(IKeyboardNavigationLayer layer);

        bool RemoveNavigationLayer(KeyboardNavigationLayerId layerId);

        bool ActivateNavigationLayer(KeyboardNavigationLayerId layerId);
    }

    public interface IKeyboardNavigationLayer
    {
        KeyboardNavigationLayerId Id { get; }
        IKeyboardFocusTarget<UIElement>? LastFocusedElement { get; }

        bool TryMoveFocus(TraversalRequest request);
        bool TryRestoreFocus();

        void OnActivated();
        void OnDeactivated();
    }

    public interface IKeyboardFocusTarget<out TElement>
        where TElement : UIElement
    {
        Rect Bounds { get; }
        TElement Element { get; }
    }
}

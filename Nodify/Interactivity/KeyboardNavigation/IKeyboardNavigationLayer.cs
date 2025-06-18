using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Represents a unique identifier for a keyboard navigation layer.
    /// </summary>
    public class KeyboardNavigationLayerId
    {
        public static readonly KeyboardNavigationLayerId Nodes = new KeyboardNavigationLayerId();
        public static readonly KeyboardNavigationLayerId Connections = new KeyboardNavigationLayerId();
        public static readonly KeyboardNavigationLayerId Decorators = new KeyboardNavigationLayerId();
    }

    /// <summary>
    /// Represents a group of keyboard navigation layers that can be activated and navigated through.
    /// </summary>
    public interface IKeyboardNavigationLayerGroup : IReadOnlyCollection<IKeyboardNavigationLayer>
    {
        /// <summary>
        /// The current active keyboard navigation layer in the group, if any.
        /// </summary>
        IKeyboardNavigationLayer? ActiveNavigationLayer { get; }

        /// <summary>
        /// Event that is raised when the active keyboard navigation layer changes.
        /// </summary>
        event Action<KeyboardNavigationLayerId>? ActiveNavigationLayerChanged;

        /// <summary>
        /// Activates the next keyboard navigation layer in the group, allowing focus to be restored to the last focused element in that layer.
        /// </summary>
        /// <returns>Returns true if the navigation layer was activated, false otherwise.</returns>
        bool ActivateNextNavigationLayer();

        /// <summary>
        /// Activates the previous keyboard navigation layer in the group, allowing focus to be restored to the last focused element in that layer.
        /// </summary>
        /// <returns>Returns true if the navigation layer was activated, false otherwise.</returns>
        bool ActivatePreviousNavigationLayer();

        /// <summary>
        /// Registers a new keyboard navigation layer to the group, allowing it to handle focus movement and restoration.
        /// </summary>
        /// <param name="layer">The navigation layer.</param>
        /// <returns></returns>
        bool RegisterNavigationLayer(IKeyboardNavigationLayer layer);

        /// <summary>
        /// Removes the specified keyboard navigation layer from the group.
        /// </summary>
        /// <param name="layerId">The navigation layer id.</param>
        /// <returns>Returns true if the layer was removed, false otherwise.</returns>
        bool RemoveNavigationLayer(KeyboardNavigationLayerId layerId);

        /// <summary>
        /// Activates the specified keyboard navigation layer, making it the active layer for focus management.
        /// </summary>
        /// <param name="layerId">The navigation layer id to activate.</param>
        /// <returns>Returns true if the navigation layer was activated, false otherwise.</returns>
        bool ActivateNavigationLayer(KeyboardNavigationLayerId layerId);
    }

    /// <summary>
    /// Represents a layer of keyboard navigation that can handle focus movement and restoration.
    /// </summary>
    public interface IKeyboardNavigationLayer
    {
        /// <summary>
        /// Gets the unique identifier for this keyboard navigation layer.
        /// </summary>
        KeyboardNavigationLayerId Id { get; }

        /// <summary>
        /// Gets the last focused element within this layer, if any.
        /// </summary>
        IKeyboardFocusTarget<UIElement>? LastFocusedElement { get; }

        /// <summary>
        /// Attempts to move focus within this layer based on the provided traversal request.
        /// </summary>
        /// <param name="request">The traversal request.</param>
        /// <returns>Returns true if the focus was moved, false otherwise.</returns>
        bool TryMoveFocus(TraversalRequest request);

        /// <summary>
        /// Attempts to restore focus to the last focused element within this layer.
        /// </summary>
        /// <returns>Returns true if the focus was restored, false otherwise.</returns>
        bool TryRestoreFocus();

        /// <summary>
        /// Called when the layer is activated, allowing for any necessary setup or focus management.
        /// </summary>
        void OnActivated();

        /// <summary>
        /// Called when the layer is deactivated, allowing for any necessary cleanup or focus management.
        /// </summary>
        void OnDeactivated();
    }

    /// <summary>
    /// Represents a target for keyboard focus within a specific layer, providing bounds and the associated UI element.
    /// </summary>
    /// <typeparam name="TElement">The associated UI element.</typeparam>
    public interface IKeyboardFocusTarget<out TElement>
        where TElement : UIElement
    {
        /// <summary>
        /// Gets the bounds of the focus target within the layer.
        /// </summary>
        Rect Bounds { get; }

        /// <summary>
        /// Gets the associated UI element for this focus target.
        /// </summary>
        TElement Element { get; }
    }
}

using Nodify.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Collections;
using System.Diagnostics;

namespace Nodify
{
    public partial class NodifyEditor : IKeyboardNavigationLayer, IKeyboardNavigationLayerGroup
    {
        private readonly List<IKeyboardNavigationLayer> _navigationLayers = new List<IKeyboardNavigationLayer>();
        private IKeyboardNavigationLayer? _activeKeyboardNavigationLayer;
        private IKeyboardNavigationLayer KeyboardNavigationLayer => this;
        private IKeyboardNavigationLayerGroup KeyboardNavigationLayerGroup => this;

        IKeyboardNavigationLayer? IKeyboardNavigationLayerGroup.ActiveLayer => _activeKeyboardNavigationLayer;

        KeyboardNavigationLayerId IKeyboardNavigationLayer.Id => KeyboardNavigationLayerId.Nodes;

        int IReadOnlyCollection<IKeyboardNavigationLayer>.Count => _navigationLayers.Count;

        #region Focus Handling

        private readonly StatefulFocusNavigator<ItemContainer> _focusNavigator = new StatefulFocusNavigator<ItemContainer>();

        bool IKeyboardNavigationLayer.TryMoveFocus(TraversalRequest request)
        {
            return _focusNavigator.TryMoveFocus(request, TryFindContainerToFocus, target => BringIntoView(target.Element, ItemContainer.BringIntoViewEdgeOffset));
        }

        private bool TryFindContainerToFocus(TraversalRequest request, out ItemContainer? containerToFocus)
        {
            containerToFocus = null;

            if (Keyboard.FocusedElement is ItemContainer focusedContainer)
            {
                containerToFocus = FindNextFocusTarget(focusedContainer, request);
            }
            else if (Keyboard.FocusedElement is NodifyEditor editor && editor.ItemContainers.Count > 0)
            {
                var viewport = new Rect(ViewportLocation, ViewportSize);
                containerToFocus = ItemContainers.FirstOrDefault(container => container.IsSelectableInArea(viewport, isContained: false))
                    ?? ItemContainers.First(); // TODO: Find the left most one?
            }
            else if (Keyboard.FocusedElement is UIElement elem && elem.GetParentOfType<ItemContainer>() is ItemContainer parentContainer)
            {
                containerToFocus = parentContainer;
            }

            return containerToFocus != null;
        }

        protected virtual ItemContainer? FindNextFocusTarget(ItemContainer currentContainer, TraversalRequest request)
        {
            var focusNavigator = new DirectionalFocusNavigator<ItemContainer>(ItemContainers);
            var result = focusNavigator.FindNextFocusTarget(currentContainer, request);

            return result?.Element;
        }

        public bool MoveFocus(FocusNavigationDirection direction)
            => MoveFocus(new TraversalRequest(direction));

        public new bool MoveFocus(TraversalRequest request)
            => KeyboardNavigationLayerGroup.ActiveLayer?.TryMoveFocus(request) ?? false;

        void IKeyboardNavigationLayer.OnActivate()
        {
            // TODO: Restore focus
        }

        void IKeyboardNavigationLayer.OnDeactivate()
        {
        }

        #endregion

        #region Layer Management

        bool IKeyboardNavigationLayerGroup.RegisterLayer(IKeyboardNavigationLayer layer)
        {
            if (_navigationLayers.Any(l => l.Id == layer.Id))
            {
                return false;
            }

            _navigationLayers.Add(layer);
            if (KeyboardNavigationLayerGroup.ActiveLayer is null)
            {
                KeyboardNavigationLayerGroup.ActivateLayer(layer.Id);
            }

            return true;
        }

        bool IKeyboardNavigationLayerGroup.RemoveLayer(KeyboardNavigationLayerId layerId)
        {
            return _navigationLayers.Remove(_navigationLayers.FirstOrDefault(layer => layer.Id == layerId)!);
        }

        bool IKeyboardNavigationLayerGroup.ActivateLayer(KeyboardNavigationLayerId layerId)
        {
            var layer = _navigationLayers.FirstOrDefault(x => x.Id == layerId);
            if (layer != null)
            {
                _activeKeyboardNavigationLayer?.OnDeactivate();
                _activeKeyboardNavigationLayer = layer;
                _activeKeyboardNavigationLayer.OnActivate();
                Debug.WriteLine($"Activated {_activeKeyboardNavigationLayer.GetType().Name} as a keyboard navigation layer in {GetType().Name}");
                return true;
            }

            return false;
        }

        bool IKeyboardNavigationLayerGroup.MoveToNextLayer()
        {
            Debug.Assert(KeyboardNavigationLayerGroup.ActiveLayer != null);

            int currentIndex = _navigationLayers.IndexOf(KeyboardNavigationLayerGroup.ActiveLayer!);
            if (currentIndex >= 0 && currentIndex < _navigationLayers.Count - 1)
            {
                var layer = _navigationLayers[currentIndex + 1];
                return KeyboardNavigationLayerGroup.ActivateLayer(layer.Id);
            }

            return false;
        }

        bool IKeyboardNavigationLayerGroup.MoveToPrevLayer()
        {
            Debug.Assert(KeyboardNavigationLayerGroup.ActiveLayer != null);

            int currentIndex = _navigationLayers.IndexOf(KeyboardNavigationLayerGroup.ActiveLayer!);
            if (currentIndex > 0)
            {
                var layer = _navigationLayers[currentIndex - 1];
                return KeyboardNavigationLayerGroup.ActivateLayer(layer.Id);
            }

            return false;
        }

        public IEnumerator<IKeyboardNavigationLayer> GetEnumerator()
            => _navigationLayers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}

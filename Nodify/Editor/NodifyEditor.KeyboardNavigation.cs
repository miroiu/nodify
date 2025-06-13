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
        /// <summary>
        /// Gets or sets the default viewport edge offset applied when bringing an item into view as a result of keyboard focus. 
        /// </summary>
        public static double BringIntoViewEdgeOffset { get; set; } = 32d;

        /// <summary>
        /// Automatically focus first container on navigation layer change or editor focus.
        /// </summary>
        public static bool AutoFocusFirstElement { get; set; } = true;

        private readonly List<IKeyboardNavigationLayer> _navigationLayers = new List<IKeyboardNavigationLayer>();
        private IKeyboardNavigationLayer? _activeKeyboardNavigationLayer;
        private IKeyboardNavigationLayer KeyboardNavigationLayer => this;
        private IKeyboardNavigationLayerGroup KeyboardNavigationLayerGroup => this;

        IKeyboardNavigationLayer? IKeyboardNavigationLayerGroup.ActiveLayer => _activeKeyboardNavigationLayer;

        KeyboardNavigationLayerId IKeyboardNavigationLayer.Id => KeyboardNavigationLayerId.Nodes;
        object? IKeyboardNavigationLayer.LastFocusedElement => _focusNavigator.LastFocusedElement;

        int IReadOnlyCollection<IKeyboardNavigationLayer>.Count => _navigationLayers.Count;

        #region Focus Handling

        private readonly StatefulFocusNavigator<ItemContainer> _focusNavigator;

        public event Action<KeyboardNavigationLayerId>? ActiveLayerChanged;

        bool IKeyboardNavigationLayer.TryMoveFocus(TraversalRequest request)
        {
            return _focusNavigator.TryMoveFocus(request, TryFindContainerToFocus);
        }

        bool IKeyboardNavigationLayer.TryRestoreFocus()
        {
            return _focusNavigator.TryRestoreFocus();
        }

        private bool TryFindContainerToFocus(TraversalRequest request, out ItemContainer? containerToFocus)
        {
            containerToFocus = null;

            if (Keyboard.FocusedElement is ItemContainer focusedContainer)
            {
                containerToFocus = FindNextFocusTarget(focusedContainer, request);
            }
            else if (Keyboard.FocusedElement is UIElement elem && elem.GetParentOfType<ItemContainer>() is ItemContainer parentContainer)
            {
                containerToFocus = parentContainer;
            }
            else if (Items.Count > 0)
            {
                var viewport = new Rect(ViewportLocation, ViewportSize);
                var containers = ItemContainers;
                containerToFocus = containers.FirstOrDefault(container => container.IsSelectableInArea(viewport, isContained: false))
                    ?? containers.First();
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
            _focusNavigator.TryRestoreFocus();
        }

        void IKeyboardNavigationLayer.OnDeactivate()
        {
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            bool isKeyboardInitiated = InputManager.Current.MostRecentInputDevice is KeyboardDevice;
            var activeKbdLayer = KeyboardNavigationLayerGroup.ActiveLayer;

            if (isKeyboardInitiated && activeKbdLayer != null)
            {
                bool isFocusComingFromOutside = e.OldFocus is null || e.OldFocus is DependencyObject dpo && !IsAncestorOf(dpo);

                if (isFocusComingFromOutside && activeKbdLayer.TryRestoreFocus())
                {
                    e.Handled = true;
                }
                else if (activeKbdLayer.LastFocusedElement is null && e.NewFocus == this && AutoFocusFirstElement)
                {
                    e.Handled = activeKbdLayer.TryMoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }

        #endregion

        #region Layer Management

        protected virtual void OnKeyboardNavigationLayerActivated(KeyboardNavigationLayerId layerId)
        {
            if (AutoFocusFirstElement && !KeyboardNavigationLayerGroup.ActiveLayer!.TryRestoreFocus())
            {
                KeyboardNavigationLayerGroup.ActiveLayer.TryMoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

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
            var layerToRemove = _navigationLayers.FirstOrDefault(layer => layer.Id == layerId);
            if (layerToRemove != null && _navigationLayers.Remove(layerToRemove))
            {
                KeyboardNavigationLayerGroup.MoveToPrevLayer();

                return true;
            }

            return false;
        }

        bool IKeyboardNavigationLayerGroup.ActivateLayer(KeyboardNavigationLayerId layerId)
        {
            var layer = _navigationLayers.FirstOrDefault(x => x.Id == layerId);
            if (layer != null)
            {
                _activeKeyboardNavigationLayer?.OnDeactivate();
                _activeKeyboardNavigationLayer = layer;
                _activeKeyboardNavigationLayer.OnActivate();
                OnKeyboardNavigationLayerActivated(layer.Id);
                Debug.WriteLine($"Activated {_activeKeyboardNavigationLayer.GetType().Name} as a keyboard navigation layer in {GetType().Name}");
                ActiveLayerChanged?.Invoke(layerId);
                return true;
            }

            return false;
        }

        bool IKeyboardNavigationLayerGroup.MoveToNextLayer()
        {
            if (_navigationLayers.Count > 0)
            {
                Debug.Assert(KeyboardNavigationLayerGroup.ActiveLayer != null);

                int currentIndex = _navigationLayers.IndexOf(KeyboardNavigationLayerGroup.ActiveLayer!);
                int nextIndex = (currentIndex + 1) % _navigationLayers.Count;

                var layer = _navigationLayers[nextIndex];
                return KeyboardNavigationLayerGroup.ActivateLayer(layer.Id);
            }

            return false;
        }

        bool IKeyboardNavigationLayerGroup.MoveToPrevLayer()
        {
            if (_navigationLayers.Count > 0)
            {
                Debug.Assert(KeyboardNavigationLayerGroup.ActiveLayer != null);

                int currentIndex = _navigationLayers.IndexOf(KeyboardNavigationLayerGroup.ActiveLayer!);
                int prevIndex = (currentIndex - 1 + _navigationLayers.Count) % _navigationLayers.Count;
                var layer = _navigationLayers[prevIndex];
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

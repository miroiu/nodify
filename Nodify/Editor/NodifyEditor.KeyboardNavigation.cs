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

        // TODO: When do we clear these?
        private readonly WeakReference<ItemContainer> _previousFocusedContainer = new WeakReference<ItemContainer>(null);
        private FocusNavigationDirection? _previousFocusNavigationDirection;

        #region Focus Handling

        bool IKeyboardNavigationLayer.TryMoveFocus(TraversalRequest request)
        {
            // TODO: throw exception if request.FocusNavigationDirection is not directional (Left, Right, Up, Down) or handle other cases too
            var prevContainer = Keyboard.FocusedElement as ItemContainer;

            if (_previousFocusNavigationDirection.HasValue && request.FocusNavigationDirection.IsOppositeOf(_previousFocusNavigationDirection.Value))
            {
                // If the request is in the opposite direction of the last focus navigation, try to restore the previous focused container
                if (_previousFocusedContainer.TryGetTarget(out var previousContainer) && previousContainer.Focus())
                {
                    _previousFocusNavigationDirection = request.FocusNavigationDirection;
                    if (prevContainer != null)
                    {
                        _previousFocusedContainer.SetTarget(prevContainer);
                    }
                    BringIntoView(previousContainer, ItemContainer.BringIntoViewEdgeOffset);
                    return true;
                }

            }
            else if (TryGetContainerToFocus(out var containerToFocus, request) && containerToFocus!.Focus())
            {
                _previousFocusNavigationDirection = request.FocusNavigationDirection;
                if (prevContainer != null)
                {
                    _previousFocusedContainer.SetTarget(prevContainer);
                }
                BringIntoView(containerToFocus, ItemContainer.BringIntoViewEdgeOffset);
                return true;
            }

            return false;
        }

        private bool TryGetContainerToFocus(out ItemContainer? containerToFocus, TraversalRequest request)
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
            var currentContainerBounds = new Rect(currentContainer.Location, currentContainer.DesiredSizeForSelection ?? currentContainer.RenderSize);
            var currentContainerCenter = new Point(currentContainerBounds.X + currentContainerBounds.Width / 2, currentContainerBounds.Y + currentContainerBounds.Height / 2);

            //IEnumerable<ItemContainer> candidates = request.FocusNavigationDirection switch
            //{
            //    FocusNavigationDirection.Left => ItemContainers.Where(c =>
            //    {
            //        var center = new Point(c.Bounds.X + c.Bounds.Width / 2, c.Bounds.Y + c.Bounds.Height / 2);
            //        return center.X < currentContainerCenter.X;
            //    }),
            //    FocusNavigationDirection.Right => ItemContainers.Where(c =>
            //    {
            //        var center = new Point(c.Bounds.X + c.Bounds.Width / 2, c.Bounds.Y + c.Bounds.Height / 2);
            //        return center.X > currentContainerCenter.X;
            //    }),
            //    FocusNavigationDirection.Up => ItemContainers.Where(c =>
            //    {
            //        var center = new Point(c.Bounds.X + c.Bounds.Width / 2, c.Bounds.Y + c.Bounds.Height / 2);
            //        return center.Y < currentContainerCenter.Y;
            //    }),
            //    FocusNavigationDirection.Down => ItemContainers.Where(c =>
            //    {
            //        var center = new Point(c.Bounds.X + c.Bounds.Width / 2, c.Bounds.Y + c.Bounds.Height / 2);
            //        return center.Y > currentContainerCenter.Y;
            //    }),
            //    _ => Enumerable.Empty<ItemContainer>()
            //};

            var itemContainers = ItemContainers;

            IEnumerable<ItemContainer> candidates = request.FocusNavigationDirection switch
            {
                FocusNavigationDirection.Left => itemContainers.Where(c => c.Bounds.Right <= currentContainerBounds.Left),
                FocusNavigationDirection.Right => itemContainers.Where(c => c.Location.X >= currentContainerBounds.Right),
                FocusNavigationDirection.Up => itemContainers.Where(c => c.Bounds.Bottom <= currentContainerBounds.Top),
                FocusNavigationDirection.Down => itemContainers.Where(c => c.Location.Y >= currentContainerBounds.Bottom),
                _ => Enumerable.Empty<ItemContainer>()
            };

            // Wrap focus if no candidates found in the current direction  
            if (!candidates.Any())
            {
                candidates = request.FocusNavigationDirection switch
                {
                    FocusNavigationDirection.Left => itemContainers.OrderByDescending(c => c.Location.X).Take(1),
                    FocusNavigationDirection.Right => itemContainers.OrderBy(c => c.Location.X).Take(1),
                    FocusNavigationDirection.Up => itemContainers.OrderByDescending(c => c.Location.Y).Take(1),
                    FocusNavigationDirection.Down => itemContainers.OrderBy(c => c.Location.Y).Take(1),
                    _ => Enumerable.Empty<ItemContainer>()
                };

                request.Wrapped = true;
            }

            ItemContainer? best = null;
            double minDistanceSquared = double.MaxValue;

            foreach (var candidate in candidates)
            {
                // TODO: If candidate is on screen, give it priority over candidates that are off-screen

                //var bounds = new Rect(candidate.Location, candidate.DesiredSizeForSelection ?? candidate.RenderSize);
                //var center = new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);

                //double distanceSquared = (center - currentContainerCenter).LengthSquared;
                //if (distanceSquared < minDistanceSquared)
                //{
                //    minDistanceSquared = distanceSquared;
                //    best = candidate;
                //}

                double distanceSquared = (candidate.Location - currentContainerBounds.TopLeft).LengthSquared;
                if (distanceSquared < minDistanceSquared)
                {
                    minDistanceSquared = distanceSquared;
                    best = candidate;
                }
            }

            return best;
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

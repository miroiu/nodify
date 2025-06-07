using Nodify.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Collections;

namespace Nodify
{
    public partial class NodifyEditor : IKeyboardNavigationLayer, INavigationLayerGroup
    {
        private readonly List<IKeyboardNavigationLayer> _focusScopes = new List<IKeyboardNavigationLayer>();
        private IKeyboardNavigationLayer _activeFocusScope;

        public IKeyboardNavigationLayer ActiveLayer => _activeFocusScope;

        bool IKeyboardNavigationLayer.IsActiveLayer => ActiveLayer == this;

        KeyboardNavigationLayerId IKeyboardNavigationLayer.Id => KeyboardNavigationLayerId.Nodes;

        int IReadOnlyCollection<IKeyboardNavigationLayer>.Count => _focusScopes.Count;

        // TODO: When do we clear these?

        #region Focus Handling

        bool IKeyboardNavigationLayer.TryMoveFocus(TraversalRequest request)
        {
            if (TryGetContainerToFocus(out var containerToFocus, request))
            {
                containerToFocus!.Focus();
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
                containerToFocus = GetDirectionalFocusTarget(focusedContainer, request);
            }
            else if (Keyboard.FocusedElement is NodifyEditor editor && editor.ItemContainers.Count > 0)
            {
                var viewport = new Rect(ViewportLocation, ViewportSize);
                containerToFocus = ItemContainers.FirstOrDefault(container => container.IsSelectableInArea(viewport, isContained: false))
                    ?? ItemContainers.FirstOrDefault(); // TODO: Find the left most one?
            }
            else if (Keyboard.FocusedElement is UIElement elem && elem.GetParentOfType<ItemContainer>() is ItemContainer parentContainer)
            {
                containerToFocus = parentContainer;
            }

            return containerToFocus != null;
        }

        // TODO: Customizable
        protected virtual ItemContainer? GetDirectionalFocusTarget(ItemContainer currentContainer, TraversalRequest request)
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
                var bounds = new Rect(candidate.Location, candidate.DesiredSizeForSelection ?? candidate.RenderSize);
                var center = new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);

                double distanceSquared = (center - currentContainerCenter).LengthSquared;
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
            => ActiveLayer.TryMoveFocus(request);

        void INavigationLayerGroup.RegisterLayer(IKeyboardNavigationLayer layer)
        {
            _focusScopes.Add(layer);
        }

        void INavigationLayerGroup.RemoveLayer(KeyboardNavigationLayerId layerId)
        {
            _focusScopes.Remove(_focusScopes.FirstOrDefault(layer => layer.Id == layerId)!);
        }

        void INavigationLayerGroup.MoveToNextLayer()
        {
            int currentIndex = _focusScopes.IndexOf(ActiveLayer);
            if (currentIndex >= 0 && currentIndex < _focusScopes.Count - 1)
            {
                _activeFocusScope = _focusScopes[currentIndex + 1];
                // TODO: Focus container or logical element?
                _activeFocusScope.TryMoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        void INavigationLayerGroup.MoveToPrevLayer()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IKeyboardNavigationLayer> GetEnumerator()
            => _focusScopes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion

    }
}

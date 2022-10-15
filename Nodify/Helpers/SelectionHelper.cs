﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Nodify
{
    /// <summary>
    /// Helps with selecting <see cref="ItemContainer"/>s and updating the <see cref="NodifyEditor.SelectedArea"/> and <see cref="NodifyEditor.IsSelecting"/> properties.
    /// </summary>
    public sealed class SelectionHelper
    {
        private readonly NodifyEditor _host;
        private Point _startLocation;
        private SelectionType _selectionType;
        private bool _isRealtime;
        private IList<ItemContainer> _initialSelection = new List<ItemContainer>();

        /// <summary>Constructs a new instance of a <see cref="SelectionHelper"/>.</summary>
        /// <param name="host">The editor to select items from.</param>
        public SelectionHelper(NodifyEditor host)
            => _host = host;

        /// <summary>Available selection logic.</summary>
        public enum SelectionType
        {
            /// <summary>Replaces the old selection.</summary>
            Replace,
            /// <summary>Removes items from existing selection.</summary>
            Remove,
            /// <summary>Adds items to the current selection.</summary>
            Append,
            /// <summary>Inverts the selection.</summary>
            Invert
        }

        /// <summary>Attempts to start a new selection.</summary>
        /// <param name="location">The location inside the graph.</param>
        /// <param name="selectionType">The type of selection.</param>
        /// <remarks>Will not do anything if selection is in progress.</remarks>
        public void Start(Point location, SelectionType selectionType)
        {
            if (!_host.IsSelecting)
            {
                _selectionType = selectionType;
                _initialSelection = GetSelectedContainers();

                _isRealtime = _host.EnableRealtimeSelection;
                _startLocation = location;

                _host.SelectedArea = new Rect();
                _host.IsSelecting = true;
            }
        }

        /// <summary>Update the end location for the selection.</summary>
        /// <param name="endLocation">An absolute location.</param>
        public void Update(Point endLocation)
        {
            double left = endLocation.X < _startLocation.X ? endLocation.X : _startLocation.X;
            double top = endLocation.Y < _startLocation.Y ? endLocation.Y : _startLocation.Y;
            double width = Math.Abs(endLocation.X - _startLocation.X);
            double height = Math.Abs(endLocation.Y - _startLocation.Y);

            _host.SelectedArea = new Rect(left, top, width, height);

            if (_isRealtime)
            {
                PreviewSelection(_host.SelectedArea);
            }
        }
        
        /// <summary>Commits the current selection to the editor.</summary>
        public void End()
        {
            if (_host.IsSelecting)
            {
                _host.IsSelecting = false;

                PreviewSelection(_host.SelectedArea);

                _host.ApplyPreviewingSelection();
                _initialSelection.Clear();
            }
        }

        private void PreviewSelection(Rect area)
        {
            switch (_selectionType)
            {
                case SelectionType.Replace:
                    PreviewSelectArea(area);
                    break;

                case SelectionType.Remove:
                    PreviewSelectContainers(_initialSelection);

                    PreviewUnselectArea(area);
                    break;

                case SelectionType.Append:
                    PreviewUnselectAll();
                    PreviewSelectContainers(_initialSelection);

                    PreviewSelectArea(area, true);
                    break;

                case SelectionType.Invert:
                    PreviewUnselectAll();
                    PreviewSelectContainers(_initialSelection);

                    PreviewInvertSelection(area);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(SelectionType));
            }
        }

        private void PreviewUnselectAll()
        {
            ItemCollection items = _host.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)_host.ItemContainerGenerator.ContainerFromIndex(i);
                container.IsPreviewingSelection = false;
            }
        }

        private void PreviewSelectArea(Rect area, bool append = false, bool fit = false)
        {
            if (!append)
            {
                PreviewUnselectAll();
            }

            ItemCollection items = _host.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)_host.ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsSelectableInArea(area, fit))
                {
                    container.IsPreviewingSelection = true;
                }
            }
        }

        private void PreviewUnselectArea(Rect area, bool fit = false)
        {
            ItemCollection items = _host.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)_host.ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsSelectableInArea(area, fit))
                {
                    container.IsPreviewingSelection = false;
                }
            }
        }

        private void PreviewSelectContainers(IList<ItemContainer> containers)
        {
            for (var i = 0; i < containers.Count; i++)
            {
                containers[i].IsPreviewingSelection = true;
            }
        }

        private void PreviewInvertSelection(Rect area, bool fit = false)
        {
            ItemCollection items = _host.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)_host.ItemContainerGenerator.ContainerFromIndex(i);
                if (container.IsSelectableInArea(area, fit))
                {
                    container.IsPreviewingSelection = !container.IsPreviewingSelection;
                }
            }
        }
        
        private IList<ItemContainer> GetSelectedContainers()
        {
            var result = new List<ItemContainer>(32);
            IList items = ((MultiSelector)_host).SelectedItems;
            for (var i = 0; i < items.Count; i++)
            {
                var container = (ItemContainer)_host.ItemContainerGenerator.ContainerFromItem(items[i]);
                result.Add(container);
            }
            return result;
        }
    }
}

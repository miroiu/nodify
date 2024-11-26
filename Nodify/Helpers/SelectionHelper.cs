using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Helps with selecting <see cref="ItemContainer"/>s.
    /// </summary>
    internal sealed class SelectionHelper
    {
        private Point _startLocation;
        private Point _endLocation;
        private SelectionType _selectionType;
        private bool _isRealtime;
        private IReadOnlyCollection<ItemContainer> _items = Array.Empty<ItemContainer>();
        private IReadOnlyList<ItemContainer> _initialSelection = Array.Empty<ItemContainer>();
        private Rect _selectedArea;

        /// <summary>Attempts to start a new selection.</summary>
        /// <param name="containers">The containers that can be part of the selection.</param>
        /// <param name="location">The location inside the graph.</param>
        /// <param name="selectionType">The type of selection.</param>
        /// <remarks>Will not do anything if selection is in progress.</remarks>
        public Rect Start(IEnumerable<ItemContainer> containers, Point location, SelectionType selectionType, bool realtime)
        {
            _items = containers.Where(x => x.IsSelectable).ToList();
            _initialSelection = containers.Where(x => x.IsSelected).ToList();

            _selectionType = selectionType;

            _isRealtime = realtime;
            _startLocation = location;
            _endLocation = location;

            _selectedArea = new Rect();
            return _selectedArea;
        }

        /// <summary>Update the end location for the selection.</summary>
        /// <param name="endLocation">An absolute location.</param>
        public Rect Update(Point endLocation)
        {
            _endLocation = endLocation;

            double left = _endLocation.X < _startLocation.X ? _endLocation.X : _startLocation.X;
            double top = _endLocation.Y < _startLocation.Y ? _endLocation.Y : _startLocation.Y;
            double width = Math.Abs(_endLocation.X - _startLocation.X);
            double height = Math.Abs(_endLocation.Y - _startLocation.Y);

            _selectedArea = new Rect(left, top, width, height);

            if (_isRealtime)
            {
                PreviewSelection(_selectedArea);
            }

            return _selectedArea;
        }

        /// <summary>Increase the selected area by the specified amount.</summary>
        public Rect Update(Vector amount)
        {
            _endLocation += amount;

            return Update(_endLocation);
        }

        /// <summary>Commits the current selection to the editor.</summary>
        public Rect End()
        {
            PreviewSelection(_selectedArea);
            _items = Array.Empty<ItemContainer>();
            _initialSelection = Array.Empty<ItemContainer>();

            return _selectedArea;
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
                    throw new NotImplementedException(nameof(SelectionType));
            }
        }

        private void PreviewUnselectAll()
        {
            foreach (var container in _items)
            {
                container.IsPreviewingSelection = false;
            }
        }

        private void PreviewSelectArea(Rect area, bool append = false, bool fit = false)
        {
            if (!append)
            {
                PreviewUnselectAll();
            }

            if (area.X != 0 || area.Y != 0 || area.Width > 0 || area.Height > 0)
            {
                foreach (var container in _items)
                {
                    if (container.IsSelectableInArea(area, fit))
                    {
                        container.IsPreviewingSelection = true;
                    }
                }
            }
        }

        private void PreviewUnselectArea(Rect area, bool fit = false)
        {
            foreach (var container in _items)
            {
                if (container.IsSelectableInArea(area, fit))
                {
                    container.IsPreviewingSelection = false;
                }
            }
        }

        private static void PreviewSelectContainers(IReadOnlyList<ItemContainer> containers)
        {
            for (var i = 0; i < containers.Count; i++)
            {
                containers[i].IsPreviewingSelection = true;
            }
        }

        private void PreviewInvertSelection(Rect area, bool fit = false)
        {
            foreach (var container in _items)
            {
                if (container.IsSelectableInArea(area, fit))
                {
                    container.IsPreviewingSelection = !container.IsPreviewingSelection;
                }
            }
        }

        internal static SelectionType GetSelectionType(MouseButtonEventArgs e)
        {
            EditorGestures.SelectionGestures gestures = EditorGestures.Mappings.Editor.Selection;
            if (gestures.Append.Matches(e.Source, e))
            {
                return SelectionType.Append;
            }

            if (gestures.Invert.Matches(e.Source, e))
            {
                return SelectionType.Invert;
            }

            if (gestures.Remove.Matches(e.Source, e))
            {
                return SelectionType.Remove;
            }

            return SelectionType.Replace;
        }
    }
}

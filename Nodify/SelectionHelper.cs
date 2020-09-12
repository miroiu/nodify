using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public sealed class SelectionHelper
    {
        private readonly NodifyEditor _host;
        private Point _startLocation;
        private SelectionType _selectionType;
        private bool _realtime;
        private List<ItemContainer> _selectedNodes = default!;
        private List<ItemContainer> _nodesInArea = default!;

        public SelectionHelper(NodifyEditor host)
            => _host = host;

        public enum SelectionType
        {
            Replace,
            Remove,
            Append,
            Invert
        }

        public void Start(Point location, bool realtime = false, SelectionType? selectionType = default)
        {
            var modifiers = Keyboard.Modifiers;
            _selectionType = selectionType ?? modifiers switch
            {
                ModifierKeys.Control => SelectionType.Invert,
                ModifierKeys.Alt => SelectionType.Remove,
                ModifierKeys.Shift => SelectionType.Append,
                _ => SelectionType.Replace
            };

            if (_selectionType == SelectionType.Replace)
            {
                _host.UnselectAll();
            }

            _realtime = realtime;
            _startLocation = location;
            _selectedNodes = _host.GetSelectedItems();
            _nodesInArea = new List<ItemContainer>(_host.Items.Count / 2);

            _host.SelectingRectangle = new Rect();
            _host.IsSelecting = true;
        }

        public void Update(Point endLocation)
        {
            var left = endLocation.X < _startLocation.X ? endLocation.X : _startLocation.X;
            var top = endLocation.Y < _startLocation.Y ? endLocation.Y : _startLocation.Y;
            var width = Math.Abs(endLocation.X - _startLocation.X);
            var height = Math.Abs(endLocation.Y - _startLocation.Y);

            _host.SelectingRectangle = new Rect(left, top, width, height);

            if (_realtime)
            {
                ApplySelection(_host.SelectingRectangle);
            }
        }

        public void End()
        {
            if (_host.IsSelecting)
            {
                _host.IsSelecting = false;
                var rect = _host.SelectingRectangle;
                if (rect.Width > 0 && rect.Height > 0)
                {
                    ApplySelection(rect);
                }

                _selectedNodes.Clear();
                _nodesInArea.Clear();
            }
        }

        private void ApplySelection(Rect area)
        {
            switch (_selectionType)
            {
                case SelectionType.Replace:
                    _host.UnselectAll();
                    _host.AppendSelection(area);
                    break;

                case SelectionType.Remove:
                    _host.GetItemsInAreaNoAlloc(_nodesInArea, area);

                    if (_realtime)
                    {
                        _host.SetSelectedItems(_selectedNodes, true);
                    }

                    _host.SetSelectedItems(_nodesInArea, false);
                    break;

                case SelectionType.Append:
                    _host.GetItemsInAreaNoAlloc(_nodesInArea, area);

                    if (_realtime)
                    {
                        _host.UnselectAll();
                        _host.SetSelectedItems(_selectedNodes, true);
                    }

                    _host.SetSelectedItems(_nodesInArea, true);
                    break;

                case SelectionType.Invert:
                    if (_realtime)
                    {
                        _host.GetItemsInAreaNoAlloc(_nodesInArea, area);
                        var toSelect = _nodesInArea.Except(_selectedNodes).ToList();
                        var toDeselect = _nodesInArea.Except(toSelect).ToList();

                        _host.UnselectAll();
                        _host.SetSelectedItems(_selectedNodes, true);
                        _host.SetSelectedItems(toSelect, true);
                        _host.SetSelectedItems(toDeselect, false);
                    }
                    else
                    {
                        _host.InvertSelection(area);
                    }
                    break;
            }
        }
    }
}

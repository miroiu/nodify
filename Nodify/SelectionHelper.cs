using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Nodify
{
    public sealed class SelectionHelper
    {
        private readonly NodifyEditor _host;
        private Point _startLocation;
        private SelectionType _selectionType;
        private bool _isRealtime;
        private object[] _initialSelection = Array.Empty<object>();

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

            _isRealtime = realtime;
            _startLocation = location;

            var items = ((MultiSelector)_host).SelectedItems;
            _initialSelection = new object[items.Count];
            items.CopyTo(_initialSelection, 0);

            _host.SelectedArea = new Rect();
            _host.IsSelecting = true;
        }

        public void Update(Point endLocation)
        {
            var left = endLocation.X < _startLocation.X ? endLocation.X : _startLocation.X;
            var top = endLocation.Y < _startLocation.Y ? endLocation.Y : _startLocation.Y;
            var width = Math.Abs(endLocation.X - _startLocation.X);
            var height = Math.Abs(endLocation.Y - _startLocation.Y);

            _host.SelectedArea = new Rect(left, top, width, height);

            if (_isRealtime)
            {
                ApplySelection(_host.SelectedArea);
            }
        }

        public void End()
        {
            if (_host.IsSelecting)
            {
                _host.IsSelecting = false;
                var rect = _host.SelectedArea;
                if (rect.Width > 0 && rect.Height > 0)
                {
                    ApplySelection(rect);
                }

                _initialSelection = Array.Empty<object>();
            }
        }

        private void ApplySelection(Rect area)
        {
            switch (_selectionType)
            {
                case SelectionType.Replace:
                    _host.SelectArea(area, false);
                    break;

                case SelectionType.Remove:
                    if (_isRealtime)
                    {
                        _host.SelectItems(_initialSelection);
                    }

                    _host.UnselectArea(area);
                    break;

                case SelectionType.Append:
                    if (_isRealtime)
                    {
                        _host.UnselectAll();
                        _host.SelectItems(_initialSelection);
                    }

                    _host.SelectArea(area, true);
                    break;

                case SelectionType.Invert:
                    if (_isRealtime)
                    {
                        _host.UnselectAll();
                        _host.SelectItems(_initialSelection);
                    }

                    _host.InvertSelection(area);
                    break;
            }
        }
    }
}

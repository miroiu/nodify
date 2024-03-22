using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify
{
    internal interface IDraggingStrategy
    {
        void Start(Vector change);
        void Update(Vector change);
        void End(Vector change);
        void Abort(Vector change);
    }

    internal class DraggingSimple : IDraggingStrategy
    {
        private readonly NodifyEditor _editor;
        private Vector _dragOffset;
        private Vector _dragAccumulator;
        private readonly IList<ItemContainer> _selectedContainers;

        public DraggingSimple(NodifyEditor editor)
        {
            _editor = editor;
            _selectedContainers = _editor.SelectedContainers.Where(c => c.IsDraggable).ToList();
        }

        public void Abort(Vector change)
        {
            for (var i = 0; i < _selectedContainers.Count; i++)
            {
                ItemContainer container = _selectedContainers[i];
                container.SetCurrentValue(ItemContainer.LocationProperty, container.Location - _dragOffset);
            }

            _selectedContainers.Clear();
        }

        public void End(Vector change)
        {
            for (var i = 0; i < _selectedContainers.Count; i++)
            {
                ItemContainer container = _selectedContainers[i];
                Point result = container.Location;

                // Correct the final position
                if (NodifyEditor.EnableSnappingCorrection)
                {
                    result = new Point(
                    (int)result.X / _editor.GridCellSize * _editor.GridCellSize,
                    (int)result.Y / _editor.GridCellSize * _editor.GridCellSize);
                }

                container.SetCurrentValue(ItemContainer.LocationProperty, result);
            }

            _selectedContainers.Clear();
        }

        public void Start(Vector change)
        {
            _dragOffset = new Vector(0, 0);
            _dragAccumulator = new Vector(0, 0);
        }

        public void Update(Vector change)
        {
            _dragAccumulator += change;
            var delta = new Vector((int)_dragAccumulator.X / _editor.GridCellSize * _editor.GridCellSize, (int)_dragAccumulator.Y / _editor.GridCellSize * _editor.GridCellSize);
            _dragAccumulator -= delta;

            if (delta.X != 0 || delta.Y != 0)
            {
                _dragOffset += delta;

                for (var i = 0; i < _selectedContainers.Count; i++)
                {
                    ItemContainer container = _selectedContainers[i];
                    container.SetCurrentValue(ItemContainer.LocationProperty, new Point(container.Location.X + delta.X, container.Location.Y + delta.Y));
                }
            }
        }
    }
}

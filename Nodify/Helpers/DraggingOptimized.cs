using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Nodify
{
    /// <summary>
    /// Commits the position changes at the end of the operation. Updates the RenderTransform to preview the container position.
    /// </summary>
    internal class DraggingOptimized : IDraggingStrategy
    {
        private readonly NodifyEditor _editor;
        private Vector _dragAccumulator;
        private readonly List<ItemContainer> _selectedContainers;

        public DraggingOptimized(NodifyEditor editor)
        {
            _editor = editor;
            _selectedContainers = _editor.SelectedContainers.Where(c => c.IsDraggable).ToList();
        }

        public void Abort(Vector change)
        {
            for (var i = 0; i < _selectedContainers.Count; i++)
            {
                ItemContainer container = _selectedContainers[i];
                var r = (TranslateTransform)container.RenderTransform;

                r.X = 0;
                r.Y = 0;

                container.OnPreviewLocationChanged(container.Location);
            }

            _selectedContainers.Clear();
        }

        public void End(Vector change)
        {
            for (var i = 0; i < _selectedContainers.Count; i++)
            {
                ItemContainer container = _selectedContainers[i];
                var r = (TranslateTransform)container.RenderTransform;

                Point result = container.Location + new Vector(r.X, r.Y);

                // Correct the final position
                if (NodifyEditor.EnableSnappingCorrection && (r.X != 0 || r.Y != 0))
                {
                    result = new Point((int)result.X / _editor.GridCellSize * _editor.GridCellSize,
                        (int)result.Y / _editor.GridCellSize * _editor.GridCellSize);
                }

                container.SetCurrentValue(ItemContainer.LocationProperty, result);

                r.X = 0;
                r.Y = 0;
            }

            _selectedContainers.Clear();
        }

        public void Start(Vector change)
        {
            _dragAccumulator = new Vector(0, 0);
        }

        public void Update(Vector change)
        {
            _dragAccumulator += change;
            var delta = new Vector((int)_dragAccumulator.X / _editor.GridCellSize * _editor.GridCellSize, (int)_dragAccumulator.Y / _editor.GridCellSize * _editor.GridCellSize);
            _dragAccumulator -= delta;

            if (delta.X != 0 || delta.Y != 0)
            {
                for (var i = 0; i < _selectedContainers.Count; i++)
                {
                    ItemContainer container = _selectedContainers[i];
                    var r = (TranslateTransform)container.RenderTransform;

                    r.X += delta.X; // Snapping without correction
                    r.Y += delta.Y; // Snapping without correction

                    container.OnPreviewLocationChanged(container.Location + new Vector(r.X, r.Y));
                }
            }
        }
    }
}

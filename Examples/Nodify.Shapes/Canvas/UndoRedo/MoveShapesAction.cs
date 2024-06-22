using Nodify.UndoRedo;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify.Shapes.Canvas.UndoRedo
{
    public class MoveShapesAction : IAction
    {
        private readonly IReadOnlyCollection<ShapeViewModel> _movedShapes;
        private readonly Dictionary<ShapeViewModel, Point> _initialLocations;
        private Dictionary<ShapeViewModel, Point>? _finalLocations;

        public MoveShapesAction(CanvasViewModel canvas)
        {
            _movedShapes = canvas.SelectedShapes;
            _initialLocations = _movedShapes.ToDictionary(x => x, x => x.Location);
        }

        public string? Label => "Move shapes";

        public void Execute()
        {
            _finalLocations?.ForEach(x => x.Key.Location = x.Value);
        }

        public void Undo()
        {
            _initialLocations.ForEach(x => x.Key.Location = x.Value);
        }

        public void SaveLocations()
        {
            _finalLocations = _movedShapes.ToDictionary(x => x, x => x.Location);
        }
    }
}

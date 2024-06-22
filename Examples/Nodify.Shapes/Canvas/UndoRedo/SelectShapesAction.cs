using Nodify.UndoRedo;
using System.Collections.Generic;
using System.Linq;

namespace Nodify.Shapes.Canvas.UndoRedo
{
    public class SelectShapesAction : IAction
    {
        private readonly CanvasViewModel _canvas;
        private readonly List<ShapeViewModel> _initialSelection;
        private List<ShapeViewModel>? _finalSelection;

        public SelectShapesAction(CanvasViewModel canvas)
        {
            _canvas = canvas;
            _initialSelection = _canvas.SelectedShapes.ToList();
        }

        public string? Label => "Select shapes";

        public void Execute()
        {
            if (_finalSelection != null)
            {
                _canvas.SelectedShapes.Clear();
                _canvas.SelectedShapes.AddRange(_finalSelection);
            }
        }

        public void Undo()
        {
            _canvas.SelectedShapes.Clear();
            _canvas.SelectedShapes.AddRange(_initialSelection);
        }

        public void SaveSelection()
        {
            _finalSelection = _canvas.SelectedShapes.ToList();
        }
    }
}

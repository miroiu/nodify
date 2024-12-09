using Nodify.UndoRedo;
using System.Collections.Generic;

namespace Nodify.Shapes.Canvas.UndoRedo
{
    public class SelectShapesAction : IAction
    {
        public string? Label => "Select shapes";

        private readonly IReadOnlyCollection<ShapeViewModel> _nodes;
        private readonly CanvasViewModel _canvas;

        public SelectShapesAction(IReadOnlyCollection<ShapeViewModel> nodes, CanvasViewModel canvas)
        {
            _nodes = nodes;
            _canvas = canvas;
        }

        public void Execute()
        {
            _canvas.SelectedShapes.AddRange(_nodes);
        }

        public void Undo()
        {
            _canvas.SelectedShapes.RemoveRange(_nodes);
        }

        public override string? ToString()
            => Label;
    }

    public class DeselectShapesAction : IAction
    {
        public string? Label => "Deselect shapes";

        private readonly IReadOnlyCollection<ShapeViewModel> _nodes;
        private readonly CanvasViewModel _canvas;

        public DeselectShapesAction(IReadOnlyCollection<ShapeViewModel> nodes, CanvasViewModel canvas)
        {
            _nodes = nodes;
            _canvas = canvas;
        }

        public void Execute()
        {
            _canvas.SelectedShapes.RemoveRange(_nodes);
        }

        public void Undo()
        {
            _canvas.SelectedShapes.AddRange(_nodes);
        }

        public override string? ToString()
            => Label;
    }
}

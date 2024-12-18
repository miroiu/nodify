using Nodify.UndoRedo;
using System.Collections.Generic;
using System.Windows;

namespace Nodify.Shapes.Canvas.UndoRedo
{
    public class MoveShapesAction : IAction
    {
        private readonly IReadOnlyCollection<ShapeViewModel> _shapes;
        private readonly Vector _offset;

        public string? Label => "Move shapes";

        public MoveShapesAction(IReadOnlyCollection<ShapeViewModel> shapes, Vector offset)
        {
            _shapes = shapes;
            _offset = offset;
        }

        public void Execute()
            => ApplyOffset(_offset);

        public void Undo()
            => ApplyOffset(-_offset);

        private void ApplyOffset(Vector offset)
        {
            foreach (var shape in _shapes)
            {
                shape.Location += offset;
            }
        }
    }
}

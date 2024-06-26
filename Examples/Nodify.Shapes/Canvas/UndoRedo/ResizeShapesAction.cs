using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Nodify.UndoRedo;

namespace Nodify.Shapes.Canvas.UndoRedo
{
    public class ResizeShapesAction : IAction
    {
        private readonly IReadOnlyCollection<ShapeViewModel> _resizedShapes;
        private readonly Dictionary<ShapeViewModel, Size> _initialSizes;
        private Dictionary<ShapeViewModel, Size>? _finalSizes;

        // Resizing could also move the shape
        private readonly MoveShapesAction _moveShapesAction;

        public ResizeShapesAction(CanvasViewModel canvas)
        {
            _resizedShapes = canvas.SelectedShapes;
            _initialSizes = _resizedShapes.ToDictionary(x => x, x => new Size(x.Width, x.Height));

            _moveShapesAction = new MoveShapesAction(canvas);
        }

        public string? Label => "Resize shapes";

        public void Execute()
        {
            _moveShapesAction.Execute();

            _finalSizes?.ForEach(x =>
            {
                x.Key.Width = x.Value.Width;
                x.Key.Height = x.Value.Height;
            });
        }

        public void Undo()
        {
            _initialSizes.ForEach(x =>
            {
                x.Key.Width = x.Value.Width;
                x.Key.Height = x.Value.Height;
            });

            _moveShapesAction.Undo();
        }

        public void SaveSizes()
        {
            _moveShapesAction.SaveLocations();

            _finalSizes = _resizedShapes.ToDictionary(x => x, x => new Size(x.Width, x.Height));
        }
    }
}

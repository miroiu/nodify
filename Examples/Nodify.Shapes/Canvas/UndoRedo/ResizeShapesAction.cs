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
        private readonly Dictionary<ShapeViewModel, Point> _initialLocations;
        private Dictionary<ShapeViewModel, Point>? _finalLocations;

        public ResizeShapesAction(CanvasViewModel canvas)
        {
            _resizedShapes = canvas.SelectedShapes;
            _initialSizes = _resizedShapes.ToDictionary(x => x, x => new Size(x.Width, x.Height));
            _initialLocations = _resizedShapes.ToDictionary(x => x, x => x.Location);
        }

        public string? Label => "Resize shapes";

        public void Execute()
        {
            _finalLocations?.ForEach(x => x.Key.Location = x.Value);

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

            _initialLocations.ForEach(x => x.Key.Location = x.Value);
        }

        public void SaveSizes()
        {
            _finalLocations = _resizedShapes.ToDictionary(x => x, x => x.Location);
            _finalSizes = _resizedShapes.ToDictionary(x => x, x => new Size(x.Width, x.Height));
        }
    }
}

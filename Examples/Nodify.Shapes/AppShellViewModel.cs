using Nodify.Shapes.Canvas;

namespace Nodify.Shapes
{
    public class AppShellViewModel : ObservableObject
    {
        public CanvasViewModel Canvas { get; } = new CanvasViewModel();
    }
}

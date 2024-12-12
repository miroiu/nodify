using Nodify.Interactivity;
using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public class DrawingGesturesMappings : EditorGestures
    {
        public static readonly DrawingGesturesMappings Instance = new DrawingGesturesMappings();

        public InputGestureRef Draw { get; }

        public DrawingGesturesMappings()
        {
            Apply(UnboundGestureMappings.Instance);

            Draw = new Interactivity.MouseGesture(MouseAction.LeftClick);
        }
    }
}

using Nodify.Interactivity;
using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public class LockedGestureMappings : EditorGestures
    {
        public static readonly LockedGestureMappings Instance = new LockedGestureMappings();

        public LockedGestureMappings()
        {
            Apply(UnboundGestureMappings.Instance);

            Editor.Pan.Value = new AnyGesture(new Interactivity.MouseGesture(MouseAction.LeftClick), new Interactivity.MouseGesture(MouseAction.RightClick), new Interactivity.MouseGesture(MouseAction.MiddleClick));
        }
    }
}

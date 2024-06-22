using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public class LockedGestureMappings : EditorGestures
    {
        public static readonly LockedGestureMappings Instance = new LockedGestureMappings();

        public LockedGestureMappings()
        {
            Apply(UnboundGestureMappings.Instance);

            Editor.Pan.Value = new AnyGesture(new MouseGesture(MouseAction.LeftClick), new MouseGesture(MouseAction.RightClick), new MouseGesture(MouseAction.MiddleClick));
        }
    }
}

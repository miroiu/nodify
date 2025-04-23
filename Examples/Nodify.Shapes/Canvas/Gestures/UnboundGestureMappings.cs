using Nodify.Interactivity;

namespace Nodify.Shapes.Canvas
{
    public class UnboundGestureMappings : EditorGestures
    {
        public static readonly UnboundGestureMappings Instance = new UnboundGestureMappings();

        public UnboundGestureMappings()
        {
            Editor.Selection.Unbind();
            Editor.SelectAll.Unbind();
            ItemContainer.Selection.Unbind();
            Connection.Disconnect.Unbind();
            Connector.Connect.Unbind();
        }
    }
}

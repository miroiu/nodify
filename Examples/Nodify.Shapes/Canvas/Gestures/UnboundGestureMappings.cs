namespace Nodify.Shapes.Canvas
{
    public class UnboundGestureMappings : EditorGestures
    {
        public static readonly UnboundGestureMappings Instance = new UnboundGestureMappings();

        public UnboundGestureMappings()
        {
            Editor.Selection.Apply(SelectionGestures.None);
            ItemContainer.Selection.Apply(SelectionGestures.None);
            Connection.Disconnect.Value = MultiGesture.None;
            Connector.Connect.Value = MultiGesture.None;
        }
    }
}

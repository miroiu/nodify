using System.Windows.Input;

namespace Nodify.Playground
{
    public enum EditorInputMode
    {
        Default,
        PanOnly,
        SelectOnly
    }

    public enum EditorInputMappings
    {
        Default,
        Blender3D
    }

    public static class EditorInputModeExtensions
    {
        public static void Apply(this EditorGestures mappings, EditorInputMode inputMode)
        {
            mappings.Apply(PlaygroundSettings.Instance.EditorInputMappings.ToInputMappings());

            switch (inputMode)
            {
                case EditorInputMode.PanOnly:
                    mappings.Editor.Selection.Apply(EditorGestures.SelectionGestures.None);
                    mappings.ItemContainer.Selection.Apply(EditorGestures.SelectionGestures.None);
                    mappings.ItemContainer.Drag.Value = MultiGesture.None;
                    mappings.Connector.Connect.Value = MultiGesture.None;
                    break;
                case EditorInputMode.SelectOnly:
                    mappings.Editor.Pan.Value = MultiGesture.None;
                    mappings.ItemContainer.Drag.Value = MultiGesture.None;
                    mappings.Connector.Connect.Value = MultiGesture.None;
                    break;
                case EditorInputMode.Default:
                    break;
            }
        }

        public static void Apply(this EditorGestures value, EditorInputMappings mappings)
        {
            var newMappings = mappings.ToInputMappings();
            value.Apply(newMappings);
        }

        public static EditorGestures ToInputMappings(this EditorInputMappings mappings)
        {
            return mappings switch
            {
                EditorInputMappings.Blender3D => new Blender3DInputMappings(),
                _ => new EditorGestures()
            };
        }
    }

    public class Blender3DInputMappings : EditorGestures
    {
        public Blender3DInputMappings()
        {
            Editor.Pan.Value = new AnyGesture(new MouseGesture(MouseAction.LeftClick), new MouseGesture(MouseAction.MiddleClick));
            Editor.Selection.Apply(new SelectionGestures(MouseAction.RightClick));
            // comment to drag with right click - we copy the default gestures of the item container which uses left click for selection
            ItemContainer.Drag.Value = new AnyGesture(ItemContainer.Selection.Replace.Value, ItemContainer.Selection.Remove.Value, ItemContainer.Selection.Append.Value, ItemContainer.Selection.Invert.Value);
            ItemContainer.Selection.Apply(Editor.Selection);
        }
    }
}

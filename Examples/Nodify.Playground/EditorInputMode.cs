using Nodify.Interactivity;
using System.Windows.Input;

namespace Nodify.Playground
{
    public enum EditorInputMode
    {
        Default,
        PanOnly,
        SelectOnly,
        CutOnly
    }

    public enum EditorGesturesMappings
    {
        Default,
        Custom
    }

    public static class EditorInputModeExtensions
    {
        public static void Apply(this EditorGestures mappings, EditorInputMode inputMode)
        {
            mappings.Apply(PlaygroundSettings.Instance.EditorGesturesMappings.ToGesturesMappings());

            switch (inputMode)
            {
                case EditorInputMode.PanOnly:
                    mappings.Editor.Selection.Unbind();
                    mappings.Editor.Cutting.Unbind();
                    mappings.ItemContainer.Selection.Unbind();
                    mappings.ItemContainer.Drag.Unbind();
                    mappings.Connector.Connect.Unbind();
                    break;
                case EditorInputMode.SelectOnly:
                    mappings.Editor.Pan.Unbind();
                    mappings.Editor.Cutting.Unbind();
                    mappings.ItemContainer.Drag.Unbind();
                    mappings.Connector.Connect.Unbind();
                    break;
                case EditorInputMode.CutOnly:
                    mappings.Editor.Cutting.Value = new Interactivity.MouseGesture(MouseAction.LeftClick);
                    mappings.Editor.Selection.Unbind();
                    mappings.Editor.Pan.Unbind();
                    mappings.ItemContainer.Selection.Unbind();
                    mappings.ItemContainer.Drag.Unbind();
                    mappings.Connector.Connect.Unbind();
                    break;
                case EditorInputMode.Default:
                    break;
            }
        }

        public static void Apply(this EditorGestures value, EditorGesturesMappings mappings)
        {
            var newMappings = mappings.ToGesturesMappings();
            value.Apply(newMappings);
        }

        public static EditorGestures ToGesturesMappings(this EditorGesturesMappings mappings)
        {
            return mappings switch
            {
                EditorGesturesMappings.Custom => new CustomGesturesMappings(),
                _ => new EditorGestures()
            };
        }
    }

    public class CustomGesturesMappings : EditorGestures
    {
        public CustomGesturesMappings()
        {
            Editor.Pan.Value = new AnyGesture(new Interactivity.MouseGesture(MouseAction.LeftClick), new Interactivity.MouseGesture(MouseAction.MiddleClick));
            Editor.ZoomModifierKey = ModifierKeys.Control;
            Editor.Selection.Apply(new SelectionGestures(MouseAction.RightClick));
            // comment to drag with right click - we copy the default gestures of the item container which uses left click for selection
            ItemContainer.Drag.Value = new AnyGesture(ItemContainer.Selection.Replace.Value, ItemContainer.Selection.Remove.Value, ItemContainer.Selection.Append.Value, ItemContainer.Selection.Invert.Value);
            ItemContainer.Selection.Apply(Editor.Selection);
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class MinimapState
    {
        public class KeyboardNavigation : InputElementState<Minimap>
        {
            public KeyboardNavigation(Minimap element) : base(element)
            {
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                if (Element.IsKeyboardFocused)
                {
                    var gestures = EditorGestures.Mappings.Minimap;

                    if (gestures.Pan.TryGetNavigationDirection(e, out var panDirection))
                    {
                        var panning = new Vector(-panDirection.X * Minimap.NavigationStepSize, panDirection.Y * Minimap.NavigationStepSize);
                        Element.UpdatePanning(panning);
                        e.Handled = true;
                    }
                    else if (gestures.ResetViewport.Matches(e.Source, e))
                    {
                        Element.ResetViewport();
                        e.Handled = true;
                    }
                }
            }
        }
    }
}

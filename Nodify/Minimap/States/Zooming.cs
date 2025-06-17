using System;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class MinimapState
    {
        /// <summary>
        /// Represents the zooming state of the <see cref="Minimap"/>.
        /// This state handles zooming operations using the mouse wheel with an optional modifier key.
        /// </summary>
        public class Zooming : InputElementState<Minimap>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Zooming"/> class.
            /// </summary>
            /// <param name="minimap">The <see cref="Minimap"/> associated with this state.</param>
            public Zooming(Minimap minimap) : base(minimap)
            {
            }

            protected override void OnMouseWheel(MouseWheelEventArgs e)
            {
                if (!Element.IsReadOnly && EditorGestures.Mappings.Minimap.ZoomModifierKey == Keyboard.Modifiers)
                {
                    double zoom = Math.Pow(2.0, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
                    Element.ZoomAtPosition(zoom, Element.MouseLocation);
                    e.Handled = true;
                }
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                var gestures = EditorGestures.Mappings.Minimap;

                if (!Element.IsReadOnly)
                {
                    if (gestures.ZoomIn.Matches(e.Source, e))
                    {
                        Element.ZoomIn();
                        e.Handled = true;
                    }
                    else if (gestures.ZoomOut.Matches(e.Source, e))
                    {
                        Element.ZoomOut();
                        e.Handled = true;
                    }
                }
            }
        }
    }
}

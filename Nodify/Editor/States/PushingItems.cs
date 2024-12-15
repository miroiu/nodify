using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        /// <summary>
        /// Represents the state of the <see cref="NodifyEditor"/> during a "push items" operation, allowing users to move items within the editor by dragging.
        /// </summary>
        public class PushingItems : DragState<NodifyEditor>
        {
            protected override bool HasContextMenu => Element.HasContextMenu;
            protected override bool CanBegin => !Element.IsSelecting && !Element.IsPanning && !Element.IsCutting;
            protected override bool CanCancel => NodifyEditor.AllowPushItemsCancellation;
            protected override bool IsToggle => EnableToggledPushingItemsMode;

            private Point _prevPosition;

            /// <summary>
            /// Initializes a new instance of the <see cref="PushingItems"/> class.
            /// </summary>
            /// <param name="editor">The <see cref="NodifyEditor"/> associated with this state.</param>
            public PushingItems(NodifyEditor editor)
                : base(editor, EditorGestures.Mappings.Editor.PushItems, EditorGestures.Mappings.Editor.CancelAction)
            {
            }

            protected override void OnBegin(InputEventArgs e)
                => _prevPosition = Element.MouseLocation;

            protected override void OnMouseMove(MouseEventArgs e)
            {
                if (Element.IsPushingItems)
                {
                    Element.UpdatePushedArea(Element.MouseLocation - _prevPosition);
                    _prevPosition = Element.MouseLocation;
                }
                else
                {
                    if (Math.Abs(Element.MouseLocation.X - _prevPosition.X) >= NodifyEditor.MouseActionSuppressionThreshold)
                    {
                        Element.BeginPushingItems(_prevPosition, Orientation.Horizontal);
                    }
                    else if (Math.Abs(Element.MouseLocation.Y - _prevPosition.Y) >= NodifyEditor.MouseActionSuppressionThreshold)
                    {
                        Element.BeginPushingItems(_prevPosition, Orientation.Vertical);
                    }
                }
            }

            protected override void OnEnd(InputEventArgs e)
                => Element.EndPushingItems();

            protected override void OnCancel(InputEventArgs e)
                => Element.CancelPushingItems();
        }
    }
}

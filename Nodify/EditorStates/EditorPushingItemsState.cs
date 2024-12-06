using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    public class EditorPushingItemsState : ElementOperationState<NodifyEditor>
    {
        protected override bool HasContextMenu => Element.HasContextMenu;

        private Point _prevPosition;

        public EditorPushingItemsState(NodifyEditor editor)
            : base(editor, EditorGestures.Mappings.Editor.PushItems, EditorGestures.Mappings.Editor.CancelAction)
        {
        }

        protected override void OnBegin(InputEventArgs e) 
            => _prevPosition = Element.MouseLocation;

        /// <inheritdoc />
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

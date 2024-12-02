using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>The default state of the <see cref="ItemContainer"/>.</summary>
    public class ContainerDefaultState : ContainerState
    {
        private Point _initialPosition;
        private SelectionType? _selectionType;
        private bool _isDragging;

        /// <summary>Creates a new instance of the <see cref="ContainerDefaultState"/>.</summary>
        /// <param name="container">The owner of the state.</param>
        public ContainerDefaultState(ItemContainer container) : base(container)
        {
        }

        /// <inheritdoc />
        public override void ReEnter(ContainerState from)
        {
            _isDragging = false;
            _selectionType = null;
            _initialPosition = Editor.MouseLocation;
        }

        /// <inheritdoc />
        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;
            if (gestures.Drag.Matches(e.Source, e))
            {
                _isDragging = Container.IsDraggable;
            }

            if (gestures.Selection.Select.Matches(e.Source, e))
            {
                _selectionType = gestures.Selection.GetSelectionType(e);
            }

            _initialPosition = Editor.MouseLocation;
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
        {
            double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
            double dragDistance = (Editor.MouseLocation - _initialPosition).LengthSquared;

            if (_isDragging && (dragDistance > dragThreshold))
            {
                if (!Container.IsSelected)
                {
                    var selectionType = GetSelectionTypeForDragging(_selectionType);
                    Container.Select(selectionType);
                }

                PushState(new ContainerDraggingState(Container));
            }
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            if (_selectionType.HasValue)
            {
                // Determine whether the current selection should remain intact or be replaced by the clicked item. 
                // If the right mouse button is pressed on an already selected item, and the item either has an 
                // explicit context menu or is configured to preserve the selection on right-click, the selection 
                // remains unchanged. This ensures that the context menu applies to the entire selection rather 
                // than only the clicked item.
                bool hasContextMenu = Container.HasContextMenu || ItemContainer.PreserveSelectionOnRightClick;
                bool allowContextMenu = e.ChangedButton == MouseButton.Right && Container.IsSelected && hasContextMenu;
                if (!(_selectionType == SelectionType.Replace && allowContextMenu))
                {
                    Container.Select(_selectionType.Value);
                }
            }

            _isDragging = false;
            _selectionType = null;
        }

        private static SelectionType GetSelectionTypeForDragging(SelectionType? selectionType)
        {
            // Always select the container when dragging
            return selectionType == SelectionType.Remove
                ? SelectionType.Replace
                : selectionType.GetValueOrDefault(SelectionType.Replace);
        }
    }
}

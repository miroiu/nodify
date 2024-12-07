using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nodify
{
    public partial class NodifyEditor
    {
        #region Dependency properties

        public static readonly DependencyProperty ItemsDragStartedCommandProperty = DependencyProperty.Register(nameof(ItemsDragStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty ItemsDragCompletedCommandProperty = DependencyProperty.Register(nameof(ItemsDragCompletedCommand), typeof(ICommand), typeof(NodifyEditor));

        protected static readonly DependencyPropertyKey IsDraggingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsDragging), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnIsDraggingChanged));
        public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

        private static void OnIsDraggingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;

            if ((bool)e.NewValue == true)
            {
                editor.OnItemsDragStarted();
            }
            else
            {
                editor.OnItemsDragCompleted();
            }
        }

        private void OnItemsDragCompleted()
        {
            if (ItemsDragCompletedCommand?.CanExecute(DataContext) ?? false)
                ItemsDragCompletedCommand.Execute(DataContext);
        }

        private void OnItemsDragStarted()
        {
            if (ItemsDragStartedCommand?.CanExecute(DataContext) ?? false)
                ItemsDragStartedCommand.Execute(DataContext);
        }

        /// <summary>
        /// Invoked when a drag operation starts for the <see cref="SelectedContainers"/>, or when <see cref="IsPushingItems"/> is set to true.
        /// </summary>
        public ICommand? ItemsDragStartedCommand
        {
            get => (ICommand?)GetValue(ItemsDragStartedCommandProperty);
            set => SetValue(ItemsDragStartedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when a drag operation is completed for the <see cref="SelectedContainers"/>, or when <see cref="IsPushingItems"/> is set to false.
        /// </summary>
        public ICommand? ItemsDragCompletedCommand
        {
            get => (ICommand?)GetValue(ItemsDragCompletedCommandProperty);
            set => SetValue(ItemsDragCompletedCommandProperty, value);
        }

        /// <summary>
        /// Gets a value that indicates whether a dragging operation is in progress.
        /// </summary>
        public bool IsDragging
        {
            get => (bool)GetValue(IsDraggingProperty);
            private set => SetValue(IsDraggingPropertyKey, value);
        }

        #endregion

        /// <summary>
        /// Gets or sets whether cancelling a dragging operation is allowed.
        /// </summary>
        public static bool AllowDraggingCancellation { get; set; } = true;

        /// <summary>
        /// Gets or sets if the current position of containers that are being dragged should not be committed until the end of the dragging operation.
        /// </summary>
        public static bool EnableDraggingContainersOptimizations { get; set; } = true;

        private IDraggingStrategy? _draggingStrategy;

        /// <summary>
        /// Initiates the dragging operation using the currently selected <see cref="ItemContainer" />s.
        /// </summary>
        /// <remarks>This method has no effect if a dragging operation is already in progress.</remarks>
        public void BeginDragging()
            => BeginDragging(SelectedContainers);

        /// <summary>
        /// Initiates the dragging operation for the specified <see cref="ItemContainer" />s.
        /// </summary>
        /// <param name="containers">The collection of item containers to be dragged.</param>
        /// <remarks>This method has no effect if a dragging operation is already in progress.</remarks>
        public void BeginDragging(IEnumerable<ItemContainer> containers)
        {
            if (IsDragging)
            {
                return;
            }

            IsDragging = true;
            _draggingStrategy = CreateDraggingStrategy(containers);
        }

        /// <summary>
        /// Updates the position of the items being dragged by a specified offset.
        /// </summary>
        /// <param name="amount">The vector by which to adjust the position of the dragged items.</param>
        /// <remarks>
        /// This method adjusts the items positions incrementally. It should only be called while a dragging operation is in progress (see <see cref="BeginDragging" />).
        /// </remarks>
        public void UpdateDragging(Vector amount)
        {
            Debug.Assert(IsDragging);
            _draggingStrategy!.Update(amount);
        }

        /// <summary>
        /// Completes the dragging operation, finalizing the position of the dragged items.
        /// </summary>
        /// <remarks>This method has no effect if there's no dragging operation in progress.</remarks>
        public void EndDragging()
        {
            if (!IsDragging)
            {
                return;
            }

            IsBulkUpdatingItems = true;
            _draggingStrategy!.End();
            IsBulkUpdatingItems = false;

            // Draw the containers at the new position.
            ItemsHost.InvalidateArrange();

            _draggingStrategy = null;
            IsDragging = false;
        }

        /// <summary>
        /// Cancels the ongoing dragging operation, reverting any changes made to the positions of the dragged items if <see cref="AllowDraggingCancellation"/> is true.
        /// Otherwise, it ends the dragging operation by calling <see cref="EndDragging"/>.
        /// </summary>
        /// <remarks>This method has no effect if there's no dragging operation in progress.</remarks>
        public void CancelDragging()
        {
            if (!AllowDraggingCancellation)
            {
                EndDragging();
                return;
            }

            if (IsDragging)
            {
                _draggingStrategy!.Abort();
                IsDragging = false;
            }
        }

        private IDraggingStrategy CreateDraggingStrategy(IEnumerable<ItemContainer> containers)
        {
            if (EnableDraggingContainersOptimizations)
            {
                return new DraggingOptimized(containers, GridCellSize);
            }

            return new DraggingSimple(containers, GridCellSize);
        }
    }
}

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Nodify
{
    [StyleTypedProperty(Property = nameof(PushedAreaStyle), StyleTargetType = typeof(Rectangle))]
    public partial class NodifyEditor
    {
        public static readonly DependencyProperty PushedAreaStyleProperty = DependencyProperty.Register(nameof(PushedAreaStyle), typeof(Style), typeof(NodifyEditor));

        protected static readonly DependencyPropertyKey PushedAreaPropertyKey = DependencyProperty.RegisterReadOnly(nameof(PushedArea), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));
        public static readonly DependencyProperty PushedAreaProperty = PushedAreaPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey IsPushingItemsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPushingItems), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnIsPushingItemsChanged));
        public static readonly DependencyProperty IsPushingItemsProperty = IsPushingItemsPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey PushedAreaOrientationPropertyKey = DependencyProperty.RegisterReadOnly(nameof(PushedAreaOrientation), typeof(Orientation), typeof(NodifyEditor), new FrameworkPropertyMetadata(Orientation.Horizontal));
        public static readonly DependencyProperty PushedAreaOrientationProperty = PushedAreaOrientationPropertyKey.DependencyProperty;

        private static void OnIsPushingItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;

            if ((bool)e.NewValue == true)
            {
                editor.OnItemsPushStarted();
            }
            else
            {
                editor.OnItemsPushCompleted();
            }
        }

        private void OnItemsPushCompleted()
        {
            if (ItemsDragCompletedCommand?.CanExecute(DataContext) ?? false)
                ItemsDragCompletedCommand.Execute(DataContext);
        }

        private void OnItemsPushStarted()
        {
            if (ItemsDragStartedCommand?.CanExecute(DataContext) ?? false)
                ItemsDragStartedCommand.Execute(DataContext);
        }

        /// <summary>
        /// Gets the currently pushed area while <see cref="IsPushingItems"/> is true.
        /// </summary>
        public Rect PushedArea
        {
            get => (Rect)GetValue(PushedAreaProperty);
            private set => SetValue(PushedAreaPropertyKey, value);
        }

        /// <summary>
        /// Gets a value that indicates whether a pushing operation is in progress.
        /// </summary>
        public bool IsPushingItems
        {
            get => (bool)GetValue(IsPushingItemsProperty);
            private set => SetValue(IsPushingItemsPropertyKey, value);
        }

        /// <summary>
        /// Gets the orientation of the <see cref="PushedArea"/>.
        /// </summary>
        public Orientation PushedAreaOrientation
        {
            get => (Orientation)GetValue(PushedAreaOrientationProperty);
            private set => SetValue(PushedAreaOrientationPropertyKey, value);
        }

        /// <summary>
        /// Gets or sets the style to use for the pushed area.
        /// </summary>
        public Style PushedAreaStyle
        {
            get => (Style)GetValue(PushedAreaStyleProperty);
            set => SetValue(PushedAreaStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets whether push items cancellation is allowed (see <see cref="EditorGestures.NodifyEditorGestures.CancelAction"/>).
        /// </summary>
        public static bool AllowPushItemsCancellation { get; set; } = true;

        private IPushStrategy? _pushStrategy;

        /// <summary>
        /// Starts the pushing items operation at the specified location with the specified orientation.
        /// </summary>
        /// <remarks>This method has no effect if a pushing operation is already in progress.</remarks>
        /// <param name="location">The starting location for pushing items, in graph space coordinates.</param>
        /// <param name="orientation">The orientation of the <see cref="PushedArea"/>.</param>
        public void BeginPushingItems(Point location, Orientation orientation)
        {
            if(IsPushingItems)
            {
                return;
            }

            IsPushingItems = true;
            PushedAreaOrientation = orientation;

            _pushStrategy = CreatePushStrategy(orientation);

            PushedArea = _pushStrategy.Start(location);
        }

        /// <summary>
        /// Cancels the current pushing operation and reverts the <see cref="PushedArea"/> to its initial state if <see cref="AllowPushItemsCancellation"/> is true.
        /// </summary>
        /// <remarks>This method has no effect if there's no pushing operation in progress.</remarks>
        public void CancelPushingItems()
        {
            if (!AllowPushItemsCancellation || !IsPushingItems)
            {
                return;
            }

            PushedArea = _pushStrategy!.Cancel();
            IsPushingItems = false;
        }

        /// <summary>
        /// Updates the pushed area based on the specified amount taking the <see cref="PushedAreaOrientation"/> into account.
        /// </summary>
        /// <param name="amount">The amount to adjust the pushed area by.</param>
        /// <remarks>
        /// This method adjusts the pushed area incrementally. It should only be called while a pushing operation is active (see <see cref="BeginPushingItems(Point, Orientation)"/>).
        /// </remarks>
        public void UpdatePushedArea(Vector amount)
        {
            Debug.Assert(IsPushingItems);
            PushedArea = _pushStrategy!.Push(amount);
        }

        /// <summary>
        /// Ends the current pushing operation and finalizes the pushed area state.
        /// </summary>
        /// <remarks>This method has no effect if there's no pushing operation in progress.</remarks>
        public void EndPushingItems()
        {
            if (!IsPushingItems)
            {
                return;
            }

            PushedArea = _pushStrategy!.End();
            _pushStrategy = null;
            IsPushingItems = false;
        }

        private void UpdatePushedArea()
        {
            if (IsPushingItems)
            {
                PushedArea = _pushStrategy!.OnViewportChanged();
            }
        }

        private IPushStrategy CreatePushStrategy(Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                return new HorizontalPushStrategy(this);
            }

            return new VerticalPushStrategy(this);
        }
    }
}

using System.Diagnostics;
using System.Windows;
using System;
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

        protected static readonly DependencyPropertyKey PushOrientationPropertyKey = DependencyProperty.RegisterReadOnly(nameof(PushOrientation), typeof(Orientation), typeof(NodifyEditor), new FrameworkPropertyMetadata(Orientation.Horizontal));
        public static readonly DependencyProperty PushOrientationProperty = PushOrientationPropertyKey.DependencyProperty;

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
            internal set => SetValue(PushedAreaPropertyKey, value);
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
        public Orientation PushOrientation
        {
            get => (Orientation)GetValue(PushOrientationProperty);
            private set => SetValue(PushOrientationPropertyKey, value);
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
        /// Gets or sets whether push items cancellation is allowed.
        /// </summary>
        public static bool AllowPushItemsCancellation { get; set; } = true;

        private IPushStrategy? _pushStrategy;

        protected internal void StartPushingItems(Point position, Orientation orientation)
        {
            IsPushingItems = true;
            PushOrientation = orientation;

            if (orientation == Orientation.Horizontal)
            {
                _pushStrategy = new HorizontalPushStrategy(this);
            }
            else
            {
                _pushStrategy = new VerticalPushStrategy(this);
            }

            _pushStrategy.Start(position);
        }

        protected internal void CancelPushingItems()
        {
            if (!AllowPushItemsCancellation)
                throw new InvalidOperationException("Push items cancellation is not allowed");

            Debug.Assert(IsPushingItems);
            if (IsPushingItems)
            {
                _pushStrategy?.Cancel();
                IsPushingItems = false;
            }
        }

        protected internal void PushItems(Vector offset)
        {
            if (IsPushingItems)
            {
                _pushStrategy?.Push(offset);
            }
        }

        protected internal void EndPushingItems()
        {
            Debug.Assert(IsPushingItems);
            if (IsPushingItems)
            {
                _pushStrategy?.End();
                IsPushingItems = false;
            }
        }

        private void UpdatePushedArea()
        {
            if (IsPushingItems)
            {
                _pushStrategy?.OnViewportChanged();
            }
        }
    }
}

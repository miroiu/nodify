using System.Diagnostics;
using System.Linq;
using System.Windows;
using System;
using System.Windows.Controls;

namespace Nodify
{
    [StyleTypedProperty(Property = nameof(PushedAreaStyle), StyleTargetType = typeof(Border))]
    public partial class NodifyEditor
    {
        public static readonly DependencyProperty PushedAreaStyleProperty = DependencyProperty.Register(nameof(PushedAreaStyle), typeof(Style), typeof(NodifyEditor));

        protected static readonly DependencyPropertyKey PushedAreaPropertyKey = DependencyProperty.RegisterReadOnly(nameof(PushedArea), typeof(Rect), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Rect));
        public static readonly DependencyProperty PushedAreaProperty = PushedAreaPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey IsPushingItemsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPushingItems), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnIsPushingItemsChanged));
        public static readonly DependencyProperty IsPushingItemsProperty = IsPushingItemsPropertyKey.DependencyProperty;

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
            internal set => SetValue(IsPushingItemsPropertyKey, value);
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

        private const int _pushAreaOffscreenOffsetY = 100;
        private const int _pushAreaMinWidth = 2;
        private double _pushAreaInitialX;
        private double _pushedAreaWidth;

        protected internal void StartPushingItems(Point location)
        {
            IsPushingItems = true;
            PushedArea = new Rect(location.X, ViewportLocation.Y, 0d, ViewportSize.Height);

            _draggingStrategy = CreateDraggingStrategy(ItemContainers.Where(item => item.Location.X >= location.X));
            _pushAreaInitialX = PushedArea.X;
            _pushedAreaWidth = 0;
        }

        protected internal void CancelPushingItems()
        {
            if (!AllowPushItemsCancellation)
                throw new InvalidOperationException("Push items cancellation is not allowed");

            Debug.Assert(IsPushingItems);
            if (IsPushingItems)
            {
                _draggingStrategy?.Abort();
                IsPushingItems = false;
            }
        }

        protected internal void PushItems(double offset)
        {
            if (IsPushingItems)
            {
                _draggingStrategy?.Update(new Vector(offset, 0));
                _pushedAreaWidth += offset;

                double newStart = _pushedAreaWidth >= 0 ? _pushAreaInitialX : SnapToGrid(_pushAreaInitialX + _pushedAreaWidth);
                double newWidth = Math.Max(_pushAreaMinWidth, SnapToGrid(_pushedAreaWidth));

                PushedArea = new Rect(newStart, ViewportLocation.Y - _pushAreaOffscreenOffsetY, newWidth, ViewportSize.Height + _pushAreaOffscreenOffsetY * 2);
            }
        }

        protected internal void EndPushingItems(Point location)
        {
            Debug.Assert(IsPushingItems);
            if (IsPushingItems)
            {
                _draggingStrategy?.End();
                IsPushingItems = false;
            }
        }

        private void UpdatePushedArea()
        {
            if (IsPushingItems)
            {
                PushedArea = new Rect(PushedArea.X, ViewportLocation.Y - _pushAreaOffscreenOffsetY, PushedArea.Width, ViewportSize.Height + _pushAreaOffscreenOffsetY * 2);
            }
        }
    }
}

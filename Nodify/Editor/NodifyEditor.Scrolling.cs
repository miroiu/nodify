using System.Windows.Controls;
using System.Windows;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Input;

namespace Nodify
{
    public partial class NodifyEditor : IScrollInfo
    {
        /// <summary>
        /// The number of units the mouse wheel is rotated to scroll one line.
        /// </summary>
        public static double ScrollIncrement { get; set; } = Mouse.MouseWheelDeltaForOneLine / 2;

        bool IScrollInfo.CanHorizontallyScroll { get; set; }
        bool IScrollInfo.CanVerticallyScroll { get; set; }

        private double _extentWidth;
        double IScrollInfo.ExtentWidth => _extentWidth;

        private double _extentHeight;
        double IScrollInfo.ExtentHeight => _extentHeight;

        private double _horizontalOffset;
        double IScrollInfo.HorizontalOffset => _horizontalOffset;

        private double _verticalOffset;
        double IScrollInfo.VerticalOffset => _verticalOffset;

        double IScrollInfo.ViewportWidth => ViewportSize.Width;
        double IScrollInfo.ViewportHeight => ViewportSize.Height;

        ScrollViewer? IScrollInfo.ScrollOwner { get; set; }

        private IScrollInfo ScrollInfo => this;
        private Point? _viewportLocationBeforeScrolling;
        private bool _isScrolling;

        void IScrollInfo.LineUp()
            => ViewportLocation -= new Vector(0, ScrollIncrement / ViewportZoom);

        void IScrollInfo.LineDown()
            => ViewportLocation += new Vector(0, ScrollIncrement / ViewportZoom);

        void IScrollInfo.LineLeft()
            => ViewportLocation -= new Vector(ScrollIncrement / ViewportZoom, 0);

        void IScrollInfo.LineRight()
            => ViewportLocation += new Vector(ScrollIncrement / ViewportZoom, 0);

        void IScrollInfo.MouseWheelUp() => ScrollInfo.LineUp();
        void IScrollInfo.MouseWheelDown() => ScrollInfo.LineDown();
        void IScrollInfo.MouseWheelLeft() => ScrollInfo.LineLeft();
        void IScrollInfo.MouseWheelRight() => ScrollInfo.LineRight();

        void IScrollInfo.PageUp()
            => ViewportLocation = new Point(ViewportLocation.X, ViewportLocation.Y - ViewportSize.Height);

        void IScrollInfo.PageDown()
            => ViewportLocation = new Point(ViewportLocation.X, ViewportLocation.Y + ViewportSize.Height);

        void IScrollInfo.PageLeft()
            => ViewportLocation = new Point(ViewportLocation.X - ViewportSize.Width, ViewportLocation.Y);

        void IScrollInfo.PageRight()
            => ViewportLocation = new Point(ViewportLocation.X + ViewportSize.Width, ViewportLocation.Y);

        Rect IScrollInfo.MakeVisible(Visual visual, Rect rectangle)
        {
            // This is called when clicking on an item container. Uncomment to automatically scroll to the selected item container.
            //if (visual is ItemContainer container)
            //{
            //    var containerBounds = new Rect(container.Location, container.RenderSize);
            //    if (!new Rect(ViewportLocation, ViewportSize).Contains(containerBounds))
            //    {
            //        BringIntoView(containerBounds);
            //        return containerBounds;
            //    }
            //}

            return rectangle;
        }

        void IScrollInfo.SetHorizontalOffset(double offset)
        {
            _horizontalOffset = double.IsInfinity(offset) ? 0d : offset;
            UpdateViewportLocationOnScroll();
        }

        void IScrollInfo.SetVerticalOffset(double offset)
        {
            _verticalOffset = double.IsInfinity(offset) ? 0d : offset;
            UpdateViewportLocationOnScroll();
        }

        private void UpdateViewportLocationOnScroll()
        {
            if (!_viewportLocationBeforeScrolling.HasValue)
            {
                _viewportLocationBeforeScrolling = ViewportLocation;
            }

            _isScrolling = true;

            double locationX = Math.Min(ItemsExtent.Left, _viewportLocationBeforeScrolling.Value.X) + ScrollInfo.HorizontalOffset;
            double locationY = Math.Min(ItemsExtent.Top, _viewportLocationBeforeScrolling.Value.Y) + ScrollInfo.VerticalOffset;
            ViewportLocation = new Point(locationX, locationY);

            ScrollInfo.ScrollOwner?.InvalidateScrollInfo();
            _isScrolling = false;
        }

        private void UpdateScrollbars()
        {
            // setting the ViewportLocation when manually scrolling triggers the ViewportUpdatedEvent which in turn calls this method, hence the !_isScrolling check
            if (ScrollInfo.ScrollOwner != null && !_isScrolling)
            {
                _viewportLocationBeforeScrolling = null;

                var extent = ItemsExtent;
                extent.Union(new Rect(ViewportLocation, ViewportSize));

                _extentHeight = extent.Height;
                _extentWidth = extent.Width;

                var scrollOffset = ViewportLocation - ItemsExtent.Location;

                _horizontalOffset = Math.Max(0, scrollOffset.X);
                _verticalOffset = Math.Max(0, scrollOffset.Y);

                ScrollInfo.ScrollOwner.InvalidateScrollInfo();
            }
        }
    }
}

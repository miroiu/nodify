using System;
using System.Linq;
using System.Windows;

namespace Nodify
{
    internal interface IPushStrategy
    {
        void Start(Point position);
        void Push(Vector offset);
        void End();
        void Cancel();
        void OnViewportChanged();
    }

    internal class HorizontalPushStrategy : IPushStrategy
    {
        private const int _offscreenOffsetY = 100;
        private const int _minWidth = 2;
        private double _initialPosition;
        private double _actualWidth;

        private readonly NodifyEditor _editor;
        private IDraggingStrategy? _draggingStrategy;

        public HorizontalPushStrategy(NodifyEditor editor)
        {
            _editor = editor;
        }

        public void Start(Point position)
        {
            _draggingStrategy = _editor.CreateDraggingStrategy(_editor.ItemContainers.Where(item => item.Location.X >= position.X));

            _initialPosition = position.X;
            _actualWidth = 0;
            _editor.PushedArea = new Rect(position.X, _editor.ViewportLocation.Y - _offscreenOffsetY, 0d, _editor.ViewportSize.Height + _offscreenOffsetY * 2);
        }

        public void Push(Vector offset)
        {
            _draggingStrategy?.Update(new Vector(offset.X, 0));

            _actualWidth += offset.X;

            double newStart = _actualWidth >= 0 ? _initialPosition : _editor.SnapToGrid(_initialPosition + _actualWidth);
            double newWidth = Math.Max(_minWidth, _editor.SnapToGrid(_actualWidth));

            _editor.PushedArea = new Rect(newStart, _editor.ViewportLocation.Y - _offscreenOffsetY, newWidth, _editor.ViewportSize.Height + _offscreenOffsetY * 2);
        }

        public void End()
        {
            _draggingStrategy?.End();
        }

        public void Cancel()
        {
            _draggingStrategy?.Abort();
        }

        public void OnViewportChanged()
        {
            _editor.PushedArea = new Rect(_editor.PushedArea.X, _editor.ViewportLocation.Y - _offscreenOffsetY, _editor.PushedArea.Width, _editor.ViewportSize.Height + _offscreenOffsetY * 2);
        }
    }

    internal class VerticalPushStrategy : IPushStrategy
    {
        private const int _offscreenOffsetX = 100;
        private const int _minHeight = 2;
        private double _initialPosition;
        private double _actualHeight;

        private readonly NodifyEditor _editor;
        private IDraggingStrategy? _draggingStrategy;

        public VerticalPushStrategy(NodifyEditor editor)
        {
            _editor = editor;
        }

        public void Start(Point position)
        {
            _draggingStrategy = _editor.CreateDraggingStrategy(_editor.ItemContainers.Where(item => item.Location.Y >= position.Y));

            _initialPosition = position.Y;
            _actualHeight = 0;
            _editor.PushedArea = new Rect(_editor.ViewportLocation.X - _offscreenOffsetX, position.Y, _editor.ViewportSize.Width + _offscreenOffsetX * 2, 0d);
        }

        public void Push(Vector offset)
        {
            _draggingStrategy?.Update(new Vector(0, offset.Y));

            _actualHeight += offset.Y;

            double newStart = _actualHeight >= 0 ? _initialPosition : _editor.SnapToGrid(_initialPosition + _actualHeight);
            double newHeight = Math.Max(_minHeight, _editor.SnapToGrid(_actualHeight));

            _editor.PushedArea = new Rect(_editor.ViewportLocation.X - _offscreenOffsetX, newStart, _editor.ViewportSize.Width + _offscreenOffsetX * 2, newHeight);
        }

        public void End()
        {
            _draggingStrategy?.End();
        }

        public void Cancel()
        {
            _draggingStrategy?.Abort();
        }

        public void OnViewportChanged()
        {
            _editor.PushedArea = new Rect(_editor.ViewportLocation.X - _offscreenOffsetX, _editor.PushedArea.Y, _editor.ViewportSize.Width + _offscreenOffsetX * 2, _editor.PushedArea.Height);
        }
    }
}

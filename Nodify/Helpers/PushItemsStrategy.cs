using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify
{
    internal interface IPushStrategy
    {
        Rect Start(Point position);
        Rect Push(Vector amount);
        Rect End();
        Rect Cancel();
        Rect GetPushedArea();
    }

    internal abstract class BasePushStrategy : IPushStrategy
    {
        private const double _minOffset = 2;
        private double _actualOffset;
        private double _initialPosition;

        protected readonly NodifyEditor Editor;
        protected const double OffscreenOffset = 100d;

        public BasePushStrategy(NodifyEditor editor)
        {
            Editor = editor;
        }

        public Rect Start(Point position)
        {
            var containers = GetFilteredContainers(position);
            Editor.BeginDragging(containers);

            _initialPosition = GetInitialPosition(position);
            _actualOffset = 0;

            return CalculatePushedArea(_initialPosition, _actualOffset);
        }

        public Rect Push(Vector amount)
        {
            var offset = GetPushOffset(amount);
            Editor.UpdateDragging(offset);

            _actualOffset += offset.X;
            _actualOffset += offset.Y;

            double newPosition = _actualOffset >= 0 ? _initialPosition : Editor.SnapToGrid(_initialPosition + _actualOffset);
            double newOffset = Math.Max(_minOffset, Editor.SnapToGrid(_actualOffset));

            return CalculatePushedArea(newPosition, newOffset);
        }

        public Rect End()
        {
            Editor.EndDragging();
            return new Rect();
        }

        public Rect Cancel()
        {
            Editor.CancelDragging();
            return new Rect();
        }

        protected abstract IEnumerable<ItemContainer> GetFilteredContainers(Point position);
        protected abstract double GetInitialPosition(Point position);
        protected abstract Vector GetPushOffset(Vector offset);
        protected abstract Rect CalculatePushedArea(double position, double offset);
        public abstract Rect GetPushedArea();
    }

    internal sealed class HorizontalPushStrategy : BasePushStrategy
    {
        public HorizontalPushStrategy(NodifyEditor editor) : base(editor)
        {
        }

        protected override IEnumerable<ItemContainer> GetFilteredContainers(Point position)
            => Editor.ItemContainers.Where(item => item.Location.X >= position.X);

        protected override double GetInitialPosition(Point position)
            => position.X;

        protected override Vector GetPushOffset(Vector offset)
            => new Vector(offset.X, 0d);

        protected override Rect CalculatePushedArea(double position, double offset)
            => new Rect(position, Editor.ViewportLocation.Y - OffscreenOffset, offset, Editor.ViewportSize.Height + OffscreenOffset * 2);

        public override Rect GetPushedArea()
            => CalculatePushedArea(Editor.PushedArea.X, Editor.PushedArea.Width);
    }

    internal sealed class VerticalPushStrategy : BasePushStrategy
    {
        public VerticalPushStrategy(NodifyEditor editor) : base(editor)
        {
        }

        protected override IEnumerable<ItemContainer> GetFilteredContainers(Point position)
            => Editor.ItemContainers.Where(item => item.Location.Y >= position.Y);

        protected override double GetInitialPosition(Point position)
            => position.Y;

        protected override Vector GetPushOffset(Vector offset)
            => new Vector(0d, offset.Y);

        protected override Rect CalculatePushedArea(double position, double offset)
            => new Rect(Editor.ViewportLocation.X - OffscreenOffset, position, Editor.ViewportSize.Width + OffscreenOffset * 2, offset);

        public override Rect GetPushedArea()
            => CalculatePushedArea(Editor.PushedArea.Y, Editor.PushedArea.Height);
    }
}

using System.Windows;

namespace Nodify.Shapes.Controls
{
    internal class ResizableContainer : ResizablePanel
    {
        public static readonly DependencyProperty GridCellSizeProperty = NodifyEditor.GridCellSizeProperty.AddOwner(typeof(ResizableContainer));

        public uint GridCellSize
        {
            get => (uint)GetValue(GridCellSizeProperty);
            set => SetValue(GridCellSizeProperty, value);
        }

        protected override void OnMove(double x, double y)
        {
            // we can't use the default behavior because we are not inside a Canvas
            if (TemplatedParent is ItemContainer item)
            {
                item.Location = new Point(item.Location.X + x, item.Location.Y + y);
            }
        }

        protected override void OnProcessDelta(ref double dx, ref double dy)
        {
            // snap to grid
            dx = (int)dx / GridCellSize * GridCellSize;
            dy = (int)dy / GridCellSize * GridCellSize;
        }
    }
}

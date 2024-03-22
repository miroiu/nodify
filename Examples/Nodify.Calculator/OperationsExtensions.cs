using System.Collections.Generic;
using System.Windows;

namespace Nodify.Calculator
{
    public static class OperationsExtensions
    {
        public static Rect GetBoundingBox(this IEnumerable<OperationViewModel> nodes, double padding = 0, int gridCellSize = 15)
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;

            var maxX = double.MinValue;
            var maxY = double.MinValue;

            const int width = 200; //node.Width
            const int height = 100; //node.Height

            foreach (var node in nodes)
            {
                if (node.Location.X < minX)
                {
                    minX = node.Location.X;
                }

                if (node.Location.Y < minY)
                {
                    minY = node.Location.Y;
                }

                var sizeX = node.Location.X + width;
                if (sizeX > maxX)
                {
                    maxX = sizeX;
                }

                var sizeY = node.Location.Y + height;
                if (sizeY > maxY)
                {
                    maxY = sizeY;
                }
            }

            var result = new Rect(minX - padding, minY - padding, maxX - minX + padding * 2, maxY - minY + padding * 2);
            result = new Rect((int)result.X / gridCellSize * gridCellSize,
                (int)result.Y / gridCellSize * gridCellSize,
                result.Width,
                result.Height);
            return result;
        }
    }
}
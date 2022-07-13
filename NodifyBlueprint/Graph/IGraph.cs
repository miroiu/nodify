using System.Collections.Generic;
using System.Windows;

namespace NodifyBlueprint
{
    public interface IGraph
    {
        IReadOnlyCollection<IGraphElement> Elements { get; }
        IReadOnlyCollection<IGraphElement> SelectedElements { get; }
        IReadOnlyCollection<IConnection> Connections { get; }

        IPendingConnection PendingConnection { get; }

        void AddElement(IGraphElement node);
        void AddElements(IEnumerable<IGraphElement> nodes);
        void RemoveElement(IGraphElement node);
        void RemoveElements(IEnumerable<IGraphElement> nodes);

        void TryConnect(IConnector source, IConnector target);
        void TryConnect(IConnector source, IGraphElement target);
        void Disconnect(IConnector connector);

        void FocusLocation(double x, double y);
    }

    public static class IGraphExtensions
    {
        public static void FocusNode(this IGraph graph, IGraphElement node)
        {
            double midX = (node.Location.X + node.Size.Width / 2);
            double midY = (node.Location.Y + node.Size.Height / 2);
            graph.FocusLocation(midX, midY);
        }

        public static void AddComment(this IGraph graph, string text, IEnumerable<IGraphElement> nodes)
        {
            var bounds = nodes.GetBoundingBox();
            graph.AddElement(new CommentNode(graph)
            {
                Location = bounds.Location,
                CommentSize = bounds.Size,
                Title = text
            });
        }

        private static (Point Location, Size Size) GetBoundingBox(this IEnumerable<IGraphElement> nodes)
        {
            double minX = double.MaxValue;
            double minY = double.MaxValue;

            double maxX = double.MinValue;
            double maxY = double.MinValue;

            foreach (var node in nodes)
            {
                double width = node.Size.Width;
                double height = node.Size.Height;

                if (node.Location.X < minX)
                {
                    minX = node.Location.X;
                }

                if (node.Location.Y < minY)
                {
                    minY = node.Location.Y;
                }

                double sizeX = node.Location.X + width;
                if (sizeX > maxX)
                {
                    maxX = sizeX;
                }

                double sizeY = node.Location.Y + height;
                if (sizeY > maxY)
                {
                    maxY = sizeY;
                }
            }

            int padding = 30;
            var result = (new Point(minX - padding, minY - padding), new Size(maxX - minX + padding * 2, maxY - minY + padding * 2));
            return result;
        }
    }
}
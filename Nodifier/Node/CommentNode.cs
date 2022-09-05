using System.Collections.Generic;
using System.Windows;

namespace Nodifier
{
    public interface ICommentNode : IGraphElement
    {
        string? Title { get; set; }
        bool CanResize { get; set; }
        Size CommentSize { get; set; }

        IReadOnlyList<IGraphElement> GetElements();
    }

    public class CommentNode : GraphElement, ICommentNode
    {
        public CommentNode(IGraph graph) : base(graph)
        {
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }

        private bool _canResize = true;
        public bool CanResize
        {
            get => _canResize;
            set => SetAndNotify(ref _canResize, value);
        }

        private Size _commentSize;
        public Size CommentSize
        {
            get => _commentSize;
            set => SetAndNotify(ref _commentSize, value);
        }

        public IReadOnlyList<IGraphElement> GetElements()
        {
            var elements = new List<IGraphElement>(4);
            var commentRect = new Rect(Location, CommentSize);

            foreach (var element in Graph.Elements)
            {
                var elemRect = new Rect(element.Location, element.Size);
                if (element != this && commentRect.Contains(elemRect))
                {
                    elements.Add(element);
                }
            }

            return elements;
        }
    }
}

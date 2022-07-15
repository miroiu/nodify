using System.Windows;

namespace Nodifier
{
    public interface ICommentNode : IGraphElement
    {
        public string? Title { get; set; }
        public bool CanResize { get; set; }
        public Size CommentSize { get; set; }
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
    }
}

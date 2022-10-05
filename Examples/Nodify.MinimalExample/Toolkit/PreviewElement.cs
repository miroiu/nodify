namespace Nodifier
{
    public interface IPreviewElement
    {
        IGraphElement Element { get; }
    }

    public class PreviewElement : GraphDecorator, IPreviewElement
    {
        public PreviewElement(IGraph graph, IGraphElement element) : base(graph)
        {
            Element = element;
        }

        public IGraphElement Element { get; }
    }
}

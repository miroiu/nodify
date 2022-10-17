namespace Nodifier
{
    public interface IPreviewElement
    {
        IGraphElement Element { get; }
    }

    public class PreviewElement : GraphDecorator, IPreviewElement
    {
        public PreviewElement(IEditor editor, IGraphElement element) : base(editor)
        {
            Element = element;
        }

        public IGraphElement Element { get; }
    }
}

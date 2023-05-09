namespace Nodifier
{
    // TODO: Move connector widget as property
    public class FlowInput : BaseConnector
    {
        public FlowInput(INodeWidget node) : base(node)
        {
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }
    }
}

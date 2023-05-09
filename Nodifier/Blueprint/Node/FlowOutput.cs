namespace Nodifier
{
    public class FlowOutput : BaseConnector
    {
        public FlowOutput(INodeWidget node) : base(node)
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

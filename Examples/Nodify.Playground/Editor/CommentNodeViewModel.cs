namespace Nodify.Playground
{
    public class CommentNodeViewModel : NodeViewModel
    {
        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public CommentNodeViewModel()
        {
            IsResizable = true;
        }
    }
}

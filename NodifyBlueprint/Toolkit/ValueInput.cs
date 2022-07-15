using Stylet;

namespace NodifyBlueprint
{
    public class ValueEditor<T> : PropertyChangedBase
    {
        private T _value;
        public T Value
        {
            get => _value;
            set => SetAndNotify(ref _value, value);
        }
    }

    public class ValueInput<T> : BaseConnector
    {
        public ValueInput(IGraphNode node) : base(node)
        {
        }

        public ValueEditor<T> Editor { get; } = new ValueEditor<T>();

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }

        private bool _hideEditor;
        public bool HideEditor
        {
            get => _hideEditor;
            set => SetAndNotify(ref _hideEditor, value);
        }

        public T Value
        {
            get => Editor.Value;
            set => Editor.Value = value;
        }
    }
}

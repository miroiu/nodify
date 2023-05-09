using System;

namespace Nodifier
{
    public class ValueEditor<T> : Undoable
    {
        public ValueEditor(IActionsHistory history) : base(history)
        {
            RecordProperty(nameof(Value), PropertyFlags.Enable);
        }

        private T _value;
        public T Value
        {
            get => _value;
            set => SetAndNotify(ref _value, value);
        }
    }

    public class ValueInput<T> : BaseConnector
    {
        public ValueInput(INodeWidget node) : base(node)
        {
            Editor = new ValueEditor<T>(History);
            Editor.PropertyChanged += OnEditorPropertyChanged;
        }

        private void OnEditorPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ValueEditor<T>.Value))
            {
                ValueChanged?.Invoke(Editor.Value);
                OnPropertyChanged(nameof(Value));
            }
        }

        public ValueEditor<T> Editor { get; }

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

        public Action<T>? ValueChanged;
    }
}

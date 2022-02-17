using System;
using System.Linq;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public class ApplicationViewModel : ObservableObject
    {
        public NodifyObservableCollection<EditorViewModel> Editors { get; } = new NodifyObservableCollection<EditorViewModel>();

        public ApplicationViewModel()
        {
            AddEditorCommand = new DelegateCommand(() => Editors.Add(new EditorViewModel
            {
                Name = $"Editor {Editors.Count + 1}"
            }));
            CloseEditorCommand = new DelegateCommand<Guid>(
                id => Editors.RemoveOne(editor => editor.Id == id),
                _ => Editors.Count > 0 && SelectedEditor != null);
            Editors.WhenAdded((editor) =>
            {
                if (AutoSelectNewEditor || Editors.Count == 1)
                {
                    SelectedEditor = editor;
                }
                editor.OnOpenInnerCalculator += OnOpenInnerCalculator;
            })
            .WhenRemoved((editor) =>
            {
                editor.OnOpenInnerCalculator -= OnOpenInnerCalculator;
                var childEditors = Editors.Where(ed => ed.Parent == editor).ToList();
                childEditors.ForEach(ed => Editors.Remove(ed));
            });
            Editors.Add(new EditorViewModel
            {
                Name = $"Editor {Editors.Count + 1}"
            });
        }

        private void OnOpenInnerCalculator(EditorViewModel parentEditor, CalculatorViewModel calculator)
        {
            var editor = Editors.FirstOrDefault(e => e.Calculator == calculator);
            if (editor != null)
            {
                SelectedEditor = editor;
            }
            else
            {
                var childEditor = new EditorViewModel
                {
                    Parent = parentEditor,
                    Calculator = calculator,
                    Name = $"[Inner] Editor {Editors.Count + 1}"
                };
                Editors.Add(childEditor);
            }
        }

        public ICommand AddEditorCommand { get; }
        public ICommand CloseEditorCommand { get; }

        private EditorViewModel? _selectedEditor;
        public EditorViewModel? SelectedEditor
        {
            get => _selectedEditor;
            set => SetProperty(ref _selectedEditor, value);
        }

        private bool _autoSelectNewEditor = true;
        public bool AutoSelectNewEditor
        {
            get => _autoSelectNewEditor;
            set => SetProperty(ref _autoSelectNewEditor , value); 
        }
    }
}

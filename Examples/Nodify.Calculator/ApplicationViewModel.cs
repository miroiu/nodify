using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public class ApplicationViewModel : ObservableObject
    {
        public ObservableCollection<EditorViewModel> Editors { get; } = new ObservableCollection<EditorViewModel>();

        public ApplicationViewModel()
        {
            AddEditorCommand = new DelegateCommand(() => 
            {
                Editors.Add(new EditorViewModel() { Name = $"Editor {Editors.Count + 1}"});
                if (AutoSelectNewEditor || Editors.Count == 1)
                {
                    SelectedEditor = Editors.Last();
                }
            });
            CloseEditorCommand = new DelegateCommand<Guid>(
                id => Editors.RemoveOne(app => app.Id == id),
                _ => Editors.Count > 0 && SelectedEditor != null
            );
            AddEditorCommand.Execute(null);
        }
        public ICommand AddEditorCommand { get; }
        public ICommand CloseEditorCommand { get; }

        private EditorViewModel? _selectedEditor;
        public EditorViewModel? SelectedEditor
        {
            get { return _selectedEditor; }
            set { SetProperty(ref _selectedEditor , value); }
        }

        private bool _autoSelectNewEditor = true;
        public bool AutoSelectNewEditor
        {
            get { return _autoSelectNewEditor; }
            set { _autoSelectNewEditor = value; }
        }
    }
}

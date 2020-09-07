using System.Linq;
using System.Windows;

namespace Nodify.StateMachine
{
    public class StateMachineViewModel : ObservableObject
    {
        public StateMachineViewModel()
        {
            PendingTransition = new TransitionViewModel();

            Transitions.WhenAdded(c => c.Source.Transitions.Add(c.Target))
            .WhenRemoved(c => c.Source.Transitions.Remove(c.Target))
            .WhenCleared(c => c.ForEach(i =>
            {
                i.Source.Transitions.Clear();
                i.Target.Transitions.Clear();
            }));

            States.WhenAdded(x => x.Graph = this)
                 .WhenRemoved(x => DisconnectState(x))
                 .WhenCleared(x =>
                 {
                     Transitions.Clear();
                     OnCreateDefaultNodes();
                 });

            OnCreateDefaultNodes();

            SelectAllCommand = new DelegateCommand(() => SelectedStates.AddRange(States), () => States.Count > 0);
            RenameStateCommand = new RequeryCommand(() => SelectedStates[0].IsRenaming = true, () => SelectedStates.Count == 1 && SelectedStates[0].IsEditable);
            DisconnectStateCommand = new RequeryCommand<StateViewModel>(x => DisconnectState(x), x => x.Transitions.Count > 0);
            DisconnectSelectionCommand = new RequeryCommand(() => SelectedStates.ForEach(x => DisconnectState(x)), () => SelectedStates.Count > 0 && Transitions.Count > 0);
            DeleteSelectionCommand = new RequeryCommand(() => SelectedStates.ToList().ForEach(x => x.IsEditable.Then(() => States.Remove(x))), () => SelectedStates.Count > 1 || (SelectedStates.Count == 1 && SelectedStates[0].IsEditable));

            AddStateCommand = new DelegateCommand<Point>(p => States.Add(new StateViewModel
            {
                Name = "New State",
                IsRenaming = true,
                Location = p
            }));

            CreateTransitionCommand = new DelegateCommand<(object Source, object? Target)>(s => Transitions.Add(new TransitionViewModel
            {
                Source = (StateViewModel)s.Source,
                Target = (StateViewModel)s.Target!
            }), s => s.Target is StateViewModel target && target != s.Source && !target.Transitions.Contains(s.Source));

            DeleteTransitionCommand = new DelegateCommand<TransitionViewModel>(t => Transitions.Remove(t));
        }

        protected virtual void OnCreateDefaultNodes()
        {
            States.Insert(0, new StateViewModel
            {
                Name = "Enter",
                Location = new Point(100, 100),
                IsEditable = false
            });
        }

        public void DisconnectState(StateViewModel state)
        {
            var transitions = Transitions.Where(t => t.Source == state || t.Target == state).ToList();
            transitions.ForEach(t => Transitions.Remove(t));
        }

        private NodifyObservableCollection<StateViewModel> _states = new NodifyObservableCollection<StateViewModel>();
        public NodifyObservableCollection<StateViewModel> States
        {
            get => _states;
            set => SetProperty(ref _states, value);
        }

        private NodifyObservableCollection<StateViewModel> _selectedStates = new NodifyObservableCollection<StateViewModel>();
        public NodifyObservableCollection<StateViewModel> SelectedStates
        {
            get => _selectedStates;
            set => SetProperty(ref _selectedStates, value);
        }

        private NodifyObservableCollection<TransitionViewModel> _connections = new NodifyObservableCollection<TransitionViewModel>();
        public NodifyObservableCollection<TransitionViewModel> Transitions
        {
            get => _connections;
            set => SetProperty(ref _connections, value);
        }

        public TransitionViewModel PendingTransition { get; }

        public INodifyCommand DeleteTransitionCommand { get; }
        public INodifyCommand DeleteSelectionCommand { get; }
        public INodifyCommand DisconnectStateCommand { get; }
        public INodifyCommand DisconnectSelectionCommand { get; }
        public INodifyCommand RenameStateCommand { get; }
        public INodifyCommand AddStateCommand { get; }
        public INodifyCommand CreateTransitionCommand { get; }
        public INodifyCommand SelectAllCommand { get; }
    }
}

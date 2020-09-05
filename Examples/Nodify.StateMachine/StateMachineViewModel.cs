using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.StateMachine
{
    public class StateMachineViewModel : ObservableObject
    {
        public StateMachineViewModel()
        {
            DeleteSelectionCommand = new DelegateCommand(DeleteSelection, () => SelectedStates.Count > 0);
            DisconnectStateCommand = new DelegateCommand<StateViewModel>(x => DisconnectState(x), x => x.Transitions.Count > 0);
            CreateTransitionCommand = new DelegateCommand<(object Source, object? Target)>(s => Transitions.Add(new TransitionViewModel
            {
                Source = (StateViewModel)s.Source,
                Target = (StateViewModel)s.Target!
            }), s => s.Target is StateViewModel target && target != s.Source && !Transitions.Any(t => t.Target == target && t.Source == s.Source));

            PendingTransition = new TransitionViewModel();

            Transitions.WhenAdded(c =>
            {
                c.Source.Transitions.Add(c.Target);
                c.Target.Transitions.Add(c.Source);
            })
            .WhenRemoved(c =>
            {
                c.Source.Transitions.Remove(c.Target);
                c.Target.Transitions.Remove(c.Source);
            });

            States.WhenAdded(x => x.Graph = this)
                 .WhenRemoved(x => DisconnectState(x))
                 .WhenCleared(x => Transitions.Clear());

            States.Add(new StateViewModel
            {
                Name = "ALT + Click on transition",
                Location = new Point(100, 100)
            });

            States.Add(new StateViewModel
            {
                Name = "Drag border to connect",
                Location = new Point(350, 100)
            });

            States.Add(new StateViewModel
            {
                Name = "Double click me to edit",
                Location = new Point(250, 250)
            });

            Transitions.Add(new TransitionViewModel
            {
                Source = States[0],
                Target = States[1]
            });
        }

        private void DisconnectState(StateViewModel state)
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

        public ICommand DeleteSelectionCommand { get; }
        public ICommand DisconnectStateCommand { get; }
        public ICommand CreateTransitionCommand { get; }

        private void DeleteSelection()
        {
            var selected = SelectedStates.ToList();

            for (int i = 0; i < selected.Count; i++)
            {
                States.Remove(selected[i]);
            }
        }
    }
}

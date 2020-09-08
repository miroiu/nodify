using System;
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
            DisconnectStateCommand = new RequeryCommand<StateViewModel>(x => DisconnectState(x), x => !IsRunning && x.Transitions.Count > 0);
            DisconnectSelectionCommand = new RequeryCommand(() => SelectedStates.ForEach(x => DisconnectState(x)), () => !IsRunning && SelectedStates.Count > 0 && Transitions.Count > 0);
            DeleteSelectionCommand = new RequeryCommand(() => SelectedStates.ToList().ForEach(x => x.IsEditable.Then(() => States.Remove(x))), () => !IsRunning && (SelectedStates.Count > 1 || (SelectedStates.Count == 1 && SelectedStates[0].IsEditable)));

            AddStateCommand = new RequeryCommand<Point>(p => States.Add(new StateViewModel
            {
                Name = "New State",
                IsRenaming = true,
                Location = p
            }), p => !IsRunning);

            CreateTransitionCommand = new DelegateCommand<(object Source, object? Target)>(s => Transitions.Add(new TransitionViewModel
            {
                Source = (StateViewModel)s.Source,
                Target = (StateViewModel)s.Target!
            }), s => !IsRunning && s.Target is StateViewModel target && target != s.Source && !target.Transitions.Contains(s.Source));

            DeleteTransitionCommand = new RequeryCommand<TransitionViewModel>(t => Transitions.Remove(t), t => !IsRunning);

            RunCommand = new RequeryCommand(() => IsRunning.Then(Stop).Else(Start), () => Transitions.Count > 0);
            PauseCommand = new RequeryCommand(() => IsPaused.Then(_stateMachine!.Unpause).Else(_stateMachine.Pause), () => IsRunning && _stateMachine != null);
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

        private string? _name = "State Machine";
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private bool _isPaused;
        public bool IsPaused
        {
            get => _isPaused;
            set => SetProperty(ref _isPaused, value);
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
        public INodifyCommand RunCommand { get; }
        public INodifyCommand PauseCommand { get; }

        private StateMachine? _stateMachine;
        private StateViewModel? _activeState;

        protected virtual void OnCreateDefaultNodes()
        {
            States.Insert(0, new StateViewModel
            {
                Name = "Enter",
                Location = new Point(100, 100),
                IsEditable = false
            });
        }

        public async void Start()
        {
            _stateMachine = StateMachineHelper.From(this);
            _stateMachine.OnCompleted += OnCompleted;
            _stateMachine.OnTransition += ActivateState;
            _stateMachine.OnPausedChanged += b => IsPaused = b;

            IsRunning = true;
            await _stateMachine.Start();
        }

        private void OnCompleted(bool aborted)
        {
            IsRunning = false;
            IsPaused = false;
            if (_activeState != null)
            {
                _activeState.IsActive = false;
            }
        }

        private void ActivateState(Guid id)
        {
            if (_activeState != null)
            {
                _activeState.IsActive = false;
            }

            _activeState = States.FirstOrDefault(st => st.Id == id);

            if (_activeState != null)
            {
                _activeState.IsActive = true;
            }
        }

        public void Stop()
        {
            _stateMachine?.Stop();
            IsRunning = false;
        }
    }
}

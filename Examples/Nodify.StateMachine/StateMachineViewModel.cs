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
            Runner = new StateMachineRunnerViewModel(this);

            Blackboard = new BlackboardViewModel()
            {
                Actions = new NodifyObservableCollection<BlackboardItemReferenceViewModel>(BlackboardDescriptor.GetAvailableItems<IBlackboardAction>()),
                Conditions = new NodifyObservableCollection<BlackboardItemReferenceViewModel>(BlackboardDescriptor.GetAvailableItems<IBlackboardCondition>())
            };

            Transitions.WhenAdded(c =>
            {
                c.Source.Transitions.Add(c.Target);
                c.Target.Transitions.Add(c.Source);
            })
            .WhenRemoved(c =>
            {
                c.Source.Transitions.Remove(c.Target);
                c.Target.Transitions.Remove(c.Source);
            })
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

            OnCreateDefaultKeys();
            OnCreateDefaultNodes();

            RenameStateCommand = new RequeryCommand(() => SelectedStates[0].IsRenaming = true, () => SelectedStates.Count == 1 && SelectedStates[0].IsEditable);
            DisconnectStateCommand = new RequeryCommand<StateViewModel>(x => DisconnectState(x), x => !IsRunning && x.Transitions.Count > 0);
            DisconnectSelectionCommand = new RequeryCommand(() => SelectedStates.ForEach(x => DisconnectState(x)), () => !IsRunning && SelectedStates.Count > 0 && Transitions.Count > 0);
            DeleteSelectionCommand = new RequeryCommand(() => SelectedStates.ToList().ForEach(x => x.IsEditable.Then(() => States.Remove(x))), () => !IsRunning && (SelectedStates.Count > 1 || (SelectedStates.Count == 1 && SelectedStates[0].IsEditable)));

            AddStateCommand = new RequeryCommand<Point>(p => States.Add(new StateViewModel
            {
                Name = "New State",
                IsRenaming = true,
                Location = p,
                ActionReference = Blackboard.Actions.Count > 0 ? Blackboard.Actions[0] : null
            }), p => !IsRunning);

            CreateTransitionCommand = new DelegateCommand<(object Source, object? Target)>(s => Transitions.Add(new TransitionViewModel
            {
                Source = (StateViewModel)s.Source,
                Target = (StateViewModel)s.Target!
            }), s => !IsRunning && s.Source is StateViewModel source && s.Target is StateViewModel target && target != s.Source && target != States[0] && !source.Transitions.Contains(s.Target));

            DeleteTransitionCommand = new RequeryCommand<TransitionViewModel>(t => Transitions.Remove(t), t => !IsRunning);

            RunCommand = new RequeryCommand(() => IsRunning.Then(Runner.Stop).Else(Runner.Start), () => Transitions.Count > 0);
            PauseCommand = new RequeryCommand(Runner.TogglePause, () => IsRunning);
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

        private StateViewModel? _selectedState;
        public StateViewModel? SelectedState
        {
            get => _selectedState;
            set => SetProperty(ref _selectedState, value);
        }

        private string? _name = "State Machine";
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool IsRunning => Runner.State != MachineState.Stopped;
        public bool IsPaused => Runner.State == MachineState.Paused;

        public TransitionViewModel PendingTransition { get; }
        public StateMachineRunnerViewModel Runner { get; }
        public BlackboardViewModel Blackboard { get; }

        public INodifyCommand DeleteTransitionCommand { get; }
        public INodifyCommand DeleteSelectionCommand { get; }
        public INodifyCommand DisconnectStateCommand { get; }
        public INodifyCommand DisconnectSelectionCommand { get; }
        public INodifyCommand RenameStateCommand { get; }
        public INodifyCommand AddStateCommand { get; }
        public INodifyCommand CreateTransitionCommand { get; }
        public INodifyCommand RunCommand { get; }
        public INodifyCommand PauseCommand { get; }

        public void DisconnectState(StateViewModel state)
        {
            var transitions = Transitions.Where(t => t.Source == state || t.Target == state).ToList();
            transitions.ForEach(t => Transitions.Remove(t));
        }

        protected virtual void OnCreateDefaultNodes()
        {
            States.Insert(0, new StateViewModel
            {
                Name = "Enter",
                Location = new Point(100, 100),
                IsEditable = false
            });

            var currentDelayKey = Blackboard.Keys.First(k => k.Name == "Current Delay");
            var originalDelayKey = Blackboard.Keys.First(k => k.Name == "Original Delay");
            var welcomeKey = Blackboard.Keys.First(k => k.Name == "Welcome");

            States.Add(new StateViewModel
            {
                Name = "Set delay value",
                Location = new Point(300, 100),
                ActionReference = Blackboard.Actions.FirstOrDefault(a => a.Type == typeof(SetKeyValueAction))
            });

            States[1].Action!.Input[0].Value = currentDelayKey;
            States[1].Action!.Input[1].ValueIsKey = false;
            States[1].Action!.Input[1].Type = BlackboardKeyType.Integer;
            States[1].Action!.Input[1].Value = 100;

            States.Add(new StateViewModel
            {
                Name = "Set new delay",
                Location = new Point(380, 250),
                ActionReference = Blackboard.Actions.FirstOrDefault(a => a.Type == typeof(SetStateDelayAction))
            });

            States[2].Action!.Input[0].Value = currentDelayKey;

            States.Add(new StateViewModel
            {
                Name = "Reset delay",
                Location = new Point(300, 350),
                ActionReference = Blackboard.Actions.FirstOrDefault(a => a.Type == typeof(CopyKeyAction))
            });

            States[3].Action!.Input[0].Value = originalDelayKey;
            States[3].Action!.Input[1].Value = currentDelayKey;

            States.Add(new StateViewModel
            {
                Name = "Set original delay",
                Location = new Point(200, 250),
                ActionReference = Blackboard.Actions.FirstOrDefault(a => a.Type == typeof(SetStateDelayAction))
            });

            States[4].Action!.Input[0].Value = originalDelayKey;

            Transitions.Add(new TransitionViewModel
            {
                Source = States[0],
                Target = States[1],
                ConditionReference = Blackboard.Conditions.FirstOrDefault(c => c.Type == typeof(HasKeyCondition))
            });

            Transitions[0].Condition!.Input[0].Value = welcomeKey;

            Transitions.Add(new TransitionViewModel
            {
                Source = States[1],
                Target = States[2],
                ConditionReference = Blackboard.Conditions.FirstOrDefault(c => c.Type == typeof(AreEqualCondition))
            });

            Transitions[1].Condition!.Input[0].Value = welcomeKey;
            Transitions[1].Condition!.Input[1].ValueIsKey = false;
            Transitions[1].Condition!.Input[1].Type = BlackboardKeyType.String;
            Transitions[1].Condition!.Input[1].Value = currentDelayKey.Name;

            Transitions.Add(new TransitionViewModel
            {
                Source = States[2],
                Target = States[3]
            });

            Transitions.Add(new TransitionViewModel
            {
                Source = States[3],
                Target = States[4]
            });

            Transitions.Add(new TransitionViewModel
            {
                Source = States[4],
                Target = States[1]
            });
        }

        protected virtual void OnCreateDefaultKeys()
        {
            Blackboard.Keys.Add(new BlackboardKeyViewModel
            {
                Name = "Current Delay",
                Type = BlackboardKeyType.Integer,
                Value = 1000
            });

            Blackboard.Keys.Add(new BlackboardKeyViewModel
            {
                Name = "Original Delay",
                Type = BlackboardKeyType.Integer,
                Value = 1000
            });

            Blackboard.Keys.Add(new BlackboardKeyViewModel
            {
                Name = "Welcome",
                Type = BlackboardKeyType.String,
                Value = "Current Delay"
            });
        }
    }
}

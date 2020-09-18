using System;
using System.Collections.Generic;
using System.Linq;

namespace Nodify.StateMachine
{
    public class StateMachineRunnerViewModel : ObservableObject
    {
        private StateMachine? _stateMachine;
        private StateViewModel? _activeState;
        private TransitionViewModel? _activeTransition;
        private readonly DebugBlackboardDecorator _debugger = new DebugBlackboardDecorator();
        private readonly Blackboard _original = new Blackboard();

        protected StateMachineViewModel StateMachineViewModel { get; }

        private MachineState _state;
        public MachineState State
        {
            get => _state;
            protected set => SetProperty(ref _state, value);
        }

        private int _nodesVisited;
        public int NodesVisited
        {
            get => _nodesVisited;
            protected set => SetProperty(ref _nodesVisited, value);
        }

        public StateMachineRunnerViewModel(StateMachineViewModel stateMachineViewModel)
        {
            StateMachineViewModel = stateMachineViewModel;
            _debugger.ValueChanged += OnBlackboardKeyValueChanged;
        }

        private void OnBlackboardKeyValueChanged(BlackboardKey key, object? newValue)
        {
            if (_stateMachine != null && _stateMachine.State != MachineState.Stopped)
            {
                var existing = StateMachineViewModel.Blackboard.Keys.FirstOrDefault(k => k.Name == key.Name && k.Type == key.Type);
                if (existing != null)
                {
                    existing.Value = newValue;
                }
            }
        }

        #region State Machine Actions

        public async void Start()
        {
            NodesVisited = 0;

            _stateMachine = new StateMachine(StateMachineViewModel.States[0].Id, CreateStates(StateMachineViewModel.States), CreateBlackboard(StateMachineViewModel.Blackboard));

            _stateMachine.StateTransition += HandleStateTransition;
            _stateMachine.StateChanged += HandleStateChange;

            await _stateMachine.Start();
        }

        public void Stop()
        {
            _stateMachine?.Stop();
            _stateMachine = null;
        }

        private void HandleStateTransition(Guid from, Guid to)
        {
            NodesVisited++;

            SetActiveStateAndTransition(false);

            _activeTransition = StateMachineViewModel.Transitions.FirstOrDefault(t => t.Source.Id == from);
            _activeState = StateMachineViewModel.States.FirstOrDefault(st => st.Id == to);

            SetActiveStateAndTransition(true);
        }

        private void SetActiveStateAndTransition(bool value)
        {
            if (_activeState != null)
            {
                _activeState.IsActive = value;
            }

            if (_activeTransition != null)
            {
                _activeTransition.IsActive = value;
            }
        }

        private void HandleStateChange(MachineState newState)
        {
            if (newState == MachineState.Stopped)
            {
                SetActiveStateAndTransition(false);
                ResetBlackboardToOriginal();
            }

            State = newState;
        }

        private void ResetBlackboardToOriginal()
        {
            var keys = StateMachineViewModel.Blackboard.Keys;
            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                key.Value = _original.GetObject(key.Name);
            }
        }

        public void TogglePause()
        {
            if (State == MachineState.Paused)
            {
                _stateMachine?.Unpause();
            }
            else if (State != MachineState.Stopped)
            {
                _stateMachine?.Pause();
            }
        }

        #endregion

        #region Initialize State Machine

        private IEnumerable<State> CreateStates(IEnumerable<StateViewModel> states)
            => states.Select(s => new DebugStateDecorator(new State(s.Id, CreateTransitions(s), CreateAction(s.Action))));

        private IEnumerable<Transition> CreateTransitions(StateViewModel state)
        {
            var transitions = StateMachineViewModel.Transitions.Where(t => t.Source == state).ToList();
            var result = new List<Transition>(transitions.Count);

            for (int i = 0; i < transitions.Count; i++)
            {
                var transition = transitions[i];
                var tr = new Transition(transition.Source.Id, transition.Target.Id, CreateCondition(transition.Condition));
                result.Add(new DebugTransitionDecorator(tr));
            }

            return result;
        }

        private IBlackboardCondition? CreateCondition(BlackboardItemViewModel? condition)
        {
            if (condition?.Type != null && typeof(IBlackboardCondition).IsAssignableFrom(condition.Type))
            {
                // TODO: DI Container
                var result = (IBlackboardCondition?)Activator.CreateInstance(condition.Type);

                InitializeKeys(condition.Input, result, condition.Type);

                return result;
            }

            return default;
        }

        private IBlackboardAction? CreateAction(BlackboardItemViewModel? action)
        {
            if (action?.Type != null && typeof(IBlackboardAction).IsAssignableFrom(action.Type))
            {
                // TODO: DI Container
                var result = (IBlackboardAction?)Activator.CreateInstance(action.Type);

                InitializeKeys(action.Input, result, action.Type);
                InitializeKeys(action.Output, result, action.Type);

                return result;
            }

            return default;
        }

        private void InitializeKeys(NodifyObservableCollection<BlackboardKeyViewModel> keys, object? instance, Type type)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                var vm = keys[i];
                var key = CreateActionValue(vm);

                // TODO: Property cache
                if (vm.PropertyName != null)
                {
                    var prop = type.GetProperty(vm.PropertyName);

                    if (prop?.CanWrite ?? false)
                    {
                        prop.SetValue(instance, key);
                    }
                }
            }
        }

        private Blackboard CreateBlackboard(BlackboardViewModel blackboard)
        {
            Blackboard result = new Blackboard();
            for (int i = 0; i < blackboard.Keys.Count; i++)
            {
                var key = blackboard.Keys[i];
                if (!string.IsNullOrWhiteSpace(key.Name))
                {
                    result.Set(new BlackboardKey(key.Name, key.Type), key.Value);
                }
            }

            result.CopyTo(_original);

            _debugger.Attach(result);
            return _debugger;
        }

        private BlackboardProperty CreateActionValue(BlackboardKeyViewModel key)
        {
            if (key.Value is BlackboardKeyViewModel bkv)
            {
                return new BlackboardProperty(new BlackboardKey(bkv.Name, bkv.Type));
            }

            return new BlackboardProperty(key.Value);
        }

        #endregion
    }
}

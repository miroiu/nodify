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
        }

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

        private IEnumerable<State> CreateStates(IEnumerable<StateViewModel> states)
            => states.Select(s => new DebugStateDecorator(new State(s.Id, CreateTransitions(s), CreateAction(s.Action))));

        private IEnumerable<Transition> CreateTransitions(StateViewModel state)
            => state.Transitions.Select(t => new DebugTransitionDecorator(CreateTransition(state, t)));

        private IBlackboardAction? CreateAction(ActionViewModel? action)
        {
            var type = action?.Type;
            if (type != null && typeof(IBlackboardAction).IsAssignableFrom(type))
            {
                return (IBlackboardAction?)Activator.CreateInstance(type, new object[] { CreateKeys(action!.Input), CreateKeys(action.Output) });
            }

            return default;
        }

        private IEnumerable<BlackboardKey> CreateKeys(NodifyObservableCollection<BlackboardKeyViewModel> keys)
            => keys.Select(k => CreateBlackboardKey(k));

        private Transition CreateTransition(StateViewModel from, StateViewModel to)
        {
            var transition = StateMachineViewModel.Transitions.FirstOrDefault(t => t.Source.Id == from.Id && t.Target.Id == to.Id);

            if (transition != null)
            {
                return new Transition(from.Id, to.Id);
            }

            return new Transition(from.Id, to.Id);
        }

        private Blackboard CreateBlackboard(BlackboardViewModel blackboard)
        {
            Blackboard result = new Blackboard();
            for (int i = 0; i < blackboard.Keys.Count; i++)
            {
                var key = blackboard.Keys[i];
                if (!string.IsNullOrWhiteSpace(key.Name))
                {
                    result.Set(CreateBlackboardKey(key), key.Value);
                }
            }

            return new DebugBlackboardDecorator(result);
        }

        private BlackboardKey CreateBlackboardKey(BlackboardKeyViewModel key)
        {
            if (key.Type == BlackboardKeyType.Key && key.Value is BlackboardKeyViewModel bkv)
            {
                return CreateBlackboardKey(bkv);
            }

            return new BlackboardKey(key.Name, key.Type);
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
            }

            State = newState;
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
    }
}

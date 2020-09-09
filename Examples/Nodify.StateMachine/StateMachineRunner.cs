using System;
using System.Collections.Generic;
using System.Linq;

namespace Nodify.StateMachine
{
    public class StateMachineRunner : ObservableObject
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

        public StateMachineRunner(StateMachineViewModel stateMachineViewModel)
        {
            StateMachineViewModel = stateMachineViewModel;
        }

        public async void Start()
        {
            NodesVisited = 0;

            _stateMachine = new StateMachine(StateMachineViewModel.States[0].Id, CreateStates(StateMachineViewModel.States), new DebugBlackboardDecorator(new Blackboard()));

            _stateMachine.StateTransition += HandleStateTransition;
            _stateMachine.StateChanged += HandleStateChange;

            await _stateMachine.Start();
        }

        public void Stop()
            => _stateMachine?.Stop();

        private IEnumerable<State> CreateStates(IEnumerable<StateViewModel> states)
            => states.Select(s => new DebugStateDecorator(new ActionState(s.Id, CreateTransitions(s), b =>
            {
                // TODO: Action to execute for a certain state
            })));

        private IEnumerable<Transition> CreateTransitions(StateViewModel state)
            => state.Transitions.Select(t => new DebugTransitionDecorator(new ConditionTransition(state.Id, t.Id, b =>
            {
                // TODO: Condition for transition to continue
                return true;
            })));

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

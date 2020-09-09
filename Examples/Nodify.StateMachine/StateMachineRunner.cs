using System;
using System.Collections.Generic;
using System.Linq;

namespace Nodify.StateMachine
{
    public class StateMachineRunner : ObservableObject
    {
        private StateMachine? _stateMachine;
        private StateViewModel? _activeState;

        protected StateMachineViewModel StateMachineViewModel { get; }

        private MachineState _state;
        public MachineState State
        {
            get => _state;
            protected set => SetProperty(ref _state, value);
        }

        public StateMachineRunner(StateMachineViewModel stateMachineViewModel)
        {
            StateMachineViewModel = stateMachineViewModel;
        }

        public async void Start()
        {
            _stateMachine = new StateMachine(StateMachineViewModel.States[0].Id, CreateStates(StateMachineViewModel.States));

            _stateMachine.NextStateChanging += HandleNextState;
            _stateMachine.RunningStateChanged += HandleStateChange;

            await _stateMachine.Start();
        }

        public void Stop()
            => _stateMachine?.Stop();

        private IEnumerable<State> CreateStates(IEnumerable<StateViewModel> states)
            => states.Select(s => new ActionState(s.Id, CreateTransitions(s), () =>
            {
                // TODO: Action to execute for a certain state
            }));

        private IEnumerable<Transition> CreateTransitions(StateViewModel state)
            => state.Transitions.Select(t => new ConditionTransition(state.Id, t.Id, () =>
            {
                // TODO: Condition for transition to continue
                return true;
            }));

        private void HandleNextState(Guid id)
        {
            if (_activeState != null)
            {
                _activeState.IsActive = false;
            }

            _activeState = StateMachineViewModel.States.FirstOrDefault(st => st.Id == id);

            if (_activeState != null)
            {
                _activeState.IsActive = true;
            }
        }

        private void HandleStateChange(MachineState newState)
        {
            if (newState == MachineState.Stopped && _activeState != null)
            {
                _activeState.IsActive = false;
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

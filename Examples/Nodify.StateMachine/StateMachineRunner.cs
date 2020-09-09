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

        private MachineStatus _state;
        public MachineStatus State
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

            _stateMachine.StateChanged += HandleStateChange;
            _stateMachine.StatusChanged += HandleStatusChange;

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

        private void HandleStateChange(Guid from, Guid to)
        {
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

        private void HandleStatusChange(MachineStatus newState)
        {
            if (newState == MachineStatus.Stopped)
            {
                SetActiveStateAndTransition(false);
            }

            State = newState;
        }

        public void TogglePause()
        {
            if (State == MachineStatus.Paused)
            {
                _stateMachine?.Unpause();
            }
            else if (State != MachineStatus.Stopped)
            {
                _stateMachine?.Pause();
            }
        }
    }
}

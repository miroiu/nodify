using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public enum MachineStatus
    {
        Stopped,
        Running,
        Paused,
    }

    public delegate void StateTransitionEventHandler(Guid from, Guid to);
    public delegate void StatusChangedEventHandler(MachineStatus newStatus);

    public class StateMachine
    {
        private readonly Dictionary<Guid, State> _states;

        public State Root { get; }
        public IReadOnlyList<State> States { get; }
        public MachineStatus? RunningState { get; private set; }

        // param = aborted
        public event StatusChangedEventHandler? StatusChanged;
        public event StateTransitionEventHandler? StateChanged;

        public StateMachine(Guid root, IEnumerable<State> states)
        {
            States = new List<State>(states);

            _states = States.ToDictionary(x => x.Id, x => x);

            if (!_states.ContainsKey(root))
            {
                throw new ArgumentException(nameof(root));
            }

            Root = _states[root];
        }

        public async Task Start()
        {
            if (ChangeState(MachineStatus.Running))
            {
                // Skip root state
                State? previous = Root;
                State? current = GetNext(Root);

                while (RunningState != MachineStatus.Stopped && current != null)
                {
                    if (RunningState == MachineStatus.Paused)
                    {
                        await Task.Delay(10);
                    }
                    else
                    {
                        StateChanged?.Invoke(previous.Id, current.Id);
                        previous = current;

                        await current.Activate();
                        current = GetNext(current);
                    }
                }

                ChangeState(MachineStatus.Stopped);
            }
        }

        private State? GetNext(State current)
        {
            var transitions = current.Transitions;
            for (int i = 0; i < transitions.Count; i++)
            {
                var transition = transitions[i];
                if (transition.CanActivate())
                {
                    return _states[transition.To];
                }
            }

            return default;
        }

        public void Stop()
            => ChangeState(MachineStatus.Stopped);

        public void Pause()
            => ChangeState(MachineStatus.Paused);

        public void Unpause()
            => ChangeState(MachineStatus.Running);

        private bool ChangeState(MachineStatus newState)
        {
            if (newState == MachineStatus.Running || (RunningState != null && RunningState != newState))
            {
                RunningState = newState;
                StatusChanged?.Invoke(newState);
                return true;
            }

            return false;
        }
    }
}

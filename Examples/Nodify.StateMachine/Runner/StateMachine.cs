using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public enum MachineState
    {
        Stopped,
        Running,
        Paused,
    }

    public class StateMachine
    {
        private readonly Dictionary<Guid, State> _states;

        public State Root { get; }
        public IReadOnlyList<State> States { get; }
        public MachineState? RunningState { get; private set; }

        // param = aborted
        public event Action<MachineState>? RunningStateChanged;
        public event Action<Guid>? NextStateChanging;

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
            if (ChangeState(MachineState.Running))
            {
                // Skip root state
                State? current = GetNext(Root);

                while (RunningState != MachineState.Stopped && current != null)
                {
                    if (RunningState == MachineState.Paused)
                    {
                        await Task.Delay(10);
                    }
                    else
                    {
                        NextStateChanging?.Invoke(current.Id);

                        await current.Activate();
                        current = GetNext(current);
                    }
                }

                ChangeState(MachineState.Stopped);
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
            => ChangeState(MachineState.Stopped);

        public void Pause()
            => ChangeState(MachineState.Paused);

        public void Unpause()
            => ChangeState(MachineState.Running);

        private bool ChangeState(MachineState newState)
        {
            if (newState == MachineState.Running || (RunningState != null && RunningState != newState))
            {
                RunningState = newState;
                RunningStateChanged?.Invoke(newState);
                return true;
            }

            return false;
        }
    }
}

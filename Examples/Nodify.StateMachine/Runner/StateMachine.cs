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

    public delegate void StateTransitionEventHandler(Guid from, Guid to);
    public delegate void StateChangedEventHandler(MachineState newStatus);

    public class StateMachine
    {
        private readonly Dictionary<Guid, State> _states;

        public State Root { get; }
        public MachineState? State { get; private set; }
        public Blackboard Blackboard { get; } = new Blackboard();

        // param = aborted
        public event StateChangedEventHandler? StateChanged;
        public event StateTransitionEventHandler? StateTransition;

        public StateMachine(Guid root, IEnumerable<State> states, Blackboard? blackboard = default)
        {
            _states = states.ToDictionary(x => x.Id, x => x);

            if (!_states.ContainsKey(root))
            {
                throw new ArgumentException(nameof(root));
            }

            Root = _states[root];

            if (blackboard != null)
            {
                Blackboard = blackboard;
            }
        }

        public async Task Start()
        {
            if (ChangeState(MachineState.Running))
            {
                // Skip root state
                State? previous = Root;
                State? current = await GetNext(Root);

                while (State != MachineState.Stopped && current != null)
                {
                    if (State == MachineState.Paused)
                    {
                        await Task.Delay(10);
                    }
                    else
                    {
                        StateTransition?.Invoke(previous.Id, current.Id);
                        previous = current;

                        await current.Activate(Blackboard);
                        current = await GetNext(current);
                    }
                }

                ChangeState(MachineState.Stopped);
            }
        }

        private async Task<State?> GetNext(State current)
        {
            var transitions = current.Transitions;
            for (int i = 0; i < transitions.Count; i++)
            {
                var transition = transitions[i];
                if (_states.TryGetValue(transition.To, out var result) && await transition.CanActivate(Blackboard))
                {
                    return result;
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
            if (newState == MachineState.Running || (State != null && State != newState))
            {
                State = newState;
                StateChanged?.Invoke(newState);
                return true;
            }

            return false;
        }
    }
}

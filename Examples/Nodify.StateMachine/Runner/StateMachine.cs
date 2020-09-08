using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Guid, State> _states;

        public StateMachine(Guid root, IEnumerable<State> states)
        {
            Root = root;
            States = new List<State>(states);

            _states = States.ToDictionary(x => x.Id, x => x);

            if (!_states.ContainsKey(Root))
            {
                throw new ArgumentException(nameof(root));
            }
        }

        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }
        public Guid Root { get; }
        public IReadOnlyList<State> States { get; }

        public event Action<Guid>? OnTransition;

        // param = aborted
        public event Action<bool>? OnCompleted;
        public event Action<bool>? OnPausedChanged;

        public async Task Start()
        {
            IsRunning = true;
            State? current = GetNext(_states[Root]);

            while (IsRunning && current != null)
            {
                if (IsPaused)
                {
                    await Task.Delay(10);
                }
                else
                {
                    OnTransition?.Invoke(current.Id);

                    await current.Activate();
                    current = GetNext(current);
                }
            }

            IsRunning = false;
            OnCompleted?.Invoke(false);
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
        {
            IsRunning = false;
            OnCompleted?.Invoke(true);
        }

        public void Pause()
        {
            IsPaused = true;
            OnPausedChanged?.Invoke(true);
        }

        public void Unpause()
        {
            IsPaused = false;
            OnPausedChanged?.Invoke(false);
        }
    }
}

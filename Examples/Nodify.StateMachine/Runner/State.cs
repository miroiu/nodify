using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public abstract class State
    {
        public Guid Id { get; }

        protected State(Guid id, IEnumerable<Transition> transitions)
        {
            Id = id;
            Transitions = new List<Transition>(transitions);
        }

        public IReadOnlyList<Transition> Transitions { get; }

        public abstract Task Activate();
    }
}

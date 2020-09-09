using System;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class Transition
    {
        public Transition(Guid from, Guid to)
        {
            From = from;
            To = to;
        }

        public Guid From { get; }
        public Guid To { get; }

        public virtual Task<bool> CanActivate(Blackboard blackboard)
            => Task.FromResult(true);
    }
}

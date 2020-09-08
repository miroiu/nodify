using System;

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

        public virtual bool CanActivate() => true;
    }
}

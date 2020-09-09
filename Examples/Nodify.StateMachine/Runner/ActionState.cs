using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class ActionState : State
    {
        private readonly Action<Blackboard> _action;

        public ActionState(Guid id, IEnumerable<Transition> transitions, Action<Blackboard> action) : base(id, transitions)
        {
            _action = action;
        }

        public override Task Activate(Blackboard blackboard)
        {
            _action?.Invoke(blackboard);
            return Task.CompletedTask;
        }
    }
}

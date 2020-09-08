using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class ActionState : State
    {
        private readonly Action _action;

        public ActionState(Guid id, IEnumerable<Transition> transitions, Action action) : base(id, transitions)
        {
            _action = action;
        }

        public override async Task Activate()
        {
            await Task.Delay(1000);
            _action?.Invoke();
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class ConditionTransition : Transition
    {
        private readonly Func<Blackboard, bool>? _condition;

        public ConditionTransition(Guid from, Guid to, Func<Blackboard, bool>? condition = default) : base(from, to)
        {
            _condition = condition;
        }

        public override Task<bool> CanActivate(Blackboard blackboard)
            => Task.FromResult(_condition?.Invoke(blackboard) ?? true);
    }
}

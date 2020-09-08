using System;

namespace Nodify.StateMachine
{
    public class ConditionTransition : Transition
    {
        private readonly Func<bool>? _condition;

        public ConditionTransition(Guid from, Guid to, Func<bool>? condition = default) : base(from, to)
        {
            _condition = condition;
        }

        public override bool CanActivate()
            => _condition?.Invoke() ?? true;
    }
}

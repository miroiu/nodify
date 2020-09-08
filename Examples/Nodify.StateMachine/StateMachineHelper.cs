using System.Linq;

namespace Nodify.StateMachine
{
    public static class StateMachineHelper
    {
        public static StateMachine From(StateMachineViewModel stateMachine)
        {
            return new StateMachine(stateMachine.States[0].Id, stateMachine.States.Select(s => new ActionState(s.Id, s.Transitions.Select(t => new ConditionTransition(s.Id, t.Id, () =>
            {
                // TODO: Condition for transition to continue
                return true;
            })), () =>
            {
                // TODO: Action to execute for a certain state
            })));
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public enum BooleanOperator
    {
        And,
        Or
    }

    public class BlackboardConditionSet : IBlackboardCondition
    {
        public BlackboardConditionSet(IEnumerable<IBlackboardCondition> conditions, BooleanOperator op)
        {
            Conditions = new List<IBlackboardCondition>(conditions);
            Operator = op;
        }

        public IReadOnlyList<IBlackboardCondition> Conditions { get; }
        public BooleanOperator Operator { get; set; }

        public async Task<bool> Evaluate(Blackboard blackboard)
        {
            bool result = true;

            if (Operator == BooleanOperator.And)
            {
                for (int i = 0; i < Conditions.Count; i++)
                {
                    result &= await Conditions[i].Evaluate(blackboard);
                }
            }
            else if (Operator == BooleanOperator.Or)
            {
                for (int i = 0; i < Conditions.Count; i++)
                {
                    result |= await Conditions[i].Evaluate(blackboard);
                }
            }

            return result;
        }
    }
}

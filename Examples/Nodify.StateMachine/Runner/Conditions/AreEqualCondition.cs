using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Are Equal")]
    public class AreEqualCondition : IBlackboardCondition
    {
        [BlackboardProperty(BlackboardKeyType.Object, CanChangeType = true)]
        public BlackboardProperty Left { get; set; }

        [BlackboardProperty(BlackboardKeyType.Object, CanChangeType = true)]
        public BlackboardProperty Right { get; set; }

        public Task<bool> Evaluate(Blackboard blackboard)
        {
            var left = blackboard.GetObject(Left);
            var right = blackboard.GetObject(Right);

            // TODO: Equality
            return Task.FromResult(Equals(left, right));
        }
    }
}

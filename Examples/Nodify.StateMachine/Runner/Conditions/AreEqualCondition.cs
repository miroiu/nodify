using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Are Equal")]
    public class AreEqualCondition : IBlackboardCondition
    {
        [BlackboardKey(BlackboardKeyType.Object)]
        public BlackboardKey Left { get; set; }

        [BlackboardKey(BlackboardKeyType.Object)]
        public BlackboardKey Right { get; set; }

        public Task<bool> Evaluate(Blackboard blackboard)
        {
            var left = blackboard.GetObject(Left);
            var right = blackboard.GetObject(Right);

            return Task.FromResult(left == right);
        }
    }
}

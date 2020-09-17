using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Has Key")]
    public class HasKeyCondition : IBlackboardCondition
    {
        [BlackboardKey(BlackboardKeyType.Object)]
        public BlackboardKey Key { get; set; }

        public Task<bool> Evaluate(Blackboard blackboard)
            => Task.FromResult(blackboard.HasKey(Key));
    }
}

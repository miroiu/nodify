using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Has Key")]
    public class HasKeyCondition : IBlackboardCondition
    {
        [BlackboardKey("Key Name", BlackboardKeyType.String)]
        public BlackboardKey Key { get; set; }

        public Task<bool> Evaluate(Blackboard blackboard)
        {
            var keyName = blackboard.GetObject<string>(Key);

            if (keyName != null)
            {
                return Task.FromResult(blackboard.HasKey(keyName));
            }

            return Task.FromResult(false);
        }
    }
}

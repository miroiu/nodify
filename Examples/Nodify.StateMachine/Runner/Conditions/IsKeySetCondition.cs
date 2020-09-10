using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class IsKeySetCondition : BlackboardCondition
    {
        public IsKeySetCondition(IEnumerable<BlackboardKey> input, IEnumerable<BlackboardKey> output) : base(input, output)
        {

        }

        public IsKeySetCondition(BlackboardKey key) : base(key)
        {

        }

        public override Task<bool> Evaluate(Blackboard blackboard)
            => Task.FromResult(blackboard.HasKey(Input[0]));
    }
}

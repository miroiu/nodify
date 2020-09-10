using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class CopyBlackboardKeyAction : BlackboardAction
    {
        public CopyBlackboardKeyAction(IEnumerable<BlackboardKey> input, IEnumerable<BlackboardKey> output) : base(input, output)
        {
        }

        public CopyBlackboardKeyAction(BlackboardKey source, BlackboardKey target) : base(new List<BlackboardKey> { source, target })
        {
        }

        public override Task Execute(Blackboard blackboard)
        {
            var source = blackboard.GetObject(Input[0]);
            blackboard.Set(Input[1], source);

            return Task.CompletedTask;
        }
    }
}

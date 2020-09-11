using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardAction("Copy Blackboard Key")]
    [BlackboardKey("Source", BlackboardKeyType.Key, BlackboardKeyUsage.Input)]
    [BlackboardKey("Target", BlackboardKeyType.Key, BlackboardKeyUsage.Input)]
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
            var source = Input[0];
            var target = Input[1];

            if (source != target)
            {
                var value = blackboard.GetObject(source);
                blackboard.Set(target, value);
            }

            return Task.CompletedTask;
        }
    }
}

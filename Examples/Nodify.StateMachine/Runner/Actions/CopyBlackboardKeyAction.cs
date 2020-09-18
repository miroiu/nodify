using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Copy Key")]
    public class CopyBlackboardKeyAction : IBlackboardAction
    {
        [BlackboardKey("Source", BlackboardKeyType.Object)]
        public BlackboardKey Source { get; set; }

        [BlackboardKey("Target", BlackboardKeyType.Object)]
        public BlackboardKey Target { get; set; }

        public Task Execute(Blackboard blackboard)
        {
            if (Source != Target)
            {
                var value = blackboard[Source];
                blackboard[Target] =  value;
            }

            return Task.CompletedTask;
        }
    }
}

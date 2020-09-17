using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Copy Blackboard Key")]
    public class CopyBlackboardKeyAction : IBlackboardAction
    {
        [BlackboardKey("Source", BlackboardKeyType.Object)]
        public BlackboardKey Source { get; set; }

        [BlackboardKey("Target", BlackboardKeyType.Object)]
        public BlackboardKey Target { get; set; }

        public Task Execute(Blackboard blackboard)
        {
            if (Source != Target && Source.IsValid() && Target.IsValid())
            {
                var value = blackboard.GetObject(Source);
                blackboard.Set(Target, value);
            }

            return Task.CompletedTask;
        }
    }
}

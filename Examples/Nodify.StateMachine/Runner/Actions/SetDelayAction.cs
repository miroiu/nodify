using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Set State Delay")]
    public class SetDelayAction : IBlackboardAction
    {
        [BlackboardKey("Delay", BlackboardKeyType.Integer)]
        public BlackboardKey Delay { get; set; }

        [BlackboardKey("Success", BlackboardKeyType.Boolean, BlackboardKeyUsage.Output)]
        public BlackboardKey Success { get; set; }

        public Task Execute(Blackboard blackboard)
        {
            if (Delay.IsValid())
            {
                var delay = blackboard.GetValue<int>(Delay);

                if (delay.HasValue)
                {
                    blackboard.Set(DebugBlackboardDecorator.StateDelayKey, delay);
                }

                if (Success.IsValid())
                {
                    blackboard.Set(Success, delay.HasValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}

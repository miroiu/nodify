using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    [BlackboardItem("Set State Delay")]
    public class SetStateDelayAction : IBlackboardAction
    {
        [BlackboardKey("Delay", BlackboardKeyType.Integer)]
        public BlackboardKey Delay { get; set; }

        [BlackboardKey("Success", BlackboardKeyType.Boolean, BlackboardKeyUsage.Output)]
        public BlackboardKey Success { get; set; }

        public Task Execute(Blackboard blackboard)
        {
            var delay = blackboard.GetValue<int>(Delay);

            if (delay.HasValue)
            {
                blackboard[DebugBlackboardDecorator.StateDelayKey] = delay;
            }

            blackboard[Success] = delay.HasValue;

            return Task.CompletedTask;
        }
    }
}

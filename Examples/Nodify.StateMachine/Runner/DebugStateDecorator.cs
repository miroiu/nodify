using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class DebugStateDecorator : State
    {
        private readonly State _state;

        public DebugStateDecorator(State state) : base(state.Id, state.Transitions)
        {
            _state = state;
        }

        public override async Task Activate(Blackboard blackboard)
        {
            int? delay = blackboard.GetValue<int>(DebugBlackboardDecorator.StateDelayKey);

            if (delay > 0)
            {
                await Task.Delay(delay.Value);
            }

            await _state.Activate(blackboard);
        }
    }
}

using System.Threading.Tasks;

namespace Nodify.StateMachine
{
    public class DebugTransitionDecorator : Transition
    {
        private readonly Transition _transition;

        public DebugTransitionDecorator(Transition transition) : base(transition.From, transition.To)
        {
            _transition = transition;
        }

        public override async Task<bool> CanActivate(Blackboard blackboard)
        {
            int? delay = blackboard.GetValue<int>(DebugBlackboardDecorator.TransitionDelayKey);

            if (delay > 0)
            {
                await Task.Delay(delay.Value);
            }

            return await _transition.CanActivate(blackboard);
        }
    }
}

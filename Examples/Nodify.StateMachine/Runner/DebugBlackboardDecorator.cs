namespace Nodify.StateMachine
{
    public class DebugBlackboardDecorator : Blackboard
    {
        public const string StateDelayKey = "__state.delay";
        public const string TransitionDelayKey = "__transition.delay";

        private readonly Blackboard _blackboard;

        public DebugBlackboardDecorator(Blackboard blackboard)
        {
            _blackboard = blackboard;

            Set(StateDelayKey, 1000);
        }

        public override void Clear(string key)
        {
            _blackboard.Clear(key);
        }

        public override void Clear()
        {
            _blackboard.Clear();
        }

        public override T? GetObject<T>(string key)
            where T : class
        {
            return _blackboard.GetObject<T>(key);
        }

        public override T? GetValue<T>(string key)
        {
            return _blackboard.GetValue<T>(key);
        }

        public override void Set(string key, object value)
        {
            _blackboard.Set(key, value);
        }
    }
}

using System.Collections.Generic;

namespace Nodify.StateMachine
{
    public class DebugBlackboardDecorator : Blackboard
    {
        public static BlackboardKey StateDelayKey { get; } = "__state.delay";
        public static BlackboardKey TransitionDelayKey { get; } = "__transition.delay";

        private readonly Blackboard _blackboard;

        public DebugBlackboardDecorator(Blackboard blackboard)
        {
            _blackboard = blackboard;

            Set(StateDelayKey, 1000);
        }

        public override IReadOnlyCollection<BlackboardKey> Keys => _blackboard.Keys;

        public override void Clear(BlackboardKey key)
        {
            _blackboard.Clear(key);
        }

        public override void Clear()
        {
            _blackboard.Clear();
        }

        public override T? GetObject<T>(BlackboardKey key)
            where T : class
        {
            return _blackboard.GetObject<T>(key);
        }

        public override T? GetValue<T>(BlackboardKey key)
        {
            return _blackboard.GetValue<T>(key);
        }

        public override void Set(BlackboardKey key, object? value)
        {
            _blackboard.Set(key, value);
        }

        public override bool HasKey(BlackboardKey key)
        {
            return _blackboard.HasKey(key);
        }

        public override object? GetObject(BlackboardKey key)
        {
            return _blackboard.GetObject(key);
        }
    }
}

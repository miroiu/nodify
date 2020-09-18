using System;
using System.Collections.Generic;

namespace Nodify.StateMachine
{
    public class DebugBlackboardDecorator : Blackboard
    {
        public static BlackboardKey StateDelayKey { get; } = "__state.delay";
        public static BlackboardKey TransitionDelayKey { get; } = "__transition.delay";

        private Blackboard? _blackboard;

        public event Action<BlackboardKey, object?>? ValueChanged;

        public DebugBlackboardDecorator(Blackboard? blackboard = default)
            => Attach(blackboard);

        public override IReadOnlyCollection<BlackboardKey> Keys => _blackboard?.Keys ?? Array.Empty<BlackboardKey>();

        public override void Remove(BlackboardKey key)
            => _blackboard?.Remove(key);

        public override void Clear()
            => _blackboard?.Clear();

        public override T? GetObject<T>(BlackboardKey key) where T : class
            => _blackboard?.GetObject<T>(key);

        public override T? GetValue<T>(BlackboardKey key)
            => _blackboard?.GetValue<T>(key);

        public override void Set(BlackboardKey key, object? value)
        {
            _blackboard?.Set(key, value);
            ValueChanged?.Invoke(key, value);
        }

        public override bool HasKey(BlackboardKey key)
            => _blackboard?.HasKey(key) ?? false;

        public override object? GetObject(BlackboardKey key)
            => _blackboard?.GetObject(key);

        public virtual void Attach(Blackboard? blackboard)
        {
            _blackboard = blackboard;

            Set(StateDelayKey, 100);
        }
    }
}

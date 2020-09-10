using System.Collections.Generic;

namespace Nodify.StateMachine
{
    public class Blackboard
    {
        private readonly Dictionary<BlackboardKey, object?> _objects = new Dictionary<BlackboardKey, object?>();

        public virtual IReadOnlyCollection<BlackboardKey> Keys
            => _objects.Keys;

        public virtual T? GetValue<T>(BlackboardKey key)
            where T : struct
        {
            if (_objects.TryGetValue(key, out var value) && value is T result)
            {
                return result;
            }

            return default;
        }

        public virtual T? GetObject<T>(BlackboardKey key)
            where T : class
        {
            if (_objects.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return default;
        }

        public virtual object? GetObject(BlackboardKey key)
        {
            if(_objects.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public virtual void Set(BlackboardKey key, object? value)
            => _objects[key] = value;

        public virtual bool HasKey(BlackboardKey key)
            => _objects.ContainsKey(key);

        public virtual void Clear(BlackboardKey key)
            => _objects.Remove(key);

        public virtual void Clear()
            => _objects.Clear();
    }
}

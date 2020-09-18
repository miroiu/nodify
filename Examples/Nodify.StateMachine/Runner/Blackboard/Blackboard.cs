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
            if (_objects.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public virtual void Set(BlackboardKey key, object? value)
            => _objects[key] = value;

        public virtual bool HasKey(BlackboardKey key)
            => _objects.ContainsKey(key);

        public virtual void Remove(BlackboardKey key)
            => _objects.Remove(key);

        public virtual void Clear()
            => _objects.Clear();

        public void CopyTo(Blackboard newBlackboard)
        {
            foreach (var kvp in _objects)
            {
                newBlackboard.Set(kvp.Key, kvp.Value);
            }
        }

        public object? this[BlackboardKey key]
        {
            get => GetObject(key);
            set => Set(key, value);
        }

        public T? GetValue<T>(BlackboardProperty value) where T : struct
            => value.IsValue ? value.GetValue<T>() : GetValue<T>(value.Key);

        public T? GetObject<T>(BlackboardProperty value) where T : class
            => value.IsValue ? value.GetObject<T>() : GetObject<T>(value.Key);

        public object? GetObject(BlackboardProperty value)
            => value.IsValue ? value.Value : GetObject(value.Key);
    }

    public static class BlackboardExtensions
    {
        public static bool IsValid(this BlackboardKey key)
            => key != BlackboardKey.Invalid;

        public static bool IsValid(this BlackboardProperty action)
            => action != BlackboardProperty.Invalid;
    }
}

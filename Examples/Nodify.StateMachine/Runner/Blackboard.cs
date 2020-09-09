using System.Collections.Generic;

namespace Nodify.StateMachine
{
    public class Blackboard
    {
        private readonly Dictionary<string, object> _objects = new Dictionary<string, object>();

        public virtual T? GetValue<T>(string key)
            where T : struct
        {
            if (_objects.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            return default;
        }

        public virtual T? GetObject<T>(string key)
            where T : class
        {
            if (_objects.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return default;
        }

        public virtual void Set(string key, object value)
            => _objects[key] = value;

        public virtual void Clear(string key)
            => _objects.Remove(key);

        public virtual void Clear()
            => _objects.Clear();
    }
}

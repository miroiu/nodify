using System;
using System.Collections.Generic;

namespace Nodify.StateMachine
{
    public readonly struct BlackboardKey : IEquatable<BlackboardKey>
    {
        private static readonly Dictionary<string, BlackboardKey> _keys = new Dictionary<string, BlackboardKey>();

        public BlackboardKey(string name, string typeName)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Type = typeName;

            _keys[name] = this;
        }

        public BlackboardKey(string name, Type type) : this(name, type.Name)
        {
        }

        public BlackboardKey(string name) : this(name, typeof(object).Name)
        {
        }

        public readonly string Name;
        public readonly string Type;

        public static implicit operator BlackboardKey(string name)
        {
            if (_keys.TryGetValue(name, out var key))
            {
                return key;
            }

            return new BlackboardKey(name);
        }

        public static implicit operator string(BlackboardKey key)
            => key.Name;

        public override bool Equals(object? obj)
            => obj is BlackboardKey bk && bk.Equals(this);

        public override int GetHashCode()
            => Name.GetHashCode();

        public bool Equals(BlackboardKey other)
            => other.Name == Name;

        public static bool operator ==(BlackboardKey left, BlackboardKey right)
            => left.Equals(right);

        public static bool operator !=(BlackboardKey left, BlackboardKey right)
            =>  !(left == right);
    }
}

using System;
using System.Diagnostics;

namespace Nodify.StateMachine
{
    public enum BlackboardKeyType
    {
        Boolean,
        Integer,
        Double,
        String,
        Object
    }

    [DebuggerDisplay("{Name}: {Type}")]
    public readonly struct BlackboardKey : IEquatable<BlackboardKey>
    {
        public static BlackboardKey Invalid { get; } = new BlackboardKey();

        public BlackboardKey(string name, BlackboardKeyType type)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Type = type;
        }

        public BlackboardKey(string name) : this(name, BlackboardKeyType.Object)
        {
        }

        public readonly string Name;
        public readonly BlackboardKeyType Type;

        public static implicit operator BlackboardKey(string name)
            => new BlackboardKey(name);

        public static implicit operator string(BlackboardKey key)
            => key.Name;

        public override bool Equals(object? obj)
            => obj is BlackboardKey bk && bk.Equals(this);

        public override int GetHashCode()
            => Name?.GetHashCode() ?? -1;

        public bool Equals(BlackboardKey other)
            => other.Name == Name;

        public static bool operator ==(BlackboardKey left, BlackboardKey right)
            => left.Equals(right);

        public static bool operator !=(BlackboardKey left, BlackboardKey right)
            => !(left == right);
    }
}

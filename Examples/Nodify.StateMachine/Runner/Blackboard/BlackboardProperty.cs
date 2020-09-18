using System;
using System.Diagnostics;

namespace Nodify.StateMachine
{
    [DebuggerDisplay("{IsKey ? Key : Value}")]
    public struct BlackboardProperty : IEquatable<BlackboardProperty>
    {
        public static BlackboardProperty Invalid { get; } = new BlackboardProperty();

        public BlackboardProperty(BlackboardKey key)
        {
            Key = key;
            Value = default;
        }

        public BlackboardProperty(object? value)
        {
            Key = BlackboardKey.Invalid;
            Value = value;
        }

        public BlackboardKey Key { get; }
        public object? Value { get; }

        public bool IsKey => Key.IsValid();
        public bool IsValue => !IsKey;

        public static implicit operator BlackboardKey(BlackboardProperty action)
            => action.Key;

        public override bool Equals(object? obj)
            => obj is BlackboardProperty action && action.Equals(this);

        public override int GetHashCode()
            => IsKey ? Key.GetHashCode() : Value?.GetHashCode() ?? -1;

        public bool Equals(BlackboardProperty other)
            => IsKey == other.IsKey && IsValue == other.IsValue && Key == other.Key && Value == other.Value;

        public static bool operator ==(BlackboardProperty left, BlackboardProperty right)
            => left.Equals(right);

        public static bool operator !=(BlackboardProperty left, BlackboardProperty right)
            => !(left == right);

        public T? GetValue<T>() where T : struct
            => Value is T result ? result : default;

        public T? GetObject<T>() where T : class
            => Value as T;
    }
}

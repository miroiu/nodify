using System;

namespace Nodify.StateMachine
{
    public enum BlackboardKeyUsage
    {
        Input,
        Output
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class BlackboardKeyAttribute : Attribute
    {
        public BlackboardKeyAttribute(string name, BlackboardKeyType type = BlackboardKeyType.Object, BlackboardKeyUsage usage = BlackboardKeyUsage.Input)
        {
            Name = name;
            Type = type;
            Usage = usage;
        }

        public string Name { get; }
        public object? DefaultValue { get; set; }
        public BlackboardKeyType Type { get; }
        public BlackboardKeyUsage Usage { get; }
    }
}

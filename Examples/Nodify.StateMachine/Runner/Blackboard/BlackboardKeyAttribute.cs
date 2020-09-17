using System;

namespace Nodify.StateMachine
{
    public enum BlackboardKeyUsage
    {
        Input,
        Output
    }
    
    /// <summary>
    /// Properties decorated with this attribute must always be of type <see cref="BlackboardKey"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class BlackboardKeyAttribute : Attribute
    {
        /// <summary>
        /// Properties decorated with this attribute must always be of type <see cref="BlackboardKey"/>.
        /// </summary>
        /// <param name="name">The display name of the key.</param>
        /// <param name="type">The data type of the value that the key refers to.</param>
        public BlackboardKeyAttribute(string name, BlackboardKeyType type = BlackboardKeyType.Object, BlackboardKeyUsage usage = BlackboardKeyUsage.Input)
        {
            Name = name;
            Type = type;
            Usage = usage;
        }

        public string Name { get; }
        public BlackboardKeyType Type { get; }
        public BlackboardKeyUsage Usage { get; }
    }
}

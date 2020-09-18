using System;

namespace Nodify.StateMachine
{
    public enum BlackboardKeyUsage
    {
        Input,
        Output
    }

    /// <summary>
    /// Properties decorated with this attribute must always be of type <see cref="BlackboardProperty"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class BlackboardPropertyAttribute : Attribute
    {
        /// <summary>
        /// Properties decorated with this attribute must always be of type <see cref="BlackboardProperty"/>.
        /// </summary>
        /// <param name="name">The display name of the key.</param>
        /// <param name="type">The data type of the value that the key refers to.</param>
        public BlackboardPropertyAttribute(string? name, BlackboardKeyType type = BlackboardKeyType.Object)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Properties decorated with this attribute must always be of type <see cref="BlackboardProperty"/>.
        /// </summary>
        /// <param name="type">The data type of the value that the key refers to.</param>
        public BlackboardPropertyAttribute(BlackboardKeyType type = BlackboardKeyType.Object) : this(null, type)
        {

        }

        public string? Name { get; }
        public BlackboardKeyType Type { get; }
        public BlackboardKeyUsage Usage { get; set; }
        public bool CanChangeType { get; set; }
    }
}

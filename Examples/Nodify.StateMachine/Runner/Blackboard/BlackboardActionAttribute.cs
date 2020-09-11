using System;

namespace Nodify.StateMachine
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class BlackboardActionAttribute : Attribute
    {
        public BlackboardActionAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}

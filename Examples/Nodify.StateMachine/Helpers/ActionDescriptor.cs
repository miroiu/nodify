using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nodify.StateMachine
{
    public class ActionDescriptor
    {
        private class KeyDescription
        {
            public KeyDescription(string displayName, string propertyName, BlackboardKeyType type)
            {
                DisplayName = displayName;
                PropertyName = propertyName;
                Type = type;
            }

            public string DisplayName { get; set; }
            public string PropertyName { get; }
            public BlackboardKeyType Type { get; }
        }

        private class ActionDescription
        {
            public string? Name { get; set; }
            public List<KeyDescription> Input { get; } = new List<KeyDescription>();
            public List<KeyDescription> Output { get; } = new List<KeyDescription>();
        }

        public static ActionViewModel? GetAction(ActionReferenceViewModel? actionRef)
        {
            if (actionRef?.Type != null && typeof(IBlackboardAction).IsAssignableFrom(actionRef.Type))
            {
                var description = GetDescription(actionRef.Type);

                var input = description.Input.Select(d => new BlackboardKeyViewModel
                {
                    Name = d.DisplayName,
                    Type = d.Type,
                    PropertyName = d.PropertyName
                });

                var output = description.Output.Select(d => new BlackboardKeyViewModel
                {
                    Name = d.DisplayName,
                    Type = d.Type,
                    PropertyName = d.PropertyName
                });

                return new ActionViewModel
                {
                    Name = actionRef.Name,
                    Type = actionRef.Type,
                    Input = new NodifyObservableCollection<BlackboardKeyViewModel>(input),
                    Output = new NodifyObservableCollection<BlackboardKeyViewModel>(output),
                };
            }

            return default;
        }

        public static ActionReferenceViewModel GetReference(Type type)
        {
            if (typeof(IBlackboardAction).IsAssignableFrom(type))
            {
                var desc = GetDescription(type);

                return new ActionReferenceViewModel
                {
                    Name = desc.Name,
                    Type = type
                };
            }

            throw new NotSupportedException(type.Name);
        }

        private static readonly Dictionary<Type, ActionDescription> _descriptions = new Dictionary<Type, ActionDescription>();
        private static ActionDescription GetDescription(Type type)
        {
            if (!_descriptions.TryGetValue(type, out var description))
            {
                var actionAttr = type.GetCustomAttribute<BlackboardActionAttribute>();

                var desc = new ActionDescription
                {
                    Name = actionAttr?.DisplayName ?? type.Name
                };

                var props = type.GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    var prop = props[i];
                    var keyAttr = prop.GetCustomAttribute<BlackboardKeyAttribute>();

                    if (keyAttr != null)
                    {
                        if (keyAttr.Usage == BlackboardKeyUsage.Input)
                        {
                            desc.Input.Add(new KeyDescription(keyAttr.Name, prop.Name, keyAttr.Type));
                        }
                        else
                        {
                            desc.Output.Add(new KeyDescription(keyAttr.Name, prop.Name, keyAttr.Type));
                        }
                    }
                }

                return desc;
            }

            return description;
        }
    }
}

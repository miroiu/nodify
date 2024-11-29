using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nodify.StateMachine
{
    public static class BlackboardDescriptor
    {
        private class KeyDescription
        {
            public KeyDescription(string displayName, string propertyName, BlackboardKeyType type, bool canChangeType)
            {
                DisplayName = displayName;
                PropertyName = propertyName;
                Type = type;
                CanChangeType = canChangeType;
            }

            public string DisplayName { get; }
            public string PropertyName { get; }
            public BlackboardKeyType Type { get; }
            public bool CanChangeType { get; }
        }

        private class ItemDescription
        {
            public string? Name { get; set; }
            public List<KeyDescription> Input { get; } = new List<KeyDescription>();
            public List<KeyDescription> Output { get; } = new List<KeyDescription>();
        }

        public static BlackboardItemViewModel? GetItem(BlackboardItemReferenceViewModel? actionRef)
        {
            if (actionRef?.Type != null)
            {
                var description = GetDescription(actionRef.Type);

                var input = description.Input.Select(d => new BlackboardKeyViewModel
                {
                    Name = d.DisplayName,
                    Type = d.Type,
                    PropertyName = d.PropertyName,
                    CanChangeType = d.CanChangeType,
                    ValueIsKey = true
                });

                var output = description.Output.Select(d => new BlackboardKeyViewModel
                {
                    Name = d.DisplayName,
                    Type = d.Type,
                    PropertyName = d.PropertyName,
                    CanChangeType = d.CanChangeType,
                    ValueIsKey = true
                });

                return new BlackboardItemViewModel
                {
                    Name = actionRef.Name,
                    Type = actionRef.Type,
                    Input = new NodifyObservableCollection<BlackboardKeyViewModel>(input),
                    Output = new NodifyObservableCollection<BlackboardKeyViewModel>(output),
                };
            }

            return default;
        }

        public static BlackboardItemReferenceViewModel GetReference(Type type)
        {
            var desc = GetDescription(type);

            return new BlackboardItemReferenceViewModel
            {
                Name = desc.Name,
                Type = type
            };
        }

        private static readonly Dictionary<Type, ItemDescription> _descriptions = new Dictionary<Type, ItemDescription>();
        private static ItemDescription GetDescription(Type type)
        {
            if (!_descriptions.TryGetValue(type, out var description))
            {
                var actionAttr = type.GetCustomAttribute<BlackboardItemAttribute>();

                var desc = new ItemDescription
                {
                    Name = actionAttr?.DisplayName ?? type.Name
                };

                var props = type.GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    var prop = props[i];
                    var keyAttr = prop.GetCustomAttribute<BlackboardPropertyAttribute>();

                    if (keyAttr != null)
                    {
                        var key = new KeyDescription(keyAttr.Name ?? prop.Name, prop.Name, keyAttr.Type, keyAttr.CanChangeType);

                        if (keyAttr.Usage == BlackboardKeyUsage.Input)
                        {
                            desc.Input.Add(key);
                        }
                        else
                        {
                            desc.Output.Add(key);
                        }
                    }
                }

                _descriptions.Add(type, desc);

                return desc;
            }

            return description;
        }

        public static List<BlackboardItemReferenceViewModel> GetAvailableItems<T>()
        {
            var result = new List<BlackboardItemReferenceViewModel>();
            var ourType = typeof(T);

            var types = ourType.Assembly.GetTypes();

            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type.IsClass && !type.IsAbstract && ourType.IsAssignableFrom(type) && type.GetCustomAttribute<BlackboardItemAttribute>() != null)
                {
                    result.Add(GetReference(type));
                }
            }

            return result;
        }
    }
}

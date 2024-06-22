using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nodify.UndoRedo
{
    public interface IPropertyAccessor
    {
        object? GetValue(object instance);
        void SetValue(object instance, object? value);
        bool CanRead { get; }
        bool CanWrite { get; }
    }

    public sealed class PropertyAccessor<TInstanceType, TPropertyType> : IPropertyAccessor where TInstanceType : class
    {
        private readonly Func<TInstanceType, TPropertyType> _getter;
        private readonly Action<TInstanceType, TPropertyType> _setter;

        public bool CanRead { get; }
        public bool CanWrite { get; }

        public PropertyAccessor(Func<TInstanceType, TPropertyType> getter, Action<TInstanceType, TPropertyType> setter)
        {
            _getter = getter;
            _setter = setter;

            CanRead = getter != null;
            CanWrite = setter != null;
        }

        public object? GetValue(object instance)
            => _getter((TInstanceType)instance);

        public void SetValue(object instance, object? value)
            => _setter((TInstanceType)instance, (TPropertyType)value!);
    }

    public class PropertyCache
    {
        private static readonly Dictionary<string, IPropertyAccessor> _properties = new Dictionary<string, IPropertyAccessor>();

        public static IPropertyAccessor Get(Type type, string name)
        {
            string propKey = $"{type.FullName}.{name}";
            if (!_properties.TryGetValue(propKey, out var result))
            {
                var prop = type.GetProperty(name);
                result = Create(type, prop!);

                _properties.Add(propKey, result);
            }

            return result;
        }

        public static IPropertyAccessor Get<T>(string name)
            => Get(typeof(T), name);

        private static IPropertyAccessor Create(Type type, PropertyInfo property)
        {
            Delegate? getterInvocation = default;
            Delegate? setterInvocation = default;

            if (property.CanRead)
            {
                MethodInfo getMethod = property.GetGetMethod(true)!;
                Type getterType = typeof(Func<,>).MakeGenericType(type, property.PropertyType);
                getterInvocation = Delegate.CreateDelegate(getterType, getMethod);
            }

            if (property.CanWrite)
            {
                MethodInfo setMethod = property.GetSetMethod(true)!;
                Type setterType = typeof(Action<,>).MakeGenericType(type, property.PropertyType);
                setterInvocation = Delegate.CreateDelegate(setterType, setMethod);
            }

            Type adapterType = typeof(PropertyAccessor<,>).MakeGenericType(type, property.PropertyType);

            return (IPropertyAccessor)Activator.CreateInstance(adapterType, getterInvocation, setterInvocation)!;
        }
    }
}

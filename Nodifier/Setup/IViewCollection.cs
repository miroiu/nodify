using System;
using System.Collections.Generic;

namespace Nodifier
{
    public interface IViewCollection
    {
        void Add(Type model, Type view);
        Type? Get(Type vmType);
    }

    public class ViewCollection : IViewCollection
    {
        private readonly Dictionary<Type, Type> _interfaceMappings = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _concreteMappings = new Dictionary<Type, Type>();

        public void Add(Type model, Type view)
        {
            if (model.IsInterface)
            {
                _interfaceMappings[model] = view;
            }
            else
            {
                _concreteMappings[model] = view;
            }
        }

        public Type? Get(Type vmType)
        {
            if (!_concreteMappings.TryGetValue(vmType, out Type? result) && !_interfaceMappings.TryGetValue(vmType, out result))
            {
                foreach (var intr in vmType.GetInterfaces())
                {
                    if (_interfaceMappings.TryGetValue(intr, out result))
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}

using System;

namespace NodifyBlueprint
{
    public class BlueprintSchema : GraphSchema
    {
        public new static readonly IGraphSchema Default = new BlueprintSchema();

        public override bool CanConnect(IConnector source, IConnector target)
        {
            bool result = base.CanConnect(source, target);

            if (result)
            {
                var srcType = GetValueType(source);
                var targetType = GetValueType(target);

                result = source is IOutputConnector && IsAssignable(srcType, targetType) || target is IOutputConnector && IsAssignable(targetType, srcType);
            }

            return result;
        }

        private static bool IsAssignable(Type? srcType, Type? targetType)
        {
            return targetType != null && targetType.IsAssignableFrom(srcType);
        }

        private static Type? GetValueType(IConnector conn)
        {
            var type = conn.GetType();
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            return type;
        }
    }
}
